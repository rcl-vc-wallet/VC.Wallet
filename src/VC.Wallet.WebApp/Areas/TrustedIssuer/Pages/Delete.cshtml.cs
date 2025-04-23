using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VC.Wallet.Core;

namespace VC.Wallet.WebApp.Areas.TrustedIssuer.Pages
{
    [Authorize(Policy = "RequireAdmin")]
    public class DeleteModel : PageModel
    {
        private readonly ITrustedIssuerService _trustedIssuerService;

        public string ErrorMessage { get; set; } = string.Empty;

        public Data.TrustedIssuer TrustedIssuer { get; set; } = new Data.TrustedIssuer();

        public DeleteModel(ITrustedIssuerService trustedIssuerService)
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

        public async Task<IActionResult> OnPostAsync(int id)
        {
            try
            {
                await _trustedIssuerService.DeleteTrustedIssuerAsync(id);
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }

            return Page();
        }
    }
}
