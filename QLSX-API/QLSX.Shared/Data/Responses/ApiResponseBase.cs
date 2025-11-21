using QLSX.Shared.Interfaces;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace QLSX.Shared.Data.Responses
{
    public class ApiResponseBase<T> : ApiResponseBase, IApiWrapperResponse
    {
        [JsonPropertyName("listData")]
        public List<T> ListData { get; set; }

        [JsonPropertyName("data")]
        public T Data { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }

    public class ApiResponseBase : IApiWrapperResponse
    {
        [JsonPropertyName("statusCode")]
        public HttpStatusCode StatusCode { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
