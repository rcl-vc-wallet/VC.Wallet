#nullable disable

using System.Security.Cryptography;

namespace VC.Wallet.Core.Test
{
    [TestClass]
    public class JwtTest
    {
        private readonly IJwtOperator _jwtOperator;
        private readonly ICryptoAlgorithmFactory _algorithmFactory;
        private readonly ICryptoAlgorithm<RSAJwk> _algoOperator; // RSA
        private readonly IDemoCredentialService _demoCredentialService;

        public JwtTest()
        {
            _jwtOperator = (IJwtOperator)DependencyResolver.ServiceProvider().GetService(typeof(IJwtOperator));
            _algorithmFactory = (ICryptoAlgorithmFactory)DependencyResolver.ServiceProvider().GetService(typeof(ICryptoAlgorithmFactory));
            _algoOperator = _algorithmFactory.Create<RSAJwk>(); // RSA
            _demoCredentialService = (IDemoCredentialService)DependencyResolver.ServiceProvider().GetService(typeof(IDemoCredentialService));
        }

        [TestMethod]
        public void SignAndVerifyTest()
        {
            try
            {
                AchievementCredential vc = _demoCredentialService.CreateDemoAchievementCredential("did:jwk:ejrdesrsdrk...", "John Doe", "did:jwk:ejfgsdrk...");

                Keys keys = _algoOperator.GenerateKeys();

                Jwt jwt = _jwtOperator.Sign(vc, keys, _algoOperator);

                JwtDecoded jwtDecoded = _jwtOperator.DecodeJwt(jwt);

                string jwtCompact = _jwtOperator.ToJwtCompact(jwt);

                bool b = _jwtOperator.Verify(jwtCompact, keys.publicKey,_algoOperator); 

                Jwt jwtDeserialized = _jwtOperator.JwtFromJwtCompact(jwtCompact);

                Assert.AreEqual(true, b);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                Assert.Fail();
            }
        }
    }
}
