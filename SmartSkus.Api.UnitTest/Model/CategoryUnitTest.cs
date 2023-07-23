using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartSkus.Api.Models;

namespace MoversCandidateTest.UnitTest.Model
{
    [TestClass]
    public class CategoryUnitTest
    {
        [TestMethod]
        public void CategoryModelTest()
        {
            //Arrange           
           int categoryId = 1;
           string title = "Test-Category";

            //Act
            Category category = new Category(categoryId, title);

            //Assert
            Assert.AreEqual(category.CategoryID, categoryId);
            Assert.AreEqual(category.Title , title);
            Assert.IsNull(category.Items);
        }
    }
}
