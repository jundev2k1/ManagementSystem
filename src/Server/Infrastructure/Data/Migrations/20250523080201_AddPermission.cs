using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "permission",
                columns: table => new
                {
                    permission_id = table.Column<Guid>(type: "uuid", nullable: false),
                    permission_name = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false, defaultValue: ""),
                    display_name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false, defaultValue: ""),
                    description = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: false, defaultValue: ""),
                    category = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false, defaultValue: ""),
                    action = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false, defaultValue: ""),
                    valid_flg = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    created_by = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false, defaultValue: ""),
                    last_modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    last_modified_by = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false, defaultValue: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_permission", x => x.permission_id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_permission_action",
                table: "permission",
                column: "action");

            migrationBuilder.CreateIndex(
                name: "ix_permission_category",
                table: "permission",
                column: "category");

            migrationBuilder.CreateIndex(
                name: "ix_permission_permission_name",
                table: "permission",
                column: "permission_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_permission_validflg",
                table: "permission",
                column: "valid_flg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "permission");

			migrationBuilder.DropIndex(
				name: "ix_permission_action",
                table: "permission");

			migrationBuilder.DropIndex(
				name: "ix_permission_category",
                table: "permission");

			migrationBuilder.DropIndex(
				name: "ix_permission_permission_name",
                table: "permission");

			migrationBuilder.DropIndex(
				name: "ix_permission_validflg",
                table: "permission");
		}
    }
}
