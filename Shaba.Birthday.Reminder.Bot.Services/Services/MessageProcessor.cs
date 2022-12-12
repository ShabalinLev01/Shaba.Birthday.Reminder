using Newtonsoft.Json;
using Shaba.Birthday.Reminder.Repository;
using Telegram.Bot.Exceptions;
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
			if (update.Type == UpdateType.Unknown)
			{
				return;
			}
			
			//var user = null//await _userRepository.Get((int)(update.Message?.From?.Id ?? update.CallbackQuery.From.Id));

			if (true/*user == null*/)
			{
				await RequestContactForRegister(update);
			}

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
			KeyboardButton button = KeyboardButton.WithRequestContact("Send contact");
			
			ReplyKeyboardMarkup keyboard = new ReplyKeyboardMarkup(button);
			
			await _botService.SendText(update?.Message?.Chat.Id, "Please send contact for register you in our system.", replyMarkup: keyboard);
		}
		private async Task RequestLocationForRegister(Update update)
		{
			KeyboardButton button = KeyboardButton.WithRequestLocation("Send location");  
			
			ReplyKeyboardMarkup keyboard = new ReplyKeyboardMarkup(button);
			
			// https://github.com/TelegramBots/Telegram.Bot/blob/master/src/Telegram.Bot/TelegramBotClient.cs#L506
			await _botService.SendText(update?.Message?.Chat.Id, "Please send location for allocate your time zone", replyMarkup: keyboard);
		}
	}
}
