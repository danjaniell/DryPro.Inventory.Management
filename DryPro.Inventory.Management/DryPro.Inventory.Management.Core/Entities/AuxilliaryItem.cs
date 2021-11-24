using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DryPro.Inventory.Management.Core.Entities
{
    public class AuxilliaryItem : IEquatable<AuxilliaryItem>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [ForeignKey("ProductId")]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }

        public bool Equals(AuxilliaryItem other) => this.Id == other.Id;
    }
}