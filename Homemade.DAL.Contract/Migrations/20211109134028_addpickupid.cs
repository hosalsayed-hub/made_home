using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Homemade.DAL.Contract.Migrations
{
    public partial class addpickupid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PickupId",
                schema: "Vendor",
                table: "Vendors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "b4998fa0-9d05-4e02-afba-7385209fb4e9");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "aa81de2e-5efa-40d1-846e-01da3c565fff");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "f180c0ec-fcb5-41b3-9754-594ec912efcb");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "01773a5f-0e13-4167-a3ce-5531115b5b69");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "3aedf8df-295f-447f-9940-f29fe192175b", "c83ad3f8-9fdf-436b-878b-54d60703f527" });

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "DeliverySetting",
                keyColumn: "DeliverySettingID",
                keyValue: 1,
                column: "DeliverySettingGuid",
                value: new Guid("1b183319-9275-4544-8de1-b89f1cf88ce2"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 1,
                column: "TransactionTypeGuid",
                value: new Guid("b653150e-0ae1-40b0-99fe-3f9a4487395a"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 2,
                column: "TransactionTypeGuid",
                value: new Guid("3249dfd0-dbfb-46e4-9184-70829e2fc419"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 3,
                column: "TransactionTypeGuid",
                value: new Guid("2f0bbd98-a20e-4c4c-a342-d60eb0340817"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 4,
                column: "TransactionTypeGuid",
                value: new Guid("297a4a7f-fb57-45d4-9252-40d4a0343d74"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 5,
                column: "TransactionTypeGuid",
                value: new Guid("30432761-d449-4b33-b3b2-c3d193193c52"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 6,
                column: "TransactionTypeGuid",
                value: new Guid("0d557fa3-2b73-4525-bfa5-62ce3bf6eec4"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 7,
                column: "TransactionTypeGuid",
                value: new Guid("279aa643-0095-4dee-a681-1ec20ab068e5"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 8,
                column: "TransactionTypeGuid",
                value: new Guid("37758438-d781-4096-b3ba-451d9620400c"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 9,
                column: "TransactionTypeGuid",
                value: new Guid("c2b7db14-f7f1-44df-aaff-7dd9ee6a88af"));

            migrationBuilder.UpdateData(
                schema: "Emp",
                table: "Employees",
                keyColumn: "EntityEmpID",
                keyValue: 1,
                columns: new[] { "CreateDate", "EnableDate" },
                values: new object[] { new DateTime(2021, 11, 9, 15, 40, 25, 417, DateTimeKind.Local).AddTicks(9749), new DateTime(2021, 11, 9, 15, 40, 25, 418, DateTimeKind.Local).AddTicks(203) });

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionAction",
                keyColumn: "PermissionActionID",
                keyValue: 1,
                column: "PermissionActionGuid",
                value: new Guid("19f2dca9-6c00-439a-9e7d-aba23c1bac99"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionAction",
                keyColumn: "PermissionActionID",
                keyValue: 2,
                column: "PermissionActionGuid",
                value: new Guid("8b87c1f7-b2a2-4429-af8d-0134921e719e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionAction",
                keyColumn: "PermissionActionID",
                keyValue: 3,
                column: "PermissionActionGuid",
                value: new Guid("e44758da-3d6b-4e0a-8a54-832ee5b89858"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionAction",
                keyColumn: "PermissionActionID",
                keyValue: 4,
                column: "PermissionActionGuid",
                value: new Guid("b6e74c39-929e-4bf1-8107-adb4266ce09b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 1,
                column: "PermissionControllerGuid",
                value: new Guid("873e212a-7eb1-43ea-be00-1dd075de2f58"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 2,
                column: "PermissionControllerGuid",
                value: new Guid("a90e0608-6967-4aab-9c4c-e18e03494e36"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 3,
                column: "PermissionControllerGuid",
                value: new Guid("16ee99e6-3c54-44b4-8f86-ce3e58f2b76c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 4,
                column: "PermissionControllerGuid",
                value: new Guid("1e4189df-9c8a-48d8-928b-f34e263b572a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 5,
                column: "PermissionControllerGuid",
                value: new Guid("f016a7cd-2534-4ecf-b4e5-4a07ed403abc"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 6,
                column: "PermissionControllerGuid",
                value: new Guid("0b2eed92-53e4-4b71-847f-9e7408a637c6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 7,
                column: "PermissionControllerGuid",
                value: new Guid("0c33f066-51de-4f5e-9b20-262c3eaac2d7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 8,
                column: "PermissionControllerGuid",
                value: new Guid("61afa6ca-18fc-414e-97bc-a91184ea9b8c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 9,
                column: "PermissionControllerGuid",
                value: new Guid("b88fafb7-a797-4833-b3be-966680995b32"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 10,
                column: "PermissionControllerGuid",
                value: new Guid("b03d464f-4848-4f01-8ddb-12b4e3caf483"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 11,
                column: "PermissionControllerGuid",
                value: new Guid("473c7cd9-5700-4851-87eb-59a3f33c33f6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 12,
                column: "PermissionControllerGuid",
                value: new Guid("d1da7f5b-837b-4c04-8ba3-496194be8dc3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 13,
                column: "PermissionControllerGuid",
                value: new Guid("34ce549b-08fe-4228-9445-786c755eb979"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 14,
                column: "PermissionControllerGuid",
                value: new Guid("5b3e1b9d-bd8a-465e-a400-f21a9aacf2dc"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 15,
                column: "PermissionControllerGuid",
                value: new Guid("ba8a9817-ab04-47fd-96ac-591a00e7ae4c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 16,
                column: "PermissionControllerGuid",
                value: new Guid("467f7eae-4f55-4da4-82bb-7491aa8eca90"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 17,
                column: "PermissionControllerGuid",
                value: new Guid("33a0a86d-0305-46aa-8e6d-acc3782dfd27"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 18,
                column: "PermissionControllerGuid",
                value: new Guid("30f72a1b-6f71-4669-80c7-3ef7c58198d7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 19,
                column: "PermissionControllerGuid",
                value: new Guid("8ffa5251-c2d2-4b0f-ac7e-b44eca0e30df"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 20,
                column: "PermissionControllerGuid",
                value: new Guid("87b1434b-2f5e-4699-a277-5f2faa3bd25f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 21,
                column: "PermissionControllerGuid",
                value: new Guid("0550f9c2-6ecc-41f4-a6cb-687f2188fbc3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 22,
                column: "PermissionControllerGuid",
                value: new Guid("5bea4d8f-e52b-4fac-9e3c-03dc263df263"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 23,
                column: "PermissionControllerGuid",
                value: new Guid("c84d0c9d-cf7a-473a-ad9d-a7aee5e32545"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 24,
                column: "PermissionControllerGuid",
                value: new Guid("40c56a89-b76e-4500-ade3-f013f740d5e6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 25,
                column: "PermissionControllerGuid",
                value: new Guid("73a0ae74-05e7-4ce0-a413-e095f4f8f4f7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 26,
                column: "PermissionControllerGuid",
                value: new Guid("d14d5c16-19ad-4373-af0d-f7ae61200522"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 27,
                column: "PermissionControllerGuid",
                value: new Guid("bbac4fc3-21bb-451f-9a37-f6e6c5475612"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 28,
                column: "PermissionControllerGuid",
                value: new Guid("0554626e-8a91-4682-b106-dd51e7f70bcf"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 29,
                column: "PermissionControllerGuid",
                value: new Guid("4a8566eb-7de0-409e-897d-7c6124ff77e2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 30,
                column: "PermissionControllerGuid",
                value: new Guid("1f717937-8760-48ea-8ef2-70a331f2a85e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 31,
                column: "PermissionControllerGuid",
                value: new Guid("7a538331-7f15-45d7-88ca-29cdb9a6710a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 32,
                column: "PermissionControllerGuid",
                value: new Guid("faeccab6-0bd7-4d61-8e92-771ef6fb4450"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 33,
                column: "PermissionControllerGuid",
                value: new Guid("003819a6-93f0-49c1-945b-042b5ee1bf48"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 34,
                column: "PermissionControllerGuid",
                value: new Guid("145489e6-6eab-4e03-b9c8-fbb6d0c15497"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 35,
                column: "PermissionControllerGuid",
                value: new Guid("489efb7c-0416-42fd-b15b-8b28559ac6db"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 36,
                column: "PermissionControllerGuid",
                value: new Guid("cb9fc8b9-eb5a-48ad-b656-15f53f38f412"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 37,
                column: "PermissionControllerGuid",
                value: new Guid("38f3e1b4-1c72-42c2-884f-a0ae7512c6f8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 38,
                column: "PermissionControllerGuid",
                value: new Guid("23cf66c4-28ce-4de7-be9e-57adea68eb0d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 39,
                column: "PermissionControllerGuid",
                value: new Guid("7c143dda-8cf8-4236-9fdd-f48f1b38f380"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 40,
                column: "PermissionControllerGuid",
                value: new Guid("78a59a61-c622-4ac1-9db9-783d0b7f38a1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 41,
                column: "PermissionControllerGuid",
                value: new Guid("8abfe26d-3d79-4c7f-87f8-6a4579c2b2b6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 42,
                column: "PermissionControllerGuid",
                value: new Guid("f698d0c3-d009-44e6-baa7-ee489ee2104e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 43,
                column: "PermissionControllerGuid",
                value: new Guid("ef72c52f-c586-4cc3-a12d-9c70c91f4b34"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 44,
                column: "PermissionControllerGuid",
                value: new Guid("8cbf399c-cc85-4a4f-a55e-8c26d27c144f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 45,
                column: "PermissionControllerGuid",
                value: new Guid("720675cb-2c1b-4fe2-88e7-fd9df8f73bfd"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 46,
                column: "PermissionControllerGuid",
                value: new Guid("5210af83-e839-4958-9504-6152c6b165b3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 47,
                column: "PermissionControllerGuid",
                value: new Guid("9745d27b-2e43-4a2a-9b81-d76fbf51fb87"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 48,
                column: "PermissionControllerGuid",
                value: new Guid("bd6ca5b0-342a-455c-9820-abd02e67ce32"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 49,
                column: "PermissionControllerGuid",
                value: new Guid("5583714a-4dd4-45f0-9b81-511b6e132db9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 50,
                column: "PermissionControllerGuid",
                value: new Guid("80930fbf-c134-48d2-8da1-07818f1fcb92"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 51,
                column: "PermissionControllerGuid",
                value: new Guid("d282e793-98cd-40d0-9bc5-83f835c6549e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 52,
                column: "PermissionControllerGuid",
                value: new Guid("1b083561-3e72-44ea-9fe8-f2d3098d9104"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 53,
                column: "PermissionControllerGuid",
                value: new Guid("c07c70da-33bd-42f4-bef4-7d091e10773b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 54,
                column: "PermissionControllerGuid",
                value: new Guid("65fbceec-7255-466b-9b7d-48710e8e2a90"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 55,
                column: "PermissionControllerGuid",
                value: new Guid("765455ba-dda6-42e4-bfb1-cc1e2e8cdbcb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 1,
                column: "PermissionControllerActionGuid",
                value: new Guid("33e1b068-f27b-4bda-81fc-230bae4ac814"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 2,
                column: "PermissionControllerActionGuid",
                value: new Guid("422d631f-8cab-4349-bb94-a98d452b4dc8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 3,
                column: "PermissionControllerActionGuid",
                value: new Guid("4128c99a-1536-41fd-abfb-fa14ff520cde"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 4,
                column: "PermissionControllerActionGuid",
                value: new Guid("dc67ca6b-b5f1-400a-bb18-cfb54d79bdf1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 5,
                column: "PermissionControllerActionGuid",
                value: new Guid("8a17ef9e-1c53-420d-9a82-abce4de2a8fb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 6,
                column: "PermissionControllerActionGuid",
                value: new Guid("477b0d57-292e-4b7a-a70d-a92b28047438"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 7,
                column: "PermissionControllerActionGuid",
                value: new Guid("03a96949-38fc-44fe-a1ee-0ac7175c511b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 8,
                column: "PermissionControllerActionGuid",
                value: new Guid("6446a821-6a6b-4412-8737-0e79f3bbb29a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 9,
                column: "PermissionControllerActionGuid",
                value: new Guid("0b7ca6a3-8306-4f2d-a2a9-337d50075549"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 10,
                column: "PermissionControllerActionGuid",
                value: new Guid("d80ee77c-d17c-4755-883a-b7306134f946"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 11,
                column: "PermissionControllerActionGuid",
                value: new Guid("bb70e420-cf3d-42f5-b5d6-99401ae57032"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 12,
                column: "PermissionControllerActionGuid",
                value: new Guid("a4ab9fa2-4700-4060-9ebc-0f1260595c45"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 13,
                column: "PermissionControllerActionGuid",
                value: new Guid("eeb86489-9e48-4936-ab63-4c829c9a0149"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 14,
                column: "PermissionControllerActionGuid",
                value: new Guid("84d280c5-bdce-4f0c-b945-8a28d1e9474e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 15,
                column: "PermissionControllerActionGuid",
                value: new Guid("7be2509d-12ea-4299-b160-5cf988ef60bb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 16,
                column: "PermissionControllerActionGuid",
                value: new Guid("7273dec5-42c0-41bb-9810-cb21d3043a5b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 17,
                column: "PermissionControllerActionGuid",
                value: new Guid("dddf6fb8-9f83-4449-a153-7b7fbb191afa"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 18,
                column: "PermissionControllerActionGuid",
                value: new Guid("4af4f837-a777-4d27-92ec-53f5e25c5009"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 19,
                column: "PermissionControllerActionGuid",
                value: new Guid("7287892c-b42c-4a6f-a203-d0db57ca6ce5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 20,
                column: "PermissionControllerActionGuid",
                value: new Guid("dafcf52d-8b64-4415-95da-9281a07f195e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 21,
                column: "PermissionControllerActionGuid",
                value: new Guid("fc249232-c483-4b72-89f8-e90e91f9adf7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 22,
                column: "PermissionControllerActionGuid",
                value: new Guid("0631671c-3edb-42dc-acd9-7ecc591702cc"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 23,
                column: "PermissionControllerActionGuid",
                value: new Guid("16ceb0b9-aeb5-4fca-b743-1715a78ad28e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 24,
                column: "PermissionControllerActionGuid",
                value: new Guid("ee394dbd-4af7-4f90-b80b-794afb9b29d3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 25,
                column: "PermissionControllerActionGuid",
                value: new Guid("2ae7e8fc-1099-4d2f-89f1-6d11b1af9975"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 26,
                column: "PermissionControllerActionGuid",
                value: new Guid("95ee7463-e6c1-48d7-8778-a91f4467488f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 27,
                column: "PermissionControllerActionGuid",
                value: new Guid("67097332-fdf1-4e7c-baa9-071d4883fdef"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 28,
                column: "PermissionControllerActionGuid",
                value: new Guid("09628472-10aa-4a99-8e88-c1828aba51d1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 29,
                column: "PermissionControllerActionGuid",
                value: new Guid("04110195-6aaa-45da-981a-3251af20c5c9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 30,
                column: "PermissionControllerActionGuid",
                value: new Guid("82bb838e-c60a-42f2-80fa-da289c4056f6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 31,
                column: "PermissionControllerActionGuid",
                value: new Guid("d2524ae0-8fab-499d-89b8-c7d2bc887fe9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 32,
                column: "PermissionControllerActionGuid",
                value: new Guid("7e30272e-9231-4a28-96e1-37e64c74c82e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 33,
                column: "PermissionControllerActionGuid",
                value: new Guid("fb7a2223-c183-4cf6-8b1a-7b0d47e5d5d9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 34,
                column: "PermissionControllerActionGuid",
                value: new Guid("83529566-a282-4206-bb07-2dd9a7fe098a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 35,
                column: "PermissionControllerActionGuid",
                value: new Guid("e039d72a-dafb-4718-9798-ce34e4e175a5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 36,
                column: "PermissionControllerActionGuid",
                value: new Guid("949d6e93-5ba8-4aa0-861d-36e32572a584"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 37,
                column: "PermissionControllerActionGuid",
                value: new Guid("bce63b91-bde1-4d25-962d-5f39ff0aec29"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 38,
                column: "PermissionControllerActionGuid",
                value: new Guid("94104008-b700-4dc5-a969-2fc24298cd4d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 39,
                column: "PermissionControllerActionGuid",
                value: new Guid("53600554-921b-4b1e-807a-2467207bd876"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 40,
                column: "PermissionControllerActionGuid",
                value: new Guid("53619101-b212-4244-b78f-f61107a4ff04"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 41,
                column: "PermissionControllerActionGuid",
                value: new Guid("0027ca50-480f-4aa2-80c6-617b3a623e0a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 42,
                column: "PermissionControllerActionGuid",
                value: new Guid("3af4bf4b-4097-446e-b942-a5f90fee5af2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 43,
                column: "PermissionControllerActionGuid",
                value: new Guid("3d4d4c1f-808e-41cd-8181-76f5195f21b6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 44,
                column: "PermissionControllerActionGuid",
                value: new Guid("d09eb8c8-9c11-457b-aced-086b4d32cb63"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 45,
                column: "PermissionControllerActionGuid",
                value: new Guid("3667805b-792a-465b-972d-5d5555da2f37"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 46,
                column: "PermissionControllerActionGuid",
                value: new Guid("10020ee7-c58c-4ea0-890b-45e386f38035"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 47,
                column: "PermissionControllerActionGuid",
                value: new Guid("1cfba234-b678-4fda-977e-2a1b029b8142"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 48,
                column: "PermissionControllerActionGuid",
                value: new Guid("40062311-f77c-4c51-885f-f8ad0fa3c848"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 49,
                column: "PermissionControllerActionGuid",
                value: new Guid("b18986f4-888f-4dee-a17b-4b7d0f1bda2f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 50,
                column: "PermissionControllerActionGuid",
                value: new Guid("bd7cc3b1-6e21-4eb5-8162-b5403fa9dc56"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 51,
                column: "PermissionControllerActionGuid",
                value: new Guid("fc6eb0a9-6665-45c8-a14c-f8d87392673f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 52,
                column: "PermissionControllerActionGuid",
                value: new Guid("b374a097-7352-4e98-8eb7-fab1a0313bff"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 53,
                column: "PermissionControllerActionGuid",
                value: new Guid("f3c0912d-1004-42cd-90c2-1de38cd4c7f4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 54,
                column: "PermissionControllerActionGuid",
                value: new Guid("71090a08-c410-4480-bd8f-a30e2fa55a6e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 55,
                column: "PermissionControllerActionGuid",
                value: new Guid("338c5630-087f-49c9-b7b2-54b438d9832f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 56,
                column: "PermissionControllerActionGuid",
                value: new Guid("e3065a7d-872b-45c5-9484-58e5f071cf26"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 57,
                column: "PermissionControllerActionGuid",
                value: new Guid("96088d5b-9f19-4148-8572-6b5536d5e2bb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 58,
                column: "PermissionControllerActionGuid",
                value: new Guid("ab066ee4-0c07-4120-9730-855c6006a152"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 59,
                column: "PermissionControllerActionGuid",
                value: new Guid("6ade4d73-42b0-490f-b1cd-cd5d94beae53"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 60,
                column: "PermissionControllerActionGuid",
                value: new Guid("5ad12920-7f8f-40e2-84f7-c3b616271e8e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 61,
                column: "PermissionControllerActionGuid",
                value: new Guid("aee3d3fc-8fd5-4c8a-a52b-7e6a3ce2d40c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 62,
                column: "PermissionControllerActionGuid",
                value: new Guid("9bce62ac-7fbb-4c9c-b868-7c597b776673"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 63,
                column: "PermissionControllerActionGuid",
                value: new Guid("b0f2168b-faac-47db-9ec4-5f91bd7f05ad"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 64,
                column: "PermissionControllerActionGuid",
                value: new Guid("308d681e-b931-4b70-bf68-0a909cc43fc1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 65,
                column: "PermissionControllerActionGuid",
                value: new Guid("572c3d5f-9d0d-45db-a498-fc273b54663f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 66,
                column: "PermissionControllerActionGuid",
                value: new Guid("26cb1c89-196d-4184-a39e-eb193a1728a1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 67,
                column: "PermissionControllerActionGuid",
                value: new Guid("2100eb5b-5085-44d2-80de-c5483a619ede"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 68,
                column: "PermissionControllerActionGuid",
                value: new Guid("5ebf96b5-2907-41c0-84f1-7b44b3a6310f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 69,
                column: "PermissionControllerActionGuid",
                value: new Guid("87032f90-4fa0-4d86-bb15-c7d8c40e15f0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 70,
                column: "PermissionControllerActionGuid",
                value: new Guid("1fe25217-c1d5-4b0b-96cf-8d7b4b36214e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 71,
                column: "PermissionControllerActionGuid",
                value: new Guid("16252748-dc70-458a-a3bc-cb0c9b39e3f8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 72,
                column: "PermissionControllerActionGuid",
                value: new Guid("ac4f3d96-863e-4d86-a537-0375dc31c3ef"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 73,
                column: "PermissionControllerActionGuid",
                value: new Guid("281c02ee-869e-4b0e-8b8b-9bd8929e4eb6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 74,
                column: "PermissionControllerActionGuid",
                value: new Guid("c15d5272-07e7-4854-9054-fe76af22d7be"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 75,
                column: "PermissionControllerActionGuid",
                value: new Guid("ce4a13d3-2bdd-4207-9d99-6445182dcb18"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 76,
                column: "PermissionControllerActionGuid",
                value: new Guid("f86b6b66-8a42-4b10-8dbc-0ed9d0cb0e57"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 77,
                column: "PermissionControllerActionGuid",
                value: new Guid("d7e28ea7-67b0-4e08-8362-c2ec95fd6b2d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 78,
                column: "PermissionControllerActionGuid",
                value: new Guid("f5ffd522-6558-4466-831f-2df4922733fb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 79,
                column: "PermissionControllerActionGuid",
                value: new Guid("5ec1ee64-0c22-48e0-a1f6-9dce67242eba"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 80,
                column: "PermissionControllerActionGuid",
                value: new Guid("563d8da8-31a4-4812-bee0-476b81546b4a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 81,
                column: "PermissionControllerActionGuid",
                value: new Guid("1daef7a1-3291-420a-84e2-74a48199c72a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 82,
                column: "PermissionControllerActionGuid",
                value: new Guid("9a496478-52bb-4ea5-9eb9-b3a62800485b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 83,
                column: "PermissionControllerActionGuid",
                value: new Guid("d08a4d12-3bd3-44f0-ad1d-654f003d61d9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 84,
                column: "PermissionControllerActionGuid",
                value: new Guid("ab72b58d-a7be-410e-8cdc-7fffebc7dd1d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 85,
                column: "PermissionControllerActionGuid",
                value: new Guid("14116459-18f0-46e5-8879-df1cc794adf1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 86,
                column: "PermissionControllerActionGuid",
                value: new Guid("4b6daae7-c454-4698-a3cb-84e14fbac4d3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 87,
                column: "PermissionControllerActionGuid",
                value: new Guid("337e7ef7-fe90-4af5-8f45-84ebde51eb8a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 88,
                column: "PermissionControllerActionGuid",
                value: new Guid("72c1427f-9802-4225-ab10-e319bae3ad51"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 89,
                column: "PermissionControllerActionGuid",
                value: new Guid("76605402-b2fd-4b00-b98e-1a1b551b3ba8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 90,
                column: "PermissionControllerActionGuid",
                value: new Guid("66bd7c44-721c-4bcd-b565-d55ec624f3c9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 91,
                column: "PermissionControllerActionGuid",
                value: new Guid("694ceda0-2b57-48d6-ad3f-a28eee73288b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 92,
                column: "PermissionControllerActionGuid",
                value: new Guid("c9ecc1e6-7a8d-4dc9-8384-e68fd32844b0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 93,
                column: "PermissionControllerActionGuid",
                value: new Guid("5ceda127-f955-4f24-9d0b-605642051567"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 94,
                column: "PermissionControllerActionGuid",
                value: new Guid("679252e2-d170-402d-8834-2405d2f3151d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 95,
                column: "PermissionControllerActionGuid",
                value: new Guid("17bc0bc5-e5eb-436c-8098-2d9ac819583d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 96,
                column: "PermissionControllerActionGuid",
                value: new Guid("7fe5470b-1bbd-4c53-9dc2-6a5f743868f7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 97,
                column: "PermissionControllerActionGuid",
                value: new Guid("9937e3e2-d0a9-44b2-86b1-dc3036d051a4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 98,
                column: "PermissionControllerActionGuid",
                value: new Guid("f8f38ad9-d3a9-425f-9387-1ec5d31a07c1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 99,
                column: "PermissionControllerActionGuid",
                value: new Guid("5f53b509-5f98-4e4a-87b2-7553dcc3fa5d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 100,
                column: "PermissionControllerActionGuid",
                value: new Guid("0ad6d694-be5f-4c02-bcb5-7c0f923b62bd"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 101,
                column: "PermissionControllerActionGuid",
                value: new Guid("5ce17c48-3489-4058-8049-8141f8257caf"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 102,
                column: "PermissionControllerActionGuid",
                value: new Guid("77072d74-9ca0-4463-8aa5-ae3d4213d8fb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 103,
                column: "PermissionControllerActionGuid",
                value: new Guid("42977ba5-ddcd-4628-bcc4-aac41b565fa0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 104,
                column: "PermissionControllerActionGuid",
                value: new Guid("77347a71-7fe6-4861-8dea-5b4b6e946766"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 105,
                column: "PermissionControllerActionGuid",
                value: new Guid("f10b737a-b742-4997-b457-8aabe254972d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 106,
                column: "PermissionControllerActionGuid",
                value: new Guid("616af063-db62-4377-bdda-a9c497c29da2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 107,
                column: "PermissionControllerActionGuid",
                value: new Guid("fb147a9c-a5a6-4998-8416-1af39eaad082"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 108,
                column: "PermissionControllerActionGuid",
                value: new Guid("bc8befc6-f2af-45e9-b376-ce883fd41820"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 109,
                column: "PermissionControllerActionGuid",
                value: new Guid("34f44eca-8d31-4bdd-9394-300dce94d142"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 110,
                column: "PermissionControllerActionGuid",
                value: new Guid("cb3a930a-d7cc-4a8e-88fb-734ebf3e1684"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 111,
                column: "PermissionControllerActionGuid",
                value: new Guid("f1827a11-ea44-4bf0-b895-c68af116c641"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 112,
                column: "PermissionControllerActionGuid",
                value: new Guid("f1a13212-fb42-468c-ad0f-bcae68b28fd6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 113,
                column: "PermissionControllerActionGuid",
                value: new Guid("4de778cf-b5a9-406f-8d62-d849a714a084"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 114,
                column: "PermissionControllerActionGuid",
                value: new Guid("d12d9505-22b4-4f12-8ed5-5e97f6ffb46a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 115,
                column: "PermissionControllerActionGuid",
                value: new Guid("176767eb-d971-44b9-8ca2-c4f8a1edbbfb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 116,
                column: "PermissionControllerActionGuid",
                value: new Guid("67610d39-b688-4341-b514-56de6f251b89"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 117,
                column: "PermissionControllerActionGuid",
                value: new Guid("77edf7ef-3808-403c-8003-bd97b336acb9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 118,
                column: "PermissionControllerActionGuid",
                value: new Guid("bf90a7bd-9676-481d-9dd0-b742404d5613"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 119,
                column: "PermissionControllerActionGuid",
                value: new Guid("bc0af8fd-6b8d-4e7c-aae3-7342d0ff0839"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 120,
                column: "PermissionControllerActionGuid",
                value: new Guid("cb13acd8-d3c4-4cea-bd47-c89e66ee5f3b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 121,
                column: "PermissionControllerActionGuid",
                value: new Guid("cfb78de0-1af5-4825-a04c-15d7feb1ae4f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 122,
                column: "PermissionControllerActionGuid",
                value: new Guid("7419bd96-ebc4-4555-a756-1c41d39201be"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 123,
                column: "PermissionControllerActionGuid",
                value: new Guid("3f03f5e1-431a-4847-a646-edf25bf7e449"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 124,
                column: "PermissionControllerActionGuid",
                value: new Guid("36764ac4-e638-40ed-ba6d-27c977a60a47"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 125,
                column: "PermissionControllerActionGuid",
                value: new Guid("287f57d4-c019-497f-9db0-f475c8f3188f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 126,
                column: "PermissionControllerActionGuid",
                value: new Guid("efe0c969-8f09-4b14-b19c-4baf6a38c67a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 127,
                column: "PermissionControllerActionGuid",
                value: new Guid("59ac09b1-2779-4ddd-81a3-af6afa4e20e2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 128,
                column: "PermissionControllerActionGuid",
                value: new Guid("377c861c-3427-4002-b285-e9d466a56f9a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 129,
                column: "PermissionControllerActionGuid",
                value: new Guid("f5eb98a6-efb9-41bd-b4ed-613f3c71d600"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 130,
                column: "PermissionControllerActionGuid",
                value: new Guid("a39bd014-2d1c-48d2-91bb-a77a3182673f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 131,
                column: "PermissionControllerActionGuid",
                value: new Guid("b26f7194-727b-4ec8-b57c-811a19cfff29"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 132,
                column: "PermissionControllerActionGuid",
                value: new Guid("9ff394d8-3185-4ba7-8d56-a76536a9e25b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 133,
                column: "PermissionControllerActionGuid",
                value: new Guid("e832e0d9-fad2-4f9c-9f3c-43397e0f74e3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 134,
                column: "PermissionControllerActionGuid",
                value: new Guid("409c5890-bead-4576-9bee-78b4754f0e58"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 135,
                column: "PermissionControllerActionGuid",
                value: new Guid("e7e63854-3d15-4f59-be07-44921c3a7fe4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 136,
                column: "PermissionControllerActionGuid",
                value: new Guid("e629db12-90f8-462d-a027-c5522d0eb345"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 137,
                column: "PermissionControllerActionGuid",
                value: new Guid("10c682a0-aba1-4e1b-bd87-dac14865d1f3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 138,
                column: "PermissionControllerActionGuid",
                value: new Guid("15157b76-9cbd-474f-ab77-2cdc4b0d639d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 139,
                column: "PermissionControllerActionGuid",
                value: new Guid("5f2df026-17b7-4bef-b4ce-72018478cdea"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 140,
                column: "PermissionControllerActionGuid",
                value: new Guid("b8d0981a-758e-43b6-b9fa-73254f424ef0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 141,
                column: "PermissionControllerActionGuid",
                value: new Guid("ec91cb36-ee6e-42be-ae20-9915772dc1f5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 142,
                column: "PermissionControllerActionGuid",
                value: new Guid("de3c68b7-c564-4110-909c-d9694f037667"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 143,
                column: "PermissionControllerActionGuid",
                value: new Guid("047f7e99-5d58-4cee-aa85-e862f1616073"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 144,
                column: "PermissionControllerActionGuid",
                value: new Guid("fc2e0b9f-a5bf-4b66-b163-6ccfbd054d02"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 145,
                column: "PermissionControllerActionGuid",
                value: new Guid("7520cd45-687a-4298-910e-30be8d686a13"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 146,
                column: "PermissionControllerActionGuid",
                value: new Guid("f3ecba55-a30d-42d8-81fd-810897de66be"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 147,
                column: "PermissionControllerActionGuid",
                value: new Guid("532f232b-91ca-4aef-a598-0bc6981fee6f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 148,
                column: "PermissionControllerActionGuid",
                value: new Guid("f48c3412-43d3-4188-af38-4e6c8eaf2ebd"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 149,
                column: "PermissionControllerActionGuid",
                value: new Guid("f31070e2-2527-4683-ac75-1f2ad5220fbf"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 150,
                column: "PermissionControllerActionGuid",
                value: new Guid("d196aabd-aa88-4251-a2b8-152fbd47461e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 151,
                column: "PermissionControllerActionGuid",
                value: new Guid("ef1119e6-cb78-4b4c-9220-794798085ddf"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 152,
                column: "PermissionControllerActionGuid",
                value: new Guid("fd9b6dc5-410f-434e-a953-bb705572d79b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 153,
                column: "PermissionControllerActionGuid",
                value: new Guid("d1067bf2-f2c1-4cb3-9524-887441c309d1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 154,
                column: "PermissionControllerActionGuid",
                value: new Guid("10d212dc-4050-498a-9285-524bbd8ee7f7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 155,
                column: "PermissionControllerActionGuid",
                value: new Guid("917524ad-63b2-42f5-8877-eb2303185ba8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 156,
                column: "PermissionControllerActionGuid",
                value: new Guid("694e5505-3e5c-42fa-8f12-764f76b2e63b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 157,
                column: "PermissionControllerActionGuid",
                value: new Guid("970ff923-0221-4f8a-a7f2-232cd44c6a6d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 158,
                column: "PermissionControllerActionGuid",
                value: new Guid("fcafbc11-b1d0-48a3-870a-0b5f28017be9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 159,
                column: "PermissionControllerActionGuid",
                value: new Guid("6624b663-4b87-40f1-a9a1-6bf69e5ba043"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 160,
                column: "PermissionControllerActionGuid",
                value: new Guid("d9fd6dc3-6cf5-4c75-b6d3-bc80b67b3a02"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 161,
                column: "PermissionControllerActionGuid",
                value: new Guid("b5069353-841f-4bc9-9328-4d82981fa3a4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 162,
                column: "PermissionControllerActionGuid",
                value: new Guid("edf73c00-2e64-42d3-9bb2-666634019445"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 163,
                column: "PermissionControllerActionGuid",
                value: new Guid("11dbe9ac-6528-43a5-89cc-102d3c35889c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 164,
                column: "PermissionControllerActionGuid",
                value: new Guid("f95fc065-54a8-4525-97c3-b2e72914c9de"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 165,
                column: "PermissionControllerActionGuid",
                value: new Guid("5ba0e7b9-77d5-4afb-9468-1596aed57c1a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 166,
                column: "PermissionControllerActionGuid",
                value: new Guid("5b790677-c7fa-4fb0-9b27-3575959b86ee"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 167,
                column: "PermissionControllerActionGuid",
                value: new Guid("5ae1b6ee-ed36-434a-8477-c27ec280e191"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 168,
                column: "PermissionControllerActionGuid",
                value: new Guid("d42f09aa-71e0-4ce4-ac07-be7d01bb300f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 169,
                column: "PermissionControllerActionGuid",
                value: new Guid("66db40a5-c622-4c43-bf36-399b8f877882"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 170,
                column: "PermissionControllerActionGuid",
                value: new Guid("6067e848-d8a1-4080-aacc-1494d28a09df"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 171,
                column: "PermissionControllerActionGuid",
                value: new Guid("bb82357f-80f1-402d-b9c3-e1a961b78129"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 172,
                column: "PermissionControllerActionGuid",
                value: new Guid("360c65eb-29c0-4fa3-a224-d52c2fef50c3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 173,
                column: "PermissionControllerActionGuid",
                value: new Guid("e7820bab-0087-4b01-9fe7-a7dc8d2c6301"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 174,
                column: "PermissionControllerActionGuid",
                value: new Guid("69d36de7-2528-40e6-ac95-513010f4ce3a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 175,
                column: "PermissionControllerActionGuid",
                value: new Guid("94af1ce4-7763-45af-970d-6a5c6cf38dbf"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 176,
                column: "PermissionControllerActionGuid",
                value: new Guid("19e932fe-dfe0-473c-9398-59c69a6a59db"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 177,
                column: "PermissionControllerActionGuid",
                value: new Guid("08ea603f-0bc8-4881-822b-0c6c33a0fd4b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 178,
                column: "PermissionControllerActionGuid",
                value: new Guid("37618a28-992e-464b-99ce-ebcefa12b493"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 179,
                column: "PermissionControllerActionGuid",
                value: new Guid("a48c3b7e-a2f0-4422-be31-a7bb4a0540a4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 180,
                column: "PermissionControllerActionGuid",
                value: new Guid("0c0c407c-4f26-403d-b97d-ea21a4365966"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 181,
                column: "PermissionControllerActionGuid",
                value: new Guid("6c82f709-d957-4782-ba5b-cc127b5e250c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 182,
                column: "PermissionControllerActionGuid",
                value: new Guid("d2415b48-3372-49fc-a191-68c5db72d713"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 183,
                column: "PermissionControllerActionGuid",
                value: new Guid("e385fcc6-c92f-44cc-a716-da568d02deb9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 184,
                column: "PermissionControllerActionGuid",
                value: new Guid("f08a8dca-e683-483b-874d-6a2ed6be5427"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 185,
                column: "PermissionControllerActionGuid",
                value: new Guid("751a2160-24c1-4bb5-a31b-42de6589a817"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 186,
                column: "PermissionControllerActionGuid",
                value: new Guid("86610551-8987-4c7b-8f4a-6292bd3788d5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 187,
                column: "PermissionControllerActionGuid",
                value: new Guid("604fa5ac-e360-4702-badc-ad14ba7ed9e0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 188,
                column: "PermissionControllerActionGuid",
                value: new Guid("f3d09cca-ace6-411c-a9d3-675e441c6308"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 189,
                column: "PermissionControllerActionGuid",
                value: new Guid("c056a339-e1d9-4502-b7bd-da1746205fb4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 190,
                column: "PermissionControllerActionGuid",
                value: new Guid("f6ca84a8-21c4-4757-baf9-224ba0a61ff8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 191,
                column: "PermissionControllerActionGuid",
                value: new Guid("ff659276-b4e8-4049-882d-7b037436e64a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 192,
                column: "PermissionControllerActionGuid",
                value: new Guid("f6633df6-9779-430d-b6b7-ed577c920feb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 193,
                column: "PermissionControllerActionGuid",
                value: new Guid("32ecb232-904c-4b60-9c45-0e9a0fc7714b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 194,
                column: "PermissionControllerActionGuid",
                value: new Guid("2d7f092d-2408-4352-a624-1b9cf37f4137"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 195,
                column: "PermissionControllerActionGuid",
                value: new Guid("e536cfb9-ecb9-41c5-8120-6100c5feefc0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 196,
                column: "PermissionControllerActionGuid",
                value: new Guid("5a4e7931-cbc7-4246-b96f-fae2273826af"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 197,
                column: "PermissionControllerActionGuid",
                value: new Guid("c69da92e-f71c-4f8d-b58b-5758d62159c7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 198,
                column: "PermissionControllerActionGuid",
                value: new Guid("5c188ee6-aa57-491b-adb3-e26b9f926665"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 199,
                column: "PermissionControllerActionGuid",
                value: new Guid("d8124d27-da28-4f51-a3c4-9ae8e919f157"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 200,
                column: "PermissionControllerActionGuid",
                value: new Guid("b1374fd5-d7ef-403d-b1fc-9fd939d86b68"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 201,
                column: "PermissionControllerActionGuid",
                value: new Guid("ec3d75cd-d1dc-4669-8b10-6baf78d80e7e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 202,
                column: "PermissionControllerActionGuid",
                value: new Guid("aafdcc9b-0f5a-411f-8b3a-6f1a4dd02dbd"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 203,
                column: "PermissionControllerActionGuid",
                value: new Guid("3b00bcaa-1c86-4a40-ab2c-c23148142a85"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 204,
                column: "PermissionControllerActionGuid",
                value: new Guid("f3560f36-b029-4cae-840a-2e8575df69eb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 205,
                column: "PermissionControllerActionGuid",
                value: new Guid("3e92d0d3-eeda-4f41-828c-69b6a95df546"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 206,
                column: "PermissionControllerActionGuid",
                value: new Guid("d29acabd-4915-4e76-a7f2-cf036be2c7dc"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 207,
                column: "PermissionControllerActionGuid",
                value: new Guid("7d050bf0-0f7e-44bc-9ac4-45d19177538c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 208,
                column: "PermissionControllerActionGuid",
                value: new Guid("1b3c409b-259e-4562-9710-054863429506"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 209,
                column: "PermissionControllerActionGuid",
                value: new Guid("3982125d-f073-496e-9efa-3d257ec3e444"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 210,
                column: "PermissionControllerActionGuid",
                value: new Guid("e8fcefe9-b7e4-4516-b3c3-962801961dba"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 211,
                column: "PermissionControllerActionGuid",
                value: new Guid("c38061f9-ac30-44bd-8230-c85a600b47ee"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 212,
                column: "PermissionControllerActionGuid",
                value: new Guid("15135fe9-bdc0-434b-a2ab-8536fd454b3a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 213,
                column: "PermissionControllerActionGuid",
                value: new Guid("c4abbc9e-052f-4acc-9881-95c689817f8c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 214,
                column: "PermissionControllerActionGuid",
                value: new Guid("d5cd91e8-8b31-438b-bc4e-536e30a25f9b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 215,
                column: "PermissionControllerActionGuid",
                value: new Guid("005acb2c-e6a3-45ad-870c-f09cc23508e9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 216,
                column: "PermissionControllerActionGuid",
                value: new Guid("a64a8623-1294-476f-ae50-566386b80a72"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 217,
                column: "PermissionControllerActionGuid",
                value: new Guid("36baf385-b2ef-4b0d-a8b7-34435b8d03a7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 218,
                column: "PermissionControllerActionGuid",
                value: new Guid("e9cb7011-253f-4c3f-8dbc-fef0eea9f919"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 219,
                column: "PermissionControllerActionGuid",
                value: new Guid("09d93963-7b63-4cdb-9580-dd48430c777f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 220,
                column: "PermissionControllerActionGuid",
                value: new Guid("b49920bf-a066-44cd-8832-5acac82a8302"));

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "City",
                keyColumn: "CityID",
                keyValue: 1,
                columns: new[] { "CityGuid", "CreateDate", "EnableDate" },
                values: new object[] { new Guid("f6521a1a-25d2-44c6-bb93-1d60e3f5ab94"), new DateTime(2021, 11, 9, 15, 40, 25, 411, DateTimeKind.Local).AddTicks(8995), new DateTime(2021, 11, 9, 15, 40, 25, 411, DateTimeKind.Local).AddTicks(9784) });

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "Country",
                keyColumn: "CountryID",
                keyValue: 1,
                columns: new[] { "CountryGuid", "CreateDate", "EnableDate" },
                values: new object[] { new Guid("9b2f21d6-65d2-4515-96c2-aea7b15b8ad3"), new DateTime(2021, 11, 9, 15, 40, 25, 407, DateTimeKind.Local).AddTicks(8907), new DateTime(2021, 11, 9, 15, 40, 25, 409, DateTimeKind.Local).AddTicks(4092) });

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "Departments",
                keyColumn: "DepartmentsID",
                keyValue: 1,
                columns: new[] { "CreateDate", "DepartmentsGuid", "EnableDate" },
                values: new object[] { new DateTime(2021, 11, 9, 15, 40, 25, 416, DateTimeKind.Local).AddTicks(8859), new Guid("a3838e80-c671-4b7b-98e0-b795f911abf1"), new DateTime(2021, 11, 9, 15, 40, 25, 416, DateTimeKind.Local).AddTicks(9642) });

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "Jobs",
                keyColumn: "JobsID",
                keyValue: 1,
                columns: new[] { "CreateDate", "EnableDate", "JobsGuid" },
                values: new object[] { new DateTime(2021, 11, 9, 15, 40, 25, 415, DateTimeKind.Local).AddTicks(2185), new DateTime(2021, 11, 9, 15, 40, 25, 415, DateTimeKind.Local).AddTicks(3420), new Guid("aadba9d4-4ef4-4648-a1c7-392dfaa307c5") });

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "MainCategory",
                keyColumn: "MainCategoryID",
                keyValue: 1,
                columns: new[] { "CreateDate", "EnableDate", "MainCategoryGuid" },
                values: new object[] { new DateTime(2021, 11, 9, 15, 40, 25, 416, DateTimeKind.Local).AddTicks(145), new DateTime(2021, 11, 9, 15, 40, 25, 416, DateTimeKind.Local).AddTicks(1466), new Guid("89d2e674-83d7-4f6d-bb68-c6286e967539") });

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "Nationality",
                keyColumn: "NationalityID",
                keyValue: 1,
                columns: new[] { "CreateDate", "EnableDate", "NationalityGuid" },
                values: new object[] { new DateTime(2021, 11, 9, 15, 40, 25, 412, DateTimeKind.Local).AddTicks(7230), new DateTime(2021, 11, 9, 15, 40, 25, 412, DateTimeKind.Local).AddTicks(8038), new Guid("605eba2c-59c3-4019-b17c-5df56a082d51") });

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "Region",
                keyColumn: "RegionID",
                keyValue: 1,
                columns: new[] { "CreateDate", "EnableDate", "RegionGuid" },
                values: new object[] { new DateTime(2021, 11, 9, 15, 40, 25, 410, DateTimeKind.Local).AddTicks(6903), new DateTime(2021, 11, 9, 15, 40, 25, 410, DateTimeKind.Local).AddTicks(7710), new Guid("31cbed4a-4d04-4ec1-9431-5a623f88faf6") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PickupId",
                schema: "Vendor",
                table: "Vendors");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "79569ecb-53d9-4fe0-93c7-37ac7a9a078e");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "6abaf06e-d50b-41a6-a53f-d7a7e69ed484");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "d50f984e-5b38-4d8f-a47c-183bb90bd01d");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "df5ce988-e8d6-4dc0-b7d7-da6fec641c6d");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "371a586d-df06-41eb-ac80-144915a7e860", "2c135993-7a0f-42fe-a809-13de9739ae0a" });

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "DeliverySetting",
                keyColumn: "DeliverySettingID",
                keyValue: 1,
                column: "DeliverySettingGuid",
                value: new Guid("abcd88f2-4491-43d4-ae15-726affa4dffc"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 1,
                column: "TransactionTypeGuid",
                value: new Guid("4ffa90ae-9b04-4442-bf05-01c72bfeb43a"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 2,
                column: "TransactionTypeGuid",
                value: new Guid("27258add-056a-4b22-bab4-6aab2d7075b2"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 3,
                column: "TransactionTypeGuid",
                value: new Guid("4c68bdfd-5472-4d24-a200-aab27b448221"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 4,
                column: "TransactionTypeGuid",
                value: new Guid("a7525b0c-e085-416c-8a25-2177bda17565"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 5,
                column: "TransactionTypeGuid",
                value: new Guid("e682d15d-36e6-4078-88c1-dfdc0022d4a6"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 6,
                column: "TransactionTypeGuid",
                value: new Guid("3cff959a-6ab8-41ae-8c5d-5270169bf637"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 7,
                column: "TransactionTypeGuid",
                value: new Guid("58afa421-6f81-4905-a38a-b8163939978c"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 8,
                column: "TransactionTypeGuid",
                value: new Guid("5765623a-1a62-4279-98a8-c4020eb854ce"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 9,
                column: "TransactionTypeGuid",
                value: new Guid("0109facb-ecc8-4da2-bbd2-3bc7224c23a9"));

            migrationBuilder.UpdateData(
                schema: "Emp",
                table: "Employees",
                keyColumn: "EntityEmpID",
                keyValue: 1,
                columns: new[] { "CreateDate", "EnableDate" },
                values: new object[] { new DateTime(2021, 11, 8, 16, 52, 29, 881, DateTimeKind.Local).AddTicks(6596), new DateTime(2021, 11, 8, 16, 52, 29, 881, DateTimeKind.Local).AddTicks(7232) });

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionAction",
                keyColumn: "PermissionActionID",
                keyValue: 1,
                column: "PermissionActionGuid",
                value: new Guid("c1eef11a-652c-41dd-ba5d-ae6765268198"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionAction",
                keyColumn: "PermissionActionID",
                keyValue: 2,
                column: "PermissionActionGuid",
                value: new Guid("a7051c48-ea24-4626-bccf-c65b26009769"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionAction",
                keyColumn: "PermissionActionID",
                keyValue: 3,
                column: "PermissionActionGuid",
                value: new Guid("77463cbc-ae77-4930-ab01-81f98e03a7e7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionAction",
                keyColumn: "PermissionActionID",
                keyValue: 4,
                column: "PermissionActionGuid",
                value: new Guid("89db375e-04e7-4a4d-bdce-63c587c498ee"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 1,
                column: "PermissionControllerGuid",
                value: new Guid("3b498fb5-723f-4ae6-9a0b-e21a7f402a73"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 2,
                column: "PermissionControllerGuid",
                value: new Guid("9d8dfeb4-9f9c-4785-9e87-a6a432367401"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 3,
                column: "PermissionControllerGuid",
                value: new Guid("f9f29897-5979-4210-af16-8677e7ccb10d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 4,
                column: "PermissionControllerGuid",
                value: new Guid("08099a3b-2e85-4c75-8555-5c76dfb57d73"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 5,
                column: "PermissionControllerGuid",
                value: new Guid("9dfe55a7-7b54-4c3c-8e56-20ce43d70e75"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 6,
                column: "PermissionControllerGuid",
                value: new Guid("6de43748-5edb-44cb-b2c4-90ddc5df5769"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 7,
                column: "PermissionControllerGuid",
                value: new Guid("4159ff5a-987d-4d90-9ec6-e7f7ca757cd9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 8,
                column: "PermissionControllerGuid",
                value: new Guid("b2c309d6-ad5e-4ff8-b1a7-f54bdfa1012f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 9,
                column: "PermissionControllerGuid",
                value: new Guid("79cd3b20-266f-4bbd-ba88-d046f7a57938"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 10,
                column: "PermissionControllerGuid",
                value: new Guid("933ee5b0-8b6b-4daf-b193-1db319728d6c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 11,
                column: "PermissionControllerGuid",
                value: new Guid("cbaaf48a-1c4a-412e-824b-2f67bcf5f7f7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 12,
                column: "PermissionControllerGuid",
                value: new Guid("02bc8f86-ae67-41b6-9d72-f485f91b2704"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 13,
                column: "PermissionControllerGuid",
                value: new Guid("3ce311e6-a979-47c0-93b8-1535b5d0ede1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 14,
                column: "PermissionControllerGuid",
                value: new Guid("ce4a32e2-c55a-4ee1-88d0-8dfc7bbd6c9d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 15,
                column: "PermissionControllerGuid",
                value: new Guid("85b21845-2ebd-496b-9d14-48a1fd5a9f1f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 16,
                column: "PermissionControllerGuid",
                value: new Guid("03cd03db-a7f1-4048-8d6c-3a5b381cf319"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 17,
                column: "PermissionControllerGuid",
                value: new Guid("21fe6685-3c1a-469f-b430-f2e5d6f0e5f0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 18,
                column: "PermissionControllerGuid",
                value: new Guid("9a803bab-c468-46d8-924b-e47b51c00641"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 19,
                column: "PermissionControllerGuid",
                value: new Guid("aa11514c-3661-4141-9d6c-ecccf5b42304"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 20,
                column: "PermissionControllerGuid",
                value: new Guid("583ef709-86cd-4de3-aeab-48b0621799d1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 21,
                column: "PermissionControllerGuid",
                value: new Guid("afb7b6e8-a8ff-4da8-8f10-2f31e5fe8316"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 22,
                column: "PermissionControllerGuid",
                value: new Guid("e9412022-b560-4c81-a231-fa340e05f2a3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 23,
                column: "PermissionControllerGuid",
                value: new Guid("a96f1c8e-ba17-48c1-a079-5ebf8bf26104"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 24,
                column: "PermissionControllerGuid",
                value: new Guid("30e95496-b81d-447e-880a-ebd51b18e45e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 25,
                column: "PermissionControllerGuid",
                value: new Guid("5bcfaeaa-2951-40e3-9f00-67512e85d621"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 26,
                column: "PermissionControllerGuid",
                value: new Guid("ae6e84db-9061-4d52-9053-bcf759660de4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 27,
                column: "PermissionControllerGuid",
                value: new Guid("f4c16709-3246-48ba-8d44-f8e20efb4b3b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 28,
                column: "PermissionControllerGuid",
                value: new Guid("7e6f3c68-c175-4cad-9153-967345792f20"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 29,
                column: "PermissionControllerGuid",
                value: new Guid("8799f524-0886-4e80-99ca-539a8f9010a2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 30,
                column: "PermissionControllerGuid",
                value: new Guid("150bfd53-4180-4c4e-bea9-14c1d0e0618b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 31,
                column: "PermissionControllerGuid",
                value: new Guid("59a348e2-1690-48e1-a6e4-3c330018e85e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 32,
                column: "PermissionControllerGuid",
                value: new Guid("68f87381-c755-46ba-93ca-14451ef120e1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 33,
                column: "PermissionControllerGuid",
                value: new Guid("d82a601f-3a8b-4489-a8db-2815ff7e86eb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 34,
                column: "PermissionControllerGuid",
                value: new Guid("d1397d05-c29a-425d-8098-3451dcb3a1b9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 35,
                column: "PermissionControllerGuid",
                value: new Guid("61b47111-49a0-42ec-ac62-cbb27a61242e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 36,
                column: "PermissionControllerGuid",
                value: new Guid("7f0362e7-5294-425e-9c6f-0c799435ea31"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 37,
                column: "PermissionControllerGuid",
                value: new Guid("d79bf38e-141b-4a52-9a99-7609907c8939"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 38,
                column: "PermissionControllerGuid",
                value: new Guid("eb056085-4eaa-441f-8cea-c29703f2cfea"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 39,
                column: "PermissionControllerGuid",
                value: new Guid("057c0698-94d9-4803-b14c-28dbc8278fef"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 40,
                column: "PermissionControllerGuid",
                value: new Guid("876e5c1d-c6aa-471d-8881-e1e4b83a3a1b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 41,
                column: "PermissionControllerGuid",
                value: new Guid("b06e3ab6-fa74-4bc8-bb17-c5c2bbad7212"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 42,
                column: "PermissionControllerGuid",
                value: new Guid("6e57f998-2a5b-48bd-b74f-f54f1ca02aec"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 43,
                column: "PermissionControllerGuid",
                value: new Guid("d5a61aa6-a455-452a-a076-03688dd606fe"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 44,
                column: "PermissionControllerGuid",
                value: new Guid("59cde1cd-e4f0-413f-96e9-3edc46586f07"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 45,
                column: "PermissionControllerGuid",
                value: new Guid("de2ec889-f72d-402c-8851-b24f634d2e02"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 46,
                column: "PermissionControllerGuid",
                value: new Guid("10011a66-e6a4-4c8d-926f-622d334da3d0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 47,
                column: "PermissionControllerGuid",
                value: new Guid("39ee787d-61bd-4f16-a881-21c419ecb2ff"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 48,
                column: "PermissionControllerGuid",
                value: new Guid("e875211a-c4ce-457d-a625-f27f129309d0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 49,
                column: "PermissionControllerGuid",
                value: new Guid("5a0dd137-f359-45a0-92fb-75dde8b243c5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 50,
                column: "PermissionControllerGuid",
                value: new Guid("e761370c-cf02-41ea-b388-0bfc202ba819"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 51,
                column: "PermissionControllerGuid",
                value: new Guid("e0275c13-3c3a-4024-b696-cec9a8ecf516"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 52,
                column: "PermissionControllerGuid",
                value: new Guid("579584fa-26b7-42da-8e0e-84745e0ec580"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 53,
                column: "PermissionControllerGuid",
                value: new Guid("69622115-90a8-41e9-b9e5-f42c75035478"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 54,
                column: "PermissionControllerGuid",
                value: new Guid("509fc6c7-4e32-4f15-afe1-98288bd3812d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 55,
                column: "PermissionControllerGuid",
                value: new Guid("c88f85d7-4e37-4f2d-8421-8c9a40d1c34b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 1,
                column: "PermissionControllerActionGuid",
                value: new Guid("f9e25577-4788-44b3-9b6f-94093ffb11a6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 2,
                column: "PermissionControllerActionGuid",
                value: new Guid("c37ccf3b-a1cc-4cb4-9b12-dcdf9c85624a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 3,
                column: "PermissionControllerActionGuid",
                value: new Guid("9731c454-146b-4c38-a95d-32e23a69f87f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 4,
                column: "PermissionControllerActionGuid",
                value: new Guid("a6cbd7eb-1ef8-4e46-8ed5-769de57fc028"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 5,
                column: "PermissionControllerActionGuid",
                value: new Guid("71a9c508-3bfc-444f-890a-0ffc0016ba69"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 6,
                column: "PermissionControllerActionGuid",
                value: new Guid("8b3554a8-92ff-40c0-9e79-e83921daafe4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 7,
                column: "PermissionControllerActionGuid",
                value: new Guid("f4de0a68-517c-4c89-8c59-8084bad5a4c7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 8,
                column: "PermissionControllerActionGuid",
                value: new Guid("d85df17f-a72b-4688-94ef-37471744c208"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 9,
                column: "PermissionControllerActionGuid",
                value: new Guid("5eba1ad3-3071-4982-9db6-0bb5fdf248c6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 10,
                column: "PermissionControllerActionGuid",
                value: new Guid("2968e624-d319-4728-b766-e5ee4642b7b2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 11,
                column: "PermissionControllerActionGuid",
                value: new Guid("6c066cb8-4e2d-4339-8c34-5f9f735e07a4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 12,
                column: "PermissionControllerActionGuid",
                value: new Guid("483c59c5-6c9f-4101-8f0a-d72d6812a329"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 13,
                column: "PermissionControllerActionGuid",
                value: new Guid("57baf409-1588-4ea3-8edc-0bc1be52f72a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 14,
                column: "PermissionControllerActionGuid",
                value: new Guid("f30a029e-1535-4dd9-8aff-8d4debb3603b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 15,
                column: "PermissionControllerActionGuid",
                value: new Guid("aa98a507-14f8-49f5-b076-2f829fbc03a4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 16,
                column: "PermissionControllerActionGuid",
                value: new Guid("9feb28ba-7943-4d8c-87dd-0a14ad144ffd"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 17,
                column: "PermissionControllerActionGuid",
                value: new Guid("a2de6320-9642-4048-a41f-3213b4bbcb18"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 18,
                column: "PermissionControllerActionGuid",
                value: new Guid("519b91c7-60c1-4580-a4bb-e10f00ec380a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 19,
                column: "PermissionControllerActionGuid",
                value: new Guid("e57104c1-0e5e-48c3-99b2-43464cb370dd"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 20,
                column: "PermissionControllerActionGuid",
                value: new Guid("4111bc01-7d50-4cdb-854f-1aae761921e5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 21,
                column: "PermissionControllerActionGuid",
                value: new Guid("ca871dae-6208-46d0-b057-0c331e1f821b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 22,
                column: "PermissionControllerActionGuid",
                value: new Guid("865b5fc5-5f52-4e76-a9c4-ce5db4a450b0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 23,
                column: "PermissionControllerActionGuid",
                value: new Guid("3e35ba8e-40a9-49c2-b3cd-ed4788190fb2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 24,
                column: "PermissionControllerActionGuid",
                value: new Guid("fbbb7dfb-d742-4ea2-947f-1e3b60f8212f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 25,
                column: "PermissionControllerActionGuid",
                value: new Guid("ce4f8e2b-6391-4bd2-9f9b-940a5c691d0c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 26,
                column: "PermissionControllerActionGuid",
                value: new Guid("3ea4e78c-f500-41ad-a6b0-3eac3bf5bbd0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 27,
                column: "PermissionControllerActionGuid",
                value: new Guid("071d39a7-9176-45b1-9695-922bace43cab"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 28,
                column: "PermissionControllerActionGuid",
                value: new Guid("976dc587-7aa1-4383-8f69-cb6c30fa435b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 29,
                column: "PermissionControllerActionGuid",
                value: new Guid("d3b6daad-51b9-4968-a16e-3f76fdc4bb00"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 30,
                column: "PermissionControllerActionGuid",
                value: new Guid("90da79cc-34d8-41be-bb39-f84db70cebc2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 31,
                column: "PermissionControllerActionGuid",
                value: new Guid("b494b1e8-b536-4050-97dc-969d31f6d2de"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 32,
                column: "PermissionControllerActionGuid",
                value: new Guid("7b29f138-ef61-4f5b-a3a3-450577956343"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 33,
                column: "PermissionControllerActionGuid",
                value: new Guid("7cebcb32-f41e-478f-98b2-4b9745790b0f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 34,
                column: "PermissionControllerActionGuid",
                value: new Guid("5aba6b99-5ab9-40f8-9f12-dcff7f60652e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 35,
                column: "PermissionControllerActionGuid",
                value: new Guid("8f5627b5-d7d3-4e31-9405-19329110b6ce"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 36,
                column: "PermissionControllerActionGuid",
                value: new Guid("bdac02f0-b384-40f4-a2ae-1e4a61a911c0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 37,
                column: "PermissionControllerActionGuid",
                value: new Guid("2bd42eda-35a2-44b1-b0ff-7a16bbb643de"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 38,
                column: "PermissionControllerActionGuid",
                value: new Guid("f2ed6d8b-5d00-4366-908a-126f5ec5477d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 39,
                column: "PermissionControllerActionGuid",
                value: new Guid("db2a7344-e5f1-492f-a51b-6dbe75e779c4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 40,
                column: "PermissionControllerActionGuid",
                value: new Guid("39db0bac-f12b-4336-8bc5-c86c14943541"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 41,
                column: "PermissionControllerActionGuid",
                value: new Guid("180ec279-7a00-40b9-9ab9-c3d51bf12965"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 42,
                column: "PermissionControllerActionGuid",
                value: new Guid("ac18a856-8732-4780-ba6c-a37ec27cbdc9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 43,
                column: "PermissionControllerActionGuid",
                value: new Guid("6c8f92ef-5717-4779-8116-d4ffa6e387cb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 44,
                column: "PermissionControllerActionGuid",
                value: new Guid("fb6293de-5583-4258-8123-be24af5d0e65"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 45,
                column: "PermissionControllerActionGuid",
                value: new Guid("295cae26-fa4d-4553-8632-bf08b34ec906"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 46,
                column: "PermissionControllerActionGuid",
                value: new Guid("b042e50d-2d5e-41e3-b2fc-e9fb56cbdcf2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 47,
                column: "PermissionControllerActionGuid",
                value: new Guid("6c2a9ca3-9553-419c-81bb-58081f640de4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 48,
                column: "PermissionControllerActionGuid",
                value: new Guid("07b7c321-2006-44e4-8082-2f77b61da868"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 49,
                column: "PermissionControllerActionGuid",
                value: new Guid("8c00c641-52c0-4075-86fa-e8003c5d4457"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 50,
                column: "PermissionControllerActionGuid",
                value: new Guid("d94823bf-e73f-4b13-a32d-ff813520141d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 51,
                column: "PermissionControllerActionGuid",
                value: new Guid("fc8fd6f8-4e98-4701-84ce-7d60d711f54f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 52,
                column: "PermissionControllerActionGuid",
                value: new Guid("0543b001-df5d-449b-be7c-413df65e517d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 53,
                column: "PermissionControllerActionGuid",
                value: new Guid("a4733696-63e6-4738-a573-b029b99d79fb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 54,
                column: "PermissionControllerActionGuid",
                value: new Guid("babc2084-1aed-474a-aa4d-ef45c40625b5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 55,
                column: "PermissionControllerActionGuid",
                value: new Guid("40542c49-4337-40eb-944b-45359b651898"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 56,
                column: "PermissionControllerActionGuid",
                value: new Guid("2ea7bd03-8308-4399-86d0-cbbdf1e2ac18"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 57,
                column: "PermissionControllerActionGuid",
                value: new Guid("3de173f4-08d2-4d64-b55a-97ed7fee3ad8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 58,
                column: "PermissionControllerActionGuid",
                value: new Guid("fb175cd5-9040-4b34-8abb-3622cfa34a44"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 59,
                column: "PermissionControllerActionGuid",
                value: new Guid("e148f130-03a5-4818-bcca-b86380415b89"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 60,
                column: "PermissionControllerActionGuid",
                value: new Guid("9c753391-4656-43f4-9d36-00cc14065f57"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 61,
                column: "PermissionControllerActionGuid",
                value: new Guid("f25b3a30-8c9e-41aa-a146-d65a7cfb05d6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 62,
                column: "PermissionControllerActionGuid",
                value: new Guid("4fb2c7e7-8f2f-4cc2-982b-c295e0768ea1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 63,
                column: "PermissionControllerActionGuid",
                value: new Guid("8f40ea55-d9b3-4962-a24e-b67ce675cddd"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 64,
                column: "PermissionControllerActionGuid",
                value: new Guid("4098298f-496c-4223-a29b-768d713a66f7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 65,
                column: "PermissionControllerActionGuid",
                value: new Guid("0b44e5f5-523b-4cc5-b3de-c3fec7df3c7d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 66,
                column: "PermissionControllerActionGuid",
                value: new Guid("8a3f4708-cf7e-40dd-a44a-4a2aa283f012"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 67,
                column: "PermissionControllerActionGuid",
                value: new Guid("17944cec-078c-4821-9f1c-fe71e74f58b5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 68,
                column: "PermissionControllerActionGuid",
                value: new Guid("f2201741-4a28-4d69-b420-5c199d9b2f5e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 69,
                column: "PermissionControllerActionGuid",
                value: new Guid("bdf930f0-af72-4a14-8767-36e2c57c0d00"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 70,
                column: "PermissionControllerActionGuid",
                value: new Guid("8658ebd0-5fe0-44aa-bfd0-3bb441173252"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 71,
                column: "PermissionControllerActionGuid",
                value: new Guid("51568e3b-d492-48ce-9053-a19f8b8dfae9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 72,
                column: "PermissionControllerActionGuid",
                value: new Guid("989a9fea-6bb7-4398-9e50-74776c701bdf"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 73,
                column: "PermissionControllerActionGuid",
                value: new Guid("3a5bcf86-3d00-49e5-a6e0-fb35f322e22f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 74,
                column: "PermissionControllerActionGuid",
                value: new Guid("c175f3b0-bcad-4e81-8576-7b897868325c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 75,
                column: "PermissionControllerActionGuid",
                value: new Guid("e3155b07-91e2-46f3-8e32-bcad8fd718c5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 76,
                column: "PermissionControllerActionGuid",
                value: new Guid("672f0aa2-1994-4d1c-9588-b914798edcca"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 77,
                column: "PermissionControllerActionGuid",
                value: new Guid("88bc0348-14f8-44cd-80ae-5c9123c3c1dc"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 78,
                column: "PermissionControllerActionGuid",
                value: new Guid("d7bdcc80-ce89-4533-92cc-18871e115779"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 79,
                column: "PermissionControllerActionGuid",
                value: new Guid("9746a4fe-7d59-4808-8913-8736e0686504"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 80,
                column: "PermissionControllerActionGuid",
                value: new Guid("e3c2cfd0-2cd5-482f-bfd8-a6022d3652c4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 81,
                column: "PermissionControllerActionGuid",
                value: new Guid("003637a0-36f7-48d9-be1d-c5214d4e85da"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 82,
                column: "PermissionControllerActionGuid",
                value: new Guid("0ba52cfb-bbd0-4675-bf09-af0060a4650f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 83,
                column: "PermissionControllerActionGuid",
                value: new Guid("16dfa95b-1738-4560-80b5-939ca5e63c30"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 84,
                column: "PermissionControllerActionGuid",
                value: new Guid("8783eda5-3d5b-4fbe-af68-cdd2cd2239e1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 85,
                column: "PermissionControllerActionGuid",
                value: new Guid("4ce1b2b0-e3a3-4cc0-aed7-2ec8cb0160c2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 86,
                column: "PermissionControllerActionGuid",
                value: new Guid("dd92d20d-e965-4b37-9bed-5f24203376fb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 87,
                column: "PermissionControllerActionGuid",
                value: new Guid("c93f4e42-a005-4da6-9778-19bf1b7fef96"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 88,
                column: "PermissionControllerActionGuid",
                value: new Guid("7865620d-93ed-4a6a-ae29-9ce1f992fe2e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 89,
                column: "PermissionControllerActionGuid",
                value: new Guid("e5eaf5c9-2a4b-470a-b2ec-eddc23bd74f6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 90,
                column: "PermissionControllerActionGuid",
                value: new Guid("dbd893f1-2fdd-4d79-9c47-63db1f80c29e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 91,
                column: "PermissionControllerActionGuid",
                value: new Guid("6c78691a-5e84-4020-8a32-848e8df1065a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 92,
                column: "PermissionControllerActionGuid",
                value: new Guid("820ffbb6-4dce-40a3-935a-890f181650a0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 93,
                column: "PermissionControllerActionGuid",
                value: new Guid("e0f8a8ed-0530-4952-ac90-d251df49a7df"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 94,
                column: "PermissionControllerActionGuid",
                value: new Guid("c23fc07c-8a26-44d2-9b1a-81f00d640417"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 95,
                column: "PermissionControllerActionGuid",
                value: new Guid("6b44ac05-8d21-4ec5-a67f-8e67ee4bfa24"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 96,
                column: "PermissionControllerActionGuid",
                value: new Guid("5fc77e21-3b73-4666-af71-76170b5f0481"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 97,
                column: "PermissionControllerActionGuid",
                value: new Guid("24db2068-e9d5-4921-b39b-ffd9a1650c60"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 98,
                column: "PermissionControllerActionGuid",
                value: new Guid("73f6bb20-8460-498a-834a-c4c666c8d7d3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 99,
                column: "PermissionControllerActionGuid",
                value: new Guid("985b31ab-028f-4b3a-8cf6-c3b915f1536e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 100,
                column: "PermissionControllerActionGuid",
                value: new Guid("ee83e373-7e17-4594-9117-d6724db50051"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 101,
                column: "PermissionControllerActionGuid",
                value: new Guid("89f73610-1eba-4c2e-b0fe-a9ef3f05b30a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 102,
                column: "PermissionControllerActionGuid",
                value: new Guid("713bb6f7-13ca-4968-98de-4806b6b33cab"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 103,
                column: "PermissionControllerActionGuid",
                value: new Guid("50634a01-d749-43f7-a7ae-ce1ed340c76f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 104,
                column: "PermissionControllerActionGuid",
                value: new Guid("4d6d9aa6-f870-4ca9-b9bd-c60f532c3796"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 105,
                column: "PermissionControllerActionGuid",
                value: new Guid("bdb2eb01-52e6-4f67-bf66-31af510a8111"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 106,
                column: "PermissionControllerActionGuid",
                value: new Guid("b3f93a8b-7250-41c2-a9f6-e13994874026"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 107,
                column: "PermissionControllerActionGuid",
                value: new Guid("5f9994f6-8cea-44cf-8168-a0f4e2637033"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 108,
                column: "PermissionControllerActionGuid",
                value: new Guid("0437f272-dd9a-42b8-baaf-2b2b5633cb6d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 109,
                column: "PermissionControllerActionGuid",
                value: new Guid("2deacd61-0b7a-43d5-aeed-9c807a671904"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 110,
                column: "PermissionControllerActionGuid",
                value: new Guid("c5003137-77d8-444f-b3ab-cb4bc189d6c7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 111,
                column: "PermissionControllerActionGuid",
                value: new Guid("70299e48-bcab-49d2-8618-d09813d603df"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 112,
                column: "PermissionControllerActionGuid",
                value: new Guid("b39dbadb-3ace-4e43-a86d-136c40445fde"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 113,
                column: "PermissionControllerActionGuid",
                value: new Guid("fccfda5b-7a02-41a1-9514-837a0ad98be1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 114,
                column: "PermissionControllerActionGuid",
                value: new Guid("5cffd021-e535-4c4a-b42d-4dc24d4b3245"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 115,
                column: "PermissionControllerActionGuid",
                value: new Guid("0952b141-59af-4846-875d-b664ef52e742"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 116,
                column: "PermissionControllerActionGuid",
                value: new Guid("939d3b50-4bb3-49e5-bce0-bb2c4c35a738"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 117,
                column: "PermissionControllerActionGuid",
                value: new Guid("2c2863e2-76c3-47aa-9aec-18c8ea71d807"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 118,
                column: "PermissionControllerActionGuid",
                value: new Guid("8bf07f48-9fd7-40f4-86a2-8e2f4b34b4a1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 119,
                column: "PermissionControllerActionGuid",
                value: new Guid("7fea3e15-652e-440c-94e5-dc89c0a871de"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 120,
                column: "PermissionControllerActionGuid",
                value: new Guid("54ea60e1-52b4-40bb-965b-39544aec4e38"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 121,
                column: "PermissionControllerActionGuid",
                value: new Guid("0af143f1-9637-4c26-b46d-bd8701ee069e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 122,
                column: "PermissionControllerActionGuid",
                value: new Guid("545a03f0-1618-4583-b1e5-00df6a071f15"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 123,
                column: "PermissionControllerActionGuid",
                value: new Guid("be15fadd-8454-485e-a762-3562669ebca5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 124,
                column: "PermissionControllerActionGuid",
                value: new Guid("055f0b1e-b56f-49ac-8d73-a8d077afd994"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 125,
                column: "PermissionControllerActionGuid",
                value: new Guid("130ec415-3f66-45f6-b90b-42735fc5d008"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 126,
                column: "PermissionControllerActionGuid",
                value: new Guid("3cdce8c3-ad6a-4b5a-b056-3bd69de30a57"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 127,
                column: "PermissionControllerActionGuid",
                value: new Guid("c0c61353-3c46-45bf-bccf-4c5ba17d7413"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 128,
                column: "PermissionControllerActionGuid",
                value: new Guid("b48d5f2a-dd3b-4494-a757-1f4a8d7e4deb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 129,
                column: "PermissionControllerActionGuid",
                value: new Guid("cb82d809-b418-4619-a8d2-0d7bdc37a04a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 130,
                column: "PermissionControllerActionGuid",
                value: new Guid("594eb9f5-2d2e-409d-a7fb-63379845de22"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 131,
                column: "PermissionControllerActionGuid",
                value: new Guid("7e656d6a-3ab0-4b13-9c69-d4d7cc1eff40"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 132,
                column: "PermissionControllerActionGuid",
                value: new Guid("8aa7910e-871a-4f8a-9346-6e777375b1b9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 133,
                column: "PermissionControllerActionGuid",
                value: new Guid("61438a6d-c0fe-4163-987c-de3423ecbc0b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 134,
                column: "PermissionControllerActionGuid",
                value: new Guid("b3056d36-cc73-44fc-b807-14134c7e19b4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 135,
                column: "PermissionControllerActionGuid",
                value: new Guid("d271684a-345e-4e76-a5af-38211ae9bd03"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 136,
                column: "PermissionControllerActionGuid",
                value: new Guid("ba47bacb-a25e-458b-bc38-5f507587405b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 137,
                column: "PermissionControllerActionGuid",
                value: new Guid("6e00405d-97fe-4c7e-93f6-150c8f0b464d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 138,
                column: "PermissionControllerActionGuid",
                value: new Guid("7c31cd3c-7027-445e-a270-c307cb922755"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 139,
                column: "PermissionControllerActionGuid",
                value: new Guid("5cf6b6e2-6c19-4f3f-87cb-a0e0f3630be6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 140,
                column: "PermissionControllerActionGuid",
                value: new Guid("f0e93661-e4df-417d-8b88-dec154e56134"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 141,
                column: "PermissionControllerActionGuid",
                value: new Guid("db181c91-05a3-40bb-b815-128c8b48a3e6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 142,
                column: "PermissionControllerActionGuid",
                value: new Guid("6f56dc50-7650-4774-b4e0-eaaaa2a8ac27"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 143,
                column: "PermissionControllerActionGuid",
                value: new Guid("3ff95c4d-dc39-4a2b-8d14-d8c124e945aa"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 144,
                column: "PermissionControllerActionGuid",
                value: new Guid("cb506bff-8e0d-44fd-83cc-5440ef5f3d3a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 145,
                column: "PermissionControllerActionGuid",
                value: new Guid("f99fcfa8-3a80-4e46-81fe-7f50a3dfd1a3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 146,
                column: "PermissionControllerActionGuid",
                value: new Guid("6f3fe7f9-da85-48a8-8ac4-f2bf6e0f2223"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 147,
                column: "PermissionControllerActionGuid",
                value: new Guid("b362a77a-1fb0-47b9-b928-98f111341a80"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 148,
                column: "PermissionControllerActionGuid",
                value: new Guid("3c9a6253-7f18-40e5-8b39-b45b39c505eb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 149,
                column: "PermissionControllerActionGuid",
                value: new Guid("fbca4e7f-6195-44cf-adef-2e60ff0de407"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 150,
                column: "PermissionControllerActionGuid",
                value: new Guid("f67deaa5-6384-4210-b566-85e5e39c8c22"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 151,
                column: "PermissionControllerActionGuid",
                value: new Guid("12966f35-5d2e-499a-9cf3-1d4dd5279efa"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 152,
                column: "PermissionControllerActionGuid",
                value: new Guid("e5fb712e-3a86-4f0c-bd2c-84044c9ca8df"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 153,
                column: "PermissionControllerActionGuid",
                value: new Guid("f3cc0b31-e3c7-4240-8c73-97cb492292dd"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 154,
                column: "PermissionControllerActionGuid",
                value: new Guid("1386fca3-8f90-488e-9e26-b67aaf0d59cc"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 155,
                column: "PermissionControllerActionGuid",
                value: new Guid("8f7fbbcc-1c11-48fb-acda-bdc6a88a9c91"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 156,
                column: "PermissionControllerActionGuid",
                value: new Guid("15601956-e5dc-4e70-b378-596237f65e8d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 157,
                column: "PermissionControllerActionGuid",
                value: new Guid("e8714ba2-7c5b-43c1-a5ca-c3a7dd960458"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 158,
                column: "PermissionControllerActionGuid",
                value: new Guid("31292843-1fc8-410b-98e2-5aa3c7ede735"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 159,
                column: "PermissionControllerActionGuid",
                value: new Guid("0e8daa62-1bee-4cd2-becc-e702ad120857"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 160,
                column: "PermissionControllerActionGuid",
                value: new Guid("79b27902-2cc5-4232-bb6a-312060473f4a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 161,
                column: "PermissionControllerActionGuid",
                value: new Guid("9db0ea59-2977-4349-a15c-b7617b936a85"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 162,
                column: "PermissionControllerActionGuid",
                value: new Guid("cab47cbd-6c7c-4104-9942-5e1b70598243"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 163,
                column: "PermissionControllerActionGuid",
                value: new Guid("4836e2a2-da23-44f6-bc4c-9209c98ea6ab"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 164,
                column: "PermissionControllerActionGuid",
                value: new Guid("c3ecdc8f-a943-49f8-b872-cf2cf9179b1a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 165,
                column: "PermissionControllerActionGuid",
                value: new Guid("e6ea28c6-16f2-4637-875d-a814d3101cb1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 166,
                column: "PermissionControllerActionGuid",
                value: new Guid("e3726ed8-aa50-4a38-b8d3-4b33aab1735c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 167,
                column: "PermissionControllerActionGuid",
                value: new Guid("f56ed4c3-0175-4ddf-8aed-b3ef55fe532a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 168,
                column: "PermissionControllerActionGuid",
                value: new Guid("73c54498-e339-4a26-a9a3-ca836d33a8e8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 169,
                column: "PermissionControllerActionGuid",
                value: new Guid("e84f8d63-dbd7-407d-891f-8822f010fb05"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 170,
                column: "PermissionControllerActionGuid",
                value: new Guid("49cc8a5f-1fa6-47df-aadb-9aae23764a41"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 171,
                column: "PermissionControllerActionGuid",
                value: new Guid("5261e3eb-0c07-4d2f-8200-80dfa8dd0eeb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 172,
                column: "PermissionControllerActionGuid",
                value: new Guid("1c4d3421-7261-4bb9-b38c-32fa7543351a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 173,
                column: "PermissionControllerActionGuid",
                value: new Guid("87c981fd-4099-46d3-b37d-496819d558d5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 174,
                column: "PermissionControllerActionGuid",
                value: new Guid("980dc9a9-2e8e-4602-8011-dfd8d4293491"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 175,
                column: "PermissionControllerActionGuid",
                value: new Guid("0fa26bf3-4070-476f-a4a8-f295c4354f6b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 176,
                column: "PermissionControllerActionGuid",
                value: new Guid("b9ec6fd4-4d37-4fce-bbe2-5678fc3978b5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 177,
                column: "PermissionControllerActionGuid",
                value: new Guid("47d3ca60-39f9-43d9-9b51-b907a4d65eca"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 178,
                column: "PermissionControllerActionGuid",
                value: new Guid("880b64d0-311b-418e-b275-9161db18f2e5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 179,
                column: "PermissionControllerActionGuid",
                value: new Guid("bbadedbb-977a-45ed-aff1-bd9071476008"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 180,
                column: "PermissionControllerActionGuid",
                value: new Guid("56b72af6-89f0-4377-8f13-8ce5077c7990"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 181,
                column: "PermissionControllerActionGuid",
                value: new Guid("f6d42a2c-f5bb-4359-be1b-b8ef833e6b6b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 182,
                column: "PermissionControllerActionGuid",
                value: new Guid("4efc1df7-e383-4c8e-be58-4851c283de3a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 183,
                column: "PermissionControllerActionGuid",
                value: new Guid("9ab51797-39c8-4ed5-96ec-18db09076436"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 184,
                column: "PermissionControllerActionGuid",
                value: new Guid("2b886f45-5c32-40f9-b8a8-09bf608a3926"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 185,
                column: "PermissionControllerActionGuid",
                value: new Guid("2b674849-d64b-4643-8746-686aa2e9912c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 186,
                column: "PermissionControllerActionGuid",
                value: new Guid("08fdcc23-9689-4361-88f2-11263b2f3c86"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 187,
                column: "PermissionControllerActionGuid",
                value: new Guid("a231c361-af1f-482b-b7d0-315d5d4391bc"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 188,
                column: "PermissionControllerActionGuid",
                value: new Guid("85b43a7d-31d8-42fd-bd6b-b06273ed3152"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 189,
                column: "PermissionControllerActionGuid",
                value: new Guid("0f20b8b5-f585-4147-afbd-64c04f61f616"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 190,
                column: "PermissionControllerActionGuid",
                value: new Guid("75613160-399e-4eb7-87b9-053aaca35faf"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 191,
                column: "PermissionControllerActionGuid",
                value: new Guid("c777a9a1-dff3-4e3b-9caa-4174b3a62e5d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 192,
                column: "PermissionControllerActionGuid",
                value: new Guid("50c8ac00-33cd-4e28-808e-d9286c206ae7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 193,
                column: "PermissionControllerActionGuid",
                value: new Guid("cd96e550-ebeb-4391-8b63-dce4d6aa3afc"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 194,
                column: "PermissionControllerActionGuid",
                value: new Guid("251925aa-47f3-4e9d-9c27-59818aeaeecf"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 195,
                column: "PermissionControllerActionGuid",
                value: new Guid("d0cfc4f4-e1a4-4c15-982e-ddcf38a5d94d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 196,
                column: "PermissionControllerActionGuid",
                value: new Guid("362fc50f-ed1b-4e26-bbf0-a8bcb3aa934a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 197,
                column: "PermissionControllerActionGuid",
                value: new Guid("1f28c3e6-3895-411f-a044-45e8edb94c44"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 198,
                column: "PermissionControllerActionGuid",
                value: new Guid("836ae8da-dafe-4035-b134-641935ca9645"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 199,
                column: "PermissionControllerActionGuid",
                value: new Guid("123b5398-dae0-4c2c-989c-38b532d392a9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 200,
                column: "PermissionControllerActionGuid",
                value: new Guid("80192687-a70d-46ca-8595-123b07e08016"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 201,
                column: "PermissionControllerActionGuid",
                value: new Guid("df5ce452-2f30-42b0-9fed-a0e164afc816"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 202,
                column: "PermissionControllerActionGuid",
                value: new Guid("d60af999-48d9-4dbf-a9cc-268a5c63e2e3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 203,
                column: "PermissionControllerActionGuid",
                value: new Guid("31ddd43f-0cd2-406b-af99-14725d90b59d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 204,
                column: "PermissionControllerActionGuid",
                value: new Guid("8b314a88-cf48-4681-b567-0955376394b0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 205,
                column: "PermissionControllerActionGuid",
                value: new Guid("a39ce256-2dc0-49f8-9aed-9df896050adc"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 206,
                column: "PermissionControllerActionGuid",
                value: new Guid("157f7014-bff7-4f3a-b0bf-bb9113cdfe30"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 207,
                column: "PermissionControllerActionGuid",
                value: new Guid("cf6cbee7-caba-43d5-be52-0ad7a0885f82"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 208,
                column: "PermissionControllerActionGuid",
                value: new Guid("a5423fb9-89ea-4199-823e-5549500eb8c4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 209,
                column: "PermissionControllerActionGuid",
                value: new Guid("7bbd5cfe-015f-426f-8007-281c0dd3238a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 210,
                column: "PermissionControllerActionGuid",
                value: new Guid("179cddf9-930e-4f89-82db-e1c8c23e518d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 211,
                column: "PermissionControllerActionGuid",
                value: new Guid("86abdf91-bacc-4e1c-b8b3-30f463841181"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 212,
                column: "PermissionControllerActionGuid",
                value: new Guid("e37ecc8e-3623-47a3-8066-a866677f0d2d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 213,
                column: "PermissionControllerActionGuid",
                value: new Guid("85c186da-9511-4de1-8ec7-f661e73e9400"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 214,
                column: "PermissionControllerActionGuid",
                value: new Guid("9cfca132-0675-486d-9e04-c9d38ca11411"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 215,
                column: "PermissionControllerActionGuid",
                value: new Guid("77c4c7b1-5658-4dc1-80d2-3d96d50e63c6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 216,
                column: "PermissionControllerActionGuid",
                value: new Guid("b19e35f3-7b66-4d4b-a750-1227942b3a2e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 217,
                column: "PermissionControllerActionGuid",
                value: new Guid("b18e7aba-1c26-4fc1-b6ba-99b43d444530"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 218,
                column: "PermissionControllerActionGuid",
                value: new Guid("fb232f49-b5a5-4dcc-a46f-2f341aab3d9d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 219,
                column: "PermissionControllerActionGuid",
                value: new Guid("42b56bff-2629-4666-b508-33039e782fc7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 220,
                column: "PermissionControllerActionGuid",
                value: new Guid("df814000-09dc-4aba-8795-14660fcdc475"));

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "City",
                keyColumn: "CityID",
                keyValue: 1,
                columns: new[] { "CityGuid", "CreateDate", "EnableDate" },
                values: new object[] { new Guid("01107c51-4d7b-421e-8f2b-e41502bd5656"), new DateTime(2021, 11, 8, 16, 52, 29, 874, DateTimeKind.Local).AddTicks(3373), new DateTime(2021, 11, 8, 16, 52, 29, 874, DateTimeKind.Local).AddTicks(4592) });

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "Country",
                keyColumn: "CountryID",
                keyValue: 1,
                columns: new[] { "CountryGuid", "CreateDate", "EnableDate" },
                values: new object[] { new Guid("cec049a0-80d4-448d-b029-23d6f563cf5a"), new DateTime(2021, 11, 8, 16, 52, 29, 870, DateTimeKind.Local).AddTicks(401), new DateTime(2021, 11, 8, 16, 52, 29, 871, DateTimeKind.Local).AddTicks(1616) });

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "Departments",
                keyColumn: "DepartmentsID",
                keyValue: 1,
                columns: new[] { "CreateDate", "DepartmentsGuid", "EnableDate" },
                values: new object[] { new DateTime(2021, 11, 8, 16, 52, 29, 880, DateTimeKind.Local).AddTicks(2188), new Guid("88323fc3-252d-4696-a7e1-0dd4f8944773"), new DateTime(2021, 11, 8, 16, 52, 29, 880, DateTimeKind.Local).AddTicks(3294) });

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "Jobs",
                keyColumn: "JobsID",
                keyValue: 1,
                columns: new[] { "CreateDate", "EnableDate", "JobsGuid" },
                values: new object[] { new DateTime(2021, 11, 8, 16, 52, 29, 878, DateTimeKind.Local).AddTicks(6034), new DateTime(2021, 11, 8, 16, 52, 29, 878, DateTimeKind.Local).AddTicks(7138), new Guid("f81f0746-e002-4480-82a2-9bcb10cd0ba4") });

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "MainCategory",
                keyColumn: "MainCategoryID",
                keyValue: 1,
                columns: new[] { "CreateDate", "EnableDate", "MainCategoryGuid" },
                values: new object[] { new DateTime(2021, 11, 8, 16, 52, 29, 879, DateTimeKind.Local).AddTicks(3072), new DateTime(2021, 11, 8, 16, 52, 29, 879, DateTimeKind.Local).AddTicks(4446), new Guid("0def749b-2f42-4246-9aba-f81ac28f3e64") });

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "Nationality",
                keyColumn: "NationalityID",
                keyValue: 1,
                columns: new[] { "CreateDate", "EnableDate", "NationalityGuid" },
                values: new object[] { new DateTime(2021, 11, 8, 16, 52, 29, 875, DateTimeKind.Local).AddTicks(7059), new DateTime(2021, 11, 8, 16, 52, 29, 875, DateTimeKind.Local).AddTicks(7846), new Guid("372ac3ba-cdca-451d-9d07-bdc865afde6e") });

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "Region",
                keyColumn: "RegionID",
                keyValue: 1,
                columns: new[] { "CreateDate", "EnableDate", "RegionGuid" },
                values: new object[] { new DateTime(2021, 11, 8, 16, 52, 29, 872, DateTimeKind.Local).AddTicks(3324), new DateTime(2021, 11, 8, 16, 52, 29, 872, DateTimeKind.Local).AddTicks(4269), new Guid("8ec417e0-0cdc-4e1b-b760-dbe3cd2bde8f") });
        }
    }
}
