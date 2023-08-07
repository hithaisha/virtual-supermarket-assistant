using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MORR.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MORR00004 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Categroy_CategoryId",
                table: "Product");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Categroy_CategoryId",
                table: "Product",
                column: "CategoryId",
                principalTable: "Categroy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Categroy_CategoryId",
                table: "Product");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Categroy_CategoryId",
                table: "Product",
                column: "CategoryId",
                principalTable: "Categroy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
