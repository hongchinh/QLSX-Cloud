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
    public class SoPhaiThuTongHopResponse : BaoCaoBase, IApiWrapperResponse
    {
        [JsonPropertyName("maDonVi")]
        public string MaDonVi { get; set; }

        [JsonPropertyName("tenDonVi")]
        public string TenDonVi { get; set; }

        [JsonPropertyName("diaChi")]
        public string DiaChi { get; set; }

        [JsonPropertyName("tenNhom")]
        public string TenNhom { get; set; }

        [JsonPropertyName("soDuDau")]
        public double SoDuDau { get; set; }
        [JsonPropertyName("soTienMua")]
        public double SoTienMua { get; set; }
        [JsonPropertyName("soTienTra")]
        public double SoTienTra { get; set; }
        [JsonPropertyName("soConLai")]
        public double SoConLai { get; set; }
    }
}
