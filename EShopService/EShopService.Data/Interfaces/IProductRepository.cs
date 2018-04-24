using System.Collections.Generic;
using System.Threading.Tasks;
using EShopService.Data.Models;

namespace EShopService.Data.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetById(int id);
        
        Task<List<Product>> GetPaginatedProducts(int pageSize, int pageNumber);
        
        Task<List<Product>> GetAllProducts();

        Task UpdateDescription(int id, string description);
    }
}