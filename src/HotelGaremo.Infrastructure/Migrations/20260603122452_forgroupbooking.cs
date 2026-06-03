using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelGaremo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class forgroupbooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GroupBookingNumber",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupBookingNumber",
                table: "Bookings");
        }
    }
}
