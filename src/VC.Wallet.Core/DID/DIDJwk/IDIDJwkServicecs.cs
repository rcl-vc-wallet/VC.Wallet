namespace VC.Wallet.Core
{
    public interface IDIDJwkService
    {
        public T GetPublicJwk<T>(string DID);
    }
}
