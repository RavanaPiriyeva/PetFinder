using Microsoft.EntityFrameworkCore.Migrations;

namespace PetFinder.Migrations
{
    public partial class UpdateBlogCategoriesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogTypes_Blogs_BlogId",
                table: "BlogTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogTypes_Categories_CategoryId",
                table: "BlogTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogTypes",
                table: "BlogTypes");

            migrationBuilder.RenameTable(
                name: "BlogTypes",
                newName: "BlogCategories");

            migrationBuilder.RenameIndex(
                name: "IX_BlogTypes_CategoryId",
                table: "BlogCategories",
                newName: "IX_BlogCategories_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_BlogTypes_BlogId",
                table: "BlogCategories",
                newName: "IX_BlogCategories_BlogId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogCategories",
                table: "BlogCategories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogCategories_Blogs_BlogId",
                table: "BlogCategories",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogCategories_Categories_CategoryId",
                table: "BlogCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogCategories_Blogs_BlogId",
                table: "BlogCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogCategories_Categories_CategoryId",
                table: "BlogCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogCategories",
                table: "BlogCategories");

            migrationBuilder.RenameTable(
                name: "BlogCategories",
                newName: "BlogTypes");

            migrationBuilder.RenameIndex(
                name: "IX_BlogCategories_CategoryId",
                table: "BlogTypes",
                newName: "IX_BlogTypes_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_BlogCategories_BlogId",
                table: "BlogTypes",
                newName: "IX_BlogTypes_BlogId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogTypes",
                table: "BlogTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogTypes_Blogs_BlogId",
                table: "BlogTypes",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogTypes_Categories_CategoryId",
                table: "BlogTypes",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
