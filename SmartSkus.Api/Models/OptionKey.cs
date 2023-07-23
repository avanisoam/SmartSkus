using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartSkus.Api.Models
{
    public class OptionKey
    {
        #region .ctor
        public OptionKey()
        { }

        public OptionKey(long optionKeyId, string keyName)
        {
            OptionKeyID = optionKeyId;
            KeyName = keyName;
        }

        #endregion

        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        [Required]
        public long OptionKeyID { get; set; }

        [MaxLength(100)]
        public string KeyName { get; set; }

        #region Navigation Properties
        public ICollection<OptionValue> OptionValues { get; set; }

        //public ICollection<CategoryOptionKey> CategoryOptionKeys { get; set; }

        #endregion
    }
}
