using DryPro.Inventory.Management.Application.Responses;
using DryPro.Inventory.Management.Common.Enums;
using DryPro.Inventory.Management.Core.Entities;
using MediatR;
using System.Collections.Generic;

namespace DryPro.Inventory.Management.Application.Commands
{
    public class CreateProductCommand : IRequest<ProductResponse>
    {
        public int Id { get; set; }
        public ProductType Type { get; set; }
        public ProductColor Color { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal SoldPrice { get; set; }
        public decimal Cost { get; set; }
        public decimal Discount { get; set; }
        public IEnumerable<AuxilliaryItem> AuxilliaryItems { get; set; }
    }
}