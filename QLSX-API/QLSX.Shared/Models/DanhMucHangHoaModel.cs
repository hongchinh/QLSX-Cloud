using QLSX.Shared.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLSX.Shared.Models;

public class DanhMucHangHoaModel
{
    public DanhMucHangHoaModel(DanhMucHangHoa entity)
    {
        if (entity != null)
        {
            Id = entity.Id;
            MaHangHoa = entity.MaHangHoa;
            TenHangHoa = entity.TenHangHoa;
            DonViTinh = entity.DonViTinh;
            GiaNhap = !entity.GiaNhap.HasValue ? 0 : (double)entity.GiaNhap;
            GiaXuat = !entity.GiaXuat.HasValue ? 0 : (double)entity.GiaXuat;
            DonGia = !entity.DonGia.HasValue ? 0 : (double)entity.DonGia;
            TyTrong = !entity.TyTrong.HasValue ? 0 : (double)entity.TyTrong;
            KhoRongTon = !entity.KhoRongTon.HasValue ? 0 : (double)entity.KhoRongTon;

            MaNhomHang = entity.MaNhomHang;
            TenNhomHang = entity.TenNhomHang;
            KieuSong = entity.KieuSong;
            MaKieuSong = entity.MaKieuSong;
            LoaiTon = entity.LoaiTon;
            MaLoaiTon = entity.MaLoaiTon;
            MaDoDay = entity.MaDoDay;
            DoDay = entity.DoDay;
            ChungLoai = entity.ChungLoai;
            MaChungLoai = entity.MaChungLoai;
            MauSac = entity.MauSac;
            MaMauSac = entity.MaMauSac;
        }
    }


    public DanhMucHangHoaModel()
    {
        MaHangHoa = string.Empty;
        TenHangHoa = string.Empty;
        DonViTinh = string.Empty;
    }

    public int Id { get; set; }

    [Required(ErrorMessage = "Bạn phải nhập vào mã hàng hóa")]
    public string MaHangHoa { get; set; }
    [Required(ErrorMessage = "Bạn phải nhập vào tên hàng hóa")]
    public string TenHangHoa { get; set; }

    [Required(ErrorMessage = "Bạn phải nhập vào đơn vị tính")]
    public string DonViTinh { get; set; }
    public double GiaNhap { get; set; }
    public double GiaXuat { get; set; }
    public double DonGia { get; set; }
    public double TyTrong { get; set; }
    public double KhoRongTon { get; set; }
    public double ChieuDai { get; set; }
    public string MaNhomHang { get; set; }
    public string TenNhomHang { get; set; }

    public string KieuSong { get; set; }
    public string MaKieuSong { get; set; }

    public string LoaiTon { get; set; }
    public string MaLoaiTon { get; set; }
    public string DoDay { get; set; }
    public string MaDoDay { get; set; }

    public string ChungLoai { get; set; }
    public string MaChungLoai { get; set; }


    public string MauSac { get; set; }
    public string MaMauSac { get; set; }

    public int UserId { get; set; }


    //[Required(ErrorMessage = "Bạn phải chọn lại tính giá")]
    public int DMTinhGiaId { get; set; }

    [NotMapped]
    public bool Selectted { get; set; }
    [NotMapped]
    public bool IsM2
    {
        get
        {
            if (MaNhomHang == "01" || MaNhomHang == "02") return true;
            else return false;
        }
    }
}
