using DryPro.Inventory.Management.Infrastructure.Data;
using DryPro.Inventory.Management.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using DryPro.Inventory.Management.Core.Entities;
using DryPro.Inventory.Management.Core.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DryPro.Inventory.Management.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Core.Entities.Product>, IProductRepository
    {
        public ProductRepository(ProductContext productContext) : base(productContext)
        {
        }

        public async Task<IEnumerable<Product>> GetProductByName(string productName) => await _productContext.Products.Where(x => x.Name.Equals(productName)).ToListAsync();
    }
}