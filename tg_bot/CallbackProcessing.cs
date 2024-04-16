using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace tg_bot
{
    /// <summary>
    /// Processing of reaction on keyboards buttons.
    /// </summary>
    public static class CallbackProcessing
    {
        async public static void CallbackProc(Update update, ITelegramBotClient botClient,
            Dictionary<long, Context> userContexts, ILogger logger)
        {
            var callbackQuery = update.CallbackQuery;
            var user = callbackQuery.From;

            Console.WriteLine($"{user.FirstName} ({user.Id}) нажал на кнопку: {callbackQuery.Data}");

            var chat = callbackQuery.Message.Chat;
            // Check right order of buttons.
            if (!userContexts.TryGetValue(user.Id, out Context context))
            {
                string resp = "Не ломай, сначала напиши /start :( ";
                await botClient.SendTextMessageAsync(
                chat.Id,
                resp);
                string logStr = $"{user.Id} press the button \"{callbackQuery.Data}\"" +
                                $"\nbot response: {resp}\n";
                logger.LogInformation(logStr);
                return;
            }

            switch (callbackQuery.Data)
            {
                // Case of button of start working.
                case "button_file":
                    {
                        await botClient.AnswerCallbackQueryAsync(callbackQuery.Id);

                        var inlineKeyboardFile = InlineMenu.MenuButtonFile();
                        string resp = "Выберите действие с файлом";
                        await botClient.SendTextMessageAsync(
                            chat.Id,
                            resp,
                            replyMarkup: inlineKeyboardFile);
                        string logStr = $"{user.Id} press the button \"{callbackQuery.Data}\"" +
                                $"\nbot response: {resp}\n + inlineKeyboardFile\n";
                        logger.LogInformation(logStr);
                        return;
                    }
                // Case of button for choosing field.
                case "button_choose":
                    {
                        await botClient.AnswerCallbackQueryAsync(callbackQuery.Id);

                        var tup = ChooseButtons.ButtonChoose(context);
                        userContexts[user.Id] = tup.Item1;
                        var inlineKeyboardChoos = tup.Item2;
                        string resp = "По какому полю делаем выборку?";
                        await botClient.SendTextMessageAsync(
                            chat.Id,
                            resp,
                            replyMarkup: inlineKeyboardChoos);
                        string logStr = $"{user.Id} press the button \"{callbackQuery.Data}\"" +
                                        $"\nbot response: {resp}\n + inlineKeyboardChoos; User context was changed\n";
                        logger.LogInformation(logStr);
                        return;

                    }
                // Case of button for value of choosing in Main Object. 
                case "button_choose_obj":
                    {
                        await botClient.AnswerCallbackQueryAsync(callbackQuery.Id);

                        userContexts[user.Id] = ChooseButtons.ButtonChooseObj(context);
                        string resp = $"Отлично, жду строчку для выборки. Что вы хотите найти в Main Object?";
                        await botClient.SendTextMessageAsync(
                            chat.Id,
                            resp);
                        string logStr = $"{user.Id} press the button \"{callbackQuery.Data}\"" +
                                        $"\nbot response: {resp}\nUser context was changed\n";
                        logger.LogInformation(logStr);
                        return;


                    }
                // Case of button for value of choosing in Workplace. 
                case "button_choose_work":
                    {
                        await botClient.AnswerCallbackQueryAsync(callbackQuery.Id);

                        userContexts[user.Id] = ChooseButtons.ButtonChooseWork(context);
                        string resp = $"Отлично, жду строчку для выборки. Что вы хотите найти в Workplace?";
                        await botClient.SendTextMessageAsync(
                            chat.Id,
                            resp);
                        string logStr = $"{user.Id} press the button \"{callbackQuery.Data}\"" +
                                        $"\nbot response: {resp}\nUser context was changed\n";
                        logger.LogInformation(logStr);
                        return;
                    }
                // Case of button for value of choosing in Rank year. 
                case "button_choose_year":
                    {
                        await botClient.AnswerCallbackQueryAsync(callbackQuery.Id);

                        userContexts[user.Id] = ChooseButtons.ButtonChooseYear(context);
                        string resp = $"Отлично, жду строчку для выборки. Что вы хотите найти в RankYear?";
                        await botClient.SendTextMessageAsync(
                            chat.Id,
                            resp);
                        string logStr = $"{user.Id} press the button \"{callbackQuery.Data}\"" +
                                        $"\nbot response: {resp}\nUser context was changed\n";
                        logger.LogInformation(logStr);
                        return;
                    }
                // Case of button to start sorting.
                case "button_sort":
                    {
                        await botClient.AnswerCallbackQueryAsync(callbackQuery.Id);

                        var tup = SortButtons.ButtonSort(context);
                        userContexts[user.Id] = tup.Item1;
                        var inlineKeybordSort = tup.Item2;
                        string resp = "Какую сортировку вы хотите произвести?";
                        await botClient.SendTextMessageAsync(
                            chat.Id,
                            resp,
                            replyMarkup: inlineKeybordSort);
                        string logStr = $"{user.Id} press the button \"{callbackQuery.Data}\"" +
                                        $"\nbot response: {resp}\n + inlineKeyboardSort; User context was changed\n";
                        logger.LogInformation(logStr);
                        return;
                    }
                // Case of button for sorting names.
                case "button_sort_name":
                    {
                        await botClient.AnswerCallbackQueryAsync(callbackQuery.Id);

                        userContexts[user.Id] = SortButtons.ButtonSortName(context);

                        var InlineMenuFormat = InlineMenu.ChoosingFormatToUpload();
                        string resp = "Выбери формат выходного файла";
                        await botClient.SendTextMessageAsync(
                            chat.Id,
                            resp,
                            replyMarkup: InlineMenuFormat);
                        string logStr = $"{user.Id} press the button \"{callbackQuery.Data}\"" +
                                        $"\nbot response: {resp}\n + inlineKeyboarChoosFormat; User context was changed\n";
                        logger.LogInformation(logStr);
                        return;
                    }
                // Case of button for sorting years.
                case "button_sort_year":
                    {
                        await botClient.AnswerCallbackQueryAsync(callbackQuery.Id);

                        userContexts[user.Id] = SortButtons.ButtonSortYear(context);

                        var InlineMenuFormat = InlineMenu.ChoosingFormatToUpload();
                        string resp = "Выбери формат выходного файла";
                        await botClient.SendTextMessageAsync(
                        chat.Id,
                        resp,
                        replyMarkup: InlineMenuFormat);
                        string logStr = $"{user.Id} press the button \"{callbackQuery.Data}\"" +
                                        $"\nbot response: {resp}\n + inlineKeyboarChoosFormat; User context was changed\n";
                        logger.LogInformation(logStr);
                        return;
                    }
                // Case of button t upload new csv file.
                case "button_upload_csv":
                    {
                        await botClient.AnswerCallbackQueryAsync(callbackQuery.Id);

                        context.UploadFormat = "csv";
                        userContexts[user.Id] = context;
                        string resp = $"Окей, жду csv или json файл.";
                        await botClient.SendTextMessageAsync(
                            chat.Id,
                            resp);
                        string logStr = $"{user.Id} press the button \"{callbackQuery.Data}\"" +
                                        $"\nbot response: {resp}\n + inlineKeyboarChoosFormat; User context was changed\n";
                        logger.LogInformation(logStr);
                        return;
                    }
                // Case of button t upload new json file.
                case "button_upload_json":
                    {
                        await botClient.AnswerCallbackQueryAsync(callbackQuery.Id);

                        context.UploadFormat = "json";
                        userContexts[user.Id] = context;
                        string resp = $"Окей, жду csv или json файл.";
                        await botClient.SendTextMessageAsync(
                            chat.Id,
                            resp);
                        string logStr = $"{user.Id} press the button \"{callbackQuery.Data}\"" +
                                        $"\nbot response: {resp}\n + inlineKeyboarChoosFormat; User context was changed\n";
                        logger.LogInformation(logStr);
                        return;
                    }

            }

            return;
        }
    }
}
