using Microsoft.EntityFrameworkCore;
using SoloAdventureAPI.Models;

namespace SoloAdventureAPI.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Aventura> Aventuras { get; set; }
    public DbSet<Passo> Passos { get; set; }
    public DbSet<OrigemDestino> PassosProximosPassos { get; set; }
    public DbSet<Idioma> Idiomas { get; internal set; }
    public DbSet<OrigemDestino> OrigensDestinos { get; internal set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ----- Configurando Model Aventura -----
        modelBuilder.Entity<Aventura>()
            .ToTable("Aventuras");

        modelBuilder.Entity<Aventura>()
            .HasKey(a => a.AventuraId);

        modelBuilder.Entity<Aventura>()
            .Property(a => a.Titulo)
                .HasMaxLength(100)
                .IsRequired();

        modelBuilder.Entity<Aventura>()
            .Property(a => a.DescricaoRapida)
                .HasMaxLength(300)
                .IsRequired();

        modelBuilder.Entity<Aventura>()
            .Property(a => a.AventuraAtiva)
                .HasDefaultValue(false)
                .IsRequired();

        modelBuilder.Entity<Aventura>()
            .Property(a => a.DataCadastro)
                .HasDefaultValue(DateTime.Now)
                .IsRequired();

        modelBuilder.Entity<Aventura>()
            .Property(a => a.DataAtualizada)
                .HasDefaultValue(DateTime.Now)
                .IsRequired();

        modelBuilder.Entity<Aventura>()
            .Property(a => a.Versao)
                .HasDefaultValue(0.01)
                .IsRequired();

        modelBuilder.Entity<Aventura>()
            .Property(a => a.ImagemUrl)
                .HasMaxLength(300);

        modelBuilder.Entity<Aventura>()
            .Property(a => a.IdiomaId)
                .IsRequired();


        // ----- Configurando Model Passo -----
        modelBuilder.Entity<Passo>()
            .ToTable("Passos");

        modelBuilder.Entity<Passo>()
            .HasKey(p => p.PassoId);

        modelBuilder.Entity<Passo>()
            .Property(p => p.Nome)
                .HasMaxLength(100)
                .IsRequired();

        modelBuilder.Entity<Passo>()
            .Property(p => p.Texto)
                .HasMaxLength(3000)
                .IsRequired();

        modelBuilder.Entity<Aventura>()
            .Property(a => a.ImagemUrl)
                .HasMaxLength(300);

        modelBuilder.Entity<Passo>()
            .Property(p => p.PrimeiroPasso)
                .IsRequired();

        modelBuilder.Entity<Passo>()
            .Property(p => p.PassoAtivo)
                .HasDefaultValue(true)
                .IsRequired();


        // ----- Configurando Model Idioma -----
        modelBuilder.Entity<Idioma>()
            .HasKey(p => p.IdiomaId);

        modelBuilder.Entity<Idioma>()
            .ToTable("Idiomas");

        modelBuilder.Entity<Idioma>()
            .Property(i => i.Nome)
                .HasMaxLength(100)
                .IsRequired();

        modelBuilder.Entity<Idioma>()
            .Property(i => i.IdiomaAtivo)
                .HasDefaultValue(true)
                .IsRequired();

        // ----- Configurando Model OrigemDestino -----
        modelBuilder.Entity<OrigemDestino>()
            .ToTable("OrigensDestinos");


        //  ----- Configurando Relação Aventura 1 x N Passos -----

        modelBuilder.Entity<Aventura>()
            .HasMany(a => a.Passos)
            .WithOne(p => p.Aventura);


        //  ----- Configurando Relação N x N OrigemDestino -----

        // Criando Chave Composta
        modelBuilder.Entity<OrigemDestino>()
            .HasKey(p => new { p.PassoOrigemId, p.PassoDestinoId });

        modelBuilder.Entity<OrigemDestino>()
            .HasOne(od => od.PassoOrigem)
            .WithMany(od => od.Origens);

        modelBuilder.Entity<OrigemDestino>()
            .HasOne(od => od.PassoDestino)
            .WithMany(od => od.Destinos);

        //  ----- Configurando Relação Aventura 1 x 1 Idioma -----
        modelBuilder.Entity<Idioma>()
            .HasMany(i => i.Aventuras)
            .WithOne(a => a.Idioma);
    }
}
