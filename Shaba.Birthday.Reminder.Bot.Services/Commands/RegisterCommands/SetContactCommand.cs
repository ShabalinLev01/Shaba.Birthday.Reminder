using Shaba.Birthday.Reminder.Bot.Services.Services;
using Shaba.Birthday.Reminder.BusinessLogic;
using Shaba.Birthday.Reminder.Repository;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using User = Shaba.Birthday.Reminder.BusinessLogic.Data.User;

namespace Shaba.Birthday.Reminder.Bot.Services.Commands.RegisterCommands
{
    public class SetContactCommand : ICommand
    {
	    private readonly IUserRepository _userRepository;
	    private readonly IBotService _botService;
	    private readonly IBotResourceService _botResourceService;
	    private readonly IReplyMarkupFactory _replyMarkupFactory;

	    public SetContactCommand(IUserRepository userRepository, IBotService botService, IBotResourceService botResourceService, IReplyMarkupFactory replyMarkupFactory)
	    {
		    _userRepository = userRepository;
		    _botService = botService;
		    _botResourceService = botResourceService;
		    _replyMarkupFactory = replyMarkupFactory;
	    }

		public async Task Execute(Update update, User user, string arg = null)
        {
	        if (update.Message?.Contact?.PhoneNumber == null)
            {
				var button = KeyboardButton.WithRequestContact(_botResourceService.Get("SharePhoneNumber", user.Language));
				var keyboard = new ReplyKeyboardMarkup(button)
                {
                    ResizeKeyboard = true
                };

				await _botService.SendText(update?.Message?.Chat?.Id ?? 0, _botResourceService.Get("EmptyPhoneNumber", user.Language), keyboard);
				return;
			}

	        user.PhoneNumber = update.Message.Contact.PhoneNumber;
	        await _userRepository.Update(user);

	        if (user.TimeZone == null)
	        {
		        var button = KeyboardButton.WithRequestLocation(_botResourceService.Get("ShareTimeZone", user.Language));
		        var keyboard = new ReplyKeyboardMarkup(button)
		        {
			        ResizeKeyboard = true
		        };

		        await _botService.SendText(update?.Message?.Chat?.Id ?? 0, _botResourceService.Get("EmptyTimeZone", user.Language), keyboard);
		        return;
	        }

	        var timeZone = TimeZoneInfo.FindSystemTimeZoneById(user.TimeZone);

			await _botService.SendText(update?.Message?.Chat?.Id!, string.Format(_botResourceService.Get("SuccessfulRegistration", user.Language), 
		        user.FirstName,
		        timeZone.DisplayName,
		        TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone)));
			await _botService.SendText(update?.Message?.Chat?.Id ?? 0, _botResourceService.Get("SendCommandForCreatingEvent", user.Language));
		}
    }
}
