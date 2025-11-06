using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using RagPdfApi.Context;
using RagPdfApi.Repositories.Interfaces;

namespace RagPdfApi.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _AppDbContext;
        private IDbContextTransaction? _Transaction;

        public UnitOfWork(AppDbContext appDbContext)
        {
            _AppDbContext = appDbContext;
        }

        public async Task BeginTransactionAsync()
        {
            if (_Transaction != null) return;

            _Transaction = await _AppDbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_Transaction == null) throw new InvalidOperationException("Nenhuma transição iniciada");

            try
            {
                await _AppDbContext.SaveChangesAsync();
                await _Transaction.CommitAsync();
            }
            catch (Exception)
            {
                await RollbackTransactionAsync();
                throw;
            }
            finally
            {
                await _Transaction.DisposeAsync();
                _Transaction = null;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if(_Transaction != null)
            {
                await _Transaction.RollbackAsync();
                await _Transaction.DisposeAsync();
                _Transaction = null;
            }
        }
    }
}