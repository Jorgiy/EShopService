using EShopService.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EShopService.Data
{
    public class EShopServiceContext : DbContext
    {
        public EShopServiceContext(DbContextOptions<EShopServiceContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}