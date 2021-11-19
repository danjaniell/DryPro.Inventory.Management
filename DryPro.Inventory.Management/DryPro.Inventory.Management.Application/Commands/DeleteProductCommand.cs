using DryPro.Inventory.Management.Application.Responses;
using DryPro.Inventory.Management.Common.Enums;
using DryPro.Inventory.Management.Core.Entities;
using MediatR;
using System.Collections.Generic;

namespace DryPro.Inventory.Management.Application.Commands
{
    public class DeleteProductCommand : IRequest<int?>
    {
        public int Id { get; set; }
    }
}