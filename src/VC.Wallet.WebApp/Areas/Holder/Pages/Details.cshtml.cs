#nullable disable

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VC.Wallet.Core;

namespace VC.Wallet.WebApplication.Areas.Holder.Pages
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly IHolderService _holderService;
        
        public Data.Holder Holder { get; set; } = new Data.Holder();
        public string ErrorMessage { get; set; } = string.Empty;

        public DetailsModel(IHolderService holderService)
        {
            _holderService = holderService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                Holder = await _holderService.GetHolderByUsernameAsync(User.Identity.Name);

                if(string.IsNullOrEmpty(Holder?.name))
                {
                    return RedirectToPage("./Create");
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
