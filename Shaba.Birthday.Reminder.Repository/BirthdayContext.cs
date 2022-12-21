using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
			modelBuilder.Entity<User>().Property(e => e.LastAction).HasConversion(
				v => JsonConvert.SerializeObject(v,
					new JsonSerializerSettings { NullValueHandling = NullValueHandling.Include }),
				v => JsonConvert.DeserializeObject<LastAction>(v,
					new JsonSerializerSettings { NullValueHandling = NullValueHandling.Include })!);
		}
	}
}
