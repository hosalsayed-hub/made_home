using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Homemade.DAL.Contract.Migrations
{
    public partial class NewEditVendorName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SeconedName",
                schema: "Vendor",
                table: "Vendors",
                newName: "SeconedNameEn");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                schema: "Vendor",
                table: "Vendors",
                newName: "SeconedNameAr");

            migrationBuilder.RenameColumn(
                name: "Pieces",
                schema: "Vendor",
                table: "Product",
                newName: "PiecesEn");

            migrationBuilder.AddColumn<string>(
                name: "FirstNameAr",
                schema: "Vendor",
                table: "Vendors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstNameEn",
                schema: "Vendor",
                table: "Vendors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PiecesAr",
                schema: "Vendor",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
