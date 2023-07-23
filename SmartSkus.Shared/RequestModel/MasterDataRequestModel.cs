namespace SmartSkus.Shared.RequestModel
{
    public class MasterDataRequestModel : InventoryRequestModel
    {
        public List<int>? OptionKeyIds { get; set; }
        public int CategoryId { get; set; }
    }
}
