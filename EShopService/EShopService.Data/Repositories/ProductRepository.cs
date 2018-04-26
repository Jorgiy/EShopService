using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Threading.Tasks;
using EShopService.Data.Interfaces;
using EShopService.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EShopService.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly EShopServiceContext _dBcontext;

        public ProductRepository(EShopServiceContext dBcontext)
        {
            _dBcontext = dBcontext;
        }
        
        public async Task<Product> GetById(int id)
        {
            return await _dBcontext.Products.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Product>> GetPaginatedProducts(int pageSize, int pageNumber)
        {
            return await _dBcontext.Products.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _dBcontext.Products.ToListAsync();
        }

        public async Task UpdateDescription(int id, string description)
        {
            var productForUpdate = await _dBcontext.Products.SingleOrDefaultAsync(x => x.Id == id);

            if (productForUpdate == null)
            {
                throw new ObjectNotFoundException($"There is no Product with id = {id}");
            }
            
            productForUpdate.Description = description;

            await _dBcontext.SaveChangesAsync();
        }
    }
}