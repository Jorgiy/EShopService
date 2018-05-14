using System.Collections.Generic;
using System.Threading.Tasks;
using EShopService.Core.Dto;

namespace EShopService.Core.Interfaces
{
    public interface IProductCoreService
    {
        Task<ProductDto> GetProductById(int id);

        Task<List<ProductDto>> GetPaginatedProducts(int pageSize, int pageNumber);
        
        Task<List<ProductDto>> GetAllProducts();

        Task UpdateProductDescription(int id, string description);
    }
}