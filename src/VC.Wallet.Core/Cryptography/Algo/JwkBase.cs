#nullable disable

using System.Text.Json.Serialization;

namespace VC.Wallet.Core
{
    public class JwkBase
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("kty")]
        [JsonPropertyOrder(1)]
        public string kty { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("kid")]
        [JsonPropertyOrder(10)]
        public string kid { get; set; }
    }
}
