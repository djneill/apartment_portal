using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apartment_portal_api.Data.Migrations
{
    /// <inheritdoc />
    public partial class removeUniqueLockerNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "packages_lockerNumber_key",
                table: "packages");

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
            migrationBuilder.AlterColumn<string>(
                name: "accessCode",
                table: "guests",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "packages_lockerNumber_key",
                table: "packages",
                column: "lockerNumber",
                unique: true);
        }
    }
}
