using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RagPdfApi.Models;

namespace RagPdfApi.Repositories.Interfaces
{
    public interface IArtigoChunkRepository : IRepository<ArtigoChunk>
    {
        Task<IEnumerable<string>> GetContentByVector(ReadOnlyMemory<float> embedding);
    }
}