using EntityInfoService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EntityInfoService.Filters
{
    public class ApiResponseExceptionFilter : IExceptionFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (context.Exception is ApiResponseException)
            {
                var ex = context.Exception as ApiResponseException;
                if (ex != null)
                {
                    context.HttpContext.Response.StatusCode = ex.Status;
                    context.Result = ex.Result;
                    context.ExceptionHandled = true;
                    return;
                }
            }

            context.HttpContext.Response.StatusCode = 500;
            context.Result = new JsonResult(ErrorResponseModel.GetUnhandledExceptionErrorResponse());
            context.ExceptionHandled = true;
        }
    }
}
