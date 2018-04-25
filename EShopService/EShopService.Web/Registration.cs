using AutoMapper;
using EShopService.Core.Dto;
using EShopService.Data;
using EShopService.Data.Models;
using EShopService.Web.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EShopService.Web
{
    public static class Registration
    {
        public static void ConfigureMappers(this IServiceCollection services)
        {
            services.AddSingleton(new MapperConfiguration(x =>
            {
                x.CreateMap<Product, ProductDto>();
                x.CreateMap<ProductDto, ProductViewModel>();
            }));
        }
        
        public static Config ConfigureSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();

            var serviceSection = configuration.GetSection("Config");

            services.Configure<Config>(serviceSection);

            return serviceSection.Get<Config>();
        }
    }
}