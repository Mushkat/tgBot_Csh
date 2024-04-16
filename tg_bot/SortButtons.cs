using Telegram.Bot.Types.ReplyMarkups;

namespace tg_bot
{
    /// <summary>
    /// Inline menus for sorting.
    /// </summary>
    public class SortButtons
    {
        /// <summary>
        /// Menu for choosing field for sorting.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static Tuple<Context, InlineKeyboardMarkup> ButtonSort(Context context)
        {
            // Update user context.
            context.FileUploadEnabled = false;
            context.ChooseNeeded = false;
            context.SortNeeded = false;
            var inlineKeyboardChoos = new InlineKeyboardMarkup(
                new List<InlineKeyboardButton[]>()
                {
                    new InlineKeyboardButton[]
                    {
                        InlineKeyboardButton.WithCallbackData("Имена в алфавитном порядке", "button_sort_name"),
                        InlineKeyboardButton.WithCallbackData("Rank Year по убыванию", "button_sort_year"),
                    },
                });
            var res = new Tuple<Context, InlineKeyboardMarkup>(context, inlineKeyboardChoos);
            return res;
        }

        /// <summary>
        /// Updating user context after 
        /// user press the button of name sorting.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static Context ButtonSortName(Context context)
        {
            context.FileUploadEnabled = true;
            context.ChooseNeeded = false;
            context.SortNeeded = true;
            context.SortField = "name";
            return context;
        }

        /// <summary>
        /// Updating user context after 
        /// user press the button of year sorting.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static Context ButtonSortYear(Context context)
        {
            context.FileUploadEnabled = true;
            context.ChooseNeeded = false;
            context.SortNeeded = true;
            context.SortField = "year";
            return context;
        }

    }
}
