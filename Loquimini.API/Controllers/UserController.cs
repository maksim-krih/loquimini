using AutoMapper;
using Loquimini.API.Controllers.Base;
using Loquimini.Common.Exceptions;
using Loquimini.Model.Entities;
using Loquimini.ModelDTO.GridDTO;
using Loquimini.ModelDTO.UserDTO;
using Loquimini.Repository.UnitOfWork;
using Loquimini.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
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
        public async Task<IActionResult> GetAllGrid(GridRequestDTO request)
        {
			var users = _databaseManager.UserRepository.Get();

            var gridResponse = await request.GenerateGridResponseAsync(users);

            return Ok(gridResponse);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<UserDTO>))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(IInvalidRequestDataStatusError))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(IStatusException))]
        public async Task<IActionResult> GetAll()
        {
            var users = _databaseManager.UserRepository.Get().ToListAsync();

            return Ok(users);
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

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(IInvalidRequestDataStatusError))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(IStatusException))]
        public async Task<IActionResult> DeleteById([FromQuery] Guid id)
        {
            var user = await _databaseManager.UserRepository
                .GetByIdAsync(id);

            await _accountService.DeleteUserAsync(user);

            return Ok(true);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDTO))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(IInvalidRequestDataStatusError))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(IStatusException))]
        public async Task<IActionResult> Update([FromBody]UserDTO userDTO)
        {
            var user = await _userService.UpdateUserAsync(userDTO);

            return Ok(_mapper.Map<UserDTO>(user));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDTO))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(IInvalidRequestDataStatusError))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(IStatusException))]
        public async Task<IActionResult> GetById([FromQuery] Guid id)
        {
            var user = await _databaseManager.UserRepository
                .Get(x => x.Id == id)
                .FirstOrDefaultAsync();

            return Ok(_mapper.Map<UserDTO>(user));
        }
    }
}
