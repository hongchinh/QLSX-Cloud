using System;
namespace QLSX.Shared.Models;

public class NhapXuatTonCuonSearchRequest : BaseRequest
{
    public int? Id { get; set; }
    public int? UserId { get; set; }
    public DateTime? NgayLap_From { get; set; }
    public DateTime? NgayLap_To { get; set; }

    public double? SoTien_From { get; set; }
    public double? SoTien_To { get; set; }

    public double? SoTienTT_From { get; set; }
    public double? SoTienTT_To { get; set; }


    public string? MaKhoHang { get; set; }
    public int DMLoaiTienId { get; set; }


    public int Index { get; set; }
    public string Loai { get; set; }
    public string SoPhieu { get; set; }

    public string MaDonVi { get; set; }
    public string TenDonVi { get; set; }
    public string DiaChi { get; set; }
    public string DienThoai { get; set; }
    public double? SoTien { get; set; }
    public double? SoTienTT { get; set; }
    public double? TongCong { get; set; }
    public string TenKho { get; set; }
    public string DienGiai { get; set; }
    public string MaHangHoa { get; set; }
    public string TenHangHoa { get; set; }
    public string DonViTinh { get; set; }
}
