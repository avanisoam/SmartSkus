using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartSkus.Api.Models
{
    public class Category
    {
        #region .ctor
        
        public Category()
        { }

        public Category(int categoryId , string title)
        {
            CategoryID = categoryId;
            Title = title;
        }

        #endregion

        [Key]
        [Required]
        public int CategoryID { get; set; }
        public string Title { get; set; }

        #region Navigation Properties
        
        public ICollection<Item> Items { get; set; }

        public ICollection<CategoryOptionKey> CategoryOptionKeys { get; set; }
        public ICollection<OptionKey> OptionKeys { get; set; }

        #endregion
    }
}
