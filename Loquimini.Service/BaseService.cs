using Loquimini.Repository.UnitOfWork;
using System;

namespace Loquimini.Service
{
    public class BaseService : IDisposable
    {
        protected readonly IDatabaseManager _databaseManager;

        public BaseService(IDatabaseManager databaseManager)
        {
            _databaseManager = databaseManager;
        }

        public void Dispose()
        {
            _databaseManager.Dispose();
        }
    }
}
