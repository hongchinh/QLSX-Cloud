using QLSX.Shared.Interfaces;
using QLSX.Shared.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace QLSX.Shared.Data.Responses.ThuChi
{
    public class InPhieuThuChiResponse : IApiWrapperResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("soPhieu")]
        public string SoPhieu { get; set; }

        [JsonPropertyName("ngayCT")]
        public DateTime NgayCT { get; set; }

        [JsonPropertyName("SoTien")]
        public double SoTien { get; set; }

        [JsonPropertyName("loai")]
        public string Loai { get; set; }

        [JsonPropertyName("maDoiTuong")]
        public String MaDoiTuong { get; set; }

        [JsonPropertyName("tenDoiTuong")]
        public String TenDoiTuong { get; set; }

        [JsonPropertyName("diaChi")]
        public String DiaChi { get; set; }

        [JsonPropertyName("BangChu")]
        public String BangChu { get; set; }

        [JsonPropertyName("thoiGian")]
        public String ThoiGian { get; set; }


        [JsonPropertyName("maKhoanThu")]
        public string MaKhoanThu { get; set; }

        [JsonPropertyName("tenKhoanThu")]
        public string TenKhoanThu { get; set; }

        [JsonPropertyName("maKhoanChi")]
        public string MaKhoanChi { get; set; }

        [JsonPropertyName("tenKhoanChi")]
        public string TenKhoanChi { get; set; }

        [JsonPropertyName("dienGiai")]
        public string DienGiai { get; set; }

        [JsonPropertyName("loaiTien")]
        public string LoaiTien { get; set; }

      
    }
}
