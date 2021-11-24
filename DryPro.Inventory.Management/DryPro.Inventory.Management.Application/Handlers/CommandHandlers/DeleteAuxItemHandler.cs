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
    public class DeleteAuxItemHandler : IRequestHandler<DeleteAuxilliaryItemCommand, int?>
    {
        private readonly IProductRepository _productRepo;

        public DeleteAuxItemHandler(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }

        public async Task<int?> Handle(DeleteAuxilliaryItemCommand request, CancellationToken cancellationToken)
        {
            var auxItemEntity = AuxItemMapper.Mapper.Map<Core.Entities.AuxilliaryItem>(request);
            var auxItem = await _productRepo.GetByIdAsync(auxItemEntity.Id);

            if (auxItem is null)
            {
                return null;
            }

            await _productRepo.DeleteAsync(auxItem);
            return auxItem.Id;
        }
    }
}