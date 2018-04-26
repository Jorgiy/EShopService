using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using EShopService.Core.Exceptions;
using EShopService.Web.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EShopService.Web.Middleware
{
    public static class ExceptionMiddleware
    {
        private static readonly Dictionary<Type, HttpStatusCode> ExceptionStatusCodes = new Dictionary<Type, HttpStatusCode>
        {
            {typeof(ValidationException), HttpStatusCode.BadRequest},
            {typeof(BuisenessException), (HttpStatusCode)422}
        };
        
         public static void UseExceptionToHttpStatusCodeMapping(this IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                try
                {
                    await next();
                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = ExceptionStatusCodes.ContainsKey(ex.GetType())
                        ? (int)ExceptionStatusCodes[ex.GetType()] : (int)HttpStatusCode.InternalServerError;

                    var errorResponceContent = GetExceptionResponse(ex);

                    await context.Response.WriteAsync(errorResponceContent);
                    
                    var logger = context.Request.HttpContext.RequestServices.GetService<ILogger<Program>>();
                    logger.LogError(ex, "ServerError");
                    
                }
            });
        }

        private static string GetExceptionResponse(Exception ex)
        {
            switch (ex)
            {
                case ValidationException validationException:
                    return HandleValidationException(validationException);
                case BuisenessException buisenessException:
                    return HandleBuisenessExceptionException(buisenessException);
            }

            return HandleGenericException(ex);
        }

        private static string HandleGenericException(Exception ex)
        {
            return new ErrorResponse
            {
                ErrorMessage = ex.Message
            }.ToJson();
        }

        private static string HandleBuisenessExceptionException(BuisenessException exception)
        {
            return new BuisenessErrorResponse
            {
                ErrorMessage = exception.Message
            }.ToJson();
        }

        private static string HandleValidationException(ValidationException ex)
        {
            return new ValidationErrorResponse
            {
                ErrorMessage = ex.Message,
                Errors = ex.InvalidFieldNames.Select(x => new ValidationErrorResponse.ValidationErrorField
                {
                    FieldName = x
                }).ToArray()
            }.ToJson(); 
        }

        internal class ValidationErrorResponse
        {
            public ValidationErrorField[] Errors { get; set; }
            
            public string ErrorMessage { get; set; }

            internal class ValidationErrorField
            {
                public string FieldName { get; set; }
            }
        }

        internal class BuisenessErrorResponse
        {
            public string ErrorMessage { get; set; }
        }

        internal class ErrorResponse
        {
            public string ErrorMessage { get; set; }
        }
    }
}