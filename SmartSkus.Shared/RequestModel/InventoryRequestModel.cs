namespace SmartSkus.Shared.RequestModel
{
    public class InventoryRequestModel
    {
        public string? SKU { get; set; }

        public string? Description { get; set; }

        public int Quantity { get; set; }
    }
}
