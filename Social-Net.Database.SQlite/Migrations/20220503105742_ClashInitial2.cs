using Microsoft.EntityFrameworkCore.Migrations;

namespace SocialNet1.Database.SQlite.Migrations
{
    public partial class ClashInitial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClashLike_Clashs_ClashDTOId",
                table: "ClashLike");

            migrationBuilder.DropForeignKey(
                name: "FK_ClashLike_Clashs_ClashDTOId1",
                table: "ClashLike");

            migrationBuilder.DropForeignKey(
                name: "FK_Clashs_Groups_Group1Id",
                table: "Clashs");

            migrationBuilder.DropForeignKey(
                name: "FK_Clashs_Groups_Group2Id",
                table: "Clashs");

            migrationBuilder.DropIndex(
                name: "IX_ClashLike_ClashDTOId",
                table: "ClashLike");

            migrationBuilder.DropColumn(
                name: "ClashDTOId",
                table: "ClashLike");

            migrationBuilder.RenameColumn(
                name: "Group2Id",
                table: "Clashs",
                newName: "Side2Id");

            migrationBuilder.RenameColumn(
                name: "Group1Id",
                table: "Clashs",
                newName: "Side1Id");

            migrationBuilder.RenameIndex(
                name: "IX_Clashs_Group2Id",
                table: "Clashs",
                newName: "IX_Clashs_Side2Id");

            migrationBuilder.RenameIndex(
                name: "IX_Clashs_Group1Id",
                table: "Clashs",
                newName: "IX_Clashs_Side1Id");

            migrationBuilder.RenameColumn(
                name: "ClashDTOId1",
                table: "ClashLike",
                newName: "SideId");

            migrationBuilder.RenameIndex(
                name: "IX_ClashLike_ClashDTOId1",
                table: "ClashLike",
                newName: "IX_ClashLike_SideId");

            migrationBuilder.CreateTable(
                name: "Side",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GroupId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Side", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Side_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Side_GroupId",
                table: "Side",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClashLike_Side_SideId",
                table: "ClashLike",
                column: "SideId",
                principalTable: "Side",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Clashs_Side_Side1Id",
                table: "Clashs",
                column: "Side1Id",
                principalTable: "Side",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Clashs_Side_Side2Id",
                table: "Clashs",
                column: "Side2Id",
                principalTable: "Side",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClashLike_Side_SideId",
                table: "ClashLike");

            migrationBuilder.DropForeignKey(
                name: "FK_Clashs_Side_Side1Id",
                table: "Clashs");

            migrationBuilder.DropForeignKey(
                name: "FK_Clashs_Side_Side2Id",
                table: "Clashs");

            migrationBuilder.DropTable(
                name: "Side");

            migrationBuilder.RenameColumn(
                name: "Side2Id",
                table: "Clashs",
                newName: "Group2Id");

            migrationBuilder.RenameColumn(
                name: "Side1Id",
                table: "Clashs",
                newName: "Group1Id");

            migrationBuilder.RenameIndex(
                name: "IX_Clashs_Side2Id",
                table: "Clashs",
                newName: "IX_Clashs_Group2Id");

            migrationBuilder.RenameIndex(
                name: "IX_Clashs_Side1Id",
                table: "Clashs",
                newName: "IX_Clashs_Group1Id");

            migrationBuilder.RenameColumn(
                name: "SideId",
                table: "ClashLike",
                newName: "ClashDTOId1");

            migrationBuilder.RenameIndex(
                name: "IX_ClashLike_SideId",
                table: "ClashLike",
                newName: "IX_ClashLike_ClashDTOId1");

            migrationBuilder.AddColumn<int>(
                name: "ClashDTOId",
                table: "ClashLike",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClashLike_ClashDTOId",
                table: "ClashLike",
                column: "ClashDTOId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClashLike_Clashs_ClashDTOId",
                table: "ClashLike",
                column: "ClashDTOId",
                principalTable: "Clashs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ClashLike_Clashs_ClashDTOId1",
                table: "ClashLike",
                column: "ClashDTOId1",
                principalTable: "Clashs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Clashs_Groups_Group1Id",
                table: "Clashs",
                column: "Group1Id",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Clashs_Groups_Group2Id",
                table: "Clashs",
                column: "Group2Id",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
