using Newtonsoft.Json;
using Shaba.Birthday.Reminder.Repository;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Requests;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Shaba.Birthday.Reminder.Bot.Services.Services
{
	public class MessageProcessor : IMessageProcessor
	{
		private readonly IUserRepository _userRepository;
		private readonly IBotService _botService;

		public MessageProcessor(IUserRepository userRepository, IBotService botService)
		{
			_userRepository = userRepository;
			_botService = botService;
		}

		public async Task Process(string updateString)
		{
			var update = JsonConvert.DeserializeObject<Update>(updateString);
			await _botService.SendText(450081254, update.Type.ToString());

			if (update.Type == UpdateType.Unknown)
			{
				return;
			}

			if (update.Type == UpdateType.CallbackQuery && !string.IsNullOrEmpty(update.CallbackQuery?.Data))
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
			}

			//var user = null//await _userRepository.Get((int)(update.Message?.From?.Id ?? update.CallbackQuery.From.Id));

			if (update.Type == UpdateType.Message)
			{
				if (update.Message?.Contact != null)
				{
					await RequestTimezone(update);
				}
				else
				{
					await RequestContactForRegister(update);
				}
			}

			/*update.Message.Location.*/

			try
			{
				//await _commandResolver.Resolve(update, user);
			}
			catch (ApiRequestException e)
			{
				if (e.ErrorCode == 403) //blocked
				{
					//await _userRepository.BlockUser(user);
				}

				throw;
			}
		}

		private async Task RequestContactForRegister(Update update)
		{
			var button = KeyboardButton.WithRequestContact("Send contact");

			var keyboard = new ReplyKeyboardMarkup(button);

			await _botService.SendText(update?.Message?.Chat?.Id ?? 0, "Please send contact for register you in our system.", replyMarkup: keyboard);
		}
		private async Task RequestTimezone(Update update)
		{
			var dictionary = TimeZoneInfo.GetSystemTimeZones().GroupBy(x => x.BaseUtcOffset)
				.ToDictionary(x => $"UTC " +
								   $"{new string(x.Key.Hours > 0 ? "+" : x.Key.Hours == 0 ? "±" : "")}" +
								   $"{x.Key.Hours}:{Math.Abs(x.Key.Minutes).ToString("00")}",
					y => y.Key.ToString());
			var ccc = CreateInlineKeyboardButton(dictionary, 4);

			await _botService.SendText(update?.Message?.Chat?.Id ?? 0, "Please select your timezone", replyMarkup: ccc);
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
