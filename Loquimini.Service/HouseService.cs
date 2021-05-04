using AutoMapper.QueryableExtensions;
using Loquimini.Model.Entities;
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
		public HouseService(IDatabaseManager databaseManager) : base(databaseManager)
		{

		}

        public async Task<House> CreateHouseAsync(House house)
        {
            
            return null;
        }

    }
}
