using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace apartment_portal_api.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:auth.aal_level", "aal1,aal2,aal3")
                .Annotation("Npgsql:Enum:auth.code_challenge_method", "s256,plain")
                .Annotation("Npgsql:Enum:auth.factor_status", "unverified,verified")
                .Annotation("Npgsql:Enum:auth.factor_type", "totp,webauthn,phone")
                .Annotation("Npgsql:Enum:auth.one_time_token_type", "confirmation_token,reauthentication_token,recovery_token,email_change_token_new,email_change_token_current,phone_change_token")
                .Annotation("Npgsql:Enum:pgsodium.key_status", "default,valid,invalid,expired")
                .Annotation("Npgsql:Enum:pgsodium.key_type", "aead-ietf,aead-det,hmacsha512,hmacsha256,auth,shorthash,generichash,kdf,secretbox,secretstream,stream_xchacha20")
                .Annotation("Npgsql:Enum:realtime.action", "INSERT,UPDATE,DELETE,TRUNCATE,ERROR")
                .Annotation("Npgsql:Enum:realtime.equality_op", "eq,neq,lt,lte,gt,gte,in")
                .Annotation("Npgsql:PostgresExtension:extensions.pg_stat_statements", ",,")
                .Annotation("Npgsql:PostgresExtension:extensions.pgcrypto", ",,")
                .Annotation("Npgsql:PostgresExtension:extensions.pgjwt", ",,")
                .Annotation("Npgsql:PostgresExtension:extensions.uuid-ossp", ",,")
                .Annotation("Npgsql:PostgresExtension:graphql.pg_graphql", ",,")
                .Annotation("Npgsql:PostgresExtension:pgsodium.pgsodium", ",,")
                .Annotation("Npgsql:PostgresExtension:vault.supabase_vault", ",,");

            migrationBuilder.CreateTable(
                name: "issueTypes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("issueTypes_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "statuses",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("statuses_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "units",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    number = table.Column<int>(type: "integer", nullable: false),
                    price = table.Column<int>(type: "integer", nullable: false),
                    statusId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("units_pkey", x => x.id);
                    table.ForeignKey(
                        name: "units_statusId_fkey",
                        column: x => x.statusId,
                        principalTable: "statuses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    firstName = table.Column<string>(type: "text", nullable: false),
                    lastName = table.Column<string>(type: "text", nullable: false),
                    dateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    statusId = table.Column<int>(type: "integer", nullable: false),
                    createdOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "(now() AT TIME ZONE 'utc'::text)"),
                    createdBy = table.Column<int>(type: "integer", nullable: false),
                    modifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "(now() AT TIME ZONE 'utc'::text)"),
                    modifiedBy = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("users_pkey", x => x.id);
                    table.ForeignKey(
                        name: "users_createdBy_fkey",
                        column: x => x.createdBy,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "users_modifiedBy_fkey",
                        column: x => x.modifiedBy,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "users_statusId_fkey",
                        column: x => x.statusId,
                        principalTable: "statuses",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "packages",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    lockerNumber = table.Column<int>(type: "integer", nullable: false),
                    code = table.Column<int>(type: "integer", nullable: false),
                    unitId = table.Column<int>(type: "integer", nullable: false),
                    statusId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("packages_pkey", x => x.id);
                    table.ForeignKey(
                        name: "packages_statusId_fkey",
                        column: x => x.statusId,
                        principalTable: "statuses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "packages_unitId_fkey",
                        column: x => x.unitId,
                        principalTable: "units",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "guests",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    userId = table.Column<int>(type: "integer", nullable: false),
                    firstName = table.Column<string>(type: "text", nullable: false),
                    lastName = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    phoneNumber = table.Column<string>(type: "text", nullable: false),
                    accessCode = table.Column<int>(type: "integer", nullable: false),
                    expiration = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    createdOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "(now() AT TIME ZONE 'utc'::text)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("guests_pkey", x => x.id);
                    table.ForeignKey(
                        name: "guests_userId_fkey",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "issues",
                columns: table => new
                {
                    userId = table.Column<int>(type: "integer", nullable: false),
                    issueTypeId = table.Column<int>(type: "integer", nullable: false),
                    createdOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "(now() AT TIME ZONE 'utc'::text)"),
                    description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("issues_pkey", x => new { x.userId, x.issueTypeId });
                    table.ForeignKey(
                        name: "issues_issueTypeId_fkey",
                        column: x => x.issueTypeId,
                        principalTable: "issueTypes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "issues_userId_fkey",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "unitUsers",
                columns: table => new
                {
                    userId = table.Column<int>(type: "integer", nullable: false),
                    unitId = table.Column<int>(type: "integer", nullable: false),
                    isPrimary = table.Column<bool>(type: "boolean", nullable: false),
                    leaseAgreement = table.Column<string>(type: "text", nullable: false),
                    createdOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "(now() AT TIME ZONE 'utc'::text)"),
                    createdBy = table.Column<int>(type: "integer", nullable: false),
                    modifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "(now() AT TIME ZONE 'utc'::text)"),
                    modifiedBy = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("unitUsers_pkey", x => new { x.userId, x.unitId });
                    table.ForeignKey(
                        name: "unitUsers_createdBy_fkey",
                        column: x => x.createdBy,
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "unitUsers_modifiedBy_fkey",
                        column: x => x.modifiedBy,
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "unitUsers_unitId_fkey",
                        column: x => x.unitId,
                        principalTable: "units",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "unitUsers_userId_fkey",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "parkingPermits",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    guestId = table.Column<int>(type: "integer", nullable: false),
                    vehicleMake = table.Column<string>(type: "text", nullable: false),
                    vehicleModel = table.Column<string>(type: "text", nullable: false),
                    licensePlate = table.Column<string>(type: "text", nullable: false),
                    licensePlateState = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("parkingPermits_pkey", x => x.id);
                    table.ForeignKey(
                        name: "parkingPermits_guestId_fkey",
                        column: x => x.guestId,
                        principalTable: "guests",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "guests_email_key",
                table: "guests",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "guests_phoneNumber_key",
                table: "guests",
                column: "phoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_guests_userId",
                table: "guests",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_issues_issueTypeId",
                table: "issues",
                column: "issueTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_packages_statusId",
                table: "packages",
                column: "statusId");

            migrationBuilder.CreateIndex(
                name: "IX_packages_unitId",
                table: "packages",
                column: "unitId");

            migrationBuilder.CreateIndex(
                name: "packages_lockerNumber_key",
                table: "packages",
                column: "lockerNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_parkingPermits_guestId",
                table: "parkingPermits",
                column: "guestId");

            migrationBuilder.CreateIndex(
                name: "statuses_name_key",
                table: "statuses",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_units_statusId",
                table: "units",
                column: "statusId");

            migrationBuilder.CreateIndex(
                name: "units_number_key",
                table: "units",
                column: "number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_unitUsers_createdBy",
                table: "unitUsers",
                column: "createdBy");

            migrationBuilder.CreateIndex(
                name: "IX_unitUsers_modifiedBy",
                table: "unitUsers",
                column: "modifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_unitUsers_unitId",
                table: "unitUsers",
                column: "unitId");

            migrationBuilder.CreateIndex(
                name: "IX_users_createdBy",
                table: "users",
                column: "createdBy");

            migrationBuilder.CreateIndex(
                name: "IX_users_modifiedBy",
                table: "users",
                column: "modifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_users_statusId",
                table: "users",
                column: "statusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "issues");

            migrationBuilder.DropTable(
                name: "packages");

            migrationBuilder.DropTable(
                name: "parkingPermits");

            migrationBuilder.DropTable(
                name: "unitUsers");

            migrationBuilder.DropTable(
                name: "issueTypes");

            migrationBuilder.DropTable(
                name: "guests");

            migrationBuilder.DropTable(
                name: "units");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "statuses");
        }
    }
}
