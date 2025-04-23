
namespace VC.Wallet.Core
{
    public interface ICryptoAlgorithm<T> where T : class, new() 
    {
        public Keys GenerateKeys();
        public T GetPublicJwk(string publicKey, string typ = "");
        public T SetPublicJwkKeyId(string keyId, string typ = "");
        public string GetPublicKey(T verifiedPublicJwk);
        public Task<T> VerifyPublicJwkAsync(T publicJwk);
        public byte[] SignData(byte[] data, string privateKey);
        public bool VerifySignature(byte[] data, byte[] signature, string publicKey);
    }
}
