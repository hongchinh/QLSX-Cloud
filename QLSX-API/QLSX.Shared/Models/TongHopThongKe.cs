using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSX.Shared.Models
{
    public class TongHopThongKe
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int Customer_NEW { get; set; }
        public int Customer_CALLED { get; set; }
        public int Customer_NOT_CALLED { get; set; }
        public int Customer_BUYED { get; set; }
        public int Customer_NOT_BUY { get; set; }
        public double  Customer_PERCENT_COMPLETE { get; set; }
    }

}
