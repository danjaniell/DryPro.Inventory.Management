using MediatR;
using DryPro.Inventory.Management.Common.Enums;

namespace DryPro.Inventory.Management.Application.Commands
{
    public class SaveInventoryCommand : IRequest<string>
    {
        public string Id { get; set; }
        public ProductType Type { get; set; }
        public ProductColor Color { get; set; }
        public int Remaining { get; set; }
        public int Sold { get; set; }
    }
}