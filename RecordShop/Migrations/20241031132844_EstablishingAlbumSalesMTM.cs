using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecordShop.Migrations
{
    /// <inheritdoc />
    public partial class EstablishingAlbumSalesMTM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Albums_Sales_SaleId",
                table: "Albums");

            migrationBuilder.DropIndex(
                name: "IX_Albums_SaleId",
                table: "Albums");

            migrationBuilder.DropColumn(
                name: "SaleId",
                table: "Albums");

            migrationBuilder.CreateTable(
                name: "AlbumSale",
                columns: table => new
                {
                    ProductsId = table.Column<int>(type: "INTEGER", nullable: false),
                    SalesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlbumSale", x => new { x.ProductsId, x.SalesId });
                    table.ForeignKey(
                        name: "FK_AlbumSale_Albums_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlbumSale_Sales_SalesId",
                        column: x => x.SalesId,
                        principalTable: "Sales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlbumSale_SalesId",
                table: "AlbumSale",
                column: "SalesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlbumSale");

            migrationBuilder.AddColumn<int>(
                name: "SaleId",
                table: "Albums",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Albums_SaleId",
                table: "Albums",
                column: "SaleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_Sales_SaleId",
                table: "Albums",
                column: "SaleId",
                principalTable: "Sales",
                principalColumn: "Id");
        }
    }
}
