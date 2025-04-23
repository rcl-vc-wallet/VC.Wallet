
namespace VC.Wallet.Core
{
    public interface ICredentialResolverFactory
    {
        public ICredentialResolver Create(string credentialFileType);
    }
}
