#nullable disable

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VC.Wallet.Core;
using System.Text;

namespace VC.Wallet.WebApp.Areas.HolderDID.Pages
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly IHolderDIDService _holderDIDService;
        private readonly ICryptoAlgorithmFactory _algorithmFactory;
        private readonly ICryptoAlgorithm<RSAJwk> _rsaOperator;
        private readonly IDIDJwkService _didJWKService;

        [BindProperty]
        public DIDViewModel DIDViewModel { get; set; } = new DIDViewModel();
        public string ErrorMessage { get; set; } = string.Empty;

        public CreateModel(IHolderDIDService holderDIDService, 
            ICryptoAlgorithmFactory algorithmFactory,
            IDIDJwkService didJWKService)
        {
            _holderDIDService = holderDIDService;
            _didJWKService = didJWKService;
            _algorithmFactory = algorithmFactory;
            _rsaOperator = _algorithmFactory.Create<RSAJwk>();
        }

        public void OnGet()
        {
        }

        public IActionResult OnPostAsync()
        {
            try
            {
                if(string.IsNullOrEmpty(DIDViewModel?.PrivateKey))
                {
                    Keys keys = _rsaOperator.GenerateKeys();
                    DIDViewModel.PrivateKey = keys.privateKey;
                    RSAJwk publicJwk = _rsaOperator.GetPublicJwk(keys.publicKey);
                    string did = _didJWKService.CreateDID(publicJwk);
                    DIDViewModel.DID = did;

                    return Page();
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return Page();
            }

            return Page();
        }

        public IActionResult OnPostDownloadPrivateKey()
        {
            try
            {
                if (!string.IsNullOrEmpty(DIDViewModel?.PrivateKey))
                {
                    Random generator = new Random();
                    string fileName = $"PK{generator.Next(1000000, 9999999)}.txt";
                    byte[] content = Encoding.UTF8.GetBytes(DIDViewModel.PrivateKey);

                    return File(content, "application/octet-stream", fileName);
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }

            return Page();
        }
    }

    public class DIDViewModel
    {
        public string DID { get; set; }
        public string PrivateKey { get; set; }
    }
}
