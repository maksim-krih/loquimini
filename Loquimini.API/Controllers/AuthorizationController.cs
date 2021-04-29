using Loquimini.API.Controllers.Base;
using Loquimini.Common.Exceptions;
using Loquimini.ModelDTO;
using Loquimini.Repository.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Loquimini.API.Controllers
{
    [Route("api/[controller]")]
    public class AuthorizationController : BaseController
    {
        public AuthorizationController(IDatabaseManager databaseManager) : base(databaseManager)
        {

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginDTO))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(IInvalidRequestDataStatusError))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(IStatusException))]
        public async Task<IActionResult> Login([FromBody]LoginDTO loginUser)
        {
            return Ok();
        }
	}
}
