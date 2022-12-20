using Microsoft.EntityFrameworkCore;
using Shaba.Birthday.Reminder.BusinessLogic.Data;

namespace Shaba.Birthday.Reminder.Repository
{
	public class BirthdayContext : DbContext
	{

		public DbSet<User> Users { get; set; }

		public DbSet<ScheduledEvent> ScheduledEvents { get; set; }

		public BirthdayContext(DbContextOptions<BirthdayContext> options) : base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ScheduledEvent>().HasIndex(s => s.Id);
			modelBuilder.Entity<User>().HasIndex(s => s.Id);
		}
	}
}
