using System;

namespace QLSX.Shared.Models.Request;

public class DonHangRequest
{
    public DateTime? NgayCTFrom { get; set; }

    public DateTime? NgayCTTo { get; set; }

    public string SoCT { get; set; }

    public string NgayCT { get; set; }

    public string NgayLSX { get; set; }

    public string SoPhieuLSX { get; set; }

    public string NgayXK { get; set; }

    public string SoPhieuXK { get; set; }

    public string DienGiai { get; set; }

    public string MaDonVi { get; set; }

    public string TenDonVi { get; set; }

    public string DiaChi { get; set; }

    public string NguoiQL { get; set; }

    public DateTime? ThoiGianGiaoHang { get; set; }

    public string DiaDiem { get; set; }

    public string MaHangHoa { get; set; }

    public string TenHangHoa { get; set; }

    public int TrangThaiDonHang { get; set; }

    public bool Equals(DonHangRequest other)
    {
        if (other == null)
            return false;

        return NgayCTFrom == other.NgayCTFrom &&
               NgayCTTo == other.NgayCTTo &&
               SoCT == other.SoCT &&
               DienGiai == other.DienGiai &&
               MaDonVi == other.MaDonVi &&
               TenDonVi == other.TenDonVi &&
               DiaChi == other.DiaChi &&
               NguoiQL == other.NguoiQL &&
               ThoiGianGiaoHang == other.ThoiGianGiaoHang &&
               DiaDiem == other.DiaDiem &&
               MaHangHoa == other.MaHangHoa &&
               TenHangHoa == other.TenHangHoa &&
               NgayCT == other.NgayCT &&
               NgayLSX == other.NgayLSX &&
               SoPhieuLSX == other.SoPhieuLSX &&
               NgayXK == other.NgayXK &&
               SoPhieuXK == other.SoPhieuXK &&
               TrangThaiDonHang == other.TrangThaiDonHang;
    }

    public DonHangRequest Clone()
    {
        return new DonHangRequest
        {
            NgayCTFrom = this.NgayCTFrom,
            NgayCTTo = this.NgayCTTo,
            SoCT = this.SoCT,
            DienGiai = this.DienGiai,
            MaDonVi = this.MaDonVi,
            TenDonVi = this.TenDonVi,
            DiaChi = this.DiaChi,
            NguoiQL = this.NguoiQL,
            ThoiGianGiaoHang = this.ThoiGianGiaoHang,
            DiaDiem = this.DiaDiem,
            MaHangHoa = this.MaHangHoa,
            TenHangHoa = this.TenHangHoa,
            TrangThaiDonHang = this.TrangThaiDonHang,
            NgayCT = this.NgayCT,
            NgayLSX = this.NgayLSX,
            SoPhieuLSX = this.SoPhieuLSX,
            NgayXK = this.NgayXK,
            SoPhieuXK = this.SoPhieuXK
        };
    }
}
