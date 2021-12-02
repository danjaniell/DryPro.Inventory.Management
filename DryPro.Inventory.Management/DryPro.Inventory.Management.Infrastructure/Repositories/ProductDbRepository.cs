using AutoFixture;
using DryPro.Inventory.Management.Common.Enums;
using DryPro.Inventory.Management.Common.Extensions;
using DryPro.Inventory.Management.Core.Entities;
using DryPro.Inventory.Management.Core.Repositories;
using DryPro.Inventory.Management.Infrastructure.Data;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DryPro.Inventory.Management.Infrastructure.Repositories
{
    public class ProductDbRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _products;

        public ProductDbRepository(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _products = database.GetCollection<Product>("Products");
        }

        public async Task<string> ClearAllAndGenerateRandomData()
        {
            try
            {
                await DeleteAll();
                if ((await GetAllAsync()).Count == 0)
                {
                    var fixture = new Fixture();
                    int[] idRange = Enumerable.Range(1, 10).ToArray();
                    var productBuilder = fixture.Build<Product>();
                    var auxItemBuilder = fixture.Build<AuxilliaryItem>();
                    var auxItems = idRange.Select(x => auxItemBuilder.With(b => b.ProductId, x).Create());
                    var products = idRange.Select(x => productBuilder.With(a => a.Id, x.ToString()).With(b=> b.AuxilliaryItems, auxItems.Where(b=>b.ProductId == x).ToList()).Create()).ToList();
                    products.ForEach(async x =>
                    {
                        await AddAsync(x);
                    });
                }
                return "Success";
            }
            catch
            {
                return "Failed";
            }
        }

        public async Task DeleteAll() => await _products.DeleteManyAsync(x => true);

        public async Task<Product> AddAsync(Product entity)
        {
            await _products.InsertOneAsync(entity);
            return entity;
        }

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

        public async Task DeleteAsync(Product entity) => await _products.DeleteOneAsync(x => x.Guid == entity.Guid);

        public async Task<int?> DeleteAuxItemAsync(AuxilliaryItem entity)
        {
            var product = await GetByIdAsync(entity.ProductId);
            product.AuxilliaryItems.Remove(entity);
            await UpdateAsync(product);
            return product.Guid;
        }

        public async Task<IReadOnlyList<Product>> GetAllAsync() => (await _products.FindAsync(x => true)).ToList();

        public async Task<IEnumerable<AuxilliaryItem>> GetAllAuxItemsAsync(int productId)
        {
            var product = await GetByIdAsync(productId);
            return product.AuxilliaryItems;
        }

        public async Task<AuxilliaryItem> GetAuxItemByIdAsync(int productId, int id)
        {
            var product = await GetByIdAsync(productId);
            return product.AuxilliaryItems.Single(x => x.Id == id);
        }

        public async Task<Product> GetByIdAsync(int id) => (await _products.FindAsync(x => x.Guid == id)).SingleOrDefault();

        public async Task<decimal> GetCostOfAllAuxItemsAsync(int productId)
        {
            var product = await GetByIdAsync(productId);
            return product.AuxilliaryItems.Sum(x => x.Cost);
        }

        public async Task<IEnumerable<Product>> GetProductByType(ProductType type) => (await _products.FindAsync(x => x.Type == type)).ToList();

        public async Task UpdateAsync(Product entity) => await _products.ReplaceOneAsync(x => x.Guid == entity.Guid, entity);

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
            return product.Guid;
        }
    }
}