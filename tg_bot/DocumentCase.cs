using FileProcessing;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace tg_bot
{
    /// <summary>
    /// 
    /// </summary>
    public class DocumentCase
    {
        async public static void DocumentWorker(Message message, ITelegramBotClient botClient, 
            Dictionary<long, Context> userContexts, Context context, Chat chat, User user, ILogger logger)
        {
            var file = message.Document;
            var fileStream = new MemoryStream();
            List<Recreator> list = null;

            // Get file extension for choosing a write method. 
            var fileExtension = Path.GetExtension(file.FileName);

            if (fileExtension.Equals(".json", StringComparison.OrdinalIgnoreCase))
            {
                await botClient.GetInfoAndDownloadFileAsync(file.FileId, fileStream);
                list = JsonProcessing.Read(fileStream);
            }
            else if (fileExtension.Equals(".csv", StringComparison.OrdinalIgnoreCase))
            {
                await botClient.GetInfoAndDownloadFileAsync(file.FileId, fileStream);
                list = CsvProcessing.Read(fileStream);
            }
            // Case for wrong format.
            else
            {
                string resp = "Не поддерживаю такой формат. Загрузи JSON или CSV файл.";
                await botClient.SendTextMessageAsync(message.Chat.Id, resp);
                fileStream.Close();
                string logStr = $"{user.Id} send a message \"{message.Text}\" + document" +
                                $"\nbot response: {resp}\n";
                logger.LogInformation(logStr);
                return;
            }
            // Check context for right buttons order.
            if (context.SortNeeded)
            {
                list = Sorting.Sort(list, context.SortField);
            }
            else if (context.ChooseNeeded)
            {
                list = Choosing.ChoosingByField(list, context.NewChooseField, context.ChooseField);
            }
            if (context.UploadFormat.Equals("json"))
            {
                Stream retStream = JsonProcessing.Write(list);
                string resp = "Успешно! Держи свой новый json файл" +
                    "\nЕсли файл пустой, то в нем были некорректные объекты или ваш результат выборки пуст." +
                    "\nЧтобы начать заново напиши /start";
                Message mes = await botClient.SendDocumentAsync(
                    chatId: chat.Id,
                    document: InputFile.FromStream(stream: retStream, fileName: "returnjs.json"),
                    caption: resp);
                string logStr = $"{user.Id} send a message \"{message.Text}\" + document .json" +
                                $"\nbot response: {resp} \n+ file .json\n";
                logger.LogInformation(logStr);
                retStream.Close();
            }
            else if (context.UploadFormat.Equals("csv"))
            {
                Stream retStream = CsvProcessing.Write(list);
                string resp = "Успешно! Держи свой новый csv файл" +
                    "\nЕсли файл пустой, то в нем были некорректные объекты или ваш результат выборки пуст." +
                    "\nЧтобы начать заново напиши /start";
                Message mes = await botClient.SendDocumentAsync(
                    chatId: chat.Id,
                    document: InputFile.FromStream(stream: retStream, fileName: "returncsv.csv"),
                    caption: resp);
                string logStr = $"{user.Id} send a message \"{message.Text}\" + document .csv" +
                                $"\nbot response: {resp} \n+ file .csv\n";
                retStream.Close();
            }
            fileStream.Close();
            context.FileUploadEnabled = false;
            userContexts[user.Id] = context;
        }
    }
}
