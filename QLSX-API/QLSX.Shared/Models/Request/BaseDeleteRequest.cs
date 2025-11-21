using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLSX.Shared.Models
{
    public class BaseDeleteRequest : BaseRequest
    {
        public int Id { get; set; }
    }
}
