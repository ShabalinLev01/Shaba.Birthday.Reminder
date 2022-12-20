using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Shaba.Birthday.Reminder.Bot.Services;

namespace Shaba.Birthday.Reminder.Bot
{
	public class TelegramWebHook
	{
		private readonly IBotService _botService;
		private readonly IMessageProcessor _messageProcessor;
		private readonly ILogger<TelegramWebHook> _logger;

		public TelegramWebHook(ILogger<TelegramWebHook> log, IBotService botService, IMessageProcessor messageProcessor)
		{
			_botService = botService;
			_messageProcessor = messageProcessor;

			_logger = log;
		}

		[FunctionName("Setup")]
		public async Task<OkObjectResult> RunAsync([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
		{
			var handleUpdateFunctionUrl = req.GetEncodedUrl().Replace("Setup", "TelegramWebHook",
				ignoreCase: true, culture: CultureInfo.InvariantCulture);
			try
			{
				await _botService.SetWebhookAsync(handleUpdateFunctionUrl);
				return new OkObjectResult($"Changed to {handleUpdateFunctionUrl}");
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}


		[FunctionName("TelegramWebHook")]
		[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
		public async Task<IActionResult> Run(
			[HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req)
		{
			try
			{
				using var sr = new StreamReader(req.Body);
				string requestBody = await sr.ReadToEndAsync();
				await _messageProcessor.Process(requestBody);
			}
			catch (Exception e)
			{
				_logger.LogError(e, e.Message);
				return new BadRequestResult();
			}
			return new OkResult();
		}
	}
}

