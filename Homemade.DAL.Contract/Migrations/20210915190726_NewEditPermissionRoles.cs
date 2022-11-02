using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Homemade.DAL.Contract.Migrations
{
    public partial class NewEditPermissionRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: 6);

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: 1,
               columns: new[] { "ConcurrencyStamp", "Name" },
                values: new object[] { "16455d48-4b00-49af-af4a-7be810d26f83", "Admin" });

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "Name", "RoleTypeId" },
                values: new object[] { "16480d48-4b00-49af-af4a-7be810d26f83", "Vendor", 2 });

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "Name" },
                values: new object[] { "acac8c22-0df9-4038-8ce3-166310b1ca72", "Customer" });

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "Name" },
                values: new object[] { "4d044bdb-e13e-4f9f-9527-a27e0698a3ce", "Employee" });



            migrationBuilder.InsertData(
                schema: "Permission",
                table: "PermissionController",
                columns: new[] { "PermissionControllerID", "PermissionControllerGuid", "PermissionControllerNameAr", "PermissionControllerNameEn" },
                values: new object[] { 30, new Guid("b9be5c80-1133-4f40-be23-614ea1d5a721"), "منتجات الاوبريشن", "Product Operation" });

            migrationBuilder.InsertData(
                schema: "Permission",
                table: "PermissionControllerAction",
                columns: new[] { "PermissionControllerActionID", "PermissionActionID", "PermissionControllerActionGuid", "PermissionControllerID" },
                values: new object[,]
                {
                    { 117, 1, new Guid("15179b43-21bb-42a2-81a5-841324c37ad8"), 30 },
                    { 118, 2, new Guid("c6186e21-8521-4825-9de0-3e288339f07d"), 30 },
                    { 119, 3, new Guid("053031dc-6cfc-4669-a578-e26434591203"), 30 },
                    { 120, 4, new Guid("9f997870-976a-4dc4-aeaa-54154457ed9c"), 30 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
