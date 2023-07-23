using SmartSkus.Api.Models;
using System.Collections.Generic;

namespace SmartSkus.Api.Data.Interface
{
    /// <summary>
    /// Interface for MasterData repository
    /// </summary>
    public interface IMasterDataRepo
    {        
        #region Category CRUD

        void AddCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);

        // GET Category Single or All
        // ADD Category
        IEnumerable<Category> GetAllCategory();

        IEnumerable<Category> DownloadCategory();

        Category GetCategoryById(int id);
        Category GetSingleCategory(string title);

        #endregion

        #region OptionKey CRUD

        void AddOptionKeys(OptionKey optionKey);

        void DeleteOptionKey(OptionKey optionKey);

        // GET optionKey Single or All
        // ADD optionKey
        IEnumerable<OptionKey> GetAllOptionKeys();

        OptionKey GetSingleOptionKey(string keyName);
        OptionKey GetOptionKeyById(long id);

        #endregion

        #region OptionValue CRUD

        void AddOptionValues(OptionValue optionValue);

        void DeleteOptionValue(OptionValue optionValue);

        /// <summary>
        ///  Add Option Values based on OptionKey Name
        /// </summary>

        IEnumerable<OptionValue> GetAllOptionValues();

        OptionValue GetOptionValueById(long id);

        #endregion

        #region CategoryOptionKey CRUD

        void AddCategoryOptionKey(CategoryOptionKey categoryOptionKey);

        void UpdateOptionKeysList(CategoryOptionKey categoryOptionKey);

        void DeleteCategoryOptionKey(CategoryOptionKey categoryOptionKey);

        /// <summary>
        ///  CategoryOptinKey
        /// </summary>

        IEnumerable<CategoryOptionKey> GetAllCategoryWithKeys();
        IEnumerable<CategoryOptionKey> GetByCategoryId(int id); 

        #endregion

        bool SaveChanges();
    }
}
