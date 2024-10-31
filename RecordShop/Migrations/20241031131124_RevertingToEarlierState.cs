using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecordShop.Migrations
{
    /// <inheritdoc />
    public partial class RevertingToEarlierState : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Albums_Sales_SaleId",
                table: "Albums");

            migrationBuilder.AlterColumn<int>(
                name: "SaleId",
                table: "Albums",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_Sales_SaleId",
                table: "Albums",
                column: "SaleId",
                principalTable: "Sales",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Albums_Sales_SaleId",
                table: "Albums");

            migrationBuilder.AlterColumn<int>(
                name: "SaleId",
                table: "Albums",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

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
