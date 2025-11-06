using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RagPdfApi.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}