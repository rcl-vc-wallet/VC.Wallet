using VC.Wallet.Data;

namespace VC.Wallet.Core
{
    public interface ITrustedIssuerService
    {
        public Task<List<TrustedIssuer>> GetTrustedUsersByAdminUsernameAsync(string adminUsername);
        public Task<TrustedIssuer> GetTrustedIssuerByIdAsync(int id);
        public Task<TrustedIssuer> GetTrustedIssuerByDIDAsync(string did);
        public Task<TrustedIssuer> GetTrustedIssuerByTypeAsync(string type);
        public Task<TrustedIssuer> CreateTrustedIssuerAsync(TrustedIssuer trustedIssuer);
        public Task<TrustedIssuer> UpdateTrustedIssuerAsync(TrustedIssuer trustedIssuer);
        public Task DeleteTrustedIssuerAsync(int id);
    }
}
