using Microsoft.EntityFrameworkCore.Migrations;

namespace eRestoran.Data.Migrations
{
    public partial class OnlineNarudzbaDodatak : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IzdavateljRačunaID",
                table: "OnlineNarudzba",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RestoranID",
                table: "OnlineNarudzba",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OnlineNarudzba_IzdavateljRačunaID",
                table: "OnlineNarudzba",
                column: "IzdavateljRačunaID");

            migrationBuilder.CreateIndex(
                name: "IX_OnlineNarudzba_RestoranID",
                table: "OnlineNarudzba",
                column: "RestoranID");

            migrationBuilder.AddForeignKey(
                name: "FK_OnlineNarudzba_AspNetUsers_IzdavateljRačunaID",
                table: "OnlineNarudzba",
                column: "IzdavateljRačunaID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OnlineNarudzba_Restoran_RestoranID",
                table: "OnlineNarudzba",
                column: "RestoranID",
                principalTable: "Restoran",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OnlineNarudzba_AspNetUsers_IzdavateljRačunaID",
                table: "OnlineNarudzba");

            migrationBuilder.DropForeignKey(
                name: "FK_OnlineNarudzba_Restoran_RestoranID",
                table: "OnlineNarudzba");

            migrationBuilder.DropIndex(
                name: "IX_OnlineNarudzba_IzdavateljRačunaID",
                table: "OnlineNarudzba");

            migrationBuilder.DropIndex(
                name: "IX_OnlineNarudzba_RestoranID",
                table: "OnlineNarudzba");

            migrationBuilder.DropColumn(
                name: "IzdavateljRačunaID",
                table: "OnlineNarudzba");

            migrationBuilder.DropColumn(
                name: "RestoranID",
                table: "OnlineNarudzba");
        }
    }
}
