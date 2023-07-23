using SmartSkus.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSkus.Core.Services.Contracts
{
    public interface IInventoryService
    {
        #region SkuModelDto CRUD

        Task<SkuModelDto> Create(SkuModelDto skuModelDto);

        Task<BulkAddDto> Add(BulkAddDto bulkAdddto);

        Task<SkuModelDto> Update(SkuModelDto skuModelDto);

        Task<SkuModelDto> Delete(int id);

        Task<IEnumerable<SkuModelDto>> GetAll();

        Task<SkuModelDto[]> GetAllSkuArray();

        Task<SkuModelDto> GetById(int id); 

        #endregion

        Task<IEnumerable<ItemDto>> GetAllItems();
        Task<IEnumerable<ItemVariationDto>> GetAllItemVariations();

        Task<bool> DeleteAll();
    }
}
