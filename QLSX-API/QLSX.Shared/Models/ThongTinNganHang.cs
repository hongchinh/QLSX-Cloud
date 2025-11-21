using System;
using System.Collections.Generic;

namespace QLSX.Shared.Models
{
    public partial class ThongTinNganHang
    {
        public string code { get; set; }
        public string desc { get; set; }

        public List<NganHangDetail>  data { get; set; }

        }

    public class NganHangDetail
    {
        public string name { get; set; }
        public string code { get; set; }
        public string bin { get; set; }
        public string short_name { get; set; }
        public string swift_code { get; set; }
    }
}
