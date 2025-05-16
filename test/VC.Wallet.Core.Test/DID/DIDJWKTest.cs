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

        private static string _did = "did:jwk:eyJrdHkiOiJSU0EiLCJuIjoicHVYb3VRS1Vha2t2X2JUZWQ4dkNYLU9FTG1jUzhqQ21DWE9WZFp2b3l5c0wxTWMyMWZGSzBxMXBIN1dMRU1hOUFhd1hQSk1sckdEdmcxT0FiS1h0TkMwZ2hHMTR2dzVqQXpieldsb3F3c25jaGlQRk5ENWt6aTNfUmNpYzlxZlpGUnN3aUdjUkNtRHNKUnlqX244MDhVZkNGdkRnYVZzVjlNNVJhMmNZMHlYQkJDM29tRkpJNXBkTEEySTFFRFZuMWJkTzFaRXQtUFY3Z3c0MWFQZHdhNzd2cTBNRkFDaTNyS0wtTEdzWkNxYlo1Q0ZsNjJFMWNYMU5KZmd1d3BoMDJHMEdRSjZIMnhBSFdnU3BkVUtXcTNQdXJrWVl3VGkwTFJXR015Mk5sSzdwVUxPMlFwem1GN2tWRmdhbW5fb0VOMFlhVkoxbVgzVE02UEVHVzZFdDFRIiwiZSI6IkFRQUIifQ";
        private static string _privateKey = "-----BEGIN RSA PRIVATE KEY-----MIIEogIBAAKCAQEApuXouQKUakkv/bTed8vCX+OELmcS8jCmCXOVdZvoyysL1Mc21fFK0q1pH7WLEMa9AawXPJMlrGDvg1OAbKXtNC0ghG14vw5jAzbzWloqwsnchiPFND5kzi3/Rcic9qfZFRswiGcRCmDsJRyj/n808UfCFvDgaVsV9M5Ra2cY0yXBBC3omFJI5pdLA2I1EDVn1bdO1ZEt+PV7gw41aPdwa77vq0MFACi3rKL+LGsZCqbZ5CFl62E1cX1NJfguwph02G0GQJ6H2xAHWgSpdUKWq3PurkYYwTi0LRWGMy2NlK7pULO2QpzmF7kVFgamn/oEN0YaVJ1mX3TM6PEGW6Et1QIDAQABAoIBADkvN0EE4ENWlknwifUz7IKcWvLnQDeZcZp1bvVgGTBbrG8YXvKI/WEYJ5fExURXQ/VpuIB5zoO0pvwxRSR06cRh3e1h8OhNjqFQV6tSj6o0LgoMAYq4AiQe4INRjG567kdsDIIABneu49qHaK1Pep1dP+RKXinbGrfJZV5OcdQOYqQya6EEtriSl/lc9dXF4LdiWx0BWuPR2DJ8KW99LIqUPyHRNm/747qujho6twlZGhdjc4j5rKJNmweHowvedmwFQAdA+ZVKqBUlbo5XNXfbNwZfpp1XDKzgvb5z0L+0+1F2TTDOqYXQBaJ4AsgoFoBTP6ioiYMAev5qCjKK5FUCgYEA2w0zZOKP04YYnYljxKA3aJPxaXJqoZVVxIsPRHM6molbtRAxjWt7aKVTVDZBqRudv4f8rKjqtfhpEHuT+u0Fy46MoPaACyfi0b7CgSRrWkUQfl6s1VWbR77DJWjyd/IW3w74J8XRFRZhWGeFiu04ZfXLfc7cHpFcD5CJm33h5BMCgYEAwwyvP/Z3I0ocpjQWF9i7iZyCaQxo3ePQpVQKQGRIN/dg5SVxzJHhT10zLSObTZ4FftD1gNso6+SJxe1RDekBTPMlCbnJqEIr3CAmLCYjCmKG+cW8NM4QEU6T7KCT/s4iwxPw27a5Y6sUaVlnLjAnHCujZyJ3Enp5Nhzkf2RaU3cCgYB9e+9wIJWx38SkIntjvUBgiTenZ2MMU4cBg4Poe3Yb5woBDFjGocbdaK/2suokXOyeNuGZa16hhb9yMMjR2wwR0wRehTdOrLez3eqAnoNc/+l16vhpcZP1oqMaACe+bJGLkIC6EAk5Yku7n/oRXtmLFyZOWL0iSbUKOYuK5q8LvwKBgFPQpZY00OlHNZxuEXhZ1+zAZoHBpWaEOOpyfJ1C+o2iLe8J8ibYIRu7jvw8qsXlu7LNFFI02xbS9CkrW4NAtyBb83SEc5VsKfDDl+Os163kvN+kCsYeLmVhgMtUDYSXC/UMIponoO6lW3YRxvbgFk5SqjrtYoF8Q43vSHk9pFrtAoGABTjCx40C7MXAMnbloyroVaqVElxsn1iKM/R3fxc6G3qNl+whgPjcSSHPoi3lGbi4mLsrjJUyIO3TbhO/SJsRmzndFhDa8ate8GQbEVouUSV3fqrbejeNMC6B1zGVni2P2e+r0jZc9ynjLceB3+nbL70EDk0aWkNhG0H+9vn6wx0=-----END RSA PRIVATE KEY-----";

        public DIDJWKTest()
        {
            _didJwk = (IDIDJwkService)DependencyResolver.ServiceProvider().GetService(typeof(IDIDJwkService));
            _algorithmFactory = (ICryptoAlgorithmFactory)DependencyResolver.ServiceProvider().GetService(typeof(ICryptoAlgorithmFactory));
            _rsaOperator = _algorithmFactory.Create<RSAJwk>();
        }

        [TestMethod]
        public void GetPublicJwk()
        {
            try
            {
                RSAJwk rsaJwk = _didJwk.GetPublicJwk<RSAJwk>(_did);
                Assert.AreNotSame(string.Empty, rsaJwk?.kty);

            }
            catch (Exception ex)
            {
                string err = ex.Message;
                Assert.Fail();
            }
        }
    }
}
