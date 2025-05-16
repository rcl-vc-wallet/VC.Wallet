#nullable disable

using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace VC.Wallet.Core
{
    internal class RSAAlgorithm : ICryptoAlgorithm<RSAJwk>
    {
        private static HttpClient _httpClient = new HttpClient();

        public RSAJwk GetPublicJwk(string publicKey)
        {
            RSA rsa = RSA.Create();
            rsa.ImportFromPem(publicKey);

            RSAParameters rsaParameters = rsa.ExportParameters(false);

            string n = Base64UrlEncoder.Encode(rsaParameters.Modulus);
            string e = Base64UrlEncoder.Encode(rsaParameters.Exponent);

            RSAJwk rsaJwk = new RSAJwk
            {
                kty = "RSA",
                e = e,
                n = n
            };

            return rsaJwk;
        }

        public string GetPublicKey(RSAJwk RsaJwk)
        {
            RSAParameters rsaParameters = new RSAParameters
            {
                Modulus = Base64UrlEncoder.DecodeBytes(RsaJwk.n),
                Exponent = Base64UrlEncoder.DecodeBytes(RsaJwk.e)
            };

            RSA rsa = RSA.Create(rsaParameters);

            string rsaPublicKey = rsa.ExportRSAPublicKeyPem();
            return rsaPublicKey;
        }

        public byte[] SignData(byte[] data, string privateKey)
        {
            RSA rsa = RSA.Create();
            rsa.ImportFromPem(privateKey);
            byte[] signatureBytes = rsa.SignData(data, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            return signatureBytes;
        }

        public bool VerifySignature(byte[] data, byte[] signature, string publicKey)
        {
            RSA rsa = RSA.Create();
            rsa.ImportFromPem(publicKey);
            return rsa.VerifyData(data, signature, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        }

        public async Task<RSAJwk> GetPublicJwkFromKidAsync(string kid)
        {
            try
            {
                var response = await _httpClient.GetAsync(kid);  
                
                if(response.IsSuccessStatusCode)
                {
                    string publicJwkString = await response.Content.ReadAsStringAsync();
                    RSAJwk rsaJwk = GetPublicJwk(publicJwkString);
                    return rsaJwk;
                }
                else
                {
                    throw new Exception("Could not get jwk from kid");
                }
            }
            catch(Exception ex) 
            {
                throw new Exception($"Could not get jwk from kid, {ex.Message}");
            }
        }

        public JwtHeader<RSAJwk> GetJwtHeader(string publicKey, string type)
        {
            RSA rsa = RSA.Create();
            rsa.ImportFromPem(publicKey);

            RSAParameters rsaParameters = rsa.ExportParameters(false);

            string n = Base64UrlEncoder.Encode(rsaParameters.Modulus);
            string e = Base64UrlEncoder.Encode(rsaParameters.Exponent);

            RSAJwk rsaJwk = new RSAJwk
            {
                kty = "RSA",
                e = e,
                n = n
            };

            return new JwtHeader<RSAJwk>
            {
                alg = "RS256",
                typ = type,
                jwk = rsaJwk
            };
        }

        public JwtHeader<RSAJwk> SetKeyIdJwtHeader(string keyId, string type)
        {
            RSAJwk rsaJwk = new RSAJwk
            {
                kty = "RSA",
                kid = keyId
            };

            return new JwtHeader<RSAJwk>
            {
                alg = "RS256",
                typ = type,
                jwk = rsaJwk
            };
        }
    }
}
