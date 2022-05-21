namespace HT.Application.Shared
{
    public class ServiceResult
    {
        public ServiceResult(bool wasSuccessful, string errorMessage)
        {
            WasSuccessful = wasSuccessful;
            ErrorMessage = errorMessage;
        }

        public bool WasSuccessful { get; private set; }
        public string ErrorMessage { get; private set; }

        public static ServiceResult Success() => new ServiceResult(true, null);
        public static ServiceResult Failure(string reason) => new ServiceResult(false, reason);
    }

    public class ServiceResult<T> : ServiceResult
    {
        private ServiceResult(bool wasSuccessful, string errorMessage, T value) : base(wasSuccessful, errorMessage)
        {
            Value = value;
        }

        public T Value { get; private set; }

        public static ServiceResult<T> Success(T value) => new ServiceResult<T>(true, null, value);
        public new static ServiceResult<T> Failure(string reason) => new ServiceResult<T>(false, reason, default(T));
    }
}