using AutoMapper;
using Loquimini.API.Controllers.Base;
using Loquimini.Common.Exceptions;
using Loquimini.Model.Entities;
using Loquimini.ModelDTO.GridDTO;
using Loquimini.ModelDTO.HouseDTO;
using Loquimini.Repository.UnitOfWork;
using Loquimini.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Loquimini.API.Controllers
{
    [Route("api/[controller]/[action]")]
    public class HouseController : BaseController
    {
        private readonly IHouseService _houseService;
        private readonly IMapper _mapper;

        public HouseController(
            IDatabaseManager databaseManager,
            IMapper mapper,
            IHouseService houseService
        ) : base(databaseManager)
        {
            _houseService = houseService;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GridResponseDTO<HouseDTO>))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(IInvalidRequestDataStatusError))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(IStatusException))]
        public async Task<IActionResult> GetAllGrid(GridRequestDTO request)
        {
			var users = _databaseManager.HouseRepository.Get();

            var gridResponse = await request.GenerateGridResponseAsync(users);

            return Ok(gridResponse);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(HouseDTO))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(IInvalidRequestDataStatusError))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(IStatusException))]
        public async Task<IActionResult> Create([FromBody]HouseDTO houseDTO)
        {
            var house = _mapper.Map<House>(houseDTO);

            house = await _houseService.CreateHouseAsync(house);

            return Ok(_mapper.Map<HouseDTO>(house));
        }
	}
}
