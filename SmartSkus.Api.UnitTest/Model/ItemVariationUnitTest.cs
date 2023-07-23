using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartSkus.Api.Models;

namespace MoversCandidateTest.UnitTest.Model
{
    [TestClass]
    public class ItemVariationUnitTest
    {
        [TestMethod]
        public void ItemVariationModelTest_Movers_Requirement()
        {
            //Arrange           
            string skuNumber = "T-SHIRT-SM-RE";
            string description = "Test Item Variation Description";
            long quantity = 1;

            //Act
            ItemVariation itemVariation = new ItemVariation(skuNumber, description, quantity);

            //Assert
            Assert.AreEqual(itemVariation.ItemVariationID, 0);
            Assert.AreEqual(itemVariation.ItemID,0);
            Assert.AreEqual(itemVariation.Price, 0);
            Assert.AreEqual(itemVariation.SKUNumber, skuNumber);
            Assert.AreEqual(itemVariation.Description, description);
            Assert.AreEqual(itemVariation.Quantity, quantity);
            Assert.IsNull(itemVariation.Item);
            Assert.IsNull(itemVariation.OptionVariations);
        }

        [TestMethod]
        public void ItemVariationModelTest()
        {
            //Arrange
            long itemVariationId = 1;
            string skuNumber = "T-SHIRT-SM-RE";
            string description = "Test Item Variation Description";
            double price = 99.99;
            long quantity = 1;
            long itemId = 10;

            //Act
            ItemVariation itemVariation = new ItemVariation(itemVariationId, skuNumber, description, price, quantity, itemId);

            //Assert
            Assert.AreEqual(itemVariation.ItemVariationID, itemVariationId);
            Assert.AreEqual(itemVariation.ItemID, itemId);
            Assert.AreEqual(itemVariation.Price, price);
            Assert.AreEqual(itemVariation.SKUNumber, skuNumber);
            Assert.AreEqual(itemVariation.Description, description);
            Assert.AreEqual(itemVariation.Quantity, quantity);
            Assert.IsNull(itemVariation.Item);
            Assert.IsNull(itemVariation.OptionVariations);
        }
    }
}
