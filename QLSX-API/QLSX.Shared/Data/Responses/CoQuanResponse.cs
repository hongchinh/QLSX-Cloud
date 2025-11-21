using QLSX.Shared.Interfaces;
using QLSX.Shared.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace QLSX.Shared.Data.Responses
{
    public class CoQuanResponse : IApiWrapperResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("maDonVi")]
        public string MaDonVi { get; set; }
        [JsonPropertyName("tenDonVi")]
        public string TenDonVi { get; set; }
        [JsonPropertyName("diaChi")]
        public string DiaChi { get; set; }
        [JsonPropertyName("maSoThue")]
        public string MaSoThue { get; set; }
        [JsonPropertyName("soTaiKhoan")]

        public string SoTaiKhoan { get; set; }
        [JsonPropertyName("tenNganHang")]
        public string TenNganHang { get; set; }
        [JsonPropertyName("dienThoai")]
        public string DienThoai { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("website")]
        public string Website { get; set; }
        [JsonPropertyName("ghiChu")]
        public string GhiChu { get; set; }
        [JsonPropertyName("quanHuyen")]
        public string QuanHuyen { get; set; }
        [JsonPropertyName("tinhThanhPho")]
        public string TinhThanhPho { get; set; }
        [JsonPropertyName("tenDonViCapTren")]
        public string TenDonViCapTren { get; set; }
        [JsonPropertyName("chucDanhLapBieu")]
        public string ChucDanhLapBieu { get; set; }
        [JsonPropertyName("hoTenNguoiLapBieu")]
        public string HoTenNguoiLapBieu { get; set; }
        [JsonPropertyName("chucDanhKeToan")]

        public string ChucDanhKeToan { get; set; }
        [JsonPropertyName("hoTenKeToan")]
        public string HoTenKeToan { get; set; }
        [JsonPropertyName("chucDanhThuKho")]
        public string ChucDanhThuKho { get; set; }
        [JsonPropertyName("hoTenThuKho")]
        public string HoTenThuKho { get; set; }
        [JsonPropertyName("chucDanhThuTruong")]
        public string ChucDanhThuTruong { get; set; }
        [JsonPropertyName("hoTenThuTruong")]
        public string HoTenThuTruong { get; set; }
        [JsonPropertyName("nganhNghe")]
        public string NganhNghe { get; set; }
        [JsonPropertyName("ngayThangLB")]
        public string NgayThangLB { get; set; }
        [JsonPropertyName("noiDungNghe")]
        public string NoiDungNghe { get; set; }
        
    }
}
