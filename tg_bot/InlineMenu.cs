

using Telegram.Bot.Types.ReplyMarkups;

namespace tg_bot
{
    /// <summary>
    /// Class with inline keyboards creating.
    /// </summary>
    public class InlineMenu
    {
        /// <summary>
        /// Keyboard for choosing format of uploading file.
        /// </summary>
        /// <returns></returns>
        public static InlineKeyboardMarkup ChoosingFormatToUpload()
        {
            InlineKeyboardMarkup inlineKeyboardChoos = new InlineKeyboardMarkup(
                new List<InlineKeyboardButton[]>()
                {new InlineKeyboardButton[]
                {
                    InlineKeyboardButton.WithCallbackData("Хочу в csv", "button_upload_csv"),
                    InlineKeyboardButton.WithCallbackData("Хочу в json", "button_upload_json"),
                },
                });
            return inlineKeyboardChoos;
        }

        /// <summary>
        /// Keyboard for choosing process.
        /// </summary>
        /// <returns></returns>
        public static InlineKeyboardMarkup MenuButtonFile()
        {
            var inlineKeyboardFile = new InlineKeyboardMarkup(
                new List<InlineKeyboardButton[]>()
                {
                    new InlineKeyboardButton[]
                    {
                        InlineKeyboardButton.WithCallbackData("Произвести выборку", "button_choose"),
                        InlineKeyboardButton.WithCallbackData("Хочу отсортировать файл", "button_sort"),
                    },
                });
            return inlineKeyboardFile;
        }
    }
}
