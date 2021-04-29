using Loquimini.Repository.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace Loquimini.API.Controllers.Base
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IDatabaseManager _databaseManager;

        public BaseController(IDatabaseManager databaseManager)
        {
            _databaseManager = databaseManager;
        }
    }
}