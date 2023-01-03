using Shaba.Birthday.Reminder.BusinessLogic.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaba.Birthday.Reminder.Repository
{
	public interface IEventRepository
	{
		Task<List<ScheduledEvent>> GetByUserId(long id);
		Task<ScheduledEvent?> GetByEventId(Guid? id);

		Task Add(ScheduledEvent scheduledEvent);

		Task Update(ScheduledEvent scheduledEvent);
		Task Delete(Guid? id);
	}
}
