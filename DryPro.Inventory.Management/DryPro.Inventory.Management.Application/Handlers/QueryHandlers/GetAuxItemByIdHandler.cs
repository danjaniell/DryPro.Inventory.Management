using DryPro.Inventory.Management.Application.Queries.AuxilliaryItem;
using DryPro.Inventory.Management.Core.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DryPro.Inventory.Management.Application.Handlers.QueryHandlers
{
    public class GetAuxItemByIdHandler : IRequestHandler<GetByIdQuery, Core.Entities.AuxilliaryItem>
    {
        private readonly IProductRepository _productRepo;

        public GetAuxItemByIdHandler(IProductRepository productRepository)
        {
            _productRepo = productRepository;
        }

        public async Task<Core.Entities.AuxilliaryItem> Handle(GetByIdQuery request, CancellationToken cancellationToken) => await _productRepo.GetAuxItemByIdAsync(request.ProductId, request.Id);
    }
}