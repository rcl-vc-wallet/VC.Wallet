#nullable disable

using Microsoft.EntityFrameworkCore;
using VC.Wallet.Data;

namespace VC.Wallet.Core
{
    public class TrustedIssuerService : ITrustedIssuerService
    {
        private readonly VCWalletDbContext _db;

        public TrustedIssuerService(VCWalletDbContext db)
        {
            _db = db;
        }

        public async Task<List<TrustedIssuer>> GetTrustedUsersByAdminUsernameAsync(string adminUsername)
        {
            List<TrustedIssuer> trustedIssuers = await _db.TrustedIssuers
                .Where(w => w.adminUsername == adminUsername)
                .AsNoTracking()
                .OrderBy(o => o.name)
                .ToListAsync();

            return trustedIssuers;
        }

        public async Task<TrustedIssuer> GetTrustedIssuerByIdAsync(int id)
        {
            TrustedIssuer trustedIssuer = await _db.TrustedIssuers
                .Where(w => w.id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return trustedIssuer;
        }

        public async Task<TrustedIssuer> GetTrustedIssuerByDIDAsync( string did)
        {
            TrustedIssuer trustedIssuer = await _db.TrustedIssuers
                .Where(w => w.did == did)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return trustedIssuer;
        }

        public async Task<TrustedIssuer> GetTrustedIssuerByTypeAsync(string type)
        {
            TrustedIssuer trustedIssuer = await _db.TrustedIssuers
                .Where(w => w.type == type)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return trustedIssuer;
        }

        public async Task<TrustedIssuer> CreateTrustedIssuerAsync(TrustedIssuer trustedIssuer)
        {
            TrustedIssuer existingTrustedIssuer = await GetTrustedIssuerByDIDAsync(trustedIssuer.did);

            if(!string.IsNullOrEmpty(existingTrustedIssuer?.did))
            {
                throw new Exception("Cannot add a Trusted Issuer with a duplicate DID");
            }

            _db.TrustedIssuers.Add(trustedIssuer);
            await _db.SaveChangesAsync();

            return trustedIssuer;
        }

        public async Task<TrustedIssuer> UpdateTrustedIssuerAsync(TrustedIssuer trustedIssuer)
        {
            _db.Attach(trustedIssuer).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return trustedIssuer;
        }

        public async Task DeleteTrustedIssuerAsync(int id)
        {
            TrustedIssuer existingTrustedIssuer = await GetTrustedIssuerByIdAsync(id);

            if (string.IsNullOrEmpty(existingTrustedIssuer?.name))
            {
                throw new Exception("Did not Find trusted Isser");
            }

            _db.TrustedIssuers.Remove(existingTrustedIssuer);
            await _db.SaveChangesAsync();
        }
    }
}
