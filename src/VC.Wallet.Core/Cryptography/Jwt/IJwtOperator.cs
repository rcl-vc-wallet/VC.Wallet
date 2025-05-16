namespace VC.Wallet.Core
{
    public interface IJwtOperator
    {
        public Jwt Sign<T>(object payload, Keys keys, ICryptoAlgorithm<T> cryptoAlgorithm, string keyId = "")
            where T : class, new();
        public bool Verify<T>(string jwtCompact, string publicKey, ICryptoAlgorithm<T> cryptoAlgorithm)
            where T : class, new();
        public string ToJwtCompact(Jwt jwt);
        public Jwt JwtFromJwtCompact(string jwtCompact);
        public JwtDecoded DecodeJwt(Jwt jwt);
        public T GetPublicJwk<T>(string jwtCompact)
            where T : class, new();
    }
}