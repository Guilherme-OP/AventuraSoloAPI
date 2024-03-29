﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SoloAdventureAPI.Context;

#nullable disable

namespace SoloAdventureAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20221121192625_MigracaoInicial")]
    partial class MigracaoInicial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("SoloAdventureAPI.Models.Aventura", b =>
                {
                    b.Property<int>("AventuraId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool?>("AventuraAtiva")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(false);

                    b.Property<DateTime>("DataAtualizada")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValue(new DateTime(2022, 11, 21, 16, 26, 25, 795, DateTimeKind.Local).AddTicks(9270));

                    b.Property<DateTime>("DataCadastro")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValue(new DateTime(2022, 11, 21, 16, 26, 25, 795, DateTimeKind.Local).AddTicks(9148));

                    b.Property<string>("DescricaoRapida")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<int>("IdiomaId")
                        .HasColumnType("int");

                    b.Property<string>("ImagemUrl")
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<float>("Versao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("float")
                        .HasDefaultValue(0.01f);

                    b.HasKey("AventuraId");

                    b.HasIndex("IdiomaId");

                    b.ToTable("Aventuras", (string)null);
                });

            modelBuilder.Entity("SoloAdventureAPI.Models.Idioma", b =>
                {
                    b.Property<int>("IdiomaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("IdiomaAtivo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(true);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("IdiomaId");

                    b.ToTable("Idiomas", (string)null);
                });

            modelBuilder.Entity("SoloAdventureAPI.Models.OrigemDestino", b =>
                {
                    b.Property<int>("PassoOrigemId")
                        .HasColumnType("int");

                    b.Property<int>("PassoDestinoId")
                        .HasColumnType("int");

                    b.HasKey("PassoOrigemId", "PassoDestinoId");

                    b.HasIndex("PassoDestinoId");

                    b.ToTable("OrigensDestinos", (string)null);
                });

            modelBuilder.Entity("SoloAdventureAPI.Models.Passo", b =>
                {
                    b.Property<int>("PassoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AventuraId")
                        .HasColumnType("int");

                    b.Property<string>("ImagemUrl")
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<bool>("PassoAtivo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(true);

                    b.Property<bool>("PrimeiroPasso")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Texto")
                        .IsRequired()
                        .HasMaxLength(3000)
                        .HasColumnType("varchar(3000)");

                    b.HasKey("PassoId");

                    b.HasIndex("AventuraId");

                    b.ToTable("Passos", (string)null);
                });

            modelBuilder.Entity("SoloAdventureAPI.Models.Aventura", b =>
                {
                    b.HasOne("SoloAdventureAPI.Models.Idioma", "Idioma")
                        .WithMany("Aventuras")
                        .HasForeignKey("IdiomaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Idioma");
                });

            modelBuilder.Entity("SoloAdventureAPI.Models.OrigemDestino", b =>
                {
                    b.HasOne("SoloAdventureAPI.Models.Passo", "PassoDestino")
                        .WithMany("Destinos")
                        .HasForeignKey("PassoDestinoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SoloAdventureAPI.Models.Passo", "PassoOrigem")
                        .WithMany("Origens")
                        .HasForeignKey("PassoOrigemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PassoDestino");

                    b.Navigation("PassoOrigem");
                });

            modelBuilder.Entity("SoloAdventureAPI.Models.Passo", b =>
                {
                    b.HasOne("SoloAdventureAPI.Models.Aventura", "Aventura")
                        .WithMany("Passos")
                        .HasForeignKey("AventuraId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Aventura");
                });

            modelBuilder.Entity("SoloAdventureAPI.Models.Aventura", b =>
                {
                    b.Navigation("Passos");
                });

            modelBuilder.Entity("SoloAdventureAPI.Models.Idioma", b =>
                {
                    b.Navigation("Aventuras");
                });

            modelBuilder.Entity("SoloAdventureAPI.Models.Passo", b =>
                {
                    b.Navigation("Destinos");

                    b.Navigation("Origens");
                });
#pragma warning restore 612, 618
        }
    }
}
