using EShopService.Core;
using EShopService.Data;
using EShopService.Web.Extensions;
using EShopService.Web.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EShopService.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            var config = services.ConfigureSettings(Configuration);
            services.RegisterData(config);
            services.RegisterCoreServices();
            services.ConfigureMappers();
            services.AddApiVersioning(x => x.ReportApiVersions = true);
            services.AddSwagger();
            services.AddMvcCore().AddVersionedApiExplorer(x => x.GroupNameFormat = "'v'VVV");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseExceptionToHttpStatusCodeMapping();
            app.UseSwaggerUi(provider);
            app.UseMvc();
        }
    }
}