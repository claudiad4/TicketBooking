using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketBooking.Web.Migrations
{
    /// <inheritdoc />
    public partial class Booking3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VIP",
                table: "Booking",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VIP",
                table: "Booking");
        }
    }
}
