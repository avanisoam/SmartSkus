using SmartSkus.Shared.Enums;

namespace SmartSkus.Shared.Dtos
{    
    public class ItemDto
    {
        public long ItemID { get; set; }

        public string? ItemCode { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public Region? Region { get; set; }

        public int CategoryID { get; set; } = 1;
    }
}
