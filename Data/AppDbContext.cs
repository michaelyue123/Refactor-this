using Microsoft.EntityFrameworkCore;
using RefactorThis.Models;

namespace RefactorThis.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductOption> ProductOptions { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite("Data Source=App_Data/products.db");
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
