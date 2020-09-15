using System;

namespace API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string errorMessage = null)
        {
            StatusCode = statusCode;
            ErrorMessage = errorMessage ?? GetDefaultErrorMessageForStatusCode(statusCode);
        }

        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }


        private string GetDefaultErrorMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "A bad request has occured",
                401 => "Unauthorized request",
                404 => "Resource was not found",
                500 => "Internal server error has occured",
                _ => null
            };
        }
    }
}