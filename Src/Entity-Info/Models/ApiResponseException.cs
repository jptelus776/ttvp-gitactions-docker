using Microsoft.AspNetCore.Mvc;

namespace EntityInfoService.Models
{
    public class ApiResponseException : Exception
    {
        public ApiResponseException()
        {
        }

        public ApiResponseException(string message) : base(message)
        {
        }

        public ApiResponseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="status"></param>
        /// <param name="error"></param>
        public ApiResponseException(string message, int status, ErrorResponseModel error) : base(message)
        {
            Status = status;
            Error = error;
            Result = new JsonResult(error);
        }

        /// <summary>
        /// 
        /// </summary>
        public int Status { get; private set; }

        public ErrorResponseModel? Error { get; private set; } = null;

        public JsonResult? Result { get; private set; } = null;
    }
}
