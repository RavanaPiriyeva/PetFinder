using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PetFinder.Migrations
{
    public partial class UpdateCategoryMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogTypes_Types_TypeId",
                table: "BlogTypes");

            migrationBuilder.DropTable(
                name: "Types");

            migrationBuilder.DropIndex(
                name: "IX_BlogTypes_TypeId",
                table: "BlogTypes");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "BlogTypes");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "BlogTypes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Categoryİd",
                table: "BlogTypes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogTypes_CategoryId",
                table: "BlogTypes",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogTypes_Categories_CategoryId",
                table: "BlogTypes",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogTypes_Categories_CategoryId",
                table: "BlogTypes");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_BlogTypes_CategoryId",
                table: "BlogTypes");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "BlogTypes");

            migrationBuilder.DropColumn(
                name: "Categoryİd",
                table: "BlogTypes");

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "BlogTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Types",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Types", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogTypes_TypeId",
                table: "BlogTypes",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogTypes_Types_TypeId",
                table: "BlogTypes",
                column: "TypeId",
                principalTable: "Types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
