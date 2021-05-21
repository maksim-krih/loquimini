using AutoMapper;
using Loquimini.API.Controllers.Base;
using Loquimini.Common.Exceptions;
using Loquimini.Model.Entities;
using Loquimini.ModelDTO.GridDTO;
using Loquimini.ModelDTO.HouseDTO;
using Loquimini.Repository.UnitOfWork;
using Loquimini.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
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

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GridResponseDTO<HouseDTO>))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(IInvalidRequestDataStatusError))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(IStatusException))]
        public async Task<IActionResult> GetAllGrid(GridRequestDTO request)
        {
            var houses = _databaseManager.HouseRepository.Get();

            var gridResponse = await request.GenerateGridResponseAsync(houses);

            return Ok(gridResponse);
        }

        [Authorize]
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

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(HouseDTO))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(IInvalidRequestDataStatusError))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(IStatusException))]
        public async Task<IActionResult> Update([FromBody]HouseDTO houseDTO)
        {
            var house = await _houseService.UpdateHouseAsync(houseDTO);

            return Ok(_mapper.Map<HouseDTO>(house));
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(HouseDTO))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(IInvalidRequestDataStatusError))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(IStatusException))]
        public async Task<IActionResult> GetById([FromQuery] Guid id)
        {
            var house = await _databaseManager.HouseRepository
                .Get(x => x.Id == id)
                .Include(x => x.Flats)
                    .ThenInclude(x => x.Info)
                .Include(x => x.Info)
                .FirstOrDefaultAsync();

            return Ok(_mapper.Map<HouseDTO>(house));
        }

        [Authorize]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(IInvalidRequestDataStatusError))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(IStatusException))]
        public async Task<IActionResult> DeleteById([FromQuery] Guid id)
        {
            var house = await _databaseManager.HouseRepository
                .Get(x => x.Id == id)
                .Include(x => x.Info)
                .Include(x => x.Flats)
                    .ThenInclude(x => x.Info)
                .FirstOrDefaultAsync();

            _databaseManager.HouseRepository.Delete(house);

            var saved = await _databaseManager.SaveChangesAsync();

            return Ok(saved);
        }
    }
}
