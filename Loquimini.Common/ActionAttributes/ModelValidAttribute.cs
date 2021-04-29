using Loquimini.Common.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Loquimini.Common.ActionAttributes
{
    public class ModelValidAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                throw new InvalidRequestDataStatusError(context.ModelState);
            }

            base.OnActionExecuting(context);
        }
    }
}
