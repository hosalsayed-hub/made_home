using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Homemade.DAL.Contract.Migrations
{
    public partial class UpdatePermisionsBranches : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.InsertData(
                schema: "Permission",
                table: "PermissionController",
                columns: new[] { "PermissionControllerID", "PermissionControllerGuid", "PermissionControllerNameAr", "PermissionControllerNameEn" },
                values: new object[] { 33, new Guid("221a5a9e-0732-4f14-a145-fb0b22e6221d"), "الافرع", "Branches" });


            migrationBuilder.InsertData(
                schema: "Permission",
                table: "PermissionControllerAction",
                columns: new[] { "PermissionControllerActionID", "PermissionActionID", "PermissionControllerActionGuid", "PermissionControllerID" },
                values: new object[,]
                {
                    { 129, 1, new Guid("0b89c89f-358d-473c-a401-4b8814da8f0a"), 33 },
                    { 130, 2, new Guid("44e3fc5c-634c-497a-940a-7f5846f86a04"), 33 },
                    { 131, 3, new Guid("64f0f23b-c414-4925-b204-13ab94ea763c"), 33 },
                    { 132, 4, new Guid("7012522f-380c-47bc-a471-23e4f9d16da3"), 33 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
