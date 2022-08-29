using Microsoft.EntityFrameworkCore.Migrations;

namespace PetFinder.Migrations
{
    public partial class CreateAnimalMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animal_Breeds_BreedId",
                table: "Animal");

            migrationBuilder.DropForeignKey(
                name: "FK_Animal_Colors_ColorId",
                table: "Animal");

            migrationBuilder.DropForeignKey(
                name: "FK_AnimalImages_Animal_AnimalId",
                table: "AnimalImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Animal",
                table: "Animal");

            migrationBuilder.RenameTable(
                name: "Animal",
                newName: "Animals");

            migrationBuilder.RenameIndex(
                name: "IX_Animal_ColorId",
                table: "Animals",
                newName: "IX_Animals_ColorId");

            migrationBuilder.RenameIndex(
                name: "IX_Animal_BreedId",
                table: "Animals",
                newName: "IX_Animals_BreedId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Animals",
                table: "Animals",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AnimalImages_Animals_AnimalId",
                table: "AnimalImages",
                column: "AnimalId",
                principalTable: "Animals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Breeds_BreedId",
                table: "Animals",
                column: "BreedId",
                principalTable: "Breeds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Colors_ColorId",
                table: "Animals",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnimalImages_Animals_AnimalId",
                table: "AnimalImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Breeds_BreedId",
                table: "Animals");

            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Colors_ColorId",
                table: "Animals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Animals",
                table: "Animals");

            migrationBuilder.RenameTable(
                name: "Animals",
                newName: "Animal");

            migrationBuilder.RenameIndex(
                name: "IX_Animals_ColorId",
                table: "Animal",
                newName: "IX_Animal_ColorId");

            migrationBuilder.RenameIndex(
                name: "IX_Animals_BreedId",
                table: "Animal",
                newName: "IX_Animal_BreedId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Animal",
                table: "Animal",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Animal_Breeds_BreedId",
                table: "Animal",
                column: "BreedId",
                principalTable: "Breeds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Animal_Colors_ColorId",
                table: "Animal",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnimalImages_Animal_AnimalId",
                table: "AnimalImages",
                column: "AnimalId",
                principalTable: "Animal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
