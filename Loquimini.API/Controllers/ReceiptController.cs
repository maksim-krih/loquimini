using AutoMapper;
using Loquimini.API.Controllers.Base;
using Loquimini.Common.Exceptions;
using Loquimini.ModelDTO.GridDTO;
using Loquimini.ModelDTO.ReceiptDTO;
using Loquimini.Repository.UnitOfWork;
using Loquimini.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loquimini.API.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ReceiptController : BaseController
    {
        private readonly IReceiptService _receiptService;
        private readonly IMapper _mapper;

        public ReceiptController(
            IDatabaseManager databaseManager,
            IMapper mapper,
            IReceiptService receiptService
        ) : base(databaseManager)
        {
            _receiptService = receiptService;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GridResponseDTO<ReceiptGridDTO>))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(IInvalidRequestDataStatusError))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(IStatusException))]
        public async Task<IActionResult> GetByUserIdGrid([FromQuery] Guid id, [FromBody]GridRequestDTO request)
        {
            var receitps = await _receiptService.GetByUserId(id);

            var receitpDTOs = _mapper.Map<List<ReceiptGridDTO>>(receitps).AsQueryable();

            var gridResponse = await request.GenerateGridResponseAsync(receitpDTOs);

            return Ok(gridResponse);
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(IInvalidRequestDataStatusError))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(IStatusException))]
        public async Task<IActionResult> GenerateReceipts()
        {
            var result = await _receiptService.GenerateReceipts();

			return Ok(result);
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(IInvalidRequestDataStatusError))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(IStatusException))]
        public async Task<IActionResult> FillReceipt([FromBody]FillReceiptDTO fillReceiptDTO)
        {
            var result = await _receiptService.FillReceipt(fillReceiptDTO);

            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(IInvalidRequestDataStatusError))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(IStatusException))]
        public async Task<IActionResult> PayReceipt([FromBody]PayReceiptDTO payReceiptDTO)
        {
            var result = await _receiptService.PayReceipt(payReceiptDTO);

            return Ok(result);
        }
    }
}
