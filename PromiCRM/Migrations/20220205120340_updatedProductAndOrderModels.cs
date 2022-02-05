using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PromiCRM.Migrations
{
    public partial class updatedProductAndOrderModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BondingTime",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CollectionTime",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "LaserTime",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "MilingTime",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PackingTime",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PaintingTime",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "BondingComplete",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "BondingTime",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "BondingUserId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CollectionComplete",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CollectionTime",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CollectionUserId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "LaserComplete",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "LaserTime",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "LaserUserId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "MilingComplete",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "MilingTime",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "MilingUserId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PackingComplete",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PackingTime",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PackingUserId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PaintingComplete",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PaintingTime",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PaintingUserId",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "MaterialsWarehouse",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastAdittion",
                value: new DateTime(2022, 2, 5, 14, 3, 39, 322, DateTimeKind.Local).AddTicks(7929));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c9490c27-1b89-4e39-8f2e-99b48dcc709e"),
                column: "Password",
                value: "$2a$11$Ws8Bfccvq3.8hxd.xcgye.iJ9r/XJRapaRIj6RHCidC03poDb1kpK");

            migrationBuilder.UpdateData(
                table: "WeeklyWorkSchedules",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2022, 2, 5, 14, 3, 39, 319, DateTimeKind.Local).AddTicks(3763));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BondingTime",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CollectionTime",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LaserTime",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MilingTime",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "PackingTime",
                table: "Products",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "PaintingTime",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BondingComplete",
                table: "Orders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BondingTime",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BondingUserId",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CollectionComplete",
                table: "Orders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CollectionTime",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CollectionUserId",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LaserComplete",
                table: "Orders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LaserTime",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LaserUserId",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "MilingComplete",
                table: "Orders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MilingTime",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MilingUserId",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PackingComplete",
                table: "Orders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PackingTime",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PackingUserId",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PaintingComplete",
                table: "Orders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaintingTime",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaintingUserId",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "MaterialsWarehouse",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastAdittion",
                value: new DateTime(2022, 2, 4, 19, 44, 32, 96, DateTimeKind.Local).AddTicks(3233));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BondingTime", "CollectionTime", "LaserTime", "MilingTime", "PackingTime", "PaintingTime" },
                values: new object[] { 40, 20, 10, 20, 10.0, 15 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c9490c27-1b89-4e39-8f2e-99b48dcc709e"),
                column: "Password",
                value: "$2a$11$CXmbkdelEBaSeK84r/twhOfxgqe2wROs6M3e.1lZmGSwLF7cUV26S");

            migrationBuilder.UpdateData(
                table: "WeeklyWorkSchedules",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2022, 2, 4, 19, 44, 32, 91, DateTimeKind.Local).AddTicks(7219));
        }
    }
}
