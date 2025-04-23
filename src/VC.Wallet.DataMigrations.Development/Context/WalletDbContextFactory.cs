using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using VC.Wallet.Data;

namespace VC.Wallet.Holder.DataMigrations.Development
{
    public class WalletDbContextFactory: IDesignTimeDbContextFactory<VCWalletDbContext>
    {
        public VCWalletDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<VCWalletDbContext>();
            optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("ConnectionStrings:Database"),
                sqlServerOptions => sqlServerOptions.MigrationsAssembly("VC.Web.Wallet.DataMigrations.Development"));

            return new VCWalletDbContext(optionsBuilder.Options);
        }
    }
}
