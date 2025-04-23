using VC.Wallet.Data;

namespace VC.Wallet.Core
{
    public interface IHolderService
    {
        public Task<Holder> CreateHolderAsync(Holder holder);
        public Task<Holder> GetHolderByUsernameAsync(string username);
        public Task<Holder> UpdateHolderAsync(Holder holder, string username);
    }
}
