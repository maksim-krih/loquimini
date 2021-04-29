using Loquimini.Common.Exceptions;
using System.Collections.Generic;

namespace Loquimini.Service.Responses
{
    public interface IServiceResponse
	{
		IEnumerable<ValidationError> Errors { get; }
		bool HasErrors { get; }
	}
}