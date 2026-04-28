using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcYummy.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTimeToTimeSpan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "Time",
                table: "Recipe",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Time",
                table: "Recipe",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "TEXT");
        }
    }
}
