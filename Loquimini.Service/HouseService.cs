using AutoMapper;
using AutoMapper.QueryableExtensions;
using Loquimini.Common.Enums;
using Loquimini.Model.Entities;
using Loquimini.ModelDTO.HouseDTO;
using Loquimini.ModelDTO.UserDTO;
using Loquimini.Repository.UnitOfWork;
using Loquimini.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loquimini.Service
{
	public class HouseService : BaseService, IHouseService
	{
        private readonly IMapper _mapper;

		public HouseService(
            IDatabaseManager databaseManager,
            IMapper mapper
        ) : base(databaseManager)
        {
            _mapper = mapper;
        }

        public async Task<House> CreateHouseAsync(House house)
        {
            var createdHouse = await _databaseManager.HouseRepository.CreateAsync(house);

            await _databaseManager.SaveChangesAsync();

            return createdHouse;
        }

        public async Task<House> UpdateHouseAsync(HouseDTO houseDTO)
        {
            var house = await _databaseManager.HouseRepository
                .Get(x => x.Id == houseDTO.Id)
                .Include(x => x.Flats)
                    .ThenInclude(x => x.Info)
                .Include(x => x.Info)
                .FirstOrDefaultAsync();

            house.Street = houseDTO.Street;
            house.Number = houseDTO.Number;
            house.Type = houseDTO.Type;

            if (houseDTO.Type != house.Type)
            {
                if (houseDTO.Type == HouseType.Private)
                {
                    _databaseManager.BuildingInfoRepository.Delete(house.Info);
                }
                else
                {
                    var newInfo = _mapper.Map<BuildingInfo>(houseDTO.Info);
                    newInfo.HouseId = house.Id;

                    await _databaseManager.BuildingInfoRepository.CreateAsync(newInfo);
                }
            }
            else
            {
                var item = _mapper.Map(houseDTO.Info, house.Info);
            }

            foreach (var flat in house.Flats)
            {
                var updatedFlat = houseDTO.Flats.FirstOrDefault(x => x.Id == flat.Id);
                
                if (updatedFlat == null)
                {
                    _databaseManager.FlatRepository.Delete(flat);
                }
                else
                {
                    var item = _mapper.Map(updatedFlat, flat);

                    _databaseManager.FlatRepository.Update(item);

                }
            }

            foreach (var flat in houseDTO.Flats)
            {
                if (!house.Flats.Any(x => x.Id == flat.Id))
                {
                    var newFlat = _mapper.Map<Flat>(flat);
                    newFlat.HouseId = house.Id;

                    await _databaseManager.FlatRepository.CreateAsync(newFlat);
                }
            }

            house = _databaseManager.HouseRepository.Update(house);

            _databaseManager.SaveChanges();

            return house;
        }
    }
}
