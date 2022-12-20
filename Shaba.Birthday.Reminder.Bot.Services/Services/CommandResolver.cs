using Shaba.Birthday.Reminder.Bot.Services.Resources;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using User = Shaba.Birthday.Reminder.BusinessLogic.Data.User;

namespace Shaba.Birthday.Reminder.Bot.Services.Services
{
	public class CommandResolver : ICommandResolver
	{
		private readonly CommandChooser _commands;
		private readonly IBotService _botService;

		public CommandResolver(CommandChooser commands, IBotService botService)
		{
			_commands = commands;
			_botService = botService;
		}

		public async Task Resolve(Update update, User user)
		{
			if (update.Message?.Text == "/start")
			{
				var ccc =  _commands(CommandNames.UnblockCommand);
				await _commands(CommandNames.UnblockCommand).Execute(update, user);
			}

			if (user.Language == null)
			{
				string arg = null!;
				var arr = update.CallbackQuery?.Data?.Split(':');
				if (arr?.Length > 1)
				{
					arg = arr[1];
				}

				if (arr?[0] != CommandNames.SetLanguageCommand)
				{
					arg = null!;
				}

				await _commands(CommandNames.SetLanguageCommand).Execute(update, user, arg);
				return;
			}

			if (user.PhoneNumber == null)
			{
				await _commands(CommandNames.SetContactCommand).Execute(update, user);
				return;
			}

			if (user.TimeZone == null)
			{
				await _commands(CommandNames.SetLocationCommand).Execute(update, user);
				return;
			}

			if (update.Type == UpdateType.Message)
			{
				await _botService.SendText(update.Message?.Chat?.Id, "Я больше ничего не умею, прости...");
			}
		}
	}
}
