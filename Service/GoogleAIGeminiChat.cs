using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.AI;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.Google;
using RagPdfApi.Service.Interfaces;

namespace RagPdfApi.Service
{
    public class GoogleAIGeminiChat : IGoogleAIGeminiChat
    {
        private GoogleAIGeminiChatCompletionService _generator;

        public GoogleAIGeminiChat(GoogleAIGeminiChatCompletionService generator)
        {
            _generator = generator;
        }

        public async Task<string> ResponseAI(IEnumerable<string> resultSearch, string prompt)
        {
            var contextsAI = resultSearch.Aggregate((total, next ) => total += next);
            var chatHistory = new ChatHistory
            {
            new ChatMessageContent(AuthorRole.System,
            $"Você é um professor amigável que responde apenas com base nos seguintes conteúdos:\n{contextsAI}\n" +
            "Se a pergunta do usuário não tiver relação com esses conteúdos, diga educadamente que não sabe."),

            new ChatMessageContent(AuthorRole.User, prompt)

            };



            var promptOptions = new GeminiPromptExecutionSettings
            {
                MaxTokens = 100,
                Temperature = 0,
                TopP = 0
            };


            var response = await _generator.GetChatMessageContentAsync(chatHistory, promptOptions);

            return response?.Content ?? string.Empty;

        }
    }
}