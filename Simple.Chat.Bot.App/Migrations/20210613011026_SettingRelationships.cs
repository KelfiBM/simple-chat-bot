using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Simple.Chat.Bot.App.Migrations
{
    public partial class SettingRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessage_AspNetUsers_UserId1",
                table: "ChatMessage");

            migrationBuilder.AlterColumn<string>(
                name: "UserId1",
                table: "ChatMessage",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DatePosted",
                table: "ChatMessage",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 6, 12, 21, 10, 26, 267, DateTimeKind.Local).AddTicks(4766),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Nickname",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Nickname",
                table: "AspNetUsers",
                column: "Nickname",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessage_AspNetUsers_UserId1",
                table: "ChatMessage",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessage_AspNetUsers_UserId1",
                table: "ChatMessage");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Nickname",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "UserId1",
                table: "ChatMessage",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DatePosted",
                table: "ChatMessage",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 6, 12, 21, 10, 26, 267, DateTimeKind.Local).AddTicks(4766));

            migrationBuilder.AlterColumn<string>(
                name: "Nickname",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessage_AspNetUsers_UserId1",
                table: "ChatMessage",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
