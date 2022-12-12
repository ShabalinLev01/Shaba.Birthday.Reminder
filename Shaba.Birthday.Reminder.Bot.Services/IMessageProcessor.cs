using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaba.Birthday.Reminder.Bot.Services
{
	public interface IMessageProcessor
	{
		Task Process(string updateString);
	}
}
