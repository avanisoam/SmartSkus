using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartSkus.Api.Data;
using SmartSkus.Api.Models;
using System.Linq;

namespace MoversCandidateTest.UnitTest.API
{
    [TestClass]
    public class TestBase
    {
        public void SeedDb(DbContextOptions<InventoryContext> dbContextOptions)
        {
            using var context = new InventoryContext(dbContextOptions);
            context.Database.EnsureCreated();

            // Look for any Categories.
            if (context.Categories.Any())
            {
                return;   // DB has been seeded
            }

            var categoryList = new Category[]
            {
            new Category{CategoryID=1,Title="None"},
            new Category{CategoryID=2,Title="Apperal"},
            new Category{CategoryID=3,Title="Parts"},
            new Category{CategoryID=4,Title="Tools"},
            new Category{CategoryID=5,Title="Electronics"}
            };
            foreach (Category c in categoryList)
            {
                context.Categories.Add(c);
            }
            context.SaveChanges();

            var items = new Item[]
            {
            new Item{ItemID=1,ItemCode="T-SH",Name="T-Shirt",Description="None of the above",CategoryID=2,Region=Region.Africa},
            new Item{ItemID=2,ItemCode="SREW",Name="Screw",Description="Good Quality Screws",CategoryID=4,Region=Region.US},
            new Item{ItemID=3,ItemCode="LAPT",Name="Laptop",Description="ThinkPad T430s",CategoryID=5,Region=Region.Asia},
            new Item{ItemID=4,ItemCode="HAMR",Name="Hammer",Description="Good Quality Hammer",CategoryID=4,Region=Region.MiddleEast},
            new Item{ItemID=5,ItemCode="JACT",Name="Jacket",Description="H&M Jacket",CategoryID=2,Region=Region.EU},
            new Item{ItemID=6,ItemCode="T-SH-SM",Name="T-SH-SM",Description="T-SH-SM",CategoryID=2,Region=Region.EU}

            };
            foreach (Item i in items)
            {
                context.Items.Add(i);
            }
            context.SaveChanges();

            var optionKeys = new OptionKey[]
            {
                new OptionKey{OptionKeyID=1,KeyName="Default"},
                new OptionKey{OptionKeyID=2,KeyName="Size"},
                new OptionKey{OptionKeyID=3,KeyName="Color"},
            };

            foreach (OptionKey ok in optionKeys)
            {
                context.OptionKeys.Add(ok);
            }
            context.SaveChanges();

            var optionValues = new OptionValue[]
            {
                new OptionValue{OptionValueID=1,OptionKeyID=1,Value="Default"},
                new OptionValue{OptionValueID=2,OptionKeyID=2,Value="Small"},
                new OptionValue{OptionValueID=3,OptionKeyID=2,Value="Medium"},
                new OptionValue{OptionValueID=4,OptionKeyID=2,Value="Large"},
                new OptionValue{OptionValueID=5,OptionKeyID=3,Value="Red"},
                new OptionValue{OptionValueID=6,OptionKeyID=3,Value="Green"},
                new OptionValue{OptionValueID=7,OptionKeyID=3,Value="Blue"}
            };

            foreach (OptionValue ov in optionValues)
            {
                context.OptionValues.Add(ov);
            }
            context.SaveChanges();

            var variations = new ItemVariation[]
            {
                new ItemVariation{ItemVariationID=1,Description="T-S-R",SKUNumber="T-S-RED",ItemID=1,Price=100,Quantity=10},
                new ItemVariation{ItemVariationID=2,Description="T-M-R",SKUNumber="T-M-RED",ItemID=1,Price=100,Quantity=5},
                new ItemVariation{ItemVariationID=3,Description="T-L-R",SKUNumber="T-L-RED",ItemID=1,Price=100,Quantity=2},
                new ItemVariation{ItemVariationID=4,Description="Small",SKUNumber="SCRE-SM",ItemID=2,Price=10,Quantity=100},
                new ItemVariation{ItemVariationID=5,Description="Intell",SKUNumber="LAP-INT",ItemID=3,Price=45000,Quantity=10},
                new ItemVariation{ItemVariationID=6,Description="Long-Hammer",SKUNumber="HAMM-LON",ItemID=4,Price=50,Quantity=106},
                new ItemVariation{ItemVariationID=7,Description="Jacket-Leather",SKUNumber="JACK-LED",ItemID=5,Price=1000,Quantity=20},
                new ItemVariation{ItemVariationID=8,Description="T-SH-SM",SKUNumber="T-SH-SM",ItemID=6,Price=1000,Quantity=20}
            };

            foreach (ItemVariation v in variations)
            {
                context.ItemVariations.Add(v);
            }
            context.SaveChanges();

            var optionVariations = new OptionVariations[]
            {
                new OptionVariations{OptionVariationID=1,ItemVariationID=1,OptionValueID=2},
                new OptionVariations{OptionVariationID=2,ItemVariationID=1,OptionValueID=5},
                new OptionVariations{OptionVariationID=3,ItemVariationID=2,OptionValueID=3},
                new OptionVariations{OptionVariationID=4,ItemVariationID=2,OptionValueID=5},
                new OptionVariations{OptionVariationID=5,ItemVariationID=3,OptionValueID=4},
                new OptionVariations{OptionVariationID=6,ItemVariationID=3,OptionValueID=2}
            };

            foreach (OptionVariations ov in optionVariations)
            {
                context.OptionVariations.Add(ov);
            }
            context.SaveChanges();

            var categoryOptionKeys = new CategoryOptionKey[]
            {
                new CategoryOptionKey{ CategoryOptionKeyId = 1, CategoryID = 1, OptionKeyID = 1},
                new CategoryOptionKey{ CategoryOptionKeyId = 2, CategoryID = 2, OptionKeyID = 2}
            };

            foreach (CategoryOptionKey co in categoryOptionKeys)
            {
                context.CategoryOptionKeys.Add(co);
            }
            context.SaveChanges();
        }
    }
}