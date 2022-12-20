using System.Globalization;
using System.Resources;
using Shaba.Birthday.Reminder.Bot.Services.Resources;
using Shaba.Birthday.Reminder.BusinessLogic;

namespace Shaba.Birthday.Reminder.Bot.Services.Services
{
	public class BotResourceService : IBotResourceService
	{
		private readonly ResourceManager _rm;

		public BotResourceService()
		{
			_rm = new ResourceManager(typeof(Answers));
		}

		public string Get(string name, Language? ci)
		{
			return _rm.GetString(name, new CultureInfo(ci?.ToString() ?? "en"))!;
		}
	}
}
