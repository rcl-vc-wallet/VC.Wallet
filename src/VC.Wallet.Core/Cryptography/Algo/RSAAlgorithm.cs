#nullable disable

using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace VC.Wallet.Core
{
    internal class RSAAlgorithm : ICryptoAlgorithm<RSAJwk>
    {
        private static HttpClient _httpClient = new HttpClient();

        public Keys GenerateKeys()
        {
            RSA rsa = RSA.Create();

            Keys keys = new Keys
            {
                publicKey = rsa.ExportRSAPublicKeyPem(),
                privateKey = rsa.ExportRSAPrivateKeyPem()
            };

            return keys;
        }

        public RSAJwk GetPublicJwk(string publicKey, string typ ="")
        {
            RSA rsa = RSA.Create();
            rsa.ImportFromPem(publicKey);

            RSAParameters rsaParameters = rsa.ExportParameters(false);

            string n = Base64UrlEncoder.Encode(rsaParameters.Modulus);
            string e = Base64UrlEncoder.Encode(rsaParameters.Exponent);

            RSAKey rsaKey = new RSAKey
            {
                e = e,
                n = n,
            };

            RSAJwk rsaJwk = new RSAJwk
            {
                kty = "RSA",
                alg = "RS256",
                e = e,
                n = n,
                jwk = rsaKey
            };

            if (typ != "")
            {
                rsaJwk.typ = typ;
            }

            return rsaJwk;
        }

        public RSAJwk SetPublicJwkKeyId(string keyId, string typ = "")
        {
            RSAJwk rsaJwk = new RSAJwk
            {
                kty = "RSA",
                alg = "RS256",
                kid = keyId
            };

            if(typ != "")
            {
                rsaJwk.typ = typ;
            }

            return rsaJwk;
        }

        public string GetPublicKey(RSAJwk verifiedPublicJwk)
        {
            RSAParameters rsaParameters = new RSAParameters
            {
                Modulus = Base64UrlEncoder.DecodeBytes(verifiedPublicJwk.jwk?.n),
                Exponent = Base64UrlEncoder.DecodeBytes(verifiedPublicJwk.jwk?.e)
            };

            RSA rsa = RSA.Create(rsaParameters);

            string rsaPublicKey = rsa.ExportRSAPublicKeyPem();
            return rsaPublicKey;
        }

        public async Task<RSAJwk> VerifyPublicJwkAsync(RSAJwk publicJwk)
        {
            RSAJwk rsaJwk = GetPublicJwkFromParameters(publicJwk);

            if(string.IsNullOrEmpty(rsaJwk?.e) || string.IsNullOrEmpty(rsaJwk?.n))
            {
              rsaJwk =  await GetPublicJwkFromKidAsync(rsaJwk.kid);
            }

            return rsaJwk;
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

        private RSAJwk GetPublicJwkFromParameters(RSAJwk rsaJwk)
        {
            if (!string.IsNullOrEmpty(rsaJwk?.e) && !string.IsNullOrEmpty(rsaJwk?.n))
            {
                return rsaJwk;
            }
            else if (!string.IsNullOrEmpty(rsaJwk?.jwk?.e) && !string.IsNullOrEmpty(rsaJwk?.jwk?.n))
            {
                rsaJwk.e = rsaJwk.jwk.e;
                rsaJwk.n = rsaJwk.jwk.n;

                return rsaJwk;
            }
            else if (!string.IsNullOrEmpty(rsaJwk?.kid))
            {
                return rsaJwk;
            }
            else
            {
                throw new Exception("Could not obtain jwk or kid");
            }
        }

        private async Task<RSAJwk> GetPublicJwkFromKidAsync(string kid)
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
    }
}
