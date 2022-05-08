using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SocialNet1.Database.SQlite.Migrations
{
    public partial class ClashInitial1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClashDTOId",
                table: "MessageDTO",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Clashs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Group1Id = table.Column<int>(type: "INTEGER", nullable: true),
                    Group2Id = table.Column<int>(type: "INTEGER", nullable: true),
                    LastTimeMess = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clashs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clashs_Groups_Group1Id",
                        column: x => x.Group1Id,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Clashs_Groups_Group2Id",
                        column: x => x.Group2Id,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClashLike",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClashDTOId = table.Column<int>(type: "INTEGER", nullable: true),
                    ClashDTOId1 = table.Column<int>(type: "INTEGER", nullable: true),
                    Likers = table.Column<string>(type: "TEXT", nullable: false),
                    Emoji = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClashLike", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClashLike_Clashs_ClashDTOId",
                        column: x => x.ClashDTOId,
                        principalTable: "Clashs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClashLike_Clashs_ClashDTOId1",
                        column: x => x.ClashDTOId1,
                        principalTable: "Clashs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MessageDTO_ClashDTOId",
                table: "MessageDTO",
                column: "ClashDTOId");

            migrationBuilder.CreateIndex(
                name: "IX_ClashLike_ClashDTOId",
                table: "ClashLike",
                column: "ClashDTOId");

            migrationBuilder.CreateIndex(
                name: "IX_ClashLike_ClashDTOId1",
                table: "ClashLike",
                column: "ClashDTOId1");

            migrationBuilder.CreateIndex(
                name: "IX_Clashs_Group1Id",
                table: "Clashs",
                column: "Group1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Clashs_Group2Id",
                table: "Clashs",
                column: "Group2Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageDTO_Clashs_ClashDTOId",
                table: "MessageDTO",
                column: "ClashDTOId",
                principalTable: "Clashs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageDTO_Clashs_ClashDTOId",
                table: "MessageDTO");

            migrationBuilder.DropTable(
                name: "ClashLike");

            migrationBuilder.DropTable(
                name: "Clashs");

            migrationBuilder.DropIndex(
                name: "IX_MessageDTO_ClashDTOId",
                table: "MessageDTO");

            migrationBuilder.DropColumn(
                name: "ClashDTOId",
                table: "MessageDTO");
        }
    }
}
