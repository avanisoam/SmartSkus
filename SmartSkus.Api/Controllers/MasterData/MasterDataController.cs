using Microsoft.AspNetCore.Mvc;
using SmartSkus.Api.Data.Interface;
using SmartSkus.Api.Models;
using SmartSkus.Shared.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace SmartSkus.Api.Controllers.MasterData
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MasterDataController : ControllerBase
    {
        private readonly IMasterDataRepo _repository;

        public MasterDataController(IMasterDataRepo repository)
        {
            _repository = repository;
        }

        [ActionName("categories")]
        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetAllCategory()
        {
            var categoryList = _repository.GetAllCategory();
            return Ok(categoryList);
        }

        [ActionName("categories")]
        [HttpGet("{title}")]
        public ActionResult<Category> GetSingleCategory(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return BadRequest("Invalid Arguments");
            }

            var category = _repository.GetSingleCategory(title);

            if (category != null)
            {
                return Ok(category);
            }

            return NotFound("Category Not Found");
        }

        [ActionName("categories/add")]
        [HttpPost]
        public ActionResult AddCategory(Category category)
        {
            if (category.Title == null)
            {
                return BadRequest("Invalid Arguments");
            }

            _repository.AddCategory(category);
            _repository.SaveChanges();

            return Created($"api/masterdata/categories/{category.Title}", category);
        }

        [ActionName("options")]
        [HttpGet]
        public ActionResult<OptionKey> GetAllOptionKeys()
        {
            var optionKeyItems = _repository.GetAllOptionKeys();
            return Ok(optionKeyItems);
        }

        [ActionName("options")]
        [HttpGet("{title}")]
        public ActionResult<OptionKey> GetSingleOptionKey(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return NotFound("Invalid Argument");
            }

            var key = _repository.GetSingleOptionKey(title);

            if (key != null)
            {
                return Ok(key);
            }

            return NotFound();
        }

        /// <summary>
        /// To Add OptionKey and Option Values
        /// 
        /// <param name="optionKey"></param>
        /// 
        /// {
        ///  "optionKeyID": 800,
        ///  "keyName": "abc",
        ///  "optionValues": [
        ///    {
        ///      "value": "s1"
        ///    },
        ///    {
        ///      "value": "s2"
        ///    }
        ///  ]
        /// }
        /// 
        /// </summary>
        [ActionName("options/add")]
        [HttpPost]
        public ActionResult AddOptionKeys(OptionKey optionKey)
        {
            if (optionKey.KeyName == null)
            {
                return BadRequest("Invalid Arguments");
            }

            _repository.AddOptionKeys(optionKey);
            _repository.SaveChanges();

            return Created($"api/masterdata/options/add/{optionKey.KeyName}", optionKey);
        }

        [HttpGet("{id}", Name = "GetCategoryById")]
        public ActionResult<Category> GetCategoryById(int id)
        {
            var category = _repository.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCategory(int id)
        {
            var category = _repository.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }

            _repository.DeleteCategory(category);
            _repository.SaveChanges();
            return NoContent();
        }

        [HttpPost]
        public ActionResult AddOptionValues(OptionValue optionValue)
        {
            if (optionValue == null)
            {
                return BadRequest();
            }
            _repository.AddOptionValues(optionValue);
            _repository.SaveChanges();

            return NoContent();
        }

        [HttpGet]
        public ActionResult<IEnumerable<OptionValue>> GetOptionValues()
        {
            var values = _repository.GetAllOptionValues();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public ActionResult<OptionValue> GetOptionValueById(long id)
        {
            var value = _repository.GetOptionValueById(id);
            return Ok(value);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteOptionValue(long id)
        {
            var optionValue = _repository.GetOptionValueById(id);
            if (optionValue == null)
            {
                return NotFound();
            }
            _repository.DeleteOptionValue(optionValue);
            _repository.SaveChanges();
            return NoContent();
        }

        [HttpGet("{id}")]
        public ActionResult<OptionKey> GetOptionKeyById(long id)
        {
            var key = _repository.GetOptionKeyById(id);
            return Ok(key);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteOptionKeyWithValues(long id)
        {
            var optionKey = _repository.GetOptionKeyById(id);
            if(optionKey == null)
            {
                return NotFound();
            }
            _repository.DeleteOptionKey(optionKey);
            _repository.SaveChanges();

            return NoContent();
        }

        [HttpGet]
        public ActionResult DownloadCategory(int isdownload)
        {
            var categoryList = _repository.GetAllCategory();

            //byte[] objAsBytes = categoryList.getBytes();

            //if (isdownload == 1)
            //    return File(()categoryList, "application/json");

            return Ok(categoryList);
        }

        [HttpGet]
        public ActionResult<IEnumerable<CategoryOptionKey>> GetAllCategoryWithKeys()
        {
            var options = _repository.GetAllCategoryWithKeys();

            return Ok(options);

        }

        [HttpGet("{id}")]
        public ActionResult<CategoryOptionKey> GetByCategoryId(int id)
        {
            var categoryOption = _repository.GetByCategoryId(id);
            if(categoryOption.Count() == 0)
            {
                return NotFound();
            }

            return Ok(categoryOption);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateOptionKeysList(int id , List<long> OptionKeyIds)
        {
            // GetByCategoryId
            var item = _repository.GetByCategoryId(id);
            // Iterate on OptionKeys in above category
            foreach(var optionKey in item)
            {
                long obj = OptionKeyIds.Where(x => x.Equals(optionKey.OptionKeyID)).FirstOrDefault();

                if (obj > 0)
                {
                    OptionKeyIds.Remove((long)obj);
                }
                else
                {
                    _repository.DeleteCategoryOptionKey(optionKey);
                }
            }

            foreach(var i in OptionKeyIds)
            {
                var categoryOptionKey = new CategoryOptionKey { CategoryID = id, OptionKeyID = i };
                //item.Add(categoryOptionKey);
                _repository.AddCategoryOptionKey(categoryOptionKey);
            }

            _repository.SaveChanges();

            // If option key exists in optionKeyIds received in api then delete from OptionkeyIds
            // If Option key does not exists the delete mapping from CategoryById
            // Iterate All remaining option key ids and map it with category

            return NoContent();
        }

       
        [HttpPut("{id}")]
        public ActionResult UpdateCategory(int id, Category category)
        {
            var item = _repository.GetCategoryById(id);
            
            if(item == null)
            {
                return NotFound();
            }

            item.Title = category.Title;
            _repository.UpdateCategory(item);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}
