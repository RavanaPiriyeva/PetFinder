using Microsoft.EntityFrameworkCore.Migrations;

namespace PetFinder.Migrations
{
    public partial class UpdateAnimalMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShelterId",
                table: "Animals",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Animals_ShelterId",
                table: "Animals",
                column: "ShelterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Shelters_ShelterId",
                table: "Animals",
                column: "ShelterId",
                principalTable: "Shelters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Shelters_ShelterId",
                table: "Animals");

            migrationBuilder.DropIndex(
                name: "IX_Animals_ShelterId",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "ShelterId",
                table: "Animals");
        }
    }
}
