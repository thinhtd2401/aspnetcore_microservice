using AutoMapper;
using Infrastructure.Mapping;
using Shared.DTOs.Product;

namespace Product.API.AutoMapper;
public class MappingProfile : Profile
{

    public MappingProfile()
    {
        CreateMap<Entities.Product, ProductDto>();
        CreateMap<CreateProductDto, Entities.Product>();
        CreateMap<UpdateProductDto, Entities.Product>().IgnoreAllNonExisting();
    }
    
}