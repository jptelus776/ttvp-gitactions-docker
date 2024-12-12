using EntityInfoService.Models;
using System.Text.Json.Serialization;

namespace EntityInfoService.Utils
{
    public class MiddlewareUtility
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Action<Microsoft.AspNetCore.Mvc.JsonOptions> ConfigureDefaultJsonOptions()
        {
            return options =>
            {
                options.JsonSerializerOptions.WriteIndented = true;
                options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.Never;
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static Task GetStatusCodePagesResponse(Microsoft.AspNetCore.Diagnostics.StatusCodeContext ctx)
        {
            ErrorResponseModel? result = null;
            switch (ctx.HttpContext.Response.StatusCode)
            {
                case 401:
                    result = ErrorResponseModel.GetUnauthorizedErrorResponse();
                    break;
                case 404:
                    result = ErrorResponseModel.GetResourceNotFoundErrorResponse();
                    break;
                case 405:
                    result = ErrorResponseModel.GetMethodNotAllowedErrorResponse();
                    break;
                case 502:
                    result = ErrorResponseModel.GetBadGatewayErrorResponse();
                    break;
                default:
                    throw new Exception();
            }

            if (result != null)
            {
                ctx.HttpContext.Response.StatusCode = result.HttpCode;
                ctx.HttpContext.Response.ContentType = "application/json";
                ctx.HttpContext.Response.WriteAsJsonAsync(result);
            }

            return Task.CompletedTask;
        }

        public static void GetExceptionHandlerResponse(IApplicationBuilder errorApp)
        {
            errorApp.Run(async context =>
            {
                var result = ErrorResponseModel.GetInternalServerErrorResponse();
                context.Response.StatusCode = result.HttpCode;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(result).ConfigureAwait(false);
            });
        }
    }
}
