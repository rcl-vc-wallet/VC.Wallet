#nullable disable

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VC.Wallet.Core;

namespace VC.Wallet.WebApp.Areas.HolderCredential.Pages
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly IHolderCredentialService _holderCredentialService;
        private readonly ICredentialResolverFactory _credentialResolverFactory;

        public string ErrorMessage { get; set; } = string.Empty;

        public Data.HolderCredential HolderCredential { get; set; } = new Data.HolderCredential();

        public AchievementCredential AchievementCredential { get; set; } = new AchievementCredential();

        public DetailsModel(IHolderCredentialService holderCredentialService, 
            ICredentialResolverFactory credentialResolverFactory)
        {
            _holderCredentialService = holderCredentialService;
            _credentialResolverFactory = credentialResolverFactory;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            try
            {
                HolderCredential = await _holderCredentialService.GetHolderCredentialByIdAsync(id, User.Identity.Name);

                ICredentialResolver credentialResolver = _credentialResolverFactory.Create(HolderCredential?.fileType);

                AchievementCredential = credentialResolver.Resolve(HolderCredential?.file);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }

            return Page();
        }
    }
}
