using MediatR;
using System.Collections.Generic;

namespace DryPro.Inventory.Management.Application.Queries.Inventory
{
    public class GetAllInventoryQuery : IRequest<List<Core.Entities.Inventory>>
    {
    }
}