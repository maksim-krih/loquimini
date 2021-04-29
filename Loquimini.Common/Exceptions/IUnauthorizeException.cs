using Loquimini.Common.Enums;

namespace Loquimini.Common.Exceptions
{
    public interface IUnauthorizeException
    {
        int StatusCode { get; }

        string Message { get; }

        UnauthorizeExceptionType Type { get; set; }
    }
}
