using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Cinema.HallManager.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cinemas",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false, comment: "Record identifier")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, comment: "Cinema name"),
                    city = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, comment: "Cinema location")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cinemas", x => x.id);
                },
                comment: "Cinema Theatres");

            migrationBuilder.CreateTable(
                name: "halls",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false, comment: "Record identifier")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, comment: "Hall name"),
                    seats = table.Column<int>(type: "integer", nullable: false, comment: "Number of seats in the hall"),
                    cinema_id = table.Column<int>(type: "integer", nullable: false, comment: "Identifier of the cinema")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_halls", x => x.id);
                    table.ForeignKey(
                        name: "fk_halls_cinemas_cinema_id",
                        column: x => x.cinema_id,
                        principalTable: "cinemas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Theatre halls");

            migrationBuilder.CreateIndex(
                name: "ix_halls_cinema_id",
                table: "halls",
                column: "cinema_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "halls");

            migrationBuilder.DropTable(
                name: "cinemas");
        }
    }
}
