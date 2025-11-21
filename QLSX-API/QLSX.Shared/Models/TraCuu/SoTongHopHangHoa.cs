using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLSX.Shared.Models
{
    public class SoTongHopHangHoa
    {
        [Column("MADONVISUDUNG")]
        public string? MaDonViSuDung { get; set; }

        [Column("MHANGHOA")]
        public string? MaHangHoa { get; set; }

        [Column("TENHANGHOA")]
        public string? TenHangHoa { get; set; }
        
        [Column("DONVITINH")]
        public string? DonViTinh { get; set; }
        
        [Column("SOLUONGDAU")]
        public double? SoLuongDau { get; set; }
        
        [Column("SOLUONGNHAP")]
        public double? SoLuongNhap { get; set; }
        
        [Column("SOTIENNHAP")]
        public double? SoTienNhap { get; set; }
        
        [Column("SOTIENXUAT")]
        public double? SoTienXuat { get; set; }
        
        [Column("SOLUONGXUAT")]
        public double? SoLuongXuat { get; set; }

        [Column("SOTIENTONN")]
        public double? SoTienTonN { get; set; }

        [Column("SOTIENTONX")]
        public double? SoTienTonX { get; set; }
        
        [Column("SOLUONGTON")]
        public double? SoLuongTon { get; set; }
        
        [Column("TENDONVISUDUNG")]
        public string? TenDonViSuDung { get; set; }
        
        [Column("THOIGIAN")]
        public string? ThoiGian { get; set; }
        
        [Column("TENKHO")]
        public string? TenKho { get; set; }

        [Column("DONGIAN")]
        public double? DonGia { get; set; }

        [Column("DONGIA1")]
        public double? DonGia1 { get; set; }

        [Column("DONGIAX")]
        public double? DonGiaX { get; set; }

        [Column("SOTIENDAUN")]
        public double? SoTienDauN { get; set; }

        [Column("SOTIEN1")]
        public double? SoTien1 { get; set; }

        [Column("SOTIENDAUX")]
        public double? SoTienDauX { get; set; }

        [Column("DONGIANHAP")]
        public double? DonGiaNhap { get; set; }

        [Column("DONGIAXUAT")]
        public double? DonGiaXuat { get; set; }

        [Column("MKHO")]
        public string? MaKho { get; set; }

        [Column("MANHACC")]
        public string? MaNhaCC { get; set; }

        [Column("TENNHACC")]
        public string? TenNhaCC { get; set; }

        [Column("ID")]
        public int Id { get; set; }

        #region property not exist in database
        [NotMapped]
        public int? DMDonViSuDungId { get; set; }

        [NotMapped]
        public int? DMHangHoaId { get; set; }

        [NotMapped]
        public int? DMKhoHangId { get; set; }

        [NotMapped]
        public int? DMNhomHangHoaId { get; set; }

        [NotMapped]
        public DateTime? NgayCT { get; set; }

        [NotMapped]
        public string? DienGiai { get; set; }

        [NotMapped]
        public string? TenNhom { get; set; }

        [NotMapped]
        public double? SoTienTon { get; set; }

        [NotMapped]
        public double? SoTienDau { get; set; }
        #endregion
    }
}
