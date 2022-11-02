using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Homemade.DAL.Contract.Migrations
{
    public partial class UpdatePermisionsOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.InsertData(
                schema: "Permission",
                table: "PermissionController",
                columns: new[] { "PermissionControllerID", "PermissionControllerGuid", "PermissionControllerNameAr", "PermissionControllerNameEn" },
                values: new object[,]
                {
                    { 31, new Guid("d909e176-f755-4d2c-8c2f-1c15d326d760"), "طلبات الاوبريشن", "Operation Orders" },
                    { 32, new Guid("e2cb6ec1-66fe-4ce9-a125-096dd09d4c83"), "طلبات المتجر", "Vendor Orders" }
                });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
