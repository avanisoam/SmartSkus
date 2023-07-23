using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartSkus.Api.Models;

namespace MoversCandidateTest.UnitTest.Model
{
    [TestClass]
    public class ItemUnitTest
    {
        [TestMethod]
        public void ItemModelTest_Movers_Requirement()
        {
            //Arrange           
            string itemCode = "ITEM-CODE";
            string description = "TEST-ITEM-DESCRIPTION";
            int categoryId = 1;

            //Act
            Item item = new(itemCode, description);

            //Assert
            Assert.AreEqual(item.ItemID, 0);
            Assert.AreEqual(item.ItemCode ,itemCode);
            Assert.AreEqual(item.Description , description);
            Assert.AreEqual(item.CategoryID, categoryId);
            Assert.IsNull(item.Category);
            Assert.IsNull(item.ItemVariations);
            Assert.IsNull(item.Name);
            Assert.IsNull(item.Region);
        }

        [TestMethod]
        public void ItemModelTest()
        {
            //Arrange
            long itemId = 1;
            string itemCode = "ITEM-CODE";
            string name = "TEST-ITEM-NAME";
            string description = "TEST-ITEM-DESCRIPTION";
            Region region = Region.EU;
            int categoryId = 1;

            //Act
            Item item = new(itemId, itemCode, name, description, region, categoryId);

            //Assert
            Assert.AreEqual(item.ItemID, itemId);
            Assert.AreEqual(item.ItemCode, itemCode);
            Assert.AreEqual(item.Name, name);
            Assert.AreEqual(item.Description, description);
            Assert.AreEqual(item.Region, region);
            Assert.AreEqual(item.CategoryID, categoryId);
            Assert.IsNull(item.Category);
            Assert.IsNull(item.ItemVariations);
        }
    }
}
