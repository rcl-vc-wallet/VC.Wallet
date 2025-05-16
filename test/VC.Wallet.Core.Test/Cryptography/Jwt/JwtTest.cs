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
       

        public JwtTest()
        {
            _jwtOperator = (IJwtOperator)DependencyResolver.ServiceProvider().GetService(typeof(IJwtOperator));
            _algorithmFactory = (ICryptoAlgorithmFactory)DependencyResolver.ServiceProvider().GetService(typeof(ICryptoAlgorithmFactory));
            _algoOperator = _algorithmFactory.Create<RSAJwk>(); // RSA
        }

        [TestMethod]
        public void SignAndVerifyTest()
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                Assert.Fail();
            }
        }
    }
}
