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
    public class BangKeDenHanThanhToanResponse : BaoCaoBase, IApiWrapperResponse
    {
        [JsonPropertyName("hanThanhToan")]
        public string HanThanhToan { get; set; }

        [JsonPropertyName("ngayThang")]
        public string NgayThang { get; set; }

        [JsonPropertyName("soHieu")]
        public int SoHieu { get; set; }

        [JsonPropertyName("tenKhachHang")]
        public int TenKhachHang { get; set; }

        [JsonPropertyName("diaChi")]
        public int DiaChi { get; set; }

        [JsonPropertyName("thanhTien")]
        public int ThanhTien { get; set; }

        [JsonPropertyName("ngayCuoiCung")]
        public int NgayCuoiCung { get; set; }

        [JsonPropertyName("ngayBatDau")]
        public int NgayBatDau { get; set; }
        
    }
}
