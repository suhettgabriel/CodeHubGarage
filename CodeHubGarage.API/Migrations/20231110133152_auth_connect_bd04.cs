using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeHubGarage.API.Migrations
{
    /// <inheritdoc />
    public partial class auth_connect_bd04 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsMensalista",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMensalista",
                table: "Users");
        }
    }
}
