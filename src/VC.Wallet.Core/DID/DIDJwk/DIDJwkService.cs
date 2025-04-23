#nullable disable

using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace VC.Wallet.Core
{
    internal class DIDJwkService : IDIDJwkService
    {
        public string CreateDID<T>(T pubicJwk)
        {
            var serializerOptions = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            string jwkstr = JsonSerializer.Serialize(pubicJwk, serializerOptions);
            byte[] bytes = Encoding.UTF8.GetBytes(jwkstr);
            jwkstr = Encoding.UTF8.GetString(bytes);
            jwkstr = Base64UrlEncoder.Encode(jwkstr);
            return $"did:jwk:{jwkstr}";
        }

        public T GetPublicJwk<T>(string DID)
        {
            var serializerOptions = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            string jwkEncoded = DID.Replace("did:jwk:", string.Empty);
            string jwkStr = Base64UrlEncoder.Decode(jwkEncoded);
            T publicJwk = JsonSerializer.Deserialize<T>(jwkStr, serializerOptions);
            return publicJwk;
        }
    }
}
