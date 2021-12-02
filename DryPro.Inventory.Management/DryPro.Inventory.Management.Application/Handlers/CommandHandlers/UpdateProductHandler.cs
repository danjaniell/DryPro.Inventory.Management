using DryPro.Inventory.Management.Application.Commands;
using DryPro.Inventory.Management.Application.Mappers;
using DryPro.Inventory.Management.Application.Responses;
using MediatR;
using DryPro.Inventory.Management.Core.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DryPro.Inventory.Management.Application.Handlers.CommandHandlers
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, int?>
    {
        private readonly IProductRepository _productRepo;

        public UpdateProductHandler(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }

        public async Task<int?> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var productEntity = ProductMapper.Mapper.Map<Core.Entities.Product>(request);

            var product = await _productRepo.GetByIdAsync(productEntity.Id);

            if (product is null)
            {
                return null;
            }

            await _productRepo.UpdateAsync(productEntity);
            return product.Id;
        }
    }
}