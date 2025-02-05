namespace TestPingApp.Models
{
    public class SiteDataModel
    {
        public string Name { get; set; }
        public string Url { get; set; }

        public SiteDataModel() { }

        public SiteDataModel(string name, string url)
        {
            Name = name;
            Url = url;
        }
    }
}
