using DryPro.Inventory.Management.Application.Queries.AuxilliaryItem;
using DryPro.Inventory.Management.Core.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DryPro.Inventory.Management.Application.Handlers.QueryHandlers
{
    public class GetAllAuxItemsHandler : IRequestHandler<GetAllAuxItemsQuery, List<Core.Entities.AuxilliaryItem>>
    {
        private readonly IProductRepository _productRepo;

        public GetAllAuxItemsHandler(IProductRepository productRepository)
        {
            _productRepo = productRepository;
        }

        public async Task<List<Core.Entities.AuxilliaryItem>> Handle(GetAllAuxItemsQuery request, CancellationToken cancellationToken) => (List<Core.Entities.AuxilliaryItem>)await _productRepo.GetAllAuxItemsAsync(request.Id);
    }
}