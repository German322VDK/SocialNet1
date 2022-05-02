using Microsoft.EntityFrameworkCore.Migrations;

namespace SocialNet1.Database.SQlite.Migrations
{
    public partial class GroupChat1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
