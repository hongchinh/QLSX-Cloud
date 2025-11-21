using QLSX.Shared.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLSX.Shared.Models;

public class NoiDungNhapXuatTonCuonModel : BaseModel
{
    public NoiDungNhapXuatTonCuonModel()
    {
    }

    public NoiDungNhapXuatTonCuonModel(NoiDungNhapXuatTonCuon entity)
    {
        if (entity != null)
        {
            Id = entity.IdId;
            MaHangHoa = entity.MaHangHoa;
            TenHangHoa = entity.TenHangHoa;
            DonViTinh = entity.DonViTinh;
            SoLuong = entity.SoLuong ?? 0;
            DonGia = entity.DonGia ?? 0;
            SoTien = entity.SoTien ?? 0;
            DonGiaHoaHong = entity.TyLeCkNv ?? 0;
            SoTienHoaHong = entity.SoTienCkNv ?? 0;
            NhapXuatId = entity.NhapXuatTonCuonId;
            KhoRongTon = entity.KhoRongTon ?? 0;
            ChieuDai = entity.ChieuDai ?? 0;
            TongChieuDai = entity.TongChieuDai ?? 0;
            TongDienTich = entity.TongDienTich ?? 0;
            //DMHangHoaId = entity.DMHangHoaId;
            //DMKhoHangId = entity.DMKhoHangId;
            //DMTinhGiaId = entity.DMTinhGiaId;
            //IsEditing = entity.IsEditing;
            GhiChu = entity.DienGiai;
            //IsM2 = entity.IsM2;
            //IsMD = entity.IsMd;
            //IsSoLuong = entity.IsSoLuong;
            SoLuongTon = entity.SoLuongTon ?? 0;
            MaNhom = entity.MaNhom;
            TenNhom = entity.TenNhom;
            KieuSong = entity.KieuSong;
            MaKieuSong = entity.MaKieuSong;
            LoaiTon = entity.LoaiTon;
            MaLoaiTon = entity.MaLoaiTon;
            DoDay = entity.DoDay;
            ChungLoai = entity.ChungLoai;
            MaChungLoai = entity.MaChungLoai;
            MauSac = entity.MauSac;
            MaMauSac = entity.MaMauSac;
        }
    }

    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
    [NotMapped] public int Index { get; set; }
    public int Id { get; set; }

    public string MaHangHoa { get; set; }
    [Required(ErrorMessage = "Bạn phải nhập vào tên hàng hóa")]
    public string TenHangHoa { get; set; }
    public string DonViTinh { get; set; }

    public double SoLuong { get; set; }

    public double DonGia { get; set; }

    public double SoTien { get; set; }

    public double DonGiaHoaHong { get; set; }

    public double SoTienHoaHong { get; set; }

    public int NhapXuatId { get; set; }


    public double KhoRongTon { get; set; }

    public double ChieuDai { get; set; }


    public double TongChieuDai { get; set; }

    //[DisplayFormat(DataFormatString = "{0:###,###.##", ApplyFormatInEditMode = true)]
    public double TongDienTich { get; set; }
    public int DMHangHoaId { get; set; }
    public int DMKhoHangId { get; set; }
    public int DMTinhGiaId { get; set; }

    public bool IsEditing { get; set; }

    public string GhiChu { get; set; }
    public string MaNhom { get; set; }
    public string TenNhom { get; set; }

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


    [NotMapped]
    public bool IsM2 { get; set; }
    [NotMapped]
    public bool IsMD { get; set; }
    [NotMapped]
    public bool IsSoLuong { get; set; }
    [NotMapped]
    public double SoLuongTon { get; set; }
    public QLSX.Shared.Models.DanhMucHangHoaModel? DMHangHoa { get; set; }
}
