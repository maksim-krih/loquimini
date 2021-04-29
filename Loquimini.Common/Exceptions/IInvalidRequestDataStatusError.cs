using System.Collections.Generic;

namespace Loquimini.Common.Exceptions
{
    public interface IInvalidRequestDataStatusError
    {
        List<ValidationError> Errors { get; }

        int StatusCode { get; }

        string Message { get; }
    }
}
