using AutoMapper;
using Loquimini.API.Controllers.Base;
using Loquimini.Common.Exceptions;
using Loquimini.Model.User;
using Loquimini.ModelDTO;
using Loquimini.Repository.UnitOfWork;
using Loquimini.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Loquimini.API.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AuthorizationController : BaseController
    {
        private readonly IMapper _mapper; 
        private readonly IAccountService _accountService;

        public AuthorizationController(
            IDatabaseManager databaseManager,
            IMapper mapper,
            IAccountService accountService
        ) : base(databaseManager)
        {
            _mapper = mapper;
            _accountService = accountService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginDTO))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(IInvalidRequestDataStatusError))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(IStatusException))]
        public async Task<IActionResult> Login([FromBody]LoginDTO loginDTO)
        {
            var login = _mapper.Map<Login>(loginDTO);
            var cred = await _accountService.Login(login);

            return Ok(cred);
        }
	}
}
