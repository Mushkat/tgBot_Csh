using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;
using Microsoft.Extensions.Logging;

namespace tg_bot
{
    /// <summary>
    /// Class for all messages processing.
    /// </summary>
    public class MessageProcessing
    {
        async public static void MessageProc(Update update, ITelegramBotClient botClient,
            Dictionary<long, Context> userContexts, ILogger logger)
        {
            var message = update.Message;
            var user = message.From;
            Console.WriteLine($"{user.FirstName} ({user.Id}) написал сообщение: {message.Text}");
            var chat = message.Chat;

            switch (message.Type)
            {
                // Case with Text processing.
                case MessageType.Text:
                    {
                        TextMessageCase.TextWorker(message, botClient, userContexts, chat, user, logger);
                        return;
                    }
                // Case with document processing.
                case MessageType.Document:
                    {
                        // Check right user's context.
                        if (!userContexts.TryGetValue(user.Id, out Context context))
                        {
                            string resp = "Не ломай, сначала напиши /start :( ";
                            await botClient.SendTextMessageAsync(
                            chat.Id,
                            resp);
                            string logStr = $"{user.Id} send a message \"{message.Text}\"" +
                                            $"\nbot response: {resp}\n";
                            logger.LogInformation(logStr);
                            return;
                        }
                        // Processing with correct document.
                        if (context.FileUploadEnabled)
                        {
                            DocumentCase.DocumentWorker(message, botClient, userContexts, context, chat, user, logger);
                            return;
                        }
                        
                        // Message, if user isn't ready to load file.
                        else
                        {
                            string resp = "Я пока не знаю, что мне с этим делать? :( " +
                                          "\nСкорее всего вы нажали кнопки не в том порядке. Попробуйте заново." +
                                          "\nДля начала работы, напиши /start";
                            await botClient.SendTextMessageAsync(
                                    chat.Id,
                                    resp);
                            string logStr = $"{user.Id} send a message \"{message.Text}\"" +
                                            $"\nbot response: {resp}\n";
                            logger.LogInformation(logStr);
                            return;
                        }
                    }
                // Case of wrong format of document.
                default:
                    string resp1 = "Я пока не умею с таким работать:( " +
                                  "\nПопробуйте заново." +
                                  "\nДля начала работы, напиши /start";
                    await botClient.SendTextMessageAsync(
                                    chat.Id,
                                    resp1);
                    string logStr1 = $"{user.Id} send a message \"{message.Text}\"" +
                                    $"\nbot response: {resp1}\n";
                    logger.LogInformation(logStr1);
                    return;
            }
        }
    }
    
}
