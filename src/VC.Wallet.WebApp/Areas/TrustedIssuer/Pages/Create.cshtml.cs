#nullable disable

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VC.Wallet.Core;

namespace VC.Wallet.WebApp.Areas.TrustedIssuer.Pages
{
    [Authorize(Policy = "RequireAdmin")]
    public class CreateModel : PageModel
    {
        private readonly ITrustedIssuerService _trustedIssuerService;

        public string ErrorMessage { get; set; } = string.Empty;

        [BindProperty]
        public Data.TrustedIssuer TrustedIssuer { get; set; } = new Data.TrustedIssuer(); 

        public CreateModel(ITrustedIssuerService trustedIssuerService)
        {
            _trustedIssuerService = trustedIssuerService;
        }

        public void OnGet()
        {
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
                    TrustedIssuer.adminUsername = User.Identity.Name;

                    Data.TrustedIssuer newTrustedIssuer = await _trustedIssuerService.CreateTrustedIssuerAsync(TrustedIssuer);

                    if(newTrustedIssuer.id < 1)
                    {
                        ErrorMessage = "Could not add Trusted Issuer";
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
