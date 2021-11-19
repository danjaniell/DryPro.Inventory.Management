using System.Collections.Generic;
using System.Threading.Tasks;

namespace DryPro.Inventory.Management.Core.Repositories.Base
{
    public interface IRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);

        Task DeleteAsync(T entity);

        Task<IReadOnlyList<T>> GetAllAsync();

        Task<T> GetByIdAsync(int id);

        Task UpdateAsync(T entity);
    }
}