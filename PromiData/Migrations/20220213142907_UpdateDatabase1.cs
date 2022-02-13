using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PromiData.Migrations
{
    public partial class UpdateDatabase1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                newName: "ProductServices");

            migrationBuilder.RenameIndex(
                name: "IX_productServices_ServiceId",
                table: "ProductServices",
                newName: "IX_ProductServices_ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_productServices_ProductId",
                table: "ProductServices",
                newName: "IX_ProductServices_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductServices",
                table: "ProductServices",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "MaterialsWarehouse",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastAdittion",
                value: new DateTime(2022, 2, 13, 16, 29, 6, 117, DateTimeKind.Local).AddTicks(1424));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c9490c27-1b89-4e39-8f2e-99b48dcc709e"),
                column: "Password",
                value: "$2a$11$qqwjVA4mr1pzo3Y/GgHd8uwJi7PzYyvUeIKjJS5ba7dxQUqKCJeDK");

            migrationBuilder.UpdateData(
                table: "WeeklyWorkSchedules",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2022, 2, 13, 16, 29, 6, 112, DateTimeKind.Local).AddTicks(654));

            migrationBuilder.AddForeignKey(
                name: "FK_ProductServices_Products_ProductId",
                table: "ProductServices",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductServices_Services_ServiceId",
                table: "ProductServices",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserServices_ProductServices_ProductServiceId",
                table: "UserServices",
                column: "ProductServiceId",
                principalTable: "ProductServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductServices_Products_ProductId",
                table: "ProductServices");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductServices_Services_ServiceId",
                table: "ProductServices");

            migrationBuilder.DropForeignKey(
                name: "FK_UserServices_ProductServices_ProductServiceId",
                table: "UserServices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductServices",
                table: "ProductServices");

            migrationBuilder.RenameTable(
                name: "ProductServices",
                newName: "productServices");

            migrationBuilder.RenameIndex(
                name: "IX_ProductServices_ServiceId",
                table: "productServices",
                newName: "IX_productServices_ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductServices_ProductId",
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
    }
}
