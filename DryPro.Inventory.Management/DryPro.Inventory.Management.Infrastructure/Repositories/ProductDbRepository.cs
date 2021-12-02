using DryPro.Inventory.Management.Infrastructure.Data;
using DryPro.Inventory.Management.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using DryPro.Inventory.Management.Core.Entities;
using DryPro.Inventory.Management.Core.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DryPro.Inventory.Management.Common.Enums;
using AutoFixture;
using System;
using DryPro.Inventory.Management.Common.Extensions;
using MongoDB.Driver;

namespace DryPro.Inventory.Management.Infrastructure.Repositories
{
    public class ProductDbRepository : Repository<Core.Entities.Product>, IProductRepository
    {
        private readonly IMongoCollection<Product> _products;

        public ProductDbRepository(IMongoClient client, ProductContext productContext) : base(productContext)
        {
        }

        public Task<AuxilliaryItem> AddAuxItemAsync(AuxilliaryItem entity)
        {
            throw new NotImplementedException();
        }

        public Task<int?> DeleteAuxItemAsync(AuxilliaryItem entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AuxilliaryItem>> GetAllAuxItemsAsync(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<AuxilliaryItem> GetAuxItemByIdAsync(int productId, int id)
        {
            throw new NotImplementedException();
        }

        public Task<decimal> GetCostOfAllAuxItemsAsync(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetProductByType(ProductType type)
        {
            throw new NotImplementedException();
        }

        public Task<int?> UpdateAuxItemAsync(AuxilliaryItem entity)
        {
            throw new NotImplementedException();
        }
    }
}