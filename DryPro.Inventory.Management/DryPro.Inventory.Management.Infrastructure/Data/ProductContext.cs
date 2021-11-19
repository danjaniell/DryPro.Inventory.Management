using Microsoft.EntityFrameworkCore;

namespace DryPro.Inventory.Management.Infrastructure.Data
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Core.Entities.Product> Products { get; set; }
    }
}