using SmartSkus.Api.Data.Interface;
using SmartSkus.Api.Models;
using System.Collections.Generic;
using System.Linq;

namespace SmartSkus.Api.Data.Repo
{
    public class SkuRepo : ISkuRepo
    {
        private readonly InventoryContext _context;

        public SkuRepo(InventoryContext context)
        {
            _context = context;
        }
        public void AddItemsToInventory(SkuModel skuModel)
        {
            _context.skuModels.Add(skuModel);
        }

        public void UpdateItemInInventory(SkuModel skuModel)
        {
            _context.skuModels.Update(skuModel);
        }

        public void DeleteItemFromInventory(SkuModel skuModel)
        {
            _context.skuModels.Remove(skuModel);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

        public IEnumerable<SkuModel> GetAll()
        {
            return _context.skuModels.ToList();
        }

        public SkuModel[] GetAllSkuArray()
        {
            return _context.skuModels.ToArray();
        }

        public SkuModel GetSkuById(int id)
        {
            return _context.skuModels.FirstOrDefault(s => s.Id == id);
        } 
    }
}
