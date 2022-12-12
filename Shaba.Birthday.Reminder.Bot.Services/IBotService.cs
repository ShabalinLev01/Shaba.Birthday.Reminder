using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Shaba.Birthday.Reminder.Bot.Services
{
	public interface IBotService
	{
		Task<Message> SendText(ChatId chatId, string text, IReplyMarkup replyMarkup = null);

		Task SetWebhookAsync(string handleUpdateFunctionUrl);
	}
}
