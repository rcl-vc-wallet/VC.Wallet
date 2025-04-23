using Microsoft.EntityFrameworkCore;

namespace VC.Wallet.Data
{
    public class VCWalletDbContext : DbContext
    {
        public VCWalletDbContext()
        {
        }

        public VCWalletDbContext(DbContextOptions<VCWalletDbContext> options)
         : base(options)
        {
        }

        public virtual DbSet<Holder> Holders { get; set; }
        public virtual DbSet<HolderDID> HolderDIDs { get; set; }
        public virtual DbSet<HolderCredential> HolderCredentials { get; set; }
        public virtual DbSet<HolderCredentialGroup> HolderCredentialGroups { get; set; }
        public virtual DbSet<TrustedIssuer> TrustedIssuers { get; set; }
    }
}
