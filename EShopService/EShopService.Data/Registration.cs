using EShopService.Data.Interfaces;
using EShopService.Data.Mocks;
using EShopService.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace EShopService.Data
{
    public static class Registration
    {
        public static void RegisterData(this IServiceCollection services, Config config)
        {
            if (config?.UseMocks ?? true)
            {
                services.AddTransient<IProductRepository, ProductRepositoryMock>();
            }
            else
            {
                services.AddDbContext<EShopServiceContext>(options =>
                    options.UseSqlServer(config.DataBaseConnectionString));
                services.AddTransient<IProductRepository, ProductRepository>();
            }
        }
    }
}