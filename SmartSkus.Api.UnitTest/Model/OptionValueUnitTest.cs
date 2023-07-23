using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartSkus.Api.Models;

namespace MoversCandidateTest.UnitTest.Model
{
    [TestClass]
    public class OptionValueUnitTest
    {
        [TestMethod]
        public void OptionValueModelTest()
        {
            //Arrange
            long optionValueId = 1;
            string value = "Small";
            long optionKeyId = 1;

            //Act
            OptionValue optionValue = new OptionValue(optionValueId, value, optionKeyId);

            //Assert
            Assert.AreEqual(optionValue.OptionValueID, optionValueId);
            Assert.AreEqual(optionValue.Value, value);
            Assert.AreEqual(optionValue.OptionKeyID, optionKeyId);
            Assert.IsNull(optionValue.OptionKey);
            Assert.IsNull(optionValue.OptionVariations);
        }
    }
}
