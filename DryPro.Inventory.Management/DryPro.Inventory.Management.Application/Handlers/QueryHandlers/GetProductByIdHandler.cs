using DryPro.Inventory.Management.Application.Queries.Product;
using DryPro.Inventory.Management.Core.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DryPro.Inventory.Management.Application.Handlers.QueryHandlers
{
    public class GetProductByIdHandler : IRequestHandler<GetByIdQuery, Core.Entities.Product>
    {
        private readonly IProductRepository _productRepo;

        public GetProductByIdHandler(IProductRepository productRepository)
        {
            _productRepo = productRepository;
        }

        public async Task<Core.Entities.Product> Handle(GetByIdQuery request, CancellationToken cancellationToken) => await _productRepo.GetByIdAsync(request.Id);
    }
}