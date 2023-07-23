using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartSkus.Api.Models
{
    public class Item
    {
        #region .ctor

        public Item()
        { }
        public Item(string itemCode, string description)
        {
            ItemCode = itemCode;
            Description = description;
        }

        public Item(long itemId , string itemCode, string name, string description, Region region, int categoryId)
        {
            ItemID = itemId;
            ItemCode = itemCode;
            Name = name;
            Description = description;
            Region = region;
            CategoryID = categoryId;
        }

        #endregion

        [Key]
        public long ItemID { get; set; }

        [Required]
        [MaxLength(12)]
        public string ItemCode { get; set; }

        [MaxLength(250)]
        public string Name { get; set; }

        public string Description { get; set; }

        public Region? Region { get; set; }

        public int CategoryID { get; set; } = 1; // Always set Default Category if not provided


        #region Navigation Properties

        public Category Category { get; set; }
        public ICollection<ItemVariation> ItemVariations { get; set; }

        #endregion
    }
}
