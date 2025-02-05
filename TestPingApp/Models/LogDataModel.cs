namespace TestPingApp.Models
{
    public class LogDataModel
    {
        public DateTime TimeStamp { get; set; }
        public string Url { get; set; }
        public string Message { get; set; }

        public LogDataModel() { }

        public LogDataModel(DateTime datetime, string message, string url)
        {
            TimeStamp = datetime;
            Message = message;
            Url = url;
        }
    }
}
