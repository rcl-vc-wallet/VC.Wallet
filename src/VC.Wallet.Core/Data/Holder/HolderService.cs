#nullable disable

using Microsoft.EntityFrameworkCore;
using VC.Wallet.Data;

namespace VC.Wallet.Core
{
    internal class HolderService : IHolderService
    {
        private readonly VCWalletDbContext _db;

        public HolderService(VCWalletDbContext db)
        {
            _db = db;
        }

        public async Task<Holder> CreateHolderAsync(Holder holder)
        {
            _db.Holders.Add(holder);
            await _db.SaveChangesAsync();
            return holder;
        }

        public async Task<Holder> GetHolderByUsernameAsync(string username)
        {
             var _holder = await _db.Holders
            .Where(w => w.username == username)
            .AsNoTracking()
            .FirstOrDefaultAsync();

            return _holder;
        }

        public async Task<Holder> UpdateHolderAsync(Holder holder, string username)
        {
            var _holder = await GetHolderByUsernameAsync(username);

            if (!string.IsNullOrEmpty(_holder?.username))
            {
                _db.Attach(holder).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return holder;
            }
            else
            {
                throw new Exception("Holder not found");
            }
        }
    }
}
