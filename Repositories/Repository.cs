using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RagPdfApi.Context;
using RagPdfApi.Models;
using RagPdfApi.Repositories.Interfaces;

namespace RagPdfApi.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        private readonly DbSet<TEntity> _DbSet;
        
        public Repository(AppDbContext _AppDbContext)
        {
            _DbSet = _AppDbContext.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
           await _DbSet.AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            _DbSet.Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
           return await _DbSet.AsNoTracking().ToListAsync();
        }

        public async Task<TEntity> GetById(int id)
        {
            return await _DbSet.FindAsync(id) ?? throw new NullReferenceException($"Não Encontrado o id: {id}");
        }

        public void Update(TEntity entity)
        {
             _DbSet.Update(entity);
        }
    }
}