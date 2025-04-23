#nullable disable

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VC.Wallet.Core;

namespace VC.Wallet.WebApp.Areas.TrustedIssuer.Pages
{
    [Authorize(Policy = "RequireAdmin")]
    public class IndexModel : PageModel
    {
        private readonly ITrustedIssuerService _trustedIssuerService;

        public string ErrorMessage { get; set; } = string.Empty;

        public List<Data.TrustedIssuer> TrustedIssuers { get; set; } = new List<Data.TrustedIssuer>();

        public IndexModel(ITrustedIssuerService trustedIssuerService)
        {
            _trustedIssuerService = trustedIssuerService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                TrustedIssuers = await _trustedIssuerService.GetTrustedUsersByAdminUsernameAsync(User.Identity.Name);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }

            return Page();
        }
    }
}
