#nullable disable

using System.Text.Json;

namespace VC.Wallet.Core.Test
{
    [TestClass]
    public class DIDJWKTest
    {
        private readonly IDIDJwkService _didJwk;
        private readonly ICryptoAlgorithmFactory _algorithmFactory;
        private readonly ICryptoAlgorithm<RSAJwk> _rsaOperator;

        public DIDJWKTest()
        {
            _didJwk = (IDIDJwkService)DependencyResolver.ServiceProvider().GetService(typeof(IDIDJwkService));
            _algorithmFactory = (ICryptoAlgorithmFactory)DependencyResolver.ServiceProvider().GetService(typeof(ICryptoAlgorithmFactory));
            _rsaOperator = _algorithmFactory.Create<RSAJwk>();
        }

        [TestMethod]
        public void CreateDIDJWKTest()
        {
            try
            {
                string didJwk = CreateDIDJwk();
                Assert.IsNotNull(didJwk);
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                Assert.Fail();
            }
        }

        [TestMethod]
        public void GetPublicJwk()
        {
            try
            {
                Keys keys = GenerateKeyPair();
                string didJwk = CreateDIDJwk(keys.publicKey);
                RSAJwk rsaJwk = _didJwk.GetPublicJwk<RSAJwk>(didJwk);
                string rsaJWKString = JsonSerializer.Serialize(rsaJwk);
                Assert.AreNotSame(string.Empty, rsaJwk?.jwk.n ?? string.Empty);

            }
            catch (Exception ex)
            {
                string err = ex.Message;
                Assert.Fail();
            }
        }

        private string CreateDIDJwk()
        {
            try
            {
                Keys keys = GenerateKeyPair();
                RSAJwk rsaJwk = _rsaOperator.GetPublicJwk(keys.publicKey);
                string didJwk = _didJwk.CreateDID(rsaJwk);

                return didJwk;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private string CreateDIDJwk(string rsaPublicKey)
        {
            try
            {
                RSAJwk rsaJwk = _rsaOperator.GetPublicJwk(rsaPublicKey);
                string didJwk = _didJwk.CreateDID(rsaJwk);

                return didJwk;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private Keys GenerateKeyPair()
        {
            try
            {
                Keys keys = _rsaOperator.GenerateKeys();
                return keys;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
