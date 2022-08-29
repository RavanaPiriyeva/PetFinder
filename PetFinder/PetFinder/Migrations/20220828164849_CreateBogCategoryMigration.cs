using Microsoft.EntityFrameworkCore.Migrations;

namespace PetFinder.Migrations
{
    public partial class CreateBogCategoryMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogCategories_AnimalKinds_AnimalKindId",
                table: "BlogCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_AnimalKinds_AnimalKindId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_AnimalKindId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_BlogCategories_AnimalKindId",
                table: "BlogCategories");

            migrationBuilder.DropColumn(
                name: "AnimalKindId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "AnimalKindId",
                table: "BlogCategories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnimalKindId",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AnimalKindId",
                table: "BlogCategories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_AnimalKindId",
                table: "Categories",
                column: "AnimalKindId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogCategories_AnimalKindId",
                table: "BlogCategories",
                column: "AnimalKindId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogCategories_AnimalKinds_AnimalKindId",
                table: "BlogCategories",
                column: "AnimalKindId",
                principalTable: "AnimalKinds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_AnimalKinds_AnimalKindId",
                table: "Categories",
                column: "AnimalKindId",
                principalTable: "AnimalKinds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
