namespace VC.Wallet.Core
{
    internal class CredentialResolverFactory : ICredentialResolverFactory
    {
        private readonly IJwtOperator _jwtOperator;
       

        public CredentialResolverFactory(IJwtOperator jwtOperator)
        {
            _jwtOperator = jwtOperator;
        }

        public ICredentialResolver Create(string credentialFileType)
        {
            if(credentialFileType == "txt")
            {
                return new CredentialResolverTxt(_jwtOperator);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
