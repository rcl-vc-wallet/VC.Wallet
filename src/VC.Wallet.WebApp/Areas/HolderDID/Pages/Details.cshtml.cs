#nullable disable

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VC.Wallet.Core;

namespace VC.Wallet.WebApp.Areas.HolderDID.Pages
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly IHolderDIDService _holderDIDService;

        public string ErrorMessage { get; set; } = string.Empty;

        public Data.HolderDID HolderDID {  get; set; } = new Data.HolderDID();

        public DetailsModel(IHolderDIDService holderDIDService)
        {
            _holderDIDService = holderDIDService;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            try
            {
                HolderDID = await _holderDIDService.GetHolderDIDByIdAsync(id);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }

            return Page();
        }
    }
}
