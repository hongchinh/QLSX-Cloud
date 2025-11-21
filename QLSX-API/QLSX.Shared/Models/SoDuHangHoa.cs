 
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace QLSX.Shared.Models
{
    public class SoDuHangHoa : BaseModel
    {
        public int Id { get; set; }
        public int DMHangHoaId { get; set; }
        public int DMKhoHangId { get; set; }
        public string MaHangHoa { get; set; }
        public string TenHangHoa { get; set; }
        public string DonViTinh { get; set; }
        public double SoLuong { get; set; }
        public double DonGia { get; set; }
        public double SoTien { get; set; }
        public double? ChieuDai { get; set; }
        public double? TongChieuDai { get; set; }
        public double? TongDienTich { get; set; }
        public double? KhoRongTon { get; set; }

        public string MaNhom { get; set; }

        public string? GhiChu { get; set; }
    }
}
