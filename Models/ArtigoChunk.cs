using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pgvector;

namespace RagPdfApi.Models
{
    public class ArtigoChunk : Entity
    {

        
        public int ArtigoId { get; set; }
        public Artigo Artigo { get; set; } = null!;
        public string Conteudo { get; set; } = string.Empty;
        public required Vector Vetor { get; set; }
    }
}