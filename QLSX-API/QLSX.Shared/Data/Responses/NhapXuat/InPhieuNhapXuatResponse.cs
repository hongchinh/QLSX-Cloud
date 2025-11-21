using QLSX.Shared.Interfaces;
using QLSX.Shared.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace QLSX.Shared.Data.Responses.NhapXuat
{
    public class InPhieuNhapXuatResponse : IApiWrapperResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("loai")]
        public string Loai { get; set; }

        [JsonPropertyName("ngayCT")]
        public DateTime NgayCT { get; set; }

        [JsonPropertyName("dMKhoHangId")]
        public int DMKhoHangId { get; set; }

        [JsonPropertyName("maDonViId")]
        public int MaDonViId { get; set; }

        [JsonPropertyName("soCT")]
        public String SoCT { get; set; }

        [JsonPropertyName("maDonVi")]
        public String MaDonVi { get; set; }

        [JsonPropertyName("tenDonVi")]
        public String TenDonVi { get; set; }

        [JsonPropertyName("diaChi")]
        public String DiaChi { get; set; }
        [JsonPropertyName("dienThoai")]
        public String DienThoai { get; set; }


        [JsonPropertyName("maNhanVienId")]
        public int MaNhanVienId { get; set; }

        [JsonPropertyName("maLyDoId")]
        public int MaLyDoId { get; set; }

        [JsonPropertyName("userId")]
        public int UserId { get; set; }

        [JsonPropertyName("ngayHenThanhToan")]
        public DateTime NgayHenThanhToan { get; set; }

        [JsonPropertyName("ngayGiao")]
        public DateTime NgayGiao { get; set; }

        [JsonPropertyName("noiGiao")]
        public string NoiGiao { get; set; }

        [JsonPropertyName("phuongTien")]
        public string PhuongTien { get; set; }

        [JsonPropertyName("tyleVAT")]
        public double TyleVAT { get; set; }

        [JsonPropertyName("dienGiai")]
        public string DienGiai { get; set; }

        [JsonPropertyName("soTienTT")]
        public double SoTienTT { get; set; }


        [JsonPropertyName("maHangHoa")]
        public string MaHangHoa { get; set; }

        [JsonPropertyName("tenHangHoa")]
        public string TenHangHoa { get; set; }

        [JsonPropertyName("donViTinh")]
        public string DonViTinh { get; set; }

        [JsonPropertyName("soluong")]
        public double Soluong { get; set; }
        [JsonPropertyName("donGia")]
        public double DonGia { get; set; }

        [JsonPropertyName("soTien")]
        public double SoTien { get; set; }

        [JsonPropertyName("khoRongTon")]
        public double KhoRongTon { get; set; }

        [JsonPropertyName("chieuDai")]
        public double ChieuDai { get; set; }

        [JsonPropertyName("tongChieuDai")]
        public double TongChieuDai { get; set; }

        [JsonPropertyName("tongDienTich")]
        public double TongDienTich { get; set; }

        [JsonPropertyName("soTienXuat")]
        public double SoTienXuat { get; set; }
        [JsonPropertyName("noCu")]
        public double NoCu { get; set; }
        [JsonPropertyName("soConLai")]
        public double SoConLai { get; set; }
        [JsonPropertyName("thoiGian")]
        public string ThoiGian { get; set; }
        [JsonPropertyName("bangChu")]
        public string BangChu { get; set; }
    }
}
