using DryPro.Inventory.Management.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using DryPro.Inventory.Management.Core.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DryPro.Inventory.Management.Infrastructure.Repositories.Base
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ProductContext _productContext;

        public Repository(ProductContext productContext)
        {
            _productContext = productContext;
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            await _productContext.Set<T>().AddAsync(entity);
            await _productContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task DeleteAsync(T entity)
        {
            _productContext.Set<T>().Remove(entity);
            await _productContext.SaveChangesAsync();
        }

        public virtual async Task<IReadOnlyList<T>> GetAllAsync() => await _productContext.Set<T>().ToListAsync();

        public virtual async Task<T> GetByIdAsync(int id) => await _productContext.Set<T>().FindAsync(id);

        public virtual async Task UpdateAsync(T entity)
        {
            _productContext.Set<T>().Update(entity);
            await _productContext.SaveChangesAsync();
        }
    }
}