using QLSX.Shared.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLSX.Shared.Models;


public class DanhMucHangHoaTonCuonModel : BaseModel
{
    public DanhMucHangHoaTonCuonModel(DanhMucHangHoaTonCuon entity)
    {
        Id = entity.Id;
        MaHangHoa = entity.MaHangHoa;
        TenHangHoa = entity.TenHangHoa;
        DonViTinh = entity.DonViTinh;
        GiaNhap = (double)entity.GiaNhap;
        GiaXuat = (double)entity.GiaXuat;
        DonGia = (double)entity.DonGia;
        TyTrong = (double)entity.TyTrong;
        KhoRongTon = (double)entity.KhoRongTon;
        DMNhomHangId = DMNhomHangs?.Id ?? 0;
        DMMauSacId = DMMauSacs?.Id ?? 0;
        DMDoDayId = DMDoDays?.Id ?? 0;
        DMLoaiTonId = DMLoaiTons?.Id ?? 0;
        DMChungLoaiId = DMChungLoais?.Id ?? 0;
        DMKieuSongId = DMKieuSongs?.Id ?? 0;
    }

    public DanhMucHangHoaTonCuonModel(DanhMucHangHoaTonCuon entity, DanhMucNhomHang nhomHang, DanhMucMauSac mauSac, DanhMucDoDay doDay, DanhMucLoaiTon loaiTon, DanhMucChungLoai chungLoai, DanhMucKieuSong kieuSong)
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
            DMNhomHangs = new DanhMucNhomHangModel(nhomHang);
            DMMauSacs = new DanhMucMauSacModel(mauSac);
            DMDoDays = new DanhMucDoDayModel(doDay);
            DMLoaiTons = new DanhMucLoaiTonModel(loaiTon);
            DMChungLoais = new DanhMucChungLoaiModel(chungLoai);
            DMKieuSongs = new DanhMucKieuSongModel(kieuSong);
            DMNhomHangId = DMNhomHangs?.Id ?? 0;
            DMMauSacId = DMMauSacs?.Id ?? 0;
            DMDoDayId = DMDoDays?.Id ?? 0;
            DMLoaiTonId = DMLoaiTons?.Id ?? 0;
            DMChungLoaiId = DMChungLoais?.Id ?? 0;
            DMKieuSongId = DMKieuSongs?.Id ?? 0;
        }
    }

    public DanhMucHangHoaTonCuonModel()
    {
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

    [Required(ErrorMessage = "Bạn chọn nhóm hàng")]
    public int? DMNhomHangId { get; set; }

    public int UserId { get; set; }
    public int? DMMauSacId { get; set; }
    public int? DMDoDayId { get; set; }
    public int? DMLoaiTonId { get; set; }
    public int? DMChungLoaiId { get; set; }

    //[Required(ErrorMessage = "Bạn phải chọn lại tính giá")]
    public int? DMKieuSongId { get; set; }

    public DanhMucNhomHangModel DMNhomHangs { get; set; }
    public DanhMucMauSacModel DMMauSacs { get; set; }
    public DanhMucDoDayModel DMDoDays { get; set; }
    public DanhMucLoaiTonModel DMLoaiTons { get; set; }
    public DanhMucChungLoaiModel DMChungLoais { get; set; }
    public DanhMucKieuSongModel DMKieuSongs { get; set; }
    public DMTinhGia DMTinhGias { get; set; }


    [NotMapped]
    public string TenNhomHang
    {
        get
        {
            return DMNhomHangs?.TenNhom;
        }
    }
    [NotMapped]
    public string TenChungLoai
    {
        get
        {
            return DMChungLoais?.ChiTieu;
        }
    }
    [NotMapped]
    public string TenMauSac
    {
        get
        {
            return DMMauSacs?.ChiTieu;
        }
    }
    [NotMapped]
    public string TenDoDay
    {
        get
        {
            return DMDoDays?.ChiTieu;
        }
    }
    [NotMapped]
    public string TenKieuSong
    {
        get
        {
            return DMKieuSongs?.ChiTieu;

        }
    }
    [NotMapped]
    public string TenLoaiTon
    {
        get
        {
            return DMLoaiTons?.ChiTieu;
        }
    }
}
