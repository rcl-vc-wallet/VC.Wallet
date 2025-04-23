namespace VC.Wallet.Core
{
    public class ValidationBuilder
    {
        public List<IValidator> validators { get; private set; }
        public string _jwtCompact;

        public ValidationBuilder(string jwtCompact)
        {
            validators = new List<IValidator>();
            _jwtCompact = jwtCompact;
        }

        public ValidationBuilder Add(IValidator validator)
        {
            validators.Add(validator);
            return this;
        }

        public async Task<ValidationResponse>  ValidateAsync()
        {
            ValidationResponse validationResponse = new ValidationResponse
            {
                errorMessage = string.Empty,
                isValid = true
            };

            foreach(var validator in validators)
            {
                validationResponse = await validator.ValidateAsync(_jwtCompact);
                if (!validationResponse.isValid)
                    return validationResponse;
            }

            return validationResponse;
        }
    }
}
