using MediatR;
using System.Collections.Generic;

namespace DryPro.Inventory.Management.Application.Queries
{
    public class GetAllProductsQuery : IRequest<List<Core.Entities.Product>>
    {
    }
}