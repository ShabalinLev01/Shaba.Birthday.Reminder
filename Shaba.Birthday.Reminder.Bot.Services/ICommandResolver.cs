using Telegram.Bot.Types;
using User = Shaba.Birthday.Reminder.BusinessLogic.Data.User;

namespace Shaba.Birthday.Reminder.Bot.Services
{
	public interface ICommandResolver
	{
		Task Resolve(Update update, User user);
	}
}
