using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tutorias.Migrations
{
    public partial class initial11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tutor_AspNetUsers_appUserId",
                table: "Tutor");

            migrationBuilder.DropIndex(
                name: "IX_Tutor_appUserId",
                table: "Tutor");

            migrationBuilder.DropColumn(
                name: "appUserId",
                table: "Tutor");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "appUserId",
                table: "Tutor",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Tutor_appUserId",
                table: "Tutor",
                column: "appUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tutor_AspNetUsers_appUserId",
                table: "Tutor",
                column: "appUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
