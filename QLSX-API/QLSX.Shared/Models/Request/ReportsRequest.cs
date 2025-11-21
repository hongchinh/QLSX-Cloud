using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLSX.Shared.Models
{
    public class ReportsRequest : BaseRequest
    {
        public Guid id { get; set; }
        public Byte[] data { get; set; }
    }
}
