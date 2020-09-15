using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using AutoMapper;
using Core.Dtos;
using Core.Helpers;

namespace Core.Application
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductBrand> _brandRepo;
        private readonly IGenericRepository<ProductType> _typeRepo;
        private readonly IMapper _mapper;

        public ProductService(IGenericRepository<Product> productRepo,
            IGenericRepository<ProductBrand> brandRepo, IGenericRepository<ProductType> typeRepo,
            IMapper mapper)
        {
            _typeRepo = typeRepo;
            _brandRepo = brandRepo;
            _productRepo = productRepo;
            _mapper = mapper;
        }

        public async Task<ProductToReturnDto> GetProductAsync(int id)
        {
            var spec = new ProductsWithBrandsAndTypesSpec(id);
            var product = await _productRepo.GetEntityWtihSpecAsync(spec);

            return _mapper.Map<Product, ProductToReturnDto>(product);
        }

        public async Task<Pagination<ProductToReturnDto>> GetProductsAsync(ProductSpecParams productParams)
        {
            var spec = new ProductsWithBrandsAndTypesSpec(productParams);
            var products = await _productRepo.GetAllWithSpecAsync(spec);

            var mappedProducts = _mapper
               .Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

            var countSpec = new ProductsWithFiltersForCountSpec(productParams);
            var totalProducts = await _productRepo.CountWithSpecAsync(countSpec);

            return new Pagination<ProductToReturnDto>
                (productParams.PageIndex, productParams.PageSize, totalProducts, mappedProducts);
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            var productBrands = await _brandRepo.GetAllAsync();
            return productBrands;
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            var productTypes = await _typeRepo.GetAllAsync();
            return productTypes;
        }


    }
}