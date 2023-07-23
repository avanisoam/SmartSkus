using System.ComponentModel.DataAnnotations;

namespace SmartSkus.Api.Models
{
    public class OptionVariations
    {
        #region .ctor

        public OptionVariations()
        { }

        public OptionVariations(long optionVariationId, long itemVariationId, long optionValueId)
        {
            OptionVariationID = optionVariationId;
            ItemVariationID = itemVariationId;
            OptionValueID = optionValueId;
        }

        #endregion

        [Key]
        [Required]
        public long OptionVariationID { get; set; }

        public long ItemVariationID { get; set; }
        
        public long OptionValueID { get; set; }

        #region Navigation Properties

        public ItemVariation ItemVariation { get; set; }

        public OptionValue OptionValue { get; set; }

        #endregion
    }
}
