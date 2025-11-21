using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace QLSX.Shared.Data.Requests
{
    public class PaginatedRequestBase
    {
        [JsonPropertyName("KeywordSearch")]
        public string KeywordSearch { get; set; }

        [JsonPropertyName("PageIndex")]
        public int PageIndex { get; set; } = 1;

        [JsonPropertyName("PageCount")]
        public int PageCount { get; set; } = 20;


        /// <summary>
        /// Sort by field name
        /// </summary>
        [JsonPropertyName("SortBy")]
        public virtual string SortBy { get; set; }

        /// <summary>
        /// Sort order
        /// </summary>
        [JsonPropertyName("SortType")]
        public virtual bool SortType { get; set; }


    }


}
