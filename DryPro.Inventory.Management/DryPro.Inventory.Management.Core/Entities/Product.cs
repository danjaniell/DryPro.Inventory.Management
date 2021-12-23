using DryPro.Inventory.Management.Common.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DryPro.Inventory.Management.Core.Entities
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public ProductType Type { get; set; }
        public ProductColor Color { get; set; }
        [DisplayName("Selling Price")]
        public decimal SellingPrice { get; set; }
        [DisplayName("Sold Price")]
        public decimal SoldPrice { get; set; }
        [DisplayName("Product Cost")]
        public decimal Cost { get; set; }
        [DisplayName("Total Cost")]
        public decimal TotalCost => Cost + AuxilliaryItems.Sum(x => x.Cost);
        public decimal Discount { get; set; }
        [DisplayName("Auxilliary Items")]
        public IList<AuxilliaryItem> AuxilliaryItems { get; set; } = new List<AuxilliaryItem>();
    }
}