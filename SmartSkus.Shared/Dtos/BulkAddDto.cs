namespace SmartSkus.Shared.Dtos
{
    public class BulkAddDto
    {
        public string? SKU { get; set; }
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public List<long>? OptionKeyIds { get; set; }
        public int CategoryId { get; set; }

    }
}
