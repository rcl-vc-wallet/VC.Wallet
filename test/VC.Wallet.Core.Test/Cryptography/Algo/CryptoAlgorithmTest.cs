#nullable disable

using System.Text;

namespace VC.Wallet.Core.Test
{
    [TestClass]
    public class CryptoAlgorithmTest
    {
        private readonly ICryptoAlgorithmFactory _algorithmFactory;
        private readonly ICryptoAlgorithm<RSAJwk> _algoOperator;

        private RSAJwk _RSAJwk = new RSAJwk
        {
            kty = "RSA",
            e = "AQAB",
            n = "xICdahlIZ5Zenx2yR8Tr_9gVJ-eqEg82gJwzaLWdhHwCfHqIcXSmBcWl8jJMYdDnjQtgpjoED9OBOlk8Eg-HSOyAudsAkqzKr3pG22YEFccFgA67U3jLFlt1pDh2jso9XZEKKRkrV0KfSbbU3VGKhX8vSV0xZcdgjGLF_dbIjHtXLChQxdIw0U6uUd857Tkz-srAXHIy1ycnxgLAinqy3L8SgMbIVRtB_f1La3WVY2uS2V3T4bpbGyUPQfi7JFfGhjpnA97-GB0eh30z1nBje6StDFFMZnbQQyOZIczeKKB_vChn0N0bN1Xmhb3tDycU1tTLdFZT6KP1QeQ10g78-Q"
        };

        private string _privateKey = "-----BEGIN RSA PRIVATE KEY-----MIIEpAIBAAKCAQEAxICdahlIZ5Zenx2yR8Tr/9gVJ+eqEg82gJwzaLWdhHwCfHqIcXSmBcWl8jJMYdDnjQtgpjoED9OBOlk8Eg+HSOyAudsAkqzKr3pG22YEFccFgA67U3jLFlt1pDh2jso9XZEKKRkrV0KfSbbU3VGKhX8vSV0xZcdgjGLF/dbIjHtXLChQxdIw0U6uUd857Tkz+srAXHIy1ycnxgLAinqy3L8SgMbIVRtB/f1La3WVY2uS2V3T4bpbGyUPQfi7JFfGhjpnA97+GB0eh30z1nBje6StDFFMZnbQQyOZIczeKKB/vChn0N0bN1Xmhb3tDycU1tTLdFZT6KP1QeQ10g78+QIDAQABAoIBAQC3D5KWiyM2zZEs7r9tuPibCjT7TgjUdjOyMNJ70+YAzH6MdKKz/5XDftQQA+fAoJt2fIj3ksjB7apQL12U/b5so1dwzaOFAVB5lZJ5Rlq75wMqv46oProEBARejvN0JthwWHR8wSPtUPWP+LHp1NMVdt0YnactSEabwlDtH7EJnSLkJiW+YqjPsG0swfoGlLL0iyMES3JJzDixuV1Ypp9Btvi6zby2/UopRm9MPFT2/aLhlSBztKXSqVmeJMxa3FQYvtZ7WCXTlCbbGDZb5m52UzRLtiNhyFRS9M+jpKdG8A4V4vLn1J6pmThOwo4dbBGeJui4G0+8IdzlDa2GcKS5AoGBAM75sSh4zXwUz5YQ53VYdu8fKG7VwJWrykQzztwC68G9CNZDN/1a+PLzuyHvL+ZYFesUhZthljE9OlZ6rcOHgW6C6avTiOtxSCN/QfyyT3/Am4BhtDiQrJh01cfoIJchQ0TrXc0jQQyVlG6j87Pz82WI131GFAFovORzUT2uqsOjAoGBAPML4IUSubi6Ak6h1HWf15aTEcdk3vz9v8iAUs61g43XI2cTwolg6+MVoJyDfn53h0rwYFv0AZtS/2AZNKquRNnKXKrxv6xPUu2rAgIdqNeZ67yosP1cZSY05M4Ya9GTbiYAceB2nbcsU6mCsn7PmeXJpcKVMF5ThF/qssqzyiazAoGAcCOw7LCnBLdx0WjPPzDzH5POIF8HvRAawuEfDu02Nv3or84zo72KbHfJyBUD6tWG6ptv2EQDUCJQXVKSmwHsCqg7WrF92bLC1xi38+XYVOtSVTiaoJsiKGgE/A60ua13+PtdEywqrlrM1TVXcQSXt0dxGZrCdBD/Zvdj6Pud4ekCgYA1ub9kyzHgwsV1ylIjujS25UW91O9x70mwsaUcaoVtGkyKIZUUNyeNqB872dlhbVyr1R0H1pVwV1V6UbgRrrorho8bfpsHJPXI8c9c1l+XYT/6ETQmnYEChBi78iwLRNLJdbm2i6HrdtH7m5eHbq6vW/bXX5E0mLOfXEsvgbTC/wKBgQCRZBtPNvarann1n3V+oik4wlid/ekfayfbNGro6wTWXS8eB2ktBKS0V8TgTGLVzp+0q9ZAhsi6dUGrCMeC0lHHA4dRFWVmXRRCXju1EHcaTY08wCJef5fa7bEKoXyFN74+e9NUZ25xZ9tpDCWd/cdwbmz0MjCFAoLVXTLG/tnPuw==-----END RSA PRIVATE KEY-----";

        private string _publicKey = "-----BEGIN RSA PUBLIC KEY-----MIIBCgKCAQEAxICdahlIZ5Zenx2yR8Tr/9gVJ+eqEg82gJwzaLWdhHwCfHqIcXSmBcWl8jJMYdDnjQtgpjoED9OBOlk8Eg+HSOyAudsAkqzKr3pG22YEFccFgA67U3jLFlt1pDh2jso9XZEKKRkrV0KfSbbU3VGKhX8vSV0xZcdgjGLF/dbIjHtXLChQxdIw0U6uUd857Tkz+srAXHIy1ycnxgLAinqy3L8SgMbIVRtB/f1La3WVY2uS2V3T4bpbGyUPQfi7JFfGhjpnA97+GB0eh30z1nBje6StDFFMZnbQQyOZIczeKKB/vChn0N0bN1Xmhb3tDycU1tTLdFZT6KP1QeQ10g78+QIDAQAB-----END RSA PUBLIC KEY-----";

        private byte[] _data = Encoding.ASCII.GetBytes("sample payload");

        public CryptoAlgorithmTest()
        {
            _algorithmFactory = (ICryptoAlgorithmFactory)DependencyResolver.ServiceProvider().GetService(typeof(ICryptoAlgorithmFactory));
            _algoOperator = _algorithmFactory.Create<RSAJwk>();
        }

        [TestMethod]
        public void GetPublicKeyTest()
        {
            try
            {
                string publicKey = _algoOperator.GetPublicKey(_RSAJwk);
                Assert.AreNotEqual(string.Empty, publicKey);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                Assert.Fail();
            }
        }

        [TestMethod]
        public void GetPublicJwkTest()
        {
            try
            {
                RSAJwk rsaJwk = _algoOperator.GetPublicJwk(_publicKey);
                Assert.AreNotEqual(string.Empty, rsaJwk?.kty ?? string.Empty);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                Assert.Fail();
            }
        }

        [TestMethod]
        public void SignDataTest()
        {
            try
            {
                byte[] signatureBytes = _algoOperator.SignData(_data, _privateKey);
                Assert.AreNotEqual(0, signatureBytes.Length);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                Assert.Fail();
            }
        }

        [TestMethod]
        public void VerifySignatureTest()
        {
            try
            {
                byte[] signatureBytes = _algoOperator.SignData(_data, _privateKey);
                bool b = _algoOperator.VerifySignature(_data, signatureBytes, _publicKey);
                Assert.IsTrue(b);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                Assert.Fail();
            }
        }

        [TestMethod]
        public void GetJwtHeaderTest()
        {
            try
            {
                JwtHeader<RSAJwk> jwtHeader = _algoOperator.GetJwtHeader(_publicKey, "JWT");
                Assert.AreEqual("JWT", jwtHeader?.typ);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                Assert.Fail();
            }
        }

        [TestMethod]
        public void SetKeyIdJwtHeaderTest()
        {
            try
            {
                JwtHeader<RSAJwk> jwtHeader = _algoOperator.SetKeyIdJwtHeader("https://example.com/.well-known/jwk.json", "JWT");
                Assert.AreEqual("JWT", jwtHeader?.typ);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                Assert.Fail();
            }
        }
    }
}
