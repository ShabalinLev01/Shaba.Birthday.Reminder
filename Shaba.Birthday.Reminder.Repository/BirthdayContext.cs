﻿using Microsoft.EntityFrameworkCore;
using Shaba.Birthday.Reminder.Repository.Data;

namespace Shaba.Birthday.Reminder.Repository
{
	public class BirthdayContext : DbContext
	{

		public DbSet<User?> Users { get; set; }

		public DbSet<ScheduledEvent> ScheduledEvents { get; set; }

		public BirthdayContext(DbContextOptions<BirthdayContext> options) : base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ScheduledEvent>().HasIndex(s => s.UserId);
		}
	}
}
