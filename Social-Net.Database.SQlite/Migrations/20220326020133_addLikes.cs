using Microsoft.EntityFrameworkCore.Migrations;

namespace SocialNet1.Database.SQlite.Migrations
{
    public partial class addLikes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LikeCount",
                table: "UserImages");

            migrationBuilder.AddColumn<int>(
                name: "UserImagesId",
                table: "Like",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Like_UserImagesId",
                table: "Like",
                column: "UserImagesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Like_UserImages_UserImagesId",
                table: "Like",
                column: "UserImagesId",
                principalTable: "UserImages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Like_UserImages_UserImagesId",
                table: "Like");

            migrationBuilder.DropIndex(
                name: "IX_Like_UserImagesId",
                table: "Like");

            migrationBuilder.DropColumn(
                name: "UserImagesId",
                table: "Like");

            migrationBuilder.AddColumn<int>(
                name: "LikeCount",
                table: "UserImages",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
