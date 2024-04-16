using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;

namespace tg_bot
{
    // Бот опубликован по ссылке https://t.me/Recreators_bot
    // Или @Recreators_bot
    // Хост лежит на сервере ubuntu с помощью сервиса beget.com
    public class Program
    {
        /// <summary>
        /// Creating and starting a bot.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        static async Task Main(string[] args)
        {
            var botClient = new TelegramBotClient("6000388736:AAG5kI9NV5AHMUm4Z5Nt0Dipa8FN0tcVYxs");

            using CancellationTokenSource cts = new();

            ReceiverOptions receiverOptions = new()
            {
                AllowedUpdates = Array.Empty<UpdateType>()
            };

            botClient.StartReceiving(
                updateHandler: Updater.HandleUpdateAsync,
                pollingErrorHandler: Errors.HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: cts.Token
            );

            var me = await botClient.GetMeAsync();

            Console.WriteLine($"Start listening for @{me.Username}");
            Console.ReadLine();

            cts.Cancel();

        }
    }
}