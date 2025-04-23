#nullable disable

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VC.Wallet.Core;

namespace VC.Wallet.WebApp.Areas.HolderCredential.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IHolderCredentialService _holderCredentialService;
        public string ErrorMessage { get; set; } = string.Empty;

        public List<Data.HolderCredential> HolderCredentials { get; set; } = new List<Data.HolderCredential>();

        public IndexModel(IHolderCredentialService holderCredentialService)
        {
            _holderCredentialService = holderCredentialService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                HolderCredentials = await _holderCredentialService.GetHolderCredentialsbyUsernameAsync(User?.Identity?.Name);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.ToString();
            }

            return Page();
        }
    }
}
