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
    public class SoQuyTienMatResponse : BaoCaoBase, IApiWrapperResponse
    {
        [JsonPropertyName("soPhieu")]
        public string SoPhieu { get; set; }

        [JsonPropertyName("ngayCT")]
        public DateTime NgayCT { get; set; }

        [JsonPropertyName("dienGiai")]
        public string DienGiai { get; set; }

        [JsonPropertyName("soTienThu")]
        public double SoTienThu { get; set; }

        [JsonPropertyName("soTienChi")]
        public double SoTienChi { get; set; }
        [JsonPropertyName("soTienTon")]
        public double SoTienTon { get; set; }
        [JsonPropertyName("soDauKy")]
        public double SoDauKy { get; set; }

        [JsonPropertyName("soDuCuoi")]
        public double SoDuCuoi { get; set; }

        [JsonPropertyName("dMLoaiTienId")]
        public int DMLoaiTienId { get; set; }

        [JsonPropertyName("tenLoaiTien")]
        public string TenLoaiTien { get; set; }

    }
}
