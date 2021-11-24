using DryPro.Inventory.Management.Application.Responses;
using DryPro.Inventory.Management.Common.Enums;
using DryPro.Inventory.Management.Core.Entities;
using MediatR;
using System.Collections.Generic;

namespace DryPro.Inventory.Management.Application.Commands
{
    public class CreateAuxilliaryItemCommand : IRequest<AuxilliaryItemResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
    }
}