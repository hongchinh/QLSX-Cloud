
using System;
namespace QLSX.Shared.Models.Request
{
    public class CustomerSearchRequest : BaseRequest
    {
        public int? UserId { get; set; } = 0;
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }

        public string Keywords { get; set; }

        public string Region { get; set; }
        public string Contents { get; set; }
    }
}
