using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSX.Shared.Models
{
    public class DieuChuyen : BaseModel
    {
        public int Id { get; set; }
        public string Loai { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập vào ngày lập phiếu")]
        public DateTime? NgayCT { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập vào ngày hoàn thành")]
        public DateTime? NgayHT { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập vào số phiếu")]
        public String SoCT { get; set; }
        [Required(ErrorMessage = "Bạn phải nhập vào mã đơn vị")]
        public String? MaDonVi { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập vào tên đơn vị")]
        public String? TenDonVi { get; set; }
        public String? DiaChi { get; set; }
        public String? DienThoai { get; set; }
        public bool? ThuongMai { get; set; }
        public bool? ThiCong { get; set; }
        public int? MaNhanVienId { get; set; }
        public int? MaDonViId { get; set; }
        public int? MaLyDoId { get; set; }
        public int? UserId { get; set; }

      
        public UserModel User { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập vào kho hàng")]
        public int DMKhoHangId { get; set; }
        [Required(ErrorMessage = "Bạn phải nhập vào kho hàng")]
        [Range (1, int.MaxValue,ErrorMessage = "Bạn phải nhập vào kho hàng")]
        public int DMKhoHang1Id { get; set; }
        public DanhMucKhoHangModel DMKhoHang { get; set; }
        public DateTime? NgayHenThanhToan { get; set; }
        public DateTime? NgayGiao { get; set; }
        public string? NoiGiao { get; set; }
        public string? PhuongTien { get; set; }
        public int? DMHinhThucTTId { get; set; }
        public int? DMTinhTrangId { get; set; }
        public double? TyleVAT { get; set; }

        public bool? TuThiCong { get; set; }
        public string? DienGiai { get; set; }

        public bool? ThueNgoai { get; set; }
        public double? TyLeThiCong { get; set; }
        public double? SoTienTT { get; set; }
      
        public int? NVKyThuatId { get; set; }

        public int? DMKhachHangId { get; set; }
        public int? DMLoaiTienId { get; set; }
        public DanhMucLoaiTienModel DMLoaiTiens { get; set; }

        [ValidateComplexType]
        public List<NoiDungDieuChuyen> NoiDungDieuChuyens { get; set; } = new List<NoiDungDieuChuyen>();
    }
}
