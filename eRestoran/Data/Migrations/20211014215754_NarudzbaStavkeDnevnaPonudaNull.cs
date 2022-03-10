using Microsoft.EntityFrameworkCore.Migrations;

namespace eRestoran.Data.Migrations
{
    public partial class NarudzbaStavkeDnevnaPonudaNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NarudzbaStavka_DnevnaPonuda_DnevnaPonudaID",
                table: "NarudzbaStavka");

            migrationBuilder.AlterColumn<int>(
                name: "DnevnaPonudaID",
                table: "NarudzbaStavka",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_NarudzbaStavka_DnevnaPonuda_DnevnaPonudaID",
                table: "NarudzbaStavka",
                column: "DnevnaPonudaID",
                principalTable: "DnevnaPonuda",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NarudzbaStavka_DnevnaPonuda_DnevnaPonudaID",
                table: "NarudzbaStavka");

            migrationBuilder.AlterColumn<int>(
                name: "DnevnaPonudaID",
                table: "NarudzbaStavka",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_NarudzbaStavka_DnevnaPonuda_DnevnaPonudaID",
                table: "NarudzbaStavka",
                column: "DnevnaPonudaID",
                principalTable: "DnevnaPonuda",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
