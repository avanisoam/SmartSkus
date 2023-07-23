using Blazorise.Extensions;
using Microsoft.AspNetCore.Mvc;
using SmartSkus.Api.Data.Interface;
using SmartSkus.Api.Models;
using System;
using System.Collections.Generic;

namespace SmartSkus.Api.Controllers.SkuData
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkuController : ControllerBase
    {
        private readonly ISkuRepo _repo;
        private readonly IInventoryRepo _repository;

        public SkuController(ISkuRepo repo, IInventoryRepo repository)
        {
            _repo = repo;
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<SkuModel>> GetAll()
        {
            var skuModels = _repo.GetAll();
            return Ok(skuModels);
        }

        [HttpGet("{id}", Name = "GetById")]
        public ActionResult<SkuModel> GetById(int id)
        {
            var skuModel = _repo.GetSkuById(id);

            return Ok(skuModel);
        }

        /// <summary>
        /// {
        ///         "itemName": "Shoes",
        ///         "attribute1": "US-44",
        ///         "attribute2": "Leather",
        ///         "attribute3": "Black"
        ///    }
        /// </summary>
        /// <param name="skuModel"></param>
        /// <returns></returns>

        [HttpPost]
        public ActionResult AddItemsToInventory(SkuModel skuModel)
        {
            if (skuModel is null)
            {
                throw new NullReferenceException();
            }

            if (skuModel != null && string.IsNullOrWhiteSpace(skuModel.ItemName))
            {
                return BadRequest("Please Add the items list properly");
            }

            // TODO: Check if Item already exits

            string newSku = "";

            if (!string.IsNullOrWhiteSpace(skuModel.ItemName) && skuModel.ItemName.Length >= 2)
            {
                newSku = skuModel.ItemName.Substring(0, 2);
            }

            if (!string.IsNullOrWhiteSpace(skuModel.Attribute1) && skuModel.Attribute1.Length >= 2)
            {
                newSku = string.Format("{0}-{1}", newSku, skuModel.Attribute1.Substring(0, 2));
            }

            if (!string.IsNullOrWhiteSpace(skuModel.Attribute2) && skuModel.Attribute2.Length >= 2)
            {
                newSku = string.Format("{0}-{1}", newSku, skuModel.Attribute2.Substring(0, 2));
            }

            if (!string.IsNullOrWhiteSpace(skuModel.Attribute3) && skuModel.Attribute3.Length >= 2)
            {
                newSku = string.Format("{0}-{1}", newSku, skuModel.Attribute3.Substring(0, 2));
            }

            //skuModel.GenerateSku = string.Concat(skuModel.ItemName.Substring(0, 2) + "-" + skuModel.Attribute1.Substring(0, 2) + "-" + skuModel.Attribute2.Substring(0, 2) + "-" + skuModel.Attribute3.Substring(0, 2));

            if(skuModel.GenerateSku.IsNullOrEmpty())
            {
                skuModel.GenerateSku = newSku;
            }

            bool skuExists = _repository.FindSku(skuModel.GenerateSku);

            if (skuExists)
            {
               skuModel =  _repository.UpdateInventory(skuModel.GenerateSku, (long)skuModel.Quantity);

                _repository.SaveChanges();

                return Ok(skuModel);
            }
            else 
            {
                _repository.AddInventory(skuModel.GenerateSku, skuModel.Description, (long)skuModel.Quantity);

                _repository.SaveChanges();

                var inventoryModel = _repository.GetVariationBySkuNumber(skuModel.GenerateSku);

                skuModel.Item = inventoryModel.Item;

                _repo.AddItemsToInventory(skuModel);
                _repo.SaveChanges();

                var newSkuModel = _repo.GetSkuById(skuModel.Id);

                return CreatedAtRoute(routeName: "GetById", routeValues: new { Id = newSkuModel.Id }, newSkuModel);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteItemFromInventory(int id)
        {
            var item = _repo.GetSkuById(id);

            if (item == null)
            {
                return NotFound("No such Item Exists");
            }

            _repo.DeleteItemFromInventory(item);
            _repo.SaveChanges();

            return NoContent();
        }

        [Route("getSkuArray")]
        [HttpGet]
        public ActionResult<SkuModel[]> GetSkuArray()
        {
            Array array = _repo.GetAllSkuArray();

            return Ok(array);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateItemInInventory(int id, SkuModel skuModel)
        {
            var item = _repo.GetSkuById(skuModel.Id);

            if (item == null && skuModel.Id != id)
            {
                return NotFound("No such Item Exists");
            }

            item.Id = skuModel.Id;
            item.Attribute1 = skuModel.Attribute1;
            item.Attribute2 = skuModel.Attribute2;
            item.Attribute3 = skuModel.Attribute3;
            item.GenerateSku = skuModel.GenerateSku;
            item.Description = skuModel.Description;
            item.Quantity = skuModel.Quantity;

            _repo.UpdateItemInInventory(item);
            _repo.SaveChanges();

            return NoContent();
        }
    }
}
