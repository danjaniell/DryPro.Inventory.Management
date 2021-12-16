using MediatR;

namespace DryPro.Inventory.Management.Application.Commands
{
    public class SaveInventoryCommand : IRequest<string>
    {
        public string Id { get; set; }
        public int Remaining { get; set; }
        public int Sold { get; set; }
    }
}