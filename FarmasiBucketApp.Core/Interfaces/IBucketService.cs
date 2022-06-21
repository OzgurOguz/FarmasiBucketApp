using FarmasiBucketApp.Core.Dtos.Shared;
using FarmasiBucketApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmasiBucketApp.Core.Interfaces
{
    public interface IBucketService
    {
        Task<ResponseDto<Product>> AddBucket(Product product);
        Task<Product> GetAllBucket();
    }
}
