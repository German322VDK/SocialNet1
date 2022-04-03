using Microsoft.EntityFrameworkCore.Migrations;

namespace SocialNet1.Database.SQlite.Migrations
{
    public partial class UpdateLikes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Like_CommentDTO_CommentDTOId",
                table: "Like");

            migrationBuilder.DropForeignKey(
                name: "FK_Like_GroupImageComments_GroupImageCommentsId",
                table: "Like");

            migrationBuilder.DropForeignKey(
                name: "FK_Like_GroupImages_GroupImagesId",
                table: "Like");

            migrationBuilder.DropForeignKey(
                name: "FK_Like_UserImageComments_UserImageCommentsId",
                table: "Like");

            migrationBuilder.DropForeignKey(
                name: "FK_Like_UserImages_UserImagesId",
                table: "Like");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Like",
                table: "Like");

            migrationBuilder.DropIndex(
                name: "IX_Like_CommentDTOId",
                table: "Like");

            migrationBuilder.DropIndex(
                name: "IX_Like_GroupImageCommentsId",
                table: "Like");

            migrationBuilder.DropIndex(
                name: "IX_Like_GroupImagesId",
                table: "Like");

            migrationBuilder.DropIndex(
                name: "IX_Like_UserImageCommentsId",
                table: "Like");

            migrationBuilder.DropColumn(
                name: "CommentDTOId",
                table: "Like");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Like");

            migrationBuilder.DropColumn(
                name: "GroupImageCommentsId",
                table: "Like");

            migrationBuilder.DropColumn(
                name: "GroupImagesId",
                table: "Like");

            migrationBuilder.DropColumn(
                name: "UserImageCommentsId",
                table: "Like");

            migrationBuilder.RenameTable(
                name: "Like",
                newName: "UserLike");

            migrationBuilder.RenameIndex(
                name: "IX_Like_UserImagesId",
                table: "UserLike",
                newName: "IX_UserLike_UserImagesId");

            migrationBuilder.AddColumn<bool>(
                name: "IsPutinUser",
                table: "UserLike",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserLike",
                table: "UserLike",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CommentLike",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsPutinLiked = table.Column<bool>(type: "INTEGER", nullable: false),
                    CommentDTOId = table.Column<int>(type: "INTEGER", nullable: true),
                    Likers = table.Column<string>(type: "TEXT", nullable: false),
                    Emoji = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentLike", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentLike_CommentDTO_CommentDTOId",
                        column: x => x.CommentDTOId,
                        principalTable: "CommentDTO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GroupCommentLike",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsPutinAdminGroupCom = table.Column<bool>(type: "INTEGER", nullable: false),
                    GroupImageCommentsId = table.Column<int>(type: "INTEGER", nullable: true),
                    Likers = table.Column<string>(type: "TEXT", nullable: false),
                    Emoji = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupCommentLike", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupCommentLike_GroupImageComments_GroupImageCommentsId",
                        column: x => x.GroupImageCommentsId,
                        principalTable: "GroupImageComments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GroupLike",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsPutinAdminGroup = table.Column<bool>(type: "INTEGER", nullable: false),
                    GroupImagesId = table.Column<int>(type: "INTEGER", nullable: true),
                    Likers = table.Column<string>(type: "TEXT", nullable: false),
                    Emoji = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupLike", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupLike_GroupImages_GroupImagesId",
                        column: x => x.GroupImagesId,
                        principalTable: "GroupImages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserCommentLike",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserImageCommentsId = table.Column<int>(type: "INTEGER", nullable: true),
                    Likers = table.Column<string>(type: "TEXT", nullable: false),
                    Emoji = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCommentLike", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserCommentLike_UserImageComments_UserImageCommentsId",
                        column: x => x.UserImageCommentsId,
                        principalTable: "UserImageComments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommentLike_CommentDTOId",
                table: "CommentLike",
                column: "CommentDTOId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupCommentLike_GroupImageCommentsId",
                table: "GroupCommentLike",
                column: "GroupImageCommentsId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupLike_GroupImagesId",
                table: "GroupLike",
                column: "GroupImagesId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCommentLike_UserImageCommentsId",
                table: "UserCommentLike",
                column: "UserImageCommentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLike_UserImages_UserImagesId",
                table: "UserLike",
                column: "UserImagesId",
                principalTable: "UserImages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLike_UserImages_UserImagesId",
                table: "UserLike");

            migrationBuilder.DropTable(
                name: "CommentLike");

            migrationBuilder.DropTable(
                name: "GroupCommentLike");

            migrationBuilder.DropTable(
                name: "GroupLike");

            migrationBuilder.DropTable(
                name: "UserCommentLike");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserLike",
                table: "UserLike");

            migrationBuilder.DropColumn(
                name: "IsPutinUser",
                table: "UserLike");

            migrationBuilder.RenameTable(
                name: "UserLike",
                newName: "Like");

            migrationBuilder.RenameIndex(
                name: "IX_UserLike_UserImagesId",
                table: "Like",
                newName: "IX_Like_UserImagesId");

            migrationBuilder.AddColumn<int>(
                name: "CommentDTOId",
                table: "Like",
                type: "INTEGER",
                nullable: true);

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
                name: "GroupImagesId",
                table: "Like",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserImageCommentsId",
                table: "Like",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Like",
                table: "Like",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Like_CommentDTOId",
                table: "Like",
                column: "CommentDTOId");

            migrationBuilder.CreateIndex(
                name: "IX_Like_GroupImageCommentsId",
                table: "Like",
                column: "GroupImageCommentsId");

            migrationBuilder.CreateIndex(
                name: "IX_Like_GroupImagesId",
                table: "Like",
                column: "GroupImagesId");

            migrationBuilder.CreateIndex(
                name: "IX_Like_UserImageCommentsId",
                table: "Like",
                column: "UserImageCommentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Like_CommentDTO_CommentDTOId",
                table: "Like",
                column: "CommentDTOId",
                principalTable: "CommentDTO",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Like_GroupImageComments_GroupImageCommentsId",
                table: "Like",
                column: "GroupImageCommentsId",
                principalTable: "GroupImageComments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Like_GroupImages_GroupImagesId",
                table: "Like",
                column: "GroupImagesId",
                principalTable: "GroupImages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Like_UserImageComments_UserImageCommentsId",
                table: "Like",
                column: "UserImageCommentsId",
                principalTable: "UserImageComments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Like_UserImages_UserImagesId",
                table: "Like",
                column: "UserImagesId",
                principalTable: "UserImages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
