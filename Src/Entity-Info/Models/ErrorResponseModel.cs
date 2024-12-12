using System.ComponentModel;
using System.Text.Json.Serialization;

namespace EntityInfoService.Models
{
    public class ErrorResponseModel
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="error"></param>
        /// <param name="description"></param>
        /// <param name="ex"></param>
        public ErrorResponseModel(int httpCode, string errorCode, string error, string description, Exception? ex = null)
        {
            HttpCode = httpCode;
            ErrorCode = errorCode;
            Error = error;
            Description = description;
            Exception = ex;
        }

        /// <summary>
        /// 
        /// </summary>
        public int HttpCode { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string ErrorCode { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string Error { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [DefaultValue(null)]
        public Exception? Exception { get; private set; } = null;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ErrorResponseModel GetUnauthorizedErrorResponse()
        {
            return new ErrorResponseModel(401, "401", "Unauthorized", "Authorization failed for resource requested.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ErrorResponseModel GetResourceNotFoundErrorResponse()
        {
            return new ErrorResponseModel(404, "404", "NotFound", "Unknown resource requested or not found.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ErrorResponseModel GetMethodNotAllowedErrorResponse()
        {
            return new ErrorResponseModel(405, "405", "MethodNotAllowed", "HTTP method not allowed for requested API.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ErrorResponseModel GetInternalServerErrorResponse()
        {
            return new ErrorResponseModel(500, "500", "InternalServerError", "Service has encountered internal error. Contact service provider with X-Correlation-Id from response headers.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ErrorResponseModel GetUnhandledExceptionErrorResponse()
        {
            return new ErrorResponseModel(500, "500", "UnhandledException", "An unhandled exception has occurred. Contact service provider with X-Correlation-Id from response headers.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ErrorResponseModel GetBadGatewayErrorResponse()
        {
            return new ErrorResponseModel(502, "502", "BadGateway", "Service has forcefully closed connection.");
        }
    }
}
