using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PromiData.Migrations
{
    public partial class updateOrderIdAsForeignKeyInUserServices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MaterialsWarehouse",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastAdittion",
                value: new DateTime(2022, 2, 11, 18, 4, 42, 332, DateTimeKind.Local).AddTicks(8089));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c9490c27-1b89-4e39-8f2e-99b48dcc709e"),
                column: "Password",
                value: "$2a$11$Un0iZKPD9sbHkxgl7n98luGtakxmhoYmXySoTDdI5.II/6nZqNnw.");

            migrationBuilder.UpdateData(
                table: "WeeklyWorkSchedules",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2022, 2, 11, 18, 4, 42, 329, DateTimeKind.Local).AddTicks(5002));

            migrationBuilder.CreateIndex(
                name: "IX_UserService_OrderId",
                table: "UserService",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserService_Orders_OrderId",
                table: "UserService",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserService_Orders_OrderId",
                table: "UserService");

            migrationBuilder.DropIndex(
                name: "IX_UserService_OrderId",
                table: "UserService");

            migrationBuilder.UpdateData(
                table: "MaterialsWarehouse",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastAdittion",
                value: new DateTime(2022, 2, 9, 15, 20, 18, 682, DateTimeKind.Local).AddTicks(2554));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c9490c27-1b89-4e39-8f2e-99b48dcc709e"),
                column: "Password",
                value: "$2a$11$4jicWKEuM/e9Zw4nhvjDDOLC8Wk2/tGT/GXoEY4gw1UiPhX.QENQu");

            migrationBuilder.UpdateData(
                table: "WeeklyWorkSchedules",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2022, 2, 9, 15, 20, 18, 677, DateTimeKind.Local).AddTicks(781));
        }
    }
}
