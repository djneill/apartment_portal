using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace apartment_portal_api.Data.Migrations
{
    /// <inheritdoc />
    public partial class leaseAgreements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "leaseAgreement",
                table: "unitUsers");

            migrationBuilder.DropColumn(
                name: "leaseExpiration",
                table: "unitUsers");

            migrationBuilder.CreateTable(
                name: "leaseStatuses",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("leaseStatuses_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "leaseAgreements",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    startDate = table.Column<DateOnly>(type: "date", nullable: false),
                    endDate = table.Column<DateOnly>(type: "date", nullable: false),
                    signedOn = table.Column<DateOnly>(type: "date", nullable: true),
                    link = table.Column<string>(type: "text", nullable: false),
                    unitUsersId = table.Column<int>(type: "integer", nullable: false),
                    leaseStatusId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("leaseAgreements_pkey", x => x.id);
                    table.ForeignKey(
                        name: "leaseAgreements_leaseStatusId_fkey",
                        column: x => x.leaseStatusId,
                        principalTable: "leaseStatuses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "leaseAgreements_unitUsersId_fkey",
                        column: x => x.unitUsersId,
                        principalTable: "unitUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_leaseAgreements_leaseStatusId",
                table: "leaseAgreements",
                column: "leaseStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_leaseAgreements_unitUsersId",
                table: "leaseAgreements",
                column: "unitUsersId");

            migrationBuilder.CreateIndex(
                name: "leaseStatuses_name_key",
                table: "leaseStatuses",
                column: "name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "leaseAgreements");

            migrationBuilder.DropTable(
                name: "leaseStatuses");

            migrationBuilder.AddColumn<string>(
                name: "leaseAgreement",
                table: "unitUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "leaseExpiration",
                table: "unitUsers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
