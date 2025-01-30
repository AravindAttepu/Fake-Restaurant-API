using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FakeRestuarantAPI.Migrations
{
    /// <inheritdoc />
    public partial class finalrepo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_masterOrders_MasterOrderMasterID",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_MasterOrderMasterID",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "MasterOrderMasterID",
                table: "Order");

            migrationBuilder.AddColumn<string>(
                name: "imageUrl",
                table: "Item",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "imageUrl",
                table: "Item");

            migrationBuilder.AddColumn<int>(
                name: "MasterOrderMasterID",
                table: "Order",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_MasterOrderMasterID",
                table: "Order",
                column: "MasterOrderMasterID");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_masterOrders_MasterOrderMasterID",
                table: "Order",
                column: "MasterOrderMasterID",
                principalTable: "masterOrders",
                principalColumn: "MasterID");
        }
    }
}
