using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apartment_portal_api.Data.Migrations
{
    /// <inheritdoc />
    public partial class unitUserLeaseExpiration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "leaseExpiration",
                table: "unitUsers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "accessCode",
                table: "guests",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "leaseExpiration",
                table: "unitUsers");

            migrationBuilder.AlterColumn<string>(
                name: "accessCode",
                table: "guests",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");
        }
    }
}
