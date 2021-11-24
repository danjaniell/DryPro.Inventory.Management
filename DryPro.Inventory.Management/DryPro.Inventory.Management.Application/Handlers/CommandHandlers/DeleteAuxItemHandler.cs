using DryPro.Inventory.Management.Application.Commands;
using DryPro.Inventory.Management.Application.Mappers;
using DryPro.Inventory.Management.Application.Responses;
using MediatR;
using DryPro.Inventory.Management.Core.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

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
            var auxItem = (await _productRepo.GetAllAuxItemsAsync(auxItemEntity.ProductId)).Single(x=> x.Id == auxItemEntity.Id);

            if (auxItem is null)
            {
                return null;
            }

            await _productRepo.DeleteAuxItemAsync(auxItem);
            return auxItem.Id;
        }
    }
}