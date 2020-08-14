using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;

namespace Core.AppServices
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> _productRepo;
        public ProductService(IGenericRepository<Product> productRepo)
        {
            _productRepo = productRepo;
        }

        public async Task<Product> GetProductAsync(int id)
        {
            var spec = new ProductsWithBrandsAndTypesSpec(id);
            var product = await _productRepo.GetEntityWtihSpecAsync(spec);
            return product;
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            var spec = new ProductsWithBrandsAndTypesSpec();
            var products = await _productRepo.GetAllWithSpecAsync(spec);
            return products;
        }
    }
}