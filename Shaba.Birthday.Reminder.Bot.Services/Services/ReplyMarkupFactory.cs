using Shaba.Birthday.Reminder.BusinessLogic;
using Shaba.Birthday.Reminder.Bot.Services.Resources;
using Telegram.Bot.Types.ReplyMarkups;
using Language = Shaba.Birthday.Reminder.BusinessLogic.Language;

namespace Shaba.Birthday.Reminder.Bot.Services.Services
{
	public class ReplyMarkupFactory : IReplyMarkupFactory
	{
		private readonly IBotResourceService _botResourceService;

		public ReplyMarkupFactory(IBotResourceService botResourceService)
		{
			_botResourceService = botResourceService;
		}

		public IReplyMarkup GetLocationMarkup(Language? lang)
		{
			throw new NotImplementedException();
		}

		public IReplyMarkup GetLanguageMarkup()
		{
			return new InlineKeyboardMarkup(new List<IEnumerable<InlineKeyboardButton>>()
			{
				new[]
				{
					new InlineKeyboardButton( $"🇬🇧 English")
						{CallbackData = CommandNames.SetLanguageCommand + ":" + (int)Language.en},
					new InlineKeyboardButton($"🇷🇺 Русский")
						{CallbackData = CommandNames.SetLanguageCommand + ":" + (int)Language.ru},
				}
			});
		}

		public IReplyMarkup GetBaseFunctionalMarkup(Language? lang)
		{
			return new InlineKeyboardMarkup(new List<IEnumerable<InlineKeyboardButton>>()
				{
					new[]
					{
						new InlineKeyboardButton(_botResourceService.Get("ListOfEvents", lang)) {CallbackData = $"{CommandNames.ListOfEventsCommand}:start"},
						new InlineKeyboardButton(_botResourceService.Get("AddNewEvent", lang)) {CallbackData = $"{CommandNames.AddEventCommand}:start"}
					}
				});
		}

		public IReplyMarkup GetAddEventKeyboard(Language? lang)
		{
			return new InlineKeyboardMarkup(new List<IEnumerable<InlineKeyboardButton>>()
				{
					new[]
					{
						new InlineKeyboardButton(_botResourceService.Get("AddNewEvent", lang)) {CallbackData = $"{CommandNames.AddEventCommand}:start"}
					}
				});
		}

		public IReplyMarkup GetKeyboardForNameOfPerson(Language? lang)
		{
			return new InlineKeyboardMarkup(new List<IEnumerable<InlineKeyboardButton>>()
				{
					new[]
					{
						new InlineKeyboardButton(_botResourceService.Get("NonePerson", lang)) {CallbackData = $"{CommandNames.AddEventCommand}:none_person"}
					}
				});
		}

		public IReplyMarkup GetKeyboardForFinalize(Language? lang)
		{
			return new InlineKeyboardMarkup(new List<IEnumerable<InlineKeyboardButton>>()
			{
				new[]
				{
					new InlineKeyboardButton(_botResourceService.Get("DeleteEvent", lang)) {CallbackData = $"{CommandNames.AddEventCommand}:delete_specific"},
					new InlineKeyboardButton(_botResourceService.Get("EditEvent", lang)) {CallbackData = $"{CommandNames.EditEventCommand}:edit_specific"},
					new InlineKeyboardButton(_botResourceService.Get("Done", lang)) {CallbackData = $"{CommandNames.AddEventCommand}:done"}
				}
			});
		}

		public IReplyMarkup GetCancelKeyboardForAddEvent(Language? lang)
		{
			return new InlineKeyboardMarkup(new List<IEnumerable<InlineKeyboardButton>>()
			{
				new[]
				{
					new InlineKeyboardButton(_botResourceService.Get("Cancel", lang)) {CallbackData = $"{CommandNames.AddEventCommand}:cancel"}
				}
			});
		}

		public IReplyMarkup GetEditEventKeyboard(Language? lang)
		{
			return new InlineKeyboardMarkup(new List<IEnumerable<InlineKeyboardButton>>()
			{
				new[]
				{
					new InlineKeyboardButton(_botResourceService.Get("EditName", lang)) {CallbackData = $"{CommandNames.EditEventCommand}:edit_name"},
					new InlineKeyboardButton(_botResourceService.Get("EditPerson", lang)) {CallbackData = $"{CommandNames.EditEventCommand}:edit_person"}
				},
				new[]
				{
					new InlineKeyboardButton(_botResourceService.Get("EditDate", lang)) {CallbackData = $"{CommandNames.EditEventCommand}:edit_date"},
					new InlineKeyboardButton(_botResourceService.Get("EditTime", lang)) {CallbackData = $"{CommandNames.EditEventCommand}:edit_time"}
				},
				new[]
				{
					new InlineKeyboardButton(_botResourceService.Get("Done", lang)) {CallbackData = $"{CommandNames.EditEventCommand}:done"},
					new InlineKeyboardButton(_botResourceService.Get("DeleteEvent", lang)) {CallbackData = $"{CommandNames.DeleteEventCommand}:delete_specific"}
				}
			});
		}

		public IReplyMarkup GetKeyboardForDetailedEvent(Language? lang)
		{
			return new InlineKeyboardMarkup(new List<IEnumerable<InlineKeyboardButton>>()
			{
				new[]
				{
					new InlineKeyboardButton(_botResourceService.Get("ListOfEvents", lang)) {CallbackData = $"{CommandNames.ListOfEventsCommand}:start"},
					new InlineKeyboardButton(_botResourceService.Get("AddNewEvent", lang)) {CallbackData = $"{CommandNames.AddEventCommand}:start"}
				},
				new[]
				{
					new InlineKeyboardButton(_botResourceService.Get("EditEvent", lang)) {CallbackData = $"{CommandNames.EditEventCommand}:edit_specific"}, 
					new InlineKeyboardButton(_botResourceService.Get("DeleteEvent", lang)) {CallbackData = $"{CommandNames.DeleteEventCommand}:delete_specific"}
				}
			});
		}
	}
}