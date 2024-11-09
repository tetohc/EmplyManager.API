namespace EmplyManager.Entities.Models
{
    /// <summary>
    /// This generic class represents the outcome of an operation, encapsulating whether it was successful, 
    /// the resulting data, and an error message.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class OperationResultModel<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public string ErrorMessage { get; set; }

        /// <summary>
        /// This static method creates and returns an instance of OperationResultModel<T> indicating a successful operation.
        /// </summary>
        /// <param name="data">The result data of the operation, of generic type T.</param>
        /// <returns>Object with Success set to true and Data set to the provided data.</returns>
        public static OperationResultModel<T> SuccessResult(T data)
            => new OperationResultModel<T> { Success = true, Data = data };

        /// <summary>
        /// This static method creates and returns an instance of OperationResultModel<T> indicating a failed operation.
        /// </summary>
        /// <param name="errorMessage">A string containing the error message describing the failure.</param>
        /// <returns>Object with Success set to false and ErrorMessage set to the provided errorMessage.</returns>
        public static OperationResultModel<T> FailureResult(string errorMessage)
            => new OperationResultModel<T> { Success = false, ErrorMessage = errorMessage };
    }
}