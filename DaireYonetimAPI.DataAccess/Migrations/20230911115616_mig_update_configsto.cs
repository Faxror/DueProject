using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DaireYonetimAPI.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class mig_update_configsto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalLastPayment",
                table: "Configs",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalLastPayment",
                table: "Configs");
        }
    }
}
