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
	public class UserService : BaseService, IUserService
	{
		public UserService(IDatabaseManager databaseManager) : base(databaseManager)
		{

		}

	}
}
