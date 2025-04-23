
namespace VC.Wallet.Core
{
    internal class CredentialResolverTxt : CredentialResolverBase, ICredentialResolver
    {
        public CredentialResolverTxt(IJwtOperator jwtOperator) : base(jwtOperator)
        {
        }

        public AchievementCredential Resolve(string credentialFileContent)
        {
            string credentialString = GetCredentialString(credentialFileContent);
            return GetAchievementCredential(credentialString);
        }
    }
}
