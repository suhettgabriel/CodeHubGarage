using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeHubGarage.API.Migrations
{
    /// <inheritdoc />
    public partial class auth_connect_bd07 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FormasPagamentoCodigo",
                table: "Estacionamentos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "isMensalista",
                table: "Estacionamentos",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FormasPagamentoCodigo",
                table: "Estacionamentos");

            migrationBuilder.DropColumn(
                name: "isMensalista",
                table: "Estacionamentos");
        }
    }
}
