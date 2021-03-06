using DryPro.Inventory.Management.Application.Commands;
using DryPro.Inventory.Management.Application.Mappers;
using DryPro.Inventory.Management.Core.Repositories;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DryPro.Inventory.Management.Application.Handlers.CommandHandlers
{
    public class UpdateAuxItemHandler : IRequestHandler<UpdateAuxilliaryItemCommand, string>
    {
        private readonly IProductRepository _productRepo;

        public UpdateAuxItemHandler(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }

        public async Task<string> Handle(UpdateAuxilliaryItemCommand request, CancellationToken cancellationToken)
        {
            var auxItemEntity = AuxItemMapper.Mapper.Map<Core.Entities.AuxilliaryItem>(request);

            var auxItem = (await _productRepo.GetAllAuxItemsAsync(auxItemEntity.ProductId)).Single(x => x.Id == auxItemEntity.Id);

            if (auxItem is null)
            {
                return null;
            }

            await _productRepo.UpdateAuxItemAsync(auxItemEntity);
            return auxItem.ProductId;
        }
    }
}