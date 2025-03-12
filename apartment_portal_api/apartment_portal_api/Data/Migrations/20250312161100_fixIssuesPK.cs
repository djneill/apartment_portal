using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace apartment_portal_api.Data.Migrations
{
    /// <inheritdoc />
    public partial class fixIssuesPK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "issues_pkey",
                table: "issues");

            //migrationBuilder.AddColumn<int>(
            //    name: "id",
            //    table: "issues",
            //    type: "integer",
            //    nullable: false,
            //    defaultValue: 0)
            //    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "issues_pkey",
                table: "issues",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_issues_userId",
                table: "issues",
                column: "userId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "issues_pkey",
                table: "issues");

            migrationBuilder.DropIndex(
                name: "IX_issues_userId",
                table: "issues");

            migrationBuilder.DropColumn(
                name: "id",
                table: "issues");

            migrationBuilder.AddPrimaryKey(
                name: "issues_pkey",
                table: "issues",
                columns: new[] { "userId", "issueTypeId" });
        }
    }
}
