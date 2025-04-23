#nullable disable

using System.Reflection;

namespace VC.Wallet.Core
{
    public static class DataValidatorRule
    {
        public static ValidationResponse NotEmpy(object obj, string propertyName)
        {
            ValidationResponse validationResponse = new ValidationResponse
            {
                isValid = true,
                errorMessage = string.Empty
            };

            PropertyInfo pinfo = obj.GetType().GetProperty(propertyName);

            string value = (string)pinfo?.GetValue(obj, null) ?? string.Empty;

            if (string.IsNullOrEmpty(value))
            {
                validationResponse.isValid = false;
                validationResponse.errorMessage = $"{pinfo.Name} cannot be null";
                return validationResponse;
            }

            return validationResponse;

        }
    }
}
