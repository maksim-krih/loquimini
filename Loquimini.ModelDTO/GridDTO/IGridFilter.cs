using System;
using System.Collections.Generic;

namespace Loquimini.ModelDTO.GridDTO
{
    public interface IGridFilter
    {
        string Field { get; set; }
        string Value { get; set; }
        string Operator { get; set; }

        string GetFilterCondition(Type entityType, List<object> parameters, int index, int recursionCounter = 0);
    }
}
