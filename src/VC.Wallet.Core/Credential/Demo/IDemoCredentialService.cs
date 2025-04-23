
namespace VC.Wallet.Core
{
    public interface IDemoCredentialService
    {
        public AchievementCredential CreateDemoAchievementCredential(string subjectDID, string subjectName, string issuerDID);
    }
}
