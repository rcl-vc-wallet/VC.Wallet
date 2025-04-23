#nullable disable

using Microsoft.EntityFrameworkCore;
using VC.Wallet.Data;

namespace VC.Wallet.Core
{
    internal class HolderCredentialGroupService : IHolderCredentialGroupService
    {
        private readonly VCWalletDbContext _db;

        public HolderCredentialGroupService(VCWalletDbContext db)
        {
            _db = db;
        }

        public async Task<HolderCredentialGroup> CreateHolderCredentialGroupAsync(HolderCredentialGroup holderCredentialGroup)
        {
            HolderCredentialGroup existingHolderCredentialGroup = await GetHolderCredentialGroupByNameAsync(holderCredentialGroup.groupName, holderCredentialGroup.holderUsername);

            if (!string.IsNullOrEmpty(existingHolderCredentialGroup?.groupName))
            {
                throw new Exception("Cannot add a badge group with a duplicate name");
            }

            _db.HolderCredentialGroups.Add(holderCredentialGroup);
            await _db.SaveChangesAsync();

            return holderCredentialGroup;
        }

        public async Task<HolderCredentialGroup> GetHolderCredentialGroupByNameAsync(string name, string username)
        {
            HolderCredentialGroup holderCredentialGroup = await _db.HolderCredentialGroups.
                    Where(w => w.groupName == name && w.holderUsername == username)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

            return holderCredentialGroup;
        }

        public async Task<List<HolderCredentialGroup>> GetHolderCredentialGroupByUsernameAsync(string username)
        {
            List<HolderCredentialGroup> holderBadgeGroups = await _db.HolderCredentialGroups.
                   Where(w => w.holderUsername == username)
                   .AsNoTracking()
                   .OrderBy(o => o.sortingCode)
                   .ToListAsync();

            return holderBadgeGroups;
        }
    }
}
