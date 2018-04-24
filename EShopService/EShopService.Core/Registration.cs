using AutoMapper;
using EShopService.Core.CoreServices;
using EShopService.Core.Dto;
using EShopService.Core.Interfaces;
using EShopService.Data.Models;
using Microsoft.Extensions.DependencyInjection;

namespace EShopService.Core
{
    public static class Registration
    {
        public static void RegisterCoreServices(this IServiceCollection services)
        {
            services.AddSingleton<MapperConfiguration>(new MapperConfiguration(x =>
                x.CreateMap<Product, ProductDto>()));

            services.AddTransient<IProductCoreService, ProductCoreService>();
        }
    }
}