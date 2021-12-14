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

        public async Task<string> ClearAllAndGenerateRandomData()
        {
            try
            {
                await DeleteAll();
                if ((await GetAllAsync()).Count == 0)
                {
                    var fixture = new Fixture();
                    var inventoryBuilder = fixture.Build<Core.Entities.Inventory>();
                    var inventory = inventoryBuilder.Do(a => a._id = ObjectId.GenerateNewId().ToString())
                                                    .CreateMany(10)
                                                    .ToList();
                    inventory.ForEach(async x =>
                    {
                        x._id = new string(x._id.Replace("-", string.Empty).Skip(3).Take(24).ToArray()); //remove prefix "_id"
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
    }
}