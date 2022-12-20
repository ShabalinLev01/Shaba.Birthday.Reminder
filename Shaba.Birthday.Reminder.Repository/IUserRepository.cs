using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shaba.Birthday.Reminder.Repository.Data;

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
