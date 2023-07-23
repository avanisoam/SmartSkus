using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSkus.Shared.Dtos
{
    public class OptionKeyAndValueDto
    {
        public long OptionKeyID { get; set; }

        public string? KeyName { get; set; }

        public List<OptionValueDto>? OptionValues { get; set; }
    }
}
