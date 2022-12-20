using Shaba.Birthday.Reminder.Bot.Services.Services;
using Shaba.Birthday.Reminder.Repository;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using User = Shaba.Birthday.Reminder.Repository.Data.User;

namespace Shaba.Birthday.Reminder.Bot.Services.Commands
{
	public class ContactCommand : ICommand
	{
		private readonly IBotService _botService;
		private readonly IUserRepository _userRepository;

		public ContactCommand(IBotService botService, IUserRepository userRepository)
		{
			_botService = botService;
			_userRepository = userRepository;
		}
		public async Task Execute(Update update, User user, string arg = null)
		{
			var button = KeyboardButton.WithRequestContact("Send contact");
			var keyboard = new ReplyKeyboardMarkup(button)
			{
				ResizeKeyboard = true
			};

			await _botService.SendText(update?.Message?.Chat?.Id ?? 0, "Please send contact for register you in our system.", replyMarkup: keyboard);
		}
	}
}
