namespace ASPNETCoreApplication.Models
{
    public class AssetMaster
    {
        public int assetid { get; set; }
        public string? itemType { get; set; }

        public string? itemName { get; set; }

        public string? subItemName { get; set; }

        public string? model { get; set; }
        public string? serialno { get; set; }
        public string? brand { get; set; }
        public string? pono { get; set; }
        public DateOnly? warrantydate { get; set; }
        public bool isActive { get; set; } = true;

        public IFormFile? FileDetails { get; set; }
    }
}
