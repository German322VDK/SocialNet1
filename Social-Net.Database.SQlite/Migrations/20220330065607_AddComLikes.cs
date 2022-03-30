using Microsoft.EntityFrameworkCore.Migrations;

namespace SocialNet1.Database.SQlite.Migrations
{
    public partial class AddComLikes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LikeCount",
                table: "UserImageComments");

            migrationBuilder.DropColumn(
                name: "LikeCount",
                table: "GroupImageComments");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Like",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "GroupImageCommentsId",
                table: "Like",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserImageCommentsId",
                table: "Like",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Like_GroupImageCommentsId",
                table: "Like",
                column: "GroupImageCommentsId");

            migrationBuilder.CreateIndex(
                name: "IX_Like_UserImageCommentsId",
                table: "Like",
                column: "UserImageCommentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Like_GroupImageComments_GroupImageCommentsId",
                table: "Like",
                column: "GroupImageCommentsId",
                principalTable: "GroupImageComments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Like_UserImageComments_UserImageCommentsId",
                table: "Like",
                column: "UserImageCommentsId",
                principalTable: "UserImageComments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Like_GroupImageComments_GroupImageCommentsId",
                table: "Like");

            migrationBuilder.DropForeignKey(
                name: "FK_Like_UserImageComments_UserImageCommentsId",
                table: "Like");

            migrationBuilder.DropIndex(
                name: "IX_Like_GroupImageCommentsId",
                table: "Like");

            migrationBuilder.DropIndex(
                name: "IX_Like_UserImageCommentsId",
                table: "Like");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Like");

            migrationBuilder.DropColumn(
                name: "GroupImageCommentsId",
                table: "Like");

            migrationBuilder.DropColumn(
                name: "UserImageCommentsId",
                table: "Like");

            migrationBuilder.AddColumn<int>(
                name: "LikeCount",
                table: "UserImageComments",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LikeCount",
                table: "GroupImageComments",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
