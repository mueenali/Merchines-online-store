using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Specifications;

namespace Core.Interfaces
{
    public interface IProductService
    {
        Task<Product> GetProductAsync(int id);
        Task<IReadOnlyList<Product>> GetProductsAsync(ProductSpecParams productParams);
        Task<int> GetProductsCountAsync(ProductSpecParams productParams);
    }
}