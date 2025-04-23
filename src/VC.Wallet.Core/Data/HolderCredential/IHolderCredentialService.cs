using VC.Wallet.Data;

namespace VC.Wallet.Core
{
    public interface IHolderCredentialService
    {
        public Task<HolderCredential> CreateHolderCredentialAsync(HolderCredential holderBadge);
        public Task<HolderCredential> GetHolderCredentialByCredentialIdAsync(string credentialId, string username);
        public Task<HolderCredential> GetHolderCredentialByIdAsync(int id, string username);
        public Task<List<HolderCredential>> GetHolderCredentialsbyUsernameAsync(string username);
    }
}