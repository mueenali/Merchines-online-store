using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Dtos;
using Core.Entities;
using Core.Helpers;
using Core.Specifications;

namespace Core.Interfaces
{
    public interface IProductService
    {
        Task<ProductToReturnDto> GetProductAsync(int id);
        Task<Pagination<ProductToReturnDto>> GetProductsAsync(ProductSpecParams productParams);
        Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync();
        Task<IReadOnlyList<ProductType>> GetProductTypesAsync();
        Task<ProductToReturnDto> AddProduct(CreateProductDto productDto);
    }
}