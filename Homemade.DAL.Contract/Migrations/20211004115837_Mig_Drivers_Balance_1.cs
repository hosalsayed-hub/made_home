using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Homemade.DAL.Contract.Migrations
{
    public partial class Mig_Drivers_Balance_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DriverCharge",
                schema: "Order",
                table: "OrderVendor",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsPaidDriver",
                schema: "Order",
                table: "OrderVendor",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<decimal>(
                name: "Before",
                schema: "Driver",
                table: "DriverBlance",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                schema: "Driver",
                table: "DriverBlance",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "After",
                schema: "Driver",
                table: "DriverBlance",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.CreateTable(
                name: "DeliverySetting",
                schema: "Driver",
                columns: table => new
                {
                    DeliverySettingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeliverySettingGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DriverCommision = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BaseFare = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MinKM = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OverKmFare = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsEnable = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EnableDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UserUpdate = table.Column<int>(type: "int", nullable: true),
                    UserDelete = table.Column<int>(type: "int", nullable: true),
                    UserEnable = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliverySetting", x => x.DeliverySettingID);
                    table.ForeignKey(
                        name: "FK_DeliverySetting_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            //migrationBuilder.InsertData(
            //    table: "Role",
            //    columns: new[] { "RoleId", "ConcurrencyStamp", "Name", "NormalizedName", "RoleTypeId" },
            //    values: new object[,]
            //    {
            //        { 1, "159f8c3f-54db-4911-8e68-045390ee92e9", "Admin", null, 1 },
            //        { 2, "52a65b36-ecd5-42f5-a724-6619c5c63a73", "Vendor", null, 2 },
            //        { 3, "ebb5248d-4a7d-4730-bc44-d3900e274e08", "Customer", null, 2 },
            //        { 4, "0898144c-e416-433f-8d88-43766c4ebbd8", "Employee", null, 2 }
            //    });

            //migrationBuilder.InsertData(
            //    table: "User",
            //    columns: new[] { "UserId", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserJWTToken", "UserName", "UserType" },
            //    values: new object[] { 1, 0, "6ca388be-12ca-4a40-bd7b-965fbb67752f", "SystemUser@Admin.com", true, false, null, "SystemUser@Admin.com", "SYSTEMUSER", "AQAAAAEAACcQAAAAEP65QXLX6e94ehLc9ntv07Q7n/aO6wf8y6j/z15XE7hfgyZLCNvHmM3Ar6SaTwzC3g==", "012", true, "753bd2cb-e913-4500-b4e5-13266c4cf40e", false, null, "SystemUser", 1 });

            //migrationBuilder.InsertData(
            //    schema: "Permission",
            //    table: "PermissionAction",
            //    columns: new[] { "PermissionActionID", "PermissionActionGuid", "PermissionActionNameAr", "PermissionActionNameEn" },
            //    values: new object[,]
            //    {
            //        { 1, new Guid("b718b31f-3c38-4d47-a794-b5e6e5fa5d69"), "عرض", "View" },
            //        { 2, new Guid("316b1ab0-738c-46e5-8f7f-88705022a16c"), "اضافة", "Insert" },
            //        { 3, new Guid("aa5a3ef0-3515-459b-8a3e-2afa78e30530"), "تعديل", "Update" },
            //        { 4, new Guid("42a9dbea-15ca-4ea3-9559-b92ab891b6b0"), "حذف", "Delete" }
            //    });

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 1,
                column: "PermissionControllerGuid",
                value: new Guid("c7800f1a-20ef-409b-a967-e120d1cd35b7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 2,
                column: "PermissionControllerGuid",
                value: new Guid("2348bd01-aad0-4248-8ca7-5120de89454e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 3,
                column: "PermissionControllerGuid",
                value: new Guid("2a49e584-4adf-4211-8573-b8229857891d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 4,
                column: "PermissionControllerGuid",
                value: new Guid("2c3e5726-886a-4dc0-8e1c-7bb75e8ba67d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 5,
                column: "PermissionControllerGuid",
                value: new Guid("a2c8cc30-371b-4b12-8337-72d3dc3b9089"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 6,
                column: "PermissionControllerGuid",
                value: new Guid("56a7b670-9e38-4779-b924-5b53d2e37b17"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 7,
                column: "PermissionControllerGuid",
                value: new Guid("65257817-5129-4136-afbc-0cfa181c13bb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 8,
                column: "PermissionControllerGuid",
                value: new Guid("06978b52-02c4-4cfa-bef2-5faa4c279217"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 9,
                column: "PermissionControllerGuid",
                value: new Guid("ef97b92d-6761-4b7f-b35a-ef6aca730de4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 10,
                column: "PermissionControllerGuid",
                value: new Guid("c7e459e5-1ae0-4728-b126-13f21c6705e3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 11,
                column: "PermissionControllerGuid",
                value: new Guid("689c0f2d-8e67-4369-808e-833c762ac731"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 12,
                column: "PermissionControllerGuid",
                value: new Guid("a42386a4-d1a8-4b71-b5f1-73891a4a41d0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 13,
                column: "PermissionControllerGuid",
                value: new Guid("6c3d408e-72ae-4342-a7df-b91651b8e0c3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 14,
                column: "PermissionControllerGuid",
                value: new Guid("a04cfcce-c0b6-4897-85eb-fc4d60ebf053"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 15,
                column: "PermissionControllerGuid",
                value: new Guid("9743b298-00a0-480a-be75-a141938e93da"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 16,
                column: "PermissionControllerGuid",
                value: new Guid("1ed5076a-d101-4b51-abad-eaf1bc93b63b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 17,
                column: "PermissionControllerGuid",
                value: new Guid("6a1b5b67-c4f2-4595-b6e4-a169c3c3912a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 18,
                column: "PermissionControllerGuid",
                value: new Guid("6ca7f2c2-56db-4499-a691-d21a4f48f53f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 19,
                column: "PermissionControllerGuid",
                value: new Guid("ea4acaf7-d37c-4a9f-80a1-888a16d32edc"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 20,
                column: "PermissionControllerGuid",
                value: new Guid("6b8d8af2-6638-4537-8836-e63dd5f0555f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 21,
                column: "PermissionControllerGuid",
                value: new Guid("3738b48c-cc76-4f35-ad98-1a62176c9848"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 22,
                column: "PermissionControllerGuid",
                value: new Guid("a267670a-ad29-4efe-a68c-0df9eaf357d3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 23,
                column: "PermissionControllerGuid",
                value: new Guid("b794e88b-1c08-4368-bcb6-bc078ab8579e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 24,
                column: "PermissionControllerGuid",
                value: new Guid("c7da6292-5d8f-4166-b3e6-064549a31a54"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 25,
                column: "PermissionControllerGuid",
                value: new Guid("e7681901-0f01-449d-ac42-597156358fce"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 26,
                column: "PermissionControllerGuid",
                value: new Guid("9a8b021b-97a1-470f-9e67-0a809b79d1f2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 27,
                column: "PermissionControllerGuid",
                value: new Guid("732d1d00-9d5f-4efd-a966-80ddf40045c6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 28,
                column: "PermissionControllerGuid",
                value: new Guid("711d5a8f-b36b-4fe9-abb6-4961ef611cee"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 29,
                column: "PermissionControllerGuid",
                value: new Guid("4db1b4a4-d86d-45aa-9723-2590c14d6457"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 30,
                column: "PermissionControllerGuid",
                value: new Guid("744a0f79-82e0-478f-8c51-ed6748bce7ce"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 31,
                column: "PermissionControllerGuid",
                value: new Guid("4a804492-04bc-404d-b367-c2ebd994a87a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 32,
                column: "PermissionControllerGuid",
                value: new Guid("265a3ba1-8e2d-4f05-a70a-ee78a518b005"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 33,
                column: "PermissionControllerGuid",
                value: new Guid("30f39da4-bf23-4343-98b8-23567f0a5ded"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 34,
                column: "PermissionControllerGuid",
                value: new Guid("0427db1b-0445-4138-a5f1-b5c6a27b78bb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 35,
                column: "PermissionControllerGuid",
                value: new Guid("54861e28-4154-4f02-b15d-c9f4f0f69f7d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 36,
                column: "PermissionControllerGuid",
                value: new Guid("69c5c11f-8878-47d4-b6b2-d5e733d68a05"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 37,
                column: "PermissionControllerGuid",
                value: new Guid("f0d62bab-b316-4d0d-bafe-e30868e9038e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 38,
                column: "PermissionControllerGuid",
                value: new Guid("35363553-446a-4f06-a999-6e59ee428fe1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 39,
                column: "PermissionControllerGuid",
                value: new Guid("9f058b8d-f6d9-4ba1-a38d-374c467e9ac1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 40,
                column: "PermissionControllerGuid",
                value: new Guid("23fb5373-8ac5-4cae-9fb1-3dbc74bee197"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 1,
                column: "PermissionControllerActionGuid",
                value: new Guid("9744b5ef-d8f7-4566-99d6-1b221d575fcc"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 2,
                column: "PermissionControllerActionGuid",
                value: new Guid("95d1e00f-ad1e-4387-a38e-721c78e88be9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 3,
                column: "PermissionControllerActionGuid",
                value: new Guid("6059549d-b556-4ef7-a579-2867906106e0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 4,
                column: "PermissionControllerActionGuid",
                value: new Guid("44291574-2939-49de-9570-f1cbfa0e17e8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 5,
                column: "PermissionControllerActionGuid",
                value: new Guid("366eca71-88c6-4c13-bfc5-2ec78899dd9b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 6,
                column: "PermissionControllerActionGuid",
                value: new Guid("b3db64b2-b77b-4dd6-b9b6-1a56f2e62133"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 7,
                column: "PermissionControllerActionGuid",
                value: new Guid("70dee548-47f0-4ef7-84e9-74241a3c0d51"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 8,
                column: "PermissionControllerActionGuid",
                value: new Guid("04204f8a-dbdd-423c-aa31-cc0f42ffe9b4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 9,
                column: "PermissionControllerActionGuid",
                value: new Guid("5ee7c507-9a53-41c2-808d-46862778bee3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 10,
                column: "PermissionControllerActionGuid",
                value: new Guid("1c8716b9-d8e3-499e-aa90-30bee89f9b38"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 11,
                column: "PermissionControllerActionGuid",
                value: new Guid("47e9fa3c-7c15-4e3b-b5dc-118c56ca0267"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 12,
                column: "PermissionControllerActionGuid",
                value: new Guid("711c39d7-b7e2-45b1-8841-d214eb0e0691"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 13,
                column: "PermissionControllerActionGuid",
                value: new Guid("627f7ea7-33d1-468c-86ca-65e4f175df49"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 14,
                column: "PermissionControllerActionGuid",
                value: new Guid("8c7ee486-2cc7-47b8-a12b-1e70906dc483"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 15,
                column: "PermissionControllerActionGuid",
                value: new Guid("5f71a3f7-67b3-4853-a558-5f7e1c5f7b5f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 16,
                column: "PermissionControllerActionGuid",
                value: new Guid("b70ef9ac-9311-4243-9575-dbb72ccdd374"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 17,
                column: "PermissionControllerActionGuid",
                value: new Guid("84173e0b-a51f-48c0-95c4-d3b8368fe6d8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 18,
                column: "PermissionControllerActionGuid",
                value: new Guid("427d0dd9-f7f8-40aa-ae42-514c09b1b6fe"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 19,
                column: "PermissionControllerActionGuid",
                value: new Guid("aaa1c64d-6ec3-473b-9772-5ee02549b86d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 20,
                column: "PermissionControllerActionGuid",
                value: new Guid("47633f6c-6703-4212-81d7-5281c1be0109"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 21,
                column: "PermissionControllerActionGuid",
                value: new Guid("cf0ce135-e31d-41ae-a902-55116b102260"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 22,
                column: "PermissionControllerActionGuid",
                value: new Guid("cffc3117-c2c6-45a2-a1e2-3664d6f647aa"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 23,
                column: "PermissionControllerActionGuid",
                value: new Guid("67f1b1a6-b890-4d50-813a-cc51de658903"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 24,
                column: "PermissionControllerActionGuid",
                value: new Guid("7fde06d5-7fab-4253-a046-f44d2e4aaa82"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 25,
                column: "PermissionControllerActionGuid",
                value: new Guid("ad1aedd7-f124-49dc-837f-64fb7e68a961"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 26,
                column: "PermissionControllerActionGuid",
                value: new Guid("57fb3bb7-5aac-4046-b2af-a033676089d3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 27,
                column: "PermissionControllerActionGuid",
                value: new Guid("47a3ee6d-7cdc-442d-bb63-dee1a0c55a09"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 28,
                column: "PermissionControllerActionGuid",
                value: new Guid("81d2e265-580c-4219-a823-3185a72e58ca"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 29,
                column: "PermissionControllerActionGuid",
                value: new Guid("8cb59519-d4e1-46a9-984c-64bb84b4967c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 30,
                column: "PermissionControllerActionGuid",
                value: new Guid("39ed1a96-f266-44c3-8a12-e1db1ff2c9e7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 31,
                column: "PermissionControllerActionGuid",
                value: new Guid("5fb67892-437f-473b-968f-8f9c427c1b31"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 32,
                column: "PermissionControllerActionGuid",
                value: new Guid("c60bff53-d532-40e9-b965-2d5a35f47d72"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 33,
                column: "PermissionControllerActionGuid",
                value: new Guid("d73a76e5-8fab-4336-a3dc-74e8a2044924"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 34,
                column: "PermissionControllerActionGuid",
                value: new Guid("2e13c525-3018-46a1-975e-e8c9244af40e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 35,
                column: "PermissionControllerActionGuid",
                value: new Guid("93a547d5-7bed-4521-a67d-802b83304048"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 36,
                column: "PermissionControllerActionGuid",
                value: new Guid("8662b679-c0c3-4af3-a5d6-82a0ef4518f9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 37,
                column: "PermissionControllerActionGuid",
                value: new Guid("358ec62b-e4ca-442f-bbaa-513ddfd5936a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 38,
                column: "PermissionControllerActionGuid",
                value: new Guid("47d19312-acbd-475c-a9d4-1bfb4a12dddc"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 39,
                column: "PermissionControllerActionGuid",
                value: new Guid("03b12c5f-b381-4117-bc2e-50d649ef54cb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 40,
                column: "PermissionControllerActionGuid",
                value: new Guid("32d11fd2-ca22-4aee-97d5-75c9dc901ee8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 41,
                column: "PermissionControllerActionGuid",
                value: new Guid("9743a57f-adef-41dc-81fd-2568ef8b5dbf"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 42,
                column: "PermissionControllerActionGuid",
                value: new Guid("d76a5d09-46b2-4953-8dcb-b07ba734d574"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 43,
                column: "PermissionControllerActionGuid",
                value: new Guid("87cd7ec7-1e82-4866-88af-9af9d2d29dc0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 44,
                column: "PermissionControllerActionGuid",
                value: new Guid("14018d38-0ca8-4657-b6ca-290868b8a085"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 45,
                column: "PermissionControllerActionGuid",
                value: new Guid("7f1dbdfb-81e3-4e13-9587-326cb9bc5b3a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 46,
                column: "PermissionControllerActionGuid",
                value: new Guid("31f857bc-a9a9-4f45-9285-3c534aa3dbed"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 47,
                column: "PermissionControllerActionGuid",
                value: new Guid("1188bf6d-e85c-4e9c-8bdd-7a833c3f5b12"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 48,
                column: "PermissionControllerActionGuid",
                value: new Guid("42955f78-2f92-45a5-bf2a-c168d201a664"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 49,
                column: "PermissionControllerActionGuid",
                value: new Guid("f5d6191e-6809-433e-9cfd-1df160d45880"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 50,
                column: "PermissionControllerActionGuid",
                value: new Guid("f3f2e890-b007-4545-a666-07ad4d7b3a25"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 51,
                column: "PermissionControllerActionGuid",
                value: new Guid("cacefb38-ba3d-4dbb-8be2-aea9e32a87f9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 52,
                column: "PermissionControllerActionGuid",
                value: new Guid("3e093809-7859-406a-8fd9-7bb0e0da0e63"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 53,
                column: "PermissionControllerActionGuid",
                value: new Guid("16fd90ab-9cab-46fe-88c9-f1536f834c27"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 54,
                column: "PermissionControllerActionGuid",
                value: new Guid("893cea66-58f6-4e83-a0f9-404af5da2dc4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 55,
                column: "PermissionControllerActionGuid",
                value: new Guid("ed3779f4-a445-4a18-bedb-94f10fb3f8cb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 56,
                column: "PermissionControllerActionGuid",
                value: new Guid("af1b5274-1a02-40e6-83b1-f9479436813a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 57,
                column: "PermissionControllerActionGuid",
                value: new Guid("5584abe1-a937-41dc-b71a-d903b0b15e24"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 58,
                column: "PermissionControllerActionGuid",
                value: new Guid("42bb219d-dba0-4865-8c36-a9eb3828c7e8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 59,
                column: "PermissionControllerActionGuid",
                value: new Guid("f6b306f8-9ab9-4603-9b94-97183f347e44"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 60,
                column: "PermissionControllerActionGuid",
                value: new Guid("672ad96d-9998-48f3-8fde-dd4e27978ce1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 61,
                column: "PermissionControllerActionGuid",
                value: new Guid("e0f80e36-b306-41a5-971e-c071cf56781a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 62,
                column: "PermissionControllerActionGuid",
                value: new Guid("ad25007d-2d18-478d-a180-95fdfea4f16a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 63,
                column: "PermissionControllerActionGuid",
                value: new Guid("871dca65-943c-4c8b-999c-4d097a1fb80d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 64,
                column: "PermissionControllerActionGuid",
                value: new Guid("ca171407-2725-483c-8559-cd50ddcacb45"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 65,
                column: "PermissionControllerActionGuid",
                value: new Guid("e92d7a17-e1a7-4e60-b789-563bd1bf9904"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 66,
                column: "PermissionControllerActionGuid",
                value: new Guid("d508eb0e-bf5b-418d-87e1-4da20e46d6b7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 67,
                column: "PermissionControllerActionGuid",
                value: new Guid("37ccd424-aed8-44b2-87a3-603ffab25c21"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 68,
                column: "PermissionControllerActionGuid",
                value: new Guid("65832e9e-8fb8-426b-a10a-2efa79544a94"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 69,
                column: "PermissionControllerActionGuid",
                value: new Guid("cb6e8a28-a001-49cf-b015-7ea36ea3bd01"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 70,
                column: "PermissionControllerActionGuid",
                value: new Guid("469262e2-1301-42e7-b50e-aa1818938bd8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 71,
                column: "PermissionControllerActionGuid",
                value: new Guid("95057b39-8b1c-429b-9c49-d909edb62678"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 72,
                column: "PermissionControllerActionGuid",
                value: new Guid("18f0c0b0-0a91-4bf1-9507-7692c8634a02"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 73,
                column: "PermissionControllerActionGuid",
                value: new Guid("e6c2d4a9-badc-42d0-91ec-edea4e727410"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 74,
                column: "PermissionControllerActionGuid",
                value: new Guid("0fb1f5bc-f77d-4117-8d6f-1b1a3b8cfc2c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 75,
                column: "PermissionControllerActionGuid",
                value: new Guid("4af933a9-f822-44e2-a169-66e28be3254d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 76,
                column: "PermissionControllerActionGuid",
                value: new Guid("e4cedc65-cb43-48b6-9730-ca80666b17f4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 77,
                column: "PermissionControllerActionGuid",
                value: new Guid("f9da27d8-6d06-46b4-83d2-a8a09d6ffd32"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 78,
                column: "PermissionControllerActionGuid",
                value: new Guid("7762f45e-98e2-4948-af2f-a19afce0f61d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 79,
                column: "PermissionControllerActionGuid",
                value: new Guid("df21ae56-fcab-470d-9934-b368cd7e020e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 80,
                column: "PermissionControllerActionGuid",
                value: new Guid("325012ff-394c-4aa5-958f-1f35a6d2c21a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 81,
                column: "PermissionControllerActionGuid",
                value: new Guid("06fe778d-2721-43b0-8563-404e4e2f8fb2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 82,
                column: "PermissionControllerActionGuid",
                value: new Guid("4ce2216e-40d2-41a5-8760-1903c338395c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 83,
                column: "PermissionControllerActionGuid",
                value: new Guid("c609fd52-2069-4a07-b74b-d7e22fdceb8e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 84,
                column: "PermissionControllerActionGuid",
                value: new Guid("7ba9c6f9-6825-42ee-a136-411b47e7c3d6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 85,
                column: "PermissionControllerActionGuid",
                value: new Guid("10b40e52-f7cb-493a-92a1-9ec12af1c95f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 86,
                column: "PermissionControllerActionGuid",
                value: new Guid("ac6398e5-78d8-4d56-8ddc-f67b0b78676e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 87,
                column: "PermissionControllerActionGuid",
                value: new Guid("43435996-a8c9-40d9-a61a-f672f98c535f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 88,
                column: "PermissionControllerActionGuid",
                value: new Guid("5332451d-cf9f-41f8-afea-f1b504fbd11f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 89,
                column: "PermissionControllerActionGuid",
                value: new Guid("ce411bc8-3253-43b1-b149-0387dccc8a71"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 90,
                column: "PermissionControllerActionGuid",
                value: new Guid("aa7357b0-da47-469a-a1fd-53d1e80a7878"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 91,
                column: "PermissionControllerActionGuid",
                value: new Guid("079501e3-6ccd-46d3-9221-56ae4fae6ed1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 92,
                column: "PermissionControllerActionGuid",
                value: new Guid("ec659220-d8f9-49dc-9d83-dbaa8ae7afd1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 93,
                column: "PermissionControllerActionGuid",
                value: new Guid("4a407068-8a44-4f38-a4e1-fca81f354f32"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 94,
                column: "PermissionControllerActionGuid",
                value: new Guid("25099f7c-109f-49d8-adbe-6b94411f1bdb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 95,
                column: "PermissionControllerActionGuid",
                value: new Guid("2b565c5c-1e45-4f9f-b580-d1f13f14228d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 96,
                column: "PermissionControllerActionGuid",
                value: new Guid("b36da7fa-335b-4675-8039-dd1f57ec06a2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 97,
                column: "PermissionControllerActionGuid",
                value: new Guid("0f132de1-68cc-4532-834e-f1a2e1ea4814"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 98,
                column: "PermissionControllerActionGuid",
                value: new Guid("9234de32-782b-4124-967a-717c59581579"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 99,
                column: "PermissionControllerActionGuid",
                value: new Guid("aebc5704-24cd-4f55-8f5b-a329ae036a57"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 100,
                column: "PermissionControllerActionGuid",
                value: new Guid("0651d4c2-3d65-41db-9d51-a21cb88440db"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 101,
                column: "PermissionControllerActionGuid",
                value: new Guid("bd3996f8-ab8b-4e6d-9478-409251279da2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 102,
                column: "PermissionControllerActionGuid",
                value: new Guid("11a036a1-3aa1-4041-9f24-757d77946997"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 103,
                column: "PermissionControllerActionGuid",
                value: new Guid("661280b9-5375-492e-a982-3dc1f13cecc8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 104,
                column: "PermissionControllerActionGuid",
                value: new Guid("aab963fe-3769-4856-bba9-ec72b2ac976b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 105,
                column: "PermissionControllerActionGuid",
                value: new Guid("0a983908-5a9f-4f3c-9aa9-185d21bc9879"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 106,
                column: "PermissionControllerActionGuid",
                value: new Guid("9414466d-7572-43a1-b1f0-c4e7cb2489e0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 107,
                column: "PermissionControllerActionGuid",
                value: new Guid("d70441ce-efc7-4337-8a49-b29f9e6c06bc"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 108,
                column: "PermissionControllerActionGuid",
                value: new Guid("50c51b6d-8dff-4c28-ad8d-b7065f1d5641"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 109,
                column: "PermissionControllerActionGuid",
                value: new Guid("c61e47b0-45b1-4154-87e0-44937364ddcc"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 110,
                column: "PermissionControllerActionGuid",
                value: new Guid("5f151a56-2e65-4f71-a564-3fad6ffbfcea"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 111,
                column: "PermissionControllerActionGuid",
                value: new Guid("c3203da8-4ad5-433c-98f1-b1a8632a2f4d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 112,
                column: "PermissionControllerActionGuid",
                value: new Guid("762bf210-60ff-4470-8bbd-24db71e1cb51"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 113,
                column: "PermissionControllerActionGuid",
                value: new Guid("abac68ca-7629-459d-9339-af4edb039bc4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 114,
                column: "PermissionControllerActionGuid",
                value: new Guid("6082a665-f255-4e64-81b6-9405c32e506d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 115,
                column: "PermissionControllerActionGuid",
                value: new Guid("8ed28036-030a-4a18-ae4f-004c76dca1bf"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 116,
                column: "PermissionControllerActionGuid",
                value: new Guid("144e81dc-09fe-418c-a1ed-cdf835c46667"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 117,
                column: "PermissionControllerActionGuid",
                value: new Guid("ebc71e7d-1bad-48f5-9a62-eb5c9216c3bc"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 118,
                column: "PermissionControllerActionGuid",
                value: new Guid("af506a8c-f980-44aa-b157-046a498487c4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 119,
                column: "PermissionControllerActionGuid",
                value: new Guid("e9c24685-4475-4e7b-b906-8278248f1506"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 120,
                column: "PermissionControllerActionGuid",
                value: new Guid("32545c71-54be-47e6-94c7-7b2fd5c988bc"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 121,
                column: "PermissionControllerActionGuid",
                value: new Guid("09b3c441-1c46-45b8-920a-352178fe0428"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 122,
                column: "PermissionControllerActionGuid",
                value: new Guid("ea6cc4a8-2c71-4e4d-b10f-087f608d1953"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 123,
                column: "PermissionControllerActionGuid",
                value: new Guid("6a003477-f7b5-400d-b580-96ae23796fc2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 124,
                column: "PermissionControllerActionGuid",
                value: new Guid("68ff29a3-826e-4a25-ace0-b7b8e7b66a5f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 125,
                column: "PermissionControllerActionGuid",
                value: new Guid("0a8228d1-7aea-4812-a645-ae9cc62cc234"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 126,
                column: "PermissionControllerActionGuid",
                value: new Guid("2406be8c-17cb-47e7-ac47-2e3f6928c785"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 127,
                column: "PermissionControllerActionGuid",
                value: new Guid("c4a9b8c1-10f3-4426-838e-ce8f78dab0ec"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 128,
                column: "PermissionControllerActionGuid",
                value: new Guid("93d99cdb-f892-4eb8-bb55-998a68043657"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 129,
                column: "PermissionControllerActionGuid",
                value: new Guid("6dde4c92-d880-429c-987a-17ba5b6fc908"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 130,
                column: "PermissionControllerActionGuid",
                value: new Guid("2bfd42b8-1afe-4480-805c-04a67182b5fd"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 131,
                column: "PermissionControllerActionGuid",
                value: new Guid("b548ca91-76cb-40a2-b2ed-691e7509f440"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 132,
                column: "PermissionControllerActionGuid",
                value: new Guid("704441ca-1892-44b0-9cbe-b01465338347"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 133,
                column: "PermissionControllerActionGuid",
                value: new Guid("e8eea5ea-99c8-42b0-bb2d-0f9aae45f4f1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 134,
                column: "PermissionControllerActionGuid",
                value: new Guid("ef35fecf-84c2-423f-ba7a-276200f5ddb2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 135,
                column: "PermissionControllerActionGuid",
                value: new Guid("c8c70198-2a29-4b45-8ee4-2d66ff9afe53"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 136,
                column: "PermissionControllerActionGuid",
                value: new Guid("fff7ec7e-5582-4376-a463-1648c525b379"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 137,
                column: "PermissionControllerActionGuid",
                value: new Guid("bfa12bd2-6935-4eb7-85be-e43b2193677f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 138,
                column: "PermissionControllerActionGuid",
                value: new Guid("f8ed12e4-52da-4534-8b46-5fb9aa6a1c34"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 139,
                column: "PermissionControllerActionGuid",
                value: new Guid("ac07dfd5-a811-4fbc-9d12-2af37a96be4d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 140,
                column: "PermissionControllerActionGuid",
                value: new Guid("53394235-ad93-47e5-8e8a-a1f3abd9811e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 141,
                column: "PermissionControllerActionGuid",
                value: new Guid("1679232a-01c1-49c4-8bf5-758dc5145c70"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 142,
                column: "PermissionControllerActionGuid",
                value: new Guid("e5bbf9f2-e638-41ac-bf1a-901de466bdaa"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 143,
                column: "PermissionControllerActionGuid",
                value: new Guid("516e38b4-0854-416b-95e8-674d11b7ebac"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 144,
                column: "PermissionControllerActionGuid",
                value: new Guid("8607d24f-bdb8-4c60-8317-79125a047cb7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 145,
                column: "PermissionControllerActionGuid",
                value: new Guid("673ef5cc-4d5c-4b24-8481-6d8e22ca241e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 146,
                column: "PermissionControllerActionGuid",
                value: new Guid("7d7d4a04-07a0-4ca9-a036-e673df776407"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 147,
                column: "PermissionControllerActionGuid",
                value: new Guid("2dc75024-b30d-4857-b0fb-efbfe139468d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 148,
                column: "PermissionControllerActionGuid",
                value: new Guid("c4756b98-96b1-424b-9491-cfea9175b590"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 149,
                column: "PermissionControllerActionGuid",
                value: new Guid("2c18d5ad-06e7-4458-81dc-d6264e2f2a90"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 150,
                column: "PermissionControllerActionGuid",
                value: new Guid("1482f9c0-a4e8-4e99-8a27-849db6bcb2ee"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 151,
                column: "PermissionControllerActionGuid",
                value: new Guid("e4a8a5de-a01b-4868-b074-157415c57d20"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 152,
                column: "PermissionControllerActionGuid",
                value: new Guid("9a89c948-91c9-4bd3-9781-6cc51cd1365d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 153,
                column: "PermissionControllerActionGuid",
                value: new Guid("9a874405-463c-455a-ba4a-a938ea640c12"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 154,
                column: "PermissionControllerActionGuid",
                value: new Guid("99231f7c-aefe-49a5-8913-2349b622c57a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 155,
                column: "PermissionControllerActionGuid",
                value: new Guid("de6d98d3-34e7-4946-880f-e125d6252b48"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 156,
                column: "PermissionControllerActionGuid",
                value: new Guid("f6768c18-9f7d-4e7e-a56a-973b1e362a9a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 157,
                column: "PermissionControllerActionGuid",
                value: new Guid("71ec210c-d649-4d71-81c0-848a2a8e0fd4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 158,
                column: "PermissionControllerActionGuid",
                value: new Guid("a7ca5d6b-ef7c-4da0-a6d7-6ef409b79f90"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 159,
                column: "PermissionControllerActionGuid",
                value: new Guid("5ee17a3f-78df-4af9-8374-2783b7bc0e51"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 160,
                column: "PermissionControllerActionGuid",
                value: new Guid("cee42646-3ada-4e3a-bfe3-ddf37777bb8d"));

            migrationBuilder.InsertData(
                schema: "Driver",
                table: "DeliverySetting",
                columns: new[] { "DeliverySettingID", "BaseFare", "CreateDate", "DeleteDate", "DeliverySettingGuid", "DriverCommision", "EnableDate", "IsDeleted", "IsEnable", "MinKM", "OverKmFare", "Tax", "UpdateDate", "UserDelete", "UserEnable", "UserId", "UserUpdate" },
                values: new object[] { 1, 20m, new DateTime(2020, 9, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("f721d906-dccd-4afd-80c3-7ef9be988587"), 80m, null, false, true, 3m, 5m, 0m, null, null, null, 1, null });

            migrationBuilder.InsertData(
                schema: "Driver",
                table: "TransactionType",
                columns: new[] { "TransactionTypeID", "CreateDate", "DeleteDate", "EnableDate", "IsDeleted", "IsEnable", "NameAR", "NameEN", "TransactionTypeGuid", "UpdateDate", "UserDelete", "UserEnable", "UserId", "UserUpdate" },
                values: new object[,]
                {
                    { 6, new DateTime(2020, 9, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, false, "توصيل طلب", "Delivery Order", new Guid("d0d3824b-059e-483b-98a1-4b07b4c3e148"), null, null, null, 1, null }
                //    { 5, new DateTime(2020, 9, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, false, "دفع للكابتن STC", "Pay to Captain STC", new Guid("e2db1f1f-0bcd-495e-99f8-64860af3229c"), null, null, null, 1, null },
                //    { 4, new DateTime(2020, 9, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, false, "عقوبة", "Punishment", new Guid("3890b629-98cb-4399-ad07-179336a72da4"), null, null, null, 1, null },
                //    { 3, new DateTime(2020, 9, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, false, "علاوة", "Bouns", new Guid("f92f3bee-01e9-4b1d-9afb-45b10130506b"), null, null, null, 1, null },
                //    { 2, new DateTime(2020, 9, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, false, "سحب - جزاء", "Withdrawal", new Guid("ba755521-b78c-4c0b-a1f9-d288bdbdafbd"), null, null, null, 1, null },
                //    { 1, new DateTime(2020, 9, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), null, null, false, false, "شحن رصيد بواسطة الإدارة", "Deposit by Operation", new Guid("eb135f8a-870c-4d00-8983-9f3b6b2f82b5"), null, null, null, 1, null }
                });

            //migrationBuilder.InsertData(
            //    schema: "Permission",
            //    table: "RoleConfig",
            //    columns: new[] { "RoleConfigID", "PermissionControllerActionID", "RoleId" },
            //    values: new object[,]
            //    {
            //        { 1, 1, 1 },
            //        { 90, 90, 1 },
            //        { 91, 91, 1 },
            //        { 92, 92, 1 },
            //        { 93, 93, 1 },
            //        { 94, 94, 1 },
            //        { 98, 98, 1 },
            //        { 96, 96, 1 },
            //        { 97, 97, 1 },
            //        { 89, 89, 1 },
            //        { 99, 99, 1 },
            //        { 100, 100, 1 },
            //        { 95, 95, 1 },
            //        { 88, 88, 1 },
            //        { 84, 84, 1 },
            //        { 86, 86, 1 },
            //        { 85, 85, 1 },
            //        { 101, 101, 1 },
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
            //        { 87, 87, 1 },
            //        { 102, 102, 1 },
            //        { 110, 110, 1 },
            //        { 104, 104, 1 },
            //        { 128, 128, 2 },
            //        { 127, 127, 2 },
            //        { 126, 126, 2 }
            //    });

            //migrationBuilder.InsertData(
            //    schema: "Permission",
            //    table: "RoleConfig",
            //    columns: new[] { "RoleConfigID", "PermissionControllerActionID", "RoleId" },
            //    values: new object[,]
            //    {
            //        { 125, 125, 2 },
            //        { 108, 108, 2 },
            //        { 107, 107, 2 },
            //        { 106, 106, 2 },
            //        { 105, 105, 2 },
            //        { 132, 132, 1 },
            //        { 131, 131, 1 },
            //        { 130, 130, 1 },
            //        { 129, 129, 1 },
            //        { 124, 124, 1 },
            //        { 103, 103, 1 },
            //        { 123, 123, 1 },
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
            //        { 73, 73, 1 },
            //        { 109, 109, 1 },
            //        { 122, 122, 1 },
            //        { 71, 71, 1 },
            //        { 72, 72, 1 },
            //        { 69, 69, 1 },
            //        { 20, 20, 1 },
            //        { 21, 21, 1 },
            //        { 22, 22, 1 },
            //        { 23, 23, 1 },
            //        { 24, 24, 1 },
            //        { 25, 25, 1 },
            //        { 19, 19, 1 },
            //        { 26, 26, 1 },
            //        { 28, 28, 1 },
            //        { 29, 29, 1 },
            //        { 30, 30, 1 },
            //        { 31, 31, 1 },
            //        { 32, 32, 1 }
            //    });

            //migrationBuilder.InsertData(
            //    schema: "Permission",
            //    table: "RoleConfig",
            //    columns: new[] { "RoleConfigID", "PermissionControllerActionID", "RoleId" },
            //    values: new object[,]
            //    {
            //        { 33, 33, 1 },
            //        { 27, 27, 1 },
            //        { 70, 70, 1 },
            //        { 18, 18, 1 },
            //        { 16, 16, 1 },
            //        { 2, 2, 1 },
            //        { 3, 3, 1 },
            //        { 4, 4, 1 },
            //        { 5, 5, 1 },
            //        { 6, 6, 1 },
            //        { 7, 7, 1 },
            //        { 17, 17, 1 },
            //        { 8, 8, 1 },
            //        { 10, 10, 1 },
            //        { 11, 11, 1 },
            //        { 12, 12, 1 },
            //        { 13, 13, 1 },
            //        { 14, 14, 1 },
            //        { 15, 15, 1 },
            //        { 9, 9, 1 },
            //        { 35, 35, 1 },
            //        { 34, 34, 1 },
            //        { 37, 37, 1 },
            //        { 68, 68, 1 },
            //        { 67, 67, 1 },
            //        { 66, 66, 1 },
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
            //        { 36, 36, 1 },
            //        { 54, 54, 1 },
            //        { 55, 55, 1 },
            //        { 52, 52, 1 },
            //        { 38, 38, 1 },
            //        { 39, 39, 1 }
            //    });

            //migrationBuilder.InsertData(
            //    schema: "Permission",
            //    table: "RoleConfig",
            //    columns: new[] { "RoleConfigID", "PermissionControllerActionID", "RoleId" },
            //    values: new object[,]
            //    {
            //        { 40, 40, 1 },
            //        { 41, 41, 1 },
            //        { 42, 42, 1 },
            //        { 53, 53, 1 },
            //        { 44, 44, 1 },
            //        { 43, 43, 1 },
            //        { 46, 46, 1 },
            //        { 47, 47, 1 },
            //        { 48, 48, 1 },
            //        { 49, 49, 1 },
            //        { 50, 50, 1 },
            //        { 51, 51, 1 },
            //        { 45, 45, 1 }
            //    });

            //migrationBuilder.InsertData(
            //    schema: "Setting",
            //    table: "Country",
            //    columns: new[] { "CountryID", "CountryGuid", "CreateDate", "DeleteDate", "EnableDate", "Extension", "Flag", "IsDeleted", "IsEnable", "Lat", "Long", "NameAR", "NameEN", "Place", "UpdateDate", "UserDelete", "UserEnable", "UserId", "UserUpdate", "Zoom" },
            //    values: new object[] { 1, new Guid("fb3dc9e3-8844-4c92-ac8e-5587f64cdef3"), new DateTime(2021, 10, 4, 13, 58, 35, 639, DateTimeKind.Local).AddTicks(4612), null, new DateTime(2021, 10, 4, 13, 58, 35, 640, DateTimeKind.Local).AddTicks(7569), "00966", null, false, true, "", "", "السعودية", "SA", "", null, null, null, 1, null, null });

            //migrationBuilder.InsertData(
            //    schema: "Setting",
            //    table: "Jobs",
            //    columns: new[] { "JobsID", "CreateDate", "DeleteDate", "DescriptionAr", "DescriptionEn", "EnableDate", "Image", "IsDeleted", "IsEnable", "JobTypeId", "JobsGuid", "NameAR", "NameEN", "UpdateDate", "UserDelete", "UserEnable", "UserId", "UserUpdate" },
            //    values: new object[] { 1, new DateTime(2021, 10, 4, 13, 58, 35, 645, DateTimeKind.Local).AddTicks(7290), null, null, null, new DateTime(2021, 10, 4, 13, 58, 35, 645, DateTimeKind.Local).AddTicks(8069), null, false, true, 2, new Guid("82bc34f9-41ca-47bd-b763-4665fdebdb5f"), "الدمام", "DMM", null, null, null, 1, null });

            //migrationBuilder.InsertData(
            //    schema: "Setting",
            //    table: "MainCategory",
            //    columns: new[] { "MainCategoryID", "CreateDate", "DeleteDate", "EnableDate", "IsDeleted", "IsEnable", "MainCategoryGuid", "NameAR", "NameEN", "UpdateDate", "UserDelete", "UserEnable", "UserId", "UserUpdate" },
            //    values: new object[] { 1, new DateTime(2021, 10, 4, 13, 58, 35, 646, DateTimeKind.Local).AddTicks(3106), null, new DateTime(2021, 10, 4, 13, 58, 35, 646, DateTimeKind.Local).AddTicks(3890), false, true, new Guid("0ea2d293-7480-4149-b271-62086c382d0d"), "الدمام", "DMM", null, null, null, 1, null });

            //migrationBuilder.InsertData(
            //    schema: "Setting",
            //    table: "Nationality",
            //    columns: new[] { "NationalityID", "CreateDate", "DeleteDate", "DescriptionAr", "DescriptionEn", "EnableDate", "IsDeleted", "IsEnable", "NameAR", "NameEN", "NationalityGuid", "UpdateDate", "UserDelete", "UserEnable", "UserId", "UserUpdate" },
            //    values: new object[] { 1, new DateTime(2021, 10, 4, 13, 58, 35, 643, DateTimeKind.Local).AddTicks(6138), null, null, null, new DateTime(2021, 10, 4, 13, 58, 35, 643, DateTimeKind.Local).AddTicks(6884), false, true, "الدمام", "DMM", new Guid("ea31ffeb-ccc8-46d9-be31-a9fd8ba00a72"), null, null, null, 1, null });

            //migrationBuilder.InsertData(
            //    schema: "Setting",
            //    table: "Departments",
            //    columns: new[] { "DepartmentsID", "Arrange", "CreateDate", "DeleteDate", "DepartmentsGuid", "DescriptionAr", "DescriptionEn", "EnableDate", "Image", "IsDeleted", "IsEnable", "Isunique", "MainCategoryID", "NameAR", "NameEN", "SiteImage", "UpdateDate", "UserDelete", "UserEnable", "UserId", "UserUpdate" },
            //    values: new object[] { 1, null, new DateTime(2021, 10, 4, 13, 58, 35, 646, DateTimeKind.Local).AddTicks(9375), null, new Guid("22f736d1-9054-41bc-aaf5-c727a9f6ef0a"), null, null, new DateTime(2021, 10, 4, 13, 58, 35, 647, DateTimeKind.Local).AddTicks(141), null, false, true, false, 1, "الدمام", "DMM", null, null, null, null, 1, null });

            //migrationBuilder.InsertData(
            //    schema: "Setting",
            //    table: "Region",
            //    columns: new[] { "RegionID", "CountryID", "CreateDate", "DeleteDate", "EnableDate", "IsDeleted", "IsEnable", "Lat", "Long", "NameAR", "NameEN", "Place", "RegionGuid", "UpdateDate", "UserDelete", "UserEnable", "UserId", "UserUpdate", "Zoom" },
            //    values: new object[] { 1, 1, new DateTime(2021, 10, 4, 13, 58, 35, 641, DateTimeKind.Local).AddTicks(6295), null, new DateTime(2021, 10, 4, 13, 58, 35, 641, DateTimeKind.Local).AddTicks(7086), false, true, "", "", "الدمام", "DMM", "", new Guid("b5dc564a-5590-4f64-824e-75f27be55b86"), null, null, null, 1, null, null });

            //migrationBuilder.InsertData(
            //    schema: "Setting",
            //    table: "City",
            //    columns: new[] { "CityID", "CityGuid", "Code", "CreateDate", "DeleteDate", "EnableDate", "IsDeleted", "IsEnable", "Lat", "Long", "NameAR", "NameEN", "Place", "RegionID", "UpdateDate", "UserDelete", "UserEnable", "UserId", "UserUpdate", "Zoom" },
            //    values: new object[] { 1, new Guid("3e221e9f-8b68-4390-80ec-7849e46f2650"), null, new DateTime(2021, 10, 4, 13, 58, 35, 642, DateTimeKind.Local).AddTicks(7619), null, new DateTime(2021, 10, 4, 13, 58, 35, 642, DateTimeKind.Local).AddTicks(8417), false, true, "", "", "الدمام", "DMM", "", 1, null, null, null, 1, null, null });

            //migrationBuilder.InsertData(
            //    schema: "Emp",
            //    table: "Employees",
            //    columns: new[] { "EntityEmpID", "AddressAR", "AddressEN", "BirthDateHijri", "BirthDateMilady", "BloodTypeId", "CityID", "CreateDate", "DeleteDate", "Email", "EnableDate", "EntityEmpGuid", "FileNo", "FirstNameAR", "FirstNameEN", "Gender", "IDNo", "IsDeleted", "IsEnable", "JobsID", "LastNameAR", "LastNameEN", "Lat", "Lng", "MidNameAR", "MidNameEN", "MobileNo", "NationalityID", "Notes", "PhoneNumber", "Photo", "UpdateDate", "UserDelete", "UserEnable", "UserId", "UserUpdate", "Zoom" },
            //    values: new object[] { 1, "الاسماعيليه", "ismailia", "", null, 1, 1, new DateTime(2021, 10, 4, 13, 58, 35, 648, DateTimeKind.Local).AddTicks(900), null, "SystemUser@Admin.com", new DateTime(2021, 10, 4, 13, 58, 35, 648, DateTimeKind.Local).AddTicks(1353), new Guid("2299447c-fc61-4aa4-ba03-8c91e4f4b2d5"), "123321", "احمد", "Ahmed", 1, "", false, true, 1, "حسين", "Hussien", "", "", "سيد", "Sayed", "0595489633", 1, "", "", "", null, null, 1, 1, null, "" });

            migrationBuilder.CreateIndex(
                name: "IX_DeliverySetting_UserId",
                schema: "Driver",
                table: "DeliverySetting",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeliverySetting",
                schema: "Driver");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "Emp",
                table: "Employees",
                keyColumn: "EntityEmpID",
                keyValue: 1);

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
                name: "DriverCharge",
                schema: "Order",
                table: "OrderVendor");

            migrationBuilder.DropColumn(
                name: "IsPaidDriver",
                schema: "Order",
                table: "OrderVendor");

            migrationBuilder.AlterColumn<double>(
                name: "Before",
                schema: "Driver",
                table: "DriverBlance",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<double>(
                name: "Amount",
                schema: "Driver",
                table: "DriverBlance",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<double>(
                name: "After",
                schema: "Driver",
                table: "DriverBlance",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 1,
                column: "PermissionControllerGuid",
                value: new Guid("c08a8982-862c-48db-81f1-410f8d51173f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 2,
                column: "PermissionControllerGuid",
                value: new Guid("fa59e28e-616b-468b-8265-986b1fbb7152"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 3,
                column: "PermissionControllerGuid",
                value: new Guid("d8291b2b-b422-4004-aea5-671e123c5a96"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 4,
                column: "PermissionControllerGuid",
                value: new Guid("e28ca851-9cdf-4c23-8fea-e4930e05330a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 5,
                column: "PermissionControllerGuid",
                value: new Guid("38de289a-7953-498f-b417-121250b6949a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 6,
                column: "PermissionControllerGuid",
                value: new Guid("e6ded42a-0e40-4b9e-bd39-8b0d5fca3086"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 7,
                column: "PermissionControllerGuid",
                value: new Guid("c47d169d-94fd-4a82-b139-40da93eca9cd"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 8,
                column: "PermissionControllerGuid",
                value: new Guid("48a9a60d-ae8f-473c-a4ce-a6ebeb7800e4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 9,
                column: "PermissionControllerGuid",
                value: new Guid("f6d81eef-371c-446a-be40-15b6ff253b55"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 10,
                column: "PermissionControllerGuid",
                value: new Guid("9812fb5d-f9a2-43c8-959d-6d4fe5b2e765"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 11,
                column: "PermissionControllerGuid",
                value: new Guid("867a6242-45b2-4585-b6dd-098ca2e92f7a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 12,
                column: "PermissionControllerGuid",
                value: new Guid("75ad1170-2d7f-463f-b5d7-ae26b630e8df"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 13,
                column: "PermissionControllerGuid",
                value: new Guid("8efc9439-49d5-4878-b1a6-c75358eca180"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 14,
                column: "PermissionControllerGuid",
                value: new Guid("07a80ab9-7a72-4a25-8ddc-2f9ba9055bf7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 15,
                column: "PermissionControllerGuid",
                value: new Guid("f48bc3b5-076e-4cf7-ab5f-dcd1d984278d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 16,
                column: "PermissionControllerGuid",
                value: new Guid("bd887ce1-7c23-4c74-b652-3b95fc73feeb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 17,
                column: "PermissionControllerGuid",
                value: new Guid("8c99af5d-3a48-4bf8-be35-55a1cfd81e3f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 18,
                column: "PermissionControllerGuid",
                value: new Guid("8d9922ba-9232-46e6-87a7-7a84d627ac1d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 19,
                column: "PermissionControllerGuid",
                value: new Guid("ee198760-0eaa-496d-83ed-268661b93bfd"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 20,
                column: "PermissionControllerGuid",
                value: new Guid("cbf35f18-9f34-4a05-92fb-6fb046192d85"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 21,
                column: "PermissionControllerGuid",
                value: new Guid("5175eb7c-e8f0-48a0-8394-55d03d030f5c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 22,
                column: "PermissionControllerGuid",
                value: new Guid("e6be22f0-f37d-43bc-b66c-653e24b187db"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 23,
                column: "PermissionControllerGuid",
                value: new Guid("a2531e0f-59a5-481d-822c-bdc722de3df1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 24,
                column: "PermissionControllerGuid",
                value: new Guid("033cf203-0fc7-405a-b6be-2623cc5c1251"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 25,
                column: "PermissionControllerGuid",
                value: new Guid("f301827a-7284-47e7-bf86-9a87b056bfa0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 26,
                column: "PermissionControllerGuid",
                value: new Guid("d6776395-6e7e-4d67-8ecc-c443a31eb01c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 27,
                column: "PermissionControllerGuid",
                value: new Guid("2edfb667-f56c-4c51-a989-71ac70a6cf7e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 28,
                column: "PermissionControllerGuid",
                value: new Guid("0ee2ca40-4a7b-443f-823c-da8bb43287e9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 29,
                column: "PermissionControllerGuid",
                value: new Guid("1e0a7e63-e500-487a-996a-6424d6f4dbef"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 30,
                column: "PermissionControllerGuid",
                value: new Guid("53db8640-b28d-4fe9-a8a2-147adc1d5b79"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 31,
                column: "PermissionControllerGuid",
                value: new Guid("4f49e6bd-f7bd-4807-b3bc-53cb87e79769"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 32,
                column: "PermissionControllerGuid",
                value: new Guid("fdc2ee82-21e1-4968-8d9e-6493db701c44"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 33,
                column: "PermissionControllerGuid",
                value: new Guid("f4ef5779-78ed-4ed6-aecc-21a8315306a3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 34,
                column: "PermissionControllerGuid",
                value: new Guid("c0604c78-b1e2-4c61-8a23-8d6652c09249"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 35,
                column: "PermissionControllerGuid",
                value: new Guid("8501b483-8b9a-4ef4-8410-c487db345349"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 36,
                column: "PermissionControllerGuid",
                value: new Guid("da9009de-95bf-418f-8196-ec4feeb65953"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 37,
                column: "PermissionControllerGuid",
                value: new Guid("fdf89b5c-f2ec-4c30-9c03-39a290394330"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 38,
                column: "PermissionControllerGuid",
                value: new Guid("2ebc44ff-6b56-46f7-8a71-0069d2b6f58d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 39,
                column: "PermissionControllerGuid",
                value: new Guid("00c367d1-13a8-4873-b8a0-dadbd49626da"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 40,
                column: "PermissionControllerGuid",
                value: new Guid("6a632372-e6dc-42d4-9536-0c8c7b91160d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 1,
                column: "PermissionControllerActionGuid",
                value: new Guid("ddf73e8d-7e91-4d50-91ab-e848dfa6eadb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 2,
                column: "PermissionControllerActionGuid",
                value: new Guid("c53d406c-33a5-4a2e-b7e9-12e753906281"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 3,
                column: "PermissionControllerActionGuid",
                value: new Guid("2cdec330-fa72-40ef-a210-8cacc92f7911"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 4,
                column: "PermissionControllerActionGuid",
                value: new Guid("cc6ed035-f6b0-4437-8158-bcd6cbd3f8da"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 5,
                column: "PermissionControllerActionGuid",
                value: new Guid("ac9fc880-e8e8-43c8-a422-e3757f33b7e8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 6,
                column: "PermissionControllerActionGuid",
                value: new Guid("d86fd95d-16eb-4d9e-b234-9059227d8d07"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 7,
                column: "PermissionControllerActionGuid",
                value: new Guid("617339d3-4ad2-42fa-8a1e-e420053e2ab6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 8,
                column: "PermissionControllerActionGuid",
                value: new Guid("07f6eee5-a518-4f38-92fc-c7c12fe4b3da"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 9,
                column: "PermissionControllerActionGuid",
                value: new Guid("d62c0918-7c03-4f70-8267-3413cdbf2976"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 10,
                column: "PermissionControllerActionGuid",
                value: new Guid("15d4c15c-8cb0-405d-9537-d2066acd1422"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 11,
                column: "PermissionControllerActionGuid",
                value: new Guid("cdeb8c5f-8e15-408b-b9c6-61db71639714"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 12,
                column: "PermissionControllerActionGuid",
                value: new Guid("fd107f83-81c3-49f6-bb5d-f1401bdd0aee"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 13,
                column: "PermissionControllerActionGuid",
                value: new Guid("a42a2592-f69f-4692-aa61-5d0387a2783e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 14,
                column: "PermissionControllerActionGuid",
                value: new Guid("cf75a4cd-d9ea-42df-8669-1c2d07034a21"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 15,
                column: "PermissionControllerActionGuid",
                value: new Guid("fb05b112-7151-4610-a15e-62e82e2646df"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 16,
                column: "PermissionControllerActionGuid",
                value: new Guid("9e0af867-bc8f-447e-bc9c-0c6ca3120ecf"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 17,
                column: "PermissionControllerActionGuid",
                value: new Guid("e59448a1-0436-4481-816b-964101734d9c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 18,
                column: "PermissionControllerActionGuid",
                value: new Guid("373a10d3-0461-4b8a-a642-a0ab7a59d529"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 19,
                column: "PermissionControllerActionGuid",
                value: new Guid("684b8e9f-fd0b-478b-937e-a8ae4f971448"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 20,
                column: "PermissionControllerActionGuid",
                value: new Guid("8dfa3b4e-bb32-497f-94c5-e23543d71388"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 21,
                column: "PermissionControllerActionGuid",
                value: new Guid("d796c31e-7f21-4426-8166-48debb9eadc7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 22,
                column: "PermissionControllerActionGuid",
                value: new Guid("d83f5364-6fbe-418a-ab47-c8007662418a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 23,
                column: "PermissionControllerActionGuid",
                value: new Guid("a9370069-099b-40b3-a68e-44648e459982"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 24,
                column: "PermissionControllerActionGuid",
                value: new Guid("c5808a97-4966-4018-848d-42a8e56954d2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 25,
                column: "PermissionControllerActionGuid",
                value: new Guid("25ad3980-1994-4c29-81e5-7cb48056b9ae"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 26,
                column: "PermissionControllerActionGuid",
                value: new Guid("c5bdf02f-551d-4211-9e14-c7811d11a0a6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 27,
                column: "PermissionControllerActionGuid",
                value: new Guid("9c74235f-086a-4c02-aaaf-b18440b6295e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 28,
                column: "PermissionControllerActionGuid",
                value: new Guid("f15eeac3-59f6-412f-a574-782a5d8a52ac"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 29,
                column: "PermissionControllerActionGuid",
                value: new Guid("2ac1ace1-2bcf-4201-8b2d-1d1137e69eac"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 30,
                column: "PermissionControllerActionGuid",
                value: new Guid("a5825ce3-82da-43e9-a604-38c8650d2973"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 31,
                column: "PermissionControllerActionGuid",
                value: new Guid("a235aae7-6394-4a2d-95ef-d19ac6da0e97"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 32,
                column: "PermissionControllerActionGuid",
                value: new Guid("e05b3d1d-2606-411c-b802-03ed7d54fc7c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 33,
                column: "PermissionControllerActionGuid",
                value: new Guid("947ba72f-2bba-4ef3-a5ca-4377e704f681"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 34,
                column: "PermissionControllerActionGuid",
                value: new Guid("20d59bdb-7d61-4104-904a-051bd934c606"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 35,
                column: "PermissionControllerActionGuid",
                value: new Guid("63a82262-f081-496a-bc4d-c9156a80c865"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 36,
                column: "PermissionControllerActionGuid",
                value: new Guid("d25928fb-a092-4759-9958-a962e8c0bd3f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 37,
                column: "PermissionControllerActionGuid",
                value: new Guid("473c99ba-d619-4b1e-951a-2ab961ea0d9a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 38,
                column: "PermissionControllerActionGuid",
                value: new Guid("9ea883b2-c978-47af-9cfb-819528c982a6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 39,
                column: "PermissionControllerActionGuid",
                value: new Guid("317939f3-27d3-454a-8a1c-f3a3e4d148bc"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 40,
                column: "PermissionControllerActionGuid",
                value: new Guid("4df78d98-89ca-4af7-a460-d802124368a0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 41,
                column: "PermissionControllerActionGuid",
                value: new Guid("d916928b-a40a-4bb1-b541-dadc918a933f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 42,
                column: "PermissionControllerActionGuid",
                value: new Guid("cdbe6ab4-7f79-4d3e-8199-6bad254ecb18"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 43,
                column: "PermissionControllerActionGuid",
                value: new Guid("5b672ba4-4963-47fa-84d9-299ca88fad61"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 44,
                column: "PermissionControllerActionGuid",
                value: new Guid("78f77dda-5a99-4a6b-8339-bb525adfafd9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 45,
                column: "PermissionControllerActionGuid",
                value: new Guid("8512e527-fe66-4cd5-9cc5-a9c47d549fc2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 46,
                column: "PermissionControllerActionGuid",
                value: new Guid("33b15eb4-47d3-4d23-8f14-a192eaae01eb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 47,
                column: "PermissionControllerActionGuid",
                value: new Guid("621c3512-fb05-4a24-80fd-aaf2af30281c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 48,
                column: "PermissionControllerActionGuid",
                value: new Guid("33074346-4d0d-46fd-9270-afe2482236e9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 49,
                column: "PermissionControllerActionGuid",
                value: new Guid("48e3f3ca-bd2f-4dee-b82c-9d191feb7787"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 50,
                column: "PermissionControllerActionGuid",
                value: new Guid("1087dda4-d7a6-419f-848b-804f4e6ad317"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 51,
                column: "PermissionControllerActionGuid",
                value: new Guid("c1e8cfee-28da-45cf-8483-76e93c4b0245"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 52,
                column: "PermissionControllerActionGuid",
                value: new Guid("37a8c9e1-db72-4111-b2e5-7c79ae291d98"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 53,
                column: "PermissionControllerActionGuid",
                value: new Guid("382c3543-1487-4a50-9691-43a45c7062ce"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 54,
                column: "PermissionControllerActionGuid",
                value: new Guid("dffe06d0-00e1-46e3-b20a-66a41e6f23b6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 55,
                column: "PermissionControllerActionGuid",
                value: new Guid("b4367ef8-5d0c-4d7e-ac54-9e2e6c67f1ca"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 56,
                column: "PermissionControllerActionGuid",
                value: new Guid("9216f355-ceb0-4140-9e17-b52a64f15aca"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 57,
                column: "PermissionControllerActionGuid",
                value: new Guid("22fd934a-6d5e-422d-858b-89be31862562"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 58,
                column: "PermissionControllerActionGuid",
                value: new Guid("cb8173f0-3715-440e-949c-06bcc03f3cb9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 59,
                column: "PermissionControllerActionGuid",
                value: new Guid("b890c51f-fe4b-4c53-abef-5276794878ef"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 60,
                column: "PermissionControllerActionGuid",
                value: new Guid("7a0b300d-d52c-4042-8b70-c7293488cffc"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 61,
                column: "PermissionControllerActionGuid",
                value: new Guid("f173f186-79db-412f-a30f-5b79a2cdb985"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 62,
                column: "PermissionControllerActionGuid",
                value: new Guid("18d5a19d-f43c-4273-a89b-ef858a7fd39e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 63,
                column: "PermissionControllerActionGuid",
                value: new Guid("bf5a6071-7c96-4d28-bf0f-d277d5093b91"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 64,
                column: "PermissionControllerActionGuid",
                value: new Guid("80809b87-6e0b-4773-9b0e-0241eb40b202"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 65,
                column: "PermissionControllerActionGuid",
                value: new Guid("1e84bca3-263f-4fe5-b608-54cef27e685f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 66,
                column: "PermissionControllerActionGuid",
                value: new Guid("6eb6cac6-d1f2-4482-88b3-a4a47cd5340b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 67,
                column: "PermissionControllerActionGuid",
                value: new Guid("5e76048d-c369-4bdb-810b-c9f42e0e22ed"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 68,
                column: "PermissionControllerActionGuid",
                value: new Guid("577c07cc-d93c-40ed-a337-7feb39621a59"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 69,
                column: "PermissionControllerActionGuid",
                value: new Guid("b6513889-c631-4f6c-8d05-0c1b0b22ffd9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 70,
                column: "PermissionControllerActionGuid",
                value: new Guid("437f5639-bc5f-4b74-89a0-4028bc790141"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 71,
                column: "PermissionControllerActionGuid",
                value: new Guid("bc22bb10-1d62-4bbf-8889-a66e1d4fc3cd"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 72,
                column: "PermissionControllerActionGuid",
                value: new Guid("8de38ad2-c4aa-4f91-b784-50e8d824bced"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 73,
                column: "PermissionControllerActionGuid",
                value: new Guid("619cbac5-ab82-40a9-a1dc-6ee4fc5dfecc"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 74,
                column: "PermissionControllerActionGuid",
                value: new Guid("519e9bba-36ee-4429-87ab-3218448db775"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 75,
                column: "PermissionControllerActionGuid",
                value: new Guid("f368196b-78dd-4b5e-a473-d58add492e88"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 76,
                column: "PermissionControllerActionGuid",
                value: new Guid("be496bfb-2628-4e02-bd19-1d262744052e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 77,
                column: "PermissionControllerActionGuid",
                value: new Guid("6e44e554-d773-48a9-97a5-1a96102f6d31"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 78,
                column: "PermissionControllerActionGuid",
                value: new Guid("eaff5124-1e99-40d0-bb4a-732430c4e7c8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 79,
                column: "PermissionControllerActionGuid",
                value: new Guid("03c24e86-ea1c-443f-96db-1c11a2d16ceb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 80,
                column: "PermissionControllerActionGuid",
                value: new Guid("44da9deb-9755-40dc-98dc-b55ff7d4b153"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 81,
                column: "PermissionControllerActionGuid",
                value: new Guid("d77a8d90-8260-4dbb-a2f1-1335dbc963c4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 82,
                column: "PermissionControllerActionGuid",
                value: new Guid("49465273-a3d3-4646-b1c8-0f06d1c8f718"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 83,
                column: "PermissionControllerActionGuid",
                value: new Guid("2dc78ad7-ef06-4fb5-8519-a9454ae78c50"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 84,
                column: "PermissionControllerActionGuid",
                value: new Guid("2b0ab23a-21b7-43c7-9c81-875bb613548b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 85,
                column: "PermissionControllerActionGuid",
                value: new Guid("a72f5022-42b5-4b7e-b6f0-5d272e89e6ef"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 86,
                column: "PermissionControllerActionGuid",
                value: new Guid("8af95976-d72a-402d-847d-61ae34fafd50"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 87,
                column: "PermissionControllerActionGuid",
                value: new Guid("bee63455-208f-43c5-ac30-2d5abc936aa1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 88,
                column: "PermissionControllerActionGuid",
                value: new Guid("cdbc460f-c945-4b10-9cba-e17197aa14d7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 89,
                column: "PermissionControllerActionGuid",
                value: new Guid("c47edd85-096a-4d7a-8562-d30e863a207e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 90,
                column: "PermissionControllerActionGuid",
                value: new Guid("f6968fec-b335-438a-92b2-03e6969ac718"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 91,
                column: "PermissionControllerActionGuid",
                value: new Guid("db9ce711-22ac-459e-97ef-71fba4531bd2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 92,
                column: "PermissionControllerActionGuid",
                value: new Guid("c7af0ec3-85ce-45e5-9fe4-58bce83d6011"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 93,
                column: "PermissionControllerActionGuid",
                value: new Guid("c56639be-20b3-4131-895d-43ef5bbe3836"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 94,
                column: "PermissionControllerActionGuid",
                value: new Guid("c9b43a8b-16bb-4735-86a9-0e2b76b54293"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 95,
                column: "PermissionControllerActionGuid",
                value: new Guid("7aabb543-e94b-404c-b0da-5a11f299c3a2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 96,
                column: "PermissionControllerActionGuid",
                value: new Guid("dd866848-7512-41b7-b00d-97742c13b20e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 97,
                column: "PermissionControllerActionGuid",
                value: new Guid("2d8aa7ad-7d43-47ea-a8a1-420ea0b07182"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 98,
                column: "PermissionControllerActionGuid",
                value: new Guid("4f0ccf4d-7fd3-4cd0-94dd-274bbe450e63"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 99,
                column: "PermissionControllerActionGuid",
                value: new Guid("15343851-0a92-47e9-8aff-9af1ff289a06"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 100,
                column: "PermissionControllerActionGuid",
                value: new Guid("bc5b9cc2-2ffd-48dd-9b4b-3c3016f4ec2e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 101,
                column: "PermissionControllerActionGuid",
                value: new Guid("6f72bde2-6e54-48cc-a0d5-2083bacde13a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 102,
                column: "PermissionControllerActionGuid",
                value: new Guid("7db97bc4-0808-4560-bbab-acf1acf97385"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 103,
                column: "PermissionControllerActionGuid",
                value: new Guid("9265b737-de95-4faa-ac20-d12505042e56"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 104,
                column: "PermissionControllerActionGuid",
                value: new Guid("23a5807b-984c-4745-ac76-af561ec84c5d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 105,
                column: "PermissionControllerActionGuid",
                value: new Guid("8c845877-982a-47c1-8a31-cccdc3cc0fd4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 106,
                column: "PermissionControllerActionGuid",
                value: new Guid("67503b75-b9c3-481a-b596-00c053ada569"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 107,
                column: "PermissionControllerActionGuid",
                value: new Guid("6963c35c-86c6-48d0-bcd9-eaec1f134eef"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 108,
                column: "PermissionControllerActionGuid",
                value: new Guid("2b79fcbc-53be-4ad9-a93e-5490eb7ff3a8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 109,
                column: "PermissionControllerActionGuid",
                value: new Guid("099a13b5-b891-4077-b00c-c14571352263"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 110,
                column: "PermissionControllerActionGuid",
                value: new Guid("04c33b72-a812-4009-9eff-72ed2c4b1b99"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 111,
                column: "PermissionControllerActionGuid",
                value: new Guid("fc75ab7a-dbdd-403e-8945-362fc6c5b4bd"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 112,
                column: "PermissionControllerActionGuid",
                value: new Guid("98cfc2e4-b176-4a0d-a973-b6170e71c8d2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 113,
                column: "PermissionControllerActionGuid",
                value: new Guid("69c9df2f-460a-4718-ab71-103bc928b431"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 114,
                column: "PermissionControllerActionGuid",
                value: new Guid("2030eb93-30e1-44a5-aaf7-ba6a554b9f04"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 115,
                column: "PermissionControllerActionGuid",
                value: new Guid("327c66d3-d894-4e2f-bc6e-ed681ab93bde"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 116,
                column: "PermissionControllerActionGuid",
                value: new Guid("798807e0-03e5-418b-a497-3b77395af2b5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 117,
                column: "PermissionControllerActionGuid",
                value: new Guid("096e91a6-7aa5-460c-ab70-eace8b992ebf"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 118,
                column: "PermissionControllerActionGuid",
                value: new Guid("fdfcb987-ef89-4a6f-ae34-687e263b07b8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 119,
                column: "PermissionControllerActionGuid",
                value: new Guid("94ae2ed1-9f57-46fd-b5bc-c03edb256253"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 120,
                column: "PermissionControllerActionGuid",
                value: new Guid("c1a60d62-ba3f-454d-97a2-749a6862a021"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 121,
                column: "PermissionControllerActionGuid",
                value: new Guid("d1c2b80a-c723-4076-8b7c-41d593db572c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 122,
                column: "PermissionControllerActionGuid",
                value: new Guid("7946f0b5-6e18-4169-aa81-5e70ab52cf2c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 123,
                column: "PermissionControllerActionGuid",
                value: new Guid("2252c3e0-670b-4325-913b-73342d9dee66"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 124,
                column: "PermissionControllerActionGuid",
                value: new Guid("6df9661f-dfe6-4e85-a0d5-8ceadc00a08e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 125,
                column: "PermissionControllerActionGuid",
                value: new Guid("be380657-25d0-434f-8026-983899fc7e62"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 126,
                column: "PermissionControllerActionGuid",
                value: new Guid("45522229-b37c-47cf-b75f-c3f919dca276"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 127,
                column: "PermissionControllerActionGuid",
                value: new Guid("06d8b5a8-b0ff-433b-913d-43c181bee9fc"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 128,
                column: "PermissionControllerActionGuid",
                value: new Guid("e79a3862-7a27-4c9f-befe-827da3eebd67"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 129,
                column: "PermissionControllerActionGuid",
                value: new Guid("6172224c-da59-4a25-9902-b9c5394d8e2b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 130,
                column: "PermissionControllerActionGuid",
                value: new Guid("92609384-6119-41dc-8485-cb7bfcc969d1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 131,
                column: "PermissionControllerActionGuid",
                value: new Guid("12d4a612-194b-401e-b15d-3040aa15dd34"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 132,
                column: "PermissionControllerActionGuid",
                value: new Guid("5c4febf8-44d6-450d-a100-72166aece327"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 133,
                column: "PermissionControllerActionGuid",
                value: new Guid("40779c6d-0e2d-41ed-8c0b-d058620e8242"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 134,
                column: "PermissionControllerActionGuid",
                value: new Guid("952ab286-b0a0-485c-8b34-813685d50627"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 135,
                column: "PermissionControllerActionGuid",
                value: new Guid("42653474-3638-4534-a42e-04828950f12c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 136,
                column: "PermissionControllerActionGuid",
                value: new Guid("74c99e98-285f-4880-bee5-428be921d0db"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 137,
                column: "PermissionControllerActionGuid",
                value: new Guid("2f5e9712-e258-44dc-a610-e88320371821"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 138,
                column: "PermissionControllerActionGuid",
                value: new Guid("852812e5-4ee0-4010-9ad0-a6a6bb139bd7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 139,
                column: "PermissionControllerActionGuid",
                value: new Guid("3ca2d94f-9144-4026-bbf2-6e57e692e03a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 140,
                column: "PermissionControllerActionGuid",
                value: new Guid("e414dc1b-6df6-4b33-8b31-672384925b48"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 141,
                column: "PermissionControllerActionGuid",
                value: new Guid("c7e12887-362b-4805-94be-63f51b8d5266"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 142,
                column: "PermissionControllerActionGuid",
                value: new Guid("392f02cf-cd47-475a-9221-ac25d431634d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 143,
                column: "PermissionControllerActionGuid",
                value: new Guid("cea0d2a6-0794-4c34-b1f9-0a95a8e84036"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 144,
                column: "PermissionControllerActionGuid",
                value: new Guid("689f8669-f6f4-4c43-9d3c-d0325d52a0c0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 145,
                column: "PermissionControllerActionGuid",
                value: new Guid("73fb3360-3041-4e44-a0cc-d8d5bfdc4696"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 146,
                column: "PermissionControllerActionGuid",
                value: new Guid("566ed3b1-8bfa-412a-aa9d-c799221a3d71"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 147,
                column: "PermissionControllerActionGuid",
                value: new Guid("1e8a018a-81c8-42cd-934f-d2e534066d21"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 148,
                column: "PermissionControllerActionGuid",
                value: new Guid("c1d92f0c-ebd2-4f2a-ae98-40e2a8fa2a2e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 149,
                column: "PermissionControllerActionGuid",
                value: new Guid("dcfba536-c1a7-4681-bc10-ecc4b7008626"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 150,
                column: "PermissionControllerActionGuid",
                value: new Guid("ca490d6d-caeb-46db-97e5-23fad5630c9c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 151,
                column: "PermissionControllerActionGuid",
                value: new Guid("34d5475d-66c6-4f84-ab86-534286c931e5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 152,
                column: "PermissionControllerActionGuid",
                value: new Guid("1ba19d32-dc8f-45fe-9142-c70390334f95"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 153,
                column: "PermissionControllerActionGuid",
                value: new Guid("87e4bd57-fe1b-463a-a25f-701dc4dc0a3e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 154,
                column: "PermissionControllerActionGuid",
                value: new Guid("eebf12da-acf3-4054-9cc3-b7c66d449965"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 155,
                column: "PermissionControllerActionGuid",
                value: new Guid("5a84c4b0-fd28-4fbf-a863-788bdc49877a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 156,
                column: "PermissionControllerActionGuid",
                value: new Guid("e7180f18-920c-4e7c-ab8c-e3917b36fb20"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 157,
                column: "PermissionControllerActionGuid",
                value: new Guid("e64bcb43-5d80-466b-9675-ae84cf0233d1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 158,
                column: "PermissionControllerActionGuid",
                value: new Guid("745e8079-7853-4e90-80e5-0148c9870aaa"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 159,
                column: "PermissionControllerActionGuid",
                value: new Guid("13d67d95-f162-4023-940b-75aa9dccea96"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 160,
                column: "PermissionControllerActionGuid",
                value: new Guid("e73eeee7-f550-4366-b5e8-f1faf2a7b618"));
        }
    }
}
