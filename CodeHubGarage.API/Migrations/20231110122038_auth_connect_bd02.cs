using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeHubGarage.API.Migrations
{
    /// <inheritdoc />
    public partial class auth_connect_bd02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CarroPlaca",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarroPlaca",
                table: "Users");
        }
    }
}
