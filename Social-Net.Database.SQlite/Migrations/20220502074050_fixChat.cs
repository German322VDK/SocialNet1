using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SocialNet1.Database.SQlite.Migrations
{
    public partial class fixChat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_Groups_GroupId",
                table: "Chats");

            migrationBuilder.DropIndex(
                name: "IX_Chats_GroupId",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "lol",
                table: "Chats");

            migrationBuilder.AddColumn<int>(
                name: "GroupChatDTOId",
                table: "MessageDTO",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GroupChats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GroupId = table.Column<int>(type: "INTEGER", nullable: true),
                    LastTimeMess = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupChats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupChats_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MessageDTO_GroupChatDTOId",
                table: "MessageDTO",
                column: "GroupChatDTOId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupChats_GroupId",
                table: "GroupChats",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageDTO_GroupChats_GroupChatDTOId",
                table: "MessageDTO",
                column: "GroupChatDTOId",
                principalTable: "GroupChats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageDTO_GroupChats_GroupChatDTOId",
                table: "MessageDTO");

            migrationBuilder.DropTable(
                name: "GroupChats");

            migrationBuilder.DropIndex(
                name: "IX_MessageDTO_GroupChatDTOId",
                table: "MessageDTO");

            migrationBuilder.DropColumn(
                name: "GroupChatDTOId",
                table: "MessageDTO");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Chats",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "Chats",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "lol",
                table: "Chats",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Chats_GroupId",
                table: "Chats",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_Groups_GroupId",
                table: "Chats",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
