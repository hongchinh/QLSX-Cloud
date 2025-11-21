using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sale.Web.Data
{
    public class SoTongHopHangHoa
    {
        public int?  DMDonViSuDungId { get; set; }
        public int? DMHangHoaId { get; set; }
        public string? MaHangHoa { get; set; }
        public string? TenHangHoa { get; set; }
        public string? DonViTinh { get; set; }
         
        public Double? SoLuongDau { get; set; }
        public Double? SoTienDau { get; set; }
        public Double? SoLuongNhap { get; set; }
        public Double? SoTienNhap { get; set; }
        public Double? SoTienXuat { get; set; }
        public Double? SoLuongXuat { get; set; }
        public Double? SoTienTon { get; set; }
        public Double? SoLuongTon { get; set; }
        public DateTime? NgayCT { get; set; }
        public string? DienGiai { get; set; }
        public string? TenDonViSuDung { get; set; }
        public string? TenNhom { get; set; }
        public string? ThoiGian { get; set; }
        public string? TenKho { get; set; }
        public int? DMKhoHangId { get; set; }
        public int? DMNhomHangHoaId { get; set; }
        public int Id { get; set; }

    }


}
