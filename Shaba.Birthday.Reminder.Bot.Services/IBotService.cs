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

		Task AnswerQueryCallback(string callbackQueryId, string? text = null, bool showAlert = false);

		Task DeleteMessage(ChatId chatId, int messageId);
	}
}
