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
using DryPro.Inventory.Management.Common.Extensions;

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
                int[] idRange = Enumerable.Range(1, 10).ToArray();
                var products = idRange.Select(x => builder.With(a => a.Id, x).Create()).ToList();
                products.ForEach(async x =>
                {
                    await AddAsync(x);
                });
            }
        }

        public override async Task<Product> AddAsync(Product entity)
        {
            await _productContext.Products.AddAsync(entity);
            await _productContext.SaveChangesAsync();
            return entity;
        }

        public override async Task DeleteAsync(Product entity)
        {
            _productContext.Products.Remove(entity);
            await _productContext.SaveChangesAsync();
        }

        public override async Task<IReadOnlyList<Product>> GetAllAsync() => await _productContext.Products.ToListAsync();

        public override async Task<Product> GetByIdAsync(int id) => await _productContext.Products.FindAsync(id);

        public override async Task UpdateAsync(Product entity)
        {
            _productContext.Products.Update(entity);
            await _productContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByType(ProductType type) => await _productContext.Products.Where(x => x.Type==type).ToListAsync();

        public async Task<AuxilliaryItem> AddAuxItemAsync(AuxilliaryItem entity)
        {
            var product = await GetByIdAsync(entity.ProductId);
            if (product.AuxilliaryItems.AddIfUnique(entity))
            {
                await UpdateAsync(product);
                return entity;
            }
            return null;
        }

        public async Task<int?> DeleteAuxItemAsync(AuxilliaryItem entity)
        {
            var product = await GetByIdAsync(entity.ProductId);
            product.AuxilliaryItems.Remove(entity);
            await UpdateAsync(product);
            return product.Id;
        }

        public async Task<IEnumerable<AuxilliaryItem>> GetAllAuxItemsAsync(int productId)
        {
            var product = await GetByIdAsync(productId);
            return product.AuxilliaryItems;
        }

        public async Task<AuxilliaryItem> GetAuxItemByIdAsync(int productId, int id)
        {
            var product = await GetByIdAsync(productId);
            return product.AuxilliaryItems.Single(x=>x.Id == id);
        }

        public async Task<decimal> GetCostOfAllAuxItemsAsync(int productId)
        {
            var product = await GetByIdAsync(productId);
            return product.AuxilliaryItems.Sum(x=>x.Cost);
        }

        public async Task<int?> UpdateAuxItemAsync(AuxilliaryItem entity)
        {
            var product = await GetByIdAsync(entity.ProductId);
            int index = product.AuxilliaryItems.IndexOf(entity);
            bool found = (index != -1);
            if (found)
            {
                product.AuxilliaryItems[index] = entity;
            }
            await UpdateAsync(product);
            return product.Id;
        }
    }
}