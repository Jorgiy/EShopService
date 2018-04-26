using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EShopService.Web.Extensions
{
    public static class SwaggerExtensions
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
                }

                options.OperationFilter<SwaggerDefaultValues>();

                var xmlCommentFilePath = XmlCommentsFilePath;

                if (!string.IsNullOrWhiteSpace(xmlCommentFilePath))
                {
                    options.IncludeXmlComments(xmlCommentFilePath);
                }
            });
        }

        public static void UseSwaggerUi(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
            });
        }

        private static string XmlCommentsFilePath
        {
            get
            {
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var fileName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name + ".xml";
                var xmlFilePath = Path.Combine(basePath, fileName);

                if (File.Exists(xmlFilePath))
                    return xmlFilePath;

                return null;
            }
        }


        private static Info CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new Info
            {
                Title = $"EShop Service API v{description.ApiVersion}",
                Version = description.ApiVersion.ToString(),
                Description = "API for providing to work with products"
            };

            if (description.IsDeprecated)
            {
                info.Description += " This API version has been deprecated.";
            }

            return info;
        }
    }

    public class SwaggerDefaultValues : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            foreach (var parameter in operation.Parameters.OfType<NonBodyParameter>())
            {
                var description = context.ApiDescription.ParameterDescriptions.First(p => p.Name == parameter.Name);

                if (parameter.Description == null)
                {
                    parameter.Description = description.ModelMetadata.Description;
                }

                if (description.RouteInfo != null)
                {
                    if (parameter.Default == null)
                    {
                        parameter.Default = description.RouteInfo.DefaultValue;
                    }

                    parameter.Required |= !description.RouteInfo.IsOptional;
                }
            }
        }
    }
}