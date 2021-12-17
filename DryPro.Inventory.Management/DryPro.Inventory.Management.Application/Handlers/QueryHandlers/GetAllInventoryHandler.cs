using DryPro.Inventory.Management.Application.Queries.Inventory;
using DryPro.Inventory.Management.Core.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DryPro.Inventory.Management.Application.Handlers.QueryHandlers
{
    public class GetAllInventoryHandler : IRequestHandler<GetAllInventoryQuery, List<Core.Entities.Inventory>>
    {
        private readonly IInventoryRepository _invRepo;

        public GetAllInventoryHandler(IInventoryRepository invRepo)
        {
            _invRepo = invRepo;
        }

        public async Task<List<Core.Entities.Inventory>> Handle(GetAllInventoryQuery request, CancellationToken cancellationToken) => (List<Core.Entities.Inventory>)await _invRepo.GetAllAsync();
    }
}