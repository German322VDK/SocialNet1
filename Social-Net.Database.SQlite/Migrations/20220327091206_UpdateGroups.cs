using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SocialNet1.Database.SQlite.Migrations
{
    public partial class UpdateGroups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroupImagesId",
                table: "Like",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RepostCount",
                table: "GroupImages",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "GroupImageComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GroupImagesId = table.Column<int>(type: "INTEGER", nullable: true),
                    UserName = table.Column<string>(type: "TEXT", nullable: false),
                    LikeCount = table.Column<int>(type: "INTEGER", nullable: false),
                    Text = table.Column<string>(type: "TEXT", nullable: false),
                    DateTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupImageComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupImageComments_GroupImages_GroupImagesId",
                        column: x => x.GroupImagesId,
                        principalTable: "GroupImages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Like_GroupImagesId",
                table: "Like",
                column: "GroupImagesId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupImageComments_GroupImagesId",
                table: "GroupImageComments",
                column: "GroupImagesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Like_GroupImages_GroupImagesId",
                table: "Like",
                column: "GroupImagesId",
                principalTable: "GroupImages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Like_GroupImages_GroupImagesId",
                table: "Like");

            migrationBuilder.DropTable(
                name: "GroupImageComments");

            migrationBuilder.DropIndex(
                name: "IX_Like_GroupImagesId",
                table: "Like");

            migrationBuilder.DropColumn(
                name: "GroupImagesId",
                table: "Like");

            migrationBuilder.DropColumn(
                name: "RepostCount",
                table: "GroupImages");
        }
    }
}
