using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSkus.Shared.Dtos
{
    public class CategoryDto
    {
        public int CategoryID { get; set; }
        public string? Title { get; set; }

        public IList<CategoryOptionKeyDto> CategoryOptionKeys { get; set; } = new List<CategoryOptionKeyDto>();
    }
}
