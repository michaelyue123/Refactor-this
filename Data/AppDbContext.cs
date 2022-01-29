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
            //optionsBuilder.EnableSensitiveDataLogging();
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<Product>().Property(x => x.Id).HasColumnType("TEXT COLLATE NOCASE");
            modelBuilder.Entity<Product>().Property(x => x.Name).HasColumnType("TEXT COLLATE NOCASE");
            modelBuilder.Entity<Product>().Property(x => x.Description).HasColumnType("TEXT COLLATE NOCASE");
            modelBuilder.Entity<Product>().Property(x => x.Price).HasColumnType("TEXT COLLATE NOCASE");
            modelBuilder.Entity<Product>().Property(x => x.DeliveryPrice).HasColumnType("TEXT COLLATE NOCASE");



            modelBuilder.Entity<ProductOption>().Property(x => x.Id).HasColumnType("TEXT COLLATE NOCASE");
            modelBuilder.Entity<ProductOption>().Property(x => x.ProductId).HasColumnType("TEXT COLLATE NOCASE");
            modelBuilder.Entity<ProductOption>().Property(x => x.Name).HasColumnType("TEXT COLLATE NOCASE");
            modelBuilder.Entity<ProductOption>().Property(x => x.Description).HasColumnType("TEXT COLLATE NOCASE");
        }
    }
}
