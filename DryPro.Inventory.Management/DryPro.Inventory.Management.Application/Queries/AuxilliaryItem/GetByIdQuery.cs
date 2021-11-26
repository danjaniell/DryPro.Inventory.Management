using MediatR;
using System.Collections.Generic;

namespace DryPro.Inventory.Management.Application.Queries.AuxilliaryItem
{
    public class GetByIdQuery : IRequest<Core.Entities.AuxilliaryItem>
    {
        public GetByIdQuery(int productId)
        {
            ProductId = productId;
        }

        public int Id { get; set; }

        public int ProductId { get; }
    }
}