using DryPro.Inventory.Management.Common.Enums;
using MediatR;
using System.Collections.Generic;

namespace DryPro.Inventory.Management.Application.Queries
{
    public class GetByTypeQuery : IRequest<List<Core.Entities.Product>>
    {
        public ProductType Type { get; set; }
    }
}