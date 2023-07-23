using System.Collections.Generic;

namespace SmartSkus.Api.Models
{
    public class SkuModel
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public string Attribute1 { get; set; }
        public string Attribute2 { get; set; }
        public string Attribute3 { get; set; }

        public string GenerateSku { get; set; } = string.Empty;

        public long ItemID { get; set; }

        public string? Description { get; set; }
        public long Quantity { get; set; }

        #region Navigation Properties

        public Item Item { get; set; }

        #endregion
    }
}
