using DryPro.Inventory.Management.Application.Commands;
using DryPro.Inventory.Management.Application.Mappers;
using DryPro.Inventory.Management.Application.Responses;
using MediatR;
using DryPro.Inventory.Management.Core.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace DryPro.Inventory.Management.Application.Handlers.CommandHandlers
{
    public class SaveInventoryHandler : IRequestHandler<SaveAllInventoryCommand, long>
    {
        private readonly IInventoryRepository _invRepo;

        public SaveInventoryHandler(IInventoryRepository invRepo)
        {
            _invRepo = invRepo;
        }

        public async Task<long> Handle(SaveAllInventoryCommand request, CancellationToken cancellationToken)
        {
            var itemsToSave = InventoryMapper.Mapper.Map<IEnumerable<SaveInventoryCommand>, List<Core.Entities.Inventory>>(request.ItemsToSave);
            return await _invRepo.UpdateAllAsync(itemsToSave);
        }
    }
}