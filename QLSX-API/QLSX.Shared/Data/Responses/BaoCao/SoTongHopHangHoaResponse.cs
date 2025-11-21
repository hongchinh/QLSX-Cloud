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
    public class SoTongHopHangHoaResponse : BaoCaoBase, IApiWrapperResponse
    {
        [JsonPropertyName("maHangHoa")]
        public string MaHangHoa { get; set; }

        [JsonPropertyName("tenHangHoa")]
        public string TenHangHoa { get; set; }

        [JsonPropertyName("donViTinh")]
        public string DonViTinh { get; set; }
        [JsonPropertyName("soLuongDau")]
        public double SoLuongDau { get; set; }
        [JsonPropertyName("soTienDau")]
        public double SoTienDau { get; set; }
        [JsonPropertyName("soLuongNhap")]
        public double SoLuongNhap { get; set; }
        [JsonPropertyName("soTienNhap")]
        public double SoTienNhap { get; set; }
        [JsonPropertyName("soTienXuat")]
        public double SoTienXuat { get; set; }
        [JsonPropertyName("soLuongXuat")]
        public double SoLuongXuat { get; set; }
        [JsonPropertyName("soTienTon")]
        public double SoTienTon { get; set; }
        [JsonPropertyName("soLuongTon")]
        public double SoLuongTon { get; set; }
        [JsonPropertyName("maNhom")]
        public string MaNhom { get; set; }

        [JsonPropertyName("tenNhom")]
        public string TenNhom { get; set; }
        [JsonPropertyName("tenKho")]
        public string TenKho { get; set; }
        [JsonPropertyName("maKho")]
        public string MaKho { get; set; }

    }
}
