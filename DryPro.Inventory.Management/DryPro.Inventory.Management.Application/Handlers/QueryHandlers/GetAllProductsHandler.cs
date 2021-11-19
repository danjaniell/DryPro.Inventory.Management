using DryPro.Inventory.Management.Application.Queries;
using MediatR;
using DryPro.Inventory.Management.Core.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DryPro.Inventory.Management.Application.Handlers.QueryHandlers
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, List<Core.Entities.Product>>
    {
        private readonly IProductRepository _productRepo;

        public GetAllProductsHandler(IProductRepository productRepository)
        {
            _productRepo = productRepository;
        }

        public async Task<List<Core.Entities.Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken) => (List<Core.Entities.Product>)await _productRepo.GetAllAsync();
    }
}