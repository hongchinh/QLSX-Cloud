using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLSX.Shared.Models
{
    public class BaseReportRequest : BaseRequest
    {
        public string Id { get; set; }
        public string date1 { get; set; }
        public string date2 { get; set; }
    }
}
