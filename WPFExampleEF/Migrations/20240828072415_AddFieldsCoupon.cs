using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WPFExampleEF.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldsCoupon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Litera",
                table: "Bonds",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<double>(
                name: "CouponValue",
                table: "Bonds",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateNextCoupon",
                table: "Bonds",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CouponValue",
                table: "Bonds");

            migrationBuilder.DropColumn(
                name: "DateNextCoupon",
                table: "Bonds");

            migrationBuilder.AlterColumn<string>(
                name: "Litera",
                table: "Bonds",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
        }
    }
}
