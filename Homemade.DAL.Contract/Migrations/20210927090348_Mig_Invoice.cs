using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Homemade.DAL.Contract.Migrations
{
    public partial class Mig_Invoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Site");

            migrationBuilder.CreateTable(
                name: "CartMaster",
                schema: "Site",
                columns: table => new
                {
                    CartMasterID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartMasterGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SessionID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CartMasterStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsAnOrder = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CustomersID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartMaster", x => x.CartMasterID);
                    table.ForeignKey(
                        name: "FK_CartMaster_Customers_CustomersID",
                        column: x => x.CustomersID,
                        principalSchema: "Customer",
                        principalTable: "Customers",
                        principalColumn: "CustomersID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceDetails",
                schema: "Order",
                columns: table => new
                {
                    InvoiceDetailsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceDetailsGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceMasterID = table.Column<int>(type: "int", nullable: false),
                    OrderVendorID = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Vat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                    table.PrimaryKey("PK_InvoiceDetails", x => x.InvoiceDetailsID);
                    table.ForeignKey(
                        name: "FK_InvoiceDetails_OrderVendor_OrderVendorID",
                        column: x => x.OrderVendorID,
                        principalSchema: "Order",
                        principalTable: "OrderVendor",
                        principalColumn: "OrderVendorID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InvoiceDetails_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceMaster",
                schema: "Order",
                columns: table => new
                {
                    InvoiceMasterID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceMasterGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceStatusId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Vat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InvoiceNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PerStore = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PerHomeMade = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VendorsID = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_InvoiceMaster", x => x.InvoiceMasterID);
                    table.ForeignKey(
                        name: "FK_InvoiceMaster_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InvoiceMaster_Vendors_VendorsID",
                        column: x => x.VendorsID,
                        principalSchema: "Vendor",
                        principalTable: "Vendors",
                        principalColumn: "VendorsID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                schema: "Order",
                columns: table => new
                {
                    NotificationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotificationGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NotificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TitleAR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TitleEN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescAR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescEN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSeen = table.Column<bool>(type: "bit", nullable: false),
                    NotificationTypeID = table.Column<int>(type: "int", nullable: false),
                    OrdersID = table.Column<int>(type: "int", nullable: true),
                    VendorsID = table.Column<int>(type: "int", nullable: true),
                    CustomersID = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_Notification", x => x.NotificationID);
                    table.ForeignKey(
                        name: "FK_Notification_Customers_CustomersID",
                        column: x => x.CustomersID,
                        principalSchema: "Customer",
                        principalTable: "Customers",
                        principalColumn: "CustomersID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notification_Orders_OrdersID",
                        column: x => x.OrdersID,
                        principalSchema: "Order",
                        principalTable: "Orders",
                        principalColumn: "OrdersID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notification_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notification_Vendors_VendorsID",
                        column: x => x.VendorsID,
                        principalSchema: "Vendor",
                        principalTable: "Vendors",
                        principalColumn: "VendorsID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CartDetails",
                columns: table => new
                {
                    CartDetailsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartDetailsGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    ProductNameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductNameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CartMasterID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartDetails", x => x.CartDetailsID);
                    table.ForeignKey(
                        name: "FK_CartDetails_CartMaster_CartMasterID",
                        column: x => x.CartMasterID,
                        principalSchema: "Site",
                        principalTable: "CartMaster",
                        principalColumn: "CartMasterID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CartDetails_Product_ProductID",
                        column: x => x.ProductID,
                        principalSchema: "Vendor",
                        principalTable: "Product",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "8a764860-77d4-4240-8d76-4f1e1e036f64");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "7861e2f6-70ae-4a89-b16e-c00493f0bcd2");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "b187ecad-6c8e-4dbb-826b-bd561271edb3");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "17ddbc0a-f358-4e78-8b54-7ac24b5d0447");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "853cc46d-ff95-4a55-ae02-6323327491b9", "d73f3b34-10dd-45dd-a34a-d3fb0b83adde" });

            migrationBuilder.UpdateData(
                schema: "Emp",
                table: "Employees",
                keyColumn: "EntityEmpID",
                keyValue: 1,
                columns: new[] { "CreateDate", "EnableDate" },
                values: new object[] { new DateTime(2021, 9, 27, 11, 3, 45, 428, DateTimeKind.Local).AddTicks(7066), new DateTime(2021, 9, 27, 11, 3, 45, 428, DateTimeKind.Local).AddTicks(7589) });

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionAction",
                keyColumn: "PermissionActionID",
                keyValue: 1,
                column: "PermissionActionGuid",
                value: new Guid("fe8a7a23-83e3-4c22-8c31-f40a85b7b9b5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionAction",
                keyColumn: "PermissionActionID",
                keyValue: 2,
                column: "PermissionActionGuid",
                value: new Guid("dd09e685-521e-4813-9f2d-b391a13c749b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionAction",
                keyColumn: "PermissionActionID",
                keyValue: 3,
                column: "PermissionActionGuid",
                value: new Guid("ebe5a467-35d6-4928-801c-275db999ee84"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionAction",
                keyColumn: "PermissionActionID",
                keyValue: 4,
                column: "PermissionActionGuid",
                value: new Guid("c2a407e9-99e1-451c-98ed-2f83c0ae7c1c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 1,
                column: "PermissionControllerGuid",
                value: new Guid("c629040a-66c0-47d4-91ac-43e288b5b9e9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 2,
                column: "PermissionControllerGuid",
                value: new Guid("ff0751e6-10e1-4676-b3e0-1e6890d0c970"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 3,
                column: "PermissionControllerGuid",
                value: new Guid("7bbcee14-f752-4f12-ab79-0e274c00def5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 4,
                column: "PermissionControllerGuid",
                value: new Guid("9bc4a71a-1681-41b4-b362-71942cdc9cc6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 5,
                column: "PermissionControllerGuid",
                value: new Guid("eda29bc2-59b2-4c1e-960c-d11568ad2bcf"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 6,
                column: "PermissionControllerGuid",
                value: new Guid("c65f1fa9-80cc-4105-92a1-9795bc94e658"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 7,
                column: "PermissionControllerGuid",
                value: new Guid("510aeb3d-3e7c-48ee-9dc9-2fed960f8fc0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 8,
                column: "PermissionControllerGuid",
                value: new Guid("09b7fd9f-c15e-4b08-8a3f-5ad297b6eb3b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 9,
                column: "PermissionControllerGuid",
                value: new Guid("90e49392-7370-45b0-b132-8fedd034542f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 10,
                column: "PermissionControllerGuid",
                value: new Guid("56877de4-88b1-48a5-8a20-8bb43aaefff8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 11,
                column: "PermissionControllerGuid",
                value: new Guid("6ef2233d-e43d-4d5c-abc8-a79b32e2160d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 12,
                column: "PermissionControllerGuid",
                value: new Guid("8ca2bf33-95c5-438f-b18b-2d496e96b493"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 13,
                column: "PermissionControllerGuid",
                value: new Guid("14f277dc-05a1-47a6-95cb-5403f8b73025"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 14,
                column: "PermissionControllerGuid",
                value: new Guid("56fd1c8f-9ad8-4862-90b9-f44fc3005c5e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 15,
                column: "PermissionControllerGuid",
                value: new Guid("394dcfa7-e659-45e1-a8e6-b16ef8ee81af"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 16,
                column: "PermissionControllerGuid",
                value: new Guid("52354213-8753-446e-bbcd-3e00452426f7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 17,
                column: "PermissionControllerGuid",
                value: new Guid("87d376b3-3396-45ca-842f-944f3c413a2c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 18,
                column: "PermissionControllerGuid",
                value: new Guid("d5818a31-6937-4da9-908b-a22e59df2334"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 19,
                column: "PermissionControllerGuid",
                value: new Guid("6ebf6221-eb5a-4c83-ad9e-54cbda7bb750"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 20,
                column: "PermissionControllerGuid",
                value: new Guid("80520895-d49d-482a-94a9-59cf3d24fb39"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 21,
                column: "PermissionControllerGuid",
                value: new Guid("b33b46be-7723-46ff-8298-74ae238f2be8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 22,
                column: "PermissionControllerGuid",
                value: new Guid("bcff8ce6-920b-4445-b51e-fa9c56dd2b90"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 23,
                column: "PermissionControllerGuid",
                value: new Guid("bd009779-8daa-44d5-8b45-fec8e45de611"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 24,
                column: "PermissionControllerGuid",
                value: new Guid("a0c9b60b-262e-4b6f-a0d9-d0da21e383eb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 25,
                column: "PermissionControllerGuid",
                value: new Guid("3e9bb672-a1c9-4fe3-8fcf-d7ae5c453219"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 26,
                column: "PermissionControllerGuid",
                value: new Guid("5678522e-8dc4-4c0c-8f5d-cf46b1ee6161"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 27,
                column: "PermissionControllerGuid",
                value: new Guid("7ad454ef-f816-4999-ad45-e7f2a9e477ac"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 28,
                column: "PermissionControllerGuid",
                value: new Guid("fe410aa9-e066-4ed6-99ba-18cecb71eccb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 29,
                column: "PermissionControllerGuid",
                value: new Guid("e2131c88-1b12-4f82-af93-e0cb86bd4b37"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 30,
                column: "PermissionControllerGuid",
                value: new Guid("8baa4cb0-765f-4788-a7c4-616824892ec7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 31,
                column: "PermissionControllerGuid",
                value: new Guid("42e27aa5-1fd7-4de6-b140-69e8bfc31e04"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 32,
                column: "PermissionControllerGuid",
                value: new Guid("61334254-f8f4-4403-8ec1-6e1871272ab0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 33,
                column: "PermissionControllerGuid",
                value: new Guid("445aa0ab-f780-44ed-826f-3d9a83242b82"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 34,
                column: "PermissionControllerGuid",
                value: new Guid("f3f97fc9-1baf-418c-abba-eb67e5fcab67"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 1,
                column: "PermissionControllerActionGuid",
                value: new Guid("5e4a204f-efb0-4346-9faf-d885a2587335"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 2,
                column: "PermissionControllerActionGuid",
                value: new Guid("5a1c4be5-15e1-4bb7-865b-c6956a0816db"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 3,
                column: "PermissionControllerActionGuid",
                value: new Guid("4704aa9e-431c-4172-96fa-c1e3c170a266"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 4,
                column: "PermissionControllerActionGuid",
                value: new Guid("3c7b9035-d8ff-41e5-b2c2-84cf526a6bf5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 5,
                column: "PermissionControllerActionGuid",
                value: new Guid("35a8e7e5-f812-4418-a374-31f20813be17"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 6,
                column: "PermissionControllerActionGuid",
                value: new Guid("11778186-fccc-4c2a-8388-567d2c9a3bc0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 7,
                column: "PermissionControllerActionGuid",
                value: new Guid("e2cc100f-9cdd-4c0a-bdc4-432957fd9e53"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 8,
                column: "PermissionControllerActionGuid",
                value: new Guid("c73e7e1f-4273-470a-aaee-427b621d459c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 9,
                column: "PermissionControllerActionGuid",
                value: new Guid("f48345e6-710e-4712-ab3d-ef5c401ebf43"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 10,
                column: "PermissionControllerActionGuid",
                value: new Guid("67ac3357-068b-4f15-899d-ef0b0c809216"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 11,
                column: "PermissionControllerActionGuid",
                value: new Guid("b4932c9a-4e35-41d4-a4e9-1fe7dae766e0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 12,
                column: "PermissionControllerActionGuid",
                value: new Guid("3b77aa73-433a-434d-be26-cd7b591a34eb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 13,
                column: "PermissionControllerActionGuid",
                value: new Guid("1b322dc8-72ad-45bf-9aac-99e93d4ee640"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 14,
                column: "PermissionControllerActionGuid",
                value: new Guid("60a980e8-cc63-47f0-b688-70441b54d7ce"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 15,
                column: "PermissionControllerActionGuid",
                value: new Guid("72237b56-7e5c-46ed-ba9d-177ce93425f3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 16,
                column: "PermissionControllerActionGuid",
                value: new Guid("ab75216c-671a-418d-be6a-ae5fe2088199"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 17,
                column: "PermissionControllerActionGuid",
                value: new Guid("169ec89c-73f5-42df-b0fc-a2f3f7324282"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 18,
                column: "PermissionControllerActionGuid",
                value: new Guid("e0fffd22-53d9-4af9-882f-1d8ccc4c533b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 19,
                column: "PermissionControllerActionGuid",
                value: new Guid("19410940-6695-4647-bfad-fd2862df547b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 20,
                column: "PermissionControllerActionGuid",
                value: new Guid("b8c4e99a-1e53-405b-95d5-cf49f7461be3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 21,
                column: "PermissionControllerActionGuid",
                value: new Guid("b7da3f82-42ec-42d8-a9b2-63fec7d91955"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 22,
                column: "PermissionControllerActionGuid",
                value: new Guid("066de0ff-bdc4-4265-a15f-64df859e688e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 23,
                column: "PermissionControllerActionGuid",
                value: new Guid("b640a98e-bb86-4b10-9208-58503fb2818e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 24,
                column: "PermissionControllerActionGuid",
                value: new Guid("8be14180-12f7-4cde-ab5c-a738e717e114"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 25,
                column: "PermissionControllerActionGuid",
                value: new Guid("cecfef20-1871-456f-9523-80ede5177805"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 26,
                column: "PermissionControllerActionGuid",
                value: new Guid("4f376899-44cb-46cf-a7b2-951f71a01457"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 27,
                column: "PermissionControllerActionGuid",
                value: new Guid("886ad345-a33f-4fe4-9aa7-db741288057b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 28,
                column: "PermissionControllerActionGuid",
                value: new Guid("4383ccfe-6cc2-4f80-8142-81288ff5094d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 29,
                column: "PermissionControllerActionGuid",
                value: new Guid("b3225e34-8e80-4a90-ad7e-27a2d053815d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 30,
                column: "PermissionControllerActionGuid",
                value: new Guid("20ecabb1-fbd5-49e7-bd97-d59a48600b7e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 31,
                column: "PermissionControllerActionGuid",
                value: new Guid("584992d8-af19-4ab3-a8ea-217f4a2f5d0a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 32,
                column: "PermissionControllerActionGuid",
                value: new Guid("a988a62d-f8e7-4359-87ec-01ea857a4b32"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 33,
                column: "PermissionControllerActionGuid",
                value: new Guid("694208dd-d6c8-418b-a127-f920c4955195"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 34,
                column: "PermissionControllerActionGuid",
                value: new Guid("2657e8b7-08c8-4b95-938e-e5206bdeb300"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 35,
                column: "PermissionControllerActionGuid",
                value: new Guid("a1fc059c-fa8c-474e-8445-92094a4b82ad"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 36,
                column: "PermissionControllerActionGuid",
                value: new Guid("c4c8599b-9d8f-4835-b25f-0143906878dc"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 37,
                column: "PermissionControllerActionGuid",
                value: new Guid("fdc03cbb-7923-4118-b870-e008d150e6b6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 38,
                column: "PermissionControllerActionGuid",
                value: new Guid("ac2c6ca5-6921-48de-98e2-f80d5ce90084"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 39,
                column: "PermissionControllerActionGuid",
                value: new Guid("e926174a-0a1a-4551-a889-6c4d0caaf9bb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 40,
                column: "PermissionControllerActionGuid",
                value: new Guid("8e48d7a9-0acc-4f9a-b4eb-4bde8adb5045"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 41,
                column: "PermissionControllerActionGuid",
                value: new Guid("aeb0e070-3299-4c7d-8ce1-7af320764790"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 42,
                column: "PermissionControllerActionGuid",
                value: new Guid("78f10272-21f8-4031-b6ce-a51b2d4076dc"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 43,
                column: "PermissionControllerActionGuid",
                value: new Guid("f1357af2-5b55-4ab4-9d9d-9b074d19a87e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 44,
                column: "PermissionControllerActionGuid",
                value: new Guid("b4f9345e-537e-4250-adc0-5b9336292bf2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 45,
                column: "PermissionControllerActionGuid",
                value: new Guid("b98c0533-cc38-4a31-8792-ec9d2452feac"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 46,
                column: "PermissionControllerActionGuid",
                value: new Guid("76989da0-8215-4936-8824-d03748649f14"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 47,
                column: "PermissionControllerActionGuid",
                value: new Guid("1b279925-8a15-48b7-9fbe-f469fa47aa47"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 48,
                column: "PermissionControllerActionGuid",
                value: new Guid("114b8755-5587-4651-b540-bd401ddbdb98"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 49,
                column: "PermissionControllerActionGuid",
                value: new Guid("6658b257-0461-4a0e-90c6-7cf81bb25ab5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 50,
                column: "PermissionControllerActionGuid",
                value: new Guid("3f794995-225f-4e09-adbc-6eea1e6d6f13"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 51,
                column: "PermissionControllerActionGuid",
                value: new Guid("3754546c-4a27-4be2-9220-b343769e3359"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 52,
                column: "PermissionControllerActionGuid",
                value: new Guid("0f7e813d-a493-477e-b5ae-1856bb203317"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 53,
                column: "PermissionControllerActionGuid",
                value: new Guid("eb36fcb0-04e0-47f0-828d-f995fc73543a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 54,
                column: "PermissionControllerActionGuid",
                value: new Guid("059854bd-b3ab-47c0-a6c6-adacb00fa7dc"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 55,
                column: "PermissionControllerActionGuid",
                value: new Guid("a6d520fe-7dcb-4f22-b89d-671294b1699c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 56,
                column: "PermissionControllerActionGuid",
                value: new Guid("5cc35655-8218-4d74-a809-66ecae219481"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 57,
                column: "PermissionControllerActionGuid",
                value: new Guid("0c841605-6452-49cc-8650-affec34a8d1c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 58,
                column: "PermissionControllerActionGuid",
                value: new Guid("09094a01-3fa7-46f4-b9b2-1acff41321c1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 59,
                column: "PermissionControllerActionGuid",
                value: new Guid("86bfc255-a054-4e5a-99db-f38934d06821"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 60,
                column: "PermissionControllerActionGuid",
                value: new Guid("df683347-0c4c-42eb-acaa-c01a9a8db503"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 61,
                column: "PermissionControllerActionGuid",
                value: new Guid("8bc8cbe3-cdf2-4713-acbf-651f806e6ef0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 62,
                column: "PermissionControllerActionGuid",
                value: new Guid("aec39851-f9e3-4dfd-b3e5-a70b531c7292"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 63,
                column: "PermissionControllerActionGuid",
                value: new Guid("e5e0a69f-1098-4257-a9af-adb215fc69eb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 64,
                column: "PermissionControllerActionGuid",
                value: new Guid("0cb4625a-8dbd-4b32-ad2f-4053f82616a4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 65,
                column: "PermissionControllerActionGuid",
                value: new Guid("084d88f0-8432-435a-b9c8-6108dc944277"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 66,
                column: "PermissionControllerActionGuid",
                value: new Guid("bb38f088-e2ee-4f98-b685-ec92e03b54b1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 67,
                column: "PermissionControllerActionGuid",
                value: new Guid("25fc7b71-867f-4fd4-92c4-45c5ee0574f1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 68,
                column: "PermissionControllerActionGuid",
                value: new Guid("0a4f7e1d-11b3-4d39-9a10-33b185a4fa60"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 69,
                column: "PermissionControllerActionGuid",
                value: new Guid("93d066e7-7ae1-4518-8600-ee775d1abe25"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 70,
                column: "PermissionControllerActionGuid",
                value: new Guid("cc215958-a6b2-422b-a2ed-72dba11f3171"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 71,
                column: "PermissionControllerActionGuid",
                value: new Guid("5799f6ad-4956-4489-a4ea-fe80bcf24a13"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 72,
                column: "PermissionControllerActionGuid",
                value: new Guid("058831cd-d0e0-4f55-b68e-ccb1f5239852"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 73,
                column: "PermissionControllerActionGuid",
                value: new Guid("74b1c894-7eeb-4bb9-912d-3fa05d2db78f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 74,
                column: "PermissionControllerActionGuid",
                value: new Guid("f40b4dbd-fc0e-41a7-96f1-eb10c88b5e80"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 75,
                column: "PermissionControllerActionGuid",
                value: new Guid("eb0137c7-9b1d-4546-ba09-db59f7a653ae"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 76,
                column: "PermissionControllerActionGuid",
                value: new Guid("cd90b90a-ed4d-4bda-81fa-60e4c0dd1f62"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 77,
                column: "PermissionControllerActionGuid",
                value: new Guid("2f7220b7-b5e8-431e-9ecd-368a9fa8c5cc"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 78,
                column: "PermissionControllerActionGuid",
                value: new Guid("d9e36c15-1292-483f-b8a3-3358798fa017"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 79,
                column: "PermissionControllerActionGuid",
                value: new Guid("33b28006-03f9-466a-9d08-50c76d9959ed"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 80,
                column: "PermissionControllerActionGuid",
                value: new Guid("3349f899-f562-47f2-9798-ba03f7c15763"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 81,
                column: "PermissionControllerActionGuid",
                value: new Guid("839313c5-7e36-4fcf-af6c-a8475a9763ca"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 82,
                column: "PermissionControllerActionGuid",
                value: new Guid("d53e6e3c-fddc-4193-9419-9d8b5a186371"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 83,
                column: "PermissionControllerActionGuid",
                value: new Guid("a3ec0c24-573a-4c16-861f-63f850e73cb3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 84,
                column: "PermissionControllerActionGuid",
                value: new Guid("e509ea74-b3b0-4bd9-b5e8-25ba763ffca6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 85,
                column: "PermissionControllerActionGuid",
                value: new Guid("e3cf6f0e-77c2-4416-88e8-0d23161a3ecf"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 86,
                column: "PermissionControllerActionGuid",
                value: new Guid("81f30aff-64b2-466b-a596-fbb0ee72fef7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 87,
                column: "PermissionControllerActionGuid",
                value: new Guid("fcb62828-f294-4d51-8591-f68cbcde4a74"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 88,
                column: "PermissionControllerActionGuid",
                value: new Guid("566a42af-acf5-4cfe-a8a6-58395c2323f9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 89,
                column: "PermissionControllerActionGuid",
                value: new Guid("b18570d8-af15-422b-8c2d-c38a1e437cd2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 90,
                column: "PermissionControllerActionGuid",
                value: new Guid("7e49cfb8-b122-4d30-8ad0-85f5dfb0b0e2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 91,
                column: "PermissionControllerActionGuid",
                value: new Guid("98d94c16-3875-44f8-807f-e3ec2710b7ca"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 92,
                column: "PermissionControllerActionGuid",
                value: new Guid("ff8b4c09-b2ee-4ab9-9175-aa5f54ff9a74"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 93,
                column: "PermissionControllerActionGuid",
                value: new Guid("9b242a57-52c0-4b27-b2c3-04ab2a913fbb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 94,
                column: "PermissionControllerActionGuid",
                value: new Guid("5ee96d55-0ecd-4e38-adfb-b115c6a7baec"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 95,
                column: "PermissionControllerActionGuid",
                value: new Guid("7bbd23a3-21d0-4f58-b7e3-6e1dd6d6625f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 96,
                column: "PermissionControllerActionGuid",
                value: new Guid("28462ba7-46b6-486d-82c3-3cec1006efe4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 97,
                column: "PermissionControllerActionGuid",
                value: new Guid("7f8c0088-a3a0-4a83-a1a4-8ed9a2948382"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 98,
                column: "PermissionControllerActionGuid",
                value: new Guid("51d04fc4-9905-4ece-9252-a4dfcbcbdc63"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 99,
                column: "PermissionControllerActionGuid",
                value: new Guid("0f32129a-06fc-448f-9881-8fc64490135d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 100,
                column: "PermissionControllerActionGuid",
                value: new Guid("eacdd116-e87b-456a-a3ad-1b77cc25127f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 101,
                column: "PermissionControllerActionGuid",
                value: new Guid("95933e7e-8ddd-46b8-b6bb-eaed745bc81c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 102,
                column: "PermissionControllerActionGuid",
                value: new Guid("79dda77e-d1d8-441c-8ff6-92bb58a3820d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 103,
                column: "PermissionControllerActionGuid",
                value: new Guid("920ce4f0-c9bf-4e53-ab60-f0765075a57d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 104,
                column: "PermissionControllerActionGuid",
                value: new Guid("7b9824a0-a7b1-440f-ad76-8b0c2f68b8c4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 105,
                column: "PermissionControllerActionGuid",
                value: new Guid("c8eef778-8e8d-443b-8caf-3bbfc2b986d2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 106,
                column: "PermissionControllerActionGuid",
                value: new Guid("ec0c458e-aa49-4ab2-be57-83230aa432e5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 107,
                column: "PermissionControllerActionGuid",
                value: new Guid("6bb6b743-5c5b-422e-a672-361e22d7a95d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 108,
                column: "PermissionControllerActionGuid",
                value: new Guid("3862abac-353e-4317-a3d8-3939d5d778b4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 109,
                column: "PermissionControllerActionGuid",
                value: new Guid("e7fdfa73-9297-48c0-90e0-4ce6669623fb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 110,
                column: "PermissionControllerActionGuid",
                value: new Guid("28cf1c35-98dd-44ca-bbc3-3b0bf7e0d0af"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 111,
                column: "PermissionControllerActionGuid",
                value: new Guid("f3e7c9bd-98bb-4709-b633-cf5585e8f5c9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 112,
                column: "PermissionControllerActionGuid",
                value: new Guid("541dc4ba-95df-4b98-85f6-920ea18a32ab"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 113,
                column: "PermissionControllerActionGuid",
                value: new Guid("d482051e-ed22-4a26-b088-a27454b435cd"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 114,
                column: "PermissionControllerActionGuid",
                value: new Guid("98606d5b-e8bb-4f9c-a950-fc4de73cff86"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 115,
                column: "PermissionControllerActionGuid",
                value: new Guid("2e190653-1be5-4153-afee-d23a47775bed"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 116,
                column: "PermissionControllerActionGuid",
                value: new Guid("d3493880-6571-4af9-9086-284242350b12"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 117,
                column: "PermissionControllerActionGuid",
                value: new Guid("e21f78e8-c1fd-4815-89b3-488ded5d2c32"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 118,
                column: "PermissionControllerActionGuid",
                value: new Guid("23a5a3a4-e04d-4728-85d8-acea61a33817"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 119,
                column: "PermissionControllerActionGuid",
                value: new Guid("88f3da8d-8402-4eab-900d-b844eecf1f32"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 120,
                column: "PermissionControllerActionGuid",
                value: new Guid("78ddc4e8-0722-4d28-aa94-c975170226c5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 121,
                column: "PermissionControllerActionGuid",
                value: new Guid("b594438f-2c63-4fee-9823-a08bc6a4f6d2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 122,
                column: "PermissionControllerActionGuid",
                value: new Guid("fb73308f-dfce-41fd-9014-03fab5a867a9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 123,
                column: "PermissionControllerActionGuid",
                value: new Guid("5567c332-e094-47d2-9f6e-bdc2d6aee551"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 124,
                column: "PermissionControllerActionGuid",
                value: new Guid("fe6fa10a-49bf-4103-9416-b5b18cf44055"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 125,
                column: "PermissionControllerActionGuid",
                value: new Guid("53a2b78e-101c-427b-85fc-c255c72ca365"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 126,
                column: "PermissionControllerActionGuid",
                value: new Guid("6cd3374c-cf5d-4d61-b5ab-a34a31cecec6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 127,
                column: "PermissionControllerActionGuid",
                value: new Guid("8438166e-1963-4c58-9086-03798ad82a60"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 128,
                column: "PermissionControllerActionGuid",
                value: new Guid("1eb0d170-4bd5-4fb4-abf1-cca9869f5cdb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 129,
                column: "PermissionControllerActionGuid",
                value: new Guid("600e888e-a16b-402a-90cb-1d0d41117387"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 130,
                column: "PermissionControllerActionGuid",
                value: new Guid("b52e3eb2-7fde-487b-a4ed-38e01b0d1726"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 131,
                column: "PermissionControllerActionGuid",
                value: new Guid("acff9ead-d9b2-448e-9a24-e485795c2e4f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 132,
                column: "PermissionControllerActionGuid",
                value: new Guid("23917461-100b-44c1-891f-ad33ff91b0da"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 133,
                column: "PermissionControllerActionGuid",
                value: new Guid("4f86257a-4ee1-4548-a976-f793ca965c58"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 134,
                column: "PermissionControllerActionGuid",
                value: new Guid("7d5bcd8a-0767-4c1a-90d0-164eb53d14f0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 135,
                column: "PermissionControllerActionGuid",
                value: new Guid("34987e1f-d68d-468d-b827-1401c6fb0776"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 136,
                column: "PermissionControllerActionGuid",
                value: new Guid("8dd8ea96-4019-437e-8da0-975ba02ef4c3"));

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "City",
                keyColumn: "CityID",
                keyValue: 1,
                columns: new[] { "CityGuid", "CreateDate", "EnableDate" },
                values: new object[] { new Guid("369c7384-eb14-4c3e-9ca1-7a759e9ceed8"), new DateTime(2021, 9, 27, 11, 3, 45, 424, DateTimeKind.Local).AddTicks(9642), new DateTime(2021, 9, 27, 11, 3, 45, 425, DateTimeKind.Local).AddTicks(610) });

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "Country",
                keyColumn: "CountryID",
                keyValue: 1,
                columns: new[] { "CountryGuid", "CreateDate", "EnableDate" },
                values: new object[] { new Guid("05b48b27-c85a-4f77-a138-5fa68e9f9b27"), new DateTime(2021, 9, 27, 11, 3, 45, 421, DateTimeKind.Local).AddTicks(847), new DateTime(2021, 9, 27, 11, 3, 45, 422, DateTimeKind.Local).AddTicks(5850) });

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "Departments",
                keyColumn: "DepartmentsID",
                keyValue: 1,
                columns: new[] { "CreateDate", "DepartmentsGuid", "EnableDate" },
                values: new object[] { new DateTime(2021, 9, 27, 11, 3, 45, 427, DateTimeKind.Local).AddTicks(6020), new Guid("c017a92c-c468-4e87-9d07-0e5eef3228c6"), new DateTime(2021, 9, 27, 11, 3, 45, 427, DateTimeKind.Local).AddTicks(6922) });

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "Jobs",
                keyColumn: "JobsID",
                keyValue: 1,
                columns: new[] { "CreateDate", "EnableDate", "JobsGuid" },
                values: new object[] { new DateTime(2021, 9, 27, 11, 3, 45, 426, DateTimeKind.Local).AddTicks(3915), new DateTime(2021, 9, 27, 11, 3, 45, 426, DateTimeKind.Local).AddTicks(4813), new Guid("3cd77329-569a-4f3e-8007-bad46d9d5905") });

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "MainCategory",
                keyColumn: "MainCategoryID",
                keyValue: 1,
                columns: new[] { "CreateDate", "EnableDate", "MainCategoryGuid" },
                values: new object[] { new DateTime(2021, 9, 27, 11, 3, 45, 426, DateTimeKind.Local).AddTicks(9367), new DateTime(2021, 9, 27, 11, 3, 45, 427, DateTimeKind.Local).AddTicks(460), new Guid("0c375334-70a5-4502-8a60-81eac940e534") });

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "Nationality",
                keyColumn: "NationalityID",
                keyValue: 1,
                columns: new[] { "CreateDate", "EnableDate", "NationalityGuid" },
                values: new object[] { new DateTime(2021, 9, 27, 11, 3, 45, 425, DateTimeKind.Local).AddTicks(8341), new DateTime(2021, 9, 27, 11, 3, 45, 425, DateTimeKind.Local).AddTicks(9275), new Guid("368c7e50-8d8a-48b2-9c7f-08d42368e8f2") });

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "Region",
                keyColumn: "RegionID",
                keyValue: 1,
                columns: new[] { "CreateDate", "EnableDate", "RegionGuid" },
                values: new object[] { new DateTime(2021, 9, 27, 11, 3, 45, 423, DateTimeKind.Local).AddTicks(6388), new DateTime(2021, 9, 27, 11, 3, 45, 423, DateTimeKind.Local).AddTicks(7375), new Guid("3e0beb91-3005-48da-b6e7-0e196a706181") });

            migrationBuilder.CreateIndex(
                name: "IX_CartDetails_CartMasterID",
                table: "CartDetails",
                column: "CartMasterID");

            migrationBuilder.CreateIndex(
                name: "IX_CartDetails_ProductID",
                table: "CartDetails",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_CartMaster_CustomersID",
                schema: "Site",
                table: "CartMaster",
                column: "CustomersID");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDetails_OrderVendorID",
                schema: "Order",
                table: "InvoiceDetails",
                column: "OrderVendorID");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDetails_UserId",
                schema: "Order",
                table: "InvoiceDetails",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceMaster_UserId",
                schema: "Order",
                table: "InvoiceMaster",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceMaster_VendorsID",
                schema: "Order",
                table: "InvoiceMaster",
                column: "VendorsID");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_CustomersID",
                schema: "Order",
                table: "Notification",
                column: "CustomersID");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_OrdersID",
                schema: "Order",
                table: "Notification",
                column: "OrdersID");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_UserId",
                schema: "Order",
                table: "Notification",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_VendorsID",
                schema: "Order",
                table: "Notification",
                column: "VendorsID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartDetails");

            migrationBuilder.DropTable(
                name: "InvoiceDetails",
                schema: "Order");

            migrationBuilder.DropTable(
                name: "InvoiceMaster",
                schema: "Order");

            migrationBuilder.DropTable(
                name: "Notification",
                schema: "Order");

            migrationBuilder.DropTable(
                name: "CartMaster",
                schema: "Site");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "86689997-25e7-4a29-811d-657b3214a2c3");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "ec65d9d0-b92b-4d60-a355-55557591be31");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "51b8475a-77e5-4d7c-9777-b67b489850fb");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "9b2bab45-bcb1-4182-b33e-3a36867bd4e3");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "f54b7ec9-a68d-4a94-bdbd-5a621baf23ce", "190f5f5e-330c-4901-91bd-96d6cd1c8d2c" });

            migrationBuilder.UpdateData(
                schema: "Emp",
                table: "Employees",
                keyColumn: "EntityEmpID",
                keyValue: 1,
                columns: new[] { "CreateDate", "EnableDate" },
                values: new object[] { new DateTime(2021, 9, 26, 11, 59, 17, 736, DateTimeKind.Local).AddTicks(282), new DateTime(2021, 9, 26, 11, 59, 17, 736, DateTimeKind.Local).AddTicks(816) });

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionAction",
                keyColumn: "PermissionActionID",
                keyValue: 1,
                column: "PermissionActionGuid",
                value: new Guid("0d51d8ff-1364-42fa-84a1-c124877499d0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionAction",
                keyColumn: "PermissionActionID",
                keyValue: 2,
                column: "PermissionActionGuid",
                value: new Guid("07541e65-742c-4b3a-9c97-0b53105c6bc1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionAction",
                keyColumn: "PermissionActionID",
                keyValue: 3,
                column: "PermissionActionGuid",
                value: new Guid("05ac6261-929e-45bc-8328-9d1b641b2d9f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionAction",
                keyColumn: "PermissionActionID",
                keyValue: 4,
                column: "PermissionActionGuid",
                value: new Guid("f2b96237-b08b-4753-9035-be3fa4324169"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 1,
                column: "PermissionControllerGuid",
                value: new Guid("8d7371ff-b1c9-4830-8cc6-7edc42ad9199"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 2,
                column: "PermissionControllerGuid",
                value: new Guid("bcf66e56-284c-4566-b3db-4fbdcfc714d8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 3,
                column: "PermissionControllerGuid",
                value: new Guid("747f17be-de72-46ad-bfd3-cf0c8470f169"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 4,
                column: "PermissionControllerGuid",
                value: new Guid("0c18e886-6d8d-4ff6-a867-3289b841a711"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 5,
                column: "PermissionControllerGuid",
                value: new Guid("6f56ab8b-60db-499e-9e77-b0731b37dd86"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 6,
                column: "PermissionControllerGuid",
                value: new Guid("ea4af7f3-d232-41b1-b6dc-3d5873c2672e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 7,
                column: "PermissionControllerGuid",
                value: new Guid("b3e8c11f-ae8e-4850-8b94-fcfabef0066b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 8,
                column: "PermissionControllerGuid",
                value: new Guid("04a01382-b7a1-4ff4-81fd-2da95dc55467"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 9,
                column: "PermissionControllerGuid",
                value: new Guid("8394ba84-f74c-4ae4-b9e6-5157a099dd7b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 10,
                column: "PermissionControllerGuid",
                value: new Guid("648bce6e-0783-47c4-a44c-7e7f75395d41"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 11,
                column: "PermissionControllerGuid",
                value: new Guid("7866d954-cd24-4f92-886a-fe52b32dcd34"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 12,
                column: "PermissionControllerGuid",
                value: new Guid("187aa168-4745-4011-8a36-89fd4e55915e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 13,
                column: "PermissionControllerGuid",
                value: new Guid("33aa02ff-2295-43ab-b65e-3424a7a2a680"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 14,
                column: "PermissionControllerGuid",
                value: new Guid("49fa01fd-09c6-4041-a5e2-a08f2374ac0c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 15,
                column: "PermissionControllerGuid",
                value: new Guid("0655aa40-cad3-4e1c-93fc-107a05199973"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 16,
                column: "PermissionControllerGuid",
                value: new Guid("0622281c-3a84-4b3f-880c-5d997ca8aca5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 17,
                column: "PermissionControllerGuid",
                value: new Guid("4a0c2713-0476-4e94-b2ea-260006b9bd2c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 18,
                column: "PermissionControllerGuid",
                value: new Guid("a1dfafcb-3340-4f29-b025-08bbeea3c1ee"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 19,
                column: "PermissionControllerGuid",
                value: new Guid("63eb3c08-d3d2-4562-b6ba-cef52ea5db6a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 20,
                column: "PermissionControllerGuid",
                value: new Guid("f1123dc1-d0aa-4ccb-8bc3-db16c75ccaa9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 21,
                column: "PermissionControllerGuid",
                value: new Guid("8adb404d-72da-4aed-82f3-789c92316447"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 22,
                column: "PermissionControllerGuid",
                value: new Guid("b055d298-aa4a-4c4b-9a2e-bc85870d4312"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 23,
                column: "PermissionControllerGuid",
                value: new Guid("e1b1f2f1-51c2-4b85-9659-54b5a2b312ab"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 24,
                column: "PermissionControllerGuid",
                value: new Guid("8eade912-1887-4311-b298-d2436958c9a1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 25,
                column: "PermissionControllerGuid",
                value: new Guid("7727d86c-6802-4394-ac42-258a78cdea9c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 26,
                column: "PermissionControllerGuid",
                value: new Guid("3dbd59a4-5ea2-41a1-8c8f-a9ba1f82d3f7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 27,
                column: "PermissionControllerGuid",
                value: new Guid("eb00131d-ecbc-49ce-95e3-f9d5f2d3ee3a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 28,
                column: "PermissionControllerGuid",
                value: new Guid("962b4a5f-5c62-4215-a8df-1dc987d052b2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 29,
                column: "PermissionControllerGuid",
                value: new Guid("01d45de7-7f93-49b6-ae94-51ce41160924"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 30,
                column: "PermissionControllerGuid",
                value: new Guid("e1890d32-ebd4-4fc0-8d82-18b52b9be474"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 31,
                column: "PermissionControllerGuid",
                value: new Guid("219a824a-f2d4-4fc0-abc4-1c9029eae487"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 32,
                column: "PermissionControllerGuid",
                value: new Guid("0ef7035d-9f35-41b2-a041-cfeb498e89fb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 33,
                column: "PermissionControllerGuid",
                value: new Guid("67e99e57-20d7-426a-bbe3-41e9f07c061c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 34,
                column: "PermissionControllerGuid",
                value: new Guid("0cae3362-2930-474b-bb58-3333b21a08eb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 1,
                column: "PermissionControllerActionGuid",
                value: new Guid("51d5e9a8-eca2-41af-a795-666976932b20"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 2,
                column: "PermissionControllerActionGuid",
                value: new Guid("0a78a8cc-08f1-4c7b-946e-2d82d002bc67"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 3,
                column: "PermissionControllerActionGuid",
                value: new Guid("4680d8b0-9cf7-486e-bdf7-b563469ed5b8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 4,
                column: "PermissionControllerActionGuid",
                value: new Guid("e6e5b1cb-81cf-4121-b73d-b7069d5676c2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 5,
                column: "PermissionControllerActionGuid",
                value: new Guid("554c9087-19da-4c2e-80fb-8f41ad517b51"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 6,
                column: "PermissionControllerActionGuid",
                value: new Guid("7d6f83a6-0266-4a92-8e50-cf1e26b86bc8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 7,
                column: "PermissionControllerActionGuid",
                value: new Guid("57090391-bc2b-40ba-90ca-f4d91a82c96a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 8,
                column: "PermissionControllerActionGuid",
                value: new Guid("a5eb2b43-915a-44e5-868a-6f29ea4ff70c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 9,
                column: "PermissionControllerActionGuid",
                value: new Guid("5b8ee61a-345b-4448-af53-3c66fc59b6c2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 10,
                column: "PermissionControllerActionGuid",
                value: new Guid("2c453035-37e9-45f8-9822-f89f4adeee77"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 11,
                column: "PermissionControllerActionGuid",
                value: new Guid("cc50dc31-a7fd-4ce2-b86f-8fee329bb8b4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 12,
                column: "PermissionControllerActionGuid",
                value: new Guid("67c7179c-c052-4e85-8c2a-96dbd4497a81"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 13,
                column: "PermissionControllerActionGuid",
                value: new Guid("8991f8ab-d0be-492f-8da8-f1265d5f2768"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 14,
                column: "PermissionControllerActionGuid",
                value: new Guid("2bb0b0c8-cf6f-4971-8a4f-492a76a1385b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 15,
                column: "PermissionControllerActionGuid",
                value: new Guid("0dd83141-fa74-4662-b01f-5c6fa5ea3943"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 16,
                column: "PermissionControllerActionGuid",
                value: new Guid("f5f2114b-03eb-4282-9873-116d205b9071"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 17,
                column: "PermissionControllerActionGuid",
                value: new Guid("c39e4928-91fd-4979-ad9d-f5d0fb784c00"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 18,
                column: "PermissionControllerActionGuid",
                value: new Guid("22d1b7c0-ce5c-4f70-af85-356bfd9a49de"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 19,
                column: "PermissionControllerActionGuid",
                value: new Guid("cedba9bc-7e45-4655-bc53-b57846e6ede7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 20,
                column: "PermissionControllerActionGuid",
                value: new Guid("b515fc0c-ddc0-4b3b-a9eb-03130a690a86"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 21,
                column: "PermissionControllerActionGuid",
                value: new Guid("c3266463-5a9d-4436-9ea7-2a57df98de2b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 22,
                column: "PermissionControllerActionGuid",
                value: new Guid("82169316-ed9f-46fa-a521-eecca47c17f6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 23,
                column: "PermissionControllerActionGuid",
                value: new Guid("26a64ab9-6fdc-4d83-9571-9bffb2bf878f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 24,
                column: "PermissionControllerActionGuid",
                value: new Guid("e692cce1-371a-4d12-831b-93a8756ca9a5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 25,
                column: "PermissionControllerActionGuid",
                value: new Guid("6de6733b-7148-4e22-8e62-60e3cb8d2ad1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 26,
                column: "PermissionControllerActionGuid",
                value: new Guid("ecfc2465-d394-42aa-bda6-8c3701a0e775"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 27,
                column: "PermissionControllerActionGuid",
                value: new Guid("61bcee3e-2ea8-4b96-abba-5b64d4ff5279"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 28,
                column: "PermissionControllerActionGuid",
                value: new Guid("c05d06d3-d05d-42c8-a29c-4b2c093fcbd7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 29,
                column: "PermissionControllerActionGuid",
                value: new Guid("47a25bc9-0606-4b2f-892f-62244083e309"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 30,
                column: "PermissionControllerActionGuid",
                value: new Guid("c1dea736-7b61-4648-9673-53b3c46188bf"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 31,
                column: "PermissionControllerActionGuid",
                value: new Guid("1f7f41ad-92ca-4aac-94f9-6d4f021a83e4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 32,
                column: "PermissionControllerActionGuid",
                value: new Guid("a36e8f3b-3811-4b08-b040-886be94ca3ad"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 33,
                column: "PermissionControllerActionGuid",
                value: new Guid("cb3c3509-7c95-4854-80ad-5d0a938efaee"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 34,
                column: "PermissionControllerActionGuid",
                value: new Guid("77af81c2-e1bc-4d82-8764-739339162e5c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 35,
                column: "PermissionControllerActionGuid",
                value: new Guid("07a715b1-89df-40bc-bb94-08ebe4aa4e8b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 36,
                column: "PermissionControllerActionGuid",
                value: new Guid("fc6b4878-526f-4af9-9e68-5264c2b82082"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 37,
                column: "PermissionControllerActionGuid",
                value: new Guid("38081c0b-2cd4-47fe-ab10-443cec4537f9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 38,
                column: "PermissionControllerActionGuid",
                value: new Guid("bf20daf6-5771-4658-a229-dda53d091ad5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 39,
                column: "PermissionControllerActionGuid",
                value: new Guid("4bead83d-da61-4f4c-b910-423dd9cc1a52"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 40,
                column: "PermissionControllerActionGuid",
                value: new Guid("53e5cb43-083b-486c-8448-06b656ac6374"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 41,
                column: "PermissionControllerActionGuid",
                value: new Guid("634917e4-ae21-46cb-a0a2-2828f1fd57b8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 42,
                column: "PermissionControllerActionGuid",
                value: new Guid("42530511-39d9-4ea1-ad11-5abd3181a10d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 43,
                column: "PermissionControllerActionGuid",
                value: new Guid("848b1b8c-b901-43fc-a6eb-63e071b70bdc"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 44,
                column: "PermissionControllerActionGuid",
                value: new Guid("ed66b463-64d9-48ed-9c66-ef21f4afaaf5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 45,
                column: "PermissionControllerActionGuid",
                value: new Guid("b2017bbc-caf0-4455-9b9d-d47ef2e029ce"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 46,
                column: "PermissionControllerActionGuid",
                value: new Guid("2dcb5556-e80a-494d-8f00-8a5cbd28a36b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 47,
                column: "PermissionControllerActionGuid",
                value: new Guid("b9194dc9-7ed5-4210-8e79-5fc66646c8dc"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 48,
                column: "PermissionControllerActionGuid",
                value: new Guid("e54fe868-b196-4a47-a938-d3b025153f36"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 49,
                column: "PermissionControllerActionGuid",
                value: new Guid("9ea164b3-d218-4df0-b6a8-b6c7dc0edcc7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 50,
                column: "PermissionControllerActionGuid",
                value: new Guid("86cf29cc-0a23-4a3f-884d-6f4bf6f67de3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 51,
                column: "PermissionControllerActionGuid",
                value: new Guid("0c64d5e0-edd8-4b2e-80f8-17b14a2e5d28"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 52,
                column: "PermissionControllerActionGuid",
                value: new Guid("1394345f-234b-40be-9033-a79f64854e63"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 53,
                column: "PermissionControllerActionGuid",
                value: new Guid("7b80eb5d-f8d1-4a5d-8b05-4ac6d5e30f28"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 54,
                column: "PermissionControllerActionGuid",
                value: new Guid("9abd2d8a-f336-495d-8e62-cd93203bea7e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 55,
                column: "PermissionControllerActionGuid",
                value: new Guid("cffd2926-6c2d-43cf-b0ee-2f11aa8f96e5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 56,
                column: "PermissionControllerActionGuid",
                value: new Guid("f2fe9b94-1fbd-42e1-98bd-cf9e5fdec758"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 57,
                column: "PermissionControllerActionGuid",
                value: new Guid("096f4399-21a9-411d-b4ff-927832a363ac"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 58,
                column: "PermissionControllerActionGuid",
                value: new Guid("29a4cce3-e108-4065-8edd-8e1431f0f4ca"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 59,
                column: "PermissionControllerActionGuid",
                value: new Guid("1fb74ba1-b078-4a2a-ab26-37762f4dbe24"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 60,
                column: "PermissionControllerActionGuid",
                value: new Guid("00a581dc-bbec-430a-b1f3-3ac0d53dab71"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 61,
                column: "PermissionControllerActionGuid",
                value: new Guid("877545f2-d861-48e8-93c8-2c07e0245dbd"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 62,
                column: "PermissionControllerActionGuid",
                value: new Guid("e2896144-04b8-40eb-aaed-491455de3967"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 63,
                column: "PermissionControllerActionGuid",
                value: new Guid("e43be98b-a48c-421a-ad5d-1dd91ab909ed"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 64,
                column: "PermissionControllerActionGuid",
                value: new Guid("d3bb9ecc-2d54-4632-a745-388cb0f3b26d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 65,
                column: "PermissionControllerActionGuid",
                value: new Guid("2a766004-18b5-45f9-9e6b-569d696ebbe0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 66,
                column: "PermissionControllerActionGuid",
                value: new Guid("cc0a18ce-8bb0-4a6d-bd6d-2727c98cc06c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 67,
                column: "PermissionControllerActionGuid",
                value: new Guid("d1e23499-8046-46d7-a4dd-6c65d48ae7d5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 68,
                column: "PermissionControllerActionGuid",
                value: new Guid("98de8285-06ca-4c61-b536-7b721d5672eb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 69,
                column: "PermissionControllerActionGuid",
                value: new Guid("791ee0d0-5ec0-4717-a84e-e7b831ef77cb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 70,
                column: "PermissionControllerActionGuid",
                value: new Guid("1ef035a3-fcec-4773-bf67-d19a3882c227"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 71,
                column: "PermissionControllerActionGuid",
                value: new Guid("4189018b-7a11-4818-a183-53da3b16faa2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 72,
                column: "PermissionControllerActionGuid",
                value: new Guid("28b1cc27-9fd7-45af-b6c1-4c9c0ce1af85"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 73,
                column: "PermissionControllerActionGuid",
                value: new Guid("9e6beea2-6870-4d08-a51d-b76b03cfe931"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 74,
                column: "PermissionControllerActionGuid",
                value: new Guid("410fab29-eb68-45cb-88e9-09bc96e4b00b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 75,
                column: "PermissionControllerActionGuid",
                value: new Guid("03d2847b-ebf8-4bf4-8dae-164c352722a3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 76,
                column: "PermissionControllerActionGuid",
                value: new Guid("5708b6d4-f77c-4ae8-9828-d36451604816"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 77,
                column: "PermissionControllerActionGuid",
                value: new Guid("54ddf2f2-4e2c-43c3-90c5-f54154ae9011"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 78,
                column: "PermissionControllerActionGuid",
                value: new Guid("477061ba-caea-4209-8f6f-89bb4e01bc0f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 79,
                column: "PermissionControllerActionGuid",
                value: new Guid("b2efc57b-0ec4-42e4-8a28-3fc4e7a2a480"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 80,
                column: "PermissionControllerActionGuid",
                value: new Guid("320c579b-9bf1-4341-acd7-fe4c0187ac62"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 81,
                column: "PermissionControllerActionGuid",
                value: new Guid("b65220b6-7819-4e63-8e56-a897c90bb4eb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 82,
                column: "PermissionControllerActionGuid",
                value: new Guid("80e16503-92fa-4701-a21a-133edfb92520"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 83,
                column: "PermissionControllerActionGuid",
                value: new Guid("655f9d52-fa80-4f91-b5f5-82e03feaad0b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 84,
                column: "PermissionControllerActionGuid",
                value: new Guid("5b658ad0-4485-44d5-9217-fa6385cef908"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 85,
                column: "PermissionControllerActionGuid",
                value: new Guid("667ef520-c098-4739-9de0-8f25fb84e375"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 86,
                column: "PermissionControllerActionGuid",
                value: new Guid("f1816bcb-cad9-4994-bba0-7e8d5429ff73"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 87,
                column: "PermissionControllerActionGuid",
                value: new Guid("65baa68f-5e03-47ea-8676-54a2844d7e85"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 88,
                column: "PermissionControllerActionGuid",
                value: new Guid("b6167417-44c4-402b-aa36-07c30fab92ce"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 89,
                column: "PermissionControllerActionGuid",
                value: new Guid("2da0e542-a0de-475d-a438-39ee124b98c0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 90,
                column: "PermissionControllerActionGuid",
                value: new Guid("c9698ef8-622b-487e-920f-5603bdb7a8ef"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 91,
                column: "PermissionControllerActionGuid",
                value: new Guid("1b239570-cc50-4b39-be61-3b99a793c26e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 92,
                column: "PermissionControllerActionGuid",
                value: new Guid("7b9e0b1b-c87a-4b20-82d9-1c1f119168d3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 93,
                column: "PermissionControllerActionGuid",
                value: new Guid("7bc92a6c-0fe9-4bfe-9226-39f46d2a95d6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 94,
                column: "PermissionControllerActionGuid",
                value: new Guid("7a9c8d6c-c0be-4b52-a71b-cf8c8b7e8461"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 95,
                column: "PermissionControllerActionGuid",
                value: new Guid("ccb320cf-4285-4c79-a974-50fb37a48ad5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 96,
                column: "PermissionControllerActionGuid",
                value: new Guid("24d59be6-ea58-4d8b-bc64-8b90600a8002"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 97,
                column: "PermissionControllerActionGuid",
                value: new Guid("e51a2b76-6360-471b-9b8c-a365d30a6721"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 98,
                column: "PermissionControllerActionGuid",
                value: new Guid("36635151-42fa-4663-830d-150be3ca38ca"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 99,
                column: "PermissionControllerActionGuid",
                value: new Guid("9c391ca6-2d13-46ea-a712-08ce15a5a056"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 100,
                column: "PermissionControllerActionGuid",
                value: new Guid("52d5fea7-06e2-4d0e-a96e-0f4e17e8f217"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 101,
                column: "PermissionControllerActionGuid",
                value: new Guid("388353e5-68a3-4856-a389-33e1983df202"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 102,
                column: "PermissionControllerActionGuid",
                value: new Guid("3ecf7380-9e88-432e-93b3-621af111b37f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 103,
                column: "PermissionControllerActionGuid",
                value: new Guid("c9659adb-6d6d-4b27-9c3b-3f7632716e06"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 104,
                column: "PermissionControllerActionGuid",
                value: new Guid("bf989eea-93e5-4bc2-9a3d-c1b90e6dac1b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 105,
                column: "PermissionControllerActionGuid",
                value: new Guid("46052582-069b-4f11-8f60-478825e43afa"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 106,
                column: "PermissionControllerActionGuid",
                value: new Guid("00db69bc-937c-4f16-a005-cbafc3770ccc"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 107,
                column: "PermissionControllerActionGuid",
                value: new Guid("35d344ad-aa5b-4ece-adf0-ba4f550cecca"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 108,
                column: "PermissionControllerActionGuid",
                value: new Guid("d4a97b26-1dfe-461c-984f-39f4454a9a9a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 109,
                column: "PermissionControllerActionGuid",
                value: new Guid("8c5da3a3-ad67-4bfd-85a0-de6ceb0f4359"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 110,
                column: "PermissionControllerActionGuid",
                value: new Guid("bdf06cf4-26a8-4914-b41c-1f9260fa00fb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 111,
                column: "PermissionControllerActionGuid",
                value: new Guid("af6d9745-0a69-429d-8bbd-f35e54e9f639"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 112,
                column: "PermissionControllerActionGuid",
                value: new Guid("b948a1bc-3e2f-40b7-a195-0c1aed45e08c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 113,
                column: "PermissionControllerActionGuid",
                value: new Guid("8248b73d-7515-4764-90f1-7241d1333baa"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 114,
                column: "PermissionControllerActionGuid",
                value: new Guid("a9eaac34-5cd1-4bb6-b2b7-44fe6031a834"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 115,
                column: "PermissionControllerActionGuid",
                value: new Guid("32453a96-3ce3-4ed0-9e56-c7a53ee8c2c5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 116,
                column: "PermissionControllerActionGuid",
                value: new Guid("885baf1b-0675-4174-b5a9-c8a4f91e516a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 117,
                column: "PermissionControllerActionGuid",
                value: new Guid("9d05f1b4-7f2d-4c09-9b0c-cc1efaa9b7d9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 118,
                column: "PermissionControllerActionGuid",
                value: new Guid("e1cc8d5e-d1c8-4af2-a25f-581e60167662"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 119,
                column: "PermissionControllerActionGuid",
                value: new Guid("f0030b1e-00e1-43d9-a587-1f0ca903cf8d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 120,
                column: "PermissionControllerActionGuid",
                value: new Guid("349a94bd-7b50-45ed-b001-c3a3552c99f6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 121,
                column: "PermissionControllerActionGuid",
                value: new Guid("a93d861c-d4c7-4667-8e73-e043c6d7b358"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 122,
                column: "PermissionControllerActionGuid",
                value: new Guid("271398dc-5a1e-4413-aa6f-c8459179129f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 123,
                column: "PermissionControllerActionGuid",
                value: new Guid("2fa0eed9-30cb-4b91-9d84-0cfebe1f391c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 124,
                column: "PermissionControllerActionGuid",
                value: new Guid("e402bc01-5373-420a-a40b-fe0a35d8da76"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 125,
                column: "PermissionControllerActionGuid",
                value: new Guid("308a3e15-4522-475b-8c6a-5a6280645c16"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 126,
                column: "PermissionControllerActionGuid",
                value: new Guid("3b7f83eb-10f1-41d3-be8f-c9908bf0b83c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 127,
                column: "PermissionControllerActionGuid",
                value: new Guid("2d740b37-661d-4bff-b7b0-f3d9f56c96d6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 128,
                column: "PermissionControllerActionGuid",
                value: new Guid("25d4edb8-0990-4cd1-bded-f4b8b19b8a98"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 129,
                column: "PermissionControllerActionGuid",
                value: new Guid("aae7d259-dc3e-457f-9112-7c01803facbd"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 130,
                column: "PermissionControllerActionGuid",
                value: new Guid("ed1ae636-dc33-4cfb-bdf7-29f62eb3423d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 131,
                column: "PermissionControllerActionGuid",
                value: new Guid("8c3f9188-5233-4bfb-8bfb-0e5260a3cde0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 132,
                column: "PermissionControllerActionGuid",
                value: new Guid("52fdebfd-4323-4bdb-96db-174f33d5c1a6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 133,
                column: "PermissionControllerActionGuid",
                value: new Guid("14aa4aa6-8982-45e6-b933-41a644be6e52"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 134,
                column: "PermissionControllerActionGuid",
                value: new Guid("784da28c-3112-45d8-8ed8-5d3dcf864c6c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 135,
                column: "PermissionControllerActionGuid",
                value: new Guid("4b96d7b0-3296-43ef-a9e2-23cb8b06e415"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 136,
                column: "PermissionControllerActionGuid",
                value: new Guid("c2d81dcf-390f-4815-a268-d66f3867d712"));

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "City",
                keyColumn: "CityID",
                keyValue: 1,
                columns: new[] { "CityGuid", "CreateDate", "EnableDate" },
                values: new object[] { new Guid("f5a09945-5c68-4539-890e-1b4238cdb732"), new DateTime(2021, 9, 26, 11, 59, 17, 731, DateTimeKind.Local).AddTicks(9450), new DateTime(2021, 9, 26, 11, 59, 17, 732, DateTimeKind.Local).AddTicks(384) });

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "Country",
                keyColumn: "CountryID",
                keyValue: 1,
                columns: new[] { "CountryGuid", "CreateDate", "EnableDate" },
                values: new object[] { new Guid("0af17eff-da39-4144-b0a4-b9033c658f77"), new DateTime(2021, 9, 26, 11, 59, 17, 728, DateTimeKind.Local).AddTicks(4923), new DateTime(2021, 9, 26, 11, 59, 17, 729, DateTimeKind.Local).AddTicks(8824) });

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "Departments",
                keyColumn: "DepartmentsID",
                keyValue: 1,
                columns: new[] { "CreateDate", "DepartmentsGuid", "EnableDate" },
                values: new object[] { new DateTime(2021, 9, 26, 11, 59, 17, 734, DateTimeKind.Local).AddTicks(8024), new Guid("c032e3e7-590b-42f3-b5c7-6c576e9d2866"), new DateTime(2021, 9, 26, 11, 59, 17, 734, DateTimeKind.Local).AddTicks(9019) });

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "Jobs",
                keyColumn: "JobsID",
                keyValue: 1,
                columns: new[] { "CreateDate", "EnableDate", "JobsGuid" },
                values: new object[] { new DateTime(2021, 9, 26, 11, 59, 17, 733, DateTimeKind.Local).AddTicks(5242), new DateTime(2021, 9, 26, 11, 59, 17, 733, DateTimeKind.Local).AddTicks(6186), new Guid("bfd6a197-30b6-4935-9602-88542dc306dc") });

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "MainCategory",
                keyColumn: "MainCategoryID",
                keyValue: 1,
                columns: new[] { "CreateDate", "EnableDate", "MainCategoryGuid" },
                values: new object[] { new DateTime(2021, 9, 26, 11, 59, 17, 734, DateTimeKind.Local).AddTicks(800), new DateTime(2021, 9, 26, 11, 59, 17, 734, DateTimeKind.Local).AddTicks(1729), new Guid("01f35db9-e87e-48ff-a7c1-a921f3bbbc16") });

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "Nationality",
                keyColumn: "NationalityID",
                keyValue: 1,
                columns: new[] { "CreateDate", "EnableDate", "NationalityGuid" },
                values: new object[] { new DateTime(2021, 9, 26, 11, 59, 17, 732, DateTimeKind.Local).AddTicks(8942), new DateTime(2021, 9, 26, 11, 59, 17, 733, DateTimeKind.Local).AddTicks(128), new Guid("eb0fa1af-6f4a-4704-913f-c805508b7ed5") });

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "Region",
                keyColumn: "RegionID",
                keyValue: 1,
                columns: new[] { "CreateDate", "EnableDate", "RegionGuid" },
                values: new object[] { new DateTime(2021, 9, 26, 11, 59, 17, 730, DateTimeKind.Local).AddTicks(7542), new DateTime(2021, 9, 26, 11, 59, 17, 730, DateTimeKind.Local).AddTicks(8496), new Guid("3e181776-171b-4bdf-9301-23f84d84b8c5") });
        }
    }
}
