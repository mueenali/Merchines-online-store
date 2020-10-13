using Core.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Address = Core.Entities.Identity.Address;

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
            CreateMap<Order, OrderDto>()
                .ForMember(d => d.DeliveryMethod, om => om.MapFrom(o => o.DeliveryMethod.ShortName))
                .ForMember(d => d.ShippingPrice, mo => mo.MapFrom(o => o.DeliveryMethod.Price));
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductId, om => om.MapFrom(oi => oi.ItemOrdered.ProductId))
                .ForMember(d => d.ProductName, om => om.MapFrom(oi => oi.ItemOrdered.ProductName))
                .ForMember(d => d.PictureUrl, om => om.MapFrom(oi => oi.ItemOrdered.PictureUrl));
        }
    } 
}
