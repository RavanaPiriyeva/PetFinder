using Microsoft.EntityFrameworkCore.Migrations;

namespace PetFinder.Migrations
{
    public partial class UpdateAnimalKindMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Breeds_AnimalKinds_AnimalKindId",
                table: "Breeds");

            migrationBuilder.DropColumn(
                name: "BreedId",
                table: "Breeds");

            migrationBuilder.AlterColumn<int>(
                name: "AnimalKindId",
                table: "Breeds",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Breeds_AnimalKinds_AnimalKindId",
                table: "Breeds",
                column: "AnimalKindId",
                principalTable: "AnimalKinds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Breeds_AnimalKinds_AnimalKindId",
                table: "Breeds");

            migrationBuilder.AlterColumn<int>(
                name: "AnimalKindId",
                table: "Breeds",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "BreedId",
                table: "Breeds",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Breeds_AnimalKinds_AnimalKindId",
                table: "Breeds",
                column: "AnimalKindId",
                principalTable: "AnimalKinds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
