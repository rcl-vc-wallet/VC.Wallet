#nullable disable

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VC.Wallet.Core;

namespace VC.Wallet.WebApp.Areas.Holder.Pages
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly IHolderService _holderService;

        [BindProperty]
        public Data.Holder Holder { get; set; } = new Data.Holder();
        public string ErrorMessage { get; set; } = string.Empty;

        public EditModel(IHolderService holderService)
        {
            _holderService = holderService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                Holder = await _holderService.GetHolderByUsernameAsync(User?.Identity?.Name);

                if (string.IsNullOrEmpty(Holder?.name))
                {
                    ErrorMessage = "Holder not found";
                }
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
                    Holder.username = User.Identity.Name;

                    var newHolder = await _holderService.UpdateHolderAsync(Holder, User.Identity.Name);

                    if (string.IsNullOrEmpty(newHolder?.name))
                    {
                        ErrorMessage = "Could not update Holder Profile";
                    }
                    else
                    {
                        return RedirectToPage("./Details");
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
