using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PromiCRM.Migrations
{
    public partial class addedCountryNameAndCurrency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CountryName",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrencyName",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "MaterialsWarehouse",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastAdittion",
                value: new DateTime(2022, 2, 8, 17, 51, 59, 187, DateTimeKind.Local).AddTicks(1819));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c9490c27-1b89-4e39-8f2e-99b48dcc709e"),
                column: "Password",
                value: "$2a$11$X.YAVAK.Qwa22kWdkCf0seN1qr5OkLdCI.okHyGaGjjI5BXwBwHy6");

            migrationBuilder.UpdateData(
                table: "WeeklyWorkSchedules",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2022, 2, 8, 17, 51, 59, 182, DateTimeKind.Local).AddTicks(9735));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountryName",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CurrencyName",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "MaterialsWarehouse",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastAdittion",
                value: new DateTime(2022, 2, 6, 14, 41, 34, 927, DateTimeKind.Local).AddTicks(5540));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c9490c27-1b89-4e39-8f2e-99b48dcc709e"),
                column: "Password",
                value: "$2a$11$hYLxM0TsX799MuEyvv4Qhui/E1QzU9njwQn0ZWtTigakBX/dELUlK");

            migrationBuilder.UpdateData(
                table: "WeeklyWorkSchedules",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2022, 2, 6, 14, 41, 34, 924, DateTimeKind.Local).AddTicks(3591));
        }
    }
}
