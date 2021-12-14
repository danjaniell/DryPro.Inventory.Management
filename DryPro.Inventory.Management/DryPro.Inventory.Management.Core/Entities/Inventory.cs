using DryPro.Inventory.Management.Common.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DryPro.Inventory.Management.Core.Entities
{
    public class Inventory
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public ProductType Type { get; set; }
        public ProductColor Color { get; set; }
        public int Remaining { get; set; }
        public int Sold { get; set; }
    }
}