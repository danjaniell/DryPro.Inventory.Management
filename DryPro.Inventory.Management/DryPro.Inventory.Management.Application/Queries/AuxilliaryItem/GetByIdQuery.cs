using MediatR;

namespace DryPro.Inventory.Management.Application.Queries.AuxilliaryItem
{
    public class GetByIdQuery : IRequest<Core.Entities.AuxilliaryItem>
    {
        public int Id { get; set; }
    }
}