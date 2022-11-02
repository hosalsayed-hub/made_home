using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Homemade.DAL.Contract.Migrations
{
    public partial class PromUpdate_Mig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<decimal>(
            //    name: "MonthlyTarget",
            //    schema: "Vendor",
            //    table: "Vendors",
            //    type: "decimal(18,2)",
            //    nullable: false,
            //    defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "PromoType",
                schema: "Order",
                table: "PromoCode",
                type: "int",
                nullable: false,
                defaultValue: 0);

            //migrationBuilder.InsertData(
            //    table: "Role",
            //    columns: new[] { "RoleId", "ConcurrencyStamp", "Name", "NormalizedName", "RoleTypeId" },
            //    values: new object[,]
            //    {
            //        { 1, "ab67b68f-4836-430b-a8a9-21b36f9ba677", "Admin", null, 1 },
            //        { 2, "0bf04846-f9f9-4b9e-9b6f-971759ffa5c7", "Vendor", null, 2 },
            //        { 3, "39e25ae0-508f-4979-989d-8cb177b88c17", "Customer", null, 2 },
            //        { 4, "00193398-0e21-4fd1-baad-6aca7fa14591", "Employee", null, 2 }
            //    });

            //migrationBuilder.InsertData(
            //    table: "User",
            //    columns: new[] { "UserId", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserJWTToken", "UserName", "UserType" },
            //    values: new object[] { 1, 0, "8fd595e7-47cf-475e-a7ad-8dab302bb1e1", "SystemUser@Admin.com", true, false, null, "SystemUser@Admin.com", "SYSTEMUSER", "AQAAAAEAACcQAAAAEP65QXLX6e94ehLc9ntv07Q7n/aO6wf8y6j/z15XE7hfgyZLCNvHmM3Ar6SaTwzC3g==", "012", true, "b0c21ef7-0e98-476f-ac13-484402f13b7b", false, null, "SystemUser", 1 });

            //migrationBuilder.InsertData(
            //    schema: "Permission",
            //    table: "PermissionAction",
            //    columns: new[] { "PermissionActionID", "PermissionActionGuid", "PermissionActionNameAr", "PermissionActionNameEn" },
            //    values: new object[,]
            //    {
            //        { 2, new Guid("ddda67ef-346e-48b6-82a3-1ddc12690acc"), "اضافة", "Insert" },
            //        { 3, new Guid("da594406-59b0-4d93-8978-f3536e2ae9d8"), "تعديل", "Update" },
            //        { 4, new Guid("08b24d70-f736-4572-9b05-85f102035c01"), "حذف", "Delete" },
            //        { 1, new Guid("b9eb0a1b-fef4-484f-a58e-d3cab82c6712"), "عرض", "View" }
            //    });

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 1,
                column: "PermissionControllerGuid",
                value: new Guid("ac08b6ab-35eb-4ecf-ad2c-f45be21aad6b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 2,
                column: "PermissionControllerGuid",
                value: new Guid("681b6a46-1911-4d47-879d-5806b1f00309"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 3,
                column: "PermissionControllerGuid",
                value: new Guid("2adc8bce-16ee-46ee-ab51-993f7afb8ce6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 4,
                column: "PermissionControllerGuid",
                value: new Guid("a52d91c9-d7bf-4de8-a997-2ec40a85ddb5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 5,
                column: "PermissionControllerGuid",
                value: new Guid("f57a0f7c-960a-41c3-9d0b-b9d522b24b58"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 6,
                column: "PermissionControllerGuid",
                value: new Guid("309ea027-67c6-4211-9ea2-54bc1e2991d9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 7,
                column: "PermissionControllerGuid",
                value: new Guid("b6c4ea2b-3b04-44d3-b4bb-f338bc212865"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 8,
                column: "PermissionControllerGuid",
                value: new Guid("c4a0aa4f-764d-4036-a9aa-69aad37ef2b4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 9,
                column: "PermissionControllerGuid",
                value: new Guid("69cf0474-3001-435d-8592-866453e95eb5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 10,
                column: "PermissionControllerGuid",
                value: new Guid("f0a18228-fe60-4092-bcb9-a5b90bcc5629"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 11,
                column: "PermissionControllerGuid",
                value: new Guid("b48c4a7f-7680-43b8-9879-8628f0d28750"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 12,
                column: "PermissionControllerGuid",
                value: new Guid("6f144e5d-962b-4f59-8b96-e510ef9193aa"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 13,
                column: "PermissionControllerGuid",
                value: new Guid("24868193-4539-401e-bfb7-e74849b07b97"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 14,
                column: "PermissionControllerGuid",
                value: new Guid("2ffc581b-c9ae-4090-88f0-2b40051a2731"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 15,
                column: "PermissionControllerGuid",
                value: new Guid("439ebc6d-ac96-418b-ba71-22715358a3b4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 16,
                column: "PermissionControllerGuid",
                value: new Guid("225241d2-a8c9-4d46-bd6c-e8414153d31f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 17,
                column: "PermissionControllerGuid",
                value: new Guid("2e3f2928-4a3f-47b6-b6f3-1182cd2c078d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 18,
                column: "PermissionControllerGuid",
                value: new Guid("7f9f0389-7a9f-48a7-84dd-67ec4c9cfd71"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 19,
                column: "PermissionControllerGuid",
                value: new Guid("4f39f63a-e7c6-4889-8912-e1fc9866dcbd"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 20,
                column: "PermissionControllerGuid",
                value: new Guid("421e778b-99ba-4acf-8f6d-6a966c0535c7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 21,
                column: "PermissionControllerGuid",
                value: new Guid("57b7345d-3b96-4b0e-8b00-256f2ca31b12"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 22,
                column: "PermissionControllerGuid",
                value: new Guid("0d51fc89-4a0f-4f8d-ac4a-23230fdd3589"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 23,
                column: "PermissionControllerGuid",
                value: new Guid("ff99cb80-7b91-4df2-95f2-44b384de4b24"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 24,
                column: "PermissionControllerGuid",
                value: new Guid("634bc694-b595-4c6a-9c88-5a0c63b5f37b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 25,
                column: "PermissionControllerGuid",
                value: new Guid("032853fd-3b20-4b16-a27f-9dee65eb0c44"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 26,
                column: "PermissionControllerGuid",
                value: new Guid("3255252f-1fe8-4e94-a957-f1084e341a9f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 27,
                column: "PermissionControllerGuid",
                value: new Guid("dfb8c171-c995-400b-98f5-f018e3c4dc86"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 28,
                column: "PermissionControllerGuid",
                value: new Guid("849293cd-0ba6-4870-bcb5-302dd787fa7e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 29,
                column: "PermissionControllerGuid",
                value: new Guid("9004b22a-4bd7-42c9-a395-78199bea0ad8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 30,
                column: "PermissionControllerGuid",
                value: new Guid("4575231a-85ac-4e4a-a088-7e9cf9214775"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 31,
                column: "PermissionControllerGuid",
                value: new Guid("8193b91a-c566-44b3-a54b-64b0b6e23b03"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 32,
                column: "PermissionControllerGuid",
                value: new Guid("837b453e-412d-417d-b463-0f5127da4cad"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 33,
                columns: new[] { "PermissionControllerGuid", "PermissionControllerNameAr" },
                values: new object[] { new Guid("ed5eca8d-5620-4b21-84a3-fd607354d591"), "الفروع" });

            migrationBuilder.InsertData(
                schema: "Permission",
                table: "PermissionController",
                columns: new[] { "PermissionControllerID", "PermissionControllerGuid", "PermissionControllerNameAr", "PermissionControllerNameEn" },
                values: new object[] { 34, new Guid("81fef68b-e540-4447-8ea4-4a506e2a8982"), "اكواد الخصم", "Promo Code" });

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 1,
                column: "PermissionControllerActionGuid",
                value: new Guid("0482f93a-440a-4c1e-9e1d-3d61c92a5567"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 2,
                column: "PermissionControllerActionGuid",
                value: new Guid("41564d92-5f19-43d8-9d36-6d06a9cff5f0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 3,
                column: "PermissionControllerActionGuid",
                value: new Guid("2f18984f-e7ae-40c4-91a6-9950cd062644"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 4,
                column: "PermissionControllerActionGuid",
                value: new Guid("b0220f29-5580-4719-b7cd-3738262662e1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 5,
                column: "PermissionControllerActionGuid",
                value: new Guid("f44d13d9-56d8-47ae-a13c-326a8f0df3a9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 6,
                column: "PermissionControllerActionGuid",
                value: new Guid("b286c113-6036-4716-bb05-7ec87f2396c3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 7,
                column: "PermissionControllerActionGuid",
                value: new Guid("5530cf07-72a4-4463-b337-220df9f57071"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 8,
                column: "PermissionControllerActionGuid",
                value: new Guid("ea17beb0-af37-4e4d-bbe0-6f79e19b8a72"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 9,
                column: "PermissionControllerActionGuid",
                value: new Guid("22f19007-2151-4bb1-9b02-fe55f070b0f3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 10,
                column: "PermissionControllerActionGuid",
                value: new Guid("c82a065e-c8c7-41f4-89bf-5dd9d15b210b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 11,
                column: "PermissionControllerActionGuid",
                value: new Guid("68cfc374-79a2-47df-84dd-f4fa70f70533"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 12,
                column: "PermissionControllerActionGuid",
                value: new Guid("fac00486-5a11-4c7a-bb92-ce415d369bbe"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 13,
                column: "PermissionControllerActionGuid",
                value: new Guid("294f7259-b659-4d9d-86ae-4c450649ca44"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 14,
                column: "PermissionControllerActionGuid",
                value: new Guid("18dfb906-f83f-4b42-a264-6411712112b9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 15,
                column: "PermissionControllerActionGuid",
                value: new Guid("a77046d3-9b82-4cb8-889c-b4385965d6bb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 16,
                column: "PermissionControllerActionGuid",
                value: new Guid("f1ad36dd-bd63-4367-b63a-6beba2d7fc18"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 17,
                column: "PermissionControllerActionGuid",
                value: new Guid("ff2563e8-51cf-4768-a242-c2c5b53cc4e4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 18,
                column: "PermissionControllerActionGuid",
                value: new Guid("75fd5975-7cd0-4e56-9940-57eec4f92d2f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 19,
                column: "PermissionControllerActionGuid",
                value: new Guid("23d45539-337b-4f23-9df7-76807bda8a98"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 20,
                column: "PermissionControllerActionGuid",
                value: new Guid("95cd3fd5-486b-4de5-909e-4d03c0dcb106"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 21,
                column: "PermissionControllerActionGuid",
                value: new Guid("290a0999-56e4-48f6-bfa4-64c3e34adb74"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 22,
                column: "PermissionControllerActionGuid",
                value: new Guid("450a6340-bc4b-45ec-b93d-8c39f89abd44"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 23,
                column: "PermissionControllerActionGuid",
                value: new Guid("a40c62f4-70ab-4732-8d5c-5f30d5a73957"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 24,
                column: "PermissionControllerActionGuid",
                value: new Guid("bc358a0e-03db-4493-b149-3a71c90e2f23"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 25,
                column: "PermissionControllerActionGuid",
                value: new Guid("2bb8d6cd-cdaa-4557-b3d6-dd1ff26ecd03"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 26,
                column: "PermissionControllerActionGuid",
                value: new Guid("ce941c46-fa90-420a-9494-049a72c51b7c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 27,
                column: "PermissionControllerActionGuid",
                value: new Guid("cfa99ef6-7f89-41fb-bca9-7a0d2d8f3436"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 28,
                column: "PermissionControllerActionGuid",
                value: new Guid("b515e90a-0b52-4e24-9648-a8ec4a9c4e60"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 29,
                column: "PermissionControllerActionGuid",
                value: new Guid("0847cb42-65ab-4816-a237-9dbc3db37b53"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 30,
                column: "PermissionControllerActionGuid",
                value: new Guid("311363fc-fdfc-4bed-8147-6341c6bf28af"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 31,
                column: "PermissionControllerActionGuid",
                value: new Guid("e12180b6-0da7-48d3-a536-d56790713d10"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 32,
                column: "PermissionControllerActionGuid",
                value: new Guid("755cc632-c1d1-478b-92fd-70d367c28a79"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 33,
                column: "PermissionControllerActionGuid",
                value: new Guid("d9fb70dc-50ff-4e2c-a25f-43ccf7b5f028"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 34,
                column: "PermissionControllerActionGuid",
                value: new Guid("fdecdbbe-c6ff-4f06-9767-797c3df78ad8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 35,
                column: "PermissionControllerActionGuid",
                value: new Guid("c1e27295-2b7f-43e6-b7da-76df2e96f99b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 36,
                column: "PermissionControllerActionGuid",
                value: new Guid("2a964ae4-f6c6-409f-b535-782f00a0905e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 37,
                column: "PermissionControllerActionGuid",
                value: new Guid("652dfd15-b185-4bec-bb3b-698fefdc21f8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 38,
                column: "PermissionControllerActionGuid",
                value: new Guid("fa0e7e98-92c3-4ff1-ba01-53d83d22c548"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 39,
                column: "PermissionControllerActionGuid",
                value: new Guid("e98cd952-33ee-49e1-bdd1-b62574ad388d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 40,
                column: "PermissionControllerActionGuid",
                value: new Guid("887bc68d-7533-4d61-aef7-4273f58b9070"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 41,
                column: "PermissionControllerActionGuid",
                value: new Guid("53f7d842-349f-4f41-8675-2bde3c1af147"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 42,
                column: "PermissionControllerActionGuid",
                value: new Guid("ddc9ce16-6972-4b8d-8f36-9254b5317304"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 43,
                column: "PermissionControllerActionGuid",
                value: new Guid("1e0d3437-7f80-410d-b2c7-bdffa2c68ffe"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 44,
                column: "PermissionControllerActionGuid",
                value: new Guid("ce15cffc-d0f1-4907-9bd2-2a6ceafc4784"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 45,
                column: "PermissionControllerActionGuid",
                value: new Guid("ee81372b-149a-4eea-ab4c-a16ea93ae2f6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 46,
                column: "PermissionControllerActionGuid",
                value: new Guid("ae5ad199-8535-40bf-bc08-87e74da0ddb7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 47,
                column: "PermissionControllerActionGuid",
                value: new Guid("1c4fc9e3-f717-498d-ae06-3afe7e14e8fe"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 48,
                column: "PermissionControllerActionGuid",
                value: new Guid("9c56a7b2-6fa9-464c-853c-8449f5014821"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 49,
                column: "PermissionControllerActionGuid",
                value: new Guid("97dc4ad5-9dba-417c-ad50-93e81603ea7f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 50,
                column: "PermissionControllerActionGuid",
                value: new Guid("d9db47a4-c4c2-453c-8f97-f0dbc7551aa7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 51,
                column: "PermissionControllerActionGuid",
                value: new Guid("c4ede3ad-9913-46bf-924b-f9b24e5bf206"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 52,
                column: "PermissionControllerActionGuid",
                value: new Guid("34051a5a-46a5-47c8-9d56-638019141798"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 53,
                column: "PermissionControllerActionGuid",
                value: new Guid("6eae4fd0-8adf-4a06-b29b-3c64dc30a803"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 54,
                column: "PermissionControllerActionGuid",
                value: new Guid("4168bfc2-1b3a-47dd-9edb-64e04911be84"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 55,
                column: "PermissionControllerActionGuid",
                value: new Guid("92e0754f-1fab-437d-a5d1-47052894f645"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 56,
                column: "PermissionControllerActionGuid",
                value: new Guid("71529095-b60c-48b6-a260-1f2aece189e8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 57,
                column: "PermissionControllerActionGuid",
                value: new Guid("2099556f-ba46-4a39-a0e7-5bb92d19e2f4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 58,
                column: "PermissionControllerActionGuid",
                value: new Guid("ebb55db8-b805-4bc8-ad5e-025835d6db43"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 59,
                column: "PermissionControllerActionGuid",
                value: new Guid("3f7f3621-53a8-4005-a0dd-e3fdaf1a9228"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 60,
                column: "PermissionControllerActionGuid",
                value: new Guid("41f2d1f1-b6da-4a97-9e7d-39f813abb616"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 61,
                column: "PermissionControllerActionGuid",
                value: new Guid("db5b2e2d-99a3-44bf-a317-a87416d7bc12"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 62,
                column: "PermissionControllerActionGuid",
                value: new Guid("6c83132c-a28e-4426-b4ba-c2a6323b9952"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 63,
                column: "PermissionControllerActionGuid",
                value: new Guid("82efe58a-d981-4350-a155-57d1d6d6ca2e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 64,
                column: "PermissionControllerActionGuid",
                value: new Guid("21000d0e-0e9d-4d1b-919e-4c22793433ca"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 65,
                column: "PermissionControllerActionGuid",
                value: new Guid("2aa76769-4945-468c-bc07-9d993c607c7a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 66,
                column: "PermissionControllerActionGuid",
                value: new Guid("032d14b9-921f-455b-b33b-bb30165a5489"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 67,
                column: "PermissionControllerActionGuid",
                value: new Guid("09418377-875f-476e-aeeb-5edebdc3a86f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 68,
                column: "PermissionControllerActionGuid",
                value: new Guid("ae0cae4d-c200-4894-9d94-c80ab66dbd0f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 69,
                column: "PermissionControllerActionGuid",
                value: new Guid("89f72c6e-43ff-4705-a54a-52e56f3a01ce"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 70,
                column: "PermissionControllerActionGuid",
                value: new Guid("b8e1d77a-1d27-41b0-bb67-cd8a56ca24e9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 71,
                column: "PermissionControllerActionGuid",
                value: new Guid("bb5b3b9d-6580-4ca1-9066-a114863f5e6c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 72,
                column: "PermissionControllerActionGuid",
                value: new Guid("d5739d81-110f-4aaa-a39b-d708100bb899"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 73,
                column: "PermissionControllerActionGuid",
                value: new Guid("b740afe9-8267-42c9-aa2c-84b9a18afebd"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 74,
                column: "PermissionControllerActionGuid",
                value: new Guid("1d1f8abd-b885-4d9e-a5d6-2bb051a8403d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 75,
                column: "PermissionControllerActionGuid",
                value: new Guid("884ecbbd-7bd9-4beb-8878-acefc7afc374"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 76,
                column: "PermissionControllerActionGuid",
                value: new Guid("7f551427-71ab-49d3-8d9b-9405507c6196"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 77,
                column: "PermissionControllerActionGuid",
                value: new Guid("f69d7983-29cb-48c5-a872-00990c648f5e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 78,
                column: "PermissionControllerActionGuid",
                value: new Guid("8a559fff-7130-4033-a76a-b0181d982739"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 79,
                column: "PermissionControllerActionGuid",
                value: new Guid("aea07a76-8c65-4c55-a3cb-c9ed3131617c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 80,
                column: "PermissionControllerActionGuid",
                value: new Guid("2ddbcc52-e221-441b-901c-e5bbd9bedd6a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 81,
                column: "PermissionControllerActionGuid",
                value: new Guid("685d93ad-75c6-4e56-ac29-42bb22a02f4a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 82,
                column: "PermissionControllerActionGuid",
                value: new Guid("e314f858-3559-4657-9ecf-61eada154394"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 83,
                column: "PermissionControllerActionGuid",
                value: new Guid("36ce30ac-04c7-41b3-9f06-6754658b9ce6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 84,
                column: "PermissionControllerActionGuid",
                value: new Guid("950ac769-eb3d-4618-a670-c63aa16539bf"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 85,
                column: "PermissionControllerActionGuid",
                value: new Guid("7a9077d2-f7ee-4906-915f-89dd155adcf7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 86,
                column: "PermissionControllerActionGuid",
                value: new Guid("63eaa01f-192c-4f73-8e28-fc13fdc8b3ef"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 87,
                column: "PermissionControllerActionGuid",
                value: new Guid("e224e7e0-a4a8-4614-80d6-ae6ddbd2e5a9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 88,
                column: "PermissionControllerActionGuid",
                value: new Guid("5d9f8d70-7ab9-4539-b657-31f8da6a2cb8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 89,
                column: "PermissionControllerActionGuid",
                value: new Guid("4956f1a8-b0e1-4699-914f-1fd2d4e99234"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 90,
                column: "PermissionControllerActionGuid",
                value: new Guid("caba12ac-0c30-41b3-bd2b-4297f2174f63"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 91,
                column: "PermissionControllerActionGuid",
                value: new Guid("020801e4-6fe7-4e6b-80b4-db7cbee922d9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 92,
                column: "PermissionControllerActionGuid",
                value: new Guid("80cddb79-7d23-4dee-b286-ddb7612cad3c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 93,
                column: "PermissionControllerActionGuid",
                value: new Guid("928796af-8506-4e5f-bd28-e20e582fb27e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 94,
                column: "PermissionControllerActionGuid",
                value: new Guid("6f3a8eaa-42ab-4e29-b7b2-8764e7414e0c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 95,
                column: "PermissionControllerActionGuid",
                value: new Guid("71db870f-6ee0-44f0-98b9-d958a003d96e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 96,
                column: "PermissionControllerActionGuid",
                value: new Guid("ef302b1d-8b6c-4fdf-8f39-9c6e0bd4ce92"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 97,
                column: "PermissionControllerActionGuid",
                value: new Guid("d47300be-a474-4f82-9697-a31e1b5f814a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 98,
                column: "PermissionControllerActionGuid",
                value: new Guid("b96b3fcf-f162-4e1f-a05e-764a168e5f2f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 99,
                column: "PermissionControllerActionGuid",
                value: new Guid("aa1d6078-8312-4a80-b15c-693e9026bfad"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 100,
                column: "PermissionControllerActionGuid",
                value: new Guid("3eb4d4c5-700d-4688-932f-39db4225acde"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 101,
                column: "PermissionControllerActionGuid",
                value: new Guid("1090d7d3-f62f-486b-ae84-422cecb5964e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 102,
                column: "PermissionControllerActionGuid",
                value: new Guid("b18a10d4-f972-41bc-afc3-994e66e3e3df"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 103,
                column: "PermissionControllerActionGuid",
                value: new Guid("00df99eb-f727-44b3-b81f-dd9a656501db"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 104,
                column: "PermissionControllerActionGuid",
                value: new Guid("e05111dd-0d16-4b18-8dd0-ea5a141836c2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 105,
                column: "PermissionControllerActionGuid",
                value: new Guid("a1092557-ea31-4f59-afc1-4d5709332a3d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 106,
                column: "PermissionControllerActionGuid",
                value: new Guid("5bc00551-0199-4837-996a-da1c5ef0f48f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 107,
                column: "PermissionControllerActionGuid",
                value: new Guid("e848cd47-a58a-4ca9-89b9-8588aaac3efa"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 108,
                column: "PermissionControllerActionGuid",
                value: new Guid("3b0e1878-3641-4a07-b2bd-318439d403ee"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 109,
                column: "PermissionControllerActionGuid",
                value: new Guid("8ef8bdb5-2a71-47fe-9ead-2fd97f156068"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 110,
                column: "PermissionControllerActionGuid",
                value: new Guid("55d6953f-4886-4a3f-9b27-32e79a13edad"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 111,
                column: "PermissionControllerActionGuid",
                value: new Guid("399bba49-f715-44a3-9e02-c5f972f34f70"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 112,
                column: "PermissionControllerActionGuid",
                value: new Guid("be6e4f77-9a68-46df-833c-82949d38ad0c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 113,
                column: "PermissionControllerActionGuid",
                value: new Guid("d888b22c-cf07-4a3f-8a43-aa359cb9bc6a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 114,
                column: "PermissionControllerActionGuid",
                value: new Guid("3d6b3b88-d425-4a07-9260-17351b8dcb57"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 115,
                column: "PermissionControllerActionGuid",
                value: new Guid("5d206d5a-93e5-4351-91d1-863eb79e2667"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 116,
                column: "PermissionControllerActionGuid",
                value: new Guid("cd54aafe-f866-4aed-8f75-0d68ffcd9267"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 117,
                column: "PermissionControllerActionGuid",
                value: new Guid("52b899ad-abe9-4a75-86e3-97f42ca3ce90"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 118,
                column: "PermissionControllerActionGuid",
                value: new Guid("ebbe0908-da79-4aeb-9a35-7b514046f5aa"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 119,
                column: "PermissionControllerActionGuid",
                value: new Guid("d1259f7a-5dbf-4e8e-a2c2-0740626397d4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 120,
                column: "PermissionControllerActionGuid",
                value: new Guid("61ae6de9-e2c1-493d-9f33-9086057da83a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 121,
                column: "PermissionControllerActionGuid",
                value: new Guid("ed699ce4-3793-49a2-88f7-172523b1fece"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 122,
                column: "PermissionControllerActionGuid",
                value: new Guid("45480e35-c680-474b-8521-7d22e88bf949"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 123,
                column: "PermissionControllerActionGuid",
                value: new Guid("095c2ba3-80ce-4f88-aa94-5757be36da5d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 124,
                column: "PermissionControllerActionGuid",
                value: new Guid("11007db5-3d4a-4959-b6e1-ae89d2e9921f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 125,
                column: "PermissionControllerActionGuid",
                value: new Guid("cc44b92c-53a6-4f16-b656-65d65344663b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 126,
                column: "PermissionControllerActionGuid",
                value: new Guid("c4a5930b-b22b-44b3-a5a9-d0a4e30eb424"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 127,
                column: "PermissionControllerActionGuid",
                value: new Guid("13590c4f-b6fc-4026-9128-2c3d4bfa8479"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 128,
                column: "PermissionControllerActionGuid",
                value: new Guid("8904357e-53d3-4d03-b661-685588964a51"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 129,
                column: "PermissionControllerActionGuid",
                value: new Guid("56326186-9ba9-49d5-9d23-e51754c28d40"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 130,
                column: "PermissionControllerActionGuid",
                value: new Guid("8262952a-4e4f-4bbc-8f0b-f5673be9243c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 131,
                column: "PermissionControllerActionGuid",
                value: new Guid("0e51a7b5-47f5-458f-9407-01bc77d9d673"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 132,
                column: "PermissionControllerActionGuid",
                value: new Guid("b53ecb2c-1b94-40b5-8153-eb10422fac1c"));

            migrationBuilder.InsertData(
                schema: "Permission",
                table: "PermissionControllerAction",
                columns: new[] { "PermissionControllerActionID", "PermissionActionID", "PermissionControllerActionGuid", "PermissionControllerID" },
                values: new object[,]
                {
                    { 136, 4, new Guid("a8e642e5-4767-43d6-85b4-bec75f22acb3"), 34 },
                    { 135, 3, new Guid("631ac72a-57f9-4ca7-9f38-12c78a145992"), 34 },
                    { 134, 2, new Guid("7e643e9b-a786-47c9-9e90-0cbbf9be014d"), 34 },
                    { 133, 1, new Guid("4d151dd2-a262-4312-9786-dd2e6771bd7e"), 34 }
                });

            //migrationBuilder.InsertData(
            //    schema: "Permission",
            //    table: "RoleConfig",
            //    columns: new[] { "RoleConfigID", "PermissionControllerActionID", "RoleId" },
            //    values: new object[,]
            //    {
            //        { 1, 1, 1 },
            //        { 88, 88, 1 },
            //        { 89, 89, 1 },
            //        { 90, 90, 1 },
            //        { 91, 91, 1 },
            //        { 92, 92, 1 },
            //        { 93, 93, 1 },
            //        { 95, 95, 1 },
            //        { 87, 87, 1 },
            //        { 96, 96, 1 },
            //        { 97, 97, 1 },
            //        { 98, 98, 1 },
            //        { 99, 99, 1 },
            //        { 94, 94, 1 },
            //        { 86, 86, 1 },
            //        { 84, 84, 1 },
            //        { 100, 100, 1 },
            //        { 83, 83, 1 },
            //        { 82, 82, 1 },
            //        { 81, 81, 1 },
            //        { 80, 80, 1 },
            //        { 79, 79, 1 },
            //        { 78, 78, 1 },
            //        { 77, 77, 1 },
            //        { 76, 76, 1 },
            //        { 75, 75, 1 },
            //        { 74, 74, 1 },
            //        { 73, 73, 1 },
            //        { 72, 72, 1 },
            //        { 85, 85, 1 },
            //        { 101, 101, 1 },
            //        { 104, 104, 1 },
            //        { 103, 103, 1 },
            //        { 128, 128, 2 },
            //        { 127, 127, 2 },
            //        { 126, 126, 2 },
            //        { 125, 125, 2 },
            //        { 108, 108, 2 }
            //    });

            //migrationBuilder.InsertData(
            //    schema: "Permission",
            //    table: "RoleConfig",
            //    columns: new[] { "RoleConfigID", "PermissionControllerActionID", "RoleId" },
            //    values: new object[,]
            //    {
            //        { 107, 107, 2 },
            //        { 106, 106, 2 },
            //        { 105, 105, 2 },
            //        { 132, 132, 1 },
            //        { 131, 131, 1 },
            //        { 130, 130, 1 },
            //        { 129, 129, 1 },
            //        { 124, 124, 1 },
            //        { 123, 123, 1 },
            //        { 122, 122, 1 },
            //        { 121, 121, 1 },
            //        { 120, 120, 1 },
            //        { 119, 119, 1 },
            //        { 118, 118, 1 },
            //        { 117, 117, 1 },
            //        { 116, 116, 1 },
            //        { 115, 115, 1 },
            //        { 114, 114, 1 },
            //        { 113, 113, 1 },
            //        { 112, 112, 1 },
            //        { 111, 111, 1 },
            //        { 110, 110, 1 },
            //        { 109, 109, 1 },
            //        { 71, 71, 1 },
            //        { 102, 102, 1 },
            //        { 69, 69, 1 },
            //        { 70, 70, 1 },
            //        { 33, 33, 1 },
            //        { 31, 31, 1 },
            //        { 30, 30, 1 },
            //        { 29, 29, 1 },
            //        { 28, 28, 1 },
            //        { 27, 27, 1 },
            //        { 26, 26, 1 },
            //        { 25, 25, 1 },
            //        { 24, 24, 1 },
            //        { 23, 23, 1 },
            //        { 22, 22, 1 },
            //        { 21, 21, 1 },
            //        { 20, 20, 1 },
            //        { 19, 19, 1 },
            //        { 18, 18, 1 }
            //    });

            //migrationBuilder.InsertData(
            //    schema: "Permission",
            //    table: "RoleConfig",
            //    columns: new[] { "RoleConfigID", "PermissionControllerActionID", "RoleId" },
            //    values: new object[,]
            //    {
            //        { 32, 32, 1 },
            //        { 17, 17, 1 },
            //        { 15, 15, 1 },
            //        { 14, 14, 1 },
            //        { 13, 13, 1 },
            //        { 12, 12, 1 },
            //        { 11, 11, 1 },
            //        { 10, 10, 1 },
            //        { 9, 9, 1 },
            //        { 8, 8, 1 },
            //        { 7, 7, 1 },
            //        { 6, 6, 1 },
            //        { 5, 5, 1 },
            //        { 4, 4, 1 },
            //        { 3, 3, 1 },
            //        { 2, 2, 1 },
            //        { 16, 16, 1 },
            //        { 68, 68, 1 },
            //        { 34, 34, 1 },
            //        { 52, 52, 1 },
            //        { 65, 65, 1 },
            //        { 64, 64, 1 },
            //        { 63, 63, 1 },
            //        { 62, 62, 1 },
            //        { 61, 61, 1 },
            //        { 60, 60, 1 },
            //        { 59, 59, 1 },
            //        { 58, 58, 1 },
            //        { 57, 57, 1 },
            //        { 56, 56, 1 },
            //        { 55, 55, 1 },
            //        { 54, 54, 1 },
            //        { 53, 53, 1 },
            //        { 35, 35, 1 },
            //        { 66, 66, 1 },
            //        { 51, 51, 1 },
            //        { 49, 49, 1 },
            //        { 48, 48, 1 },
            //        { 47, 47, 1 },
            //        { 46, 46, 1 },
            //        { 45, 45, 1 },
            //        { 44, 44, 1 }
            //    });

            //migrationBuilder.InsertData(
            //    schema: "Permission",
            //    table: "RoleConfig",
            //    columns: new[] { "RoleConfigID", "PermissionControllerActionID", "RoleId" },
            //    values: new object[,]
            //    {
            //        { 43, 43, 1 },
            //        { 42, 42, 1 },
            //        { 41, 41, 1 },
            //        { 40, 40, 1 },
            //        { 39, 39, 1 },
            //        { 38, 38, 1 },
            //        { 37, 37, 1 },
            //        { 36, 36, 1 },
            //        { 50, 50, 1 },
            //        { 67, 67, 1 }
            //    });

            //migrationBuilder.InsertData(
            //    schema: "Setting",
            //    table: "Country",
            //    columns: new[] { "CountryID", "CountryGuid", "CreateDate", "DeleteDate", "EnableDate", "Extension", "Flag", "IsDeleted", "IsEnable", "Lat", "Long", "NameAR", "NameEN", "Place", "UpdateDate", "UserDelete", "UserEnable", "UserId", "UserUpdate", "Zoom" },
            //    values: new object[] { 1, new Guid("8ad7e9fd-9d4d-4249-8160-5bbe1395c264"), new DateTime(2021, 9, 19, 18, 31, 43, 162, DateTimeKind.Local).AddTicks(1837), null, new DateTime(2021, 9, 19, 18, 31, 43, 163, DateTimeKind.Local).AddTicks(5278), "00966", null, false, true, "", "", "السعودية", "SA", "", null, null, null, 1, null, null });

            //migrationBuilder.InsertData(
            //    schema: "Setting",
            //    table: "Jobs",
            //    columns: new[] { "JobsID", "CreateDate", "DeleteDate", "DescriptionAr", "DescriptionEn", "EnableDate", "Image", "IsDeleted", "IsEnable", "JobTypeId", "JobsGuid", "NameAR", "NameEN", "UpdateDate", "UserDelete", "UserEnable", "UserId", "UserUpdate" },
            //    values: new object[] { 1, new DateTime(2021, 9, 19, 18, 31, 43, 167, DateTimeKind.Local).AddTicks(3319), null, null, null, new DateTime(2021, 9, 19, 18, 31, 43, 167, DateTimeKind.Local).AddTicks(4373), null, false, true, 2, new Guid("b86c44bb-2133-44ea-b23b-18c4669bfa6c"), "الدمام", "DMM", null, null, null, 1, null });

            //migrationBuilder.InsertData(
            //    schema: "Setting",
            //    table: "MainCategory",
            //    columns: new[] { "MainCategoryID", "CreateDate", "DeleteDate", "EnableDate", "IsDeleted", "IsEnable", "MainCategoryGuid", "NameAR", "NameEN", "UpdateDate", "UserDelete", "UserEnable", "UserId", "UserUpdate" },
            //    values: new object[] { 1, new DateTime(2021, 9, 19, 18, 31, 43, 167, DateTimeKind.Local).AddTicks(9581), null, new DateTime(2021, 9, 19, 18, 31, 43, 168, DateTimeKind.Local).AddTicks(637), false, true, new Guid("b6906345-9954-475b-a10e-7bdcd99154e5"), "الدمام", "DMM", null, null, null, 1, null });

            //migrationBuilder.InsertData(
            //    schema: "Setting",
            //    table: "Nationality",
            //    columns: new[] { "NationalityID", "CreateDate", "DeleteDate", "DescriptionAr", "DescriptionEn", "EnableDate", "IsDeleted", "IsEnable", "NameAR", "NameEN", "NationalityGuid", "UpdateDate", "UserDelete", "UserEnable", "UserId", "UserUpdate" },
            //    values: new object[] { 1, new DateTime(2021, 9, 19, 18, 31, 43, 166, DateTimeKind.Local).AddTicks(6967), null, null, null, new DateTime(2021, 9, 19, 18, 31, 43, 166, DateTimeKind.Local).AddTicks(8030), false, true, "الدمام", "DMM", new Guid("f40be95e-307e-409a-8417-56ff3ba988db"), null, null, null, 1, null });

            //migrationBuilder.InsertData(
            //    schema: "Setting",
            //    table: "Departments",
            //    columns: new[] { "DepartmentsID", "Arrange", "CreateDate", "DeleteDate", "DepartmentsGuid", "DescriptionAr", "DescriptionEn", "EnableDate", "Image", "IsDeleted", "IsEnable", "Isunique", "MainCategoryID", "NameAR", "NameEN", "SiteImage", "UpdateDate", "UserDelete", "UserEnable", "UserId", "UserUpdate" },
            //    values: new object[] { 1, null, new DateTime(2021, 9, 19, 18, 31, 43, 168, DateTimeKind.Local).AddTicks(7157), null, new Guid("3a60ac3f-e7c9-418f-9a26-80f0887bb620"), null, null, new DateTime(2021, 9, 19, 18, 31, 43, 168, DateTimeKind.Local).AddTicks(8217), null, false, true, false, 1, "الدمام", "DMM", null, null, null, null, 1, null });

            //migrationBuilder.InsertData(
            //    schema: "Setting",
            //    table: "Region",
            //    columns: new[] { "RegionID", "CountryID", "CreateDate", "DeleteDate", "EnableDate", "IsDeleted", "IsEnable", "Lat", "Long", "NameAR", "NameEN", "Place", "RegionGuid", "UpdateDate", "UserDelete", "UserEnable", "UserId", "UserUpdate", "Zoom" },
            //    values: new object[] { 1, 1, new DateTime(2021, 9, 19, 18, 31, 43, 164, DateTimeKind.Local).AddTicks(4595), null, new DateTime(2021, 9, 19, 18, 31, 43, 164, DateTimeKind.Local).AddTicks(5687), false, true, "", "", "الدمام", "DMM", "", new Guid("83c20a69-70b8-4f7f-b067-88c9c58a81b5"), null, null, null, 1, null, null });

            //migrationBuilder.InsertData(
            //    schema: "Setting",
            //    table: "City",
            //    columns: new[] { "CityID", "CityGuid", "Code", "CreateDate", "DeleteDate", "EnableDate", "IsDeleted", "IsEnable", "Lat", "Long", "NameAR", "NameEN", "Place", "RegionID", "UpdateDate", "UserDelete", "UserEnable", "UserId", "UserUpdate", "Zoom" },
            //    values: new object[] { 1, new Guid("2d43aae1-4ed2-41e5-a012-5dfbd4ad9f2f"), null, new DateTime(2021, 9, 19, 18, 31, 43, 165, DateTimeKind.Local).AddTicks(7667), null, new DateTime(2021, 9, 19, 18, 31, 43, 165, DateTimeKind.Local).AddTicks(8709), false, true, "", "", "الدمام", "DMM", "", 1, null, null, null, 1, null, null });

            //migrationBuilder.InsertData(
            //    schema: "Emp",
            //    table: "Employees",
            //    columns: new[] { "EntityEmpID", "AddressAR", "AddressEN", "BirthDateHijri", "BirthDateMilady", "BloodTypeId", "CityID", "CreateDate", "DeleteDate", "Email", "EnableDate", "EntityEmpGuid", "FileNo", "FirstNameAR", "FirstNameEN", "Gender", "IDNo", "IsDeleted", "IsEnable", "JobsID", "LastNameAR", "LastNameEN", "Lat", "Lng", "MidNameAR", "MidNameEN", "MobileNo", "NationalityID", "Notes", "PhoneNumber", "Photo", "UpdateDate", "UserDelete", "UserEnable", "UserId", "UserUpdate", "Zoom" },
            //    values: new object[] { 1, "الاسماعيليه", "ismailia", "", null, 1, 1, new DateTime(2021, 9, 19, 18, 31, 43, 170, DateTimeKind.Local).AddTicks(987), null, "SystemUser@Admin.com", new DateTime(2021, 9, 19, 18, 31, 43, 170, DateTimeKind.Local).AddTicks(1920), new Guid("2299447c-fc61-4aa4-ba03-8c91e4f4b2d5"), "123321", "احمد", "Ahmed", 1, "", false, true, 1, "حسين", "Hussien", "", "", "سيد", "Sayed", "0595489633", 1, "", "", "", null, null, 1, 1, null, "" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "Emp",
                table: "Employees",
                keyColumn: "EntityEmpID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 133);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 134);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 135);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 136);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 11);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 12);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 13);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 14);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 15);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 16);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 17);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 18);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 19);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 20);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 21);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 22);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 23);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 24);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 25);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 26);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 27);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 28);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 29);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 30);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 31);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 32);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 33);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 34);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 35);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 36);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 37);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 38);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 39);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 40);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 41);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 42);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 43);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 44);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 45);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 46);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 47);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 48);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 49);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 50);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 51);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 52);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 53);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 54);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 55);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 56);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 57);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 58);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 59);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 60);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 61);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 62);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 63);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 64);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 65);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 66);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 67);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 68);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 69);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 70);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 71);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 72);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 73);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 74);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 75);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 76);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 77);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 78);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 79);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 80);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 81);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 82);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 83);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 84);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 85);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 86);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 87);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 88);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 89);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 90);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 91);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 92);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 93);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 94);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 95);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 96);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 97);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 98);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 99);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 100);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 101);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 102);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 103);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 104);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 105);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 106);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 107);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 108);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 109);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 110);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 111);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 112);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 113);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 114);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 115);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 116);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 117);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 118);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 119);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 120);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 121);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 122);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 123);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 124);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 125);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 126);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 127);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 128);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 129);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 130);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 131);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "RoleConfig",
                keyColumn: "RoleConfigID",
                keyValue: 132);

            migrationBuilder.DeleteData(
                schema: "Setting",
                table: "Departments",
                keyColumn: "DepartmentsID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionAction",
                keyColumn: "PermissionActionID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionAction",
                keyColumn: "PermissionActionID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionAction",
                keyColumn: "PermissionActionID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionAction",
                keyColumn: "PermissionActionID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 34);

            migrationBuilder.DeleteData(
                schema: "Setting",
                table: "City",
                keyColumn: "CityID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "Setting",
                table: "Jobs",
                keyColumn: "JobsID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "Setting",
                table: "MainCategory",
                keyColumn: "MainCategoryID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "Setting",
                table: "Nationality",
                keyColumn: "NationalityID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "Setting",
                table: "Region",
                keyColumn: "RegionID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "Setting",
                table: "Country",
                keyColumn: "CountryID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "MonthlyTarget",
                schema: "Vendor",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "PromoType",
                schema: "Order",
                table: "PromoCode");

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 1,
                column: "PermissionControllerGuid",
                value: new Guid("8f0abeaa-2f80-4763-a2a9-80c3d18bcb53"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 2,
                column: "PermissionControllerGuid",
                value: new Guid("25ca2a09-8d84-4170-a842-97281ff5dd50"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 3,
                column: "PermissionControllerGuid",
                value: new Guid("de7daed1-55f3-48be-a6c0-24a56b9aaba3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 4,
                column: "PermissionControllerGuid",
                value: new Guid("42d63658-3b9e-4efd-a947-275b16fbb774"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 5,
                column: "PermissionControllerGuid",
                value: new Guid("59bae0a1-8389-4ae1-856f-1649919707a3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 6,
                column: "PermissionControllerGuid",
                value: new Guid("ef4fca39-05e4-43f8-b9d7-93d19c935a5e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 7,
                column: "PermissionControllerGuid",
                value: new Guid("6e4db82f-b6cd-4d47-856e-95759571f54a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 8,
                column: "PermissionControllerGuid",
                value: new Guid("f1049b45-18c4-4c5e-ae73-c5bb3a6694ac"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 9,
                column: "PermissionControllerGuid",
                value: new Guid("b4b98893-6696-43bb-9bde-c4b3aad93e8f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 10,
                column: "PermissionControllerGuid",
                value: new Guid("904aaea9-79f0-45dc-8599-7300e3747140"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 11,
                column: "PermissionControllerGuid",
                value: new Guid("7ed9c942-3394-421b-ad28-ee6128cb06bc"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 12,
                column: "PermissionControllerGuid",
                value: new Guid("7e6cf9e0-9f40-462e-a99f-c0dd7c27422b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 13,
                column: "PermissionControllerGuid",
                value: new Guid("a895429a-3560-40ce-8079-3a046d31d671"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 14,
                column: "PermissionControllerGuid",
                value: new Guid("1b187291-e759-4b5c-89ec-8e884beb3e73"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 15,
                column: "PermissionControllerGuid",
                value: new Guid("41334d9b-a6aa-493a-95b9-b02c63ebf390"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 16,
                column: "PermissionControllerGuid",
                value: new Guid("b745e613-71f1-4899-99d6-22bad0e1538d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 17,
                column: "PermissionControllerGuid",
                value: new Guid("24519660-a4d3-464a-baac-276b92c33366"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 18,
                column: "PermissionControllerGuid",
                value: new Guid("0e4ec24b-6a29-47b8-b470-c296d129fe5a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 19,
                column: "PermissionControllerGuid",
                value: new Guid("48e20f19-fe95-4b09-8a7c-b48317b59b25"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 20,
                column: "PermissionControllerGuid",
                value: new Guid("8f42d4a1-af61-439b-9a20-84ac349be1f4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 21,
                column: "PermissionControllerGuid",
                value: new Guid("86f3e964-4c84-4996-9e60-d1edf68cbfb9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 22,
                column: "PermissionControllerGuid",
                value: new Guid("28fddc30-5b59-4a62-b13a-462a629a1c2f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 23,
                column: "PermissionControllerGuid",
                value: new Guid("af0ba1de-7c9c-4869-8a31-6021f8dce06f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 24,
                column: "PermissionControllerGuid",
                value: new Guid("365e5ccc-1a2b-480b-ab1e-c350a4ba9faa"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 25,
                column: "PermissionControllerGuid",
                value: new Guid("d840055b-e48f-4381-95c2-c67b522b9a5d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 26,
                column: "PermissionControllerGuid",
                value: new Guid("9292830d-5cec-4129-99d0-e03f1e4b4226"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 27,
                column: "PermissionControllerGuid",
                value: new Guid("285d9c74-4dd6-4b08-9b8b-9017b57b3c35"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 28,
                column: "PermissionControllerGuid",
                value: new Guid("530b566b-f202-44f5-8317-b7812d08c0e1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 29,
                column: "PermissionControllerGuid",
                value: new Guid("6aa33f18-9ff5-4db4-99ad-62f04afc3122"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 30,
                column: "PermissionControllerGuid",
                value: new Guid("a4d9228e-f52d-4f30-808f-55cb653a3a2d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 31,
                column: "PermissionControllerGuid",
                value: new Guid("46d3214c-7678-4b9c-b128-fe0740a1ea42"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 32,
                column: "PermissionControllerGuid",
                value: new Guid("84c86c59-7b53-416b-a4e8-e5825181095a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 33,
                columns: new[] { "PermissionControllerGuid", "PermissionControllerNameAr" },
                values: new object[] { new Guid("221a5a9e-0732-4f14-a145-fb0b22e6221d"), "الافرع" });

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 1,
                column: "PermissionControllerActionGuid",
                value: new Guid("a0ff9915-e72d-470f-9e0f-612cbe9d3d9a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 2,
                column: "PermissionControllerActionGuid",
                value: new Guid("4d1f9c13-6936-4d38-a1c6-b79507516acf"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 3,
                column: "PermissionControllerActionGuid",
                value: new Guid("b91c62ec-8470-4e1a-805d-57e00337660b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 4,
                column: "PermissionControllerActionGuid",
                value: new Guid("43f0900f-6e70-46ab-a583-ce11bcda99a5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 5,
                column: "PermissionControllerActionGuid",
                value: new Guid("aa461476-2afe-4a1b-9e56-18c3c213e202"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 6,
                column: "PermissionControllerActionGuid",
                value: new Guid("fab26173-c9e8-4b9e-88b2-d0889de7bf06"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 7,
                column: "PermissionControllerActionGuid",
                value: new Guid("e374ea9e-8466-4dd6-9c07-fd0a0dc0785f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 8,
                column: "PermissionControllerActionGuid",
                value: new Guid("3f4a53bb-c17b-4c83-992c-220f0efde4ca"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 9,
                column: "PermissionControllerActionGuid",
                value: new Guid("60b9ce3d-3f7d-4111-8137-18403fa106d9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 10,
                column: "PermissionControllerActionGuid",
                value: new Guid("da70a931-15d3-4fd8-8635-d6718495457e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 11,
                column: "PermissionControllerActionGuid",
                value: new Guid("b0cb1b1b-a001-40d3-a7de-42ea7fbba8ec"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 12,
                column: "PermissionControllerActionGuid",
                value: new Guid("e1da010f-6522-4265-b3e3-b793690b012b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 13,
                column: "PermissionControllerActionGuid",
                value: new Guid("fe1c5f62-1647-48b0-a29f-046c843e18f4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 14,
                column: "PermissionControllerActionGuid",
                value: new Guid("a6b0e03b-febf-471e-9015-fe59d32a5d1a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 15,
                column: "PermissionControllerActionGuid",
                value: new Guid("37ebf2e5-e547-4c34-aff3-ed9bd8ce82fc"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 16,
                column: "PermissionControllerActionGuid",
                value: new Guid("6d773687-fb68-4876-9943-7c830baa4b6d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 17,
                column: "PermissionControllerActionGuid",
                value: new Guid("ae246bbd-3c90-4a80-81da-4a7f1e641619"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 18,
                column: "PermissionControllerActionGuid",
                value: new Guid("0d277abe-5d05-42bd-a4ee-5d2c81267c8f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 19,
                column: "PermissionControllerActionGuid",
                value: new Guid("aa4b2ce2-884a-40d9-a479-4b9a0e044be2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 20,
                column: "PermissionControllerActionGuid",
                value: new Guid("125e1de8-79ae-4045-be18-0bc0f612b5d8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 21,
                column: "PermissionControllerActionGuid",
                value: new Guid("dbd3d9e4-e792-49fa-99f2-f91ae22ab694"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 22,
                column: "PermissionControllerActionGuid",
                value: new Guid("fa001cfa-9260-4338-a254-bff1a9395db2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 23,
                column: "PermissionControllerActionGuid",
                value: new Guid("e82846dc-37c8-438c-963d-00c9388c55d5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 24,
                column: "PermissionControllerActionGuid",
                value: new Guid("7e1e944f-030c-43d8-94fb-cadc00a0025f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 25,
                column: "PermissionControllerActionGuid",
                value: new Guid("ce1a4013-aa2b-4c76-ac90-53d0375eb4b4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 26,
                column: "PermissionControllerActionGuid",
                value: new Guid("0289faa4-f322-4db1-822e-592fea8c6465"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 27,
                column: "PermissionControllerActionGuid",
                value: new Guid("58cf193a-2db0-4ec4-8a6f-23610cc5e837"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 28,
                column: "PermissionControllerActionGuid",
                value: new Guid("34f82e9b-952f-4244-812d-717ced8ebc3d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 29,
                column: "PermissionControllerActionGuid",
                value: new Guid("bd9e9d8e-a6dc-42ba-aee1-5a46d371b346"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 30,
                column: "PermissionControllerActionGuid",
                value: new Guid("5e73733e-3af7-4fb5-8fd1-c8e0797a0b62"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 31,
                column: "PermissionControllerActionGuid",
                value: new Guid("bb7489a1-900f-4e0e-9c92-151f554745c3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 32,
                column: "PermissionControllerActionGuid",
                value: new Guid("55aa2f91-7991-4748-aa26-7846a8daa263"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 33,
                column: "PermissionControllerActionGuid",
                value: new Guid("2e0cb0b9-d681-48ee-8836-2638fe76c434"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 34,
                column: "PermissionControllerActionGuid",
                value: new Guid("0a10efc0-3a8e-41b1-9cdf-6e6bf6121f6b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 35,
                column: "PermissionControllerActionGuid",
                value: new Guid("fb5b7cdd-1b7b-489a-9ffc-800cbf8e807a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 36,
                column: "PermissionControllerActionGuid",
                value: new Guid("594ef8d9-5617-49d6-bb3d-7a98fa96817f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 37,
                column: "PermissionControllerActionGuid",
                value: new Guid("2e7b1217-ad7f-4fc6-b648-9e6021276121"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 38,
                column: "PermissionControllerActionGuid",
                value: new Guid("6e165965-5564-4eab-a3e1-d6b0c71dabf2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 39,
                column: "PermissionControllerActionGuid",
                value: new Guid("4467ff14-c65e-4bbe-87fd-cd480dc4dd7f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 40,
                column: "PermissionControllerActionGuid",
                value: new Guid("9f214cc1-3373-4df1-9111-067333da8027"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 41,
                column: "PermissionControllerActionGuid",
                value: new Guid("2dccd7ba-b595-4aff-bab3-27984d050a3f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 42,
                column: "PermissionControllerActionGuid",
                value: new Guid("4d3d6ab2-1796-4d52-a06a-51d5fbf16fc1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 43,
                column: "PermissionControllerActionGuid",
                value: new Guid("6b1f9418-1a5e-4d27-a241-7746de0b80ab"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 44,
                column: "PermissionControllerActionGuid",
                value: new Guid("4d2be2ae-698e-449f-8e94-c6b063846850"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 45,
                column: "PermissionControllerActionGuid",
                value: new Guid("aca2a4b1-fc35-4990-acf7-3c800ef0bdcd"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 46,
                column: "PermissionControllerActionGuid",
                value: new Guid("4cc7c235-33d5-43d0-9ca5-1c2a29817969"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 47,
                column: "PermissionControllerActionGuid",
                value: new Guid("aa7d91cd-2c64-4ccc-970f-ed4b1f4374a8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 48,
                column: "PermissionControllerActionGuid",
                value: new Guid("5306b0ff-07b4-42aa-a772-d6d149c4d1e7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 49,
                column: "PermissionControllerActionGuid",
                value: new Guid("ac94bc13-225d-4231-b9e1-2a62c33511ce"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 50,
                column: "PermissionControllerActionGuid",
                value: new Guid("2ee2edb1-672d-40f8-a137-875859ab46a7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 51,
                column: "PermissionControllerActionGuid",
                value: new Guid("33e9e010-92c4-4d3b-aaab-e1d4b21aa256"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 52,
                column: "PermissionControllerActionGuid",
                value: new Guid("25132ad9-d44a-44ae-954a-c893c0bcb6a3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 53,
                column: "PermissionControllerActionGuid",
                value: new Guid("a70c065c-dd6a-42ed-b748-a803a2f007f8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 54,
                column: "PermissionControllerActionGuid",
                value: new Guid("740ccb43-7d99-46ea-ac1f-db8abc3ad73b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 55,
                column: "PermissionControllerActionGuid",
                value: new Guid("98403f45-e10f-458e-92a9-116d186c64b8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 56,
                column: "PermissionControllerActionGuid",
                value: new Guid("2e046595-4fff-4722-8c3d-4a9a21175584"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 57,
                column: "PermissionControllerActionGuid",
                value: new Guid("4dcaaca2-bdef-42c6-8dbe-1c5eda0b8ce3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 58,
                column: "PermissionControllerActionGuid",
                value: new Guid("0c48bb53-cef6-4314-b87c-51a82274b5e3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 59,
                column: "PermissionControllerActionGuid",
                value: new Guid("3f1f7250-4ff2-4638-883d-0a4ee28dd988"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 60,
                column: "PermissionControllerActionGuid",
                value: new Guid("ee674caa-f71e-4fd6-b188-589d815567af"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 61,
                column: "PermissionControllerActionGuid",
                value: new Guid("550312ca-cfe2-407f-ab36-2f18a6b44160"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 62,
                column: "PermissionControllerActionGuid",
                value: new Guid("846647e2-f7f4-43a1-a6b2-327c963b513c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 63,
                column: "PermissionControllerActionGuid",
                value: new Guid("8eee6992-590a-4e82-9e7a-514773928fbc"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 64,
                column: "PermissionControllerActionGuid",
                value: new Guid("a2274fae-073b-464c-853b-26801dfceda5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 65,
                column: "PermissionControllerActionGuid",
                value: new Guid("33f4fefc-7aab-42fb-af7e-3bd39a7d5365"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 66,
                column: "PermissionControllerActionGuid",
                value: new Guid("60473ebf-ee15-442b-8d6e-bfa3432bd732"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 67,
                column: "PermissionControllerActionGuid",
                value: new Guid("92182e5f-6c84-481e-9fef-e60624f630b3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 68,
                column: "PermissionControllerActionGuid",
                value: new Guid("fd0ed04d-bada-4f92-9a01-c16e0b6ae867"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 69,
                column: "PermissionControllerActionGuid",
                value: new Guid("9aea3d1c-76da-4132-a3ee-e7c76cd583c3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 70,
                column: "PermissionControllerActionGuid",
                value: new Guid("197e23ef-6f26-4795-8f44-47c6933926b8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 71,
                column: "PermissionControllerActionGuid",
                value: new Guid("4dfbebe3-fe8f-4976-8b69-8fd317407fdd"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 72,
                column: "PermissionControllerActionGuid",
                value: new Guid("f103f280-3e46-4e7f-b465-5ac2482bd111"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 73,
                column: "PermissionControllerActionGuid",
                value: new Guid("4776cfbf-9401-4133-bd12-25a73fa43da0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 74,
                column: "PermissionControllerActionGuid",
                value: new Guid("f874caee-b10a-4b7b-b545-3739656559dc"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 75,
                column: "PermissionControllerActionGuid",
                value: new Guid("8c56ce22-0b77-48af-96eb-9ca72e299037"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 76,
                column: "PermissionControllerActionGuid",
                value: new Guid("dec590c5-24da-4bc1-92dc-9e3664354c96"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 77,
                column: "PermissionControllerActionGuid",
                value: new Guid("fd06d62f-bf4b-41ad-974c-c54e03a73e77"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 78,
                column: "PermissionControllerActionGuid",
                value: new Guid("7535fec5-b33d-403d-8144-2cd2dc3b45c5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 79,
                column: "PermissionControllerActionGuid",
                value: new Guid("411a18b3-a203-4412-abdb-8321aef3c30a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 80,
                column: "PermissionControllerActionGuid",
                value: new Guid("bf5a2e6e-dfd1-486f-a0e7-8e60016020ab"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 81,
                column: "PermissionControllerActionGuid",
                value: new Guid("d43f8312-7be3-4ec5-8054-72774e931f47"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 82,
                column: "PermissionControllerActionGuid",
                value: new Guid("4851bcac-494a-4d62-b587-780a9a02fd51"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 83,
                column: "PermissionControllerActionGuid",
                value: new Guid("62ad17ca-1c52-47ba-91d8-51892850d32a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 84,
                column: "PermissionControllerActionGuid",
                value: new Guid("2785a1d8-26f4-44f1-a79d-ae01578608f4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 85,
                column: "PermissionControllerActionGuid",
                value: new Guid("b06fc3c9-fc9f-4507-81dc-857cd8768bf8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 86,
                column: "PermissionControllerActionGuid",
                value: new Guid("e183f0af-7e4d-426a-af57-b8d622362d91"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 87,
                column: "PermissionControllerActionGuid",
                value: new Guid("9d7aa9a5-9ed4-48c7-9fc0-0bac38518385"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 88,
                column: "PermissionControllerActionGuid",
                value: new Guid("bdf200e5-4f2f-40ad-9bad-42447138d683"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 89,
                column: "PermissionControllerActionGuid",
                value: new Guid("a1fd0965-4dc9-45a4-8462-812545c40468"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 90,
                column: "PermissionControllerActionGuid",
                value: new Guid("e5ce8c3e-e251-471a-a363-31b73eaa991e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 91,
                column: "PermissionControllerActionGuid",
                value: new Guid("b9acd1ed-261d-495e-a5d6-3692e7e83384"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 92,
                column: "PermissionControllerActionGuid",
                value: new Guid("38d04adf-6470-4c73-a979-ad4744273803"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 93,
                column: "PermissionControllerActionGuid",
                value: new Guid("3a8a8b2c-0299-4554-a57b-cc27764b5b5c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 94,
                column: "PermissionControllerActionGuid",
                value: new Guid("f04aa43b-e195-4d19-81a3-cd92b8ec495f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 95,
                column: "PermissionControllerActionGuid",
                value: new Guid("26d2d59f-9a41-4022-8b88-50bd0124efe4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 96,
                column: "PermissionControllerActionGuid",
                value: new Guid("58b7930e-6d51-4281-932d-0e07cfdc2e62"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 97,
                column: "PermissionControllerActionGuid",
                value: new Guid("6c635994-c08c-4ac1-9c6a-b0d8da5059cb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 98,
                column: "PermissionControllerActionGuid",
                value: new Guid("47540bfb-227f-46b3-ad9c-e097d8333a66"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 99,
                column: "PermissionControllerActionGuid",
                value: new Guid("14d1d77e-7dab-4dde-8bd8-53537b4203ae"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 100,
                column: "PermissionControllerActionGuid",
                value: new Guid("67f37c75-b752-4463-b3d9-9a00a2c00b46"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 101,
                column: "PermissionControllerActionGuid",
                value: new Guid("86c4939b-99d2-4d32-b541-2dc25d52056a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 102,
                column: "PermissionControllerActionGuid",
                value: new Guid("33169e34-7391-4376-b36c-cf5f68e5e269"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 103,
                column: "PermissionControllerActionGuid",
                value: new Guid("fe40c5e9-68ec-425f-b070-f4ba87670bb8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 104,
                column: "PermissionControllerActionGuid",
                value: new Guid("2e4609fe-6fd5-4753-b320-3a4a2a095dd2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 105,
                column: "PermissionControllerActionGuid",
                value: new Guid("b95b5a9d-e278-4c71-9c73-a7671cfeab21"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 106,
                column: "PermissionControllerActionGuid",
                value: new Guid("97e77741-5194-4886-b0a5-71b158e64444"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 107,
                column: "PermissionControllerActionGuid",
                value: new Guid("229a5390-4c1f-4872-9081-77a9f0405e3c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 108,
                column: "PermissionControllerActionGuid",
                value: new Guid("461aa25c-1b2e-45d0-8c6e-b6042e519335"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 109,
                column: "PermissionControllerActionGuid",
                value: new Guid("bb1ff0d5-99f4-4484-b150-554b89b78ba0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 110,
                column: "PermissionControllerActionGuid",
                value: new Guid("20ce8f0c-55ca-46f9-91a5-ed1670b74a44"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 111,
                column: "PermissionControllerActionGuid",
                value: new Guid("3a7809d1-f71b-4ff9-b63e-204e2cada7e8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 112,
                column: "PermissionControllerActionGuid",
                value: new Guid("d3cdf9c5-9bc2-4a89-9461-30a8ae3370ff"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 113,
                column: "PermissionControllerActionGuid",
                value: new Guid("9c34d555-0b03-400e-a214-fd218cc9860d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 114,
                column: "PermissionControllerActionGuid",
                value: new Guid("38ba5687-b6f1-45be-a1a6-2db31f3beabd"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 115,
                column: "PermissionControllerActionGuid",
                value: new Guid("61db0f2e-f09e-47d2-abf0-1feaec7549e9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 116,
                column: "PermissionControllerActionGuid",
                value: new Guid("9ae193cd-ddb3-4d5b-a437-54b0c9116e5b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 117,
                column: "PermissionControllerActionGuid",
                value: new Guid("23f905e7-5fad-430e-ab60-3871e2e1642b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 118,
                column: "PermissionControllerActionGuid",
                value: new Guid("5e1dc7d3-abdf-4270-be8b-2e2e4f7ed76a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 119,
                column: "PermissionControllerActionGuid",
                value: new Guid("7d8d50d3-4dd5-437f-80e3-8d2dc2665c49"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 120,
                column: "PermissionControllerActionGuid",
                value: new Guid("b0619cd0-d54c-430b-a930-1d914c6859c4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 121,
                column: "PermissionControllerActionGuid",
                value: new Guid("0fba69f7-f18a-499d-80c5-18126252a03b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 122,
                column: "PermissionControllerActionGuid",
                value: new Guid("e5817015-df9f-48d3-9874-6bc2fd0a1bac"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 123,
                column: "PermissionControllerActionGuid",
                value: new Guid("db73be5e-3ee7-404c-abba-8709858c817b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 124,
                column: "PermissionControllerActionGuid",
                value: new Guid("fee8a322-dd70-44a0-aa35-eeaf0904c9dd"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 125,
                column: "PermissionControllerActionGuid",
                value: new Guid("fbf08a9a-d901-4e95-b469-db2e8218a10a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 126,
                column: "PermissionControllerActionGuid",
                value: new Guid("5b044063-4685-4fa4-b044-157878bc415d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 127,
                column: "PermissionControllerActionGuid",
                value: new Guid("d46106f5-f8fd-4b70-880e-4e7394e37e02"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 128,
                column: "PermissionControllerActionGuid",
                value: new Guid("2435e987-dd8b-493e-af9b-199478b40709"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 129,
                column: "PermissionControllerActionGuid",
                value: new Guid("0b89c89f-358d-473c-a401-4b8814da8f0a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 130,
                column: "PermissionControllerActionGuid",
                value: new Guid("44e3fc5c-634c-497a-940a-7f5846f86a04"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 131,
                column: "PermissionControllerActionGuid",
                value: new Guid("64f0f23b-c414-4925-b204-13ab94ea763c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 132,
                column: "PermissionControllerActionGuid",
                value: new Guid("7012522f-380c-47bc-a471-23e4f9d16da3"));
        }
    }
}
