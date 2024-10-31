using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecordShop.Migrations
{
    /// <inheritdoc />
    public partial class SaleAlbumEntity : Migration
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
                name: "SaleAlbum",
                columns: table => new
                {
                    SaleId = table.Column<int>(type: "INTEGER", nullable: false),
                    AlbumId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleAlbum", x => new { x.SaleId, x.AlbumId });
                    table.ForeignKey(
                        name: "FK_SaleAlbum_Albums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SaleAlbum_Sales_SaleId",
                        column: x => x.SaleId,
                        principalTable: "Sales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SaleAlbum_AlbumId",
                table: "SaleAlbum",
                column: "AlbumId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SaleAlbum");

            migrationBuilder.AddColumn<int>(
                name: "SaleId",
                table: "Albums",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Albums_SaleId",
                table: "Albums",
                column: "SaleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_Sales_SaleId",
                table: "Albums",
                column: "SaleId",
                principalTable: "Sales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
