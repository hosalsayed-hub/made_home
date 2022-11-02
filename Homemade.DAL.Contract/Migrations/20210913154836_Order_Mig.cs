using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Homemade.DAL.Contract.Migrations
{
    public partial class Order_Mig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DeleteData(
            //    schema: "Permission",
            //    table: "PermissionControllerAction",
            //    keyColumn: "PermissionControllerActionID",
            //    keyValue: 500);

            //migrationBuilder.DeleteData(
            //    schema: "Permission",
            //    table: "PermissionControllerAction",
            //    keyColumn: "PermissionControllerActionID",
            //    keyValue: 501);

            //migrationBuilder.DeleteData(
            //    schema: "Permission",
            //    table: "PermissionControllerAction",
            //    keyColumn: "PermissionControllerActionID",
            //    keyValue: 502);

            //migrationBuilder.DeleteData(
            //    schema: "Permission",
            //    table: "PermissionControllerAction",
            //    keyColumn: "PermissionControllerActionID",
            //    keyValue: 503);

            //migrationBuilder.DeleteData(
            //    schema: "Permission",
            //    table: "PermissionControllerAction",
            //    keyColumn: "PermissionControllerActionID",
            //    keyValue: 504);

            //migrationBuilder.DeleteData(
            //    schema: "Permission",
            //    table: "PermissionControllerAction",
            //    keyColumn: "PermissionControllerActionID",
            //    keyValue: 505);

            //migrationBuilder.DeleteData(
            //    schema: "Permission",
            //    table: "PermissionControllerAction",
            //    keyColumn: "PermissionControllerActionID",
            //    keyValue: 506);

            //migrationBuilder.DeleteData(
            //    schema: "Permission",
            //    table: "PermissionControllerAction",
            //    keyColumn: "PermissionControllerActionID",
            //    keyValue: 507);

            //migrationBuilder.DeleteData(
            //    schema: "Permission",
            //    table: "PermissionControllerAction",
            //    keyColumn: "PermissionControllerActionID",
            //    keyValue: 508);

            //migrationBuilder.DeleteData(
            //    schema: "Permission",
            //    table: "PermissionControllerAction",
            //    keyColumn: "PermissionControllerActionID",
            //    keyValue: 509);

            //migrationBuilder.DeleteData(
            //    schema: "Permission",
            //    table: "PermissionControllerAction",
            //    keyColumn: "PermissionControllerActionID",
            //    keyValue: 510);

            //migrationBuilder.DeleteData(
            //    schema: "Permission",
            //    table: "PermissionControllerAction",
            //    keyColumn: "PermissionControllerActionID",
            //    keyValue: 511);

            //migrationBuilder.DeleteData(
            //    schema: "Permission",
            //    table: "PermissionControllerAction",
            //    keyColumn: "PermissionControllerActionID",
            //    keyValue: 512);

            //migrationBuilder.DeleteData(
            //    schema: "Permission",
            //    table: "PermissionControllerAction",
            //    keyColumn: "PermissionControllerActionID",
            //    keyValue: 513);

            //migrationBuilder.DeleteData(
            //    schema: "Permission",
            //    table: "PermissionControllerAction",
            //    keyColumn: "PermissionControllerActionID",
            //    keyValue: 514);

            //migrationBuilder.DeleteData(
            //    schema: "Permission",
            //    table: "PermissionControllerAction",
            //    keyColumn: "PermissionControllerActionID",
            //    keyValue: 516);

            //migrationBuilder.DeleteData(
            //    schema: "Permission",
            //    table: "PermissionControllerAction",
            //    keyColumn: "PermissionControllerActionID",
            //    keyValue: 517);

            //migrationBuilder.DeleteData(
            //    schema: "Permission",
            //    table: "PermissionControllerAction",
            //    keyColumn: "PermissionControllerActionID",
            //    keyValue: 518);

            //migrationBuilder.DeleteData(
            //    schema: "Permission",
            //    table: "PermissionControllerAction",
            //    keyColumn: "PermissionControllerActionID",
            //    keyValue: 519);

            //migrationBuilder.DeleteData(
            //    schema: "Permission",
            //    table: "PermissionControllerAction",
            //    keyColumn: "PermissionControllerActionID",
            //    keyValue: 520);

            //migrationBuilder.DeleteData(
            //    schema: "Permission",
            //    table: "PermissionControllerAction",
            //    keyColumn: "PermissionControllerActionID",
            //    keyValue: 521);

            //migrationBuilder.DeleteData(
            //    schema: "Permission",
            //    table: "PermissionControllerAction",
            //    keyColumn: "PermissionControllerActionID",
            //    keyValue: 522);

            //migrationBuilder.DeleteData(
            //    schema: "Permission",
            //    table: "PermissionControllerAction",
            //    keyColumn: "PermissionControllerActionID",
            //    keyValue: 523);

            //migrationBuilder.DeleteData(
            //    schema: "Permission",
            //    table: "PermissionControllerAction",
            //    keyColumn: "PermissionControllerActionID",
            //    keyValue: 524);

            //migrationBuilder.DeleteData(
            //    schema: "Permission",
            //    table: "PermissionControllerAction",
            //    keyColumn: "PermissionControllerActionID",
            //    keyValue: 525);

            //migrationBuilder.DeleteData(
            //    schema: "Permission",
            //    table: "PermissionControllerAction",
            //    keyColumn: "PermissionControllerActionID",
            //    keyValue: 526);

            //migrationBuilder.DeleteData(
            //    schema: "Permission",
            //    table: "PermissionControllerAction",
            //    keyColumn: "PermissionControllerActionID",
            //    keyValue: 527);

            //migrationBuilder.DeleteData(
            //    schema: "Permission",
            //    table: "PermissionControllerAction",
            //    keyColumn: "PermissionControllerActionID",
            //    keyValue: 615);

            //migrationBuilder.DeleteData(
            //    schema: "Permission",
            //    table: "PermissionController",
            //    keyColumn: "PermissionControllerID",
            //    keyValue: 106);

            migrationBuilder.EnsureSchema(
                name: "Order");

            migrationBuilder.AddColumn<int>(
                name: "AddressTypesID",
                schema: "Customer",
                table: "CustomerLocation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsVerfiy",
                schema: "Customer",
                table: "CustomerLocation",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "MobileNo",
                schema: "Customer",
                table: "CustomerLocation",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "Customer",
                table: "CustomerLocation",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AddressTypes",
                schema: "Setting",
                columns: table => new
                {
                    AddressTypesID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressTypesGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameAR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameEN = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_AddressTypes", x => x.AddressTypesID);
                    table.ForeignKey(
                        name: "FK_AddressTypes_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PromoCode",
                schema: "Order",
                columns: table => new
                {
                    PromoCodeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PromoCodeGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LimitCount = table.Column<double>(type: "float", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiscountValue = table.Column<double>(type: "float", nullable: false),
                    DiscountType = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_PromoCode", x => x.PromoCodeID);
                    table.ForeignKey(
                        name: "FK_PromoCode_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                schema: "Order",
                columns: table => new
                {
                    OrdersID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrdersGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Vat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DeliveryPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentTypeId = table.Column<int>(type: "int", nullable: false),
                    OrderStatusID = table.Column<int>(type: "int", nullable: false),
                    CustomersID = table.Column<int>(type: "int", nullable: false),
                    CustomerLocationID = table.Column<int>(type: "int", nullable: false),
                    PromoCodeID = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_Orders", x => x.OrdersID);
                    table.ForeignKey(
                        name: "FK_Orders_CustomerLocation_CustomerLocationID",
                        column: x => x.CustomerLocationID,
                        principalSchema: "Customer",
                        principalTable: "CustomerLocation",
                        principalColumn: "CustomerLocationID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomersID",
                        column: x => x.CustomersID,
                        principalSchema: "Customer",
                        principalTable: "Customers",
                        principalColumn: "CustomersID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_OrderStatus_OrderStatusID",
                        column: x => x.OrderStatusID,
                        principalSchema: "Setting",
                        principalTable: "OrderStatus",
                        principalColumn: "OrderStatusID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_PromoCode_PromoCodeID",
                        column: x => x.PromoCodeID,
                        principalSchema: "Order",
                        principalTable: "PromoCode",
                        principalColumn: "PromoCodeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderPromo",
                schema: "Order",
                columns: table => new
                {
                    OrderPromoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderPromoGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PromoCodeID = table.Column<int>(type: "int", nullable: false),
                    OrdersID = table.Column<int>(type: "int", nullable: false),
                    DiscountValue = table.Column<double>(type: "float", nullable: false),
                    DiscountType = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_OrderPromo", x => x.OrderPromoID);
                    table.ForeignKey(
                        name: "FK_OrderPromo_Orders_OrdersID",
                        column: x => x.OrdersID,
                        principalSchema: "Order",
                        principalTable: "Orders",
                        principalColumn: "OrdersID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderPromo_PromoCode_PromoCodeID",
                        column: x => x.PromoCodeID,
                        principalSchema: "Order",
                        principalTable: "PromoCode",
                        principalColumn: "PromoCodeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderPromo_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderVendor",
                schema: "Order",
                columns: table => new
                {
                    OrderVendorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderVendorGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Vat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DeliveryPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TrackNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrdersID = table.Column<int>(type: "int", nullable: false),
                    VendorsID = table.Column<int>(type: "int", nullable: false),
                    OrderStatusID = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_OrderVendor", x => x.OrderVendorID);
                    table.ForeignKey(
                        name: "FK_OrderVendor_Orders_OrdersID",
                        column: x => x.OrdersID,
                        principalSchema: "Order",
                        principalTable: "Orders",
                        principalColumn: "OrdersID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderVendor_OrderStatus_OrderStatusID",
                        column: x => x.OrderStatusID,
                        principalSchema: "Setting",
                        principalTable: "OrderStatus",
                        principalColumn: "OrderStatusID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderVendor_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderVendor_Vendors_VendorsID",
                        column: x => x.VendorsID,
                        principalSchema: "Vendor",
                        principalTable: "Vendors",
                        principalColumn: "VendorsID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderHistory",
                schema: "Order",
                columns: table => new
                {
                    OrderHistoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderHistoryGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Lat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Lng = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderVendorID = table.Column<int>(type: "int", nullable: false),
                    OrderStatusID = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_OrderHistory", x => x.OrderHistoryID);
                    table.ForeignKey(
                        name: "FK_OrderHistory_OrderStatus_OrderStatusID",
                        column: x => x.OrderStatusID,
                        principalSchema: "Setting",
                        principalTable: "OrderStatus",
                        principalColumn: "OrderStatusID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderHistory_OrderVendor_OrderVendorID",
                        column: x => x.OrderVendorID,
                        principalSchema: "Order",
                        principalTable: "OrderVendor",
                        principalColumn: "OrderVendorID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderHistory_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                schema: "Order",
                columns: table => new
                {
                    OrderItemsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderItemsGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProdName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quesntity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProdImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderVendorID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_OrderItems", x => x.OrderItemsID);
                    table.ForeignKey(
                        name: "FK_OrderItems_OrderVendor_OrderVendorID",
                        column: x => x.OrderVendorID,
                        principalSchema: "Order",
                        principalTable: "OrderVendor",
                        principalColumn: "OrderVendorID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderItems_Product_ProductID",
                        column: x => x.ProductID,
                        principalSchema: "Vendor",
                        principalTable: "Product",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderItems_User_UserId",
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
            //        { 1, "7ab52200-8b73-45a9-8900-1374f617a01f", "Admin", null, 1 },
            //        { 2, "9fff7996-d06a-4a28-9e70-58eec879d99e", "Operation", null, 1 },
            //        { 3, "88bd7fee-e9b3-4a19-873f-31b357bd38de", "Medical Managment", null, 2 },
            //        { 4, "136776b4-6ead-4ab2-a7e4-eaf1104f1eb4", "Doctors", null, 2 },
            //        { 5, "ceb40f61-8fbf-485f-9cdc-eca0fd0730c6", "Nurses", null, 2 },
            //        { 6, "da2a4d96-c8de-4c22-9e1b-32a3c9ac0a38", "Employees", null, 2 }
            //    });

            //migrationBuilder.InsertData(
            //    table: "User",
            //    columns: new[] { "UserId", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserJWTToken", "UserName", "UserType" },
            //    values: new object[] { 1, 0, "5d0583c7-651c-4e9b-b6fe-80463fd2eb01", "SystemUser@Admin.com", true, false, null, "SystemUser@Admin.com", "SYSTEMUSER", "AQAAAAEAACcQAAAAEP65QXLX6e94ehLc9ntv07Q7n/aO6wf8y6j/z15XE7hfgyZLCNvHmM3Ar6SaTwzC3g==", "012", true, "deabc33c-ec2f-4c19-8036-7aded5b12a63", false, null, "SystemUser", 1 });

            //migrationBuilder.InsertData(
            //    schema: "Permission",
            //    table: "PermissionAction",
            //    columns: new[] { "PermissionActionID", "PermissionActionGuid", "PermissionActionNameAr", "PermissionActionNameEn" },
            //    values: new object[,]
            //    {
            //        { 4, new Guid("5b0162bc-06dc-458c-bd6d-212abb303cab"), "حذف", "Delete" },
            //        { 3, new Guid("b58d7eee-d26e-4d31-b510-5dee2ad9a7f8"), "تعديل", "Update" },
            //        { 2, new Guid("391d9101-0b11-460d-b11b-93a8f9be28b5"), "اضافة", "Insert" },
            //        { 1, new Guid("a4571fc9-dfe6-461e-be21-e8649e9aa9f8"), "عرض", "View" }
            //    });

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 1,
                column: "PermissionControllerGuid",
                value: new Guid("dc324639-864d-4878-a158-b1397c7329b4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 2,
                column: "PermissionControllerGuid",
                value: new Guid("fd1b0759-bb7a-4bdf-9e36-e6f19803f669"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 3,
                column: "PermissionControllerGuid",
                value: new Guid("8c34d4af-e196-4756-a614-a884c3c3e58e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 4,
                column: "PermissionControllerGuid",
                value: new Guid("700b8a1d-7bfd-474a-9007-ffc4092dba9b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 5,
                column: "PermissionControllerGuid",
                value: new Guid("4c5ad34e-f4bc-4c12-843c-9f5dd8c84ef6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 6,
                column: "PermissionControllerGuid",
                value: new Guid("f8a0c3e5-6968-4400-9ff0-0a009fd25728"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 7,
                column: "PermissionControllerGuid",
                value: new Guid("41bb18df-6883-436f-aedb-aea51d098804"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 8,
                column: "PermissionControllerGuid",
                value: new Guid("240eca2c-7203-4127-a670-a3a9ff65f391"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 9,
                column: "PermissionControllerGuid",
                value: new Guid("9797387b-86b7-4bb7-8205-5624aaf0b3f4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 10,
                column: "PermissionControllerGuid",
                value: new Guid("226bc125-4caa-4254-bfe0-6c502832997b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 11,
                column: "PermissionControllerGuid",
                value: new Guid("c0f84f15-7e11-425a-a332-eda2b12fb68d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 12,
                column: "PermissionControllerGuid",
                value: new Guid("6d1dc758-59c6-4ef0-97e7-43a01c5e5920"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 13,
                column: "PermissionControllerGuid",
                value: new Guid("eba53b5f-b496-47c4-acad-bd0208f6c5fa"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 14,
                column: "PermissionControllerGuid",
                value: new Guid("b0e00fc6-773e-4bdf-975d-074e8c828b7d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 15,
                column: "PermissionControllerGuid",
                value: new Guid("d4acf4b3-f3d3-4847-a163-29afe63c927a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 16,
                column: "PermissionControllerGuid",
                value: new Guid("ded5b52f-d888-462b-b2d9-87df74da57d2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 17,
                column: "PermissionControllerGuid",
                value: new Guid("56fd277d-bde6-45dd-81af-8ef97239f965"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 18,
                column: "PermissionControllerGuid",
                value: new Guid("b459e91c-8c59-49e5-8b9b-5487e6dd7aa5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 19,
                column: "PermissionControllerGuid",
                value: new Guid("911bebf0-32b9-4265-80de-cc7078b4ecda"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 20,
                column: "PermissionControllerGuid",
                value: new Guid("3b7528da-1a11-4e4d-8225-b7a15db9d7b9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 21,
                column: "PermissionControllerGuid",
                value: new Guid("8faafe72-9cb2-4aea-a5c3-a30df1645dc1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 22,
                column: "PermissionControllerGuid",
                value: new Guid("4710a0eb-578a-4c0a-93e0-d6f39cdace75"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 23,
                column: "PermissionControllerGuid",
                value: new Guid("bd0ceac7-c6f2-45e8-928b-d963f2aef09d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 24,
                column: "PermissionControllerGuid",
                value: new Guid("6cac643d-d51b-41df-b305-93a27828d755"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 25,
                column: "PermissionControllerGuid",
                value: new Guid("d939b12f-857b-4379-8218-d451dab16a41"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 26,
                column: "PermissionControllerGuid",
                value: new Guid("7242c185-c4d0-4be4-adbe-eff88cfcbcfa"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 27,
                column: "PermissionControllerGuid",
                value: new Guid("d4763659-dc62-4e9d-8b49-629d5ecedc9d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 1,
                column: "PermissionControllerActionGuid",
                value: new Guid("fdbcb3ef-6094-4860-9582-0797772841ca"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 2,
                column: "PermissionControllerActionGuid",
                value: new Guid("bc5b0db2-b405-4016-b259-ffb34c28a287"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 3,
                column: "PermissionControllerActionGuid",
                value: new Guid("f078be23-b4b4-469d-adfc-25700e51cf96"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 4,
                column: "PermissionControllerActionGuid",
                value: new Guid("fc64ebcb-1553-4b2c-9304-25208393ffa6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 5,
                column: "PermissionControllerActionGuid",
                value: new Guid("972a7866-6963-40b0-8618-14d587310e63"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 6,
                column: "PermissionControllerActionGuid",
                value: new Guid("a3742a62-6525-4409-98b8-b6e383a03c35"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 7,
                column: "PermissionControllerActionGuid",
                value: new Guid("4542e968-1dac-4e70-acb3-3490009a9981"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 8,
                column: "PermissionControllerActionGuid",
                value: new Guid("a46ca90b-39dd-4481-a876-87cedb01045b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 9,
                column: "PermissionControllerActionGuid",
                value: new Guid("580f39ee-812b-493b-9e15-30ece2bbb568"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 10,
                column: "PermissionControllerActionGuid",
                value: new Guid("6f7eb924-365a-4ef0-9c79-c187ca175e53"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 11,
                column: "PermissionControllerActionGuid",
                value: new Guid("3b9c5f19-a58a-425c-8fbf-82959c5eb8cb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 12,
                column: "PermissionControllerActionGuid",
                value: new Guid("b927ff17-6831-408e-a3a2-f364627aa8bf"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 13,
                column: "PermissionControllerActionGuid",
                value: new Guid("b90372d2-f212-473b-8773-d923c4c11c4c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 14,
                column: "PermissionControllerActionGuid",
                value: new Guid("dffd5abc-d7bf-4313-b2fa-b895048ed52d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 15,
                column: "PermissionControllerActionGuid",
                value: new Guid("adc3d910-016c-4466-b9c3-12198a99845b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 16,
                column: "PermissionControllerActionGuid",
                value: new Guid("db235e73-e62d-4a6b-80aa-c4a2adb635ce"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 17,
                column: "PermissionControllerActionGuid",
                value: new Guid("1ede1d30-7a39-467f-b524-e709684e4e76"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 18,
                column: "PermissionControllerActionGuid",
                value: new Guid("90a1cd4e-5d84-4ba1-b279-34e4425a350f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 19,
                column: "PermissionControllerActionGuid",
                value: new Guid("d7bac7f7-d7fc-4389-9929-d0b7306c9327"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 20,
                column: "PermissionControllerActionGuid",
                value: new Guid("2ac108bd-1aa2-41a1-9a54-b086fd14ab81"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 21,
                column: "PermissionControllerActionGuid",
                value: new Guid("b672cf5b-09c6-4deb-8af0-b1c1907b7897"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 22,
                column: "PermissionControllerActionGuid",
                value: new Guid("bb7146d1-6f70-4859-bc3a-b3a608592bbe"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 23,
                column: "PermissionControllerActionGuid",
                value: new Guid("ce8daa97-ef1f-4939-b32e-f25bf7f5f0a1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 24,
                column: "PermissionControllerActionGuid",
                value: new Guid("2289d851-0328-40d0-ad3f-43642b21b989"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 25,
                column: "PermissionControllerActionGuid",
                value: new Guid("56ea2954-8116-4ee5-a333-5bed5aafdf00"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 26,
                column: "PermissionControllerActionGuid",
                value: new Guid("ca828d62-389c-41b9-8801-8dd1b9b1aa7e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 27,
                column: "PermissionControllerActionGuid",
                value: new Guid("5eae1a79-b20f-49f5-a1fe-6fe1f2d1bae2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 28,
                column: "PermissionControllerActionGuid",
                value: new Guid("e3334204-26e7-4c72-b902-8e06368674d3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 29,
                column: "PermissionControllerActionGuid",
                value: new Guid("eef83c45-9b84-481b-a16e-a1ee3e57eb91"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 30,
                column: "PermissionControllerActionGuid",
                value: new Guid("f2f466b6-7ac9-445a-b02a-9c351485cf9a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 31,
                column: "PermissionControllerActionGuid",
                value: new Guid("2530aca8-1818-4fab-b529-48b96c1fd428"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 32,
                column: "PermissionControllerActionGuid",
                value: new Guid("60ac9c38-874c-4f16-b13d-d4aaf79de476"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 33,
                column: "PermissionControllerActionGuid",
                value: new Guid("1efbf37b-6b45-4a2a-98b3-8253477d71c5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 34,
                column: "PermissionControllerActionGuid",
                value: new Guid("a86c7f84-d535-4c31-be71-6f5214d04769"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 35,
                column: "PermissionControllerActionGuid",
                value: new Guid("7bcc67cb-ef8f-40f8-a02c-768be19f85b3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 36,
                column: "PermissionControllerActionGuid",
                value: new Guid("ca7c4848-776c-44e7-9446-412d7f43916e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 37,
                column: "PermissionControllerActionGuid",
                value: new Guid("351eecc7-1596-4eac-ac00-6d9443059d09"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 38,
                column: "PermissionControllerActionGuid",
                value: new Guid("6b7c342c-c536-4ea1-bcbd-561f804c6891"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 39,
                column: "PermissionControllerActionGuid",
                value: new Guid("11880f74-0d15-48ae-a524-e147960583df"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 40,
                column: "PermissionControllerActionGuid",
                value: new Guid("3fcc45d6-add5-419b-b93b-67a46aadf42d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 41,
                column: "PermissionControllerActionGuid",
                value: new Guid("06547bd5-24c0-445c-a63b-9568d3430ffe"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 42,
                column: "PermissionControllerActionGuid",
                value: new Guid("413c063a-a819-4cfd-9f6b-ae7cec07148e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 43,
                column: "PermissionControllerActionGuid",
                value: new Guid("70723734-6efe-4d4c-944e-31e35b32a770"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 44,
                column: "PermissionControllerActionGuid",
                value: new Guid("93efc9f2-9895-43ae-b972-edc53bd9350d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 45,
                column: "PermissionControllerActionGuid",
                value: new Guid("332be544-0436-496f-a946-ea05ee06134a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 46,
                column: "PermissionControllerActionGuid",
                value: new Guid("2a649d3a-62f7-4576-aa7f-323f520ce87d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 47,
                column: "PermissionControllerActionGuid",
                value: new Guid("e5fffc87-ad64-42bc-9747-7bebc29520cc"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 48,
                column: "PermissionControllerActionGuid",
                value: new Guid("209d1bb4-3d11-475e-a840-83fe7a9734be"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 49,
                column: "PermissionControllerActionGuid",
                value: new Guid("43ead62b-ad6a-4778-900f-c5f352035f97"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 50,
                column: "PermissionControllerActionGuid",
                value: new Guid("50f114e4-1f4e-4852-b65a-ebc4576f8417"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 51,
                column: "PermissionControllerActionGuid",
                value: new Guid("154530e3-afcb-4530-aa50-d166a0d33e8d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 52,
                column: "PermissionControllerActionGuid",
                value: new Guid("fb30c635-a67c-4ab8-8e46-bde6c3bd1b1b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 53,
                column: "PermissionControllerActionGuid",
                value: new Guid("c4df5c6f-c25f-4888-a19a-62c8cb26800d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 54,
                column: "PermissionControllerActionGuid",
                value: new Guid("7686ce4d-e9f3-402a-9968-40f74fb5ff17"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 55,
                column: "PermissionControllerActionGuid",
                value: new Guid("a43c0f4f-4a5f-44f7-a623-c3df0dd073d9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 56,
                column: "PermissionControllerActionGuid",
                value: new Guid("6c29324e-5d00-428e-8385-80a60d79f12b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 57,
                column: "PermissionControllerActionGuid",
                value: new Guid("e041e883-9a06-480d-8581-fe3b3a010589"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 58,
                column: "PermissionControllerActionGuid",
                value: new Guid("f5873302-a078-489d-9a6f-ad258bf8ece9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 59,
                column: "PermissionControllerActionGuid",
                value: new Guid("16f7e7ec-b1ff-480c-b447-75c2702021f4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 60,
                column: "PermissionControllerActionGuid",
                value: new Guid("bf8554cb-a1dc-4a88-b11f-2808960d6539"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 61,
                column: "PermissionControllerActionGuid",
                value: new Guid("5ddcd28c-09af-4ea8-921c-836a2c13e3d8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 62,
                column: "PermissionControllerActionGuid",
                value: new Guid("9fa8b73a-6dc9-4d73-ade0-2b8129cfe520"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 63,
                column: "PermissionControllerActionGuid",
                value: new Guid("146cb614-003c-4538-aa30-dee5ca3394b6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 64,
                column: "PermissionControllerActionGuid",
                value: new Guid("5f2ce813-3a15-4975-9c8a-53326bda82b0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 65,
                column: "PermissionControllerActionGuid",
                value: new Guid("343e8872-6aba-4906-89c4-dc2b753b2f7b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 66,
                column: "PermissionControllerActionGuid",
                value: new Guid("6da95213-cc10-4067-9fc5-92ff624e5626"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 67,
                column: "PermissionControllerActionGuid",
                value: new Guid("47305d3d-bb58-4932-83f9-43125b38bdbb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 68,
                column: "PermissionControllerActionGuid",
                value: new Guid("18e4c69b-3b45-4cec-a6aa-9f5bc2a9f258"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 69,
                column: "PermissionControllerActionGuid",
                value: new Guid("3b51f961-978f-41e8-a1d8-beed4db45510"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 70,
                column: "PermissionControllerActionGuid",
                value: new Guid("c93ec56b-499b-475d-8a96-1ea54bd6ee40"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 71,
                column: "PermissionControllerActionGuid",
                value: new Guid("e000cd46-dc57-41f2-9382-b58158c3bf82"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 72,
                column: "PermissionControllerActionGuid",
                value: new Guid("5e8cc9a9-3a1b-41df-b353-0f92e98a05f6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 73,
                column: "PermissionControllerActionGuid",
                value: new Guid("734995ce-dd40-438e-9fb6-2827576dae1f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 74,
                column: "PermissionControllerActionGuid",
                value: new Guid("af7290f7-e6f0-40f3-a7d5-b719683765dc"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 75,
                column: "PermissionControllerActionGuid",
                value: new Guid("fbfdb410-b6f2-445c-8506-d9083cd2e68f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 76,
                column: "PermissionControllerActionGuid",
                value: new Guid("9132b345-2edd-49e1-9528-e2e79bf7a184"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 77,
                column: "PermissionControllerActionGuid",
                value: new Guid("724c9aa4-6809-48c0-b9ef-5b61740695d0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 78,
                column: "PermissionControllerActionGuid",
                value: new Guid("1251d0da-083b-478a-b633-e9efb03dcc4f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 79,
                column: "PermissionControllerActionGuid",
                value: new Guid("33730dc6-a651-4d3e-b576-044482306259"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 80,
                column: "PermissionControllerActionGuid",
                value: new Guid("93b23d21-78d9-45b7-bdfa-f6f4c86e7bb2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 81,
                column: "PermissionControllerActionGuid",
                value: new Guid("7fa23635-069d-4d9c-80ed-45a945c84756"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 82,
                column: "PermissionControllerActionGuid",
                value: new Guid("b9345dda-86fa-4f6a-b458-74c76bf70332"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 83,
                column: "PermissionControllerActionGuid",
                value: new Guid("561bce47-2142-4527-a478-9d65d17d51e8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 84,
                column: "PermissionControllerActionGuid",
                value: new Guid("c149f3f8-4f10-4a89-91eb-b149905d2e43"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 85,
                column: "PermissionControllerActionGuid",
                value: new Guid("2d6ec7e7-f1af-4875-8833-f6a7ecb0cf15"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 86,
                column: "PermissionControllerActionGuid",
                value: new Guid("3019366c-e136-4273-b80f-cbeb63847b56"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 87,
                column: "PermissionControllerActionGuid",
                value: new Guid("63b225c2-8959-46af-a3dd-c95b8630d5f0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 88,
                column: "PermissionControllerActionGuid",
                value: new Guid("f39ecd23-1197-4cf5-b525-95e8fe620cda"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 89,
                column: "PermissionControllerActionGuid",
                value: new Guid("7a19606c-33da-4ce4-bfdf-53ff229bcb98"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 90,
                column: "PermissionControllerActionGuid",
                value: new Guid("13d9ca2c-8162-44fa-bfe3-56d9f9a89f0a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 91,
                column: "PermissionControllerActionGuid",
                value: new Guid("e162f3b2-2b7b-4e46-9734-cd4030d5910b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 92,
                column: "PermissionControllerActionGuid",
                value: new Guid("1dba87eb-f3fb-42b7-bbb9-31832d99218d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 93,
                column: "PermissionControllerActionGuid",
                value: new Guid("e6db2d7c-cc0f-4c24-a0d0-8fbf381198cf"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 94,
                column: "PermissionControllerActionGuid",
                value: new Guid("98c45e42-d238-4aaa-af33-93fbb2eafd09"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 95,
                column: "PermissionControllerActionGuid",
                value: new Guid("2a1b4b3e-a15e-4570-8ef8-c068d6153687"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 96,
                column: "PermissionControllerActionGuid",
                value: new Guid("94873267-41d8-4d73-bcba-354bded28bc4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 97,
                column: "PermissionControllerActionGuid",
                value: new Guid("488dc2e0-ab11-4163-81b0-b8029bfa75e8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 98,
                column: "PermissionControllerActionGuid",
                value: new Guid("33e2b820-01e7-422c-ac45-bc98a1373bfd"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 99,
                column: "PermissionControllerActionGuid",
                value: new Guid("b9e386d4-8c31-4195-a4b2-4d575e08fe93"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 100,
                column: "PermissionControllerActionGuid",
                value: new Guid("ebd672e4-e348-4e62-bcff-16e032276b6c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 101,
                column: "PermissionControllerActionGuid",
                value: new Guid("f7b679b7-f8f2-475f-a7d3-0d2e1ca8914d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 102,
                column: "PermissionControllerActionGuid",
                value: new Guid("311167df-a706-4d13-86f5-4a4ec139fbd7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 103,
                column: "PermissionControllerActionGuid",
                value: new Guid("f4523c0e-36ee-4d0f-8993-9121305ccff2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 104,
                column: "PermissionControllerActionGuid",
                value: new Guid("f8d6e8dc-2c86-442d-8e68-5f8528ca4cb7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 105,
                column: "PermissionControllerActionGuid",
                value: new Guid("fc744ccf-bdeb-4bf8-b84b-12d6922c3686"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 106,
                column: "PermissionControllerActionGuid",
                value: new Guid("9d5cfd22-96cc-41c3-a727-6b2904f89e3b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 107,
                column: "PermissionControllerActionGuid",
                value: new Guid("6b9bcebf-db05-4c9d-9468-88d78214ec40"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 108,
                column: "PermissionControllerActionGuid",
                value: new Guid("587c3ff2-916b-474f-a104-00e60cb31365"));

            //migrationBuilder.InsertData(
            //    schema: "Setting",
            //    table: "Country",
            //    columns: new[] { "CountryID", "CountryGuid", "CreateDate", "DeleteDate", "EnableDate", "Extension", "Flag", "IsDeleted", "IsEnable", "Lat", "Long", "NameAR", "NameEN", "Place", "UpdateDate", "UserDelete", "UserEnable", "UserId", "UserUpdate", "Zoom" },
            //    values: new object[] { 1, new Guid("5644696e-38c6-4d2e-8043-fca0698c03f3"), new DateTime(2021, 9, 13, 17, 48, 34, 350, DateTimeKind.Local).AddTicks(4634), null, new DateTime(2021, 9, 13, 17, 48, 34, 352, DateTimeKind.Local).AddTicks(320), "00966", null, false, true, "", "", "السعودية", "SA", "", null, null, null, 1, null, null });

            //migrationBuilder.InsertData(
            //    schema: "Setting",
            //    table: "Jobs",
            //    columns: new[] { "JobsID", "CreateDate", "DeleteDate", "DescriptionAr", "DescriptionEn", "EnableDate", "Image", "IsDeleted", "IsEnable", "JobTypeId", "JobsGuid", "NameAR", "NameEN", "UpdateDate", "UserDelete", "UserEnable", "UserId", "UserUpdate" },
            //    values: new object[] { 1, new DateTime(2021, 9, 13, 17, 48, 34, 355, DateTimeKind.Local).AddTicks(6909), null, null, null, new DateTime(2021, 9, 13, 17, 48, 34, 355, DateTimeKind.Local).AddTicks(7955), null, false, true, 2, new Guid("44a7e19f-b268-485e-ae55-94427491b818"), "الدمام", "DMM", null, null, null, 1, null });

            //migrationBuilder.InsertData(
            //    schema: "Setting",
            //    table: "MainCategory",
            //    columns: new[] { "MainCategoryID", "CreateDate", "DeleteDate", "EnableDate", "IsDeleted", "IsEnable", "MainCategoryGuid", "NameAR", "NameEN", "UpdateDate", "UserDelete", "UserEnable", "UserId", "UserUpdate" },
            //    values: new object[] { 1, new DateTime(2021, 9, 13, 17, 48, 34, 356, DateTimeKind.Local).AddTicks(3001), null, new DateTime(2021, 9, 13, 17, 48, 34, 356, DateTimeKind.Local).AddTicks(4040), false, true, new Guid("2f29ab91-022f-463c-b1fe-78ee66946874"), "الدمام", "DMM", null, null, null, 1, null });

            //migrationBuilder.InsertData(
            //    schema: "Setting",
            //    table: "Nationality",
            //    columns: new[] { "NationalityID", "CreateDate", "DeleteDate", "DescriptionAr", "DescriptionEn", "EnableDate", "IsDeleted", "IsEnable", "NameAR", "NameEN", "NationalityGuid", "UpdateDate", "UserDelete", "UserEnable", "UserId", "UserUpdate" },
            //    values: new object[] { 1, new DateTime(2021, 9, 13, 17, 48, 34, 355, DateTimeKind.Local).AddTicks(701), null, null, null, new DateTime(2021, 9, 13, 17, 48, 34, 355, DateTimeKind.Local).AddTicks(1740), false, true, "الدمام", "DMM", new Guid("873e87dc-5983-4632-8d45-64533c5d344b"), null, null, null, 1, null });

            //migrationBuilder.InsertData(
            //    schema: "Setting",
            //    table: "Departments",
            //    columns: new[] { "DepartmentsID", "CreateDate", "DeleteDate", "DepartmentsGuid", "DescriptionAr", "DescriptionEn", "EnableDate", "Image", "IsDeleted", "IsEnable", "Isunique", "MainCategoryID", "NameAR", "NameEN", "UpdateDate", "UserDelete", "UserEnable", "UserId", "UserUpdate" },
            //    values: new object[] { 1, new DateTime(2021, 9, 13, 17, 48, 34, 356, DateTimeKind.Local).AddTicks(9698), null, new Guid("731b44f3-1b49-4299-a1dd-50c627e74fe4"), null, null, new DateTime(2021, 9, 13, 17, 48, 34, 357, DateTimeKind.Local).AddTicks(715), null, false, true, false, 1, "الدمام", "DMM", null, null, null, 1, null });

            //migrationBuilder.InsertData(
            //    schema: "Setting",
            //    table: "Region",
            //    columns: new[] { "RegionID", "CountryID", "CreateDate", "DeleteDate", "EnableDate", "IsDeleted", "IsEnable", "Lat", "Long", "NameAR", "NameEN", "Place", "RegionGuid", "UpdateDate", "UserDelete", "UserEnable", "UserId", "UserUpdate", "Zoom" },
            //    values: new object[] { 1, 1, new DateTime(2021, 9, 13, 17, 48, 34, 352, DateTimeKind.Local).AddTicks(9628), null, new DateTime(2021, 9, 13, 17, 48, 34, 353, DateTimeKind.Local).AddTicks(674), false, true, "", "", "الدمام", "DMM", "", new Guid("ddf31fc0-43a1-4fb5-a8a0-17d9e73b56f9"), null, null, null, 1, null, null });

            //migrationBuilder.InsertData(
            //    schema: "Setting",
            //    table: "City",
            //    columns: new[] { "CityID", "CityGuid", "Code", "CreateDate", "DeleteDate", "EnableDate", "IsDeleted", "IsEnable", "Lat", "Long", "NameAR", "NameEN", "Place", "RegionID", "UpdateDate", "UserDelete", "UserEnable", "UserId", "UserUpdate", "Zoom" },
            //    values: new object[] { 1, new Guid("7e4a599e-40cb-4e6a-b462-269035a80906"), null, new DateTime(2021, 9, 13, 17, 48, 34, 354, DateTimeKind.Local).AddTicks(1681), null, new DateTime(2021, 9, 13, 17, 48, 34, 354, DateTimeKind.Local).AddTicks(2709), false, true, "", "", "الدمام", "DMM", "", 1, null, null, null, 1, null, null });

            //migrationBuilder.InsertData(
            //    schema: "Emp",
            //    table: "Employees",
            //    columns: new[] { "EntityEmpID", "AddressAR", "AddressEN", "BirthDateHijri", "BirthDateMilady", "BloodTypeId", "CityID", "CreateDate", "DeleteDate", "Email", "EnableDate", "EntityEmpGuid", "FileNo", "FirstNameAR", "FirstNameEN", "Gender", "IDNo", "IsDeleted", "IsEnable", "JobsID", "LastNameAR", "LastNameEN", "Lat", "Lng", "MidNameAR", "MidNameEN", "MobileNo", "NationalityID", "Notes", "PhoneNumber", "Photo", "UpdateDate", "UserDelete", "UserEnable", "UserId", "UserUpdate", "Zoom" },
            //    values: new object[] { 1, "الاسماعيليه", "ismailia", "", null, 1, 1, new DateTime(2021, 9, 13, 17, 48, 34, 358, DateTimeKind.Local).AddTicks(2149), null, "SystemUser@Admin.com", new DateTime(2021, 9, 13, 17, 48, 34, 358, DateTimeKind.Local).AddTicks(2736), new Guid("2299447c-fc61-4aa4-ba03-8c91e4f4b2d5"), "123321", "احمد", "Ahmed", 1, "", false, true, 1, "حسين", "Hussien", "", "", "سيد", "Sayed", "0595489633", 1, "", "", "", null, null, 1, 1, null, "" });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerLocation_AddressTypesID",
                schema: "Customer",
                table: "CustomerLocation",
                column: "AddressTypesID");

            migrationBuilder.CreateIndex(
                name: "IX_AddressTypes_UserId",
                schema: "Setting",
                table: "AddressTypes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistory_OrderStatusID",
                schema: "Order",
                table: "OrderHistory",
                column: "OrderStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistory_OrderVendorID",
                schema: "Order",
                table: "OrderHistory",
                column: "OrderVendorID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistory_UserId",
                schema: "Order",
                table: "OrderHistory",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderVendorID",
                schema: "Order",
                table: "OrderItems",
                column: "OrderVendorID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductID",
                schema: "Order",
                table: "OrderItems",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_UserId",
                schema: "Order",
                table: "OrderItems",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPromo_OrdersID",
                schema: "Order",
                table: "OrderPromo",
                column: "OrdersID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPromo_PromoCodeID",
                schema: "Order",
                table: "OrderPromo",
                column: "PromoCodeID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPromo_UserId",
                schema: "Order",
                table: "OrderPromo",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerLocationID",
                schema: "Order",
                table: "Orders",
                column: "CustomerLocationID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomersID",
                schema: "Order",
                table: "Orders",
                column: "CustomersID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderStatusID",
                schema: "Order",
                table: "Orders",
                column: "OrderStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PromoCodeID",
                schema: "Order",
                table: "Orders",
                column: "PromoCodeID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                schema: "Order",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderVendor_OrdersID",
                schema: "Order",
                table: "OrderVendor",
                column: "OrdersID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderVendor_OrderStatusID",
                schema: "Order",
                table: "OrderVendor",
                column: "OrderStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderVendor_UserId",
                schema: "Order",
                table: "OrderVendor",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderVendor_VendorsID",
                schema: "Order",
                table: "OrderVendor",
                column: "VendorsID");

            migrationBuilder.CreateIndex(
                name: "IX_PromoCode_UserId",
                schema: "Order",
                table: "PromoCode",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerLocation_AddressTypes_AddressTypesID",
                schema: "Customer",
                table: "CustomerLocation",
                column: "AddressTypesID",
                principalSchema: "Setting",
                principalTable: "AddressTypes",
                principalColumn: "AddressTypesID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerLocation_AddressTypes_AddressTypesID",
                schema: "Customer",
                table: "CustomerLocation");

            migrationBuilder.DropTable(
                name: "AddressTypes",
                schema: "Setting");

            migrationBuilder.DropTable(
                name: "OrderHistory",
                schema: "Order");

            migrationBuilder.DropTable(
                name: "OrderItems",
                schema: "Order");

            migrationBuilder.DropTable(
                name: "OrderPromo",
                schema: "Order");

            migrationBuilder.DropTable(
                name: "OrderVendor",
                schema: "Order");

            migrationBuilder.DropTable(
                name: "Orders",
                schema: "Order");

            migrationBuilder.DropTable(
                name: "PromoCode",
                schema: "Order");

            migrationBuilder.DropIndex(
                name: "IX_CustomerLocation_AddressTypesID",
                schema: "Customer",
                table: "CustomerLocation");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
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
                schema: "Setting",
                table: "Departments",
                keyColumn: "DepartmentsID",
                keyValue: 1);

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
                name: "AddressTypesID",
                schema: "Customer",
                table: "CustomerLocation");

            migrationBuilder.DropColumn(
                name: "IsVerfiy",
                schema: "Customer",
                table: "CustomerLocation");

            migrationBuilder.DropColumn(
                name: "MobileNo",
                schema: "Customer",
                table: "CustomerLocation");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "Customer",
                table: "CustomerLocation");

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 1,
                column: "PermissionControllerGuid",
                value: new Guid("2a1b43c6-4bcc-4f4e-aefc-a94dc0d1f3ee"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 2,
                column: "PermissionControllerGuid",
                value: new Guid("8b323291-0d1d-4c25-9f50-5312d0f1027a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 3,
                column: "PermissionControllerGuid",
                value: new Guid("f27b4966-6a9d-42d4-afac-be441e9dc66d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 4,
                column: "PermissionControllerGuid",
                value: new Guid("61b7c04e-619c-4078-bee4-fd6d5e928880"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 5,
                column: "PermissionControllerGuid",
                value: new Guid("cff0d01b-4964-4f3c-ad96-98bd03757f98"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 6,
                column: "PermissionControllerGuid",
                value: new Guid("eea306c8-4e3c-4fac-8836-9423413edc72"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 7,
                column: "PermissionControllerGuid",
                value: new Guid("355b2628-5d65-49f4-889d-e45892db1ca7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 8,
                column: "PermissionControllerGuid",
                value: new Guid("007021e8-697e-4ff6-b1b4-90a149bcaf9b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 9,
                column: "PermissionControllerGuid",
                value: new Guid("a1a6d08a-00aa-437f-9545-2d1b66133b6b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 10,
                column: "PermissionControllerGuid",
                value: new Guid("aa4004e9-6a0a-44b2-aa93-48c6c71fbca9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 11,
                column: "PermissionControllerGuid",
                value: new Guid("1098559d-3aab-41d7-a6f1-856df3a8975e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 12,
                column: "PermissionControllerGuid",
                value: new Guid("33de6acd-042c-4780-955c-07a11ff8b85f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 13,
                column: "PermissionControllerGuid",
                value: new Guid("65493efd-08bc-4112-be63-57b63a87b873"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 14,
                column: "PermissionControllerGuid",
                value: new Guid("b3bd1a54-c2aa-4b6e-9f50-bb9ff8f30cfd"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 15,
                column: "PermissionControllerGuid",
                value: new Guid("414c5016-4712-4da5-b8b3-03e70a40b80e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 16,
                column: "PermissionControllerGuid",
                value: new Guid("6fff47d9-4d3b-4282-9e3b-d97d1d3e8a2f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 17,
                column: "PermissionControllerGuid",
                value: new Guid("363006fe-5d9a-4731-bff0-7c59b642af7b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 18,
                column: "PermissionControllerGuid",
                value: new Guid("d0100337-aea3-4c9d-b7f2-d442c7996800"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 19,
                column: "PermissionControllerGuid",
                value: new Guid("cfb0f1a4-c09b-470f-8273-74bdef6a6262"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 20,
                column: "PermissionControllerGuid",
                value: new Guid("08899a79-774a-4182-87b0-7eda72db4c5c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 21,
                column: "PermissionControllerGuid",
                value: new Guid("104edbe8-b47e-4168-8186-c405ad8eaead"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 22,
                column: "PermissionControllerGuid",
                value: new Guid("765c3632-ea1d-4119-aa06-9ee7ea3b0403"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 23,
                column: "PermissionControllerGuid",
                value: new Guid("028b4807-efbf-44f9-9fb8-0634599debd7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 24,
                column: "PermissionControllerGuid",
                value: new Guid("20b43775-f960-462e-8211-4297d87ac04a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 25,
                column: "PermissionControllerGuid",
                value: new Guid("3b4e97d3-91fa-45c8-a2ff-6205f2e418e7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 26,
                column: "PermissionControllerGuid",
                value: new Guid("32499e83-d2b8-4fda-8107-f47da6fb7209"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 27,
                column: "PermissionControllerGuid",
                value: new Guid("6e0e5113-6e91-4744-bfe9-5ed00797005c"));

            //migrationBuilder.InsertData(
            //    schema: "Permission",
            //    table: "PermissionController",
            //    columns: new[] { "PermissionControllerID", "PermissionControllerGuid", "PermissionControllerNameAr", "PermissionControllerNameEn" },
            //    values: new object[] { 106, new Guid("490584f0-7209-4c36-9350-2d5729f7b331"), "العملاء", "Customer" });

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 1,
                column: "PermissionControllerActionGuid",
                value: new Guid("0c29d9a0-01b5-4ba8-be81-aae76cfcfb5a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 2,
                column: "PermissionControllerActionGuid",
                value: new Guid("ef231230-e72e-4579-ad52-13ff66fa887d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 3,
                column: "PermissionControllerActionGuid",
                value: new Guid("9e6d9889-a30d-46a2-b376-deefb46b4c9a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 4,
                column: "PermissionControllerActionGuid",
                value: new Guid("8bb86d34-1eb8-40fe-b317-2333d59108c4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 5,
                column: "PermissionControllerActionGuid",
                value: new Guid("b19fe389-bc24-4318-bba9-211f74fce0a5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 6,
                column: "PermissionControllerActionGuid",
                value: new Guid("ea738a1a-ae4c-435c-aaf9-83ea74c17238"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 7,
                column: "PermissionControllerActionGuid",
                value: new Guid("65dfc17a-7876-47ca-bf77-9ffd6b0b0638"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 8,
                column: "PermissionControllerActionGuid",
                value: new Guid("c4eab51c-23b0-4037-ad55-d9807f7d6e10"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 9,
                column: "PermissionControllerActionGuid",
                value: new Guid("b4494504-b8d6-485b-80e2-36480a933ed3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 10,
                column: "PermissionControllerActionGuid",
                value: new Guid("c95df278-2ee7-4ad6-a494-ccf0317814ea"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 11,
                column: "PermissionControllerActionGuid",
                value: new Guid("e5a0361a-8af4-47f3-b875-665b39d18dc4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 12,
                column: "PermissionControllerActionGuid",
                value: new Guid("0ede8533-9585-4fbc-b3ae-37dd1b48be1b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 13,
                column: "PermissionControllerActionGuid",
                value: new Guid("a36b9671-3d04-44d3-aae8-988b50c6be2b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 14,
                column: "PermissionControllerActionGuid",
                value: new Guid("5f720cfc-8797-4103-9b16-e8046b4409b5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 15,
                column: "PermissionControllerActionGuid",
                value: new Guid("3047bcea-9d75-45da-a48e-ec2a3d09cb74"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 16,
                column: "PermissionControllerActionGuid",
                value: new Guid("44ff3295-cf4c-41f4-bbac-4e3db369399e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 17,
                column: "PermissionControllerActionGuid",
                value: new Guid("79f38e27-bb1a-46ec-a448-28e2613d1e73"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 18,
                column: "PermissionControllerActionGuid",
                value: new Guid("9bcd189a-28ad-4754-a26d-e6f077307ab2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 19,
                column: "PermissionControllerActionGuid",
                value: new Guid("c6ffe7d0-41d6-45c0-8b40-91bf5cd55304"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 20,
                column: "PermissionControllerActionGuid",
                value: new Guid("ef2aa334-1869-4f3e-ad93-12d61b22085f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 21,
                column: "PermissionControllerActionGuid",
                value: new Guid("2e7707d8-408d-4840-b74b-725cbaedf49b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 22,
                column: "PermissionControllerActionGuid",
                value: new Guid("9b82917a-b21a-4c77-a452-4f020848c1a4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 23,
                column: "PermissionControllerActionGuid",
                value: new Guid("bb7c3bb8-fd86-4f6c-b21d-01f40f027479"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 24,
                column: "PermissionControllerActionGuid",
                value: new Guid("a4d4543d-5a5e-40a3-9ba3-6e1e024544f5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 25,
                column: "PermissionControllerActionGuid",
                value: new Guid("cc814407-6077-460a-a94e-63aea9d535f1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 26,
                column: "PermissionControllerActionGuid",
                value: new Guid("c9f2d97d-4c9f-40a1-a571-0823f265ae55"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 27,
                column: "PermissionControllerActionGuid",
                value: new Guid("1730315a-e79d-4e7c-afda-6b291c9259ff"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 28,
                column: "PermissionControllerActionGuid",
                value: new Guid("993267fe-0289-4c4a-89de-e1839b5be83e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 29,
                column: "PermissionControllerActionGuid",
                value: new Guid("ca53828d-46c4-493a-8d96-00cfcc7625b8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 30,
                column: "PermissionControllerActionGuid",
                value: new Guid("4361509b-e838-4638-a227-b75a11858d3c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 31,
                column: "PermissionControllerActionGuid",
                value: new Guid("9cc8061c-b863-4927-9223-915745429f76"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 32,
                column: "PermissionControllerActionGuid",
                value: new Guid("2a07fa10-0b19-4e63-a82c-6b1851332dfe"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 33,
                column: "PermissionControllerActionGuid",
                value: new Guid("da8f3ce5-c0e2-4f72-9d74-08d4eb3473b7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 34,
                column: "PermissionControllerActionGuid",
                value: new Guid("657617bf-a595-4eef-8ec5-2a10ab518c5c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 35,
                column: "PermissionControllerActionGuid",
                value: new Guid("946f780e-1970-4e55-bcb1-3b9c9221d36f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 36,
                column: "PermissionControllerActionGuid",
                value: new Guid("57371b54-5768-40dc-bcc9-8abb36dce64e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 37,
                column: "PermissionControllerActionGuid",
                value: new Guid("bed88a6d-31ad-4607-b33e-05b10a1d9881"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 38,
                column: "PermissionControllerActionGuid",
                value: new Guid("43e410bc-7411-4c99-b2e6-1fb7b3d4f9d4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 39,
                column: "PermissionControllerActionGuid",
                value: new Guid("42b2e9c5-8de3-49f2-86e1-d129d7896745"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 40,
                column: "PermissionControllerActionGuid",
                value: new Guid("10c7f41c-d9fa-49a9-a14a-b08f97932a58"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 41,
                column: "PermissionControllerActionGuid",
                value: new Guid("7ffe3659-895e-4a21-b13e-86416546c4dc"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 42,
                column: "PermissionControllerActionGuid",
                value: new Guid("0ef562c0-121f-499d-b543-5ed0417d721f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 43,
                column: "PermissionControllerActionGuid",
                value: new Guid("602b5030-ea6e-4f23-9d27-3a665063a816"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 44,
                column: "PermissionControllerActionGuid",
                value: new Guid("b28e6ca2-6f65-4356-9a40-b570a704f937"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 45,
                column: "PermissionControllerActionGuid",
                value: new Guid("dcb22a7b-262c-4349-875a-e206d2e216c2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 46,
                column: "PermissionControllerActionGuid",
                value: new Guid("04a80ec9-72a9-4566-8bb8-475286a2a694"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 47,
                column: "PermissionControllerActionGuid",
                value: new Guid("37aa1ba0-a4ff-435a-995f-a7bc588f0e85"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 48,
                column: "PermissionControllerActionGuid",
                value: new Guid("2d751df0-be37-4576-aa5f-04034407e455"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 49,
                column: "PermissionControllerActionGuid",
                value: new Guid("ceebacb8-f84b-49a1-a7be-a48f60d8cb14"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 50,
                column: "PermissionControllerActionGuid",
                value: new Guid("b34f5b1f-c316-4164-982f-501d3e485549"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 51,
                column: "PermissionControllerActionGuid",
                value: new Guid("d35685de-3171-4dbb-bd49-7ac9a81c893d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 52,
                column: "PermissionControllerActionGuid",
                value: new Guid("8b4feaa0-04f6-42cb-b102-07cd86bff964"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 53,
                column: "PermissionControllerActionGuid",
                value: new Guid("3212a430-ac18-4bc7-a622-191ab47b7f33"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 54,
                column: "PermissionControllerActionGuid",
                value: new Guid("4a748d81-4907-4f93-8a4e-729696892b40"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 55,
                column: "PermissionControllerActionGuid",
                value: new Guid("8ad93e68-ffd6-49b4-8671-d97e9082ae76"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 56,
                column: "PermissionControllerActionGuid",
                value: new Guid("917a18b0-877a-4866-8f13-ace05cc67874"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 57,
                column: "PermissionControllerActionGuid",
                value: new Guid("66a71941-a6c8-4885-800b-ab54e902fc92"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 58,
                column: "PermissionControllerActionGuid",
                value: new Guid("f9bee95b-990a-4dec-9630-bd545e981d63"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 59,
                column: "PermissionControllerActionGuid",
                value: new Guid("71cc4509-fa65-4586-8e03-5249f8612644"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 60,
                column: "PermissionControllerActionGuid",
                value: new Guid("0baed146-2444-4906-a519-35aba33dd5ee"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 61,
                column: "PermissionControllerActionGuid",
                value: new Guid("2ac0d801-25d9-448f-ba2f-8023b0bfdbed"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 62,
                column: "PermissionControllerActionGuid",
                value: new Guid("08b6a896-863f-428a-a26c-7ac13c718934"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 63,
                column: "PermissionControllerActionGuid",
                value: new Guid("3b589be2-fc6a-4945-bb4a-2f2302c32678"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 64,
                column: "PermissionControllerActionGuid",
                value: new Guid("158e2cea-75a2-4edf-a62e-6e9a5d2b2ff6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 65,
                column: "PermissionControllerActionGuid",
                value: new Guid("7ce387aa-5829-4b38-bdaa-f5ae9df04d5b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 66,
                column: "PermissionControllerActionGuid",
                value: new Guid("bcf146b8-0dda-4d79-ba91-84a052b766b3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 67,
                column: "PermissionControllerActionGuid",
                value: new Guid("0257c9ca-adb9-4dd7-99ff-71b06a78e109"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 68,
                column: "PermissionControllerActionGuid",
                value: new Guid("ae19d7ec-e5e6-41be-88f6-d8658f741c7a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 69,
                column: "PermissionControllerActionGuid",
                value: new Guid("271332ae-52e3-4cb3-a024-0ad370ee834f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 70,
                column: "PermissionControllerActionGuid",
                value: new Guid("a3593ddf-d908-4f2e-8dc4-16c4e478a1ac"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 71,
                column: "PermissionControllerActionGuid",
                value: new Guid("1fb0ca44-f88c-4306-8849-9bd6eee07c0f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 72,
                column: "PermissionControllerActionGuid",
                value: new Guid("a76dab35-644b-4f84-be36-45b2f1dfcb23"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 73,
                column: "PermissionControllerActionGuid",
                value: new Guid("d5d26548-6b0f-4668-90ec-de6c698d7f23"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 74,
                column: "PermissionControllerActionGuid",
                value: new Guid("8bbfb94f-6cef-4501-8bfe-32804832c997"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 75,
                column: "PermissionControllerActionGuid",
                value: new Guid("06d68bba-98a7-4ec4-ac4d-93e028ea1d96"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 76,
                column: "PermissionControllerActionGuid",
                value: new Guid("4de96f4e-18ec-49fe-9b7a-445ab4844b52"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 77,
                column: "PermissionControllerActionGuid",
                value: new Guid("05c24d84-2ff3-4ad5-b4ac-f0b7e6fadbfd"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 78,
                column: "PermissionControllerActionGuid",
                value: new Guid("8a661a99-427c-4483-bd71-90004ce73c04"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 79,
                column: "PermissionControllerActionGuid",
                value: new Guid("d12cf8ba-8144-4807-8c77-f135ba28facb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 80,
                column: "PermissionControllerActionGuid",
                value: new Guid("3da4a6aa-52f4-4e06-a973-99bc04ea9581"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 81,
                column: "PermissionControllerActionGuid",
                value: new Guid("e7f3b582-39a3-4a94-9f71-c1504dc24113"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 82,
                column: "PermissionControllerActionGuid",
                value: new Guid("5a48ab00-810a-4767-a3d7-3f346cda31ca"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 83,
                column: "PermissionControllerActionGuid",
                value: new Guid("47e87751-ee2a-4e09-b12b-34791ae75f24"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 84,
                column: "PermissionControllerActionGuid",
                value: new Guid("8f742ef1-3d8b-43db-a76d-d815e4fd11e6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 85,
                column: "PermissionControllerActionGuid",
                value: new Guid("4dd6657d-2d72-4c1b-be3c-ca2d194b7028"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 86,
                column: "PermissionControllerActionGuid",
                value: new Guid("3c881e7c-d095-4243-ab2a-8364338d7d0f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 87,
                column: "PermissionControllerActionGuid",
                value: new Guid("25cacf94-c2ff-4968-8b16-27baccfc46ce"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 88,
                column: "PermissionControllerActionGuid",
                value: new Guid("f012e07c-70e5-4845-903e-69c97dd4f5cd"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 89,
                column: "PermissionControllerActionGuid",
                value: new Guid("b8837363-4c46-49bf-9e50-95ed69705716"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 90,
                column: "PermissionControllerActionGuid",
                value: new Guid("313ed2ee-d85b-4438-8c21-e8909d9c80a2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 91,
                column: "PermissionControllerActionGuid",
                value: new Guid("c91f9fb6-e035-4adb-88ad-fac416759e48"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 92,
                column: "PermissionControllerActionGuid",
                value: new Guid("0520ea9d-7462-44ff-bbce-bdeb8ec17320"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 93,
                column: "PermissionControllerActionGuid",
                value: new Guid("2f9c017a-7693-4c5c-a79b-8f1079288dd5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 94,
                column: "PermissionControllerActionGuid",
                value: new Guid("06127e32-b9df-4955-811c-5a9a823dd2f9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 95,
                column: "PermissionControllerActionGuid",
                value: new Guid("25159ebc-2614-4daa-a503-72311a1eb828"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 96,
                column: "PermissionControllerActionGuid",
                value: new Guid("be9bf891-d177-4bef-bd55-3788e21c1268"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 97,
                column: "PermissionControllerActionGuid",
                value: new Guid("b63212fc-3be5-4b70-a291-0f4fcbb082ec"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 98,
                column: "PermissionControllerActionGuid",
                value: new Guid("65c6b7cd-1ec3-4992-9dca-4a039ec32fa2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 99,
                column: "PermissionControllerActionGuid",
                value: new Guid("0a853477-0790-4de6-8b06-9478b4a133e4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 100,
                column: "PermissionControllerActionGuid",
                value: new Guid("4489c4a3-4c42-4602-81d5-98be8736200f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 101,
                column: "PermissionControllerActionGuid",
                value: new Guid("20234b1a-afb7-449a-8973-e8e4f7e727bd"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 102,
                column: "PermissionControllerActionGuid",
                value: new Guid("2a1e4009-f625-433f-81c7-ac746216f43d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 103,
                column: "PermissionControllerActionGuid",
                value: new Guid("18de67f8-3240-4c92-90b3-9eade5c49e12"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 104,
                column: "PermissionControllerActionGuid",
                value: new Guid("e54da605-4d22-4be7-9446-2eae8e6f51ad"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 105,
                column: "PermissionControllerActionGuid",
                value: new Guid("ae07b56a-5d81-43fb-813a-08db1e3d9c3d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 106,
                column: "PermissionControllerActionGuid",
                value: new Guid("d859e49c-474e-4a9e-ad71-82dd62014264"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 107,
                column: "PermissionControllerActionGuid",
                value: new Guid("96d7aa9c-5ded-4376-b955-d10daccefab9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 108,
                column: "PermissionControllerActionGuid",
                value: new Guid("e7927e6d-3eab-4d5d-90ae-4da8fede7d13"));

            migrationBuilder.InsertData(
                schema: "Permission",
                table: "PermissionControllerAction",
                columns: new[] { "PermissionControllerActionID", "PermissionActionID", "PermissionControllerActionGuid", "PermissionControllerID" },
                values: new object[,]
                {
                    { 500, 1, new Guid("e4d22d38-636b-4809-90f8-f20fef7edfcb"), 100 },
                    { 521, 2, new Guid("5814ffc0-86c6-4d82-a8bc-85d6508350d4"), 105 },
                    { 520, 1, new Guid("3390f9a0-c399-413a-9437-da628fd54b4b"), 105 },
                    { 519, 4, new Guid("cbc14edc-84c0-4d1a-8f23-b69fcb173c5d"), 104 },
                    { 518, 3, new Guid("4fefe1b4-5c41-4652-a758-60b6c5261268"), 104 },
                    { 517, 2, new Guid("1447ca60-99ea-4c66-b041-a1be899813ae"), 104 },
                    { 516, 1, new Guid("28773df8-cb8f-4c31-9fb8-90566e71f11f"), 104 },
                    { 615, 4, new Guid("d56df675-78df-4d1f-ac56-89d188122d8f"), 103 },
                    { 514, 3, new Guid("bd616524-2df0-444d-87f2-f4032e1c96b8"), 103 },
                    { 513, 2, new Guid("1a7304a5-7b3b-4aa4-9b27-07202be8d338"), 103 },
                    { 512, 1, new Guid("706692d3-e357-4a28-847e-71ef4f1602a7"), 103 },
                    { 511, 4, new Guid("0ca9452f-d93c-4bec-aab6-75f4e7c8c9e6"), 102 },
                    { 510, 3, new Guid("cf3d5e37-cec4-4d8d-8c98-98b64ec93d85"), 102 },
                    { 509, 2, new Guid("e2bc471d-ae5b-488e-9487-b8df306d8ee0"), 102 },
                    { 508, 1, new Guid("f8c98fe0-a868-4a55-882e-1d4d35d7b67e"), 102 },
                    { 507, 4, new Guid("1c24d4e3-4f5e-4456-b366-0362c85dce2e"), 101 },
                    { 506, 3, new Guid("c337d8b0-81d2-49ac-b3ea-0b73c1750669"), 101 },
                    { 505, 2, new Guid("97edeb42-07dd-4d1f-94b1-b97480a8ea90"), 101 },
                    { 504, 1, new Guid("d2b37762-d4c8-4fda-949d-554f50520012"), 101 },
                    { 503, 4, new Guid("f0d562f9-c272-44cc-9b95-26d93bd47ead"), 100 },
                    { 502, 3, new Guid("89e6e392-2578-4ec2-94b3-aa638a27e280"), 100 },
                    { 501, 2, new Guid("6d8d9d47-ee1b-4c4e-9554-5d1f64ca19d1"), 100 },
                    { 523, 4, new Guid("f2cbe649-17d4-4051-aab9-bf96465ff6fb"), 105 },
                    { 522, 3, new Guid("d240f9cd-57e3-4f0c-809a-b5c9646bf7c3"), 105 }
                });

            //migrationBuilder.InsertData(
            //    schema: "Permission",
            //    table: "PermissionControllerAction",
            //    columns: new[] { "PermissionControllerActionID", "PermissionActionID", "PermissionControllerActionGuid", "PermissionControllerID" },
            //    values: new object[,]
            //    {
            //        { 524, 1, new Guid("aa1f802a-b32c-4b9e-882a-30971b5b2559"), 106 },
            //        { 525, 2, new Guid("686dd3b0-967f-460b-a2bb-b0f684ac84a1"), 106 },
            //        { 526, 3, new Guid("51640b11-ec2b-433a-a4d5-c6ce86c5383f"), 106 },
            //        { 527, 4, new Guid("3cd3dad0-5cb8-4069-9462-7c2320b81e88"), 106 }
            //    });
        }
    }
}
