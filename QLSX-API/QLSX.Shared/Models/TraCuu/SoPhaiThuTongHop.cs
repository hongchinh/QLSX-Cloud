using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSX.Shared.Models
{
    public class SoPhaiThuTongHop
    {
        public string? ThoiGian { get; set; }
        public int? DMKhachHangId { get; set; }
        public string? MaDonVi { get; set; }
        public string? TenDonVi { get; set; }
        public string? DiaChi { get; set; }
         
        public Double? SoDuDau { get; set; }
        public Double? SoTienMua { get; set; }
        public Double? SoTienTra { get; set; }
        public Double? SoConLai { get; set; }
        public int? DMDonViSuDungId { get; set; }
        public string? TenDonViSuDung { get; set; }

        
        public DateTime? NgayCT { get; set; }
        public string? DienGiai { get; set; }
       
        public string? SoCTNX { get; set; }
        public string? SoCTTC { get; set; }
        public string? Lphieu { get; set; }
        public int? Loai { get; set; }
        public int? DMNhomKhachHangId { get; set; }
        public string? TenNhom { get; set; }
        public int Id { get; set; }

    }


}
