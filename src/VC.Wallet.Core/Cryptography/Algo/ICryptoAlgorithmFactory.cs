
namespace VC.Wallet.Core
{
    public interface ICryptoAlgorithmFactory
    {
        public ICryptoAlgorithm<T> Create<T>()
            where T : class, new();
    }
}
