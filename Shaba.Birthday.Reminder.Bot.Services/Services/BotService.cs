using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using static System.Net.Mime.MediaTypeNames;

namespace Shaba.Birthday.Reminder.Bot.Services.Services
{
    public class BotService : IBotService
    {
	    private readonly TelegramBotClient _botClient;

	    public BotService(IHttpClientFactory httpClientFactory)
	    {
		    var ccc = Environment.GetEnvironmentVariable("TelegramToken");

			_botClient = new TelegramBotClient(Environment.GetEnvironmentVariable("TelegramToken") ?? throw new NullReferenceException(), httpClientFactory.CreateClient());
	    }

	    public async Task<Message> SendText(ChatId chatId, string text, IReplyMarkup replyMarkup = null)
	    {
		    return await _botClient.SendTextMessageAsync(chatId, text, replyMarkup: replyMarkup);
	    }

	    public async Task SetWebhookAsync(string handleUpdateFunctionUrl)
		{
			await _botClient.SetWebhookAsync(handleUpdateFunctionUrl);
		}

	    public async Task AnswerQueryCallback(string callbackQueryId, string? text = null, bool showAlert = false)
		{
		    await _botClient.AnswerCallbackQueryAsync(callbackQueryId, text, showAlert);
	    }

	    public async Task DeleteMessage(ChatId chatId, int messageId)
	    {
		    await _botClient.DeleteMessageAsync(chatId, messageId);
	    }
	}
}