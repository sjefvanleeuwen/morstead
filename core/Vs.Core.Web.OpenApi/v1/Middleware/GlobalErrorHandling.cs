using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;
using Vs.Core.Web.OpenApi.v1.Dto.ProtocolErrors;

namespace Vs.Core.Web.OpenApi.v1.Middleware
{
    public static class GlobalErrorHandling
    {
            public static void ConfigureExceptionHandler(this IApplicationBuilder app)
            {
                app.UseExceptionHandler(appError =>
                {
                    appError.Run(async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        context.Response.ContentType = "application/json";

                        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                        if (contextFeature != null)
                        {
                            await context.Response.WriteAsync(new ServerError500Response()
                            {
                                Message = "Internal Server Error."
                            }.ToString());
                        }
                    });
                });
            }
        }
}
