# 🚀 Projeto - Busca em RAG com Resposta por IA
Sistema em Back-End desenvolvido com **ASP.NET Core**.  

O projeto se trata de alimentar o Banco de Dados com artigos dos quais tenha interesse em certos tópicos, e obter um resumo ou resposta objetiva gerado por uma IA.  
O Armazenamento dos Artigos é feito em um Banco de Dados Postgres com uma extensão de **Vetores**, onde com um prompt é feito uma busca semântica no Banco de Dados e a IA trazendo uma resposta NLP.

## 🔍 Observações
Alguns dos pacotes até o momento se encontram em **Pre-release**.  
Precisa do .NET 8.0+ e de uma [chave Api do Gemini](https://aistudio.google.com/welcome).  
Arquivo pdf para testar funcionamento na **Pasta Asset**.
Caso queira testar com pdf muito grande checar a capacidade do modelo de **Embedding** que estiver em uso.

## 🧰 Pacotes

```csharp
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.SemanticKernel
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
dotnet add package Pgvector.EntityFrameworkCore  
dotnet add package PdfPig  
dotnet add package Microsoft.SemanticKernel.Connectors.PgVector --pre-release   
dotnet add package Microsoft.SemanticKernel.Connectors.Google --pre-release
```  

## ⚙️ Como Executar o Projeto  
### 1. Clone o repositório  
```bash
git clone https://github.com/mathpss/Busca_IA_RAG.git
```  
### 2. Restaure os pacotes 
```bash
dotnet restore
```

## 📦 Endpoints
POST ```http://localhost:5131/v1/artigo ``` end-point onde é feito o up-load de arquivos pdf.  

POST ```http://localhost:5131/v1/prompt``` end-point onde é passado o prompt com o questionamento sobre os arquivos salvos.

# Em breve... um front-end em Next.
