using AutoFixture;
using DryPro.Inventory.Management.Common.Enums;
using DryPro.Inventory.Management.Common.Extensions;
using DryPro.Inventory.Management.Core.Entities;
using DryPro.Inventory.Management.Core.Repositories;
using DryPro.Inventory.Management.Infrastructure.Data;
using MongoDB.Bson;
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
                    var productBuilder = fixture.Build<Product>();
                    var auxItemBuilder = fixture.Build<AuxilliaryItem>();
                    var products = productBuilder.Do(a => a._id = ObjectId.GenerateNewId().ToString())
                                                 .Without(a=> a.AuxilliaryItems)
                                                 .CreateMany(10)
                                                 .ToList();
                    products.ForEach(async x =>
                    {
                        x._id = new string(x._id.Replace("-", string.Empty).Skip(3).Take(24).ToArray()); //remove prefix "_id"
                        x.AuxilliaryItems = products.ConvertAll(a => auxItemBuilder.With(b => b.ProductId, x._id).Create());
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

        public async Task DeleteAsync(Product entity) => await _products.DeleteOneAsync(x => x._id == entity._id);

        public async Task<string> DeleteAuxItemAsync(AuxilliaryItem entity)
        {
            var product = await GetByIdAsync(entity.ProductId);
            product.AuxilliaryItems.Remove(entity);
            await UpdateAsync(product);
            return product._id;
        }

        public async Task<IReadOnlyList<Product>> GetAllAsync() => (await _products.FindAsync(x => true)).ToList();

        public async Task<IEnumerable<AuxilliaryItem>> GetAllAuxItemsAsync(string productId)
        {
            var product = await GetByIdAsync(productId);
            return product.AuxilliaryItems;
        }

        public async Task<AuxilliaryItem> GetAuxItemByIdAsync(string productId, int id)
        {
            var product = await GetByIdAsync(productId);
            return product.AuxilliaryItems.Single(x => x.Id == id);
        }

        public async Task<Product> GetByIdAsync(string id) => (await _products.FindAsync(x => x._id == id)).SingleOrDefault();

        public async Task<decimal> GetCostOfAllAuxItemsAsync(string productId)
        {
            var product = await GetByIdAsync(productId);
            return product.AuxilliaryItems.Sum(x => x.Cost);
        }

        public async Task<IEnumerable<Product>> GetProductByType(ProductType type) => (await _products.FindAsync(x => x.Type == type)).ToList();

        public async Task UpdateAsync(Product entity) => await _products.ReplaceOneAsync(x => x._id == entity._id, entity);

        public async Task<string> UpdateAuxItemAsync(AuxilliaryItem entity)
        {
            var product = await GetByIdAsync(entity.ProductId);
            int index = product.AuxilliaryItems.IndexOf(entity);
            bool found = (index != -1);
            if (found)
            {
                product.AuxilliaryItems[index] = entity;
            }
            await UpdateAsync(product);
            return product._id;
        }
    }
}