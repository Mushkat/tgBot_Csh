using Telegram.Bot.Types.ReplyMarkups;

namespace tg_bot
{
    /// <summary>
    /// Class for Keyboards and changing context for inline buttons.
    /// </summary>
    public static class ChooseButtons
    {
        /// <summary>
        /// Keyboard for choosing field.
        /// </summary>
        /// <param name="context"></param>
        /// <returns>tuple with keyboard and new context.</returns>
        public static Tuple<Context, InlineKeyboardMarkup> ButtonChoose(Context context)
        {
            context.FileUploadEnabled = false;
            context.SortNeeded = false;
            context.ChooseNeeded = false;
            var inlineKeyboardChoos = new InlineKeyboardMarkup(
                    new List<InlineKeyboardButton[]>()
                    {
                        new InlineKeyboardButton[]
                        {
                            InlineKeyboardButton.WithCallbackData("Main Objects", "button_choose_obj"),
                            InlineKeyboardButton.WithCallbackData("Workplace", "button_choose_work"),
                            InlineKeyboardButton.WithCallbackData("Rank Year", "button_choose_year"),
                        },
                    });
            var res = new Tuple<Context, InlineKeyboardMarkup>(context  , inlineKeyboardChoos);
            return res;
        }

        /// <summary>
        /// Changing context for ready to take field for choosing.
        /// </summary>
        /// <param name="context"></param>
        /// <returns> new Context.</returns>
        public static Context ButtonChooseObj(Context context)
        {
            context.FileUploadEnabled = false;
            context.SortNeeded = false;
            context.ChooseNeeded = true;
            context.ChooseField = "mainobject";
            return context;
        }

        /// <summary>
        /// Changing context for ready to take field for choosing.
        /// </summary>
        /// <param name="context"></param>
        /// <returns>new Context.</returns>
        public static Context ButtonChooseWork(Context context)
        {
            context.FileUploadEnabled = false;
            context.SortNeeded = false;
            context.ChooseNeeded = true;
            context.ChooseField = "workplace";
            return context;
        }

        /// <summary>
        /// Changing context for ready to take field for choosing.
        /// </summary>
        /// <param name="context"></param>
        /// <returns>new Context.</returns>
        public static Context ButtonChooseYear(Context context)
        {
            context.FileUploadEnabled = false;
            context.SortNeeded = false;
            context.ChooseNeeded = true;
            context.ChooseField = "year";
            return context;
        }
    }
}
