using Microsoft.AspNetCore.Diagnostics;
using System.Net;

//namespace ChatApp.API.Middlewares
//{
//    public static class ExceptionMiddlewareExtensions
//    {
//        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
//        {
//            app.UseExceptionHandler(appError =>
//            {
//                appError.Run(async context =>
//                {
//                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
//                    context.Response.ContentType = "application/json";
//                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
//                    if (contextFeature != null)
//                    {
//                        await context.Response.WriteAsync(new ErrorDetailModel(400,"")
//                        {
//                            StatusCode = context.Response.StatusCode,
//                            Messages =new List<string>() { "Internal Server Error." }
//                        }.ToString());
//                    }
//                });
//            });
//        }
//    }
//}
