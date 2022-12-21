using Shaba.Birthday.Reminder.Repository;
using Shaba.Birthday.Reminder.BusinessLogic;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Language = Shaba.Birthday.Reminder.BusinessLogic.Language;
using User = Shaba.Birthday.Reminder.BusinessLogic.Data.User;
using Telegram.Bot.Types.ReplyMarkups;

namespace Shaba.Birthday.Reminder.Bot.Services.Commands.RegisterCommands
{
    public class SetLanguageCommand : ICommand
    {
	    private readonly IUserRepository _userRepository;
	    private readonly IBotService _botService;
	    private readonly IBotResourceService _botResourceService;
	    private readonly IReplyMarkupFactory _replyMarkupFactory;

	    public SetLanguageCommand(IUserRepository userRepository, IBotService botService, IBotResourceService botResourceService, IReplyMarkupFactory replyMarkupFactory)
	    {
		    _userRepository = userRepository;
		    _botService = botService;
		    _botResourceService = botResourceService;
		    _replyMarkupFactory = replyMarkupFactory;
	    }
		public async Task Execute(Update update, User user, string? arg = null!)
		{
			

			if (arg == null! || !int.TryParse(arg, out var resultLanguage))
			{
				await _botService.SendText(user.Id, _botResourceService.Get("Language", user.Language),
					_replyMarkupFactory.GetLanguageMarkup());
				return;
			}



			await _botService.AnswerQueryCallback(update.CallbackQuery.Id);
			user.Language = (Language?) resultLanguage; 
			await _userRepository.Update(user);

			if (update.CallbackQuery?.Message?.Type == MessageType.Text)
			{
				await _botService.DeleteMessage(user.Id, update.CallbackQuery.Message.MessageId);
			}

			if (user.PhoneNumber == null)
			{
				var button = KeyboardButton.WithRequestContact(_botResourceService.Get("SharePhoneNumber", user.Language));
				var keyboard = new ReplyKeyboardMarkup(button)
				{
					ResizeKeyboard = true
				};

				await _botService.SendText(user.Id, _botResourceService.Get("EmptyPhoneNumber", user.Language), keyboard);
				return;
			}
			
			if (user.TimeZone == null)
			{
				var button = KeyboardButton.WithRequestLocation(_botResourceService.Get("ShareTimeZone", user.Language));
				var keyboard = new ReplyKeyboardMarkup(button)
				{
					ResizeKeyboard = true
				};

				await _botService.SendText(user.Id, _botResourceService.Get("EmptyTimeZone", user.Language), keyboard);
				return;
			}

			await _botService.SendText(user.Id, _botResourceService.Get("SendCommandForCreatingEvent", user.Language));
		}
    }
}
