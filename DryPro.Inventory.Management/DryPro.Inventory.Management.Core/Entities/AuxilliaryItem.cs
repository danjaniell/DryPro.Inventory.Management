using DryPro.Inventory.Management.Common.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DryPro.Inventory.Management.Core.Entities
{
    public class AuxilliaryItem : IEquatable<AuxilliaryItem>
    {
        public int Id { get; set; }
        [BsonId]
        [BsonRequired]
        [BsonRepresentation(BsonType.ObjectId)]
        [ForeignKey("ProductId")]
        public string ProductId { get; set; }
        public AuxItemCategory? Category { get; set; }
        public decimal Cost { get; set; }
        public string Description { get; set; }

        public bool Equals(AuxilliaryItem other) => this.Id == other.Id;
    }
}