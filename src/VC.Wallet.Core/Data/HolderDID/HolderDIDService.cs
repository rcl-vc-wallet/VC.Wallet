#nullable disable

using Microsoft.EntityFrameworkCore;
using VC.Wallet.Data;

namespace VC.Wallet.Core
{   
    internal class HolderDIDService : IHolderDIDService
    {
        private readonly VCWalletDbContext _db;

        public HolderDIDService(VCWalletDbContext db)
        {
            _db = db;
        }

        public async Task<List<HolderDID>> GetHolderDIDsByUserAsync(string username)
        {
            List<HolderDID> dids = await _db.HolderDIDs
                    .Where(w => w.holderUsername == username)
                    .AsNoTracking()
                    .OrderBy(o => o.sortingCode)
                    .ToListAsync();

            return dids;
        }

        public async Task<HolderDID> GetHolderDIDByIdAsync(int id)
        {
            HolderDID holderDID = await _db.HolderDIDs.Where(w => w.id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return holderDID;
        }

        public async Task<HolderDID> AddHolderDIDAsync(HolderDID holderDID)
        {
            HolderDID existigDID = await GetGlobalDIDAsync(holderDID.did);

            if (!string.IsNullOrEmpty(existigDID?.holderUsername))
            {
                throw new Exception("Cannot add a duplicate DID");
            }

            _db.HolderDIDs.Add(holderDID);
            await _db.SaveChangesAsync();

            return holderDID;
        }

        private async Task<HolderDID> GetGlobalDIDAsync(string did)
        {
            HolderDID _did = await _db.HolderDIDs
                   .Where(w => w.did == did)
                   .AsNoTracking()
                   .FirstOrDefaultAsync();

            return _did;
        }

    }
}
