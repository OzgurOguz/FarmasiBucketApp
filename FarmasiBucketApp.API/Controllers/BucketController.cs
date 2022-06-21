using FarmasiBucketApp.Core.Dtos.Shared;
using FarmasiBucketApp.Core.Interfaces;
using FarmasiBucketApp.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmasiBucketApp.API.Controllers
{
    [Route("api/[controller]")]
    public class BucketController : Controller
    {
        public readonly IBucketService _bucketService;
        public BucketController(IBucketService bucketService)
        {
            _bucketService = bucketService;
        }

        [HttpPost]
        public async Task<ResponseDto<Product>> AddBucket(Product product)
        {
            return await _bucketService.AddBucket(product);
        }

        [HttpGet]
        public async Task<Product> GetAllBucket()
        {
            return await _bucketService.GetAllBucket();
        }
    }
}
