using System.ComponentModel.DataAnnotations;

namespace SmartSkus.Api.Models
{
    public class CategoryOptionKey
    {
        [Key]
        public int CategoryOptionKeyId { get; set; }
        public int CategoryID { get; set; }
        public long OptionKeyID { get; set; }

        #region Navigation Properties

        public virtual Category Category { get; set; }

        public virtual OptionKey OptionKey { get; set; }

        #endregion
    }
}
