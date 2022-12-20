using Microsoft.Extensions.DependencyInjection;
using Shaba.Birthday.Reminder.Bot.Services.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shaba.Birthday.Reminder.Bot.Services;

namespace Shaba.Birthday.Reminder.Bot.Extension
{
	public static class ServiceCollectionExtension
	{
		public static void AddCommands(this IServiceCollection services)
		{
			services.AddScoped<ICommand, UnblockCommand>();
			services.AddScoped<ICommand, LanguageCommand>();
			services.AddScoped<ICommand, ContactCommand>();
			services.AddScoped<ICommand, LocationCommand>();
		}
	}
}
