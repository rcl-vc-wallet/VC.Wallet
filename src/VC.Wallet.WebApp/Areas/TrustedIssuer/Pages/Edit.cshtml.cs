using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VC.Wallet.Core;

namespace VC.Wallet.WebApp.Areas.TrustedIssuer.Pages
{
    [Authorize(Policy = "RequireAdmin")]
    public class EditModel : PageModel
    {
        private readonly ITrustedIssuerService _trustedIssuerService;

        public string ErrorMessage { get; set; } = string.Empty;

        [BindProperty]
        public Data.TrustedIssuer TrustedIssuer { get; set; } = new Data.TrustedIssuer();

        public EditModel(ITrustedIssuerService trustedIssuerService)
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

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ErrorMessage = "Data input was not valid";

                    foreach (var modelState in ModelState.Values)
                    {
                        foreach (var modelError in modelState.Errors)
                        {
                            ErrorMessage = $"{ErrorMessage},{modelError.ErrorMessage}";
                        }
                    }
                }
                else
                {
                    Data.TrustedIssuer editedTrustedIssuer = await _trustedIssuerService.UpdateTrustedIssuerAsync(TrustedIssuer);

                    if(editedTrustedIssuer.id < 1)
                    {
                        ErrorMessage = "Could not update Trusted Issuer";
                    }
                    else
                    {
                        return RedirectToPage("./Index");
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }

            return Page();
        }
    }
}
