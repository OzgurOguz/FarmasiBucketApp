using FarmasiBucketApp.Core.Dtos.Product;
using FarmasiBucketApp.Core.Dtos.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmasiBucketApp.Core.Interfaces
{
    public interface IProductService
    {
        Task<ResponseDto<List<ProductDto>>> GetAllAsync();

        Task<ResponseDto<ProductDto>> GetByIdAsyncAsync(string id);

        Task<ResponseDto<ProductDto>> CreateAsync(ProductCreateDto product);

        Task<ResponseDto<NoContent>> UpdateAsync(ProductUpdateDto productUpdateDto);

        Task<ResponseDto<NoContent>> DeleteAsync(string id);

    }
}
