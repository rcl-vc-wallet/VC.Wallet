#nullable disable

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;
using VC.Wallet.Core;
using VC.Wallet.Core.Credential;

namespace VC.Wallet.WebApp.Areas.HolderCredential.Pages
{
    [Authorize]
    public class AddModel : PageModel
    {
        private readonly IHolderCredentialService _holderCredentialService;
        private readonly IHolderCredentialGroupService _holderCredentialGroupService;
        private readonly IImageFactory _imageFactory;
        private readonly ICryptoAlgorithmFactory _cryptoAlgorithmFactory;
        private readonly IJwtOperator _jwtOperator;

        public string ErrorMessage { get; set; } = string.Empty;

        [BindProperty]
        public Data.HolderCredential HolderCredential { get; set; } = new Data.HolderCredential();

        public AddModel(IHolderCredentialService holderCredentialService,
            IHolderCredentialGroupService holderCredentialGroupService,
            IImageFactory imageFactory,
            ICryptoAlgorithmFactory cryptoAlgorithmFactory,
            IJwtOperator jwtOperator)
        {
            _holderCredentialService = holderCredentialService;
            _holderCredentialGroupService = holderCredentialGroupService;
            _imageFactory = imageFactory;
            _cryptoAlgorithmFactory = cryptoAlgorithmFactory;
            _jwtOperator = jwtOperator;
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
                    if (fileUpload?.Length > 0)
                    {
                        string jwtCompact = await GetJwtCompactFromFile(fileUpload);

                        if (!string.IsNullOrEmpty(jwtCompact))
                        {
                            bool b = await ValidateJwtCompact(jwtCompact);

                            if (b == true)
                            {
                                GetCredentialData(jwtCompact);

                                // TODO - verify credential subject id is a DID owned by the holder

                                var newCredential = await _holderCredentialService.CreateHolderCredentialAsync(HolderCredential);

                                if (!string.IsNullOrEmpty(newCredential?.credentialName))
                                {
                                    return RedirectToPage("./index");
                                }
                                else
                                {
                                    ErrorMessage = "Could not save credential";
                                }
                            }
                            else
                            {
                                ErrorMessage = "Validation failed";
                            }
                        }
                        else
                        {
                            ErrorMessage = "Could not read jwt from file";
                        }
                    }
                    else
                    {
                        ErrorMessage = "No file uploaded";
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }

            return Page();
        }

        public async Task<JsonResult> OnGetCredentialGroups()
        {
            List<Data.HolderCredentialGroup> _credentialGroups = new List<Data.HolderCredentialGroup>();

            try
            {
                _credentialGroups = await _holderCredentialGroupService.GetHolderCredentialGroupByUsernameAsync(User?.Identity?.Name);

                return new JsonResult(_credentialGroups);
            }
            catch (Exception)
            {
                return new JsonResult(_credentialGroups);
            }
        }

        public async Task<JsonResult> OnGetAddNewCredentialGroup(string groupName, string sortingCode)
        {
            Data.HolderCredentialGroup _credentialGroup = new Data.HolderCredentialGroup();

            try
            {
                Data.HolderCredentialGroup newGroup = new Data.HolderCredentialGroup
                {
                    groupName = groupName,
                    sortingCode = sortingCode,
                    holderUsername = User?.Identity?.Name
                };

                _credentialGroup = await _holderCredentialGroupService.CreateHolderCredentialGroupAsync(newGroup);

                return new JsonResult(_credentialGroup);
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                return new JsonResult(_credentialGroup);
            }
        }

        private async Task<string> GetJwtCompactFromFile(IFormFile fileUpload)
        {
            string jwtCompact = string.Empty;

            try
            {
                string ext = string.Empty;
                int fileExtPos = fileUpload.FileName.LastIndexOf(".", StringComparison.Ordinal);
                if (fileExtPos >= 0)
                {
                    ext = fileUpload.FileName.Substring(fileExtPos, fileUpload.FileName.Length - fileExtPos).ToLower();
                }

                if (ext?.ToLower() == ".png")
                {
                    HolderCredential.fileType = "png";
                    IImageService pngService = _imageFactory.Create(ImageType.PNG);
                    using (var ms = new MemoryStream())
                    {
                        fileUpload.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        string base64String = Convert.ToBase64String(fileBytes);
                        HolderCredential.file = $"data:image/png;base64,{base64String}";
                        jwtCompact = pngService.ReadEmbeddedTextFromImageMetaData(fileBytes, "openbadgecredential");
                    }
                }
                else if (ext?.ToLower() == ".txt")
                {
                    HolderCredential.fileType = "txt";
                    var result = new StringBuilder();
                    using (var reader = new StreamReader(fileUpload.OpenReadStream()))
                    {
                        while (reader.Peek() >= 0)
                        {
                            result.AppendLine(await reader.ReadLineAsync());
                        }
                    }
                    jwtCompact = result.ToString();
                    HolderCredential.file = jwtCompact;
                }
                else
                {
                    throw new Exception("Invalid file type. Only .png and .txt allowed");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not get Jwt from file {ex.Message}");
            }

            return jwtCompact;
        }

        private async Task<bool> ValidateJwtCompact(string jwtCompact)
        {
            bool b = false;

            try
            {
                JwkBase jwkBase = _jwtOperator.GetPublicJwkBase(jwtCompact);
                string algo = jwkBase.alg.ToLower();

                ValidationBuilder validationBuilder = new ValidationBuilder(jwtCompact);

                if (algo == "rs256")
                {
                    var _cryptoAlgorithm = _cryptoAlgorithmFactory.Create<RSAJwk>();

                    CheckForValidSignature<RSAJwk> _checkForValidSignature =
                        new CheckForValidSignature<RSAJwk>(_jwtOperator, _cryptoAlgorithm);

                    validationBuilder.Add(_checkForValidSignature);
                }
                else
                {
                    throw new Exception("Only RS256 is supported as this time");
                }

                validationBuilder.Add(new CheckForRequiredData(_jwtOperator));
                ValidationResponse validationResponse = await validationBuilder.ValidateAsync();

                if (validationResponse?.isValid == true)
                {
                    b = true;
                }
                else
                {
                    throw new Exception($"Validation failed {validationResponse?.errorMessage}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not validate jwt {ex.Message}");
            }

            return b;
        }

        private void GetCredentialData(string jwtCompact)
        {
            try
            {
                Jwt jwt = _jwtOperator.JwtFromJwtCompact(jwtCompact);
                JwtDecoded jwtDecoded = _jwtOperator.DecodeJwt(jwt);
                AchievementCredential credential = JsonSerializer.Deserialize<AchievementCredential>(jwtDecoded.decodedPayload);

                HolderCredential.credentialName = credential.credentialSubject.achievement.name;
                HolderCredential.credentialId = credential.id;
                HolderCredential.holderUsername = User.Identity.Name;

            }
            catch (Exception ex)
            {
                throw new Exception($"Could not get credential data from jwt {ex.Message}");
            }
        }
    }
}
