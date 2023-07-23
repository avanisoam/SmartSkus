using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartSkus.Api.Models;

namespace MoversCandidateTest.UnitTest.Model
{
    [TestClass]
    public class OptionVariationsUnitTest
    {
        [TestMethod]
        public void OptionVariationsModelTest()
        {
            //Arrange
            long optionVariationId = 1;
            long itemVariationId = 1;
            long optionValueId = 1;

            //Act
            OptionVariations optionVariation = new OptionVariations(optionVariationId, itemVariationId, optionValueId);

            //Assert
            Assert.AreEqual(optionVariation.OptionVariationID, optionVariationId);
            Assert.AreEqual(optionVariation.ItemVariationID, itemVariationId);
            Assert.AreEqual(optionVariation.OptionValueID, optionValueId);
            Assert.IsNull(optionVariation.ItemVariation);
            Assert.IsNull(optionVariation.OptionValue);
        }
    }
}
