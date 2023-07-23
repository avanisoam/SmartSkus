using SmartSkus.Api.Models;
using System.Collections.Generic;

namespace SmartSkus.Api.Data.Interface
{
    /// <summary>
    /// SkuModel CRUD
    /// </summary>
    public interface ISkuRepo
    {
        void AddItemsToInventory(SkuModel skuModel);

        void UpdateItemInInventory(SkuModel skuModel);

        void DeleteItemFromInventory(SkuModel skuModel);

        bool SaveChanges();


        IEnumerable<SkuModel> GetAll();

        SkuModel[] GetAllSkuArray();

        SkuModel GetSkuById(int id);
    }
}
