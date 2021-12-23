using DryPro.Inventory.Management.Common.Enums;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace DryPro.Inventory.Management.Application.Commands
{
    public class UpdateAuxilliaryItemCommand : IRequest<string>
    {
        [Key]
        public int Id { get; set; }
        public string ProductId { get; set; }
        public AuxItemCategory Category { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
    }
}