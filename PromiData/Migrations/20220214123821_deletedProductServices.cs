using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PromiData.Migrations
{
    public partial class deletedProductServices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserServices_ProductServices_ProductServiceId",
                table: "UserServices");

            migrationBuilder.DropTable(
                name: "ProductServices");

            migrationBuilder.DropIndex(
                name: "IX_UserServices_ProductServiceId",
                table: "UserServices");

            migrationBuilder.DropColumn(
                name: "ProductServiceId",
                table: "UserServices");

            migrationBuilder.UpdateData(
                table: "MaterialsWarehouse",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastAdittion",
                value: new DateTime(2022, 2, 14, 14, 38, 20, 859, DateTimeKind.Local).AddTicks(3834));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c9490c27-1b89-4e39-8f2e-99b48dcc709e"),
                column: "Password",
                value: "$2a$11$Yi2.Z8843HLked1B/yJVRu9AyDYusIZV5/k94rFvJBq9ZO3dPrbYW");

            migrationBuilder.UpdateData(
                table: "WeeklyWorkSchedules",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2022, 2, 14, 14, 38, 20, 852, DateTimeKind.Local).AddTicks(7960));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductServiceId",
                table: "UserServices",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProductServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    TimeConsumption = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductServices_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductServices_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "MaterialsWarehouse",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastAdittion",
                value: new DateTime(2022, 2, 14, 12, 52, 19, 311, DateTimeKind.Local).AddTicks(8077));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c9490c27-1b89-4e39-8f2e-99b48dcc709e"),
                column: "Password",
                value: "$2a$11$9MrygBBeAMKPq9uNWcsgOuMD0CHLJOwdbO2X9v9HL2.OilfN9goEi");

            migrationBuilder.UpdateData(
                table: "WeeklyWorkSchedules",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2022, 2, 14, 12, 52, 19, 308, DateTimeKind.Local).AddTicks(3557));

            migrationBuilder.CreateIndex(
                name: "IX_UserServices_ProductServiceId",
                table: "UserServices",
                column: "ProductServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductServices_ProductId",
                table: "ProductServices",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductServices_ServiceId",
                table: "ProductServices",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserServices_ProductServices_ProductServiceId",
                table: "UserServices",
                column: "ProductServiceId",
                principalTable: "ProductServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
