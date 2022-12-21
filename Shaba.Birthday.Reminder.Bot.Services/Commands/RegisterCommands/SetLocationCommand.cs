using Shaba.Birthday.Reminder.Repository;
using GeoTimeZone;
using Shaba.Birthday.Reminder.BusinessLogic;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TimeZoneConverter;
using User = Shaba.Birthday.Reminder.BusinessLogic.Data.User;

namespace Shaba.Birthday.Reminder.Bot.Services.Commands.RegisterCommands
{
    public class SetLocationCommand : ICommand
    {
	    private readonly IUserRepository _userRepository;
	    private readonly IBotService _botService;
	    private readonly IBotResourceService _botResourceService;
	    private readonly IReplyMarkupFactory _replyMarkupFactory;

	    public SetLocationCommand(IUserRepository userRepository, IBotService botService, IBotResourceService botResourceService, IReplyMarkupFactory replyMarkupFactory)
	    {
		    _userRepository = userRepository;
		    _botService = botService;
		    _botResourceService = botResourceService;
		    _replyMarkupFactory = replyMarkupFactory;
		}

		public async Task Execute(Update update, User user, string? arg = null)
        {
	        if (update.Message?.Location == null)
	        {
		        var button = KeyboardButton.WithRequestLocation(_botResourceService.Get("ShareTimeZone", user.Language));
		        var keyboard = new ReplyKeyboardMarkup(button)
		        {
			        ResizeKeyboard = true
		        };

		        await _botService.SendText(user.Id, _botResourceService.Get("EmptyTimeZone", user.Language), keyboard);
		        return;
			}
	        var timeZone = TZConvert.GetTimeZoneInfo(TimeZoneLookup.GetTimeZone(update.Message.Location.Latitude, update.Message.Location.Longitude).Result);
	        var time = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone);
	        user.TimeZone = timeZone.Id;
	        await _userRepository.Update(user);

			await _botService.SendText(user.Id, string.Format(_botResourceService.Get("SuccessfulRegistration", user.Language), user.FirstName, timeZone.DisplayName, time), new ReplyKeyboardRemove());

	        await _botService.SendText(user.Id, _botResourceService.Get("SendCommandForCreatingEvent", user.Language), _replyMarkupFactory.GetBaseFunctionalMarkup(user.Language));
		}
    }
}
