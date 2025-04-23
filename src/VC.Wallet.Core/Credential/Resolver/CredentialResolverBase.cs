#nullable disable

using System.Text.Json;

namespace VC.Wallet.Core
{
    public abstract class CredentialResolverBase
    {
        private readonly IJwtOperator _jwtOperator;

        protected CredentialResolverBase(IJwtOperator jwtOperator)
        {
            _jwtOperator = jwtOperator;
        }

        public virtual string GetCredentialString(string credentialFileContent)
        {
            return credentialFileContent;
        }

        public AchievementCredential GetAchievementCredential(string credentialStrng)
        {
           Jwt jwt = _jwtOperator.JwtFromJwtCompact(credentialStrng);
           JwtDecoded jwtDecoded = _jwtOperator.DecodeJwt(jwt);
            return JsonSerializer.Deserialize<AchievementCredential>(jwtDecoded?.decodedPayload);
        }
    }
}
