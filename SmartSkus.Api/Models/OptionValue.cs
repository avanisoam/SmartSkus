using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartSkus.Api.Models
{
    public class OptionValue
    {
        #region .ctor

        public OptionValue()
        { }

        public OptionValue(long optionValueId, string value, long optionKeyId)
        {
            OptionValueID = optionValueId;
            Value = value;
            OptionKeyID = optionKeyId; 
        }

        #endregion

        [Key]
        [Required]
        public long OptionValueID { get; set; }

        [MaxLength(100)]
        public string Value { get; set; }

        public long OptionKeyID { get; set; }
        
        #region Navigation Properties
       
        public OptionKey OptionKey { get; set; }

        public ICollection<OptionVariations> OptionVariations { get; set; }

        #endregion
    }
}
