using QLSX.Shared.Data.Requests.BaoCao;
using QLSX.Shared.Interfaces;
using QLSX.Shared.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace QLSX.Shared.Data.Responses.BaoCao
{
    public class ChiTietKhoanChiResponse : BaoCaoBase, IApiWrapperResponse
    {
        [JsonPropertyName("soCT")]
        public string SoCT { get; set; }

        [JsonPropertyName("ngayCT")]
        public DateTime NgayCT { get; set; }

        [JsonPropertyName("soTien")]
        public double SoTien { get; set; }

        [JsonPropertyName("tenKhoanChi")]
        public string TenKhoanChi { get; set; }

        [JsonPropertyName("tenDonVi")]
        public string TenDonVi { get; set; }

        [JsonPropertyName("diaChi")]
        public string DiaChi { get; set; }

        [JsonPropertyName("tenLoaiTien")]
        public string TenLoaiTien { get; set; }

        [JsonPropertyName("dienGiai")]
        public string DienGiai { get; set; }

       
        [JsonPropertyName("dMKhoanChiId")]
        public int DMKhoanChiId { get; set; }
        [JsonPropertyName("dMLoaiTienId")]
        public int DMLoaiTienId { get; set; }
       
    }
}
