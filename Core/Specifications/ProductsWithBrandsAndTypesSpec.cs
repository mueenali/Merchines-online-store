
using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithBrandsAndTypesSpec : BaseSpecification<Product>
    {
        public ProductsWithBrandsAndTypesSpec(ProductSpecParams productParams)
        : base(p => (string.IsNullOrEmpty(productParams.Search) ||
                 p.Name.ToLower().Contains(productParams.Search)) &&
                 (!productParams.TypeId.HasValue || p.ProductTypeId == productParams.TypeId) &&
                 (!productParams.BrandId.HasValue || p.ProductBrandId == productParams.BrandId))
        {

            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
            ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1),
            productParams.PageSize);

            if (!string.IsNullOrEmpty(productParams.SortQuery))
            {
                switch (productParams.SortQuery)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }
        }

        public ProductsWithBrandsAndTypesSpec(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
    }
}