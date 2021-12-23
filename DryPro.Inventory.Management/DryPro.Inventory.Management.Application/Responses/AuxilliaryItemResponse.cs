
using DryPro.Inventory.Management.Common.Enums;

namespace DryPro.Inventory.Management.Application.Responses
{
    public class AuxilliaryItemResponse
    {
        public int Id { get; set; }
        public AuxItemCategory Category { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
    }
}