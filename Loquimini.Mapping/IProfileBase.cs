using AutoMapper;

namespace Loquimini.Mapping
{
    public interface IProfileBase
    {
        IProfileExpression Configure(IProfileExpression config);
    }
}
