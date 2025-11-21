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
    public class BangCanDoiThuChiResponse : BaoCaoBase, IApiWrapperResponse
    {
        [JsonPropertyName("maSo")]
        public string MaSo { get; set; }

        [JsonPropertyName("noiDung")]
        public string NoiDung { get; set; }

        [JsonPropertyName("stt")]
        public int Stt { get; set; }
        [JsonPropertyName("soTien")]
        public double SoTien { get; set; }

    }
}
