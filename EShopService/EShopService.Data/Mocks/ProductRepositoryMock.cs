using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Threading.Tasks;
using EShopService.Data.Interfaces;
using EShopService.Data.Models;

namespace EShopService.Data.Mocks
{
    public class ProductRepositoryMock : IProductRepository
    {
        private readonly List<Product> _mockContext = MockEShopServiceContext.Products;
        
        public Task<Product> GetById(int id)
        {
            return Task.FromResult(_mockContext.SingleOrDefault(x => x.Id == id));
        }

        public Task<List<Product>> GetPaginatedProducts(int pageSize, int pageNumber)
        {
            return Task.FromResult(_mockContext.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList());
        }

        public Task<List<Product>> GetAllProducts()
        {
            return Task.FromResult(_mockContext.ToList());
        }

        public Task UpdateDescription(int id, string description)
        {
            var productForUpdate = _mockContext.SingleOrDefault(x => x.Id == id);

            if (productForUpdate == null)
            {
                throw new ObjectNotFoundException($"There is no Product with id = {id}");
            }
            
            productForUpdate.Description = description;

            return Task.CompletedTask;
        }
    }
}