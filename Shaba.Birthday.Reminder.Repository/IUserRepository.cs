using Shaba.Birthday.Reminder.BusinessLogic.Data;

namespace Shaba.Birthday.Reminder.Repository
{
	public interface IUserRepository
	{
		Task<User?> Get(long id);

		Task Add(User user);

		Task Update(User user);

		Task BlockUser(User user);
		Task UnBlockUser(User user);
	}
}
