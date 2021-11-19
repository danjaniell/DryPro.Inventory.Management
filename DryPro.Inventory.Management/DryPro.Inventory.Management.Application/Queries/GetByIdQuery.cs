using MediatR;
using System.Collections.Generic;

namespace DryPro.Inventory.Management.Application.Queries
{
    public class GetByIdQuery : IRequest<Core.Entities.Product>
    {
        public int Id { get; set; }
    }
}