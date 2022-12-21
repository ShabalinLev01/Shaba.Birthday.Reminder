using Telegram.Bot.Types;
using User = Shaba.Birthday.Reminder.BusinessLogic.Data.User;

namespace Shaba.Birthday.Reminder.Bot.Services.Commands
{
	public class ListOfEventsCommand : ICommand
	{
		public Task Execute(Update update, User user, string? arg = null)
		{
			throw new NotImplementedException();
		}
	}
}
