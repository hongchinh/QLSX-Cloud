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
    public class BaoCaoDoanhThuResponse : BaoCaoBase, IApiWrapperResponse
    {

        [JsonPropertyName("tenKhachHang")]
        public string TenKhachHang { get; set; }

        [JsonPropertyName("maHangHoa")]
        public string MaHangHoa { get; set; }

        [JsonPropertyName("soHoaDon")]
        public string SoHoaDon { get; set; }

         [JsonPropertyName("phiVanChuyen")]
        public string PhiVanChuyen { get; set; }

        [JsonPropertyName("tenHangHoa")]
        public string TenHangHoa { get; set; }

        [JsonPropertyName("donViTinh")]
        public string DonViTinh { get; set; }

        [JsonPropertyName("soLuong")]
        public int SoLuong { get; set; }

        [JsonPropertyName("donGia")]
        public int DonGia { get; set; }

        [JsonPropertyName("soTien")]
        public int SoTien { get; set; }

        [JsonPropertyName("ghiChu")]
        public string GhiChu { get; set; }

        [JsonPropertyName("tongSoLuong")]
        public string TongSoLuong { get; set; }

        [JsonPropertyName("tongTien")]
        public string TongTien { get; set; }
        
    }
}
