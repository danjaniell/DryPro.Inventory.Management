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
    public class SaveOneInventoryHandler : IRequestHandler<SaveInventoryCommand, string>
    {
        private readonly IInventoryRepository _invRepo;

        public SaveOneInventoryHandler(IInventoryRepository invRepo)
        {
            _invRepo = invRepo;
        }

        public async Task<string> Handle(SaveInventoryCommand request, CancellationToken cancellationToken)
        {
            var inventoryEntity = InventoryMapper.Mapper.Map<Core.Entities.Inventory>(request);

            var item = await _invRepo.GetByIdAsync(inventoryEntity._id);

            if (item is null)
            {
                await _invRepo.AddAsync(inventoryEntity);
                return inventoryEntity._id;
            }

            await _invRepo.UpdateAsync(inventoryEntity);
            return item._id;
        }
    }
}