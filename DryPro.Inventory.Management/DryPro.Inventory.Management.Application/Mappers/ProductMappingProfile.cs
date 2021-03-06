using AutoMapper;
using DryPro.Inventory.Management.Application.Commands;
using DryPro.Inventory.Management.Application.Responses;

namespace DryPro.Inventory.Management.Application.Mappers
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Core.Entities.Product, ProductResponse>().ReverseMap();
            CreateMap<Core.Entities.Product, Core.Entities.Inventory>().ReverseMap();
            CreateMap<Core.Entities.Product, CreateProductCommand>().ReverseMap();
            CreateMap<Core.Entities.Product, UpdateProductCommand>().ReverseMap();
            CreateMap<Core.Entities.Product, DeleteProductCommand>().ReverseMap();
        }
    }
}