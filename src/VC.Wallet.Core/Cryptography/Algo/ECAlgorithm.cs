#nullable disable

using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace VC.Wallet.Core
{
    internal class ECAlgorithm : ICryptoAlgorithm<ECJwk>
    {
        public Keys GenerateKeys()
        {
            ECDsa ecdsa = ECDsa.Create(ECCurve.NamedCurves.nistP256);

            Keys keys = new Keys
            {
                publicKey = ecdsa.ExportSubjectPublicKeyInfoPem(),
                privateKey = ecdsa.ExportECPrivateKeyPem(),
            };

            return keys;
        }

        public ECJwk GetPublicJwk(string publicKeyPem, string typ = "")
        {
            ECDsa ecdsa = ECDsa.Create(ECCurve.NamedCurves.nistP256);
            ecdsa.ImportFromPem(publicKeyPem);
            ECParameters parameters = ecdsa.ExportParameters(false);

            string x = Base64UrlEncoder.Encode(parameters.Q.X);
            string y = Base64UrlEncoder.Encode(parameters.Q.Y);

            ECKey ecKey = new ECKey
            {
                crv = "P-256",
                x = x,
                y = y,
            };

            ECJwk ecJwk = new ECJwk
            {
                kty = "EC",
                alg = "ES256",
                crv = "P-256",
                x = x,
                y = y,
                jwk = ecKey
            };

            if (typ != "")
            {
                ecJwk.typ = typ;
            }

            return ecJwk;
        }

        // TODO

        public string GetPublicKey(ECJwk publicJwk)
        {
            throw new NotImplementedException();
        }

        // TODO

        public ECJwk SetPublicJwkKeyId(string keyId, string type = "")
        {
            throw new NotImplementedException();
        }

        // TODO

        public byte[] SignData(byte[] data, string privateKey)
        {
            throw new NotImplementedException();
        }

        // TODO
        public Task<ECJwk> VerifyPublicJwkAsync(ECJwk publicJwk)
        {
            throw new NotImplementedException();
        }

        // TODO

        public bool VerifySignature(byte[] data, byte[] signature, string publicKey)
        {
            throw new NotImplementedException();
        }
    }
}
