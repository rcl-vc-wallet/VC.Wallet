#nullable disable

using Microsoft.IdentityModel.Tokens;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace VC.Wallet.Core
{
    internal class DIDJwkService : IDIDJwkService
    {
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