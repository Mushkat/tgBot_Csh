using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Microsoft.Extensions.Logging;

namespace tg_bot
{
    /// <summary>
    /// Class for Updating processing.
    /// </summary>
    public class Updater
    {
        // Contains context of every users with chat.id in keys.
        private static Dictionary<long, Context> _userContexts = new Dictionary<long, Context>();

        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // Object of logger.
            var fileLoggerProvider = new FileLoggerProvider($"../../../../var/log{DateTime.Now.ToShortDateString()}.log");
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddProvider(fileLoggerProvider);
            });
            var _logger = loggerFactory.CreateLogger<Updater>();

            try
            {
                switch (update.Type)
                {
                    //  Action if Update type is Message.
                    case UpdateType.Message:
                        {
                            MessageProcessing.MessageProc(update, botClient, _userContexts, _logger);
                            return;
                        }
                    ////  Action if Update user pressed a button.
                    case UpdateType.CallbackQuery:
                        {
                            CallbackProcessing.CallbackProc(update, botClient, _userContexts, _logger);
                            return;
                        }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                _logger.LogError(ex.ToString() + '\n');
            }
        }






    }
}
