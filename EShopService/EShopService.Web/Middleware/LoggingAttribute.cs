using System.Buffers;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using EShopService.Web.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EShopService.Web.Middleware
{
    // To be honest I want to make logging like middleware too, but there were problems with response stream
    // and I considered to do it a bit later
    public class LoggingAttribute : ActionFilterAttribute
    {
        private readonly ArrayPool<byte> _arrayPool = ArrayPool<byte>.Shared;

        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var requestBody = await GetRequestBody(context.HttpContext.Request);

            var logger = context.HttpContext.RequestServices.GetService<ILogger<LoggingAttribute>>();

            var originalBodyStream = context.HttpContext.Response.Body;

            using (var responseBody = new MemoryStream())
            {
                context.HttpContext.Response.Body = responseBody;
  
                var response = context.HttpContext.Response;

                await next();

                response.Body.Seek(0, SeekOrigin.Begin);

                var responseText = await new StreamReader(response.Body).ReadToEndAsync();

                response.Body.Seek(0, SeekOrigin.Begin);

                logger.LogInformation("REQUEST: {RequestMethod} {RequestPath} {RequestBody} RESPPONSE: {ResponseText}",
                    response.HttpContext.Request.Method, response.HttpContext.Request.Path, requestBody, responseText.ToJson());

                await responseBody.CopyToAsync(originalBodyStream);
            }
        }

        private async Task<string> GetRequestBody(HttpRequest request)
        {
            request.EnableRewind();

            var body = string.Empty;

            if (!request.ContentLength.HasValue)
                return body;

            request.Body.Seek(0, SeekOrigin.Begin);

            var contextLength = (int)request.ContentLength.Value;

            var buffer = _arrayPool.Rent(contextLength);

            await request.Body.ReadAsync(buffer, 0, buffer.Length);

            body = Encoding.UTF8.GetString(buffer, 0, contextLength);

            _arrayPool.Return(buffer, true);

            request.Body.Seek(0, SeekOrigin.Begin);
            request.Body = request.Body;

            return body;
        }
    }
}

