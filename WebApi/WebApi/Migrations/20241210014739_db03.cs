using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class db03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductGroups_ProductGroupId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductGroups",
                table: "ProductGroups");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Product");

            migrationBuilder.RenameTable(
                name: "ProductGroups",
                newName: "ProductGroup");

            migrationBuilder.RenameIndex(
                name: "IX_Products_ProductGroupId",
                table: "Product",
                newName: "IX_Product_ProductGroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductGroup",
                table: "ProductGroup",
                column: "ProductGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductGroup_ProductGroupId",
                table: "Product",
                column: "ProductGroupId",
                principalTable: "ProductGroup",
                principalColumn: "ProductGroupId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductGroup_ProductGroupId",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductGroup",
                table: "ProductGroup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.RenameTable(
                name: "ProductGroup",
                newName: "ProductGroups");

            migrationBuilder.RenameTable(
                name: "Product",
                newName: "Products");

            migrationBuilder.RenameIndex(
                name: "IX_Product_ProductGroupId",
                table: "Products",
                newName: "IX_Products_ProductGroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductGroups",
                table: "ProductGroups",
                column: "ProductGroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductGroups_ProductGroupId",
                table: "Products",
                column: "ProductGroupId",
                principalTable: "ProductGroups",
                principalColumn: "ProductGroupId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
