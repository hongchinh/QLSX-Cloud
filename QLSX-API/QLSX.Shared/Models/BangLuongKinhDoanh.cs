using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSX.Shared.Models
{
    public class BangLuongKinhDoanh
    {
        public int THANG { get; set; }
        public int NAM { get; set; }
        public int MaNV { get; set; }
        public string TenNV { get; set; }
        public Double TRONG_THANG_HOANTHANH_TM { get; set; }
        public Double TRONG_THANG_HOANTHANH_TC { get; set; }
        public Double TRUOC_THANG_HOANTHANH_TRUOC_TM { get; set; }
        public Double TRUOC_THANG_HOANTHANH_TRUOC_TC { get; set; }
        public Double TRONG_CHUA_HOANTHANH_TM { get; set; }
        public Double TRONG_CHUA_HOANTHANH_TC { get; set; }
        public Double TRUOC_CHUA_HOANTHANH_TM { get; set; }
        public Double TRUOC_CHUA_HOANTHANH_TC { get; set; }
    }


}
