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
    public class BangDoiChieuCongNoResponse : BaoCaoBase, IApiWrapperResponse
    {
        [JsonPropertyName("ten")]
        public string Ten { get; set; }

        [JsonPropertyName("soDienThoai")]
        public string SoDienThoai { get; set; }

        [JsonPropertyName("diaChi")]
        public string DiaChi { get; set; }

        [JsonPropertyName("ngayBatDau")]
        public string NgayBatDau { get; set; }

        [JsonPropertyName("ngayCuoiCung")]
        public string NgayCuoiCung { get; set; }

        [JsonPropertyName("soChungTu")]
        public string SoChungTu { get; set; }

        [JsonPropertyName("ngayChungTu")]
        public string NgayChungTu { get; set; }

        [JsonPropertyName("dienGiai")]
        public string DienGiai { get; set; }

        [JsonPropertyName("soTienNo")]
        public int SoTienNo { get; set; }
        [JsonPropertyName("soTienCo")]
        public int SoTienCo { get; set; }

        [JsonPropertyName("tongNo")]
        public int TongNo { get; set; }
        
    }
}
