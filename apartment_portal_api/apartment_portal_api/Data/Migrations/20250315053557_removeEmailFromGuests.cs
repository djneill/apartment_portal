using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apartment_portal_api.Data.Migrations
{
    /// <inheritdoc />
    public partial class removeEmailFromGuests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "guests_email_key",
                table: "guests");

            migrationBuilder.DropColumn(
                name: "email",
                table: "guests");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "guests",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "guests_email_key",
                table: "guests",
                column: "email",
                unique: true);
        }
    }
}
