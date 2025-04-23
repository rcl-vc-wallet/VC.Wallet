using VC.Wallet.Data;

namespace VC.Wallet.Core
{
    public interface IHolderDIDService
    {
        public Task<List<HolderDID>> GetHolderDIDsByUserAsync(string username);
        public Task<HolderDID> GetHolderDIDByIdAsync(int id);
        public Task<HolderDID> AddHolderDIDAsync(HolderDID holderDID);
    }
}
