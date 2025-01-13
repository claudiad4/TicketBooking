using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketBooking.Web.Migrations
{
    /// <inheritdoc />
    public partial class KupBilet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KupBilet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MiejscaDetailsId = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KupBilet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KupBilet_MiejscaKoncertDetails_MiejscaDetailsId",
                        column: x => x.MiejscaDetailsId,
                        principalTable: "MiejscaKoncertDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KupBilet_MiejscaDetailsId",
                table: "KupBilet",
                column: "MiejscaDetailsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KupBilet");
        }
    }
}
