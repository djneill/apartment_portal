using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apartment_portal_api.Data.Migrations;

/// <inheritdoc />
public partial class insightStatusesFK : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateIndex(
            name: "IX_insights_insightStatusId",
            table: "insights",
            column: "insightStatusId");

        migrationBuilder.AddForeignKey(
            name: "insights_insightStatusId_fkey",
            table: "insights",
            column: "insightStatusId",
            principalTable: "insightStatuses",
            principalColumn: "id",
            onDelete: ReferentialAction.Restrict);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "insights_insightStatusId_fkey",
            table: "insights");

        migrationBuilder.DropIndex(
            name: "IX_insights_insightStatusId",
            table: "insights");
    }
}
