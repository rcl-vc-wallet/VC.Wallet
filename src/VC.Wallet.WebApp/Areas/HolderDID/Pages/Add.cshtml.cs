#nullable disable

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VC.Wallet.Core;

namespace VC.Wallet.WebApp.Areas.HolderDID.Pages
{
    [Authorize]
    public class AddModel : PageModel
    {
        private readonly IHolderDIDService _holderDIDService;
        private readonly ICryptoAlgorithmFactory _algorithmFactory;
        private readonly ICryptoAlgorithm<RSAJwk> _rsaOperator;
        private readonly IJwtOperator _jwtOperator;
        private readonly IDIDJwkService _didJWKService;

        private const string _payload = "The quick brown fox jumps over the lazy dog";

        public string ErrorMessage { get; set; } = string.Empty;

        [BindProperty]
        public Data.HolderDID HolderDID { get; set; } = new Data.HolderDID();

        public AddModel(IHolderDIDService holderDIDService,
            ICryptoAlgorithmFactory algorithmFactory,
            IJwtOperator jwtOperator,
            IDIDJwkService didJWKService)
        {
            _holderDIDService = holderDIDService;
            _algorithmFactory = algorithmFactory;
            _jwtOperator = jwtOperator;
            _didJWKService = didJWKService;

            _rsaOperator = _algorithmFactory.Create<RSAJwk>();
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(IFormFile fileUpload)
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
                    if (fileUpload != null)
                    {
                        string rsaPrivateKey = string.Empty;

                        using (var reader = new StreamReader(fileUpload.OpenReadStream()))
                        {
                            rsaPrivateKey = await reader.ReadToEndAsync();
                        }

                        bool b = ValidateDID(HolderDID.did, rsaPrivateKey);

                        if (b == true)
                        {
                            HolderDID.holderUsername = User.Identity.Name;

                            await _holderDIDService.AddHolderDIDAsync(HolderDID);

                            return RedirectToPage("./Index");
                        }
                        else
                        {
                            ErrorMessage = "Could not validate DID and Private Key";
                        }
                    }
                    else
                    {
                        ErrorMessage = "The Private Key file was not uploaded";
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }

            return Page();
        }

        private bool ValidateDID(string DID, string rsaPrivateKey)
        {
            bool b = false;

            try
            {
                RSAJwk rsaPublicJwk = _didJWKService.GetPublicJwk<RSAJwk>(DID);
                string rsaPublicKey = _rsaOperator.GetPublicKey(rsaPublicJwk);

                Keys keys = new Keys()
                {
                    publicKey = rsaPublicKey,
                    privateKey = rsaPrivateKey,
                };

                Jwt jwt = _jwtOperator.Sign(_payload,keys, _rsaOperator);
                string jwtCompact = _jwtOperator.ToJwtCompact(jwt);
                b = _jwtOperator.Verify(jwtCompact, rsaPublicKey,_rsaOperator);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return b;
        }
    }
}