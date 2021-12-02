using DryPro.Inventory.Management.Common.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DryPro.Inventory.Management.Core.Entities
{
    public class Product
    {
        private int _guid;

        [BsonId]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public int Guid
        {
            get
            {
                if (!int.TryParse(Id, out _guid)) { return 0; }
                return _guid;
            }
            set
            {
            }
        }
        public ProductType Type { get; set; }
        public ProductColor Color { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal SoldPrice { get; set; }
        public decimal Cost { get; set; }
        public decimal TotalCost => Cost + AuxilliaryItems.Sum(x => x.Cost);
        public decimal Discount { get; set; }
        public IList<AuxilliaryItem> AuxilliaryItems { get; set; } = new List<AuxilliaryItem>();
    }
}