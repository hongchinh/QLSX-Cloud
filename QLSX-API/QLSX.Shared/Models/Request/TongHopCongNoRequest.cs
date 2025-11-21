using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSX.Shared.Models.Request
{
    public class TongHopCongNoRequest
    {
        public int hien { get; set; }
        public string date1 { get; set; }
        public string date2 { get; set; }
        public string MNhom { get; set; }
        public int RoleId { get; set; }
        public string mdvsd { get; set; }
        public string tmptblOK { get; set; }
    }
}
