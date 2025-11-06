using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.SemanticKernel;

namespace RagPdfApi.Service.Interfaces
{
    public interface IGoogleAIGeminiChat
    {
        Task<string> ResponseAI(IEnumerable<string> contextsAI, string prompt);
    }
}