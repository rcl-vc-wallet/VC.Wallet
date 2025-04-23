using VC.Wallet.Core.Credential.Resolver;

namespace VC.Wallet.Core
{
    internal class CredentialResolverFactory : ICredentialResolverFactory
    {
        private readonly IJwtOperator _jwtOperator;
        private readonly IImageFactory _imageFactory;

        public CredentialResolverFactory(IJwtOperator jwtOperator, 
            IImageFactory imageFactory)
        {
            _jwtOperator = jwtOperator;
            _imageFactory = imageFactory;
        }

        public ICredentialResolver Create(string credentialFileType)
        {
            if(credentialFileType == "txt")
            {
                return new CredentialResolverTxt(_jwtOperator);
            }
            else if(credentialFileType == "png")
            {
                IImageService imageService = _imageFactory.Create(ImageType.PNG);
                return new CredentialResolverImg(_jwtOperator,imageService);
            }
            else if (credentialFileType == "svg")
            {
                IImageService imageService = _imageFactory.Create(ImageType.SVG);
                return new CredentialResolverImg(_jwtOperator, imageService);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
