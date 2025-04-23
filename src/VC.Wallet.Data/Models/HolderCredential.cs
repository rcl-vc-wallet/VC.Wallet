#nullable disable

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VC.Wallet.Data
{
    [Table(name: "vc_wallet_holder_credential")]
    public class HolderCredential
    {
        [Key]
        public int id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Credential Id")]
        [MaxLength(350)]
        public string credentialId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Holder Username")]
        [MaxLength(150)]
        public string holderUsername { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Credential Name")]
        [MaxLength(150)]
        public string credentialName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "File Data")]
        public string file { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "file Type")]
        [MaxLength(10)]
        public string fileType { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Sorting Code")]
        [MaxLength(10)]
        public string sortingCode { get; set; }

        [Required]
        [Display(Name = "Credential Group Id")]
        public int groupId { get; set; }

    }
}
