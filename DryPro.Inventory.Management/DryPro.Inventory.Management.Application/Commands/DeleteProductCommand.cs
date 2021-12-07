using MediatR;

namespace DryPro.Inventory.Management.Application.Commands
{
    public class DeleteProductCommand : IRequest<string>
    {
        public string Id { get; set; }
    }
}