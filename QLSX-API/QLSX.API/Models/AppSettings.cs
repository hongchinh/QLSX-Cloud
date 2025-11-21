using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaleAPI.Models
{
    public class AppSettings
    {
        public const string CONFIG_NAME = "AppSettings";
        public string ApiBase { get; set; }
        public string WebBase { get; set; }
        public string ReportUrl { get; set; }
        public string QRCodeLink { get; set; }
    }
}
