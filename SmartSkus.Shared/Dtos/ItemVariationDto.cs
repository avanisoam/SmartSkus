namespace SmartSkus.Shared.Dtos
{
    public class ItemVariationDto
    {
        public long ItemVariationID { get; set; }

        public string? SKUNumber { get; set; }

        public string? Description { get; set; }

        public double Price { get; set; }

        public long Quantity { get; set; }

        public long ItemID { get; set; }
    }
}
