using MediatR;
using System.Collections.Generic;

namespace DryPro.Inventory.Management.Application.Queries.Product
{
    public class GetAllProductsQuery : IRequest<List<Core.Entities.Product>>
    {
    }
}