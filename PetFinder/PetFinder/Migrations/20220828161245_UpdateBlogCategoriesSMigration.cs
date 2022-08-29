using Microsoft.EntityFrameworkCore.Migrations;

namespace PetFinder.Migrations
{
    public partial class UpdateBlogCategoriesSMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogCategories_Categories_CategoryId",
                table: "BlogCategories");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "BlogCategories",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogCategories_Categories_CategoryId",
                table: "BlogCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogCategories_Categories_CategoryId",
                table: "BlogCategories");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "BlogCategories",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_BlogCategories_Categories_CategoryId",
                table: "BlogCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
