using AutoMapper;
using Loquimini.API.Controllers.Base;
using Loquimini.Common.Exceptions;
using Loquimini.Model.Entities;
using Loquimini.ModelDTO.UserDTO;
using Loquimini.Repository.UnitOfWork;
using Loquimini.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Loquimini.API.Controllers
{
    [Route("api/[controller]/[action]")]
    public class RoleController : BaseController
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper; 

        public RoleController(
            IDatabaseManager databaseManager, 
            IAccountService accountService,
            IMapper mapper
        ) : base(databaseManager)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RoleDTO))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(IInvalidRequestDataStatusError))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(IStatusException))]
        public async Task<IActionResult> Create([FromBody]RoleDTO roleDTO)
        {
            var role = _mapper.Map<Role>(roleDTO);

            role = await _accountService.CreateRoleAsync(role, new string[] { });
            
            return Ok(_mapper.Map<RoleDTO>(role));
        }
	}
}
