using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DaireYonetimAPI.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class update_database : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SmptSenderUsers",
                table: "Configs",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SmptSenderUsers",
                table: "Configs");
        }
    }
}
