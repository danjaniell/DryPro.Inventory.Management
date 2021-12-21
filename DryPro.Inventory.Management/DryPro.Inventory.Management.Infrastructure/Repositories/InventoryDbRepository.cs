using AutoFixture;
using DryPro.Inventory.Management.Core.Repositories;
using DryPro.Inventory.Management.Infrastructure.Data;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DryPro.Inventory.Management.Infrastructure.Repositories
{
    public class InventoryDbRepository : IInventoryRepository
    {
        private readonly IMongoCollection<Core.Entities.Inventory> _inventory;

        public InventoryDbRepository(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _inventory = database.GetCollection<Core.Entities.Inventory>("Inventory");
        }

        public async Task DeleteAll() => await _inventory.DeleteManyAsync(x => true);

        public async Task<Core.Entities.Inventory> AddAsync(Core.Entities.Inventory entity)
        {
            await _inventory.InsertOneAsync(entity);
            return entity;
        }

        public async Task DeleteAsync(Core.Entities.Inventory entity) => await _inventory.DeleteOneAsync(x => x._id == entity._id);

        public async Task<IReadOnlyList<Core.Entities.Inventory>> GetAllAsync() => (await _inventory.FindAsync(x => true)).ToList();

        public async Task<Core.Entities.Inventory> GetByIdAsync(string id) => (await _inventory.FindAsync(x => x._id == id)).SingleOrDefault();

        public async Task UpdateAsync(Core.Entities.Inventory entity) => await _inventory.ReplaceOneAsync(x => x._id == entity._id, entity);

        public async Task<long> UpdateAllAsync(IEnumerable<Core.Entities.Inventory> entities)
        {
            var updates = new List<WriteModel<Core.Entities.Inventory>>();

            foreach (var entity in entities)
            {
                var filter = Builders<Core.Entities.Inventory>.Filter.Eq(x =>x._id, entity._id);
                updates.Add(new ReplaceOneModel<Core.Entities.Inventory>(filter, entity));
            }

            var result = await _inventory.BulkWriteAsync(updates);
            return result.ModifiedCount;
        }
    }
}