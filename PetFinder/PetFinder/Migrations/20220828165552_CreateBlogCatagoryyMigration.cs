using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PetFinder.Migrations
{
    public partial class CreateBlogCatagoryyMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogCategories_Categories_CategoryId",
                table: "BlogCategories");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_BlogCategories_CategoryId",
                table: "BlogCategories");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "BlogCategories");

            migrationBuilder.AddColumn<int>(
                name: "CatagoryId",
                table: "BlogCategories",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Catagories",
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
                    table.PrimaryKey("PK_Catagories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogCategories_CatagoryId",
                table: "BlogCategories",
                column: "CatagoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogCategories_Catagories_CatagoryId",
                table: "BlogCategories",
                column: "CatagoryId",
                principalTable: "Catagories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogCategories_Catagories_CatagoryId",
                table: "BlogCategories");

            migrationBuilder.DropTable(
                name: "Catagories");

            migrationBuilder.DropIndex(
                name: "IX_BlogCategories_CatagoryId",
                table: "BlogCategories");

            migrationBuilder.DropColumn(
                name: "CatagoryId",
                table: "BlogCategories");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "BlogCategories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Categories",
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
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogCategories_CategoryId",
                table: "BlogCategories",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogCategories_Categories_CategoryId",
                table: "BlogCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
