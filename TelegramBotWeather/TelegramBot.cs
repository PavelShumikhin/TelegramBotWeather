using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace bot
{
    class TelegramBot
    {
        private ITelegramBotClient client;
        private DataWeather dataWeather;

        public TelegramBot(string token)
        {
            client = new TelegramBotClient(token);
            dataWeather = new DataWeather();
        }

        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));

            var message = update.Message;

            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {                
                if (message.Text.ToLower() == "/start")
                {
                    await botClient.SendTextMessageAsync(message.Chat, "Добро пожаловать!");
                    return;
                }
                await botClient.SendTextMessageAsync(message.Chat, "Hello!");                
            }

            SendInline(botClient, message.Chat.Id, cancellationToken);

            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
            {
                string codeOfButton = update.CallbackQuery.Data;
                if (codeOfButton == "Moscow")
                {
                    string telegramMessage = "dataWeather";
                    await botClient.SendTextMessageAsync(chatId: update.CallbackQuery.Message.Chat.Id, telegramMessage, parseMode: Telegram.Bot.Types.Enums.ParseMode.Html);
                }
            }
        }

        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }

        public static async void SendInline(ITelegramBotClient botClient, long chatId, CancellationToken cancellationToken)
        {
            InlineKeyboardMarkup inlineKeyboard = new InlineKeyboardMarkup(
                new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData(text: "Москва", callbackData: "Moscow"),
                        InlineKeyboardButton.WithCallbackData(text: "Санкт-Петербург", callbackData: "Saint Petersburg"),
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData(text: "Казань", callbackData: "Kazan"),
                        InlineKeyboardButton.WithCallbackData(text: "Нижний Новгород", callbackData: "Nizhny Novgorod")
                    },

                });

            Message sentMessage = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Выберите город",
                replyMarkup: inlineKeyboard,
                cancellationToken: cancellationToken);
        }

        public void Start()
        {
            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }
            };

            client.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );
            Console.ReadLine();
        }

        public void Stop()
        {
        }
    }
}
