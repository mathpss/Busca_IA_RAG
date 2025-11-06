using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pgvector;
using Pgvector.EntityFrameworkCore;
using RagPdfApi.Context;
using RagPdfApi.Models;
using RagPdfApi.Repositories.Interfaces;

namespace RagPdfApi.Repositories
{
    public class ArtigoChunkRepository : Repository<ArtigoChunk> , IArtigoChunkRepository
    {
        private readonly DbSet<ArtigoChunk> _dbSet;
        public ArtigoChunkRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _dbSet = appDbContext.Set<ArtigoChunk>();
        }
        ///<summary>
        /// Função de busca semântica no Banco de Dados.
        /// <param name="embedding">Vetor de entrada como parâmetro de busca</param>
        /// 
        public async Task<IEnumerable<string>> GetContentByVector(ReadOnlyMemory<float> embedding)
        {
            return await _dbSet.AsNoTracking()
                    .Where(x => x.Vetor.CosineDistance(new Vector(embedding)) < 0.3)
                    .OrderBy(x => x.Vetor.CosineDistance(new Vector(embedding)))                  
                    .Select(x => x.Conteudo)
                    .ToListAsync();
        }
    }
}