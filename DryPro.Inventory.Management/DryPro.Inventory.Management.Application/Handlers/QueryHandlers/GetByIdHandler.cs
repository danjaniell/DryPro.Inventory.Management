using DryPro.Inventory.Management.Application.Queries;
using MediatR;
using DryPro.Inventory.Management.Core.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DryPro.Inventory.Management.Application.Queries.Product;

namespace DryPro.Inventory.Management.Application.Handlers.QueryHandlers
{
    public class GetByIdHandler : IRequestHandler<GetByIdQuery, Core.Entities.Product>
    {
        private readonly IProductRepository _productRepo;

        public GetByIdHandler(IProductRepository productRepository)
        {
            _productRepo = productRepository;
        }

        public async Task<Core.Entities.Product> Handle(GetByIdQuery request, CancellationToken cancellationToken) => await _productRepo.GetByIdAsync(request.Id);
    }
}