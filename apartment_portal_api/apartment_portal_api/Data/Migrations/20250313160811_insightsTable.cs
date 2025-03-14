using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace apartment_portal_api.Data.Migrations
{
    /// <inheritdoc />
    public partial class insightsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "insights",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: false),
                    summary = table.Column<string>(type: "text", nullable: false),
                    suggestion = table.Column<string>(type: "text", nullable: false),
                    createdOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "(now() AT TIME ZONE 'utc'::text)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("insights_pkey", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "insights");
        }
    }
}
