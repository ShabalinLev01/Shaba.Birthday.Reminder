﻿using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Shaba.Birthday.Reminder.Bot.Services.Commands.RegisterCommands;
using Shaba.Birthday.Reminder.Bot.Services.Commands;
using Shaba.Birthday.Reminder.Bot.Services.Resources;
using Shaba.Birthday.Reminder.Bot.Services.Services;

namespace Shaba.Birthday.Reminder.Bot.Extension
{
    public static class ServiceCollectionExtension
	{
		public static void AddCommands(this IServiceCollection services)
		{
			services.AddScoped<UnblockCommand>();
			services.AddScoped<SetLanguageCommand>();
			services.AddScoped<SetContactCommand>();
			services.AddScoped<SetLocationCommand>();
			services.AddScoped<AddEventCommand>();
			services.AddScoped<EditEventCommand>();
			services.AddScoped<DeleteEventCommand>();
			services.AddScoped<ListOfEventsCommand>();

			services.AddScoped<CommandChooser>(serviceProvider => key =>
			{
				return key switch
				{
					CommandNames.UnblockCommand => serviceProvider.GetService<UnblockCommand>(),
					CommandNames.SetLanguageCommand => serviceProvider.GetService<SetLanguageCommand>(),
					CommandNames.SetContactCommand => serviceProvider.GetService<SetContactCommand>(),
					CommandNames.SetLocationCommand => serviceProvider.GetService<SetLocationCommand>(),
					CommandNames.AddEventCommand => serviceProvider.GetService<AddEventCommand>(),
					CommandNames.EditEventCommand => serviceProvider.GetService<EditEventCommand>(),
					CommandNames.DeleteEventCommand => serviceProvider.GetService<DeleteEventCommand>(),
					CommandNames.ListOfEventsCommand => serviceProvider.GetService<ListOfEventsCommand>(),
					_ => throw new KeyNotFoundException()
				};
			});
		}
	}
}
