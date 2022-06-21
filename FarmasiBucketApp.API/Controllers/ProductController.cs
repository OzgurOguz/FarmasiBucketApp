using FarmasiBucketApp.Core.Dtos.Product;
using FarmasiBucketApp.Core.Dtos.Shared;
using FarmasiBucketApp.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmasiBucketApp.API.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        public readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<ResponseDto<ProductDto>> CreateAsync(ProductCreateDto productCreateDto)
        {
            return await _productService.CreateAsync(productCreateDto);
        }

        [HttpDelete]
        public async Task<ResponseDto<NoContent>> DeleteAsync(string id)
        {
            return await _productService.DeleteAsync(id);
        }

        [HttpGet]
        public async Task<ResponseDto<List<ProductDto>>> GetAllAsync()
        {
            return await _productService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ResponseDto<ProductDto>> GetByIdAsyncAsync(string id)
        {
            return await _productService.GetByIdAsyncAsync(id);
        }

        [HttpPut]
        public async Task<ResponseDto<NoContent>> UpdateAsync(ProductUpdateDto productUpdateDto)
        {
            return await _productService.UpdateAsync(productUpdateDto);
        }
    }
}
