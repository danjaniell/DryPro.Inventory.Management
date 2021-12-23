using DryPro.Inventory.Management.Application.Responses;
using DryPro.Inventory.Management.Common.Enums;
using MediatR;

namespace DryPro.Inventory.Management.Application.Commands
{
    public class CreateAuxilliaryItemCommand : IRequest<AuxilliaryItemResponse>
    {
        public int Id { get; set; }
        public string ProductId { get; set; }
        public AuxItemCategory Category { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
    }
}