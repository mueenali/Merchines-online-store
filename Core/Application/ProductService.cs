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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProductToReturnDto> GetProductAsync(int id)
        {
            var spec = new ProductsWithBrandsAndTypesSpec(id);
            var product = await _unitOfWork.Repository<Product>().GetEntityWtihSpecAsync(spec);

            return _mapper.Map<Product, ProductToReturnDto>(product);
        }

        public async Task<Pagination<ProductToReturnDto>> GetProductsAsync(ProductSpecParams productParams)
        {
            var spec = new ProductsWithBrandsAndTypesSpec(productParams);
            var products = await _unitOfWork.Repository<Product>()
                .GetAllWithSpecAsync(spec);

            var mappedProducts = _mapper
               .Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

            var countSpec = new ProductsWithFiltersForCountSpec(productParams);
            var totalProducts = await _unitOfWork.Repository<Product>().CountWithSpecAsync(countSpec);

            return new Pagination<ProductToReturnDto>
                (productParams.PageIndex, productParams.PageSize, totalProducts, mappedProducts);
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            var productBrands = await _unitOfWork.Repository<ProductBrand>().GetAllAsync();
            return productBrands;
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            var productTypes = await _unitOfWork.Repository<ProductType>().GetAllAsync();
            return productTypes;
        }


    }
}