using Shaba.Birthday.Reminder.BusinessLogic;
using Shaba.Birthday.Reminder.BusinessLogic.Data;
using Shaba.Birthday.Reminder.Repository;
using Telegram.Bot.Types;
using User = Shaba.Birthday.Reminder.BusinessLogic.Data.User;

namespace Shaba.Birthday.Reminder.Bot.Services.Commands
{
	public class DeleteEventCommand : ICommand
	{
		private readonly IUserRepository _userRepository;
		private readonly IBotService _botService;
		private readonly IBotResourceService _botResourceService;
		private readonly IReplyMarkupFactory _replyMarkupFactory;
		private readonly IEventRepository _eventRepository;
		public DeleteEventCommand(IUserRepository userRepository, IBotService botService, IBotResourceService botResourceService, IReplyMarkupFactory replyMarkupFactory, IEventRepository eventRepository)
		{
			_userRepository = userRepository;
			_eventRepository = eventRepository;
			_botService = botService;
			_botResourceService = botResourceService;
			_replyMarkupFactory = replyMarkupFactory;
		}

		public async Task Execute(Update update, User user, string? arg = null)
		{

			string?[]? arr = update.CallbackQuery?.Data?.Split(':');
			if (arr?.Length > 1)
			{
				if (arr![1] == "delete_specific")
				{;
					await _eventRepository.Delete(user.LastAction.Id);
					user.LastAction = new LastAction();
					await _userRepository.Update(user);
					await _botService.SendText(user.Id, "Event was deleted", _replyMarkupFactory.GetBaseFunctionalMarkup(user.Language));
				}
			}
		}
	}
}
