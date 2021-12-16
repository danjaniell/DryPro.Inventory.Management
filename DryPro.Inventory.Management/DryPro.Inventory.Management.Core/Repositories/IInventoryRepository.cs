using DryPro.Inventory.Management.Common.Enums;
using DryPro.Inventory.Management.Core.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DryPro.Inventory.Management.Core.Repositories
{
    public interface IInventoryRepository : IRepository<Entities.Inventory>
    {
        Task<long> UpdateAllAsync(IEnumerable<Entities.Inventory> entities);
    }
}