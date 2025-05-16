
namespace VC.Wallet.Core
{
    public interface ICryptoAlgorithm<T> where T : class, new() 
    {
        public T GetPublicJwk(string publicKey);
        public string GetPublicKey(T RsaJwk);
        public byte[] SignData(byte[] data, string privateKey);
        public bool VerifySignature(byte[] data, byte[] signature, string publicKey);
        public Task<T> GetPublicJwkFromKidAsync(string kid);
        public JwtHeader<T> GetJwtHeader(string publicKey, string type);
        public JwtHeader<T> SetKeyIdJwtHeader(string keyId, string type);
    }
}
