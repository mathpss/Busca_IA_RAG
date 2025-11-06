using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RagPdfApi.Models;

namespace RagPdfApi.Service.Interfaces
{
    public interface IArtigoChunkService
    {
        Task<List<ArtigoChunk>> GerarListaVetorizada(string texto);
    }
}