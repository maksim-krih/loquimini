using AutoMapper;
using Loquimini.Common.Enums;
using Loquimini.Manager.Interfaces;
using Loquimini.Model.Entities;
using Loquimini.ModelDTO.ReceiptDTO;
using Loquimini.Repository.UnitOfWork;
using Loquimini.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loquimini.Service
{
    public class ReceiptService : BaseService, IReceiptService
    {
        private readonly IMapper _mapper;
        private readonly IIdentityManager _identityManager;

        public ReceiptService(
            IDatabaseManager databaseManager,
            IMapper mapper,
            IIdentityManager identityManager
        ) : base(databaseManager)
        {
            _mapper = mapper;
            _identityManager = identityManager;
        }

        public async Task<bool> GenerateReceipts()
        {
            var users = await _databaseManager.UserRepository.Get(x =>
                    x.UserRoles.Any(ur => ur.Role.Name == "User")
                )
                .Include(x => x.Flats)
                    .ThenInclude(x => x.Info.DefaultIndicators)
                .Include(x => x.Houses)
                    .ThenInclude(x => x.Info.DefaultIndicators)
                .ToListAsync();

            var currentUser = _identityManager.GetCurrentUser();

            foreach (var user in users)
            {
                foreach (var house in user.Houses.Where(x => x.Type == HouseType.Private))
                {
                    foreach (var receiptType in Enum.GetValues(typeof(ReceiptType)))
                    {
                        await CreateReceiptByType(currentUser.Id, house.Info.DefaultIndicators, (ReceiptType)receiptType, area: house.Info.Area, houseId: house.Id);
                    }
                }

                foreach (var flat in user.Flats)
                {
                    foreach (var receiptType in Enum.GetValues(typeof(ReceiptType)))
                    {
                        await CreateReceiptByType(currentUser.Id, flat.Info.DefaultIndicators, (ReceiptType)receiptType, area: flat.Info.Area, flatId: flat.Id);
                    }
                }
            }

            await _databaseManager.SaveChangesAsync();

            return true;
        }

        public async Task<List<Receipt>> GetByUserId(Guid id)
        {
            var user = await _databaseManager.UserRepository.Get(x => x.Id == id)
                .Include(x => x.Flats)
                .Include(x => x.Houses)
                .FirstOrDefaultAsync();
            
            var receipts = await _databaseManager.ReceiptRepository.Get()
                .Include(x => x.House)
                .Include(x => x.Flat)
                    .ThenInclude(x => x.House)
                .ToListAsync();

            var userReceipts = new List<Receipt>();

            foreach (var flat in user.Flats)
            {
                var flatReceipts = receipts.Where(x => x.FlatId == flat.Id);

                userReceipts.AddRange(flatReceipts);
            }

            foreach (var house in user.Houses.Where(x => x.Type == HouseType.Private))
            {
                var houseReceipts = receipts.Where(x => x.HouseId == house.Id);

                userReceipts.AddRange(houseReceipts);
            }

            userReceipts = userReceipts.OrderByDescending(x => x.CreatedDate).ToList();

            return userReceipts;
        }

        public async Task<bool> FillReceipt(FillReceiptDTO fillReceiptDTO)
        {
            var receipt = await _databaseManager.ReceiptRepository.Get(x =>
                    x.Id == fillReceiptDTO.ReceiptId
                )
                .FirstOrDefaultAsync();

            var lastReceipt = await _databaseManager.ReceiptRepository
                .Get(x => (x.HouseId == receipt.HouseId && receipt.HouseId != null || x.FlatId == receipt.FlatId && receipt.FlatId != null) && x.Type == receipt.Type)
                .OrderByDescending(x => x.CreatedDate)
                .Skip(1)
                .FirstOrDefaultAsync();

            receipt.NewIndicator = fillReceiptDTO.NewIndicator;
            receipt.Status = ReceiptStatus.Filled;
            receipt.Total = lastReceipt != null ? lastReceipt.Total - lastReceipt.Paid + Convert.ToDecimal(receipt.Rate * (receipt.NewIndicator - receipt.OldIndicator)) : 
                Convert.ToDecimal(receipt.Rate * (receipt.NewIndicator - receipt.OldIndicator));

            _databaseManager.ReceiptRepository.Update(receipt);

            await _databaseManager.SaveChangesAsync();

            return true;
        }

        public async Task<bool> PayReceipt(PayReceiptDTO payReceiptDTO)
        {
            var receipt = await _databaseManager.ReceiptRepository.Get(x =>
                    x.Id == payReceiptDTO.ReceiptId
                )
                .FirstOrDefaultAsync();

            var lastReceipt = await _databaseManager.ReceiptRepository
                .Get(x => (x.HouseId == receipt.HouseId && receipt.HouseId != null || x.FlatId == receipt.FlatId && receipt.FlatId != null) && x.Type == receipt.Type)
                .OrderByDescending(x => x.CreatedDate)
                .Skip(1)
                .FirstOrDefaultAsync();

            receipt.Paid = payReceiptDTO.Value;
            receipt.Status = ReceiptStatus.Paid;

            _databaseManager.ReceiptRepository.Update(receipt);

            await _databaseManager.SaveChangesAsync();

            return true;
        }

        private async Task CreateReceiptByType(Guid currentUserId, ICollection<DefaultIndicator> defaultIndicators, ReceiptType receiptType, Guid? houseId = null, Guid? flatId = null, double? area = null)
        {
            switch (receiptType)
            {
                case ReceiptType.ColdWater:
                    var defaultColdWaterValue = defaultIndicators.Where(x => x.Type == ReceiptType.ColdWater).FirstOrDefault().Value;

                    await CreateColdWaterReceipt(currentUserId, defaultColdWaterValue, houseId, flatId);
                    break;
                case ReceiptType.Electricity:
                    var defaultElectricityValue = defaultIndicators.Where(x => x.Type == ReceiptType.Electricity).FirstOrDefault().Value;

                    await CreateElectricityReceipt(currentUserId, defaultElectricityValue, houseId, flatId);
                    break;
                case ReceiptType.Garbage:
                    await CreateGarbageReceipt(currentUserId, houseId, flatId);
                    break;
                case ReceiptType.Gas:
                    var defaultGasValue = defaultIndicators.Where(x => x.Type == ReceiptType.Gas).FirstOrDefault().Value;

                    await CreateGasReceipt(currentUserId, defaultGasValue, houseId, flatId);
                    break;
                case ReceiptType.HotWater:
                    var defaultHotWaterValue = defaultIndicators.Where(x => x.Type == ReceiptType.HotWater).FirstOrDefault().Value;

                    await CreateHotWaterReceipt(currentUserId, defaultHotWaterValue, houseId, flatId);
                    break;
                case ReceiptType.Intercom:
                    await CreateIntercomReceipt(currentUserId, houseId, flatId);
                    break;
                case ReceiptType.Rent:
                    await CreateRentReceipt(currentUserId, area.Value, houseId, flatId);
                    break;
                case ReceiptType.Sewerage:
                    await CreateSewerageReceipt(currentUserId, houseId, flatId);
                    break;
                default:
                    break;
            }
        }

        private async Task CreateColdWaterReceipt(Guid currentUserId, int defaultIndicatorValue, Guid? houseId = null, Guid? flatId = null)
        {
            const double coldWaterRate = 1.5;

            var lastReceipt = await _databaseManager.ReceiptRepository
                .Get(x => (x.HouseId == houseId && houseId != null || x.FlatId == flatId && flatId != null) && x.Type == ReceiptType.ColdWater)
                .OrderByDescending(x => x.CreatedDate)
                .FirstOrDefaultAsync();

            var receipt = new Receipt
            {
                CreatedBy = currentUserId,
                CreatedDate = DateTime.Now,
                HouseId = houseId,
                FlatId = flatId,
                Type = ReceiptType.ColdWater,
                Debt = lastReceipt != null ? lastReceipt.Total - lastReceipt.Paid : 0,
                Status = ReceiptStatus.Created,
                Rate = coldWaterRate,
                OldIndicator = lastReceipt?.NewIndicator ?? defaultIndicatorValue
            };

            await _databaseManager.ReceiptRepository.CreateAsync(receipt);
        }

        private async Task CreateElectricityReceipt(Guid currentUserId, int defaultIndicatorValue, Guid? houseId = null, Guid? flatId = null)
        {
            const double electricityRate = 1;

            var lastReceipt = await _databaseManager.ReceiptRepository
                .Get(x => (x.HouseId == houseId && houseId != null || x.FlatId == flatId && flatId != null) && x.Type == ReceiptType.Electricity)
                .OrderByDescending(x => x.CreatedDate)
                .FirstOrDefaultAsync();

            var receipt = new Receipt
            {
                CreatedBy = currentUserId,
                CreatedDate = DateTime.Now,
                HouseId = houseId,
                FlatId = flatId,
                Type = ReceiptType.Electricity,
                Debt = lastReceipt != null ? lastReceipt.Total - lastReceipt.Paid : 0,
                Status = ReceiptStatus.Created,
                Rate = electricityRate,
                OldIndicator = lastReceipt?.NewIndicator ?? defaultIndicatorValue
            };

            await _databaseManager.ReceiptRepository.CreateAsync(receipt);
        }

        private async Task CreateGarbageReceipt(Guid currentUserId, Guid? houseId = null, Guid? flatId = null)
        {
            const double garbageRate = 45;

            var lastReceipt = await _databaseManager.ReceiptRepository
                .Get(x => (x.HouseId == houseId && houseId != null || x.FlatId == flatId && flatId != null) && x.Type == ReceiptType.Garbage)
                .OrderByDescending(x => x.CreatedDate)
                .FirstOrDefaultAsync();

            var receipt = new Receipt
            {
                CreatedBy = currentUserId,
                CreatedDate = DateTime.Now,
                HouseId = houseId,
                FlatId = flatId,
                Type = ReceiptType.Garbage,
                Debt = lastReceipt != null ? lastReceipt.Total - lastReceipt.Paid : 0,
                Status = ReceiptStatus.Filled,
                Rate = garbageRate,
                Total = lastReceipt != null ? lastReceipt.Total - lastReceipt.Paid + Convert.ToDecimal(garbageRate) : Convert.ToDecimal(garbageRate)
            };
         
            await _databaseManager.ReceiptRepository.CreateAsync(receipt);
        }

        private async Task CreateGasReceipt(Guid currentUserId, int defaultIndicatorValue, Guid? houseId = null, Guid? flatId = null)
        {
            const double gasRate = 7.5;

            var lastReceipt = await _databaseManager.ReceiptRepository
                .Get(x => (x.HouseId == houseId && houseId != null || x.FlatId == flatId && flatId != null) && x.Type == ReceiptType.Gas)
                .OrderByDescending(x => x.CreatedDate)
                .FirstOrDefaultAsync();

            var receipt = new Receipt
            {
                CreatedBy = currentUserId,
                CreatedDate = DateTime.Now,
                HouseId = houseId,
                FlatId = flatId,
                Type = ReceiptType.Gas,
                Debt = lastReceipt != null ? lastReceipt.Total - lastReceipt.Paid : 0,
                Status = ReceiptStatus.Created,
                Rate = gasRate,
                OldIndicator = lastReceipt?.NewIndicator ?? defaultIndicatorValue
            };

            await _databaseManager.ReceiptRepository.CreateAsync(receipt);
        }

        private async Task CreateHotWaterReceipt(Guid currentUserId, int defaultIndicatorValue, Guid? houseId = null, Guid? flatId = null)
        {
            const double hotWaterRate = 1.8;

            var lastReceipt = await _databaseManager.ReceiptRepository
                .Get(x => (x.HouseId == houseId && houseId != null || x.FlatId == flatId && flatId != null) && x.Type == ReceiptType.HotWater)
                .OrderByDescending(x => x.CreatedDate)
                .FirstOrDefaultAsync();

            var receipt = new Receipt
            {
                CreatedBy = currentUserId,
                CreatedDate = DateTime.Now,
                HouseId = houseId,
                FlatId = flatId,
                Type = ReceiptType.HotWater,
                Debt = lastReceipt != null ? lastReceipt.Total - lastReceipt.Paid : 0,
                Status = ReceiptStatus.Created,
                Rate = hotWaterRate,
                OldIndicator = lastReceipt?.NewIndicator ?? defaultIndicatorValue
            };

            await _databaseManager.ReceiptRepository.CreateAsync(receipt);
        }

        private async Task CreateIntercomReceipt(Guid currentUserId, Guid? houseId = null, Guid? flatId = null)
        {
            const double intercomRate = 45;

            var lastReceipt = await _databaseManager.ReceiptRepository
                .Get(x => (x.HouseId == houseId && houseId != null || x.FlatId == flatId && flatId != null) && x.Type == ReceiptType.Intercom)
                .OrderByDescending(x => x.CreatedDate)
                .FirstOrDefaultAsync();

            var receipt = new Receipt
            {
                CreatedBy = currentUserId,
                CreatedDate = DateTime.Now,
                HouseId = houseId,
                FlatId = flatId,
                Type = ReceiptType.Intercom,
                Debt = lastReceipt != null ? lastReceipt.Total - lastReceipt.Paid : 0,
                Status = ReceiptStatus.Filled,
                Rate = intercomRate,
                Total = lastReceipt != null ? lastReceipt.Total - lastReceipt.Paid + Convert.ToDecimal(intercomRate) : Convert.ToDecimal(intercomRate)
            };

            await _databaseManager.ReceiptRepository.CreateAsync(receipt);
        }

        private async Task CreateRentReceipt(Guid currentUserId, double area, Guid? houseId = null, Guid? flatId = null)
        {
            const double rentRate = 2.5;

            var lastReceipt = await _databaseManager.ReceiptRepository
                .Get(x => (x.HouseId == houseId && houseId != null || x.FlatId == flatId && flatId != null) && x.Type == ReceiptType.Rent)
                .OrderByDescending(x => x.CreatedDate)
                .FirstOrDefaultAsync();

            var receipt = new Receipt
            {
                CreatedBy = currentUserId,
                CreatedDate = DateTime.Now,
                HouseId = houseId,
                FlatId = flatId,
                Type = ReceiptType.Rent,
                Debt = lastReceipt != null ? lastReceipt.Total - lastReceipt.Paid : 0,
                Status = ReceiptStatus.Filled,
                Rate = rentRate,
                Total = lastReceipt != null ? lastReceipt.Total - lastReceipt.Paid + Convert.ToDecimal(rentRate * area) : Convert.ToDecimal(rentRate * area)
            };

            await _databaseManager.ReceiptRepository.CreateAsync(receipt);
        }

        private async Task CreateSewerageReceipt(Guid currentUserId, Guid? houseId = null, Guid? flatId = null)
        {
            const double sewerageRate = 20;

            var lastReceipt = await _databaseManager.ReceiptRepository
                .Get(x => (x.HouseId == houseId && houseId != null || x.FlatId == flatId && flatId != null) && x.Type == ReceiptType.Sewerage)
                .OrderByDescending(x => x.CreatedDate)
                .FirstOrDefaultAsync();

            var receipt = new Receipt
            {
                CreatedBy = currentUserId,
                CreatedDate = DateTime.Now,
                HouseId = houseId,
                FlatId = flatId,
                Type = ReceiptType.Sewerage,
                Debt = lastReceipt != null ? lastReceipt.Total - lastReceipt.Paid : 0,
                Status = ReceiptStatus.Filled,
                Rate = sewerageRate,
                Total = lastReceipt != null ? lastReceipt.Total - lastReceipt.Paid + Convert.ToDecimal(sewerageRate) : Convert.ToDecimal(sewerageRate)
            };

            await _databaseManager.ReceiptRepository.CreateAsync(receipt);
        }
    }
}
