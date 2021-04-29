using Loquimini.Common.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace Loquimini.Service.Responses
{
    public class ServiceResponse : IServiceResponse
	{
		public IEnumerable<ValidationError> Errors { get; }

		public bool HasErrors => Errors.Any();

		public ServiceResponse()
		{
			Errors = new List<ValidationError>();
		}

		public ServiceResponse(IEnumerable<ValidationError> errors)
		{
			Errors = errors;
		}
	}
}