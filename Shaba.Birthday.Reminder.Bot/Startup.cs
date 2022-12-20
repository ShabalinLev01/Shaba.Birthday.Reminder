using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shaba.Birthday.Reminder.Bot.Extension;
using Shaba.Birthday.Reminder.Bot.Services.Services;
using Shaba.Birthday.Reminder.Bot.Services;
using Shaba.Birthday.Reminder.BusinessLogic;
using Shaba.Birthday.Reminder.Repository;
using Shaba.Birthday.Reminder.Repository.Repository;


[assembly: FunctionsStartup(typeof(Shaba.Birthday.Reminder.Bot.Startup))]
namespace Shaba.Birthday.Reminder.Bot
{
	public class Startup : FunctionsStartup
	{
		public override void Configure(IFunctionsHostBuilder builder)
		{
			builder.Services.AddDbContextPool<BirthdayContext>((options) => options.UseSqlServer(Environment.GetEnvironmentVariable("ShabaBirthdayReminderDB")));
			builder.Services.AddScoped<IBotService, BotService>();
			builder.Services.AddSingleton<IBotResourceService, BotResourceService>();
			builder.Services.AddScoped<IMessageProcessor, MessageProcessor>();
			builder.Services.AddScoped<IUserRepository, UserRepository>();
			builder.Services.AddScoped<ICommandResolver, CommandResolver>();
			builder.Services.AddCommands();
			builder.Services.AddHttpClient();
		}
	}
}
