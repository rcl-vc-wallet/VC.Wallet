#nullable disable

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VC.Wallet.Data
{
    [Table(name: "vc_wallet_holder")]
    public class Holder
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
        [Display(Name = "Username/Email")]
        [MaxLength(150)]
        public string username { get; set; }

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

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date Created")]
        public DateTime dateCreated { get; set; }
    }
}
