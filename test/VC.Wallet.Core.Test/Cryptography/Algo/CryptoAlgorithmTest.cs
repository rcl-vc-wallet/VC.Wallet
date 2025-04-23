#nullable disable

using System.Text.Json;

namespace VC.Wallet.Core.Test
{
    [TestClass]
    public class CryptoAlgorithmTest
    {
        private readonly ICryptoAlgorithmFactory _algorithmFactory;
        private readonly ICryptoAlgorithm<RSAJwk> _algoOperator; // RSA
     // private readonly ICryptoAlgorithmOperator<ECJwk> _algoOperator; // EC

        public CryptoAlgorithmTest()
        {
            _algorithmFactory = (ICryptoAlgorithmFactory)DependencyResolver.ServiceProvider().GetService(typeof(ICryptoAlgorithmFactory));
            _algoOperator = _algorithmFactory.Create<RSAJwk>(); // RSA
         // _algoOperator = _algorithmFactory.Create<ECJwk>(); // EC
        }

        [TestMethod]
        public void GenerateKeyPairTest()
        {
            try
            {
                Keys keys = GenerateRSAKeyPair();
                Assert.AreNotEqual(string.Empty, keys?.publicKey ?? string.Empty);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                Assert.Fail();
            }
        }

        [TestMethod]
        public void GetPublicJwkAndKeyTest()
        {
            try
            {
                Keys keys = GenerateRSAKeyPair();
                RSAJwk publicJwk = _algoOperator.GetPublicJwk(keys.publicKey); // RSA
             // ECJwk publicJwk = _algoOperator.GetPublicJwk(keys.publicKey); // EC
                string publicJWKJson = JsonSerializer.Serialize(publicJwk);
                string publicKey = _algoOperator.GetPublicKey(publicJwk);
                Assert.AreEqual(keys.publicKey, publicKey);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                Assert.Fail();
            }
        }

        private Keys GenerateRSAKeyPair()
        {
            try
            {
                Keys keys = _algoOperator.GenerateKeys();
                return keys;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
