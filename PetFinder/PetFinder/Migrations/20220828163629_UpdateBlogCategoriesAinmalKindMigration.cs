using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PetFinder.Migrations
{
    public partial class UpdateBlogCategoriesAinmalKindMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Categories",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Categories",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "Categories",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Categories",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_AnimalKindId",
                table: "Categories",
                column: "AnimalKindId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_AnimalKinds_AnimalKindId",
                table: "Categories",
                column: "AnimalKindId",
                principalTable: "AnimalKinds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_AnimalKinds_AnimalKindId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_AnimalKindId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Categories");
        }
    }
}
