using QLSX.Shared.Interfaces;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace QLSX.Shared.Data.Responses
{
    public class PaginatedResponseBase<T> : IApiWrapperResponse
    {
        [JsonPropertyName("TotalCount")]
        public long TotalCount { get; set; }

        [JsonPropertyName("DataList")]
        public IEnumerable<T> DataList { get; set; } = new List<T>();
    }
}
