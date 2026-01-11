
using MudBlazor;
using QLSX.Shared.Entities;
using System;
using System.Collections.Generic;

namespace QLSX.Shared.Models
{
    public class ThuChiSearchRequest : BaseRequest
    {
        public int? Id { get; set; }
        public DateTime? NgayLap_From { get; set; }
        public DateTime? NgayLap_To { get; set; }

        public DateTime? NgayHoanThanh_From { get; set; }
        public DateTime? NgayHoanThanh_To { get; set; }

        public string MaDonVi { get; set; }
        public string TenDonVi { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }
        public string DonViSearch { get; set; }

        public int Index { get; set; }
        public string Loai { get; set; }
        public string SoPhieu { get; set; }
        public string MaDonHang { get; set; }
        public int DMKhoanChiId { get; set; }
        public int DMKhoanThuId { get; set; }
        public string DienGiai { get; set; }
        public string MaKhoanThu { get; set; }
        public string TenKhoanThu { get; set; }
        public string MaKhoanChi { get; set; }
        public string TenKhoanChi { get; set; }
        public double? SoTien_From { get; set; }
        public double? SoTien_To { get; set; }
        public ICollection<FilterDefinition<ThuChi>> Filter { get; set; }
    }
}
