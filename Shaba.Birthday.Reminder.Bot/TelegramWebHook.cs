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
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Shaba.Birthday.Reminder.Bot.Services;
using Shaba.Birthday.Reminder.Bot.Services.Services;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

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
			await _botService.SetWebhookAsync(handleUpdateFunctionUrl);
			return new OkObjectResult($"Changed to {handleUpdateFunctionUrl}");
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

				/*var update = JsonConvert.DeserializeObject<Update>(requestBody);
				if (update.Type == UpdateType.Unknown)
				{
					return new BadRequestResult();
				}


				try
				{
					var firstName = update.Message?.From?.FirstName;
					var lastName = update.Message?.From?.LastName;
					var username = update.Message?.From?.Username;

					await _botService.SendText(update.Message?.From?.Id ?? update?.CallbackQuery?.From?.Id, $"I can say only: \"Hello, {firstName ?? lastName ?? username}\"");

				}
				catch (ApiRequestException e)
				{
					return new BadRequestResult();
				}

				_logger.LogInformation("C# HTTP trigger function processed a request.");
				return new OkResult();*/
			}
			catch (Exception e)
			{
				return new BadRequestResult(); ;
			}
			return new OkResult();
		}
	}
}

