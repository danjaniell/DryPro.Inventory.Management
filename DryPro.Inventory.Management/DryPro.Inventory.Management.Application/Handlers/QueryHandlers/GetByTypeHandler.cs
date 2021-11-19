using DryPro.Inventory.Management.Application.Queries;
using MediatR;
using DryPro.Inventory.Management.Core.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DryPro.Inventory.Management.Application.Handlers.QueryHandlers
{
    public class GetByTypeHandler : IRequestHandler<GetByTypeQuery, List<Core.Entities.Product>>
    {
        private readonly IProductRepository _productRepo;

        public GetByTypeHandler(IProductRepository productRepository)
        {
            _productRepo = productRepository;
        }

        public async Task<List<Core.Entities.Product>> Handle(GetByTypeQuery request, CancellationToken cancellationToken) => (List<Core.Entities.Product>)await _productRepo.GetProductByType(request.Type);
    }
}