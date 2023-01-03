using System.Text;
using Shaba.Birthday.Reminder.BusinessLogic;
using Shaba.Birthday.Reminder.BusinessLogic.Data;
using Shaba.Birthday.Reminder.Repository;
using Telegram.Bot.Types;
using User = Shaba.Birthday.Reminder.BusinessLogic.Data.User;

namespace Shaba.Birthday.Reminder.Bot.Services.Commands
{
	public class ListOfEventsCommand : ICommand
	{
		private readonly IUserRepository _userRepository;
		private readonly IBotService _botService;
		private readonly IBotResourceService _botResourceService;
		private readonly IReplyMarkupFactory _replyMarkupFactory;
		private readonly IEventRepository _eventRepository;

		public ListOfEventsCommand(IUserRepository userRepository, IBotService botService, IBotResourceService botResourceService, IReplyMarkupFactory replyMarkupFactory, IEventRepository eventRepository)
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
			var lang = user.Language;
			if (arr?.Length > 1)
			{
				if (arr![1] == "start")
				{
					user.LastAction = new LastAction()
					{
						Type = arr[0],
						Action = "list_of_events"
					};
					var events = await _eventRepository.GetByUserId(user.Id);
					var stringBuilder = new StringBuilder();
					var i = 1;
					foreach (var scheduledEvent in events.OrderBy(x => x.Date))
					{
						stringBuilder.AppendLine(string.Format(_botResourceService.Get("StringBuilderList", lang), i,
							scheduledEvent.NameOfEvent, scheduledEvent.Date));
						i++;
					}
					await _userRepository.Update(user);
					await _botService.SendText(user.Id, _botResourceService.Get("ChooseEventForDetailsOrAddNew", lang), _replyMarkupFactory.GetAddEventKeyboard(lang));
					return;
				}
			}

			if (user.LastAction?.Action == "list_of_events")
			{
				var isParsed = int.TryParse(arg, out var num);
				if (!isParsed || num <=0)
				{
					await _botService.SendText(user.Id, _botResourceService.Get("IncorrectNumberOfEventTryAgain", lang), _replyMarkupFactory.GetBaseFunctionalMarkup(lang));
					return;
				}

				try
				{
					var scheduledEvent = (await _eventRepository.GetByUserId(user.Id)).OrderBy(x => x.Date).ToArray()[num];
					user.LastAction = new LastAction()
					{
						Type = arr[0],
						Action = "details_event",
						Id = scheduledEvent.Id
					};
					await _userRepository.Update(user);
					await _botService.SendText(user.Id, string.Format(_botResourceService.Get("DetailedInfoAboutEvent", lang),
						                                    scheduledEvent.NameOfEvent,
						                                    string.IsNullOrEmpty(scheduledEvent.CelebratedPerson)
							                                    ? _botResourceService.Get("None", lang)
							                                    : scheduledEvent.CelebratedPerson,
						                                    scheduledEvent.Date.Date.ToShortDateString(),
						                                    scheduledEvent.Date.TimeOfDay.ToString("hh:mm")) +"\n" + 
					                                    _botResourceService.Get("ClickButtonForAction", lang), _replyMarkupFactory.GetKeyboardForDetailedEvent(lang));
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
					throw;
				}
			}
		}
	}
}
