using DryPro.Inventory.Management.Core.Repositories.Base;
using MongoDB.Driver;

namespace DryPro.Inventory.Management.Core.Repositories
{
    public interface IMongoProductContext : IRepository<Entities.Product>
    {
        IMongoCollection<T> GetCollection<T>(string name);
    }
}