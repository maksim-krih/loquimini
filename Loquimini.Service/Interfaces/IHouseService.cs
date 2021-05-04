using Loquimini.Model.Entities;
using Loquimini.Model.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loquimini.Service.Interfaces
{
    public interface IHouseService : IDisposable
    {
        Task<House> CreateHouseAsync(House house);
	}
}
