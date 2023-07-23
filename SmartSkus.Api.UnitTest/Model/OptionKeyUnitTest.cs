using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartSkus.Api.Models;

namespace MoversCandidateTest.UnitTest.Model
{
    [TestClass]
    public class OptionKeyUnitTest
    {
        [TestMethod]
        public void OptionKeyModelTest()
        {
            //Arrange
            long optionKeyId = 1;
            string keyName = "Size";

            //Act
            OptionKey optionKey = new OptionKey(optionKeyId, keyName);

            //Assert
            Assert.AreEqual(optionKey.OptionKeyID, optionKeyId);
            Assert.AreEqual(optionKey.KeyName, keyName);
            Assert.IsNull(optionKey.OptionValues);
        }
    }
}
