using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DaireYonetimAPI.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class create_database_pgadmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bakiyes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApartmentNo = table.Column<int>(type: "integer", nullable: false),
                    Paid = table.Column<decimal>(type: "numeric", nullable: false),
                    ApartmentId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bakiyes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Configs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    value = table.Column<string>(type: "text", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SmptEmailAddress = table.Column<string>(type: "text", nullable: false),
                    SmptEmailPassword = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Daires",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    apartmentno = table.Column<int>(type: "integer", nullable: false),
                    familyname = table.Column<string>(type: "text", nullable: false),
                    apartmentemail = table.Column<string>(type: "text", nullable: false),
                    BakiyeId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Daires", x => x.id);
                    table.ForeignKey(
                        name: "FK_Daires_Bakiyes_BakiyeId",
                        column: x => x.BakiyeId,
                        principalTable: "Bakiyes",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Daires_BakiyeId",
                table: "Daires",
                column: "BakiyeId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Configs");

            migrationBuilder.DropTable(
                name: "Daires");

            migrationBuilder.DropTable(
                name: "Bakiyes");
        }
    }
}
