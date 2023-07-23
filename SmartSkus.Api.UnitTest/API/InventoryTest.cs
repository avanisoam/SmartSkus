using Castle.Core.Configuration;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartSkus.Api.Controllers.Inventory;
using SmartSkus.Api.Data;
using SmartSkus.Api.Data.Interface;
using SmartSkus.Api.Data.Repo;
using SmartSkus.Api.Models;
using SmartSkus.Shared.RequestModel;
using System.Collections.Generic;

namespace MoversCandidateTest.UnitTest.API
{
    [TestClass]
    public class InventoryTest : TestBase
    {
        private readonly DbContextOptions<InventoryContext> dbContextOptions =
           new DbContextOptionsBuilder<InventoryContext>()
           .UseInMemoryDatabase(databaseName: "InventoryTest_InventoryDb")
           .Options;

        readonly InventoryController _controller;
       
        public InventoryTest()
        {
            var context = new InventoryContext(dbContextOptions);
            IInventoryRepo repo = new InventoryRepo(context);
            _controller = new InventoryController(repo);
           
            SeedDb(dbContextOptions);
        }

        [TestMethod]
        public void GetAllInventoryTest()
        {
            //Arrange

            //Act
            var result = _controller.GetAll();

            //Assert           
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));

            var list = result.Result as OkObjectResult;
            Assert.IsInstanceOfType(list.Value, typeof(List<Item>));

        }

        [TestMethod]
        public void GetVariationTest()
        {
            //Arrange
            string skuNumber = "T-SH-SM";

            //Act
            var result = _controller.Get(skuNumber);

            //Assert           
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));

            var item = result.Result as OkObjectResult;
            Assert.IsInstanceOfType(item.Value, typeof(ItemVariation));
        }

        [TestMethod]
        public void GetItemSkuTest_NotFound_Result()
        {
            //Arrange
            string SKUNumber = "Abcd";

            //Act
            var result = _controller.Get(SKUNumber);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void DeleteQuantity_Test()
        {
            // Arrange
            string SKUNumber = "T-SH-SM";
            long quantity = 2;

            // Act
            var result = _controller.Delete(SKUNumber, quantity);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

       [TestMethod]
       public void AddInventory_ReturnsCreatedResponse()
        {
            // Arrange
            var inventory = new InventoryRequestModel()
            {
                SKU = "Test-SKU",
                Description = "Test-Description",
                Quantity = 20
            };

            // Act
            var result = _controller.Add(inventory);

            // Assert
            Assert.IsInstanceOfType(result, typeof(CreatedResult));
        }

        [TestMethod]
        public void AddInventory_ReturnedResponseAsCreatedItem()
        {
            // Arrange
            var inventory = new InventoryRequestModel()
            {
                SKU = "Test-SKU",
                Description = "Test-Description",
                Quantity = 20
            };

            // Act
            var result = _controller.Add(inventory) as CreatedResult;
            var item = result.Value as ItemVariation;

            // Assert
            Assert.IsInstanceOfType(item, typeof(ItemVariation));
        }

        [TestMethod]
        public void AddInventory_ReturnsBadRequest()
        {
            // Arrange
            var inventory = new InventoryRequestModel()
            {
                SKU = "Test-SKU",
                Description = "Test-Description",
            };

            // Act
            var result = _controller.Add(inventory);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void AddBulkInventory_ReturnedResponseAsOK()
        {
            // Arrange
            var bulkAdd = new MasterDataRequestModel()
            {
                SKU = "Test-SKU",
                OptionKeyIds = new List<int>() { 2, 3},
                CategoryId = 2
            };

            // Act
            var result = _controller.AddBulkSkus(bulkAdd) as OkResult;
           

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public void GetAllItemVariationsTest()
        {
            //Arrange

            //Act
            var result = _controller.GetVariationsOfAllItems();

            //Assert           
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));

            var list = result.Result as OkObjectResult;
            Assert.IsInstanceOfType(list.Value, typeof(List<ItemVariation>));

        }
    }
}
