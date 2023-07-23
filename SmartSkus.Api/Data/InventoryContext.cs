using Microsoft.EntityFrameworkCore;
using SmartSkus.Api.Models;

namespace SmartSkus.Api.Data
{
    public class InventoryContext : DbContext
    {
        public InventoryContext(DbContextOptions<InventoryContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Item> Items { get; set; }
        public DbSet<ItemVariation> ItemVariations { get; set; }

        public DbSet<OptionKey> OptionKeys { get; set; }
        public DbSet<OptionValue> OptionValues { get; set; }
        public DbSet<OptionVariations> OptionVariations { get; set; }

        public DbSet<SkuModel> skuModels { get; set; }

        public DbSet<CategoryOptionKey> CategoryOptionKeys { get; set; }

        public DbSet<Settings> Settings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<Item>().ToTable("Item");
            modelBuilder.Entity<ItemVariation>().ToTable("ItemVariation");

            modelBuilder.Entity<OptionKey>().ToTable("OptionKey");
            modelBuilder.Entity<OptionValue>().ToTable("OptionValue");
            modelBuilder.Entity<OptionVariations>().ToTable("OptionVariation");
            modelBuilder.Entity<SkuModel>().ToTable("SkuModel");
            modelBuilder.Entity<CategoryOptionKey>().ToTable("CategoryOptionKey");
            modelBuilder.Entity<Settings>().ToTable("Setting");
        }

        // To Enable Sensitive Data Logging
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
