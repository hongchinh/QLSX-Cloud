namespace SaleAPI.Models
{
    public class BaseUrlConfiguration
    {
        public const string CONFIG_NAME = "AppSettings";

        public string ApiBase { get; set; }
        public string WebBase { get; set; }
    }
}
