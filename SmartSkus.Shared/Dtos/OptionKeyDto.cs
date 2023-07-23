using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSkus.Shared.Dtos
{
    public class OptionKeyDto
    {
        public long OptionKeyID { get; set; }

        public string? KeyName { get; set; }

        public bool ShowOptionValues { get; set; } = false;

        public IList<OptionValueDto>? OptionValues { get; set; }
    }
}
