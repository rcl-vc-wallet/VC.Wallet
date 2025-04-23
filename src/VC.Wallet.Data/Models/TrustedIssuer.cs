#nullable disable

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VC.Wallet.Data
{
    [Table(name: "vc_wallet_trusted_issuer")]
    public class TrustedIssuer
    {
        [Key]
        public int id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        [MaxLength(80)]
        public string name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Description")]
        [MaxLength(350)]
        public string description { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "DID")]
        public string did { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Admin Username")]
        [MaxLength(150)]
        public string adminUsername { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Country Code")]
        [MaxLength(10)]
        public string countryCode { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Region/State/Province")]
        [MaxLength(150)]
        public string region { get; set; }

        [DataType(DataType.Text)]
        [MaxLength(10)]
        public string type { get; set; }

        [DataType(DataType.Text)]
        public string privateKey { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Website URL")]
        [MaxLength(250)]
        public string websiteUrl { get; set; }
    }
}
