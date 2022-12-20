using Shaba.Birthday.Reminder.Bot.Services.Services;
using Shaba.Birthday.Reminder.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using User = Shaba.Birthday.Reminder.Repository.Data.User;

namespace Shaba.Birthday.Reminder.Bot.Services.Commands
{
	public class LocationCommand : ICommand
	{
		private readonly IBotService _botService;
		private readonly IUserRepository _userRepository;

		public LocationCommand(IBotService botService, IUserRepository userRepository)
		{
			_botService = botService;
			_userRepository = userRepository;
		}
		public async Task Execute(Update update, User user, string arg = null)
		{
			var button = KeyboardButton.WithRequestLocation("Share location");
			var keyboard = new ReplyKeyboardMarkup(button)
			{
				ResizeKeyboard = true
			};

			await _botService.SendText(update?.Message?.Chat?.Id ?? 0, "Send your location, for detection your time zone. We don't save or share your location.", replyMarkup: keyboard);
		}
	}
}
