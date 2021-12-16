using MediatR;
using System.Collections.Generic;

namespace DryPro.Inventory.Management.Application.Commands
{
    public class SaveAllInventoryCommand : IRequest<long>
    {
        public IEnumerable<SaveInventoryCommand> ItemsToSave { get; set; }
    }
}