namespace tg_bot
{
    /// <summary>
    /// Class for saving user context.
    /// </summary>
    public class Context
    {
        public bool FileUploadEnabled { get; set; }
        public bool ChooseNeeded { get; set; }
        public bool SortNeeded { get; set; }
        public string SortField { get; set; }
        public string NewChooseField { get; set; }
        public string ChooseField { get; set; }
        public string UploadFormat { get; set; }
        public Context()
        {
            FileUploadEnabled = false;
            ChooseNeeded = false;
            SortNeeded = false;
            SortField = "empty";
            NewChooseField = "empty";
            ChooseField = "empty";
            UploadFormat = "empty";
        }
    }
}
