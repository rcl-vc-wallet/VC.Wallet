
namespace VC.Wallet.Core
{
    internal class CryptoAlgorithmFactory : ICryptoAlgorithmFactory 
    {
        public ICryptoAlgorithm<T> Create<T>()
            where T : class, new()
        {
            if(typeof(T) == typeof(ECJwk))
            {
                return (ICryptoAlgorithm<T>) new ECAlgorithm();
            }
            else if(typeof(T) == typeof(RSAJwk))
            {
                return (ICryptoAlgorithm<T>) new RSAAlgorithm();
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
