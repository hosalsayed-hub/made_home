using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Homemade.DAL.Contract.Migrations
{
    public partial class addQuantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductOrder",
                schema: "Vendor",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductQuantity",
                schema: "Vendor",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductQuantityType",
                schema: "Vendor",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

           }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductOrder",
                schema: "Vendor",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ProductQuantity",
                schema: "Vendor",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ProductQuantityType",
                schema: "Vendor",
                table: "Product");

            }
    }
}
