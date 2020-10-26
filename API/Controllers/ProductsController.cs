using System.Collections.Generic;
using System.Threading.Tasks;
using API.Errors;
using API.Helpers;
using Core.Dtos;
using Core.Entities;
using Core.Helpers;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductsController : ApiBaseController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Cached(300)]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts(
            [FromQuery] ProductSpecParams productParams)
        {
            var products = await _productService.GetProductsAsync(productParams);

            return Ok(products);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [Cached(300)]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var product = await _productService.GetProductAsync(id);

            if (product == null)
                return NotFound(new ApiResponse(404));

            return Ok(product);
        }

        [HttpGet("brands")]
        [Cached(300)]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            var productBrands = await _productService.GetProductBrandsAsync();
            return Ok(productBrands);
        }

        [HttpGet("types")]
        [Cached(300)]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            var productTypes = await _productService.GetProductTypesAsync();
            return Ok(productTypes);
        }
        
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ProductToReturnDto>> AddProduct(CreateProductDto productDto)
        {
            var product = await _productService.AddProduct(productDto);
            if (product == null)
            {
                return BadRequest(new ApiResponse(400, "Problem in creating the product"));
            }
            
            return Ok(product);
        }
        
    }

}