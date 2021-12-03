using MediatR;
using System.Collections.Generic;

namespace DryPro.Inventory.Management.Application.Queries.AuxilliaryItem
{
    public class GetAllAuxItemsQuery : IRequest<List<Core.Entities.AuxilliaryItem>>
    {
        public GetAllAuxItemsQuery(string productId)
        {
            ProductId = productId;
        }

        public string ProductId { get; }
    }
}