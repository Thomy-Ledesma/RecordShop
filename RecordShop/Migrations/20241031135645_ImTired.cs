using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecordShop.Migrations
{
    /// <inheritdoc />
    public partial class ImTired : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlbumSale_Sales_SalesId",
                table: "AlbumSale");

            migrationBuilder.RenameColumn(
                name: "SalesId",
                table: "AlbumSale",
                newName: "SaleId");

            migrationBuilder.RenameIndex(
                name: "IX_AlbumSale_SalesId",
                table: "AlbumSale",
                newName: "IX_AlbumSale_SaleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AlbumSale_Sales_SaleId",
                table: "AlbumSale",
                column: "SaleId",
                principalTable: "Sales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlbumSale_Sales_SaleId",
                table: "AlbumSale");

            migrationBuilder.RenameColumn(
                name: "SaleId",
                table: "AlbumSale",
                newName: "SalesId");

            migrationBuilder.RenameIndex(
                name: "IX_AlbumSale_SaleId",
                table: "AlbumSale",
                newName: "IX_AlbumSale_SalesId");

            migrationBuilder.AddForeignKey(
                name: "FK_AlbumSale_Sales_SalesId",
                table: "AlbumSale",
                column: "SalesId",
                principalTable: "Sales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
