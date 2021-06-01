using AutoMapper;
using Loquimini.API.Controllers.Base;
using Loquimini.Common.Exceptions;
using Loquimini.Manager.Interfaces;
using Loquimini.Model.Entities;
using Loquimini.ModelDTO.DashboardDTO;
using Loquimini.Repository.UnitOfWork;
using Loquimini.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loquimini.API.Controllers
{
    [Route("api/[controller]/[action]")]
    public class DashboardController : BaseController
    {
        private readonly IReceiptService _receiptService;
        private readonly IMapper _mapper;
        private readonly IIdentityManager _identityManager;

        public DashboardController(
            IDatabaseManager databaseManager,
            IMapper mapper,
            IReceiptService receiptService,
            IIdentityManager identityManager
        ) : base(databaseManager)
        {
            _receiptService = receiptService;
            _mapper = mapper;
            _identityManager = identityManager;
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DashboardInfoDTO))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(IInvalidRequestDataStatusError))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(IStatusException))]
        public async Task<IActionResult> GetInfo()
        {
            var currentUser = _identityManager.GetCurrentUser();

            var user = await _databaseManager.UserRepository.Get(x => x.Id == currentUser.Id)
                .Include(x => x.Flats)
                .Include(x => x.Houses)
                .FirstOrDefaultAsync();

            var dateNow = DateTime.Now.Date;

            var firstDayOfMonth = new DateTime(dateNow.Year, dateNow.Month, 1);


            var receipts = await _databaseManager.ReceiptRepository.Get()
                .Include(x => x.House)
                .Include(x => x.Flat)
                .Where(x => x.CreatedDate > firstDayOfMonth)
                .ToListAsync();

            var userReceipts = new List<Receipt>();

            foreach (var flat in user.Flats)
            {
                var flatReceipts = receipts.Where(x => x.FlatId == flat.Id);

                userReceipts.AddRange(flatReceipts);
            }

            foreach (var house in user.Houses)
            {
                var houseReceipts = receipts.Where(x => x.HouseId == house.Id);

                userReceipts.AddRange(houseReceipts);
            }

            var result = new DashboardInfoDTO
            {
                TotalSum = userReceipts.Sum(x => x.Total),
                TotalDebts = userReceipts.Sum(x => x.Debt),
                CurrentFilled = userReceipts.Count(x => x.Total != decimal.Zero),
                TotalFilled = userReceipts.Count,
                CurrentPaid = userReceipts.Count(x => x.Paid != decimal.Zero),
                TotalPaid = userReceipts.Count
            };

			return Ok(result);
        }
    }
}
