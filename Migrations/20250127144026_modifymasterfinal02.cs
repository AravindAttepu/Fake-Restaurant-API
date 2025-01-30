using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FakeRestuarantAPI.Migrations
{
    /// <inheritdoc />
    public partial class modifymasterfinal02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_masterOrders_User_Usercode",
                table: "masterOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_User_UserID",
                table: "Order");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_UserEmail",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Userapi",
                table: "masterOrders");

            migrationBuilder.RenameColumn(
                name: "Usercode",
                table: "masterOrders",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_masterOrders_Usercode",
                table: "masterOrders",
                newName: "IX_masterOrders_UserID");

            migrationBuilder.AlterColumn<string>(
                name: "Usercode",
                table: "User",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "UserEmail");

            migrationBuilder.CreateIndex(
                name: "IX_User_Usercode",
                table: "User",
                column: "Usercode",
                unique: true,
                filter: "[Usercode] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_masterOrders_User_UserID",
                table: "masterOrders",
                column: "UserID",
                principalTable: "User",
                principalColumn: "UserEmail",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_User_UserID",
                table: "Order",
                column: "UserID",
                principalTable: "User",
                principalColumn: "UserEmail",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_masterOrders_User_UserID",
                table: "masterOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_User_UserID",
                table: "Order");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_Usercode",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "masterOrders",
                newName: "Usercode");

            migrationBuilder.RenameIndex(
                name: "IX_masterOrders_UserID",
                table: "masterOrders",
                newName: "IX_masterOrders_Usercode");

            migrationBuilder.AlterColumn<string>(
                name: "Usercode",
                table: "User",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Userapi",
                table: "masterOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Usercode");

            migrationBuilder.CreateIndex(
                name: "IX_User_UserEmail",
                table: "User",
                column: "UserEmail",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_masterOrders_User_Usercode",
                table: "masterOrders",
                column: "Usercode",
                principalTable: "User",
                principalColumn: "Usercode",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_User_UserID",
                table: "Order",
                column: "UserID",
                principalTable: "User",
                principalColumn: "Usercode",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
