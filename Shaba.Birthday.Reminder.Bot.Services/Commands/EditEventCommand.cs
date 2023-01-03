using Shaba.Birthday.Reminder.Bot.Services.Services;
using Shaba.Birthday.Reminder.BusinessLogic;
using Shaba.Birthday.Reminder.BusinessLogic.Data;
using Shaba.Birthday.Reminder.Repository;
using Telegram.Bot.Types;
using User = Shaba.Birthday.Reminder.BusinessLogic.Data.User;

namespace Shaba.Birthday.Reminder.Bot.Services.Commands
{
	public class EditEventCommand : ICommand
	{
		private readonly IUserRepository _userRepository;
		private readonly IBotService _botService;
		private readonly IBotResourceService _botResourceService;
		private readonly IReplyMarkupFactory _replyMarkupFactory;
		private readonly IEventRepository _eventRepository;

		public EditEventCommand(IUserRepository userRepository, IBotService botService, IBotResourceService botResourceService, IReplyMarkupFactory replyMarkupFactory, IEventRepository eventRepository)
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
				if (arr![1] == "edit_specific")
				{
					user.LastAction = new LastAction()
					{
						Type = arr[0],
						Action = "request_date",
						Id = user.LastAction?.Id
					};
					var scheduledEvent = await _eventRepository.GetByEventId(user.LastAction.Id);
					await _userRepository.Update(user);
					await _botService.SendText(user.Id, "Info:\n" +
					                                    $"Event: {scheduledEvent.NameOfEvent}\n" +
					                                    (string.IsNullOrEmpty(scheduledEvent.CelebratedPerson) ?
						                                    ""
						                                    : $"Person: {scheduledEvent.CelebratedPerson}\n" +
						                                      $"Date: {scheduledEvent.Date.Date.ToShortDateString()}\n" +
						                                      $"Time: {scheduledEvent.Date.TimeOfDay.ToString("hh:mm")}\n" +
						                                      $"Choose what you want to edit"), _replyMarkupFactory.GetEditEventKeyboard(lang));
				}
				if (arr![1] == "edit_name")
				{
					user.LastAction = new LastAction()
					{
						Type = arr[0],
						Action = "edit_name",
						Id = user.LastAction?.Id
					};
					await _userRepository.Update(user);
					await _botService.SendText(user.Id, _botResourceService.Get("SendNameOfEvent", lang));
					await _userRepository.Update(user);
					await _botService.SendText(user.Id, _botResourceService.Get("ClickButtonForAction", lang), _replyMarkupFactory.GetBaseFunctionalMarkup(lang));
				}
				if (arr![1] == "edit_person")
				{
					user.LastAction = new LastAction();
					await _userRepository.Update(user);
					await _botService.SendText(user.Id, _botResourceService.Get("ClickButtonForAction", lang), _replyMarkupFactory.GetBaseFunctionalMarkup(lang));
				}
				if (arr![1] == "edit_date")
				{
					user.LastAction = new LastAction();
					await _userRepository.Update(user);
					await _botService.SendText(user.Id, _botResourceService.Get("ClickButtonForAction", lang), _replyMarkupFactory.GetBaseFunctionalMarkup(lang));
				}
				if (arr![1] == "edit_time")
				{
					user.LastAction = new LastAction();
					await _userRepository.Update(user);
					await _botService.SendText(user.Id, _botResourceService.Get("ClickButtonForAction", lang), _replyMarkupFactory.GetBaseFunctionalMarkup(lang));
				}
				if (arr![1] == "done")
				{
					user.LastAction = new LastAction();
					await _userRepository.Update(user);
					await _botService.SendText(user.Id, _botResourceService.Get("ClickButtonForAction", lang), _replyMarkupFactory.GetBaseFunctionalMarkup(lang));
				}
			}
		}
	}
}
