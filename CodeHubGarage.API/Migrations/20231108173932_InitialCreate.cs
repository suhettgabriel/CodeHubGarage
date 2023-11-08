using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeHubGarage.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FormasPagamento",
                columns: table => new
                {
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Garagens",
                columns: table => new
                {
                    Codigo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Preco_1aHora = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Preco_HorasExtra = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Preco_Mensalista = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Garagens", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "Passagens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Garagem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarroPlaca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarroMarca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarroModelo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataHoraEntrada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataHoraSaida = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FormaPagamento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrecoTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passagens", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FormasPagamento");

            migrationBuilder.DropTable(
                name: "Garagens");

            migrationBuilder.DropTable(
                name: "Passagens");
        }
    }
}
