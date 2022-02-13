using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PromiData.Migrations
{
    public partial class updateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductService_Products_ProductId",
                table: "ProductService");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductService_Services_ServiceId",
                table: "ProductService");

            migrationBuilder.DropForeignKey(
                name: "FK_UserServices_ProductService_ProductServiceId",
                table: "UserServices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductService",
                table: "ProductService");

            migrationBuilder.RenameTable(
                name: "ProductService",
                newName: "productServices");

            migrationBuilder.RenameIndex(
                name: "IX_ProductService_ServiceId",
                table: "productServices",
                newName: "IX_productServices_ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductService_ProductId",
                table: "productServices",
                newName: "IX_productServices_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_productServices",
                table: "productServices",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "MaterialsWarehouse",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastAdittion",
                value: new DateTime(2022, 2, 13, 16, 8, 52, 761, DateTimeKind.Local).AddTicks(5923));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c9490c27-1b89-4e39-8f2e-99b48dcc709e"),
                column: "Password",
                value: "$2a$11$F4PEe5jay1pvTkcHdKNau.ntLz2C4kYM6Jf4xE6qRgHDL8/mVQ0TO");

            migrationBuilder.UpdateData(
                table: "WeeklyWorkSchedules",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2022, 2, 13, 16, 8, 52, 756, DateTimeKind.Local).AddTicks(6887));

            migrationBuilder.AddForeignKey(
                name: "FK_productServices_Products_ProductId",
                table: "productServices",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_productServices_Services_ServiceId",
                table: "productServices",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserServices_productServices_ProductServiceId",
                table: "UserServices",
                column: "ProductServiceId",
                principalTable: "productServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productServices_Products_ProductId",
                table: "productServices");

            migrationBuilder.DropForeignKey(
                name: "FK_productServices_Services_ServiceId",
                table: "productServices");

            migrationBuilder.DropForeignKey(
                name: "FK_UserServices_productServices_ProductServiceId",
                table: "UserServices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_productServices",
                table: "productServices");

            migrationBuilder.RenameTable(
                name: "productServices",
                newName: "ProductService");

            migrationBuilder.RenameIndex(
                name: "IX_productServices_ServiceId",
                table: "ProductService",
                newName: "IX_ProductService_ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_productServices_ProductId",
                table: "ProductService",
                newName: "IX_ProductService_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductService",
                table: "ProductService",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "MaterialsWarehouse",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastAdittion",
                value: new DateTime(2022, 2, 13, 16, 5, 11, 538, DateTimeKind.Local).AddTicks(1106));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c9490c27-1b89-4e39-8f2e-99b48dcc709e"),
                column: "Password",
                value: "$2a$11$hwAo5vxxWmU97wioq/q/Q.dI43FPKkZGG/.IJa0uLbUSH5cS70GV2");

            migrationBuilder.UpdateData(
                table: "WeeklyWorkSchedules",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2022, 2, 13, 16, 5, 11, 533, DateTimeKind.Local).AddTicks(7505));

            migrationBuilder.AddForeignKey(
                name: "FK_ProductService_Products_ProductId",
                table: "ProductService",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductService_Services_ServiceId",
                table: "ProductService",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserServices_ProductService_ProductServiceId",
                table: "UserServices",
                column: "ProductServiceId",
                principalTable: "ProductService",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
