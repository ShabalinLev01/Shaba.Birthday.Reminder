using Shaba.Birthday.Reminder.Bot.Services.Resources;
using Shaba.Birthday.Reminder.BusinessLogic;
using Shaba.Birthday.Reminder.BusinessLogic.Data;
using Shaba.Birthday.Reminder.Repository;
using Telegram.Bot.Types;
using User = Shaba.Birthday.Reminder.BusinessLogic.Data.User;

namespace Shaba.Birthday.Reminder.Bot.Services.Commands
{
	public class AddEventCommand : ICommand
	{
		private readonly IUserRepository _userRepository;
		private readonly IBotService _botService;
		private readonly IBotResourceService _botResourceService;
		private readonly IReplyMarkupFactory _replyMarkupFactory;
		private readonly IEventRepository _eventRepository;

		public AddEventCommand(IUserRepository userRepository, IBotService botService, IBotResourceService botResourceService, IReplyMarkupFactory replyMarkupFactory, IEventRepository eventRepository)
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
						Action = "name_event"
					};
					await _userRepository.Update(user);
					await _botService.SendText(user.Id, _botResourceService.Get("SendNameOfEvent", lang));
					return;
				}

				if (arr![1] == "none_person")
				{
					user.LastAction = new LastAction()
					{
						Type = arr[0],
						Action = "request_date",
						Id = user.LastAction?.Id
					};
					await _userRepository.Update(user);
					await _botService.SendText(user.Id, _botResourceService.Get("SendDateOfEvent", lang));
				}


				if (arr![1] == "done")
				{
					user.LastAction = new LastAction();
					await _userRepository.Update(user);
					await _botService.SendText(user.Id, _botResourceService.Get("ClickButtonForAction", lang), _replyMarkupFactory.GetBaseFunctionalMarkup(lang));
				}


				if (arr![1] == "cancel")
				{
					if (user.LastAction?.Id != null)
					{
						await _eventRepository.Delete(user.LastAction.Id);
					}
					user.LastAction = new LastAction();
					await _userRepository.Update(user);
					await _botService.SendText(user.Id, _botResourceService.Get("EventWasSuccessfulAdded", lang));
					await _botService.SendText(user.Id, _botResourceService.Get("ClickButtonForAction", lang), _replyMarkupFactory.GetBaseFunctionalMarkup(lang));
				}
			}

			if (user.LastAction!.Action == "name_event")
			{
				var nameOfEvent = arg;
				var scheduledEvent = new ScheduledEvent(user.Id)
				{
					NameOfEvent = nameOfEvent,
					Id = Guid.NewGuid(),
				};
				user.LastAction = new LastAction()
				{
					Type = CommandNames.AddEventCommand,
					Action = "request_name_person",
					Id = scheduledEvent.Id
				};
				await _eventRepository.Add(scheduledEvent);
				await _userRepository.Update(user);
				await _botService.SendText(user.Id, _botResourceService.Get("SendNameOfPerson", lang), _replyMarkupFactory.GetKeyboardForNameOfPerson(lang));
				return;
			}

			if (user.LastAction.Action == "request_name_person")
			{
				var nameOfPerson = arg;
				var scheduledEvent = await _eventRepository.GetByEventId(user.LastAction?.Id);
				scheduledEvent!.CelebratedPerson = nameOfPerson!;
				user.LastAction = new LastAction()
				{
					Type = CommandNames.AddEventCommand,
					Action = "request_date",
					Id = scheduledEvent.Id
				};
				await _eventRepository.Update(scheduledEvent);
				await _userRepository.Update(user);
				await _botService.SendText(user.Id, _botResourceService.Get("SendDateOfEvent", lang));
				return;
			}

			if (user.LastAction.Action == "request_date")
			{
				var isParsed = DateTime.TryParse(arg, out var dateTime);
				if (!isParsed)
				{
					await _botService.SendText(user.Id, _botResourceService.Get("IncorrectDateTryAgain", lang));
					return;
				}
				var scheduledEvent = await _eventRepository.GetByEventId(user.LastAction?.Id);
				scheduledEvent!.Date = dateTime;
				user.LastAction = new LastAction()
				{
					Type = CommandNames.AddEventCommand,
					Action = "request_time",
					Id = scheduledEvent.Id
				};
				await _eventRepository.Update(scheduledEvent);
				await _userRepository.Update(user);
				await _botService.SendText(user.Id, _botResourceService.Get("SendTimeOfEvent", lang));
				return;
			}

			if (user.LastAction.Action == "request_time")
			{
				if (!TimeSpan.TryParse(arg, out var time))
				{
					await _botService.SendText(user.Id, _botResourceService.Get("IncorrectTimeTryAgain", lang));
					return;
				}
				var scheduledEvent = await _eventRepository.GetByEventId(user.LastAction?.Id);
				scheduledEvent!.Date = scheduledEvent.Date.Add(time);
				user.LastAction = new LastAction()
				{
					Type = CommandNames.AddEventCommand,
					Action = "done",
					Id = scheduledEvent.Id
				};
				await _eventRepository.Update(scheduledEvent);
				await _userRepository.Update(user);
				await _botService.SendText(user.Id, _botResourceService.Get("IncorrectTimeTryAgain", lang) +
				                                    string.Format(_botResourceService.Get("DetailedInfoAboutEvent", lang),
					                                    scheduledEvent.NameOfEvent,
					                                    string.IsNullOrEmpty(scheduledEvent.CelebratedPerson)
						                                    ? _botResourceService.Get("None", lang)
						                                    : scheduledEvent.CelebratedPerson,
					                                    scheduledEvent.Date.Date.ToShortDateString(),
					                                    scheduledEvent.Date.TimeOfDay.ToString("hh:mm")), _replyMarkupFactory.GetKeyboardForFinalize(lang));
			}
		}
	}
}
