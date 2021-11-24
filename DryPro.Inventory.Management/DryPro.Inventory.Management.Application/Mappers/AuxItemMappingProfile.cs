using AutoMapper;
using DryPro.Inventory.Management.Application.Commands;
using DryPro.Inventory.Management.Application.Responses;

namespace DryPro.Inventory.Management.Application.Mappers
{
    public class AuxItemMappingProfile : Profile
    {
        public AuxItemMappingProfile()
        {
            CreateMap<Core.Entities.AuxilliaryItem, AuxilliaryItemResponse>().ReverseMap();
            CreateMap<Core.Entities.AuxilliaryItem, CreateAuxilliaryItemCommand>().ReverseMap();
            CreateMap<Core.Entities.AuxilliaryItem, UpdateAuxilliaryItemCommand>().ReverseMap();
            CreateMap<Core.Entities.AuxilliaryItem, DeleteAuxilliaryItemCommand>().ReverseMap();
        }
    }
}