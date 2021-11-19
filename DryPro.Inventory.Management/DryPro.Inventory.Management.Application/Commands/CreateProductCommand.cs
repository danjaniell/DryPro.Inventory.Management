using DryPro.Inventory.Management.Application.Responses;
using MediatR;

namespace DryPro.Inventory.Management.Application.Commands
{
    public class CreateProductCommand : IRequest<ProductResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}