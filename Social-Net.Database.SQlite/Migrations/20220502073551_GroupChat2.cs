using Microsoft.EntityFrameworkCore.Migrations;

namespace SocialNet1.Database.SQlite.Migrations
{
    public partial class GroupChat2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "lol",
                table: "Chats",
                type: "INTEGER",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "lol",
                table: "Chats");
        }
    }
}
