using System.Text.Json.Serialization;

namespace QLSX.Shared.Data.Responses
{
    public class AucErrorResponse
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("error")]
        public string Error { get; set; }
    }
}
