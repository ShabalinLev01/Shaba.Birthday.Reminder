using Shaba.Birthday.Reminder.BusinessLogic;
using Telegram.Bot.Types.ReplyMarkups;

namespace Shaba.Birthday.Reminder.Bot.Services
{
	public interface IReplyMarkupFactory
	{
		IReplyMarkup GetLocationMarkup(Language? lang);
		//IReplyMarkup GetProfileMarkup(Language? lang);

		//IReplyMarkup GetBackMarkup(Language? lang);
		//IReplyMarkup GetSettingsMarkup(string gender, string searchGender, int? radius, Language? lang);
		//IReplyMarkup GetSearchGenderMarkup(Language? lang);
		//IReplyMarkup GetRadiusMarkup(Language? lang);
	//	IReplyMarkup GetYesNoMarkup(int id, Language? lang);
		//IReplyMarkup GetMyGenderMarkup(Language? lang);

		//IReplyMarkup InlineCancelMarkup(Language? lang);

		//IReplyMarkup GoToProfileMarkup(Language? lang);

		IReplyMarkup GetLanguageMarkup();

		IReplyMarkup GetBaseFunctionalMarkup(Language? lang);
		IReplyMarkup GetKeyboardForNameOfPerson(Language? lang);
		IReplyMarkup GetKeyboardForFinalize(Language? lang);
		IReplyMarkup GetEditEventKeyboard(Language? lang);

		IReplyMarkup GetCancelKeyboardForAddEvent(Language? lang);
		IReplyMarkup GetAddEventKeyboard(Language? lang);
		IReplyMarkup GetKeyboardForDetailedEvent(Language? lang);
	}
}
