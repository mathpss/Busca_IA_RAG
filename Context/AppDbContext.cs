using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RagPdfApi.Models;

namespace RagPdfApi.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ArtigoChunk> ArtigoChunks { get; set; }
        public DbSet<Artigo> Artigos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("vector");

            modelBuilder.Entity<Artigo>(entity =>
            {
                entity.ToTable("artigos");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).HasColumnName("id").UseIdentityAlwaysColumn();
                entity.Property(x => x.Titulo).HasColumnName("titulo");
                entity.Property(x => x.DataArtigo).HasColumnName("data_artigo");
                entity.HasMany(e => e.ArtigoChunks).WithOne(e => e.Artigo).HasForeignKey(e => e.ArtigoId).IsRequired();

            });

            modelBuilder.Entity<ArtigoChunk>(entity =>
            {
                entity.ToTable("artigos_chunks");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).HasColumnName("id").UseIdentityAlwaysColumn();
                entity.Property(x => x.ArtigoId).HasColumnName("artigo_id");
                entity.Property(x => x.Conteudo).HasColumnName("conteudo");
                entity.Property(x => x.Vetor).HasColumnName("vector").HasColumnType("vector(384)");
                
            });
        }
    }
}