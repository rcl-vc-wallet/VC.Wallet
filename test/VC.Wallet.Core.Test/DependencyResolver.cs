#nullable disable

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace VC.Wallet.Core.Test
{
    public static class DependencyResolver
    {
        public static ServiceProvider ServiceProvider()
        {
            ConfigurationBuilder builder = new ConfigurationBuilder();
            IConfiguration Configuration = builder.Build();

            IServiceCollection services = new ServiceCollection();

            services.AddVCWebWalletServices(options => Configuration.Bind("Configuration:AdminUsername"));

            return services.BuildServiceProvider();
        }
    }

    public class TestProject
    {
    }
}
