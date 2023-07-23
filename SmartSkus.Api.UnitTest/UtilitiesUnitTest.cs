
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartSkus.Shared;
using System.Collections.Generic;

namespace MoversCandidateTest.UnitTest
{
    [TestClass]
    public class UtilitiesUnitTest
    {
        /// <summary>
        /// Default char Length for SKU Substring
        /// </summary>
        int defaultlength { get; set; } = 2;

        #region GetAllUniqueSKUs - return List

        [TestMethod]
        public void GetAllUniqueSKUs_List_len_2_Sucess_1()
        {
            // Arrange
            Dictionary<string, List<string>> keyValuePairs = new Dictionary<string, List<string>>();

            keyValuePairs.Add("Size", new List<string> { "Small", "Medium", "Large" });


            // Act
            List<string> uniqueSkus_len_2 = Utilities.GetAllUniqueSKUs(keyValuePairs, defaultlength);

            // Assert
            Assert.AreEqual(uniqueSkus_len_2.Count, keyValuePairs["Size"].Count);
            Assert.AreEqual(uniqueSkus_len_2[0], "SM");
            Assert.AreEqual(uniqueSkus_len_2[1], "ME");
            Assert.AreEqual(uniqueSkus_len_2[2], "LA");
        }

        [TestMethod]
        public void GetAllUniqueSKUs_List_len_3_Sucess_1()
        {
            // Arrange
            int length = 3;

            Dictionary<string, List<string>> keyValuePairs = new Dictionary<string, List<string>>();

            keyValuePairs.Add("Size", new List<string> { "Small", "Medium", "Large" });

            // Act
            List<string> uniqueSkus_len_3 = Utilities.GetAllUniqueSKUs(keyValuePairs, length);

            // Assert
            Assert.AreEqual(uniqueSkus_len_3.Count, keyValuePairs["Size"].Count);
            Assert.AreEqual(uniqueSkus_len_3[0], "SMA");
            Assert.AreEqual(uniqueSkus_len_3[1], "MED");
            Assert.AreEqual(uniqueSkus_len_3[2], "LAR");
        }

        [TestMethod]
        public void GetAllUniqueSKUs_List__char_Sucess_2()
        {
            // Arrange
            Dictionary<string, List<string>> keyValuePairs = new Dictionary<string, List<string>>();

            keyValuePairs.Add("Size", new List<string> { "Small", "Medium", "Large" });
            keyValuePairs.Add("Color", new List<string> { "Red", "Blue", "Green" });

            // Act
            List<string> uniqueSkus_len_2 = Utilities.GetAllUniqueSKUs(keyValuePairs, defaultlength, '*');

            // Assert
            Assert.AreEqual(uniqueSkus_len_2.Count, 3 * 3);
            Assert.AreEqual(uniqueSkus_len_2[0], "SM*RE");
            Assert.AreEqual(uniqueSkus_len_2[1], "SM*BL");
            Assert.AreEqual(uniqueSkus_len_2[2], "SM*GR");
            Assert.AreEqual(uniqueSkus_len_2[3], "ME*RE");
            Assert.AreEqual(uniqueSkus_len_2[4], "ME*BL");
            Assert.AreEqual(uniqueSkus_len_2[5], "ME*GR");
            Assert.AreEqual(uniqueSkus_len_2[6], "LA*RE");
            Assert.AreEqual(uniqueSkus_len_2[7], "LA*BL");
            Assert.AreEqual(uniqueSkus_len_2[8], "LA*GR");
        }

        [TestMethod]
        public void GetAllUniqueSKUs_List_Sucess_3()
        {
            // Arrange
            Dictionary<string, List<string>> keyValuePairs = new Dictionary<string, List<string>>();

            keyValuePairs.Add("Size", new List<string> { "Small", "Medium", "Large" });
            keyValuePairs.Add("Color", new List<string> { "Red", "Blue" });

            // Act
            List<string> uniqueSkus_len_2 = Utilities.GetAllUniqueSKUs(keyValuePairs, defaultlength);

            // Assert
            Assert.AreEqual(uniqueSkus_len_2.Count, 3 * 2);
            Assert.AreEqual(uniqueSkus_len_2[0], "SM-RE");
            Assert.AreEqual(uniqueSkus_len_2[1], "SM-BL");
            Assert.AreEqual(uniqueSkus_len_2[2], "ME-RE");
            Assert.AreEqual(uniqueSkus_len_2[3], "ME-BL");
            Assert.AreEqual(uniqueSkus_len_2[4], "LA-RE");
            Assert.AreEqual(uniqueSkus_len_2[5], "LA-BL");

        }

        [TestMethod]
        public void GetAllUniqueSKUs_List_Sucess_4()
        {
            // Arrange
            Dictionary<string, List<string>> keyValuePairs = new Dictionary<string, List<string>>();

            keyValuePairs.Add("Size", new List<string> { "Small", "Medium", "Large" });
            keyValuePairs.Add("Color", new List<string> { "Red", "Blue" });
            keyValuePairs.Add("Test", new List<string> { "T1*", "T2*" });

            // Act
            List<string> uniqueSkus_len_2 = Utilities.GetAllUniqueSKUs(keyValuePairs, defaultlength);

            // Assert
            Assert.AreEqual(uniqueSkus_len_2.Count, 3 * 2 * 2);
            Assert.AreEqual(uniqueSkus_len_2[0], "SM-RE-T1");
            Assert.AreEqual(uniqueSkus_len_2[1], "SM-RE-T2");
            Assert.AreEqual(uniqueSkus_len_2[2], "SM-BL-T1");
            Assert.AreEqual(uniqueSkus_len_2[3], "SM-BL-T2");
            Assert.AreEqual(uniqueSkus_len_2[4], "ME-RE-T1");
            Assert.AreEqual(uniqueSkus_len_2[5], "ME-RE-T2");
            Assert.AreEqual(uniqueSkus_len_2[6], "ME-BL-T1");
            Assert.AreEqual(uniqueSkus_len_2[7], "ME-BL-T2");
            Assert.AreEqual(uniqueSkus_len_2[8], "LA-RE-T1");
            Assert.AreEqual(uniqueSkus_len_2[9], "LA-RE-T2");
            Assert.AreEqual(uniqueSkus_len_2[10], "LA-BL-T1");
            Assert.AreEqual(uniqueSkus_len_2[11], "LA-BL-T2");
        }

        #endregion

        #region GetAllUniqueSKUs - return Dictionary

        [TestMethod]
        public void GetAllUniqueSKUs_Dict_len_2_Sucess_1()
        {
            // Arrange
            Dictionary<string, Dictionary<long, string>> keyValuePairs = new Dictionary<string, Dictionary<long, string>>();

            keyValuePairs.Add("Size", new Dictionary<long, string> { { 1, "Small" }, { 2, "Medium" }, { 3, "Large" } });


            // Act
            Dictionary<string, List<long>> uniqueSkus_len_2 = Utilities.GetAllUniqueSKUs(keyValuePairs, defaultlength);

            // Assert
            Assert.AreEqual(uniqueSkus_len_2.Count, keyValuePairs["Size"].Count);
            Assert.AreEqual(uniqueSkus_len_2["SM"].Count, 1);
            Assert.AreEqual(uniqueSkus_len_2["ME"].Count, 1);
            Assert.AreEqual(uniqueSkus_len_2["LA"].Count, 1);
        }

        [TestMethod]
        public void GetAllUniqueSKUs_Dict_len_3_Sucess_1()
        {
            // Arrange
            int length = 3;

            Dictionary<string, Dictionary<long, string>> keyValuePairs = new Dictionary<string, Dictionary<long, string>>();

            keyValuePairs.Add("Size", new Dictionary<long, string> { { 1, "Small" }, { 2, "Medium" }, { 3, "Large" } });


            // Act
            Dictionary<string, List<long>> uniqueSkus_len_3 = Utilities.GetAllUniqueSKUs(keyValuePairs, length);

            // Assert
            Assert.AreEqual(uniqueSkus_len_3.Count, keyValuePairs["Size"].Count);
            Assert.AreEqual(uniqueSkus_len_3["SMA"].Count, 1);
            Assert.AreEqual(uniqueSkus_len_3["MED"].Count, 1);
            Assert.AreEqual(uniqueSkus_len_3["LAR"].Count, 1);
        }

        [TestMethod]
        public void GetAllUniqueSKUs_Dict_char_Sucess_2()
        {
            // Arrange
            Dictionary<string, Dictionary<long, string>> keyValuePairs = new Dictionary<string, Dictionary<long, string>>();

            keyValuePairs.Add("Size", new Dictionary<long, string> { { 1, "Small" }, { 2, "Medium" }, { 3, "Large" } });
            keyValuePairs.Add("Color", new Dictionary<long, string> { { 4, "Red" }, { 5, "Blue" }, { 6, "Green" } });

            // Act
            Dictionary<string, List<long>> uniqueSkus_len_2 = Utilities.GetAllUniqueSKUs(keyValuePairs, defaultlength, '*');

            // Assert
            Assert.AreEqual(uniqueSkus_len_2.Count, 3 * 3);
            Assert.AreEqual(uniqueSkus_len_2["SM*RE"].Count, 2);
            Assert.AreEqual(uniqueSkus_len_2["SM*BL"].Count, 2);
            Assert.AreEqual(uniqueSkus_len_2["SM*GR"].Count, 2);
            Assert.AreEqual(uniqueSkus_len_2["ME*RE"].Count, 2);
            Assert.AreEqual(uniqueSkus_len_2["ME*BL"].Count, 2);
            Assert.AreEqual(uniqueSkus_len_2["ME*GR"].Count, 2);
            Assert.AreEqual(uniqueSkus_len_2["LA*RE"].Count, 2);
            Assert.AreEqual(uniqueSkus_len_2["LA*BL"].Count, 2);
            Assert.AreEqual(uniqueSkus_len_2["LA*GR"].Count, 2);
        }

        [TestMethod]
        public void GetAllUniqueSKUs_Dict_Sucess_3()
        {
            // Arrange
            Dictionary<string, Dictionary<long, string>> keyValuePairs = new Dictionary<string, Dictionary<long, string>>();

            keyValuePairs.Add("Size", new Dictionary<long, string> { { 1, "Small" }, { 2, "Medium" }, { 3, "Large" } });
            keyValuePairs.Add("Color", new Dictionary<long, string> { { 4, "Red" }, { 5, "Blue" } });

            // Act
            Dictionary<string, List<long>> uniqueSkus_len_2 = Utilities.GetAllUniqueSKUs(keyValuePairs, defaultlength);

            // Assert
            Assert.AreEqual(uniqueSkus_len_2.Count, 3 * 2);
            Assert.AreEqual(uniqueSkus_len_2["SM-RE"].Count, 2);
            Assert.AreEqual(uniqueSkus_len_2["SM-BL"].Count, 2);
            Assert.AreEqual(uniqueSkus_len_2["ME-RE"].Count, 2);
            Assert.AreEqual(uniqueSkus_len_2["ME-BL"].Count, 2);
            Assert.AreEqual(uniqueSkus_len_2["LA-RE"].Count, 2);
            Assert.AreEqual(uniqueSkus_len_2["LA-BL"].Count, 2);

        }

        [TestMethod]
        public void GetAllUniqueSKUs_Dict_Sucess_4()
        {
            // Arrange
            Dictionary<string, Dictionary<long, string>> keyValuePairs = new Dictionary<string, Dictionary<long, string>>();

            keyValuePairs.Add("Size", new Dictionary<long, string> { { 1, "Small" }, { 2, "Medium" }, { 3, "Large" } });
            keyValuePairs.Add("Color", new Dictionary<long, string> { { 4, "Red" }, { 5, "Blue" } });
            keyValuePairs.Add("Test", new Dictionary<long, string> { { 6, "T1*" }, { 7, "T2*" } });

            // Act
            Dictionary<string, List<long>> uniqueSkus_len_2 = Utilities.GetAllUniqueSKUs(keyValuePairs, defaultlength);

            // Assert
            Assert.AreEqual(uniqueSkus_len_2.Count, 3 * 2 * 2);
            Assert.AreEqual(uniqueSkus_len_2["SM-RE-T1"].Count, 3);
            Assert.AreEqual(uniqueSkus_len_2["SM-RE-T2"].Count, 3);
            Assert.AreEqual(uniqueSkus_len_2["SM-BL-T1"].Count, 3);
            Assert.AreEqual(uniqueSkus_len_2["SM-BL-T2"].Count, 3);
            Assert.AreEqual(uniqueSkus_len_2["ME-RE-T1"].Count, 3);
            Assert.AreEqual(uniqueSkus_len_2["ME-RE-T2"].Count, 3);
            Assert.AreEqual(uniqueSkus_len_2["ME-BL-T1"].Count, 3);
            Assert.AreEqual(uniqueSkus_len_2["ME-BL-T2"].Count, 3);
            Assert.AreEqual(uniqueSkus_len_2["LA-RE-T1"].Count, 3);
            Assert.AreEqual(uniqueSkus_len_2["LA-RE-T2"].Count, 3);
            Assert.AreEqual(uniqueSkus_len_2["LA-BL-T1"].Count, 3);
            Assert.AreEqual(uniqueSkus_len_2["LA-BL-T2"].Count, 3);
        }

        #endregion
    }
}