using SmartSkus.Shared.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartSkus.Core.Services.Contracts
{
    public interface IMasterService
    {
        #region Category CRUD

        Task<CategoryDto> AddCategory(CategoryDto categoryDto);
        Task<CategoryDto> UpdateCategory(CategoryDto categoryDto);
        Task<CategoryDto> DeleteCategory(long id);
        Task<IEnumerable<CategoryDto>> GetAllCategory();
        Task<CategoryDto> GetCategoryById(int id);

        #endregion

        #region OptionKey CRUD

        Task<OptionKeyDto> AddOptionKeys(OptionKeyDto optionKeyDto);
        Task<OptionKeyAndValueDto> DeleteOptionKeysWithValues(long id);
        Task<IEnumerable<OptionKeyDto>> GetAllOptionKeys();

        #endregion

        #region OptionValue CRUD

        Task<OptionValueDto> AddOptionValues(OptionValueDto optionValueDto);
        Task<OptionValueDto> DeleteOptionValues(long id);
        Task<IEnumerable<OptionValueDto>> GetAllOptionValue();

        #endregion

        #region CategoryOptionKey CRUD

        Task UpdateCategoryOptionKey(int categoryId, List<long> OptionKeyIds);
        Task<IEnumerable<CategoryOptionKeyDto>> GetAllCategoryOptionKey();
        Task<IEnumerable<CategoryOptionKeyDto>> GetByCategoryId(int id);

        #endregion
    }
}
