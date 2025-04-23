
namespace VC.Wallet.Core
{
    public interface ICredentialResolver
    {
        public AchievementCredential Resolve(string credentialFileContent);
    }
}
