namespace Shaba.Birthday.Reminder.Bot.Services
{
	public interface IMessageProcessor
	{
		Task Process(string updateString);
	}
}
