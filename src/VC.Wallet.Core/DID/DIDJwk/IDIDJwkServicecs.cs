namespace VC.Wallet.Core
{
    public interface IDIDJwkService
    {
        public string CreateDID<T>(T publicJwk);
        public T GetPublicJwk<T>(string DID);
    }
}
