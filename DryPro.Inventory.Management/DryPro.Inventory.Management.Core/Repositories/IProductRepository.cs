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

        Task<int?> DeleteAuxItemAsync(Entities.AuxilliaryItem entity);

        Task<IEnumerable<Entities.AuxilliaryItem>> GetAllAuxItemsAsync(int productId);

        Task<Entities.AuxilliaryItem> GetAuxItemByIdAsync(int productId, int id);

        Task<decimal> GetCostOfAllAuxItemsAsync(int productId);

        Task<int?> UpdateAuxItemAsync(Entities.AuxilliaryItem entity);
    }
}