using FarmasiBucketApp.Core.Dtos.Product;
using FarmasiBucketApp.Core.Dtos.Shared;
using FarmasiBucketApp.Core.Interfaces;
using FarmasiBucketApp.Core.Models;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmasiBucketApp.Service
{
    public class BucketService : IBucketService
    {
        public readonly IDistributedCache _distributedCache;
        public readonly IProductService _productService;

        public BucketService(IDistributedCache distributedCache, IProductService productService)
        {
            _distributedCache = distributedCache;
            _productService = productService;
        }

        public async Task<ResponseDto<Product>> AddBucket(Product product)
        {
            try
            {
                var hasProduct = await _productService.GetByIdAsyncAsync(product.Id);
            }
            catch (Exception)
            {
                return ResponseDto<Product>.Fail("Product Info Not Found", 404);
            }


            DistributedCacheEntryOptions distributedCacheEntryOptions = new DistributedCacheEntryOptions();
            string jsonProduct = JsonConvert.SerializeObject(product);
            await _distributedCache.SetStringAsync("product:1", jsonProduct, distributedCacheEntryOptions);
            return ResponseDto<Product>.Success(product, 200);
        }

        public async Task<Product> GetAllBucket()
        {
            string jsonProduct = await _distributedCache.GetStringAsync("product:1");
            Product product = JsonConvert.DeserializeObject<Product>(jsonProduct);
            return product;
        }


    }
}
