using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DineHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MakingUserfieldrequired : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_AspNetUsers_UserId",
                table: "Restaurants");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Restaurants",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_AspNetUsers_UserId",
                table: "Restaurants",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_AspNetUsers_UserId",
                table: "Restaurants");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Restaurants",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_AspNetUsers_UserId",
                table: "Restaurants",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
