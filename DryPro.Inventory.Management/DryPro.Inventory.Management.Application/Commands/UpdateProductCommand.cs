using DryPro.Inventory.Management.Application.Responses;
using DryPro.Inventory.Management.Common.Enums;
using DryPro.Inventory.Management.Core.Entities;
using MediatR;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DryPro.Inventory.Management.Application.Commands
{
    public class UpdateProductCommand : IRequest<string>
    {
        public string Id { get; set; }
        public ProductType Type { get; set; }
        public ProductColor Color { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal SoldPrice { get; set; }
        public decimal Cost { get; set; }
        public decimal Discount { get; set; }
    }
}