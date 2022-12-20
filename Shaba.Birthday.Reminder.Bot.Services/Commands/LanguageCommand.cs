using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using User = Shaba.Birthday.Reminder.Repository.Data.User;

namespace Shaba.Birthday.Reminder.Bot.Services.Commands
{
	public class LanguageCommand : ICommand
	{
		public Task Execute(Update update, User user, string arg = null)
		{
			throw new NotImplementedException();
		}
	}
}
