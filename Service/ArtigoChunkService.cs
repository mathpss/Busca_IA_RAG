using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.AI;
using Microsoft.SemanticKernel.Connectors.Google;
using Microsoft.SemanticKernel.Text;
using RagPdfApi.Models;
using RagPdfApi.Service.Interfaces;

namespace RagPdfApi.Service
{
    public class ArtigoChunkService : IArtigoChunkService
    {
        private readonly GoogleAIEmbeddingGenerator _generator;

        public ArtigoChunkService(GoogleAIEmbeddingGenerator generator)
        {
            _generator = generator;
        }

        public async Task<List<ArtigoChunk>> GerarListaVetorizada(string texto)
        {

            var listaArtigoChunk = new List<ArtigoChunk>();
            
            #pragma warning disable SKEXP0050 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
                var textChunk = TextChunker.SplitPlainTextLines(texto, 250);
            #pragma warning restore SKEXP0050 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.            

            if (textChunk != null)
            {
                foreach (var item in textChunk)
                {
                    var vetor = await _generator.GenerateVectorAsync(item);
                    var artigoChunk = new ArtigoChunk
                    {
                        Vetor = new Pgvector.Vector(vetor),
                        Conteudo = item
                    };
                    listaArtigoChunk.Add(artigoChunk);
                }
            }
            return listaArtigoChunk ?? throw new NullReferenceException("Parâmetro entrou vazio/nulo");
        }
    }
}