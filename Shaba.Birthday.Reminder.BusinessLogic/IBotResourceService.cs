namespace Shaba.Birthday.Reminder.BusinessLogic
{
	public interface IBotResourceService
	{
		string Get(string name, Language? ci);
	}
}
