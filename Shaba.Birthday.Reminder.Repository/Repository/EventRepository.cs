using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shaba.Birthday.Reminder.BusinessLogic;
using Shaba.Birthday.Reminder.BusinessLogic.Data;

namespace Shaba.Birthday.Reminder.Repository.Repository
{
	public class EventRepository : IEventRepository
	{
		private readonly BirthdayContext _context;
		private readonly IBotResourceService _botResourceService;

		public EventRepository(BirthdayContext context, IBotResourceService botResourceService)
		{
			_context = context;
			_botResourceService = botResourceService;
		}

		public async Task<List<ScheduledEvent>> GetByUserId(long id)
		{
			return await _context.ScheduledEvents.Where(x => x!.UserId == id).ToListAsync();
		}
		public async Task<ScheduledEvent?> GetByEventId(Guid? id)
		{
			return await _context.ScheduledEvents.SingleOrDefaultAsync(x => x.Id == id);
		}

		public async Task Add(ScheduledEvent scheduledEvent)
		{
			await _context.ScheduledEvents.AddAsync(scheduledEvent);
			await _context.SaveChangesAsync();
		}

		public async Task Update(ScheduledEvent scheduledEvent)
		{
			_context.ScheduledEvents.Update(scheduledEvent);
			await _context.SaveChangesAsync();
		}

		public async Task Delete(Guid? id)
		{
			var obj = await _context.ScheduledEvents.SingleOrDefaultAsync(x => x.Id == id);
			if (obj == null)
				return;
			_context.ScheduledEvents.Remove(obj);
			await _context.SaveChangesAsync();
		}
	}
}
