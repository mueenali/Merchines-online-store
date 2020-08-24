using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithFiltersForCountSpec : BaseSpecification<Product>
    {
        public ProductsWithFiltersForCountSpec(ProductSpecParams productParams) :
         base(p => (string.IsNullOrEmpty(productParams.Search) ||
                 p.Name.ToLower().Contains(productParams.Search)) &&
                 (!productParams.TypeId.HasValue || p.ProductTypeId == productParams.TypeId) &&
                 (!productParams.BrandId.HasValue || p.ProductBrandId == productParams.BrandId))
        {
        }
    }
}