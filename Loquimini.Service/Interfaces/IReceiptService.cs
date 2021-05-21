using Loquimini.Model.Entities;
using Loquimini.ModelDTO.HouseDTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loquimini.Service.Interfaces
{
    public interface IReceiptService : IDisposable
    {
        Task<bool> GenerateReceipts();

        Task<List<Receipt>> GetByUserId(Guid id);
    }
}
