using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketBooking.Web.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KoncertInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazwaKoncertu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Wykonawca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaksymalnaIloscSiedzen = table.Column<int>(type: "int", nullable: false),
                    KoncertImage = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KoncertInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MiejscaKoncertDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumerMiejsca = table.Column<int>(type: "int", nullable: false),
                    KoncertID = table.Column<int>(type: "int", nullable: false),
                    StatusMiejsca = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MiejscaKoncertDetails", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KoncertInfo");

            migrationBuilder.DropTable(
                name: "MiejscaKoncertDetails");
        }
    }
}
