using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSkus.Shared.Dtos
{
    public class CategoryOptionKeyDto
    {
        public int CategoryOptionKeyId { get; set; }
        public int CategoryID { get; set; }
        public long OptionKeyID { get; set; }

        public virtual CategoryDto Category { get; set; } = new CategoryDto();

        public virtual OptionKeyDto OptionKey { get; set; } = new OptionKeyDto();
    }
}
