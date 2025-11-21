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
    public class SoPhaiTraResponse : BaoCaoBase, IApiWrapperResponse
    {
        [JsonPropertyName("soCTNX")]
        public string SoCTNX { get; set; }
        [JsonPropertyName("soCTTC")]
        public string SoCTTC { get; set; }

        [JsonPropertyName("ngayCT")]
        public DateTime NgayCT { get; set; }


        [JsonPropertyName("soDuDau")]
        public double SoDuDau { get; set; }

        [JsonPropertyName("soDuCuoi")]
        public double SoDuCuoi { get; set; }

        [JsonPropertyName("donGia")]
        public double DonGia { get; set; }

        [JsonPropertyName("soLuong")]
        public double SoLuong { get; set; }

        [JsonPropertyName("soTien")]
        public double SoTien { get; set; }
        [JsonPropertyName("soTienMua")]
        public double SoTienMua { get; set; }

        [JsonPropertyName("soTienTra")]
        public double SoTienTra { get; set; }
        [JsonPropertyName("soConLai")]
        public double SoConLai { get; set; }


        [JsonPropertyName("maDonVi")]
        public string MaDonVi { get; set; }
        [JsonPropertyName("tenDonVi")]
        public string TenDonVi { get; set; }

        [JsonPropertyName("diaChi")]
        public string DiaChi { get; set; }
        [JsonPropertyName("dienThoai")]
        public string DienThoai { get; set; }


        [JsonPropertyName("dienGiai")]
        public string DienGiai { get; set; }

        [JsonPropertyName("maHangHoa")]
        public string MaHangHoa { get; set; }
        [JsonPropertyName("tenHangHoa")]
        public string TenHangHoa { get; set; }
        [JsonPropertyName("donViTinh")]
        public string DonViTinh { get; set; }

        [JsonPropertyName("tenNhom")]
        public string TenNhom { get; set; }

        [JsonPropertyName("lphieu")]
        public string Lphieu { get; set; }

        [JsonPropertyName("loai")]
        public int Loai { get; set; }

    }
}
