using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RagPdfApi.Models;

namespace RagPdfApi.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        Task AddAsync(TEntity entity);
        Task<TEntity> GetById(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();

        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}