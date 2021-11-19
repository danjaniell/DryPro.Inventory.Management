using DryPro.Inventory.Management.Common.Enums;
using DryPro.Inventory.Management.Core.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DryPro.Inventory.Management.Core.Repositories
{
    public interface IProductRepository : IRepository<Entities.Product>
    {
        Task<IEnumerable<Entities.Product>> GetProductByType(ProductType type);
    }
}