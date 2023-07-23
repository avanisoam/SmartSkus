
namespace SmartSkus.Shared
{
    public static class Utilities
    {
        /// <summary>
        /// Get all unique SKU's list from the list of diffrent options
        /// 
        /// INPUT:  {
        ///             "Size",  -> { "Small", "Medium", "Large" }
        ///             "Color", -> { "Red", "Blue" }
        ///         }
        ///         
        /// OUTPUT: { 
        ///             "SM-RE", 
        ///             "SM-BL", 
        ///             "ME-RE", 
        ///             "ME-BL", 
        ///             "LA-RE", 
        ///             "LA-BL"
        ///          }
        /// 
        /// </summary>
        /// <param name="keyValuePairs">options and id values</param>
        /// <param name="charCount">Number of char from option</param>
        /// <param name="splitChar">Split char for SKU's. Default = '-'</param>
        /// <returns>List of unique SKU's</returns>
        public static List<string> GetAllUniqueSKUs(Dictionary<string, List<string>> keyValuePairs, int charCount, char splitChar = '-')
        {
            var result = new List<string>();
            var tempResult = new List<string>();

            foreach (var kv in keyValuePairs)
            {
                if (result.Count == 0)
                {
                    foreach (var item in kv.Value)
                    {
                        result.Add(item.ToUpper().Substring(0, charCount));
                    }
                }
                else
                {
                    foreach (var r in result)
                    {
                        foreach (var item in kv.Value)
                        {
                            tempResult.Add(r + splitChar + item.ToUpper().Substring(0, charCount));
                        }
                    }

                    // Swap Values
                    result.Clear();
                    foreach (var r in tempResult)
                    {
                        result.Add(r);
                    }
                    tempResult.Clear();
                }
            }

            return result;
        }

        /// <summary>
        /// Get all unique SKU's dictionary from with underlying codes from the list of diffrent options
        /// 
        /// INPUT: { 
        ///             "Size",  -> { { 1, "Small"}, {2, "Medium"}, {3, "Large"} }
        ///             "Color", -> { { 4, "Red" }, { 5, "Blue" } }
        ///        }
        ///         
        /// OUTPUT: { 
        ///             {"SM-RE", {1, 4}}, 
        ///             {"SM-BL", {1, 5}}, 
        ///             {"ME-RE", {2, 4}}, 
        ///             {"ME-BL", {2, 5}}, 
        ///             {"LA-RE", {3, 4}}, 
        ///             {"LA-BL", {3, 5}}
        ///          }
        ///          
        /// </summary>
        /// <param name="keyValuePairs">options and id values</param>
        /// <param name="charCount">Number of char from option</param>
        /// <param name="splitChar">Split char for SKU's</param>
        /// <returns>>Dictionary of unique SKU's with underlying codes</returns>
        public static Dictionary<string, List<long>> GetAllUniqueSKUs(Dictionary<string, Dictionary<long, string>> keyValuePairs, int charCount, char splitChar = '-')
        {
            var result = new Dictionary<string, List<long>>();
            var tempResult = new Dictionary<string, List<long>>();

            foreach (var kv in keyValuePairs)
            {
                if (result.Count == 0)
                {
                    foreach (var item in kv.Value)
                    {
                        result.Add(item.Value.ToUpper().Substring(0, charCount), new List<long> { item.Key });
                    }
                }
                else
                {
                    foreach (var r in result)
                    {
                        foreach (var item in kv.Value)
                        {
                            List<long> copy = new List<long>(r.Value);
                            copy.Add(item.Key);

                            tempResult.Add(string.Format("{0}{1}{2}", r.Key, splitChar, item.Value.ToUpper().Substring(0, charCount)), copy);
                        }
                    }

                    // Swap Values
                    result.Clear();
                    foreach (var r in tempResult)
                    {
                        result.Add(r.Key, r.Value);
                    }
                    tempResult.Clear();
                }
            }

            return result;
        }
    }
}
