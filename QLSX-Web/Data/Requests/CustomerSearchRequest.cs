
using System;
namespace QLSX.Web.Data
{
    public class CustomerSearchRequest_BK : BaseRequest
    {
        public int? UserId { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }

        public string Keywords { get; set; }

        public string Region { get; set; }
        public string Contents { get; set; }


    }
}
