
namespace VC.Wallet.Core
{
    internal class CredentialResolverJwt : CredentialResolverBase, ICredentialResolver
    {
        public CredentialResolverJwt(IJwtOperator jwtOperator) : base(jwtOperator)
        {
        }

        public AchievementCredential Resolve(string credentialFileContent)
        {
            string credentialString = GetCredentialString(credentialFileContent);
            return GetAchievementCredential(credentialString);
        }
    }
}
