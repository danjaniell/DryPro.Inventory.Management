using MediatR;

namespace DryPro.Inventory.Management.Application.Commands
{
    public class DeleteAuxilliaryItemCommand : IRequest<int?>
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
    }
}