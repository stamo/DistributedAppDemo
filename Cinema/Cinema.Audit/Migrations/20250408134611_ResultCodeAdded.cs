using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinema.Audit.Migrations
{
    /// <inheritdoc />
    public partial class ResultCodeAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "result_code",
                table: "audit_log",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "result_code",
                table: "audit_log");
        }
    }
}
