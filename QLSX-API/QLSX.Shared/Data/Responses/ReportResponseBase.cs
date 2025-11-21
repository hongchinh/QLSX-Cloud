using QLSX.Shared.Interfaces;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace QLSX.Shared.Data.Responses
{
    public class ReportResponseBase<T> : ReportResponseBase, IApiWrapperResponse
    {
        [JsonPropertyName("listData")]
        public List<T> ListData { get; set; }

        [JsonPropertyName("data")]
        public T Data { get; set; }

        [JsonPropertyName("thongTin")]
        public CoQuanResponse ThongTin { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

        public string Host { get; set; }
    }

    public class ReportResponseBase : IApiWrapperResponse
    {
        [JsonPropertyName("statusCode")]
        public int StatusCode { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
