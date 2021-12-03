using MediatR;

namespace DryPro.Inventory.Management.Application.Commands
{
    public class DeleteAuxilliaryItemCommand : IRequest<string>
    {
        public int Id { get; set; }
        public string ProductId { get; set; }
    }
}