using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelGaremo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserEntityChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PasswordRecoveryCodeExpiry",
                table: "Users",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordRecoveryCodeExpiry",
                table: "Users");
        }
    }
}
