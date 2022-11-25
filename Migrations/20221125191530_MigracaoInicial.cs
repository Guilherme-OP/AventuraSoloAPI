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
                name: "Aventuras",
                columns: table => new
                {
                    AventuraId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Titulo = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descricao = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AventuraAtiva = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValue: new DateTime(2022, 11, 25, 16, 15, 30, 231, DateTimeKind.Local).AddTicks(8530))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aventuras", x => x.AventuraId);
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
                    Inicio = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PassoAtivo = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
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
        }
    }
}
