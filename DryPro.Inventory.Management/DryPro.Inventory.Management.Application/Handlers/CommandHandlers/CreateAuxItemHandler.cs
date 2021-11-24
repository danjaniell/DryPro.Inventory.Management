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
    public class CreateAuxItemHandler : IRequestHandler<CreateAuxilliaryItemCommand, AuxilliaryItemResponse>
    {
        private readonly IProductRepository _productRepo;

        public CreateAuxItemHandler(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }

        public async Task<AuxilliaryItemResponse> Handle(CreateAuxilliaryItemCommand request, CancellationToken cancellationToken)
        {
            var auxItemEntity = AuxItemMapper.Mapper.Map<Core.Entities.AuxilliaryItem>(request);

            if (auxItemEntity is null)
            {
                throw new ApplicationException("Issue encountered in Mapper");
            }

            var newAuxItem = await _productRepo.AddAuxItemAsync(auxItemEntity);
            var auxItemResponse = AuxItemMapper.Mapper.Map<AuxilliaryItemResponse>(newAuxItem);
            return auxItemResponse;
        }
    }
}