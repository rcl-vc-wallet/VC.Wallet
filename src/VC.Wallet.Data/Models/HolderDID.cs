#nullable disable

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VC.Wallet.Data
{
    [Table(name: "vc_wallet_holder_did")]
    public class HolderDID
    {
        [Key]
        public int id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Holder Username")]
        [MaxLength(150)]
        public string holderUsername { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "DID")]
        public string did { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Sorting Code")]
        [MaxLength(10)]
        public string sortingCode { get; set; }
    }
}