using DryPro.Inventory.Management.Infrastructure.Data;
using DryPro.Inventory.Management.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using DryPro.Inventory.Management.Core.Entities;
using DryPro.Inventory.Management.Core.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DryPro.Inventory.Management.Common.Enums;
using AutoFixture;
using System;

namespace DryPro.Inventory.Management.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Core.Entities.Product>, IProductRepository
    {
        public ProductRepository(ProductContext productContext) : base(productContext)
        {
            PopulateRepoWithMocks();
        }

        private void PopulateRepoWithMocks()
        {
            if (_productContext.Products.Count() == 0)
            {
                var fixture = new Fixture();
                var builder = fixture.Build<Product>();
                int[] idRange = Enumerable.Range(1, 50).ToArray();
                var products = idRange.Select(x => builder.With(a => a.Id, x).Create()).ToList();
                products.ForEach(x => AddAsync(x));
            }
        }

        public async Task<IEnumerable<Product>> GetProductByType(ProductType type) => await _productContext.Products.Where(x => x.Type==type).ToListAsync();
    }
}