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
    public class BangKeHanMucCongNoPhaiTraResponse : BaoCaoBase, IApiWrapperResponse
    {
        [JsonPropertyName("maDoiTuong")]
        public string MaDoiTuong { get; set; }

        [JsonPropertyName("tenDoiTuong")]
        public string TenDoiTuong { get; set; }

        [JsonPropertyName("hanMuc")]
        public int HanMuc { get; set; }

        [JsonPropertyName("duCongNo")]
        public int DuCongNo { get; set; }

        [JsonPropertyName("chenhLech")]
        public int ChenhLech { get; set; }

        [JsonPropertyName("ngayBatDau")]
        public int NgayBatDau { get; set; }

        [JsonPropertyName("ngayCuoiCung")]
        public int NgayCuoiCung { get; set; }
        
    }
}
