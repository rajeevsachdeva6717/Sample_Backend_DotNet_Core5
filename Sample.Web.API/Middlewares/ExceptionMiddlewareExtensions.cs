using Sample.Web.API.ExceptionLogger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Sample.Web.API.Middlewares
{
    /// <summary>
    /// Middleware extension method for error handling.
    /// </summary>
    public static class ExceptionMiddlewareExtensions
    {
        /// <summary>
        /// Extension Method of application builder to use the exception handler
        /// </summary>
        /// <param name="app">application builder object</param>
        /// <param name="exceptionLogger">exception logger object for logging the exception</param>
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(context =>
               {
                   return Task.Run(async () =>
                   {
                       context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                       context.Response.Headers.Add("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");
                       context.Response.Headers.Add("Access-Control-Allow-Methods", "GET, PUT, POST, DELETE, OPTIONS");
                       IExceptionHandlerFeature contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                       IExceptionLogger exceptionLogger = context.RequestServices.GetRequiredService<IExceptionLogger>();
                       context.Response.StatusCode = 500;
                       context.Response.ContentType = "application/json";

                       if (contextFeature != null)
                       {
                           Exception exception = contextFeature.Error;
                           HttpRequest request = context.Request;
                           exceptionLogger.Log(exception, request);
                           var exceptionObject = new { exception.StackTrace, exception.Message };

                           await context.Response.WriteAsync(Newtonsoft.Json.JsonConvert.SerializeObject(exceptionObject));
                       }
                   });
               });
            });
        }
    }
}
