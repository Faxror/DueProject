using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DaireYonetimAPI.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class update_database_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SmptSenderServers",
                table: "Configs",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SmptSenderServers",
                table: "Configs");
        }
    }
}
