using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSX.Web.Data
{
    public class TongHopThongKe_BK
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int Customer_NEW { get; set; }
        public int Customer_CALLED { get; set; }
        public int Customer_NOT_CALLED { get; set; }
        public int Customer_BUYED { get; set; }
        public int Customer_NOT_BUY { get; set; }
        public double Customer_PERCENT_COMPLETE { get; set; }

        [NotMapped]
        public bool IsEdit { get; set; } = false;

    }

}
