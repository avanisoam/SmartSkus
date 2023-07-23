using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartSkus.Api.Models
{
    public class ItemVariation
    {
        #region .ctor

        public ItemVariation()
        { }

        public ItemVariation(string skuNumber, string description, long quantity )
        {
            SKUNumber = skuNumber;
            Description = description;
            Quantity = quantity;
        }

        public ItemVariation(long itemVariationItem, string skuNumber, string description, double price, long quantity, long itemId)
        {
            ItemVariationID = itemVariationItem;
            SKUNumber = skuNumber;
            Description = description;
            Price = price;
            Quantity = quantity;
            ItemID = itemId;
        }

        #endregion

        [Key]
        [Required]
        public long ItemVariationID { get; set; }

        [MaxLength(100)]
        public string SKUNumber { get; set; }
        
        public string Description { get; set; }

        public double Price { get; set; }

        public long Quantity { get; set; }

        public long ItemID { get; set; }

        #region Navigation Properties
       
        public Item Item { get; set; }

        public ICollection<OptionVariations> OptionVariations { get; set; }

        #endregion
    }
}
