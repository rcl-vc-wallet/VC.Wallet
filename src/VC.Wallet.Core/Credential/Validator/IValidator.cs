namespace VC.Wallet.Core
{
    public interface IValidator
    {
        Task<ValidationResponse> ValidateAsync(string jwtCompact);
    }
}
