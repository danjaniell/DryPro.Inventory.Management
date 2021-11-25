using DryPro.Inventory.Management.Application.Queries.AuxilliaryItem;
using DryPro.Inventory.Management.Core.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DryPro.Inventory.Management.Application.Handlers.QueryHandlers
{
    public class GetTotalCostHandler : IRequestHandler<GetTotalCostQuery, decimal>
    {
        private readonly IProductRepository _productRepo;

        public GetTotalCostHandler(IProductRepository productRepository)
        {
            _productRepo = productRepository;
        }

        public async Task<decimal> Handle(GetTotalCostQuery request, CancellationToken cancellationToken) => await _productRepo.GetCostOfAllAuxItemsAsync(request.Id);
    }
}