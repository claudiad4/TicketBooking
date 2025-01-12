using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketBooking.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddKoncertIdForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_MiejscaKoncertDetails_KoncertID",
                table: "MiejscaKoncertDetails",
                column: "KoncertID");

            migrationBuilder.AddForeignKey(
                name: "FK_MiejscaKoncertDetails_KoncertInfo_KoncertID",
                table: "MiejscaKoncertDetails",
                column: "KoncertID",
                principalTable: "KoncertInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MiejscaKoncertDetails_KoncertInfo_KoncertID",
                table: "MiejscaKoncertDetails");

            migrationBuilder.DropIndex(
                name: "IX_MiejscaKoncertDetails_KoncertID",
                table: "MiejscaKoncertDetails");
        }
    }
}
