using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace QLSX.Shared.Data.Requests.BaoCao
{
    public class BaoCaoBase
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("thoiGian")]
        public string ThoiGian { get; set; }

        [JsonPropertyName("tenDonViSuDung")]
        public string TenDonViSuDung { get; set; }
        [JsonPropertyName("dMDonViSuDungId")]
        public int DMDonViSuDungId { get; set; }
    }
}
