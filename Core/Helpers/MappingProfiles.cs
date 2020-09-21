using Core.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;

namespace Core.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
            .ForMember(d => d.ProductBrand, o => o.MapFrom(p => p.ProductBrand.Name))
            .ForMember(d => d.ProductType, o => o.MapFrom(p => p.ProductType.Name))
            .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<CartDto, Cart>().ReverseMap();
            CreateMap<CartItemDto, CartItem>().ReverseMap();
            CreateMap<AddressDto, Entities.OrderAggregate.Address>().ReverseMap();
        }
    } 
}
