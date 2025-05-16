#nullable disable

using System.Text.Json.Serialization;

namespace VC.Wallet.Core
{
    public class Jwt
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("protected")]
        [JsonPropertyOrder(1)]
        public string encodedHeader { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyOrder(2)]
        public string encodedPayload { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyOrder(3)]
        public string encodedSignature { get; set; }
    }

    public class JwtDecoded
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyOrder(1)]
        public string decodedHeader { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyOrder(1)]
        public string decodedPayload { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyOrder(1)]
        public string decodedSignature { get; set; }
    }

    public class JwtHeader<T> where T : class
    {
        public string alg { get; set; }
        public string typ { get; set; }
        public T jwk { get; set; }
    }
}
