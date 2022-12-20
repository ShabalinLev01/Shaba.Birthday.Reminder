using Shaba.Birthday.Reminder.Repository.Data;
using Shaba.Birthday.Reminder.BusinessLogic;
using Microsoft.EntityFrameworkCore;

namespace Shaba.Birthday.Reminder.Repository.Repository
{
	public class UserRepository : IUserRepository
	{
		private readonly BirthdayContext _context;
		private readonly IBotResourceService _botResourceService;

		public UserRepository(BirthdayContext context, IBotResourceService botResourceService)
		{
			_context = context;
			_botResourceService = botResourceService;
		}

		public async Task<User?> Get(long id)
		{
			return await _context.Users.SingleOrDefaultAsync(x=>x!.Id == id);
		}

		public async Task Add(User user)
		{
			await _context.Users.AddAsync(user);
			await _context.SaveChangesAsync();
		}

		public async Task Update(User user)
		{
			_context.Update(user);
			await _context.SaveChangesAsync();
		}

		public async Task BlockUser(User user)
		{
			user.IsBlock = true;
			await Update(user);
		}

		public async Task UnBlockUser(User user)
		{
			user.IsBlock = false;
			await Update(user);
		}
	}
}
