using QLSX.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace QLSX.Shared.Data.Responses.DMHangHoa
{
    public class DMHangHoaResponse : IApiWrapperResponse
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }
        [JsonPropertyName("MaHangHoa")]
        public string MaHangHoa { get; set; }
        [JsonPropertyName("TenHangHoa")]
        public string TenHangHoa { get; set; }
        [JsonPropertyName("DonViTinh")]
        public string DonViTinh { get; set; }
        [JsonPropertyName("GiaNhap")]
        public double GiaNhap { get; set; }
        [JsonPropertyName("GiaXuat")]
        public double GiaXuat { get; set; }
        [JsonPropertyName("DonGia")]
        public double DonGia { get; set; }
        [JsonPropertyName("TyTrong")]
        public double TyTrong { get; set; }
        [JsonPropertyName("KhoRongTon")]
        public double KhoRongTon { get; set; }
        [JsonPropertyName("ChieuDai")]
        public double ChieuDai { get; set; }
        [JsonPropertyName("DMNhomHangId")]
        public int? DMNhomHangId { get; set; }
        [JsonPropertyName("UserId")]
        public int UserId { get; set; }
        [JsonPropertyName("DMMauSacId")]
        public int? DMMauSacId { get; set; }
        [JsonPropertyName("DMDoDayId")]
        public int? DMDoDayId { get; set; }
        [JsonPropertyName("DMLoaiTonId")]
        public int? DMLoaiTonId { get; set; }
        [JsonPropertyName("DMChungLoaiId")]
        public int? DMChungLoaiId { get; set; }
        
        [JsonPropertyName("DMKieuSongId")]
        public int? DMKieuSongId { get; set; }
        
    }
}
