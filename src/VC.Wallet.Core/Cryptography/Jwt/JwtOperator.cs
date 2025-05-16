#nullable disable

using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;

namespace VC.Wallet.Core
{
    internal class JwtOperator : IJwtOperator
    {
        private const string _typ = "JWT";

        public Jwt Sign<T>(object payload, Keys keys, ICryptoAlgorithm<T> cryptoAlgorithm, string keyId = "")
                   where T : class, new()
        {
            JwtHeader<T> jwtHeader = null;

            if (keyId == "")
            {
                jwtHeader = cryptoAlgorithm.GetJwtHeader(keys.publicKey, _typ);
            }
            else
            {
                jwtHeader = cryptoAlgorithm.SetKeyIdJwtHeader(keyId, _typ);
            }

            string jsonJwsHeader = JsonSerializer.Serialize(jwtHeader);

            string jsonJwsHeaderEncoded = Base64UrlEncoder.Encode(Encoding.UTF8.GetBytes(jsonJwsHeader));

            string jsonJwsPayloadEncoded = Base64UrlEncoder.Encode(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(payload)));

            byte[] data = Encoding.UTF8.GetBytes($"{jsonJwsHeaderEncoded}.{jsonJwsPayloadEncoded}");

            byte[] signatureBytes = cryptoAlgorithm.SignData(data, keys.privateKey);

            string signatureEncoded = Base64UrlEncoder.Encode(signatureBytes);

            var body = new Jwt
            {
                encodedHeader = jsonJwsHeaderEncoded,
                encodedPayload = jsonJwsPayloadEncoded,
                encodedSignature = signatureEncoded
            };

            return body;
        }

        public bool Verify<T>(string jwtCompact, string publicKey, ICryptoAlgorithm<T> cryptoAlgorithm)
            where T : class, new()
        {
            Jwt jwt = JwtFromJwtCompact(jwtCompact);

            byte[] data = Encoding.UTF8.GetBytes($"{jwt.encodedHeader}.{jwt.encodedPayload}");
            byte[] signature = Base64UrlEncoder.DecodeBytes(jwt.encodedSignature);

            return cryptoAlgorithm.VerifySignature(data, signature, publicKey);
        }

        public string ToJwtCompact(Jwt jwt)
        {
            return $"{jwt.encodedHeader}.{jwt.encodedPayload}.{jwt.encodedSignature}";
        }

        public Jwt JwtFromJwtCompact(string jwtCompact)
        {
            string[] parts = jwtCompact.Split('.');
            string headerEncoded = parts[0];

            string payloadEncoded = string.Empty;
            string signatureEncoded = string.Empty;

            if (parts.Length == 2)
            {
                signatureEncoded = parts[1];
            }
            else
            {
                payloadEncoded = parts[1];
                signatureEncoded = parts[2];
            }

            return new Jwt
            {
                encodedHeader = headerEncoded,
                encodedPayload = payloadEncoded,
                encodedSignature = signatureEncoded
            };
        }

        public JwtDecoded DecodeJwt(Jwt jwt)
        {
            JwtDecoded decoded = new JwtDecoded();

            decoded.decodedHeader = Encoding.UTF8.GetString(Base64UrlEncoder.DecodeBytes(jwt.encodedHeader)); 
            decoded.decodedPayload = Encoding.UTF8.GetString(Base64UrlEncoder.DecodeBytes(jwt.encodedPayload));
            decoded.decodedSignature = jwt.encodedSignature;
            
            return decoded;
        }

        public T GetPublicJwk<T>(string jwtCompact)
             where T : class, new()
        {
            string jwtDecodedHeader = DecodeJwtCompactHeader(jwtCompact);
            return JsonSerializer.Deserialize<T>(jwtDecodedHeader);
        }

        private string DecodeJwtCompactHeader(string jwtCompact)
        {
            Jwt jwt = JwtFromJwtCompact(jwtCompact);
            byte[] jwtHeaderDecodedBytes = Base64UrlEncoder.DecodeBytes(jwt.encodedHeader);
            string jwtDecodedHeader = Encoding.UTF8.GetString(jwtHeaderDecodedBytes);
            return jwtDecodedHeader;
        }
    }
}
