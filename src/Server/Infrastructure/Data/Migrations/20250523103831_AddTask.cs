using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "task",
                columns: table => new
                {
                    task_id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false, defaultValue: ""),
                    description = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: false, defaultValue: ""),
                    status = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    progress = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    start_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    due_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    priority = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    assigned_to = table.Column<Guid>(type: "uuid", nullable: true),
                    assigned_by = table.Column<Guid>(type: "uuid", nullable: true),
                    note = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: false, defaultValue: ""),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    created_by = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false, defaultValue: ""),
                    last_modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    last_modified_by = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false, defaultValue: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_task", x => x.task_id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_task_assignedby",
                table: "task",
                column: "assigned_by");

            migrationBuilder.CreateIndex(
                name: "ix_task_assignedto",
                table: "task",
                column: "assigned_to");

            migrationBuilder.CreateIndex(
                name: "ix_task_createdat",
                table: "task",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "ix_task_duedate",
                table: "task",
                column: "due_date");

            migrationBuilder.CreateIndex(
                name: "ix_task_priority",
                table: "task",
                column: "priority");

            migrationBuilder.CreateIndex(
                name: "ix_task_status",
                table: "task",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "ix_task_status_duedate",
                table: "task",
                columns: new[] { "status", "due_date" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "task");

            migrationBuilder.DropIndex(
				name: "ix_task_assignedby",
				table: "task");

            migrationBuilder.DropIndex(
				name: "ix_task_assignedto",
				table: "task");

            migrationBuilder.DropIndex(
				name: "ix_task_createdat",
				table: "task");

            migrationBuilder.DropIndex(
				name: "ix_task_duedate",
				table: "task");

            migrationBuilder.DropIndex(
				name: "ix_task_priority",
				table: "task");

            migrationBuilder.DropIndex(
				name: "ix_task_status",
				table: "task");

            migrationBuilder.DropIndex(
				name: "ix_task_status_duedate",
				table: "task");
		}
    }
}
