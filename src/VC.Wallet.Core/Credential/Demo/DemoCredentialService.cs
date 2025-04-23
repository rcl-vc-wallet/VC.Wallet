
namespace VC.Wallet.Core
{
    internal class DemoCredentialService : IDemoCredentialService
    {
        public AchievementCredential CreateDemoAchievementCredential(string subjectDID, string subjectName, string issuerDID)
        {
            List<string> _content = new List<string> { "https://www.w3.org/ns/credentials/v2", "https://purl.imsglobal.org/spec/ob/v3p0/context-3.0.3.json" };
            
            string _id = $"urn:uuid:{Guid.NewGuid().ToString()}";
            
            List<string> _type = new List<string> { "VerifiableCredential", "OpenBadgeCredential" };

            List<string> _profileTypes = new List<string> { "Profile" };

            Profile _issuer = new Profile
            {
                name = "VC Web Wallet",
                id = issuerDID,
                type = _profileTypes
            };

            string _validFrom = DateTime.Now.ToString();

            string _validUntil = DateTime.Now.AddYears(20).ToString();

            List<string> _achievementTypes = new List<string> { "Achievement" };
            Criteria _criteria = new Criteria
            {
                narrative = "To obtain this credential the user is able to create a profile, DID and add a credential to their VCl Web Wallet."
            };

            Achievement _achievement = new Achievement
            {
                id = $"urn:uuid:08e26d22-8dca-4558-9c1",
                type = _achievementTypes,
                name = "Demo Verifiable Credential",
                description = "This credential is issued to a user who can use the Verifiable Credential Web Wallet.",
                criteria = _criteria
            };

            List<string> _subjectTypes = new List<string> { "AchievementSubject" };

            AchievementSubject _credentialSubject = new AchievementSubject
            {
                id = subjectDID,
                name = subjectName,
                type = _subjectTypes,
                achievement = _achievement
            };

            AchievementCredential _credential = new AchievementCredential
            {
                context = _content,
                id = _id,
                type = _type,
                issuer = _issuer,
                validFrom = _validFrom,
                validUntil = _validUntil,
                credentialSubject = _credentialSubject,
                iss = _issuer.id,
                jti = _id,
                sub = _credentialSubject.id,
            };

            return _credential;
        }
    }
}
