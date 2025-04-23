using VC.Wallet.Data;

namespace VC.Wallet.Core
{
    public interface IHolderCredentialGroupService
    {
        public Task<HolderCredentialGroup> CreateHolderCredentialGroupAsync(HolderCredentialGroup holderCredentialGroup);
        public Task<HolderCredentialGroup> GetHolderCredentialGroupByNameAsync(string name, string username);
        public Task<List<HolderCredentialGroup>> GetHolderCredentialGroupByUsernameAsync(string username);
    }
}
