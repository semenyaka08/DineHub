using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DineHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingaKiloCaloriesfieldtotheDishtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KiloCalories",
                table: "Dish",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KiloCalories",
                table: "Dish");
        }
    }
}
