using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tutorias.Migrations
{
    public partial class initial8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_AspNetUsers_Id",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_Tutor_AspNetUsers_Id",
                table: "Tutor");

            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "Tutor",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "Tutor",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Tutor",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "Tutor",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "Tutor",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "Tutor",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "Tutor",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "Tutor",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Tutor",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Tutor",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "Tutor",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "Tutor",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "Tutor",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Tutor",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "Student",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "Student",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Student",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "Student",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "Student",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "Student",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "Student",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "Student",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Student",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Student",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "Student",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "Student",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "Student",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Student",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StudentId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TutorId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_StudentId",
                table: "AspNetUsers",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TutorId",
                table: "AspNetUsers",
                column: "TutorId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Student_StudentId",
                table: "AspNetUsers",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Tutor_TutorId",
                table: "AspNetUsers",
                column: "TutorId",
                principalTable: "Tutor",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Student_StudentId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Tutor_TutorId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_StudentId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_TutorId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                table: "Tutor");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "Tutor");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Tutor");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "Tutor");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "Tutor");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "Tutor");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "Tutor");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "Tutor");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Tutor");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Tutor");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "Tutor");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "Tutor");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "Tutor");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Tutor");

            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TutorId",
                table: "AspNetUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_AspNetUsers_Id",
                table: "Student",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tutor_AspNetUsers_Id",
                table: "Tutor",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
