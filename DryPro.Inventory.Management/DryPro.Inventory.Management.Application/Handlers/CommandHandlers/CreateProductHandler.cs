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
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, ProductResponse>
    {
        private readonly IProductRepository _productRepo;

        public CreateProductHandler(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }

        public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var productEntity = ProductMapper.Mapper.Map<Core.Entities.Product>(request);

            if (productEntity is null)
            {
                throw new ApplicationException("Issue encountered in Mapper");
            }

            var newProduct = await _productRepo.AddAsync(productEntity);
            var productResponse = ProductMapper.Mapper.Map<ProductResponse>(newProduct);
            return productResponse;
        }
    }
}