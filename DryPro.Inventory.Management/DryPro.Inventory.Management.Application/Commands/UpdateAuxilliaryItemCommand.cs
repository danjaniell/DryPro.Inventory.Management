using MediatR;
using System.ComponentModel.DataAnnotations;

namespace DryPro.Inventory.Management.Application.Commands
{
    public class UpdateAuxilliaryItemCommand : IRequest<int?>
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
    }
}