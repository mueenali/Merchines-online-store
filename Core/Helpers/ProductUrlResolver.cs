using Core.Dtos;
using AutoMapper;
using Core.Entities;


namespace Core.Helpers
{
    public class ProductUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {

        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return "https://localhost:5001/" + source.PictureUrl;
            }

            return null;
        }
    }
}