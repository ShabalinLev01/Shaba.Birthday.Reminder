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
		    _botClient = new TelegramBotClient("5832575812:AAH0P7d_2zPv1I3hW-f_Sfd1PArsRTqLUI4", httpClientFactory.CreateClient());
	    }

	    public async Task<Message> SendText(ChatId chatId, string text, IReplyMarkup replyMarkup = null)
	    {
		    return await _botClient.SendTextMessageAsync(chatId, text, replyMarkup: replyMarkup);
	    }

	    public async Task SetWebhookAsync(string handleUpdateFunctionUrl)
		{
			await _botClient.SetWebhookAsync(handleUpdateFunctionUrl);
		}
    }
}