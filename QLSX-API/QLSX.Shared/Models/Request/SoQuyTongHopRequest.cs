using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLSX.Shared.Models
{
    public class SoQuyTongHopRequest  
    {
        public int? UserId { get; set; }


        public DateTime DateNow { get; set; } = DateTime.Now;


        public DateTime? From { get; set; } = DateTime.Now;


        public DateTime? To { get; set; } = DateTime.Now.AddDays(10);
    }
}
