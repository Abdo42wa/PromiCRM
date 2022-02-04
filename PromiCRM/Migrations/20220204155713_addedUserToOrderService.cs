using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PromiCRM.Migrations
{
    public partial class addedUserToOrderService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "OrderServices",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "MaterialsWarehouse",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastAdittion",
                value: new DateTime(2022, 2, 4, 17, 57, 11, 732, DateTimeKind.Local).AddTicks(476));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c9490c27-1b89-4e39-8f2e-99b48dcc709e"),
                column: "Password",
                value: "$2a$11$sdMnl4jaNDMDTPSeWPzqcOmGJrLAICyFa9X0lzlcw15o4sMFPwrg.");

            migrationBuilder.UpdateData(
                table: "WeeklyWorkSchedules",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2022, 2, 4, 17, 57, 11, 727, DateTimeKind.Local).AddTicks(3817));

            migrationBuilder.CreateIndex(
                name: "IX_OrderServices_UserId",
                table: "OrderServices",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderServices_Users_UserId",
                table: "OrderServices",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderServices_Users_UserId",
                table: "OrderServices");

            migrationBuilder.DropIndex(
                name: "IX_OrderServices_UserId",
                table: "OrderServices");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "OrderServices");

            migrationBuilder.UpdateData(
                table: "MaterialsWarehouse",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastAdittion",
                value: new DateTime(2022, 2, 4, 17, 47, 48, 485, DateTimeKind.Local).AddTicks(246));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c9490c27-1b89-4e39-8f2e-99b48dcc709e"),
                column: "Password",
                value: "$2a$11$EmNLtp/EvJ341.O/tAWBX.zgYNnKFllKFBQV8uNAOgKeG1JGy6MpO");

            migrationBuilder.UpdateData(
                table: "WeeklyWorkSchedules",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2022, 2, 4, 17, 47, 48, 479, DateTimeKind.Local).AddTicks(4835));
        }
    }
}
