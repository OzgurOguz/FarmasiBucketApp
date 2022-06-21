using AutoMapper;
using FarmasiBucketApp.Core.Dtos.Product;
using FarmasiBucketApp.Core.Dtos.Shared;
using FarmasiBucketApp.Core.Interfaces;
using FarmasiBucketApp.Core.Interfaces.DatabaseInterfaces;
using FarmasiBucketApp.Core.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmasiBucketApp.Service
{
    public class ProductService : IProductService
    {

        private readonly IMongoCollection<Product> _productCollection;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public ProductService(IMapper mapper, IDatabaseSettings dbSettings, IConfiguration configuration)
        {
            var client = new MongoClient(dbSettings.ConnectionString);
            var db = client.GetDatabase(dbSettings.DatabaseName);
            _productCollection = db.GetCollection<Product>(dbSettings.ProductCollectionName);
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<ResponseDto<List<ProductDto>>> GetAllAsync()
        {
            var product = await _productCollection.Find(x => true).ToListAsync();
            return ResponseDto<List<ProductDto>>.Success(_mapper.Map<List<ProductDto>>(product), 200);
        }


        public async Task<ResponseDto<ProductDto>> GetByIdAsyncAsync(string id)
        {

            var product = await _productCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            if (product == null) return ResponseDto<ProductDto>.Fail("Product Info Not Found", 404);


            return ResponseDto<ProductDto>.Success(_mapper.Map<ProductDto>(product), 200);
        }


        public async Task<ResponseDto<ProductDto>> CreateAsync(ProductCreateDto productCreateDto)
        {

            await _productCollection.InsertOneAsync(_mapper.Map<Product>(productCreateDto));

            return ResponseDto<ProductDto>.Success(_mapper.Map<ProductDto>(productCreateDto), 200);
        }


        public async Task<ResponseDto<NoContent>> UpdateAsync(ProductUpdateDto productUpdateDto)
        {
            var updateproduct = _mapper.Map<Product>(productUpdateDto);
            var result = await _productCollection.FindOneAndReplaceAsync(x => x.Id == productUpdateDto.Id, updateproduct);


            if (result == null) return ResponseDto<NoContent>.Fail("Product Not Found", 404);

            return ResponseDto<NoContent>.Success(204);
        }

        public async Task<ResponseDto<NoContent>> DeleteAsync(string id)
        {
            var result = await _productCollection.DeleteOneAsync(x => x.Id == id);

            if (result.DeletedCount == 0) return ResponseDto<NoContent>.Fail("Product Not Found", 404);

            return ResponseDto<NoContent>.Success(204);
        }

    }
}
