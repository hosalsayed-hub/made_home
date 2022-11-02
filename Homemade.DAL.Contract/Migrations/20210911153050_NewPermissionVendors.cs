using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Homemade.DAL.Contract.Migrations
{
    public partial class NewPermissionVendors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.InsertData(
                schema: "Permission",
                table: "PermissionController",
                columns: new[] { "PermissionControllerID", "PermissionControllerGuid", "PermissionControllerNameAr", "PermissionControllerNameEn" },
                values: new object[,]
                {
                    { 25, new Guid("5e94c61c-515f-4c8b-91ff-61d52cc19745"), "المتاجر", "Vendors" },
                    { 26, new Guid("dff490d1-c3ca-496d-8c12-ef7afd56a5e3"), "الزبائن", "Customer" }
                });


            migrationBuilder.InsertData(
                schema: "Permission",
                table: "PermissionControllerAction",
                columns: new[] { "PermissionControllerActionID", "PermissionActionID", "PermissionControllerActionGuid", "PermissionControllerID" },
                values: new object[,]
                {
                    { 97, 1, new Guid("72e8e4da-96ea-47f6-8d87-aa0dde3c38c1"), 25 },
                    { 98, 2, new Guid("9a95cb24-4248-4632-a9fb-582e3414f1e2"), 25 },
                    { 99, 3, new Guid("4358cea8-39a1-4e7e-8461-9d5ff3687d22"), 25 },
                    { 100, 4, new Guid("f49fc0ef-2415-48a3-b546-3e355145baf7"), 25 },
                    { 101, 1, new Guid("fa746a39-2e72-4db2-ab15-b9fecf2a60a1"), 26 },
                    { 102, 2, new Guid("45486984-ced5-47ab-80c6-94b38e6a4d6b"), 26 },
                    { 103, 3, new Guid("577ceffd-9a94-46a9-8701-ab956b19f820"), 26 },
                    { 104, 4, new Guid("c2feedd2-4628-4c82-9756-36d56044bad9"), 26 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
