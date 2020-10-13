using Microsoft.AspNetCore.Http;

namespace Core.Dtos
{
    public class CreateProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public IFormFile Picture { get; set; }
        public int ProductType { get; set; }
        public int ProductTypeId { get; set; }
        public int ProductBrand { get; set; }
        public int ProductBrandId { get; set; }
    }
}