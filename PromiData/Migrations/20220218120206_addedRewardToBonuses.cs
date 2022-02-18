using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PromiData.Migrations
{
    public partial class addedRewardToBonuses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Reward",
                table: "Bonuses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "MaterialsWarehouse",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastAdittion",
                value: new DateTime(2022, 2, 18, 14, 2, 5, 809, DateTimeKind.Local).AddTicks(588));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c9490c27-1b89-4e39-8f2e-99b48dcc709e"),
                column: "Password",
                value: "$2a$11$Yq/cMDU9oOJE3l4FSTioS.8bE51JJkYTSiV9ZMOQXT8QaJoyhNEYu");

            migrationBuilder.UpdateData(
                table: "WeeklyWorkSchedules",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2022, 2, 18, 14, 2, 5, 805, DateTimeKind.Local).AddTicks(3049));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reward",
                table: "Bonuses");

            migrationBuilder.UpdateData(
                table: "MaterialsWarehouse",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastAdittion",
                value: new DateTime(2022, 2, 17, 20, 37, 37, 61, DateTimeKind.Local).AddTicks(571));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c9490c27-1b89-4e39-8f2e-99b48dcc709e"),
                column: "Password",
                value: "$2a$11$ZCxJndqlQqIWxet28fNqZubGOo3clvhkBII0lxNN36I19cDrevpgm");

            migrationBuilder.UpdateData(
                table: "WeeklyWorkSchedules",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2022, 2, 17, 20, 37, 37, 54, DateTimeKind.Local).AddTicks(5557));
        }
    }
}
