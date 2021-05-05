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
	public class UserService : BaseService, IUserService
	{
        private readonly IMapper _mapper;

		public UserService(
            IDatabaseManager databaseManager,
            IMapper mapper
        ) : base(databaseManager)
        {
            _mapper = mapper;
        }

        public async Task<User> UpdateUserAsync(UserDTO userDTO)
        {
            var user = await _databaseManager.UserRepository
                .Get(x => x.Id == userDTO.Id)
                .FirstOrDefaultAsync();

            user.Email = userDTO.Email;
            user.FirstName = userDTO.FirstName;
            user.LastName = userDTO.LastName;

            user = _databaseManager.UserRepository.Update(user);

            _databaseManager.SaveChanges();

            return user;
        }
    }
}
