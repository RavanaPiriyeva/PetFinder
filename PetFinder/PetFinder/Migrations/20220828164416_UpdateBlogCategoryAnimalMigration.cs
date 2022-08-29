using Microsoft.EntityFrameworkCore.Migrations;

namespace PetFinder.Migrations
{
    public partial class UpdateBlogCategoryAnimalMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnimalKindId",
                table: "Blogs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_AnimalKindId",
                table: "Blogs",
                column: "AnimalKindId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_AnimalKinds_AnimalKindId",
                table: "Blogs",
                column: "AnimalKindId",
                principalTable: "AnimalKinds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_AnimalKinds_AnimalKindId",
                table: "Blogs");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_AnimalKindId",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "AnimalKindId",
                table: "Blogs");
        }
    }
}
