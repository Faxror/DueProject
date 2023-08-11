using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DaireYonetimAPI.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class mig_createdatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bakiyes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DaireNo = table.Column<int>(type: "int", nullable: false),
                    BorcBakiye = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Faiz = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToplamBorc = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SonOdemeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OdemeTutari = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DaireId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bakiyes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Daires",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    dairenumber = table.Column<int>(type: "int", nullable: false),
                    daireisim = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    daireeposta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    daireaidat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    geçerliliktarihi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    aidatödemedurumu = table.Column<bool>(type: "bit", nullable: false),
                    BorcBakiye = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SonOdemeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BakiyeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Daires", x => x.id);
                    table.ForeignKey(
                        name: "FK_Daires_Bakiyes_id",
                        column: x => x.id,
                        principalTable: "Bakiyes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Daires");

            migrationBuilder.DropTable(
                name: "Bakiyes");
        }
    }
}
