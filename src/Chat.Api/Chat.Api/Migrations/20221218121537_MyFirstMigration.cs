using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chat.Api.Migrations
{
    public partial class MyFirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Users_UserId",
                table: "Rooms");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomUser_Rooms_RoomId",
                table: "RoomUser");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomUser_Users_UserId",
                table: "RoomUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomUser",
                table: "RoomUser");

            migrationBuilder.DropIndex(
                name: "IX_RoomUser_RoomId",
                table: "RoomUser");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_UserId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "RoomUser");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "RoomUser");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "RoomUser");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Rooms");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "RoomUser",
                newName: "UsersId");

            migrationBuilder.RenameColumn(
                name: "RoomId",
                table: "RoomUser",
                newName: "RoomsId");

            migrationBuilder.RenameIndex(
                name: "IX_RoomUser_UserId",
                table: "RoomUser",
                newName: "IX_RoomUser_UsersId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomUser",
                table: "RoomUser",
                columns: new[] { "RoomsId", "UsersId" });

            migrationBuilder.CreateTable(
                name: "RoomsUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserRoomId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomsUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoomsUsers_Rooms_UserRoomId",
                        column: x => x.UserRoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoomsUsers_UserRoomId",
                table: "RoomsUsers",
                column: "UserRoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomUser_Rooms_RoomsId",
                table: "RoomUser",
                column: "RoomsId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomUser_Users_UsersId",
                table: "RoomUser",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomUser_Rooms_RoomsId",
                table: "RoomUser");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomUser_Users_UsersId",
                table: "RoomUser");

            migrationBuilder.DropTable(
                name: "RoomsUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomUser",
                table: "RoomUser");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "RoomUser",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "RoomsId",
                table: "RoomUser",
                newName: "RoomId");

            migrationBuilder.RenameIndex(
                name: "IX_RoomUser_UsersId",
                table: "RoomUser",
                newName: "IX_RoomUser_UserId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "RoomUser",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "RoomUser",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "RoomUser",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Rooms",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomUser",
                table: "RoomUser",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_RoomUser_RoomId",
                table: "RoomUser",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_UserId",
                table: "Rooms",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Users_UserId",
                table: "Rooms",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomUser_Rooms_RoomId",
                table: "RoomUser",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomUser_Users_UserId",
                table: "RoomUser",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
