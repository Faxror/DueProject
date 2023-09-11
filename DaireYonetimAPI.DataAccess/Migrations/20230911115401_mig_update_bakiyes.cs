using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DaireYonetimAPI.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class mig_update_bakiyes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "LastPayment",
                table: "Bakiyes",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastPayment",
                table: "Bakiyes");
        }
    }
}
