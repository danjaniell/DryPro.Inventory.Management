
using DryPro.Inventory.Management.Common.Enums;

namespace DryPro.Inventory.Management.Application.Responses
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public ProductType Type { get; set; }
        public ProductColor Color { get; set; }
    }
}