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
    public class DeleteAllAndGenerateRandomDataHandler : IRequestHandler<DeleteAllAndGenerateRandomDataCommand, string>
    {
        private readonly IProductRepository _productRepo;

        public DeleteAllAndGenerateRandomDataHandler(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }

        public async Task<string> Handle(DeleteAllAndGenerateRandomDataCommand request, CancellationToken cancellationToken) => await _productRepo.ClearAllAndGenerateRandomData();
    }
}