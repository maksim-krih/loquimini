using Loquimini.Repository.UnitOfWork;
using Loquimini.Service.Interfaces;
using System.Threading.Tasks;

namespace Loquimini.Service
{
	public class UserService : BaseService, IUserService
	{
		public UserService(IDatabaseManager databaseManager) : base(databaseManager)
		{

		}

		public async Task<object> Login(object loginUser)
		{
			return null;
		}
	}
}
