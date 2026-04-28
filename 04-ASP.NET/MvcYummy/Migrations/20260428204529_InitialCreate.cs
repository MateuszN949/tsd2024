using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcYummy.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Recipe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Time = table.Column<int>(type: "INTEGER", nullable: false),
                    Difficulty = table.Column<string>(type: "TEXT", nullable: true),
                    Likes = table.Column<int>(type: "INTEGER", nullable: false),
                    Ingredients = table.Column<string>(type: "TEXT", nullable: true),
                    Process = table.Column<string>(type: "TEXT", nullable: true),
                    Tips = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipe", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Recipe");
        }
    }
}
