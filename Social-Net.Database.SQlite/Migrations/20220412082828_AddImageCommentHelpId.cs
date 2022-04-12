using Microsoft.EntityFrameworkCore.Migrations;

namespace SocialNet1.Database.SQlite.Migrations
{
    public partial class AddImageCommentHelpId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HelpId",
                table: "UserLike",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HelpId",
                table: "UserCommentLike",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HelpId",
                table: "GroupLike",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HelpId",
                table: "GroupCommentLike",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HelpId",
                table: "CommentLike",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HelpId",
                table: "UserLike");

            migrationBuilder.DropColumn(
                name: "HelpId",
                table: "UserCommentLike");

            migrationBuilder.DropColumn(
                name: "HelpId",
                table: "GroupLike");

            migrationBuilder.DropColumn(
                name: "HelpId",
                table: "GroupCommentLike");

            migrationBuilder.DropColumn(
                name: "HelpId",
                table: "CommentLike");
        }
    }
}
