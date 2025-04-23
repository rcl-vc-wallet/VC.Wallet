#nullable disable

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VC.Wallet.Core;
using System.Text;

namespace VC.Wallet.WebApp.Areas.HolderCredential.Pages
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly IHolderService _holderService;
        private readonly IHolderDIDService _holderDIDService;
        private readonly IDemoCredentialService _demoCredentialService;
        private readonly ITrustedIssuerService _trustedIssuerService;
        private readonly IJwtOperator _jwtOperator;
        private readonly IDIDJwkService _didJwkService;
        private readonly ICryptoAlgorithmFactory _cryptoAlgorithmFactory;
        private readonly ICryptoAlgorithm<RSAJwk> _cryptoAlgorithm;

        public string ErrorMessage { get; set; } = string.Empty;

        public CreateModel(IHolderService holderService,
            IHolderDIDService holderDIDService,
            IDemoCredentialService demoCredentialService,
            ITrustedIssuerService trustedIssuerService,
            IJwtOperator jwtOperator,
            IDIDJwkService didJwkService,
            ICryptoAlgorithmFactory cryptoAlgorithmFactory)
        {
            _holderService = holderService;
            _holderDIDService = holderDIDService;
            _demoCredentialService = demoCredentialService;
            _trustedIssuerService = trustedIssuerService;
            _jwtOperator = jwtOperator;
            _didJwkService = didJwkService;
            _cryptoAlgorithmFactory = cryptoAlgorithmFactory;
            _cryptoAlgorithm = _cryptoAlgorithmFactory.Create<RSAJwk>();
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostDownloadBadge()
        {
            try
            {
                Data.Holder holder = await _holderService.GetHolderByUsernameAsync(User?.Identity.Name);

                if (string.IsNullOrEmpty(holder?.username))
                {
                    ErrorMessage = "Holder not found.";
                    return Page();
                }

                List<Data.HolderDID> holderDIDs = await _holderDIDService.GetHolderDIDsByUserAsync(User?.Identity.Name);

                if (holderDIDs?.Count < 1)
                {
                    ErrorMessage = "Holder DID not found.";
                    return Page();
                }

                Data.TrustedIssuer selfIssuer = await _trustedIssuerService.GetTrustedIssuerByTypeAsync(VCWalletConstants.SELF);

                Keys keys = new Keys();

                if (string.IsNullOrEmpty(selfIssuer?.name))
                {
                    (keys, selfIssuer) = await CreateSelfIssuerAsync();
                }

                AchievementCredential achievementCredential = _demoCredentialService.CreateDemoAchievementCredential(holderDIDs[0].did, holder.name, selfIssuer.did);

                Jwt jwt = _jwtOperator.Sign(achievementCredential, keys, _cryptoAlgorithm);

                string jwtcompact = _jwtOperator.ToJwtCompact(jwt);

                byte[] bytes = Encoding.UTF8.GetBytes(jwtcompact);

                return File(bytes, "text/plain", "credential.txt");
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }

            return Page();
        }

        private async Task<(Keys, Data.TrustedIssuer)> CreateSelfIssuerAsync()
        {
            try
            {
                Keys keys = _cryptoAlgorithm.GenerateKeys();

                RSAJwk rsaPublicJwk = _cryptoAlgorithm.GetPublicJwk(keys.publicKey);

                string did = _didJwkService.CreateDID<RSAJwk>(rsaPublicJwk);

                Data.TrustedIssuer trustedIssuer = new Data.TrustedIssuer
                {
                    name = "VC Web Wallet",
                    description = "Self issuer for the demo verifiable credential",
                    adminUsername = User.Identity.Name,
                    countryCode = "TT",
                    region = "None",
                    websiteUrl = "None",
                    type = VCWalletConstants.SELF,
                    privateKey = keys.privateKey,
                    did = did,
                };

                await _trustedIssuerService.CreateTrustedIssuerAsync(trustedIssuer);

                return (keys, trustedIssuer);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
