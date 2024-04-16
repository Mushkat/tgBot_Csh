using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using Microsoft.Extensions.Logging;

namespace tg_bot
{
    /// <summary>
    /// Processing with Updates with type Message.
    /// </summary>
    public class TextMessageCase
    {
        async public static void TextWorker(Message message, ITelegramBotClient botClient,
            Dictionary<long, Context> userContexts, Chat chat, User user, ILogger logger)
        {
            //Start of work with bot.
            if (message.Text == "/start")
            {
                //Creating new Context for user and add it in dictionary.
                userContexts[user.Id] = new Context();
                //Show new inline menu.
                var inlineKeyboard = new InlineKeyboardMarkup(
                    new List<InlineKeyboardButton[]>()
                    {
                        new InlineKeyboardButton[]
                        {
                            InlineKeyboardButton.WithCallbackData("Работаем с файлом", "button_file"),
                        },
                    });
                string resp1 = "Привет! Давайте посмотрим на ваши файлы?";
                // Send new message to user.
                await botClient.SendTextMessageAsync(
                    chat.Id,
                    resp1,
                    replyMarkup: inlineKeyboard);
                string logStr1 = $"{user.Id} send a message \"{message.Text}\"" +
                                 $"\nbot response: {resp1} \n+ show inlineKeyboard\n";
                //Write info about message and bot response in file.
                logger.LogInformation(logStr1);
                return;
            }
            // Checking user Context for the need to write value for choosing field.
            if (userContexts.TryGetValue(user.Id, out Context context))
            {
                if (context.ChooseNeeded && !context.FileUploadEnabled)
                {
                    if (message.Text is null || message.Text.Equals(""))
                    {
                        string resp2 = "Лучше не надо такое вводить, пожалуйста. Попробуй еще раз.";
                        await botClient.SendTextMessageAsync(
                            chat.Id,
                            resp2);
                        string logStr2 = $"{user.Id} send a message \"{message.Text}\"" +
                                         $"\nbot response: {resp2}\n";
                        logger.LogInformation(logStr2);
                        return;
                    }
                    // Changing user context, add field for choosing to it.
                    context.NewChooseField = message.Text;
                    context.FileUploadEnabled = true;
                    string resp3 = "Окей, я запомнил твою строчку для выборки (0_0)";
                    // Send accepting message to user.
                    await botClient.SendTextMessageAsync(
                        chat.Id,
                        resp3);
                    string logStr3 = $"{user.Id} send a message \"{message.Text}\"" +
                                     $"\nbot response: {resp3} \n+ show inlineKeyboard to Choose Format\n";
                    // Write new log.
                    logger.LogInformation(logStr3);
                    userContexts[user.Id] = context;
                    // Show inline menu for choosing format of output file.
                    var InlineMenuFormat = InlineMenu.ChoosingFormatToUpload();
                    await botClient.SendTextMessageAsync(
                    chat.Id,
                    "Выберите формат выходного файла",
                    replyMarkup: InlineMenuFormat);
                    return;
                }
            }
            string resp = "Не понял, что вы хотите? :( " +
                        "\nДля начала работы напишите /start";
            await botClient.SendTextMessageAsync(
                chat.Id,
                resp);
            string logStr = $"{user.Id} send a message \"{message.Text}\"" +
                            $"\nbot response: {resp} \n";
            logger.LogInformation(logStr);
            return;
        }
    }
}
