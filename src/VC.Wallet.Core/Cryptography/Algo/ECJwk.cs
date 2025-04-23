#nullable disable

using System.Text.Json.Serialization;

namespace VC.Wallet.Core
{
    public class ECJwk : JwkBase
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("crv")]
        [JsonPropertyOrder(5)]
        public string crv { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("x")]
        [JsonPropertyOrder(6)]
        public string x { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("y")]
        [JsonPropertyOrder(7)]
        public string y { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("jwk")]
        [JsonPropertyOrder(8)]
        public ECKey jwk { get; set; }

    }

    public class ECKey
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("crv")]
        [JsonPropertyOrder(5)]
        public string crv { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("x")]
        [JsonPropertyOrder(6)]
        public string x { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("y")]
        [JsonPropertyOrder(7)]
        public string y { get; set; }
    }
}
