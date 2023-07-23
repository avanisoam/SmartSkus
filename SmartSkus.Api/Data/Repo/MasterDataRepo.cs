using Microsoft.EntityFrameworkCore;
using SmartSkus.Api.Data.Interface;
using SmartSkus.Api.Models;
using SmartSkus.Shared;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SmartSkus.Api.Data.Repo
{
    public class MasterDataRepo : IMasterDataRepo
    {
        private readonly InventoryContext _context;

        public MasterDataRepo(InventoryContext context)
        {
            _context = context;
        }

        #region Category CRUD

        public void AddCategory(Category category)
        {
            if (category.Title == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            _context.Categories.Add(category);

            _context.SaveChanges();
        }

        public void UpdateCategory(Category category)
        {
            _context.Categories.Update(category);
        }

        public void DeleteCategory(Category category)
        {
            _context.Categories.Remove(category);
        }

        public IEnumerable<Category> GetAllCategory()
        {
            return _context.Categories.ToList();
        }

        public IEnumerable<Category> DownloadCategory()
        {
            var response = _context.Categories.ToList();

            StreamWriter sw = new("DownloadCategory.txt");

            sw.Write(response);
            sw.Close();


            return response;
        }

        public Category GetCategoryById(int id)
        {
            return _context.Categories
                           .Include(o => o.CategoryOptionKeys)
                           .FirstOrDefault(x => x.CategoryID == id);
        }

        public Category GetSingleCategory(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentNullException(nameof(title));
            }

            var findCategory = (from c in _context.Categories
                                .Include(i => i.Items)
                                .Include(o => o.CategoryOptionKeys)
                                where c.Title == title
                                select c).FirstOrDefault();

            return findCategory;
        }

        #endregion

        #region OptionKey CRUD

        public void AddOptionKeys(OptionKey optionKey)
        {
            _context.OptionKeys.Add(optionKey);

            //_context.SaveChanges();
        }

        public void DeleteOptionKey(OptionKey optionKey)
        {
            foreach (var item in optionKey.OptionValues.Where(x => x.OptionKeyID == optionKey.OptionKeyID))
            {
                _context.OptionValues.Remove(item);
            }
            _context.OptionKeys.Remove(optionKey);
        }

        public IEnumerable<OptionKey> GetAllOptionKeys()
        {
            var list = _context.OptionKeys
                .Include(o => o.OptionValues)
                .ToList();

            return list;
        }

        public OptionKey GetSingleOptionKey(string keyName)
        {
            if (string.IsNullOrWhiteSpace(keyName))
            {
                throw new ArgumentNullException(nameof(keyName));
            }

            var findKey = (from k in _context.OptionKeys
                           .Include(o => o.OptionValues)
                           where k.KeyName == keyName
                           select k).FirstOrDefault();

            return findKey;
        }

        public OptionKey GetOptionKeyById(long id)
        {
            return _context.OptionKeys
                           .Include(v => v.OptionValues)
                           .FirstOrDefault(x => x.OptionKeyID == id);
        }

        #endregion

        #region OptionValue CRUD

        public void AddOptionValues(OptionValue optionValue)
        {
            _context.OptionValues.Add(optionValue);
        }

        public void DeleteOptionValue(OptionValue optionValue)
        {
            _context.OptionValues.Remove(optionValue);
        }

        public IEnumerable<OptionValue> GetAllOptionValues()
        {
            return _context.OptionValues.ToList();
        }

        public OptionValue GetOptionValueById(long id)
        {
            return _context.OptionValues.FirstOrDefault(x => x.OptionValueID == id);
        }

        #endregion

        #region CategoryOptionKey CRUD

        public void AddCategoryOptionKey(CategoryOptionKey categoryOptionKey)
        {
            _context.CategoryOptionKeys.Add(categoryOptionKey);
        }

        public void UpdateOptionKeysList(CategoryOptionKey categoryOptionKey)
        {
            _context.CategoryOptionKeys.Update(categoryOptionKey);  //Not yet used
        }

        public void DeleteCategoryOptionKey(CategoryOptionKey categoryOptionKey)
        {
            _context.CategoryOptionKeys.Remove(categoryOptionKey);
        }

        public IEnumerable<CategoryOptionKey> GetAllCategoryWithKeys()
        {
            return _context.CategoryOptionKeys
                           .Include(x => x.Category)
                           .Include(y => y.OptionKey)
                           .ToList();
        }

        public IEnumerable<CategoryOptionKey> GetByCategoryId(int id)
        {
            return _context.CategoryOptionKeys
                           .Include(x => x.OptionKey)
                           .Where(x => x.CategoryID == id)
                           .ToList();
        }

        #endregion

        public bool SaveChanges()
        {
            return _context.SaveChanges() < 0;
        }
    }
}
