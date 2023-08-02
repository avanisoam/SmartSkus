using SmartSkus.Api.Models;
using SmartSkus.Shared.Dtos;
using SmartSkus.Shared.Enums;
using System.Collections.Generic;
using System.Linq;
using Region = SmartSkus.Shared.Enums.Region;

namespace SmartSkus.Api.Data
{
    public class DbInitializer
    {
        private static Dictionary<long, Settings> _settingsDict = new();

        public static IReadOnlyDictionary<long, Settings> AllSettings => _settingsDict;

        public static long NextSettingsId => _settingsDict.Keys.DefaultIfEmpty().Max() + 1;
        public static void Initialize(InventoryContext context)
        {
            // To ensure database and schemas are created
            context.Database.EnsureCreated();

            // Verify Master Data if it exists
            if(context.Categories.Any() || context.OptionKeys.Any() || context.OptionValues.Any())
            {
                return;   // DB has been seeded
            }

            #region Seed Master Data

            // Add Categories

            var categoryList = new Category[]
            {
                // CategoryID - Autogenrated

                new Category{Title="None"},
                new Category{Title="Apperal"},
                new Category{Title="Parts"},
                new Category{Title="Tools"},
                new Category{Title="Electronics"}
            };

            foreach (Category c in categoryList)
            {
                context.Categories.Add(c);
            }

            context.SaveChanges();

            // Add OptionKeys

            OptionKey[] optionkeys = new OptionKey[]
            {
                // OptionKeyID - Custom

                //new OptionKey{OptionKeyID=100,KeyName="Default"},
                //new OptionKey{OptionKeyID=200,KeyName="Size"},
                //new OptionKey{OptionKeyID=300,KeyName="Color"},
                new OptionKey{KeyName="Default"},
                new OptionKey{KeyName="Size"},
                new OptionKey{KeyName="Color"},
                new OptionKey{KeyName="OneSize"},
                //new OptionKey{KeyName="Kits"},
                new OptionKey{KeyName="Lights"},

            };

            foreach (OptionKey ok in optionkeys)
            {
                context.OptionKeys.Add(ok);
            }

            context.SaveChanges();

            var categoryOptionKey = new CategoryOptionKey[]
            {
                new CategoryOptionKey{Category= categoryList.FirstOrDefault(x => x.Title == "None"), OptionKey= optionkeys.FirstOrDefault(y => y.KeyName=="Default")},
                new CategoryOptionKey{Category= categoryList.FirstOrDefault(x => x.Title == "Apperal"), OptionKey= optionkeys.FirstOrDefault(y => y.KeyName=="Size")},
                new CategoryOptionKey{Category= categoryList.FirstOrDefault(x => x.Title == "Apperal"), OptionKey= optionkeys.FirstOrDefault(y => y.KeyName=="Color")},
                new CategoryOptionKey{Category= categoryList.FirstOrDefault(x => x.Title == "Parts"), OptionKey= optionkeys.FirstOrDefault(y => y.KeyName=="OneSize")},
                new CategoryOptionKey{Category= categoryList.FirstOrDefault(x => x.Title == "Tools"), OptionKey= optionkeys.FirstOrDefault(y => y.KeyName=="Size")},
                //new CategoryOptionKey{Category= categoryList.FirstOrDefault(x => x.Title == "Tools"), OptionKey= optionkeys.FirstOrDefault(y => y.KeyName=="Kits")},
                new CategoryOptionKey{Category= categoryList.FirstOrDefault(x => x.Title == "Electronics"), OptionKey= optionkeys.FirstOrDefault(y => y.KeyName=="Lights")}
            };

            foreach (var co in categoryOptionKey)
            {
                context.CategoryOptionKeys.Add(co);
            }

            context.SaveChanges();

            // Add OptionValues

            var optionValues = new OptionValue[]
            {
                // OptionValueID - Autogenrated

                new OptionValue{Value="Default", OptionKeyID=1},
                new OptionValue{Value="Small", OptionKeyID=2},
                new OptionValue{Value="Medium", OptionKeyID=2},
                new OptionValue{Value="Large", OptionKeyID=2},
                new OptionValue{OptionKeyID=3,Value="Red"},
                new OptionValue{OptionKeyID=3,Value="Green"},
                new OptionValue{OptionKeyID=3,Value="Blue"},
                new OptionValue{OptionKeyID=4,Value="OneSize"},
                //new OptionValue{OptionKeyID=5,Value="SetOf3"},
                //new OptionValue{OptionKeyID=5,Value="SetOf5"},
                //new OptionValue{OptionKeyID=5,Value="SetOf10"},
                new OptionValue{OptionKeyID=5,Value="LED"},
                new OptionValue{OptionKeyID=5,Value="CFL"}
            };

            foreach (OptionValue ov in optionValues)
            {
                context.OptionValues.Add(ov);
            }

            context.SaveChanges();

            #endregion

            #region Seed Inventory Data

            #region Add new Item

            var apperalCategory = (from c in context.Categories
                                   where c.Title == "Apperal"
                                   select c).FirstOrDefault<Category>();

            var item = new Item
            {
                // ItemID - Autogenrated
                ItemCode = "T0-SH",
                Name = "T0-Shirt",
                Description = "Default Item Description",
                Category = apperalCategory,
                Region = (Models.Region?)Region.Africa
            };

            #endregion

            #region Get Unique SKUs List - Auto generated

            /*

                Dictionary<string, Dictionary<long, string>> keyValuePairs = new();

                var sizeOptionValue = (from k in context.OptionValues
                                       where k.OptionKeyID == optionkeys[1].OptionKeyID // OptionKey for Size
                                       select new { k.OptionValueID, k.Value })
                                       .ToDictionary(t => t.OptionValueID, t => t.Value);

                keyValuePairs.Add("Size-2", sizeOptionValue);

                Dictionary<string, List<long>> uniqueSKUs = Utilities.GetAllUniqueSKUs(keyValuePairs, 2);

                */

            //var optionValueName = "";

            //var sizeOptionKey = (from x in context.OptionKeys
            //                     where x.OptionKeyID == optionkeys[1].OptionKeyID
            //                     select x.KeyName).FirstOrDefault();

            Dictionary<string, List<long>> uniqueSKUs = new();

            var sizeOptionValue = (from k in context.OptionValues
                                   where k.OptionKeyID == optionkeys[1].OptionKeyID // OptionKey for Size
                                   select k).ToList();

            foreach (var ov in sizeOptionValue)
            {
                uniqueSKUs.Add(ov.Value.ToUpper().Substring(0, 2), new List<long> { ov.OptionValueID });
            }

            #endregion

            #region Add product variation based on Unique SKUs

            foreach (var s in uniqueSKUs)
            {
                string newSku = item.ItemCode + "-" + s.Key;
                var splitSku = newSku.Split('-');
                if(splitSku.Length != 4)
                {
                    for(int i = splitSku.Length;i<4 ; i++)
                    {
                        newSku = newSku + "-00";
                    }
                }

                var variation = new ItemVariation
                {
                    // VariationID - Autogenrated
                    // Description = "Variation - Default Description",//String.Empty,
                    Description = "Variation - " + s.Key, 
                    SKUNumber = newSku,//item.ItemCode + "-" + s.Key,
                    Price = 0,
                    Quantity = 10,
                    Item = item
                };

                context.ItemVariations.Add(variation);

                foreach (long i in s.Value)
                {
                    var optionVariation = new OptionVariations
                    {
                        // OptionVariationsID - Autogenrated
                        ItemVariation = variation,
                        OptionValueID = i
                    };
                    //optionValueName = (from v in context.OptionValues
                    //               where v.OptionValueID== optionVariation.OptionValueID
                    //               select v.Value).FirstOrDefault();
                    context.OptionVariations.Add(optionVariation);
                }

                var splitSkuAttribute = newSku.Split('-');

                var skuModel = new SkuModel
                {
                    ItemName = item.Name,
                    Attribute1 = splitSkuAttribute[1],//"Type",//item.ItemCode,//"Type",
                    Attribute2 = splitSkuAttribute[2],//"Size",//sizeOptionKey,//"Size",
                    Attribute3 = splitSkuAttribute[3],//"",//optionValueName,
                    //GenerateSku = variation.SKUNumber,//item.ItemCode + "-" + s.Key,
                    GenerateSku = newSku,//item.ItemCode + "-" + s.Key,
                    Item = item,
                    Description = "Seed Data",
                    Quantity = 1
                };

                context.skuModels.Add(skuModel);

            }

            #endregion

            context.SaveChanges();

            #endregion

            Settings[] settings = AddSettings(context);
        }

        private static Settings[] AddSettings(InventoryContext context)
        {
            var settingList = new Settings[]
            { 
                /* 1 - 1 */ CreateSetting(),
            };

            foreach (var s in settingList)
            {
                context.Settings.Add(s);
            }

            context.SaveChanges();

            return settingList;
        }

        private static Settings CreateSetting()
        {
            long tempId = NextSettingsId;

            Settings setting = new()
            {
                Id = tempId,
                Name = "Smart Sku's",
                SelectedBackupFormat = DataFormat.Json,
                Size = 0,
                Culture = "en",
                //Culture = "hi",
                //Theme = "default",
                Theme = "sketchy",
                Background = "Default",
                Screen = Screen.Main
            };

            _settingsDict[tempId] = setting;

            return setting;
        }
    }
}
