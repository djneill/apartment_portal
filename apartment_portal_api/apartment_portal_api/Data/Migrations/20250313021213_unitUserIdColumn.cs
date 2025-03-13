using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace apartment_portal_api.Data.Migrations
{
    /// <inheritdoc />
    public partial class unitUserIdColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "unitUsers_pkey",
                table: "unitUsers");

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "unitUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "unitUsers_pkey",
                table: "unitUsers",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "unitUsers_userId_key",
                table: "unitUsers",
                column: "userId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "unitUsers_pkey",
                table: "unitUsers");

            migrationBuilder.DropIndex(
                name: "unitUsers_userId_key",
                table: "unitUsers");

            migrationBuilder.DropColumn(
                name: "id",
                table: "unitUsers");

            migrationBuilder.AddPrimaryKey(
                name: "unitUsers_pkey",
                table: "unitUsers",
                columns: new[] { "userId", "unitId" });
        }
    }
}
