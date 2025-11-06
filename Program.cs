using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.AI;
using Microsoft.SemanticKernel.Connectors.Google;
using RagPdfApi.Context;
using RagPdfApi.Models;
using RagPdfApi.Models.Dto;
using RagPdfApi.Repositories;
using RagPdfApi.Repositories.Interfaces;
using RagPdfApi.Service;
using RagPdfApi.Service.Interfaces;

var builder = WebApplication.CreateBuilder(args);
///<summary>
/// <param name="apiKey">Local onde deve ser passado a chave api.</param>
/// 
var apiKey = builder.Configuration["Gemini_Api"] ?? throw new NullReferenceException("Não encontrado a chave Gemini_Api");

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(connectionString, x => x.UseVector()));

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IArtigoRepository, ArtigoRepository>();
builder.Services.AddScoped<IArtigoChunkRepository, ArtigoChunkRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IArtigoChunkService, ArtigoChunkService>();
builder.Services.AddScoped<IGoogleAIGeminiChat, GoogleAIGeminiChat>();

builder.Services.AddTransient(x =>
    new GoogleAIEmbeddingGenerator(
                modelId: "gemini-embedding-001",
                apiKey: apiKey,
                dimensions: 384
    )
);

builder.Services.AddTransient(x =>
    new GoogleAIGeminiChatCompletionService(
        apiKey: apiKey,
        modelId:"gemini-2.0-flash-lite"
    )
);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
///<summary>
/// Rota para salvar um artigo em pdf no Banco de Dados.
/// 
app.MapPost("v1/artigo", async ( IFormFile file,
    [FromServices] IArtigoRepository _artigoRepository,
    [FromServices] IArtigoChunkService _artigoChunkService,
    [FromServices] IUnitOfWork _uow ) =>
{
    try
    {
        string pdfToString = PdfPigService.PdfToString(file);
        List<ArtigoChunk> listaArtigoChunk = await _artigoChunkService.GerarListaVetorizada(pdfToString);

        var artigo = new Artigo
        {
            ArtigoChunks = listaArtigoChunk,
            Titulo = PdfPigService.TitleOrName(file)
        };

        await _uow.BeginTransactionAsync();
        await _artigoRepository.AddAsync(artigo);
        await _uow.CommitTransactionAsync();

        return Results.Created($"/artigo/{artigo.Id}", artigo);
    }
    catch (Exception ex)
    {
        throw new ArgumentException("Falha na inserção do artigo no DB: ", ex);
    }

}).AllowAnonymous().DisableAntiforgery();
///<summary>
/// Rota onde passam um prompt para fazer pesquisa sobre os artigos no Banco de Dados.
/// 
app.MapPost("v1/prompt", async (
    [FromBody] PromptDto prompt,
    GoogleAIEmbeddingGenerator generator,
    IArtigoChunkRepository _artigoChunkrepository,
    IGoogleAIGeminiChat _geminiChat
) =>
{
    var embedding = await generator.GenerateVectorAsync(prompt.Prompt);
    var resultSearch = await _artigoChunkrepository.GetContentByVector(embedding);

    var reponseAI = await _geminiChat.ResponseAI(resultSearch, prompt.Prompt);

    return Results.Ok(reponseAI);

});

app.Run();
