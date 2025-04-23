
namespace VC.Wallet.Core.Credential.Resolver
{
    // TODO Resolve PNG and SVG file type

    internal class CredentialResolverImg : CredentialResolverBase, ICredentialResolver
    {
        private readonly IImageService _imageService;

        public CredentialResolverImg(IJwtOperator jwtOperator, IImageService imageService) 
            : base(jwtOperator)
        {
            _imageService = imageService;
        }

        public AchievementCredential Resolve(string credentialFileContent)
        {
            throw new NotImplementedException();
        }
    }
}
