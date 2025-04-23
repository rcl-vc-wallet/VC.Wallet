#nullable disable

using System.Text.Json;

namespace VC.Wallet.Core.Credential
{
    public class CheckForRequiredData : IValidator
    {
        private readonly IJwtOperator _jwtOperator;

        public CheckForRequiredData(IJwtOperator jwtOperator)
        {
            _jwtOperator = jwtOperator;
        }
        public async Task<ValidationResponse> ValidateAsync(string jwtCompact)
        {
            ValidationResponse validationResponse = new ValidationResponse
            {
                isValid = true,
                errorMessage = string.Empty
            };

            try
            {
                Jwt jwt = _jwtOperator.JwtFromJwtCompact(jwtCompact);
                JwtDecoded decodedJwt = _jwtOperator.DecodeJwt(jwt);
                AchievementCredential achievementCredential = JsonSerializer.Deserialize<AchievementCredential>(decodedJwt.decodedPayload);
                
                List<ValidationResponse> _validationResponses = new List<ValidationResponse>();

                _validationResponses.Add(DataValidatorRule.NotEmpy(achievementCredential, nameof(achievementCredential.id)));
                _validationResponses.Add(DataValidatorRule.NotEmpy(achievementCredential.issuer, nameof(achievementCredential.issuer.id)));
                _validationResponses.Add(DataValidatorRule.NotEmpy(achievementCredential, nameof(achievementCredential.validFrom)));
                _validationResponses.Add(DataValidatorRule.NotEmpy(achievementCredential.credentialSubject, nameof(achievementCredential.credentialSubject.id)));
                _validationResponses.Add(DataValidatorRule.NotEmpy(achievementCredential.credentialSubject.achievement, nameof(achievementCredential.credentialSubject.achievement.name)));
                _validationResponses.Add(DataValidatorRule.NotEmpy(achievementCredential.credentialSubject.achievement, nameof(achievementCredential.credentialSubject.achievement.description)));

                foreach (ValidationResponse _validationResponse in _validationResponses)
                {
                    if (_validationResponse.isValid == false)
                    {
                        validationResponse.isValid = _validationResponse.isValid;
                        validationResponse.errorMessage = _validationResponse.errorMessage;
                        break;
                    }
                }
            }
            catch(Exception ex) 
            {
                validationResponse.isValid = false;
                validationResponse.errorMessage = ex.Message;
            }

            return await Task.Run(() => validationResponse);
        }
    }
}
