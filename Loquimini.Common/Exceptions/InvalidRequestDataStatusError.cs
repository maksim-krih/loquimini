using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Loquimini.Common.Exceptions
{
    public class InvalidRequestDataStatusError : StatusException, IInvalidRequestDataStatusError, IStatusException
    {
        [JsonProperty]
        public List<ValidationError> Errors { get; }

        public override int StatusCode => (int)HttpStatusCode.UnprocessableEntity;

        public InvalidRequestDataStatusError(ModelStateDictionary modelState) : base("Unprocessable Entity Error")
        {
            Errors = modelState.Keys
                   .SelectMany(key => modelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage)))
                   .ToList();
        }

        public InvalidRequestDataStatusError(List<ValidationError> errors) : base("Unprocessable Entity Error")
        {
            Errors = errors;
        }

        public InvalidRequestDataStatusError(ModelStateDictionary modelState, string message)
            : base(message)
        {
            Errors = modelState.Keys
                               .SelectMany(key => modelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage)))
                               .ToList();
        }

        public InvalidRequestDataStatusError(List<ValidationError> errors, string message)
            : base(message)
        {
            Errors = errors;
        }

        public override string GetResponse()
        {
            return JsonConvert.SerializeObject(new { Message, StatusCode, Errors });
        }
    }
}
