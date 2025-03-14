using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apartment_portal_api.Data.Migrations
{
    /// <inheritdoc />
    public partial class removeUnitUserUserIdIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "unitUsers_userId_key",
                table: "unitUsers");

            migrationBuilder.CreateIndex(
                name: "IX_unitUsers_userId",
                table: "unitUsers",
                column: "userId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_unitUsers_userId",
                table: "unitUsers");

            migrationBuilder.CreateIndex(
                name: "unitUsers_userId_key",
                table: "unitUsers",
                column: "userId",
                unique: true);
        }
    }
}
