using DryPro.Inventory.Management.Common.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DryPro.Inventory.Management.Core.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public ProductType Type { get; set; }
        public ProductColor Color { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal SoldPrice { get; set; }
        public decimal Cost { get; set; }
        public decimal Discount { get; set; }
        public ICollection<AuxilliaryItem> AuxilliaryItems { get; set; } = new List<AuxilliaryItem>();
    }
}