#nullable disable

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VC.Wallet.Core;

namespace VC.Wallet.WebApp.Areas.HolderDID.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IHolderDIDService _holderDIDService;
        public string ErrorMessage { get; set; } = string.Empty;

        public List<Data.HolderDID> HolderDIDs { get; set; } = new List<Data.HolderDID>();

        public IndexModel(IHolderDIDService holderDIDService)
        {
            _holderDIDService = holderDIDService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                HolderDIDs = await _holderDIDService.GetHolderDIDsByUserAsync(User?.Identity?.Name);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }

            return Page();
        }
    }
}
