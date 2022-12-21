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

		/*public IReplyMarkup GetLocationMarkup(Language? lang)
		{
			/*return new ReplyKeyboardMarkup(new List<IEnumerable<KeyboardButton>>()
			{
				new[] { new KeyboardButton(_botResourceService.Get("AttachLocation", lang)) { RequestLocation = true } },
				new[] { new KeyboardButton(_botResourceService.Get("Back", lang)) }
			}
			, true, true);#1#
		}*/

		/*public IReplyMarkup InlineCancelMarkup(Language? lang)
		{
			return new InlineKeyboardMarkup(new InlineKeyboardButton() { Text = _botResourceService.Get("Back", lang), CallbackData = nameof(ProfileCommand) + ":0" }); //0 -indicates that need to set ready status
		}

		public IReplyMarkup GoToProfileMarkup(Language? lang)
		{
			return new InlineKeyboardMarkup(new InlineKeyboardButton() { Text = _botResourceService.Get("GoToProfile", lang), CallbackData = nameof(ProfileCommand) });
		}

		public IReplyMarkup GetProfileMarkup(Language? lang)
		{
			return new InlineKeyboardMarkup(new List<IEnumerable<InlineKeyboardButton>>()
			{
				new []{new InlineKeyboardButton(){Text = _botResourceService.Get("Search", lang) , CallbackData = nameof(SearchCommand)}, },
				new []
				{
					new InlineKeyboardButton(){Text = _botResourceService.Get("MyMatches", lang) , CallbackData = nameof(MatchCommand)},
					new InlineKeyboardButton(){Text = _botResourceService.Get("Settings", lang), CallbackData = nameof(SettingsCommand)},
				}
			});
		}

		public IReplyMarkup GetBackMarkup(Language? lang)
		{
			return new ReplyKeyboardMarkup(
				new[] { new KeyboardButton(_botResourceService.Get("Back", lang)) }, true, true);
		}

		public IReplyMarkup GetSettingsMarkup(string gender, string searchGender, int? radius, Language? lang)
		{
			var radiusString = (radius == null ? "♾" : radius.ToString() + "  km");
			return new InlineKeyboardMarkup(new List<IEnumerable<InlineKeyboardButton>>()
			{
				new []
				{
					new InlineKeyboardButton(){Text = _botResourceService.Get("ChangePhoto", lang), CallbackData = nameof(SettingPhotoCommand)},
					new InlineKeyboardButton(){Text = _botResourceService.Get("ChangeLocation", lang), CallbackData = nameof(SettingLocationCommand)},
				},
				new []
				{
					new InlineKeyboardButton(){Text = string.Format(_botResourceService.Get("Iam", lang), gender) , CallbackData = nameof(SettingMyGenderCommand)},
					new InlineKeyboardButton(){Text = string.Format(_botResourceService.Get("LookingFor", lang), searchGender) , CallbackData = nameof(SettingGenderCommand)},
				},
				new []
				{
					new InlineKeyboardButton(){Text =string.Format(_botResourceService.Get("SearchRadius", lang), radiusString) , CallbackData = nameof(SettingRadiusCommand)},
					new InlineKeyboardButton(){Text =string.Format(_botResourceService.Get("LanguageSetting", lang)) , CallbackData = nameof(SetLanguageCommand)},
				},
				new []{new InlineKeyboardButton(){Text = _botResourceService.Get("Back", lang), CallbackData = nameof(ProfileCommand)}, },
			});
		}
		//Cancel ❌
		public IReplyMarkup GetSearchGenderMarkup(Language? lang)
		{
			return new InlineKeyboardMarkup(new List<IEnumerable<InlineKeyboardButton>>()
			{
				new []
				{
					new InlineKeyboardButton(){Text = _botResourceService.Get("Male", lang) , CallbackData = nameof(SetSearchGenderCommand)+ ":" +(int)Gender.Male},
					new InlineKeyboardButton(){Text = _botResourceService.Get("Female", lang), CallbackData = nameof(SetSearchGenderCommand)+":" +(int)Gender.Female},
				}
			});
		}

		public IReplyMarkup GetMyGenderMarkup(Language? lang)
		{
			return new InlineKeyboardMarkup(new List<IEnumerable<InlineKeyboardButton>>()
			{
				new []
				{
					new InlineKeyboardButton(){Text = _botResourceService.Get("Male", lang) , CallbackData = nameof(SetMyGenderCommand)+ ":" +(int)Gender.Male},
					new InlineKeyboardButton(){Text = _botResourceService.Get("Female", lang), CallbackData = nameof(SetMyGenderCommand)+":" +(int)Gender.Female},
				}
			});
		}

		public IReplyMarkup GetRadiusMarkup(Language? lang)
		{
			return new InlineKeyboardMarkup(new List<IEnumerable<InlineKeyboardButton>>()
			{
				new []
				{
					new InlineKeyboardButton(){Text = "10 km" , CallbackData = nameof(SetRadiusCommand)+ ":10" },
					new InlineKeyboardButton(){Text = "100 km", CallbackData = nameof(SetRadiusCommand)+":100" },
				},
				new []
				{
					new InlineKeyboardButton(){Text = "1000 km" , CallbackData = nameof(SetRadiusCommand)+ ":1000" },
					new InlineKeyboardButton(){Text = _botResourceService.Get("Everywhere", lang), CallbackData = nameof(SetRadiusCommand) },
				},
			});
		}

		public IReplyMarkup GetYesNoMarkup(int id, Language? lang)
		{
			return new InlineKeyboardMarkup(new List<IEnumerable<InlineKeyboardButton>>()
			{
				new []
				{
					new InlineKeyboardButton(){Text = $"👎" , CallbackData = nameof(SearchCommand)},
					new InlineKeyboardButton(){Text = $"❤️", CallbackData = nameof(SearchCommand)+":"+id},
				},
				new []{new InlineKeyboardButton(){Text = _botResourceService.Get("Back", lang), CallbackData = nameof(ProfileCommand)}, },
			});
		}*/

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
					},
					new[]
					{
						new InlineKeyboardButton(_botResourceService.Get("EditEvent", lang)) {CallbackData = $"{CommandNames.EditEventCommand}:start"}, 
						new InlineKeyboardButton(_botResourceService.Get("DeleteEvent", lang)) {CallbackData = $"{CommandNames.DeleteEventCommand}:start"}
					}
				});
		}
	}
}