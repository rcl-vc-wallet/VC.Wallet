namespace VC.Wallet.Core
{
    public class CheckForValidSignature<T> : IValidator
        where T : class,new()
    {
        private readonly IJwtOperator _jwtOperator;
        private readonly ICryptoAlgorithm<T> _cryptoAlgorithm;

        public CheckForValidSignature(IJwtOperator jwtOperator,ICryptoAlgorithm<T> cryptoAlgorithm)
        {
            _jwtOperator = jwtOperator;
            _cryptoAlgorithm = cryptoAlgorithm;
        }

        public async Task<ValidationResponse> ValidateAsync(string jwtCompact)
        {
            ValidationResponse validationResponse = new ValidationResponse
            {
                errorMessage = string.Empty,
                isValid = true
            };

            try
            {                
                T publicJwk = _jwtOperator.GetPublicJwk<T>(jwtCompact);
                string publicKey = _cryptoAlgorithm.GetPublicKey(publicJwk);
                bool b = _jwtOperator.Verify<T>(jwtCompact, publicKey, _cryptoAlgorithm);

                if(b == true)
                {
                    validationResponse.isValid = true;
                }
                else
                {
                    validationResponse.isValid = false;
                    validationResponse.errorMessage = "Signature is invalid";
                }
            }
            catch (Exception ex)
            {
                validationResponse.isValid = false;
                validationResponse.errorMessage = ex.Message;
            }
          
            return await Task.Run(() => validationResponse);
        }
    }
}
