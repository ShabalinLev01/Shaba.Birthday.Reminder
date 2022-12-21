using Telegram.Bot.Types;
using User = Shaba.Birthday.Reminder.BusinessLogic.Data.User;

namespace Shaba.Birthday.Reminder.Bot.Services
{
    public interface ICommand
    {
	    Task Execute(Update update, User user, string? arg = null);
	}
}
