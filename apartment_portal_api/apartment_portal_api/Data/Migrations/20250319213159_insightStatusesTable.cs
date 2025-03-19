using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace apartment_portal_api.Data.Migrations;

/// <inheritdoc />
public partial class insightStatusesTable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<int>(
            name: "insightStatusId",
            table: "insights",
            type: "integer",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.CreateTable(
            name: "insightStatuses",
            columns: table => new
            {
                id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                name = table.Column<string>(type: "character varying", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("insightStatuses_pkey", x => x.id);
            });

        migrationBuilder.CreateIndex(
            name: "insightStatuses_name_key",
            table: "insightStatuses",
            column: "name",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "insightStatuses");

        migrationBuilder.DropColumn(
            name: "insightStatusId",
            table: "insights");
    }
}
