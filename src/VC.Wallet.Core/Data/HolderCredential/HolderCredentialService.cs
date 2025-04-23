#nullable disable

using Microsoft.EntityFrameworkCore;
using VC.Wallet.Data;

namespace VC.Wallet.Core
{
    internal class HolderCredentialService : IHolderCredentialService
    {
        private readonly VCWalletDbContext _db;

        public HolderCredentialService(VCWalletDbContext db)
        {
            _db = db;
        }

        public async Task<HolderCredential> CreateHolderCredentialAsync(HolderCredential holderCredential)
        {
            HolderCredential existingHolderCredential = await GetHolderCredentialByCredentialIdAsync(holderCredential.credentialId, holderCredential.holderUsername);

            if (!string.IsNullOrEmpty(existingHolderCredential?.credentialId))
            {
                throw new Exception("Cannot add a badge with a duplicate credential id");
            }

            _db.HolderCredentials.Add(holderCredential);
            await _db.SaveChangesAsync();
            return holderCredential;
        }

        public async Task<HolderCredential> GetHolderCredentialByCredentialIdAsync(string credentialId, string username)
        {
            HolderCredential holderCredential = await _db.HolderCredentials
                     .Where(w => w.credentialId == credentialId && w.holderUsername == username)
                     .AsNoTracking()
                     .FirstOrDefaultAsync();

            return holderCredential;
        }

        public async Task<HolderCredential> GetHolderCredentialByIdAsync(int id, string username)
        {
            HolderCredential holderCredential = await _db.HolderCredentials
                     .Where(w => w.id == id && w.holderUsername == username)
                     .AsNoTracking()
                     .FirstOrDefaultAsync();

            return holderCredential;
        }

        public async Task<List<HolderCredential>> GetHolderCredentialsbyUsernameAsync(string username)
        {
            List<HolderCredential> holderCredentials = await _db.HolderCredentials
                     .Where(w => w.holderUsername == username)
                     .AsNoTracking()
                     .OrderBy(o => o.sortingCode)
                     .ToListAsync();

            return holderCredentials;
        }
    }
}
