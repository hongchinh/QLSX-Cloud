using Newtonsoft.Json;

namespace QLSX.Web.Commons.Select2
{
    public class PaginationOptions {
        [JsonProperty("more")]
        public bool More { get; set; } = false;
    }
}
