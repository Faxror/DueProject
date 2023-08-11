using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DaireYonetimAPI.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class mig_updatedatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "geçerliliktarihi",
                table: "Daires",
                newName: "dateofvalidity");

            migrationBuilder.RenameColumn(
                name: "dairenumber",
                table: "Daires",
                newName: "apartmentno");

            migrationBuilder.RenameColumn(
                name: "daireisim",
                table: "Daires",
                newName: "apartmentname");

            migrationBuilder.RenameColumn(
                name: "daireeposta",
                table: "Daires",
                newName: "apartmentemail");

            migrationBuilder.RenameColumn(
                name: "daireaidat",
                table: "Daires",
                newName: "apartmentdues");

            migrationBuilder.RenameColumn(
                name: "aidatödemedurumu",
                table: "Daires",
                newName: "payduestatus");

            migrationBuilder.RenameColumn(
                name: "SonOdemeTarihi",
                table: "Daires",
                newName: "PaymentdueDate");

            migrationBuilder.RenameColumn(
                name: "BorcBakiye",
                table: "Daires",
                newName: "BalanceDue");

            migrationBuilder.RenameColumn(
                name: "ToplamBorc",
                table: "Bakiyes",
                newName: "TotalDebt");

            migrationBuilder.RenameColumn(
                name: "SonOdemeTarihi",
                table: "Bakiyes",
                newName: "LastPaymentDate");

            migrationBuilder.RenameColumn(
                name: "OdemeTutari",
                table: "Bakiyes",
                newName: "PaymentAmount");

            migrationBuilder.RenameColumn(
                name: "DaireNo",
                table: "Bakiyes",
                newName: "ApartmentNo");

            migrationBuilder.RenameColumn(
                name: "BorcBakiye",
                table: "Bakiyes",
                newName: "BalanceDue");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "payduestatus",
                table: "Daires",
                newName: "aidatödemedurumu");

            migrationBuilder.RenameColumn(
                name: "dateofvalidity",
                table: "Daires",
                newName: "geçerliliktarihi");

            migrationBuilder.RenameColumn(
                name: "apartmentno",
                table: "Daires",
                newName: "dairenumber");

            migrationBuilder.RenameColumn(
                name: "apartmentname",
                table: "Daires",
                newName: "daireisim");

            migrationBuilder.RenameColumn(
                name: "apartmentemail",
                table: "Daires",
                newName: "daireeposta");

            migrationBuilder.RenameColumn(
                name: "apartmentdues",
                table: "Daires",
                newName: "daireaidat");

            migrationBuilder.RenameColumn(
                name: "PaymentdueDate",
                table: "Daires",
                newName: "SonOdemeTarihi");

            migrationBuilder.RenameColumn(
                name: "BalanceDue",
                table: "Daires",
                newName: "BorcBakiye");

            migrationBuilder.RenameColumn(
                name: "TotalDebt",
                table: "Bakiyes",
                newName: "ToplamBorc");

            migrationBuilder.RenameColumn(
                name: "PaymentAmount",
                table: "Bakiyes",
                newName: "OdemeTutari");

            migrationBuilder.RenameColumn(
                name: "LastPaymentDate",
                table: "Bakiyes",
                newName: "SonOdemeTarihi");

            migrationBuilder.RenameColumn(
                name: "BalanceDue",
                table: "Bakiyes",
                newName: "BorcBakiye");

            migrationBuilder.RenameColumn(
                name: "ApartmentNo",
                table: "Bakiyes",
                newName: "DaireNo");
        }
    }
}
