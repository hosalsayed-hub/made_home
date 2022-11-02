using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Homemade.DAL.Contract.Migrations
{
    public partial class NewEditPermissionDriver : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.InsertData(
                schema: "Permission",
                table: "PermissionController",
                columns: new[] { "PermissionControllerID", "PermissionControllerGuid", "PermissionControllerNameAr", "PermissionControllerNameEn" },
                values: new object[,]
                {
                    { 37, new Guid("fdf89b5c-f2ec-4c30-9c03-39a290394330"), "الطلبات الجديده للسائق", "Driver New Requests" },
                    { 40, new Guid("6a632372-e6dc-42d4-9536-0c8c7b91160d"), "طلبات تحت المراجعة للسائق", "Driver Under Requests" },
                    { 39, new Guid("00c367d1-13a8-4873-b8a0-dadbd49626da"), "طلبات انتظار التفعيل للسائق", "Driver Waiting Activation" },
                    { 38, new Guid("2ebc44ff-6b56-46f7-8a71-0069d2b6f58d"), "الطلبات الملغية للسائق", "Driver Rejected Requests" }
                });

            migrationBuilder.InsertData(
                schema: "Permission",
                table: "PermissionControllerAction",
                columns: new[] { "PermissionControllerActionID", "PermissionActionID", "PermissionControllerActionGuid", "PermissionControllerID" },
                values: new object[,]
                {
                    { 145, 1, new Guid("73fb3360-3041-4e44-a0cc-d8d5bfdc4696"), 37 },
                    { 146, 2, new Guid("566ed3b1-8bfa-412a-aa9d-c799221a3d71"), 37 },
                    { 147, 3, new Guid("1e8a018a-81c8-42cd-934f-d2e534066d21"), 37 },
                    { 148, 4, new Guid("c1d92f0c-ebd2-4f2a-ae98-40e2a8fa2a2e"), 37 },
                    { 149, 1, new Guid("dcfba536-c1a7-4681-bc10-ecc4b7008626"), 38 },
                    { 150, 2, new Guid("ca490d6d-caeb-46db-97e5-23fad5630c9c"), 38 },
                    { 151, 3, new Guid("34d5475d-66c6-4f84-ab86-534286c931e5"), 38 },
                    { 152, 4, new Guid("1ba19d32-dc8f-45fe-9142-c70390334f95"), 38 },
                    { 153, 1, new Guid("87e4bd57-fe1b-463a-a25f-701dc4dc0a3e"), 39 },
                    { 154, 2, new Guid("eebf12da-acf3-4054-9cc3-b7c66d449965"), 39 },
                    { 155, 3, new Guid("5a84c4b0-fd28-4fbf-a863-788bdc49877a"), 39 },
                    { 156, 4, new Guid("e7180f18-920c-4e7c-ab8c-e3917b36fb20"), 39 },
                    { 157, 1, new Guid("e64bcb43-5d80-466b-9675-ae84cf0233d1"), 40 },
                    { 158, 2, new Guid("745e8079-7853-4e90-80e5-0148c9870aaa"), 40 },
                    { 159, 3, new Guid("13d67d95-f162-4023-940b-75aa9dccea96"), 40 },
                    { 160, 4, new Guid("e73eeee7-f550-4366-b5e8-f1faf2a7b618"), 40 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
