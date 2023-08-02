using SmartSkus.Api.Models;
using System.Collections.Generic;

namespace SmartSkus.Api.Data.Interface
{
    /// <summary>
    /// Interface for Invertory repository
    /// </summary>
    public interface IInventoryRepo
    {
        void AddInventory(string sku, string description, long quantity);

        SkuModel UpdateInventory(string sku, long quantity);

        /// <summary>
        ///  Add Bulk SKUs based on Item code & Options
        /// </summary>
        void AddBulkSkus(string sku, List<int> optionKeyIds, int categoryId,string description);


        void RemoveQuantity(string skuNumber, long quantity);

        bool SaveChanges();

        ItemVariation GetVariationBySkuNumber(string skuNumber);

        SkuModel GetSkuModelBySkuNumber(string skuNumber);

        IEnumerable<Item> GetAllItems();

        IEnumerable<object> GetVariationsOfItems();

        bool FindSku(string skuNumber);

        void DeleteAll();
    }
}
