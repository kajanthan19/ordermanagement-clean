using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderManagement.Migrations.Migrations
{
    public partial class Initial_002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Person_Email",
                schema: "User",
                table: "Person",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_OrderNo",
                schema: "Order",
                table: "Order",
                column: "OrderNo",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Person_Email",
                schema: "User",
                table: "Person");

            migrationBuilder.DropIndex(
                name: "IX_Order_OrderNo",
                schema: "Order",
                table: "Order");
        }
    }
}
