#nullable disable

using System.Text.Json.Serialization;

namespace VC.Wallet.Core
{
    public class JwkBase
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("alg")]
        [JsonPropertyOrder(1)]
        public string alg { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("typ")]
        [JsonPropertyOrder(2)]
        public string typ { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("kty")]
        [JsonPropertyOrder(3)]
        public string kty { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("kid")]
        [JsonPropertyOrder(4)]
        public string kid { get; set; }
    }
}
