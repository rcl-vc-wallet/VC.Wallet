using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace VC.Wallet.Core
{
    public static class VCWalletExtension
    {
        public static IServiceCollection AddVCWebWalletServices(this IServiceCollection services, 
            Action<IConfigurationOptions> configureOptions)
        {
            services.TryAddTransient<IHolderService, HolderService>();
            services.TryAddTransient<IHolderDIDService, HolderDIDService>();
            services.TryAddTransient<IHolderCredentialService, HolderCredentialService>();
            services.TryAddTransient<IHolderCredentialGroupService, HolderCredentialGroupService>();
            services.TryAddTransient<ITrustedIssuerService, TrustedIssuerService>();
            services.TryAddTransient<ICryptoAlgorithmFactory, CryptoAlgorithmFactory>();
            services.TryAddTransient<IJwtOperator, JwtOperator>();
            services.TryAddTransient<IDIDJwkService, DIDJwkService>();
            
            services.TryAddTransient<ICredentialResolverFactory, CredentialResolverFactory>();

            services.Configure<IConfigurationOptions>(configureOptions);

            return services;
        }
    }
}
