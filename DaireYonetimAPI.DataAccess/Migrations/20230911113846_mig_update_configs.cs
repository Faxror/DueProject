using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DaireYonetimAPI.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class mig_update_configs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SmptUsersMailTitle",
                table: "Configs",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SmptUsersMailTitle",
                table: "Configs");
        }
    }
}
