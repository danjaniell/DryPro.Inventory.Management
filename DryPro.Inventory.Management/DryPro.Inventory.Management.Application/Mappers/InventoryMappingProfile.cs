using AutoMapper;
using DryPro.Inventory.Management.Application.Commands;
using DryPro.Inventory.Management.Application.Responses;

namespace DryPro.Inventory.Management.Application.Mappers
{
    public class InventoryMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Core.Entities.Inventory, SaveInventoryCommand>().ReverseMap();
        }
    }
}