using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Homemade.DAL.Contract.Migrations
{
    public partial class NewEditPermissionProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.InsertData(
                schema: "Permission",
                table: "PermissionController",
                columns: new[] { "PermissionControllerID", "PermissionControllerGuid", "PermissionControllerNameAr", "PermissionControllerNameEn" },
                values: new object[,]
                {
                    { 27, new Guid("6e0e5113-6e91-4744-bfe9-5ed00797005c"), "المنتجات", "Product" }
                });


            migrationBuilder.InsertData(
                schema: "Permission",
                table: "PermissionControllerAction",
                columns: new[] { "PermissionControllerActionID", "PermissionActionID", "PermissionControllerActionGuid", "PermissionControllerID" },
                values: new object[,]
                {
                    { 105, 1, new Guid("ae07b56a-5d81-43fb-813a-08db1e3d9c3d"), 27 },
                    { 106, 2, new Guid("d859e49c-474e-4a9e-ad71-82dd62014264"), 27 },
                    { 107, 3, new Guid("96d7aa9c-5ded-4376-b955-d10daccefab9"), 27 },
                    { 108, 4, new Guid("e7927e6d-3eab-4d5d-90ae-4da8fede7d13"), 27 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }

    }
}
