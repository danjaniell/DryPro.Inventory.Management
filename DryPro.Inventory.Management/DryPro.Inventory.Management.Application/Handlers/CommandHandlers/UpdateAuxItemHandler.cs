using DryPro.Inventory.Management.Application.Commands;
using DryPro.Inventory.Management.Application.Mappers;
using DryPro.Inventory.Management.Core.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DryPro.Inventory.Management.Application.Handlers.CommandHandlers
{
    public class UpdateAuxItemHandler : IRequestHandler<UpdateAuxilliaryItemCommand, int?>
    {
        private readonly IProductRepository _productRepo;

        public UpdateAuxItemHandler(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }

        public async Task<int?> Handle(UpdateAuxilliaryItemCommand request, CancellationToken cancellationToken)
        {
            var auxItemEntity = AuxItemMapper.Mapper.Map<Core.Entities.AuxilliaryItem>(request);

            var auxItem = await _productRepo.GetByIdAsync(auxItemEntity.Id);

            if (auxItem is null)
            {
                return null;
            }

            await _productRepo.UpdateAuxItemAsync(auxItemEntity);
            return auxItem.Id;
        }
    }
}