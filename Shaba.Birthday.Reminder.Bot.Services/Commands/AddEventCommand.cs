using Shaba.Birthday.Reminder.BusinessLogic;
using Shaba.Birthday.Reminder.BusinessLogic.Data;
using Shaba.Birthday.Reminder.Repository;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using User = Shaba.Birthday.Reminder.BusinessLogic.Data.User;

namespace Shaba.Birthday.Reminder.Bot.Services.Commands
{
	public class AddEventCommand : ICommand
	{
		private readonly IUserRepository _userRepository;
		private readonly IBotService _botService;
		private readonly IBotResourceService _botResourceService;
		private readonly IReplyMarkupFactory _replyMarkupFactory;

		public AddEventCommand(IUserRepository userRepository, IBotService botService, IBotResourceService botResourceService, IReplyMarkupFactory replyMarkupFactory)
		{
			_userRepository = userRepository;
			_botService = botService;
			_botResourceService = botResourceService;
			_replyMarkupFactory = replyMarkupFactory;
		}
		public async Task Execute(Update update, User user, string? arg = null)
		{
			string?[]? arr = update.CallbackQuery?.Data?.Split(':');
			if (arr?.Length > 1)
			{
				if (arr![1] == "start")
				{
					user.LastAction = new LastAction()
					{
						Type = arr[0],
						Action = arr[1]
					};
					await _userRepository.Update(user);
					await _botService.SendText(user.Id, "Send name of event");
				}

				if (arr![1] == "none_person")
				{

				}
			}

			if (user.LastAction.Action == "start")
			{
				var nameOfEvent = arg;
				user.LastAction = new LastAction()
				{
					Type = arr[0],
					Action = "request_name_person"
				};
				await _userRepository.Update(user);
				await _botService.SendText(user.Id, "Send name of person or just click button \"None\"");
			}
		}
	}
}
