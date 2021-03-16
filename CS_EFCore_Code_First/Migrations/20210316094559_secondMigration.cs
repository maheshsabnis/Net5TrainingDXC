using Microsoft.EntityFrameworkCore.Migrations;

namespace CS_EFCore_Code_First.Migrations
{
    public partial class secondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryRowId1",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryRowId1",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CategoryRowId1",
                table: "Products");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryRowId",
                table: "Products",
                column: "CategoryRowId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryRowId",
                table: "Products",
                column: "CategoryRowId",
                principalTable: "Categories",
                principalColumn: "CategoryRowId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryRowId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryRowId",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "CategoryRowId1",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryRowId1",
                table: "Products",
                column: "CategoryRowId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryRowId1",
                table: "Products",
                column: "CategoryRowId1",
                principalTable: "Categories",
                principalColumn: "CategoryRowId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
