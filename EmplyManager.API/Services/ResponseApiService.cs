using EmplyManager.Entities.Models;

namespace EmplyManager.API.Services
{
    /// <summary>
    /// Provide the configuration for the API response.
    /// </summary>
    public class ResponseApiService
    {
        /// <summary>
        /// Configures and returns a basic API response.
        /// </summary>
        /// <param name="statusCode">HTTP status code of the response.</param>
        /// <param name="data">Optional data to include in the response.</param>
        /// <param name="message">Optional message to include in the response.</param>
        /// <returns>Basic response model that includes status code, success, message and data.</returns>
        public static BaseResponseModel Response(int statusCode, object data = null, string message = null)
        {
            bool isSuccess = false;
            if (statusCode >= 200 && statusCode <= 300)
                isSuccess = true;

            var result = new BaseResponseModel()
            {
                StatusCode = statusCode,
                Success = isSuccess,
                Message = message,
                Data = data
            };
            return result;
        }
    }
}