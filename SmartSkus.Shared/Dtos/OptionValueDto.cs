using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSkus.Shared.Dtos
{
    public class OptionValueDto
    {
        public long OptionValueID { get; set; }
        public string? Value { get; set; }

        public long OptionKeyID { get; set; }

        public bool Selected { get; set; }
    }
}
