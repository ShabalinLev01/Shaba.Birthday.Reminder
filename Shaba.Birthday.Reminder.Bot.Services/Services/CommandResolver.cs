using GeoTimeZone;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TimeZoneConverter;
using User = Shaba.Birthday.Reminder.Repository.Data.User;

namespace Shaba.Birthday.Reminder.Bot.Services.Services
{
	public class CommandResolver : ICommandResolver
	{
		private readonly IEnumerable<ICommand> _commands;
		private readonly IBotService _botService;

		public CommandResolver(IEnumerable<ICommand> commands, IBotService botService)
		{
			_commands = commands;
			_botService = botService;
		}

		public async Task Resolve(Update update, User user)
		{
			if (update.Type == UpdateType.Message)
			{
				if (update.Message?.Contact != null)
				{
					await RequestTimezone(update);
				}
				else if (update.Message?.Location != null)
				{
					var timeZone = TZConvert.GetTimeZoneInfo(TimeZoneLookup.GetTimeZone(update.Message.Location.Latitude, update.Message.Location.Longitude).Result);
					var time = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone);
					await _botService.SendText(update.Message.From.Id,
						$"You are successful registrated, {update.Message.From.FirstName}");
					await _botService.SendText(update.Message.From.Id,
						$"You're time zone is {timeZone.DisplayName} and current time is {time.ToString("t")}, {update.Message.From.FirstName}");
				}
				else
				{
					await RequestContactForRegister(update);
				}
			}
		}
	}
}
