namespace SmartSkus.Shared.Dtos
{
    public class SkuModelDto
    {
        public int Id { get; set; }
        public string? ItemName { get; set; }
        public string? Attribute1 { get; set; }
        public string? Attribute2 { get; set; }
        public string? Attribute3 { get; set; }

        public string? GenerateSku { get; set; }

        public long ItemID { get; set; }

        public string? Description { get; set; } = "";
        public long? Quantity { get; set; } = 0;
    }
}
