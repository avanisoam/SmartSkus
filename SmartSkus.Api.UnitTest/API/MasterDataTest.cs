using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartSkus.Api.Controllers.MasterData;
using SmartSkus.Api.Data;
using SmartSkus.Api.Data.Interface;
using SmartSkus.Api.Data.Repo;
using SmartSkus.Api.Models;
using System;
using System.Collections.Generic;

namespace MoversCandidateTest.UnitTest.API
{
    [TestClass]
    public class MasterDataTest : TestBase
    {
        private readonly DbContextOptions<InventoryContext> dbContextOptions =
           new DbContextOptionsBuilder<InventoryContext>()
           .UseInMemoryDatabase(databaseName: "MasterDataTest_InventoryDb")
           .Options;

        readonly MasterDataController _controller;

        public MasterDataTest()
        {
            var context = new InventoryContext(dbContextOptions);
            IMasterDataRepo repo = new MasterDataRepo(context);
            _controller = new MasterDataController(repo);

            SeedDb(dbContextOptions);
        }

        [TestMethod]
        public void GetAllCategoryTest()
        {
            //Arrange

            //Act
            var result = _controller.GetAllCategory();

            //Assert           
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));

            var list = result.Result as OkObjectResult;
            Assert.IsInstanceOfType(list.Value, typeof(List<Category>));
        }

        [TestMethod]
        public void GetSingleCategoryTest()
        {
            //Arrange
            string title = "Apperal";

            //Act
            var result = _controller.GetSingleCategory(title);

            //Assert           
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));

            var item = result.Result as OkObjectResult;
            Assert.IsInstanceOfType(item.Value , typeof(Category));

        }

        [TestMethod]
        public void GetCategoryTest_InvalidArgument()
        {
            //Arrange
            string title = " ";

            try
            {
                var result = _controller.GetSingleCategory(title);
            }
            catch(ArgumentNullException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.Fail();
            }

        }

        [TestMethod]
        public void GetCategoryTest_NotFound_Result()
        {
            //Arrange
            string title = "Abcd";

            //Act
            var result = _controller.GetSingleCategory(title);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundObjectResult));
        }

        [TestMethod]
        public void GetCategoryByIdTest()
        {
            //Arrange
            int id = 2;

            //Act
            var result = _controller.GetCategoryById(id);

            //Assert           
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));

            var item = result.Result as OkObjectResult;
            Assert.IsInstanceOfType(item.Value, typeof(Category));

        }

        [TestMethod]
        public void GetCategoryByIdTest_NotFound_Result()
        {
            //Arrange
            int id = 200;

            //Act
            var result = _controller.GetCategoryById(id);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetAllOptionKeysTest()
        {
            //Arrange

            //Act
            var result = _controller.GetAllOptionKeys();

            //Assert           
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));

            var list = result.Result as OkObjectResult;
            Assert.IsInstanceOfType(list.Value, typeof(List<OptionKey>));
        }

        [TestMethod]
        public void GetSingleOptionKeyTest()
        {
            //Arrange
            string KeyName = "Size";

            //Act
            var result = _controller.GetSingleOptionKey(KeyName);

            //Assert           
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));

            var item = result.Result as OkObjectResult;
            Assert.IsInstanceOfType(item.Value, typeof(OptionKey));

        }

        [TestMethod]
        public void GetOptionKeyTest_NotFound_Result()
        {
            //Arrange
            string KeyName = "Abcd";

            //Act
            var result = _controller.GetSingleOptionKey(KeyName);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void AddCategory_ReturnsCreatedResponse()
        {
            // Arrange
            var category = new Category()
            {
                Title = "Test-Category"
            };

            // Act
            var result = _controller.AddCategory(category);

            // Assert
            Assert.IsInstanceOfType(result, typeof(CreatedResult));
        }

        [TestMethod]
        public void AddCategory_ReturnedResponseAsCreatedItem()
        {
            // Arrange
            var category = new Category()
            {
                Title = "Test-Category"
            };

            // Act
            var result = _controller.AddCategory(category) as CreatedResult;
            var item = result.Value as Category;

            // Assert
            Assert.IsInstanceOfType(item, typeof(Category));
        }

        [TestMethod]
        public void AddCategory_ReturnsBadRequest()
        {
            // Arrange
            var category = new Category()
            {
                CategoryID = 1
            };

            // Act
            var result = _controller.AddCategory(category);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void GetAllOptionsKeys_Test()
        {
            //Arrange

            //Act
            var result = _controller.GetAllOptionKeys();

            //Assert           
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));

            var list = result.Result as OkObjectResult;
            Assert.IsInstanceOfType(list.Value, typeof(List<OptionKey>));

        }

        [TestMethod]
        public void GetSingleOptionKey_Test()
        {
            //Arrange
            string keyName = "Size";

            //Act
            var result = _controller.GetSingleOptionKey(keyName);

            //Assert           
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));

            var item = result.Result as OkObjectResult;
            Assert.IsInstanceOfType(item.Value, typeof(OptionKey));

        }

        [TestMethod]
        public void AddOptonKey_ReturnsCreatedResponse()
        {
            // Arrange
            var optionKey = new OptionKey()
            {
                //OptionKeyID = 100,
                KeyName = "Test-OptionKey"
            };

            // Act
            var result = _controller.AddOptionKeys(optionKey);

            // Assert
            Assert.IsInstanceOfType(result, typeof(CreatedResult));
        }

        [TestMethod]
        public void AddOptonKey_ReturnedResponseAsCreatedItem()
        {
            // Arrange
            var optionKey = new OptionKey()
            {
                //OptionKeyID = 200,
                KeyName = "Test-OptionKey"
            };

            // Act
            var result = _controller.AddOptionKeys(optionKey) as CreatedResult;
            var item = result.Value as OptionKey;

            // Assert
            Assert.IsInstanceOfType(item, typeof(OptionKey));
        }

        [TestMethod]
        public void AddOptonKey_ReturnsBadRequest()
        {
            // Arrange
            var optionKey = new OptionKey()
            {
                OptionKeyID = 300
            };

            // Act
            var result = _controller.AddOptionKeys(optionKey);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void GetOptionKeyByIdTest()
        {
            //Arrange
            long id = 2;

            //Act
            var result = _controller.GetOptionKeyById(id);

            //Assert           
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));

            var item = result.Result as OkObjectResult;
            Assert.IsInstanceOfType(item.Value, typeof(OptionKey));

        }

        [TestMethod]
        public void AddOptonValue_ReturnedResponseAsNoContent()
        {
            // Arrange
            var optionValue = new OptionValue()
            {
                Value = "Test-OptionValue",
                OptionKeyID = 1
            };

            // Act
            var result = _controller.AddOptionValues(optionValue) as NoContentResult;
           

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public void AddOptonValue_ReturnsBadRequest()
        {
            // Arrange

            // Act
            var result = _controller.AddOptionValues(null);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public void GetAllOptionValuesTest()
        {
            //Arrange

            //Act
            var result = _controller.GetOptionValues();

            //Assert           
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));

            var list = result.Result as OkObjectResult;
            Assert.IsInstanceOfType(list.Value, typeof(List<OptionValue>));
        }

        [TestMethod]
        public void GetOptionValueByIdTest()
        {
            //Arrange
            long id = 2;

            //Act
            var result = _controller.GetOptionValueById(id);

            //Assert           
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));

            var item = result.Result as OkObjectResult;
            Assert.IsInstanceOfType(item.Value, typeof(OptionValue));

        }

        [TestMethod]
        public void GetAllCategoryOptionKey_Test()
        {
            //Arrange

            //Act
            var result = _controller.GetAllCategoryWithKeys();

            //Assert           
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));

            var list = result.Result as OkObjectResult;
            Assert.IsInstanceOfType(list.Value, typeof(List<CategoryOptionKey>));

        }

        [TestMethod]
        public void GetCategoryOptionKeyByIdTest()
        {
            //Arrange
            int id = 2;

            //Act
            var result = _controller.GetByCategoryId(id);

            //Assert           
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));

            var item = result.Result as OkObjectResult;
            Assert.IsInstanceOfType(item.Value, typeof(List<CategoryOptionKey>));

        }

        [TestMethod]
        public void GetCategoryOptionKeyById_NotFound_Result()
        {
            //Arrange
            int id = 200;

            //Act
            var result = _controller.GetByCategoryId(id);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void UpdateCategory_ReturnedResponseAsNoContent()
        {
            // Arrange
            var category = new Category()
            {
                CategoryID = 5,
                Title = "Test-Category"
            };

            // Act
            var result = _controller.UpdateCategory(5, category) as NoContentResult;


            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public void UpdateCategory_NotFound_Result()
        {
            //Arrange
            var category = new Category()
            {
                CategoryID = 60,
                Title = "Test-Category-60"
            };

            //Act
            var result = _controller.UpdateCategory(60, category);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void z_DeleteCategory_Test()
        {
            // Arrange
            int id = 2;

            // Act
            var result = _controller.DeleteCategory(id);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public void z_DeleteCategoryNotFoundResult_Test()
        {
            // Arrange
            int id = 200;

            // Act
            var result = _controller.DeleteCategory(id);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void z_DeleteOptionKeyWithValue_Test()
        {
            // Arrange
            long id = 2;

            // Act
            var result = _controller.DeleteOptionKeyWithValues(id);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public void z_DeleteOptionKeyNotFoundResult_Test()
        {
            // Arrange
            long id = 200;

            // Act
            var result = _controller.DeleteOptionKeyWithValues(id);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void z_DeleteOptionValue_Test()
        {
            // Arrange
            long id = 1;

            // Act
            var result = _controller.DeleteOptionValue(id);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public void z_DeleteOptionValueNotFoundResult_Test()
        {
            // Arrange
            long id = 200;

            // Act
            var result = _controller.DeleteOptionValue(id);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
