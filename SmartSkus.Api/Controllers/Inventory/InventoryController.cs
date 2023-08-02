using Microsoft.AspNetCore.Mvc;
using SmartSkus.Api.Data.Interface;
using SmartSkus.Api.Models;
using SmartSkus.Shared.RequestModel;
using System;
using System.Collections.Generic;

namespace SmartSkus.Api.Controllers.Inventory
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryRepo _repository;

        public InventoryController(IInventoryRepo repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public ActionResult Add(InventoryRequestModel inventory)
        {
            if (string.IsNullOrWhiteSpace(inventory.SKU))
            {
                return BadRequest("SKU is required parameter");
            }

            if (inventory.Quantity <= 0)
            {
                return BadRequest("Quantity is required parameter");
            }

            _repository.AddInventory(inventory.SKU, inventory.Description, inventory.Quantity);
            _repository.SaveChanges();

            var inventoryModel = _repository.GetVariationBySkuNumber(inventory.SKU);

            return Created($"api/inventory/items/{inventory.SKU}", inventoryModel);
        }

        /// <summary>
        /// To create bulk skus based on option keys
        /// </summary>
        /// <param name="inventory"></param>
        /// 
        /// {
        /// "sku": "sku123",
        /// "description": "Adding Sku",
        /// "quantity": 10,
        /// "optionKeyIds": [
        ///  200,300
        /// ],
        /// "categoryId": 2
        /// }
        /// 
        [ActionName("skus/bulk-add")]
        [HttpPost]
        public ActionResult AddBulkSkus(MasterDataRequestModel inventory)
        {
            _repository.AddBulkSkus(inventory.SKU, inventory.OptionKeyIds, inventory.CategoryId,inventory.Description);
            _repository.SaveChanges();

            return Ok();
        }

        [HttpPatch("{skuNumber}/{quantity}")]
        public ActionResult Delete(string skuNumber, long quantity)
        {
            var variationModel = _repository.GetVariationBySkuNumber(skuNumber);

            if(variationModel == null)
            {
                return NotFound();
            }
            _repository.RemoveQuantity(skuNumber, quantity);
            _repository.SaveChanges();

            return NoContent();
        }

        [ActionName("items")]
        [HttpGet("{skuNumber}")]
        public ActionResult<ItemVariation> Get(string skuNumber)
        {
            var variationItem = _repository.GetVariationBySkuNumber(skuNumber);
            
            if (variationItem != null)
            {
                return Ok(variationItem); 
            }
           
            return NotFound();
        }

        [ActionName("items")]
        [HttpGet]
        public ActionResult<IEnumerable<Item>> GetAll()
        {
            var items = _repository.GetAllItems();
            return Ok(items);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Object>> GetVariationsOfAllItems()
        {
            var items = _repository.GetVariationsOfItems();
            return Ok(items);
        }

        [HttpDelete]
        public ActionResult DeleteAll()
        {
            _repository.DeleteAll();
            _repository.SaveChanges();

            return NoContent();
        }
    }
}
