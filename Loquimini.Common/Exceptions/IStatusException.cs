namespace Loquimini.Common.Exceptions
{
    public interface IStatusException
    {
        int StatusCode { get; }

        string Message { get; }
    }
}
