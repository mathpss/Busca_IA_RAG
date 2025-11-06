using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RagPdfApi.Context;
using RagPdfApi.Models;
using RagPdfApi.Repositories.Interfaces;

namespace RagPdfApi.Repositories
{
    public class ArtigoRepository : Repository<Artigo> , IArtigoRepository
    {
        public ArtigoRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            
        }
    }
}