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
            if(credentialFileType == "jwt")
            {
                return new CredentialResolverJwt(_jwtOperator);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
