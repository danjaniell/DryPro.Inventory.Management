using DryPro.Inventory.Management.Common.Enums;
using DryPro.Inventory.Management.Core.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DryPro.Inventory.Management.Core.Repositories
{
    public interface IProductRepository : IRepository<Entities.Product>
    {
        Task<IEnumerable<Entities.Product>> GetProductByType(ProductType type);

        Task<Entities.AuxilliaryItem> AddAuxItemAsync(Entities.AuxilliaryItem entity);

        Task<string> DeleteAuxItemAsync(Entities.AuxilliaryItem entity);

        Task<IEnumerable<Entities.AuxilliaryItem>> GetAllAuxItemsAsync(string productId);

        Task<Entities.AuxilliaryItem> GetAuxItemByIdAsync(string productId, int id);

        Task<decimal> GetCostOfAllAuxItemsAsync(string productId);

        Task<string> UpdateAuxItemAsync(Entities.AuxilliaryItem entity);

        Task<string> ClearAllAndGenerateRandomData();
    }
}