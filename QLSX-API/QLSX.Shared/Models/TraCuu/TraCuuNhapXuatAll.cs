using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSX.Shared.Models
{
    public class TraCuuNhapXuatAll
    {
        public int Id { get; set; }
        public string Loai { get; set; }
        public DateTime NgayCT { get; set; }
        public string SoChungTu { get; set; }
        public string? MaDoiTuong { get; set; }
        public string? TenDoiTuong { get; set; }
        public string? DiaChiDoiTuong { get; set; }
        public string? MaDonHang { get; set; }
        public Double SoTien { get; set; }
        public string? DienGiai { get; set; }

    }

    public class ViewNhapXuat
    {
        public int MaDonViSuDung { get; set; }
        public int Id { get; set; }
        public string Loai { get; set; }
        public DateTime NgayCT { get; set; }
        public string SoChungTu { get; set; }
        public string MaDoiTuong { get; set; }
        public string TenDoiTuong { get; set; }
        public string DiaChiDoiTuong { get; set; }
        public string DienGiai { get; set; }
        public Double SoTien { get; set; }
        public string MaHangHoa { get; set; }
        public string TenHangHoa { get; set; }
        public string DonViTinh { get; set; }
        public float SoLuong { get; set; }
        public float DonGia { get; set; }
        public float SoTienHang { get; set; }


    }
}
