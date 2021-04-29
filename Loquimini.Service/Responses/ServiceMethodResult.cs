namespace Loquimini.Service.Responses
{
    public class ServiceMethodResult<T>
    {
        public T Value { get; set; }
        public string ErrorCode { get; set; }

        public ServiceMethodResult(T value, string error)
        {
            Value = value;
            ErrorCode = error;
        }

        public bool IsSuccess() => string.IsNullOrWhiteSpace(ErrorCode);
    }
}
