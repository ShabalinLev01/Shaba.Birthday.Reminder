﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaba.Birthday.Reminder.Repository
{
	public interface IUserRepository
	{
		Task<object> Get(int id);
	}
}