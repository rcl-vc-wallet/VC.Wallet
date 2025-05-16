#nullable disable

using System.Text.Json.Serialization;

namespace VC.Wallet.Core
{
    public class RSAJwk : JwkBase
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("n")]
        [JsonPropertyOrder(2)]
        public string n { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("e")]
        [JsonPropertyOrder(3)]
        public string e { get; set; }
    }
}
