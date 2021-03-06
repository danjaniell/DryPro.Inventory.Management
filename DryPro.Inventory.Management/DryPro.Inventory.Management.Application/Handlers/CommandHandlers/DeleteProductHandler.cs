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
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, string>
    {
        private readonly IProductRepository _productRepo;

        public DeleteProductHandler(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }

        public async Task<string> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepo.GetByIdAsync(request.Id);

            if (product is null)
            {
                return null;
            }

            await _productRepo.DeleteAsync(product);
            return product._id;
        }
    }
}