using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoloAdventureAPI.Migrations
{
    public partial class MigracaoInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Idiomas",
                columns: table => new
                {
                    IdiomaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IdiomaAtivo = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Idiomas", x => x.IdiomaId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Aventuras",
                columns: table => new
                {
                    AventuraId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Titulo = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DescricaoRapida = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AventuraAtiva = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValue: new DateTime(2022, 11, 16, 16, 3, 23, 393, DateTimeKind.Local).AddTicks(8330)),
                    DataAtualizada = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValue: new DateTime(2022, 11, 16, 16, 3, 23, 393, DateTimeKind.Local).AddTicks(8460)),
                    Versao = table.Column<float>(type: "float", nullable: false, defaultValue: 0.01f),
                    ImagemUrl = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IdiomaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aventuras", x => x.AventuraId);
                    table.ForeignKey(
                        name: "FK_Aventuras_Idiomas_IdiomaId",
                        column: x => x.IdiomaId,
                        principalTable: "Idiomas",
                        principalColumn: "IdiomaId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Passos",
                columns: table => new
                {
                    PassoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Texto = table.Column<string>(type: "varchar(3000)", maxLength: 3000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ImagemUrl = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PrimeiroPasso = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PassoAtivo = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    AventuraId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passos", x => x.PassoId);
                    table.ForeignKey(
                        name: "FK_Passos_Aventuras_AventuraId",
                        column: x => x.AventuraId,
                        principalTable: "Aventuras",
                        principalColumn: "AventuraId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OrigensDestinos",
                columns: table => new
                {
                    PassoOrigemId = table.Column<int>(type: "int", nullable: false),
                    PassoDestinoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrigensDestinos", x => new { x.PassoOrigemId, x.PassoDestinoId });
                    table.ForeignKey(
                        name: "FK_OrigensDestinos_Passos_PassoDestinoId",
                        column: x => x.PassoDestinoId,
                        principalTable: "Passos",
                        principalColumn: "PassoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrigensDestinos_Passos_PassoOrigemId",
                        column: x => x.PassoOrigemId,
                        principalTable: "Passos",
                        principalColumn: "PassoId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Aventuras_IdiomaId",
                table: "Aventuras",
                column: "IdiomaId");

            migrationBuilder.CreateIndex(
                name: "IX_OrigensDestinos_PassoDestinoId",
                table: "OrigensDestinos",
                column: "PassoDestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_Passos_AventuraId",
                table: "Passos",
                column: "AventuraId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrigensDestinos");

            migrationBuilder.DropTable(
                name: "Passos");

            migrationBuilder.DropTable(
                name: "Aventuras");

            migrationBuilder.DropTable(
                name: "Idiomas");
        }
    }
}
