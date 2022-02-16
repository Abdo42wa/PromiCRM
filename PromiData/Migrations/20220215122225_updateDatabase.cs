using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PromiData.Migrations
{
    public partial class updateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MaterialsWarehouse",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastAdittion",
                value: new DateTime(2022, 2, 15, 14, 22, 24, 83, DateTimeKind.Local).AddTicks(9576));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c9490c27-1b89-4e39-8f2e-99b48dcc709e"),
                column: "Password",
                value: "$2a$11$GekZVf3GxqqozKSWtd2L7u4E7wj1nhxqbuE9OclkADGqXmYyrIC02");

            migrationBuilder.UpdateData(
                table: "WeeklyWorkSchedules",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2022, 2, 15, 14, 22, 24, 77, DateTimeKind.Local).AddTicks(8261));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
