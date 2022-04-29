using Microsoft.EntityFrameworkCore.Migrations;

namespace SocialNet1.Database.SQlite.Migrations
{
    public partial class FixGroupUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserGroupStatuses_AspNetUsers_UserDTOId",
                table: "UserGroupStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGroupStatuses_Groups_GroupDTOId",
                table: "UserGroupStatuses");

            migrationBuilder.DropIndex(
                name: "IX_UserGroupStatuses_GroupDTOId",
                table: "UserGroupStatuses");

            migrationBuilder.DropColumn(
                name: "GroupDTOId",
                table: "UserGroupStatuses");

            migrationBuilder.AlterColumn<string>(
                name: "UserDTOId",
                table: "UserGroupStatuses",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "UserGroupStatuses",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserGroupStatuses_GroupId",
                table: "UserGroupStatuses",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroupStatuses_AspNetUsers_UserDTOId",
                table: "UserGroupStatuses",
                column: "UserDTOId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroupStatuses_Groups_GroupId",
                table: "UserGroupStatuses",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserGroupStatuses_AspNetUsers_UserDTOId",
                table: "UserGroupStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGroupStatuses_Groups_GroupId",
                table: "UserGroupStatuses");

            migrationBuilder.DropIndex(
                name: "IX_UserGroupStatuses_GroupId",
                table: "UserGroupStatuses");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "UserGroupStatuses");

            migrationBuilder.AlterColumn<string>(
                name: "UserDTOId",
                table: "UserGroupStatuses",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "GroupDTOId",
                table: "UserGroupStatuses",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserGroupStatuses_GroupDTOId",
                table: "UserGroupStatuses",
                column: "GroupDTOId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroupStatuses_AspNetUsers_UserDTOId",
                table: "UserGroupStatuses",
                column: "UserDTOId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroupStatuses_Groups_GroupDTOId",
                table: "UserGroupStatuses",
                column: "GroupDTOId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
