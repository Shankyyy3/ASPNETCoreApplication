using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ASPNETCoreApplication.Models
{
    public class DataViewModel
    {
        public string itemType { get; set; }
        [Required]
        [DisplayName("Item Type")]
        public string itemName { get; set; }
        [Required]
        public string subItemName { get; set; }
        [Required]
        public string model { get; set; }
        [Required]
        public string serialno { get; set; }
        [Required]
        public string brand { get; set; }
        [Required]
        public string pono { get; set; }
        [Required]
        public DateOnly warrantydate { get; set; }
    }
}
