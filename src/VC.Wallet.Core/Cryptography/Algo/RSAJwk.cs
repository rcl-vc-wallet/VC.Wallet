#nullable disable

using System.Text.Json.Serialization;

namespace VC.Wallet.Core
{
    public class RSAJwk : JwkBase
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("n")]
        [JsonPropertyOrder(5)]
        public string n { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("e")]
        [JsonPropertyOrder(6)]
        public string e { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("jwk")]
        [JsonPropertyOrder(5)]
        public RSAKey jwk { get; set; }
    }

    public class RSAKey
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("n")]
        [JsonPropertyOrder(1)]
        public string n { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("e")]
        [JsonPropertyOrder(2)]
        public string e { get; set; }
    }
}
