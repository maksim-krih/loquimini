using AutoMapper;
using Loquimini.API.Controllers.Base;
using Loquimini.Common.Exceptions;
using Loquimini.Model.Entities;
using Loquimini.ModelDTO;
using Loquimini.ModelDTO.GridDTO;
using Loquimini.ModelDTO.UserDTO;
using Loquimini.Repository.UnitOfWork;
using Loquimini.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loquimini.API.Controllers
{
    [Route("api/[controller]/[action]")]
    public class UserController : BaseController
    {
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(
            IDatabaseManager databaseManager,
            IAccountService accountService,
            IMapper mapper,
            IUserService userService
        ) : base(databaseManager)
        {
            _accountService = accountService;
            _mapper = mapper;
            _userService = userService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GridResponseDTO<UserDTO>))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(IInvalidRequestDataStatusError))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(IStatusException))]
        public async Task<IActionResult> GetAll(GridRequestDTO request)
        {
			var users = _databaseManager.UserRepository.Get();

            var gridResponse = await request.GenerateGridResponseAsync(_mapper.Map<UserDTO>(users);

            return Ok(gridResponse);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDTO))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(IInvalidRequestDataStatusError))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(IStatusException))]
        public async Task<IActionResult> Create([FromBody]UserDTO userDTO)
        {
            var user = _mapper.Map<User>(userDTO);

            user = await _accountService.CreateUserAsync(user, userDTO.Roles, userDTO.Password);

            return Ok(_mapper.Map<UserDTO>(user));
        }
	}
}
