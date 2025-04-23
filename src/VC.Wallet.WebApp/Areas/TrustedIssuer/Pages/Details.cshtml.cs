#nullable disable

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VC.Wallet.Core;

namespace VC.Wallet.WebApp.Areas.TrustedIssuer.Pages
{
    [Authorize(Policy = "RequireAdmin")]
    public class DetailsModel : PageModel
    {
        private readonly ITrustedIssuerService _trustedIssuerService;

        public string ErrorMessage { get; set; } = string.Empty;

        public Data.TrustedIssuer TrustedIssuer { get; set; } = new Data.TrustedIssuer();

        public DetailsModel(ITrustedIssuerService trustedIssuerService)
        {
            _trustedIssuerService = trustedIssuerService;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            try
            {
                TrustedIssuer = await _trustedIssuerService.GetTrustedIssuerByIdAsync(id);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }

            return Page();
        }
    }
}
