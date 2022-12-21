using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Shaba.Birthday.Reminder.Repository;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using User = Shaba.Birthday.Reminder.BusinessLogic.Data.User;

namespace Shaba.Birthday.Reminder.Bot.Services.Services
{
	public class MessageProcessor : IMessageProcessor
	{
		private readonly IUserRepository _userRepository;
		private readonly IBotService _botService;
		private readonly ICommandResolver _commandResolver;
		private readonly ILogger _logger;

		public MessageProcessor(IUserRepository userRepository, IBotService botService, ICommandResolver commandResolver, ILogger logger)
		{
			_userRepository = userRepository;
			_botService = botService;
			_commandResolver = commandResolver;
			_logger = logger;
		}

		public async Task Process(string updateString)
		{
			var update = JsonConvert.DeserializeObject<Update>(updateString);

			/*if (update.Type == UpdateType.CallbackQuery && !string.IsNullOrEmpty(update.CallbackQuery?.Data))
			{
				await _botService.AnswerQueryCallback(update.CallbackQuery?.Id);
				await _botService.SendText(450081254, update.CallbackQuery.Data);
				var idExitstForTimeZone = TimeZoneInfo.GetSystemTimeZones().Any(x=>x.Id == update.CallbackQuery.Data);
				if (idExitstForTimeZone)
				{
					await _botService.SendText(update.CallbackQuery.From.Id,
						$"You are successful registrated, {update.CallbackQuery.From.FirstName}");
				}
				else
				{
					await RequestRegionOfTimezone(update);
				}

				await _botService.DeleteMessage(update.CallbackQuery.From.Id, update.CallbackQuery.Message.MessageId);
			}*/

			//var user = null//await _userRepository.Get((int)(update.Message?.From?.Id ?? update.CallbackQuery.From.Id));
			
			if (update.Type == UpdateType.Unknown)
			{
				return;
			}

			var user = await GetOrCreateUser(update);

			try
			{
				await _commandResolver.Resolve(update, user);
			}
			catch (ApiRequestException e)
			{
				_logger.LogError(e, e.Message);
				if (e.ErrorCode == 403) //blocked
				{
					await _userRepository.BlockUser(user);
				}
				throw;
			}
			catch (Exception e)
			{
				_logger.LogError(e, e.Message);
				throw;
			}
		}

		private async Task<User> GetOrCreateUser(Update update)
		{
			var user = await _userRepository.Get(update.Message?.From?.Id ?? update.CallbackQuery?.From.Id ?? throw new InvalidOperationException());

			if (user == null)
			{
				user = new User()
				{
					Id = update.Message?.From?.Id ?? update.CallbackQuery?.From.Id ?? throw new InvalidOperationException(),
					FirstName = (update.Message?.From?.FirstName ?? update.CallbackQuery?.From?.FirstName) ?? throw new InvalidOperationException() ,
					LastName = (update.Message?.From?.LastName ?? update.CallbackQuery?.From?.LastName) ?? throw new InvalidOperationException(),
					Username = (update.Message?.From?.Username ?? update.CallbackQuery?.From?.Username) ?? throw new InvalidOperationException(),
				};
				await _userRepository.Add(user);
			}

			return user;
		}

		private async Task RequestRegionOfTimezone(Update update)
		{
			var text = update.CallbackQuery?.Data ?? "";
			var timeSpan = TimeSpan.TryParse(text, out TimeSpan result);
			var neededTimeZones = TimeZoneInfo.GetSystemTimeZones().Where(x => x.BaseUtcOffset == result).ToList();
			var dictionary = neededTimeZones.ToDictionary(x => x.DisplayName,
					y => y.Id);
			var ccc = CreateInlineKeyboardButton(dictionary, 1);

			await _botService.SendText(update?.CallbackQuery?.From?.Id ?? 0, "Please send contact for register you in our system.", replyMarkup: ccc);
		}

		private IReplyMarkup CreateInlineKeyboardButton(Dictionary<string, string> buttonList, int columns)
		{
			var rows = (int)Math.Ceiling((double)buttonList.Count / (double)columns);
			var buttons = new InlineKeyboardButton[rows][];

			for (var i = 0; i < buttons.Length; i++)
			{
				buttons[i] = buttonList
					.Skip(i * columns)
					.Take(columns)
					.Select(direction => InlineKeyboardButton.WithCallbackData(
						direction.Key, direction.Value)
					)
					.ToArray();
			}
			return new InlineKeyboardMarkup(buttons);
		}
	}
}
