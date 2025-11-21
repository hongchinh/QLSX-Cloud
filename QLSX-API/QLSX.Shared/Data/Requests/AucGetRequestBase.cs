using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace QLSX.Shared.Data.Requests
{
    public class AucGetRequestBase
    {
        [JsonPropertyName("queryParams")]
        public Dictionary<string, string> QueryParams { get; set; }
    }
}
