namespace Loquimini.Common.Enums
{
    public enum UnauthorizeExceptionType
    {
        TokenExpired = 0,
        TokenRevoked = 1,
        DeletedFromOrg = 2,
        TokenInvalid = 3
    }
}
