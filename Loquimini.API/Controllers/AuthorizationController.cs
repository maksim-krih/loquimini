using Loquimini.API.Controllers.Base;
using Loquimini.ModelDTO;
using Loquimini.Repository.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Loquimini.API.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AuthorizationController : BaseController
    {
        public AuthorizationController(IDatabaseManager databaseManager) : base(databaseManager)
        {

        }

        [HttpPost]
        public async Task<ActionResult<LoginDTO>> Login([FromBody]LoginDTO loginUser)
        {
            return Ok();
        }
	}
}
