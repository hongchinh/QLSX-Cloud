using FoolProof.Core;
using QLSX.Shared.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace QLSX.Shared.Models
{
    public class ThuChiModel : BaseModel
    {
        public ThuChiModel(ThuChi entity, List<DanhMucLoaiTien> danhMucLoaiTienList)
        {
            Id = entity.Id;
            Stt = entity.Stt ?? 0;
            Loai = entity.Loai;
            Phieu = entity.Phieu;
            MaDoiTuong = entity.MaDoiTuong;
            TenDoiTuong = entity.TenDoiTuong;
            DiaChi = entity.DiaChi;
            MaDonHang = entity.SoDonHang;
            NgayCT = entity.NgayCT;
            NgayHoanThanh = entity.NgayThanhToan;
            SoPhieu = entity.SoChungTu;
            SoTien = entity.SoTienVND;
            DienGiai = entity.DienGiai;
            GhiChu = entity.GhiChu;
            DMLoaiTiens = new DanhMucLoaiTienModel(danhMucLoaiTienList.FirstOrDefault(item => item.Id.ToString() == entity.LoaiTien));
            MaKhoanChi = entity.MaKhoanChi;
            TenKhoanChi = entity.TenKhoanChi;
            MaKhoanThu = entity.MaKhoanThu;
            TenKhoanThu = entity.TenKhoanThu;
            LoaiTien = entity.LoaiTien;
        }

        public ThuChiModel(ThuChi entity)
        {
            Id = entity.Id;
            Stt = entity.Stt ?? 0;
            Loai = entity.Loai;
            Phieu = entity.Phieu;
            MaDoiTuong = entity.MaDoiTuong;
            TenDoiTuong = entity.TenDoiTuong;
            DiaChi = entity.DiaChi;
            MaDonHang = entity.SoDonHang;
            NgayCT = entity.NgayCT;
            NgayHoanThanh = entity.NgayThanhToan;
            SoPhieu = entity.SoChungTu;
            SoTien = entity.SoTienVND;
            DienGiai = entity.DienGiai;
            GhiChu = entity.GhiChu;

            MaKhoanChi = entity.MaKhoanChi;
            TenKhoanChi = entity.TenKhoanChi;
            MaKhoanThu = entity.MaKhoanThu;
            TenKhoanThu = entity.TenKhoanThu;
            LoaiTien = entity.LoaiTien;


        }

        public ThuChiModel()
        {
        }



        public int Id { get; set; }
        public int Stt { get; set; }
        public string Loai { get; set; }
        public string LoaiDisplay
        {
            get
            {
                switch (Loai ?? string.Empty.ToLower())
                {
                    case "thutm":
                        return "Phiếu thu";
                    case "chitm":
                        return "Phiếu chi";
                    default:
                        return Loai;
                };
            }
        }
        public string Phieu { get; set; }
        [Required(ErrorMessage = "Bạn phải nhập vào mã đối tượng")]
        public string MaDoiTuong { get; set; }
        [Required(ErrorMessage = "Bạn phải nhập vào tên đối tượng")]
        public string TenDoiTuong { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }
        public string MaDonHang { get; set; }
        public string MaKhoanThu { get; set; }
        public string TenKhoanThu { get; set; }
        public string MaKhoanChi { get; set; }
        public string TenKhoanChi { get; set; }


        [Required(ErrorMessage = "Bạn phải nhập vào ngày lập phiếu")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = CubeCloud.Common.Constants.FormatCommons.Format_Short_Date)]
        public DateTime? NgayCT { get; set; }
        public DateTime? NgayHoanThanh { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập vào số phiếu")]
        public string SoPhieu { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập vào số tiền")]
        public double? SoTien { get; set; }
        [Required(ErrorMessage = "Bạn phải nhập vào diễn giải")]
        public string DienGiai { get; set; }
        public string GhiChu { get; set; }
        public string LoaiTien { get; set; }
        public int DMKhachHangId { get; set; }
        public DanhMucLoaiTienModel DMLoaiTiens { get; set; }
        public DanhMucKhoanThuModel DanhMucKhoanThuModels { get; set; }
        public DanhMucKhoanChiModel DMKhoanChis { get; set; }

        [NotMapped]
        public string PrintLink
        {
            get
            {
                return this.SoTien.ToString();
            }
        }

        [NotMapped]
        public string BangChu
        {
            get
            {
                return this.SoTien.ToString();
            }
        }

        [NotMapped]
        public bool IsThu
        {
            get
            {
                if (this.Loai == "thutm")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        [NotMapped]
        public bool IsChi
        {
            get
            {
                if (this.Loai == "chitm")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

    }
}
