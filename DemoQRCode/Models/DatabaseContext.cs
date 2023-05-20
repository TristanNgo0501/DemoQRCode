using Microsoft.EntityFrameworkCore;

namespace DemoQRCode.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().ToTable("products").HasKey(p => p.Id);
        }

        public DbSet<Product> Products { get; set; }
    }
}