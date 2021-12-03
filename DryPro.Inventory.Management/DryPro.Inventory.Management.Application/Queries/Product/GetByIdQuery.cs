using MediatR;

namespace DryPro.Inventory.Management.Application.Queries.Product
{
    public class GetByIdQuery : IRequest<Core.Entities.Product>
    {
        public string Id { get; set; }
    }
}