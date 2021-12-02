using DryPro.Inventory.Management.Common.Enums;
using DryPro.Inventory.Management.Core.Entities;
using DryPro.Inventory.Management.Core.Repositories;
using DryPro.Inventory.Management.Infrastructure.Data;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
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

        public async Task<Product> AddAsync(Product entity)
        {
            await _products.InsertOneAsync(entity);
            return entity;
        }

        public Task<AuxilliaryItem> AddAuxItemAsync(AuxilliaryItem entity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Product entity) => await _products.DeleteOneAsync(x => x.Id == entity.Id);

        public Task<int?> DeleteAuxItemAsync(AuxilliaryItem entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<Product>> GetAllAsync() => (await _products.FindAsync(x => true)).ToList();

        public Task<IEnumerable<AuxilliaryItem>> GetAllAuxItemsAsync(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<AuxilliaryItem> GetAuxItemByIdAsync(int productId, int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Product> GetByIdAsync(int id) => (await _products.FindAsync(x => x.Id == id)).SingleOrDefault();

        public Task<decimal> GetCostOfAllAuxItemsAsync(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetProductByType(ProductType type)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Product entity) => await _products.ReplaceOneAsync(x => x.Id == entity.Id, entity);

        public Task<int?> UpdateAuxItemAsync(AuxilliaryItem entity)
        {
            throw new NotImplementedException();
        }
    }
}