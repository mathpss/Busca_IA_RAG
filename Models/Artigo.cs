using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RagPdfApi.Models
{
    public class Artigo : Entity
    {
        
        public string Titulo { get; set; } = string.Empty;
        public List<ArtigoChunk> ArtigoChunks { get; set; } = [];
        public DateTime DataArtigo { get; set; } = DateTime.Now;


    }
}