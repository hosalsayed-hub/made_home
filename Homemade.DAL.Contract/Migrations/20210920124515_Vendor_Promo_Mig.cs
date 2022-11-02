using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Homemade.DAL.Contract.Migrations
{
    public partial class Vendor_Promo_Mig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VendorPromo",
                schema: "Order",
                columns: table => new
                {
                    VendorPromoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VendorPromoGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VendorsID = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_VendorPromo", x => x.VendorPromoID);
                    table.ForeignKey(
                        name: "FK_VendorPromo_PromoCode_PromoCodeID",
                        column: x => x.PromoCodeID,
                        principalSchema: "Order",
                        principalTable: "PromoCode",
                        principalColumn: "PromoCodeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VendorPromo_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VendorPromo_Vendors_VendorsID",
                        column: x => x.VendorsID,
                        principalSchema: "Vendor",
                        principalTable: "Vendors",
                        principalColumn: "VendorsID",
                        onDelete: ReferentialAction.Restrict);
                });

            //migrationBuilder.InsertData(
            //    table: "Role",
            //    columns: new[] { "RoleId", "ConcurrencyStamp", "Name", "NormalizedName", "RoleTypeId" },
            //    values: new object[,]
            //    {
            //        { 1, "f80b674b-226a-4324-8d00-affd448f0ff8", "Admin", null, 1 },
            //        { 2, "08c6e18e-399e-4bdc-9b8b-18c0016f257c", "Vendor", null, 2 },
            //        { 3, "6ab08ae8-a6c1-4fa6-8923-792790392acd", "Customer", null, 2 },
            //        { 4, "532c882b-bb0e-4566-acae-ab16bd92ac81", "Employee", null, 2 }
            //    });

            //migrationBuilder.InsertData(
            //    table: "User",
            //    columns: new[] { "UserId", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserJWTToken", "UserName", "UserType" },
            //    values: new object[] { 1, 0, "eb4ea292-2d81-4cc7-8f6c-d7ea02e96c61", "SystemUser@Admin.com", true, false, null, "SystemUser@Admin.com", "SYSTEMUSER", "AQAAAAEAACcQAAAAEP65QXLX6e94ehLc9ntv07Q7n/aO6wf8y6j/z15XE7hfgyZLCNvHmM3Ar6SaTwzC3g==", "012", true, "90c6fe91-5c5c-44b3-95a9-d77582563aa9", false, null, "SystemUser", 1 });

            //migrationBuilder.InsertData(
            //    schema: "Permission",
            //    table: "PermissionAction",
            //    columns: new[] { "PermissionActionID", "PermissionActionGuid", "PermissionActionNameAr", "PermissionActionNameEn" },
            //    values: new object[,]
            //    {
            //        { 2, new Guid("0527cabe-ef22-4b8b-9787-145ec50ba4c8"), "اضافة", "Insert" },
            //        { 3, new Guid("ab3f22ec-d2e1-4a63-a628-746bb66378c9"), "تعديل", "Update" },
            //        { 4, new Guid("e7fda5b7-358b-471a-a624-3bb53b03a9fe"), "حذف", "Delete" },
            //        { 1, new Guid("cf6c0a4c-09e9-4cd6-af3d-fcee416f15b3"), "عرض", "View" }
            //    });

            //migrationBuilder.InsertData(
            //    schema: "Permission",
            //    table: "PermissionController",
            //    columns: new[] { "PermissionControllerID", "PermissionControllerGuid", "PermissionControllerNameAr", "PermissionControllerNameEn" },
            //    values: new object[,]
            //    {
            //        { 19, new Guid("161a101e-2562-4a84-8c0c-be057830a723"), "اعدادت الدفع", "Payment Configuration" },
            //        { 20, new Guid("6eaaa84f-c5e6-4779-8372-fb85ad2c0d98"), "التصنيفات الرئسيه", "Main categories" },
            //        { 21, new Guid("3de9ddf7-ff39-465d-8e72-f0a382ddf2be"), "البنرات", "Sliders" },
            //        { 22, new Guid("6d8d74de-78d5-48e1-8123-6408605ad760"), "شركات الشحن", "Shipping Company" },
            //        { 23, new Guid("35eb536d-db76-4bd7-a1ac-d217f5565402"), "الاستفسارات", "Inqueries" },
            //        { 24, new Guid("29aea49f-4037-4241-bc4a-7ced13c37f5c"), "الصفحات الرئيسية", "Main Pages" },
            //        { 25, new Guid("5a6b2e3d-e925-4f79-8cc1-747a985c34d3"), "المتاجر", "Vendors" },
            //        { 27, new Guid("4ed58be3-8a79-4504-970f-99e4be361134"), "المنتجات", "Product" },
            //        { 28, new Guid("697f1cb5-92f6-456b-966b-8fc56a930261"), "أنواع العناوين", "Address Types" },
            //        { 29, new Guid("a8118e37-0ba3-4c00-b959-cc1dc960ceef"), "الكلمات المفتاحية", "KeyWords" },
            //        { 30, new Guid("4082af1c-8ba4-408f-9446-7f3bf435d2fc"), "منتجات الاوبريشن", "Product Operation" },
            //        { 31, new Guid("a692c2af-e086-4122-be47-60eef839a35c"), "طلبات الاوبريشن", "Operation Orders" },
            //        { 32, new Guid("d96549e0-ba71-4a0d-a77c-c41e05fd77fe"), "طلبات المتجر", "Vendor Orders" },
            //        { 33, new Guid("c98dbfc5-f7e9-463c-9bfd-9a3fb04b5345"), "الفروع", "Branches" },
            //        { 26, new Guid("30256615-5d2a-4ddc-b11b-9b2f28c8bce8"), "الزبائن", "Customer" },
            //        { 18, new Guid("cebfafec-e18c-4992-bcf2-20d36d085df0"), "اعدادت الشركه", " Company Configuration" },
            //        { 16, new Guid("9c226776-084b-45b1-b998-7539198469ab"), "باقات التجار", "Package" },
            //        { 15, new Guid("93ab1e2c-cf5c-4eff-a1d2-8cdf4a7431d5"), "الأحياء", "Block" },
            //        { 1, new Guid("1143bafd-8b91-4f75-9bd0-55d7591dc3f7"), "الصلاحيات", "Permission" },
            //        { 2, new Guid("fb801320-e7cf-492b-b12d-e716f1d237ed"), "الدور", "Role" },
            //        { 3, new Guid("834190b9-36a2-401a-96d5-869dd3d33b71"), "الدولة", "Country" },
            //        { 4, new Guid("f75b1b4e-e03b-41d3-bd28-dcb0db900164"), "المنطقة", "Region" },
            //        { 5, new Guid("67155ccd-f9e6-49ba-bfe7-cba16dfa6558"), "المدينة", "City" },
            //        { 6, new Guid("8c02d452-aed3-4e81-b245-ab05c1b9d856"), "الوظائف", "Jobs" },
            //        { 17, new Guid("10bf0e35-f28e-4572-a166-dde8c21d6404"), "حالات الدفع", "PaymentStatus" },
            //        { 7, new Guid("bf150aba-10fa-4df8-9817-9575010d1369"), "الاقسام", "Departments" },
            //        { 9, new Guid("0de0873b-b2cf-441a-9188-e33d89093c16"), "الجنسية", "Nationality" },
            //        { 10, new Guid("ab407147-0b3f-4ab7-b93c-8a94ac80ff43"), "اعدادات الدور", "Role Configuration" },
            //        { 11, new Guid("d77d7174-7652-4c11-b49a-6fc215071317"), "الاعدادات", "Configuration" },
            //        { 12, new Guid("3e05be88-2504-4302-a620-549ceaed7c5d"), "البنوك", "Bank" },
            //        { 13, new Guid("d92a96c6-d0d8-4e4f-8fdf-6e9c61f07ca1"), "طرق الدفع", "PaymentWay" },
            //        { 34, new Guid("50c028a7-2523-49ac-8a00-9d439d8181f5"), "اكواد الخصم", "Promo Code" },
            //        { 8, new Guid("d6b8e87f-2eb7-4c22-bc52-0376195c9fa9"), "الموظفين", "Employees" }
            //    });

            //migrationBuilder.InsertData(
            //    schema: "Permission",
            //    table: "PermissionController",
            //    columns: new[] { "PermissionControllerID", "PermissionControllerGuid", "PermissionControllerNameAr", "PermissionControllerNameEn" },
            //    values: new object[] { 14, new Guid("dd56e1cb-6818-463f-9d07-610e1703cb28"), "الأنشطة", "Activity" });

            //migrationBuilder.InsertData(
            //    schema: "Permission",
            //    table: "PermissionControllerAction",
            //    columns: new[] { "PermissionControllerActionID", "PermissionActionID", "PermissionControllerActionGuid", "PermissionControllerID" },
            //    values: new object[,]
            //    {
            //        { 1, 1, new Guid("fa73bde0-1e2a-436c-b192-6a0a17b6f523"), 1 },
            //        { 101, 1, new Guid("e6f24a30-e748-40a4-8e26-3f3a8b5c2c6d"), 26 },
            //        { 100, 4, new Guid("dbaf3569-ba05-4192-94da-00a9a962dd09"), 25 },
            //        { 99, 3, new Guid("63acc631-69ea-451b-bf09-32488ae2e6d3"), 25 },
            //        { 98, 2, new Guid("dc9455fe-31ff-431a-95c3-18612bcfe147"), 25 },
            //        { 97, 1, new Guid("85179cd5-3e68-4041-b059-4b81229e4bbb"), 25 },
            //        { 96, 4, new Guid("e8ae1b3a-89ec-4e82-a10e-d5fabeb138ab"), 24 },
            //        { 95, 3, new Guid("31c5ae9b-31b4-4ed8-9c44-b913098b41a7"), 24 },
            //        { 94, 2, new Guid("4472c0dc-f6db-49bc-884c-f29d5761631f"), 24 },
            //        { 93, 1, new Guid("ab8f0647-4ea8-4212-9160-ca9398b954ca"), 24 },
            //        { 92, 4, new Guid("6841b2e0-2311-450f-8718-f4cac1f7c1c4"), 23 },
            //        { 91, 3, new Guid("5f531975-8e61-41eb-bd1f-f5c7cc071bdc"), 23 },
            //        { 90, 2, new Guid("61a13a66-06ff-4e0e-9e13-cad35c11b4ac"), 23 },
            //        { 89, 1, new Guid("76d8053b-8a7c-4f59-b68f-a5f0b4e66f35"), 23 },
            //        { 88, 4, new Guid("63bb9500-fec3-401a-b482-5a14def5aaaf"), 22 },
            //        { 102, 2, new Guid("f3363f1f-bf1b-4edc-b305-0cd427497328"), 26 },
            //        { 87, 3, new Guid("af49fce9-1f07-4492-8bf9-7da9c77bcbc4"), 22 },
            //        { 85, 1, new Guid("f364d6ce-18b7-4f7c-8ffd-7594d0b33673"), 22 },
            //        { 84, 4, new Guid("43c1bb16-25f1-4d26-8291-d0c94facd3db"), 21 },
            //        { 83, 3, new Guid("8fc2ae10-d951-47c7-a0d6-09099938c9e9"), 21 },
            //        { 82, 2, new Guid("bce78df7-afde-4af9-81d0-8127087fd08c"), 21 },
            //        { 81, 1, new Guid("0f2e5bcd-a18b-4beb-8ed4-14a14a6ada43"), 21 },
            //        { 80, 4, new Guid("16172f30-0cc3-4474-8ced-44fa27ac4bc3"), 20 },
            //        { 79, 3, new Guid("c056608d-4891-4d9e-aafd-5681ee7f9d3a"), 20 },
            //        { 78, 2, new Guid("a86e0a50-7b78-4ee6-8352-c4da39d10ba3"), 20 },
            //        { 77, 1, new Guid("a6219f89-aeea-4ba9-9f69-07a7c4c6ae36"), 20 },
            //        { 76, 4, new Guid("56ee98c2-7054-4fbb-8a66-1194bad43b72"), 19 },
            //        { 75, 3, new Guid("19de4ebc-1404-470d-bdc4-dc83e80d0cab"), 19 },
            //        { 74, 2, new Guid("098be709-9a78-4e0b-ba6e-8066970cc4a3"), 19 },
            //        { 73, 1, new Guid("9f7469e6-09a3-499a-8525-c5674ac9f689"), 19 },
            //        { 72, 4, new Guid("169a5c9c-d373-4988-bc5e-9cb9f4c5c076"), 18 },
            //        { 86, 2, new Guid("8bf3d36d-f2d5-4a2f-926d-37c99d934385"), 22 },
            //        { 103, 3, new Guid("6e852583-bac6-4909-9f9a-495f3197b0b0"), 26 },
            //        { 104, 4, new Guid("63880dd2-92f1-4c52-a500-9dae1a795535"), 26 },
            //        { 105, 1, new Guid("07a012a7-5dfc-4bea-8329-d4842494d415"), 27 },
            //        { 136, 4, new Guid("292218d3-6f41-4675-88c5-68ced445a520"), 34 },
            //        { 135, 3, new Guid("5dc59deb-27df-4c36-aead-aa3a3aec6334"), 34 },
            //        { 134, 2, new Guid("6e6d4536-254a-496d-a73f-959010bd1726"), 34 },
            //        { 133, 1, new Guid("71d2d5e7-3aa9-4485-bfcd-5368780c1e81"), 34 },
            //        { 132, 4, new Guid("df43c52a-ef7a-4f68-b73b-32882b2c02ae"), 33 },
            //        { 131, 3, new Guid("75147f50-56da-4086-9619-f285b9d38dc5"), 33 },
            //        { 130, 2, new Guid("2ce1bc56-8ced-4fac-9d02-f3c7a09b029a"), 33 }
            //    });

            //migrationBuilder.InsertData(
            //    schema: "Permission",
            //    table: "PermissionControllerAction",
            //    columns: new[] { "PermissionControllerActionID", "PermissionActionID", "PermissionControllerActionGuid", "PermissionControllerID" },
            //    values: new object[,]
            //    {
            //        { 129, 1, new Guid("04f7a84d-21f9-46f6-928a-f64ce3a2075f"), 33 },
            //        { 128, 4, new Guid("a8d8935f-be07-4afe-8246-cb97c584da76"), 32 },
            //        { 127, 3, new Guid("3e836f48-c8c8-4efb-898a-03b649f80c77"), 32 },
            //        { 126, 2, new Guid("df5b03ea-2e47-4f95-909c-b0861eaa471f"), 32 },
            //        { 125, 1, new Guid("55c41f76-7bb1-4388-ad1e-87e1c27ba3fe"), 32 },
            //        { 124, 4, new Guid("74f8bd9b-68e1-41f3-9269-3ccd737906f9"), 31 },
            //        { 123, 3, new Guid("de34dd9f-8423-4b9c-ab42-5d4ee7b7a660"), 31 },
            //        { 122, 2, new Guid("5b0eb712-3bc4-41e0-af69-4aedceb47ef8"), 31 },
            //        { 121, 1, new Guid("817db844-e618-45e5-bf5c-498e29ec102d"), 31 },
            //        { 120, 4, new Guid("57bd1f6e-e50f-498c-984a-f5184e98d2fb"), 30 },
            //        { 106, 2, new Guid("16f30902-39d5-43fd-901d-1bf88aa93bf5"), 27 },
            //        { 107, 3, new Guid("b9ac8651-960e-4a41-aab7-1d4fea7e9f3c"), 27 },
            //        { 108, 4, new Guid("f44dcb61-1101-4d5c-afeb-9a6cea8f5538"), 27 },
            //        { 109, 1, new Guid("b5f452e2-5c96-420c-837d-b2e9b55bc069"), 28 },
            //        { 110, 2, new Guid("b0a65b58-7630-45da-aa43-e7e2a3559a25"), 28 },
            //        { 111, 3, new Guid("2684abe3-0e71-431c-8f6d-b4e093bd7171"), 28 },
            //        { 71, 3, new Guid("892779c8-b6bb-493c-bf10-e69e74604c8d"), 18 },
            //        { 112, 4, new Guid("fcb29a97-71a7-45ba-9af5-0f5d9909923a"), 28 },
            //        { 114, 2, new Guid("3b9981fa-3b92-46bf-bf7b-214aefbc4e99"), 29 },
            //        { 115, 3, new Guid("7130846c-1e17-441b-aada-4eefe1919da1"), 29 },
            //        { 116, 4, new Guid("3967f3f3-8571-4d03-a491-2b4c79591710"), 29 },
            //        { 117, 1, new Guid("828aa73e-fdac-448e-a8f0-5cff5083e71a"), 30 },
            //        { 118, 2, new Guid("34b6c2a8-4a5c-46a9-917f-e4064cafedf9"), 30 },
            //        { 119, 3, new Guid("c4bad31f-7028-4aeb-a5e2-9b9e7d843c2b"), 30 },
            //        { 113, 1, new Guid("56d7bf0c-31a8-46a3-bbe0-df9d6c8164ce"), 29 },
            //        { 69, 1, new Guid("798c8a16-ce2c-499a-9c7d-a850f297c762"), 18 },
            //        { 70, 2, new Guid("2dd68668-cb29-4603-aa03-dbbf3879dc66"), 18 },
            //        { 33, 1, new Guid("73d8819b-88a4-4e9d-8918-09975497537a"), 9 },
            //        { 31, 3, new Guid("520aa626-7556-4f76-a059-312ccbbbb80c"), 8 },
            //        { 30, 2, new Guid("088c49d2-4c78-435d-8a75-3e0d44752d1b"), 8 },
            //        { 29, 1, new Guid("37f482bf-9a81-4730-b7c2-2425eaa7025b"), 8 },
            //        { 28, 4, new Guid("92cba8ff-100d-4b22-bbc9-ae3d5c44f8d8"), 7 },
            //        { 27, 3, new Guid("c1585c57-6ad6-4ab0-b037-4f66ed0467a4"), 7 },
            //        { 26, 2, new Guid("27c60b74-05f3-4958-80ab-f14c8ed3a728"), 7 },
            //        { 25, 1, new Guid("8d7d53f7-a0aa-4926-a547-480534cc648e"), 7 },
            //        { 24, 4, new Guid("cd20de48-8aa2-41b9-bbf1-1f0f82d823d5"), 6 },
            //        { 23, 3, new Guid("830f5aa6-8bc7-4342-a456-506af7447441"), 6 },
            //        { 22, 2, new Guid("c7bc26b8-c81c-4f7e-80f2-7655408bfce2"), 6 },
            //        { 21, 1, new Guid("e130d200-8883-4b25-a228-4bf623c53662"), 6 },
            //        { 20, 4, new Guid("78113d7a-646a-45b1-94ee-326a9c2fa09d"), 5 },
            //        { 19, 3, new Guid("beea2c7b-55e6-4210-a9b0-2b23ea9f816b"), 5 },
            //        { 18, 2, new Guid("4e57ff27-ee76-4de6-b9d3-b488a6426c70"), 5 }
            //    });

            //migrationBuilder.InsertData(
            //    schema: "Permission",
            //    table: "PermissionControllerAction",
            //    columns: new[] { "PermissionControllerActionID", "PermissionActionID", "PermissionControllerActionGuid", "PermissionControllerID" },
            //    values: new object[,]
            //    {
            //        { 32, 4, new Guid("6bd39973-154d-48e2-92c8-add87a9d9e5d"), 8 },
            //        { 17, 1, new Guid("1900ff7b-3cd7-47c9-9bba-91fadf8d5f32"), 5 },
            //        { 15, 3, new Guid("b8c59a85-582a-4987-9636-36a31dd147ab"), 4 },
            //        { 14, 2, new Guid("53681a57-bd55-47dc-a1d9-bfebb23333c2"), 4 },
            //        { 13, 1, new Guid("77f82790-3365-4b69-b4f7-f567b287149d"), 4 },
            //        { 12, 4, new Guid("3439b985-9ccb-45e6-9e98-2652da0d8510"), 3 },
            //        { 11, 3, new Guid("b1e69561-3752-4141-a0dd-f61bb5011ea3"), 3 },
            //        { 10, 2, new Guid("ef72caca-8a17-4b4d-b3b5-c2e2cf5be7d0"), 3 },
            //        { 9, 1, new Guid("ef9a044f-f199-484a-9a26-a95ef8e50278"), 3 },
            //        { 8, 4, new Guid("1e54ef2d-5a59-433e-acef-987715ef6ab2"), 2 },
            //        { 7, 3, new Guid("862f8fd2-9dfd-43bb-b60a-1811b4869194"), 2 },
            //        { 6, 2, new Guid("00b40b39-b45d-4074-868a-60f92c65dab0"), 2 },
            //        { 5, 1, new Guid("fa528e89-78eb-4ce0-b848-d73d35ccf33e"), 2 },
            //        { 4, 4, new Guid("880ac5af-5899-44d9-8a4d-b643dc1facb4"), 1 },
            //        { 3, 3, new Guid("e55733bc-86a2-4fa3-ad00-b50b3e77dd1f"), 1 },
            //        { 2, 2, new Guid("0bd98c10-3094-4594-ac08-6af53de9a495"), 1 },
            //        { 16, 4, new Guid("54f83b7d-893a-4ddd-b341-afba4f54a915"), 4 },
            //        { 68, 4, new Guid("27474a87-d511-4438-a983-aec8daf83c63"), 17 },
            //        { 34, 2, new Guid("fcab098b-b703-4acd-b935-e2cefab9b77f"), 9 },
            //        { 52, 4, new Guid("a070284d-9937-466a-ae08-c761d63061da"), 13 },
            //        { 65, 1, new Guid("48c14dd9-9c66-419a-abbd-9f2363522a7c"), 17 },
            //        { 64, 4, new Guid("166d411b-7913-4272-9e23-224bc2fdde01"), 16 },
            //        { 63, 3, new Guid("7449f62d-82da-4a22-8535-f777dcb43148"), 16 },
            //        { 62, 2, new Guid("787d739c-208a-48a5-abc7-c971f6e6f764"), 16 },
            //        { 61, 1, new Guid("8178edc6-2a2f-4a4b-b9a7-486bbcdf50f7"), 16 },
            //        { 60, 4, new Guid("bfbb6f35-b1e4-48b1-8b1a-9c6e7251b916"), 15 },
            //        { 59, 3, new Guid("0fd3e333-676f-4d65-aac5-987f6f1ac11d"), 15 },
            //        { 58, 2, new Guid("9a1e9897-4306-47bc-86cc-4f36ae4e3cfd"), 15 },
            //        { 57, 1, new Guid("b30d6ff6-d616-4822-b205-933cd42fd7e7"), 15 },
            //        { 56, 4, new Guid("efb2f1b5-7ebf-4bfa-8459-87075563fb63"), 14 },
            //        { 55, 3, new Guid("034cbadf-51ef-4610-b551-e95e944be733"), 14 },
            //        { 54, 2, new Guid("5e543754-5e4b-4e32-8fe3-1fc74d2bbfb6"), 14 },
            //        { 53, 1, new Guid("cd01d532-3716-4225-a6fc-51492d268b72"), 14 },
            //        { 35, 3, new Guid("b9336576-8680-4753-8931-f5fcac6c8b1c"), 9 },
            //        { 66, 2, new Guid("48677a45-6a5d-4b45-931a-33b4c582d0b7"), 17 },
            //        { 51, 3, new Guid("416d3f08-06fc-40a9-a8df-b6c373bfb8b2"), 13 },
            //        { 49, 1, new Guid("da57e645-0ac4-45c8-9db3-6e51547ddb32"), 13 },
            //        { 48, 4, new Guid("b15e5f5e-b1ec-41d1-be13-b7799e73faed"), 12 },
            //        { 47, 3, new Guid("113a1e1f-975c-4cd3-a563-99378928d588"), 12 },
            //        { 46, 2, new Guid("6540e085-ed89-4e6e-a871-b1c262e81d28"), 12 },
            //        { 45, 1, new Guid("2f0de6d5-ca44-49e5-8d6d-f51df626aeaa"), 12 },
            //        { 44, 4, new Guid("272f1dc4-ca91-4c29-8958-d4b5d03bfb4c"), 11 }
            //    });

            //migrationBuilder.InsertData(
            //    schema: "Permission",
            //    table: "PermissionControllerAction",
            //    columns: new[] { "PermissionControllerActionID", "PermissionActionID", "PermissionControllerActionGuid", "PermissionControllerID" },
            //    values: new object[,]
            //    {
            //        { 43, 3, new Guid("614f8c8e-3762-499b-bb02-6b1db0deb473"), 11 },
            //        { 42, 2, new Guid("00e2e4a5-a5ab-4283-ab59-6c2aa25c3c6b"), 11 },
            //        { 41, 1, new Guid("4d64d8f0-30ae-4d6f-a864-114008c731b1"), 11 },
            //        { 40, 4, new Guid("3dc3a350-d5ae-4788-9869-d77bbfef8cdc"), 10 },
            //        { 39, 3, new Guid("4937053f-43ec-419c-b22b-15ac8ca5ad1b"), 10 },
            //        { 38, 2, new Guid("65010c13-a5ed-4df7-9730-9d97e190516d"), 10 },
            //        { 37, 1, new Guid("d172c120-540a-43a5-935a-ed20a064df2f"), 10 },
            //        { 36, 4, new Guid("6b6dd681-57bc-41bd-bdd1-cd4177386070"), 9 },
            //        { 50, 2, new Guid("3153f603-c998-4146-9180-cd4df779306a"), 13 },
            //        { 67, 3, new Guid("5b042190-54f6-4ffd-9b3e-eae049a3495d"), 17 }
            //    });

            //migrationBuilder.InsertData(
            //    schema: "Setting",
            //    table: "Country",
            //    columns: new[] { "CountryID", "CountryGuid", "CreateDate", "DeleteDate", "EnableDate", "Extension", "Flag", "IsDeleted", "IsEnable", "Lat", "Long", "NameAR", "NameEN", "Place", "UpdateDate", "UserDelete", "UserEnable", "UserId", "UserUpdate", "Zoom" },
            //    values: new object[] { 1, new Guid("9939d711-0f7e-4446-9671-7005b9bb5c55"), new DateTime(2021, 9, 20, 14, 45, 13, 200, DateTimeKind.Local).AddTicks(363), null, new DateTime(2021, 9, 20, 14, 45, 13, 201, DateTimeKind.Local).AddTicks(4794), "00966", null, false, true, "", "", "السعودية", "SA", "", null, null, null, 1, null, null });

            //migrationBuilder.InsertData(
            //    schema: "Setting",
            //    table: "Jobs",
            //    columns: new[] { "JobsID", "CreateDate", "DeleteDate", "DescriptionAr", "DescriptionEn", "EnableDate", "Image", "IsDeleted", "IsEnable", "JobTypeId", "JobsGuid", "NameAR", "NameEN", "UpdateDate", "UserDelete", "UserEnable", "UserId", "UserUpdate" },
            //    values: new object[] { 1, new DateTime(2021, 9, 20, 14, 45, 13, 205, DateTimeKind.Local).AddTicks(4276), null, null, null, new DateTime(2021, 9, 20, 14, 45, 13, 205, DateTimeKind.Local).AddTicks(5947), null, false, true, 2, new Guid("30963611-f2a4-419d-921f-8a7a0d9ff650"), "الدمام", "DMM", null, null, null, 1, null });

            //migrationBuilder.InsertData(
            //    schema: "Setting",
            //    table: "MainCategory",
            //    columns: new[] { "MainCategoryID", "CreateDate", "DeleteDate", "EnableDate", "IsDeleted", "IsEnable", "MainCategoryGuid", "NameAR", "NameEN", "UpdateDate", "UserDelete", "UserEnable", "UserId", "UserUpdate" },
            //    values: new object[] { 1, new DateTime(2021, 9, 20, 14, 45, 13, 206, DateTimeKind.Local).AddTicks(1843), null, new DateTime(2021, 9, 20, 14, 45, 13, 206, DateTimeKind.Local).AddTicks(2923), false, true, new Guid("747876bd-7291-4f52-a7dc-70263c78c599"), "الدمام", "DMM", null, null, null, 1, null });

            //migrationBuilder.InsertData(
            //    schema: "Setting",
            //    table: "Nationality",
            //    columns: new[] { "NationalityID", "CreateDate", "DeleteDate", "DescriptionAr", "DescriptionEn", "EnableDate", "IsDeleted", "IsEnable", "NameAR", "NameEN", "NationalityGuid", "UpdateDate", "UserDelete", "UserEnable", "UserId", "UserUpdate" },
            //    values: new object[] { 1, new DateTime(2021, 9, 20, 14, 45, 13, 204, DateTimeKind.Local).AddTicks(7921), null, null, null, new DateTime(2021, 9, 20, 14, 45, 13, 204, DateTimeKind.Local).AddTicks(9012), false, true, "الدمام", "DMM", new Guid("0d61510a-0ec3-4705-b962-e6bbe6ae65cf"), null, null, null, 1, null });

            //migrationBuilder.InsertData(
            //    schema: "Permission",
            //    table: "RoleConfig",
            //    columns: new[] { "RoleConfigID", "PermissionControllerActionID", "RoleId" },
            //    values: new object[,]
            //    {
            //        { 1, 1, 1 },
            //        { 98, 98, 1 },
            //        { 97, 97, 1 },
            //        { 96, 96, 1 },
            //        { 95, 95, 1 },
            //        { 94, 94, 1 },
            //        { 93, 93, 1 },
            //        { 92, 92, 1 },
            //        { 91, 91, 1 },
            //        { 90, 90, 1 },
            //        { 89, 89, 1 },
            //        { 88, 88, 1 },
            //        { 87, 87, 1 },
            //        { 86, 86, 1 },
            //        { 85, 85, 1 },
            //        { 84, 84, 1 },
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
            //        { 71, 71, 1 },
            //        { 70, 70, 1 },
            //        { 99, 99, 1 },
            //        { 100, 100, 1 },
            //        { 101, 101, 1 },
            //        { 102, 102, 1 },
            //        { 132, 132, 1 },
            //        { 131, 131, 1 },
            //        { 130, 130, 1 },
            //        { 129, 129, 1 },
            //        { 128, 128, 2 },
            //        { 127, 127, 2 },
            //        { 126, 126, 2 },
            //        { 125, 125, 2 }
            //    });

            //migrationBuilder.InsertData(
            //    schema: "Permission",
            //    table: "RoleConfig",
            //    columns: new[] { "RoleConfigID", "PermissionControllerActionID", "RoleId" },
            //    values: new object[,]
            //    {
            //        { 124, 124, 1 },
            //        { 123, 123, 1 },
            //        { 122, 122, 1 },
            //        { 121, 121, 1 },
            //        { 120, 120, 1 },
            //        { 119, 119, 1 },
            //        { 69, 69, 1 },
            //        { 118, 118, 1 },
            //        { 116, 116, 1 },
            //        { 115, 115, 1 },
            //        { 114, 114, 1 },
            //        { 113, 113, 1 },
            //        { 112, 112, 1 },
            //        { 111, 111, 1 },
            //        { 110, 110, 1 },
            //        { 109, 109, 1 },
            //        { 108, 108, 2 },
            //        { 107, 107, 2 },
            //        { 106, 106, 2 },
            //        { 105, 105, 2 },
            //        { 104, 104, 1 },
            //        { 103, 103, 1 },
            //        { 117, 117, 1 },
            //        { 68, 68, 1 },
            //        { 67, 67, 1 },
            //        { 66, 66, 1 },
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
            //        { 18, 18, 1 },
            //        { 17, 17, 1 },
            //        { 16, 16, 1 },
            //        { 15, 15, 1 }
            //    });

            //migrationBuilder.InsertData(
            //    schema: "Permission",
            //    table: "RoleConfig",
            //    columns: new[] { "RoleConfigID", "PermissionControllerActionID", "RoleId" },
            //    values: new object[,]
            //    {
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
            //        { 31, 31, 1 },
            //        { 32, 32, 1 },
            //        { 33, 33, 1 },
            //        { 50, 50, 1 },
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
            //        { 52, 52, 1 },
            //        { 51, 51, 1 },
            //        { 34, 34, 1 },
            //        { 49, 49, 1 },
            //        { 48, 48, 1 },
            //        { 47, 47, 1 },
            //        { 46, 46, 1 },
            //        { 45, 45, 1 },
            //        { 44, 44, 1 },
            //        { 43, 43, 1 },
            //        { 42, 42, 1 },
            //        { 41, 41, 1 },
            //        { 40, 40, 1 },
            //        { 39, 39, 1 }
            //    });

            //migrationBuilder.InsertData(
            //    schema: "Permission",
            //    table: "RoleConfig",
            //    columns: new[] { "RoleConfigID", "PermissionControllerActionID", "RoleId" },
            //    values: new object[,]
            //    {
            //        { 38, 38, 1 },
            //        { 37, 37, 1 },
            //        { 36, 36, 1 },
            //        { 35, 35, 1 },
            //        { 64, 64, 1 },
            //        { 65, 65, 1 }
            //    });

            //migrationBuilder.InsertData(
            //    schema: "Setting",
            //    table: "Departments",
            //    columns: new[] { "DepartmentsID", "Arrange", "CreateDate", "DeleteDate", "DepartmentsGuid", "DescriptionAr", "DescriptionEn", "EnableDate", "Image", "IsDeleted", "IsEnable", "Isunique", "MainCategoryID", "NameAR", "NameEN", "SiteImage", "UpdateDate", "UserDelete", "UserEnable", "UserId", "UserUpdate" },
            //    values: new object[] { 1, null, new DateTime(2021, 9, 20, 14, 45, 13, 206, DateTimeKind.Local).AddTicks(9480), null, new Guid("a442f120-1b3c-4c3c-835a-0298fd8e35c6"), null, null, new DateTime(2021, 9, 20, 14, 45, 13, 207, DateTimeKind.Local).AddTicks(559), null, false, true, false, 1, "الدمام", "DMM", null, null, null, null, 1, null });

            //migrationBuilder.InsertData(
            //    schema: "Setting",
            //    table: "Region",
            //    columns: new[] { "RegionID", "CountryID", "CreateDate", "DeleteDate", "EnableDate", "IsDeleted", "IsEnable", "Lat", "Long", "NameAR", "NameEN", "Place", "RegionGuid", "UpdateDate", "UserDelete", "UserEnable", "UserId", "UserUpdate", "Zoom" },
            //    values: new object[] { 1, 1, new DateTime(2021, 9, 20, 14, 45, 13, 202, DateTimeKind.Local).AddTicks(5086), null, new DateTime(2021, 9, 20, 14, 45, 13, 202, DateTimeKind.Local).AddTicks(6189), false, true, "", "", "الدمام", "DMM", "", new Guid("0a4b8cd0-a37c-47db-aa93-88e154c194a0"), null, null, null, 1, null, null });

            //migrationBuilder.InsertData(
            //    schema: "Setting",
            //    table: "City",
            //    columns: new[] { "CityID", "CityGuid", "Code", "CreateDate", "DeleteDate", "EnableDate", "IsDeleted", "IsEnable", "Lat", "Long", "NameAR", "NameEN", "Place", "RegionID", "UpdateDate", "UserDelete", "UserEnable", "UserId", "UserUpdate", "Zoom" },
            //    values: new object[] { 1, new Guid("a71cd6ea-4fb6-4736-acf7-1d9ee991e75f"), null, new DateTime(2021, 9, 20, 14, 45, 13, 203, DateTimeKind.Local).AddTicks(8488), null, new DateTime(2021, 9, 20, 14, 45, 13, 203, DateTimeKind.Local).AddTicks(9546), false, true, "", "", "الدمام", "DMM", "", 1, null, null, null, 1, null, null });

            //migrationBuilder.InsertData(
            //    schema: "Emp",
            //    table: "Employees",
            //    columns: new[] { "EntityEmpID", "AddressAR", "AddressEN", "BirthDateHijri", "BirthDateMilady", "BloodTypeId", "CityID", "CreateDate", "DeleteDate", "Email", "EnableDate", "EntityEmpGuid", "FileNo", "FirstNameAR", "FirstNameEN", "Gender", "IDNo", "IsDeleted", "IsEnable", "JobsID", "LastNameAR", "LastNameEN", "Lat", "Lng", "MidNameAR", "MidNameEN", "MobileNo", "NationalityID", "Notes", "PhoneNumber", "Photo", "UpdateDate", "UserDelete", "UserEnable", "UserId", "UserUpdate", "Zoom" },
            //    values: new object[] { 1, "الاسماعيليه", "ismailia", "", null, 1, 1, new DateTime(2021, 9, 20, 14, 45, 13, 208, DateTimeKind.Local).AddTicks(3311), null, "SystemUser@Admin.com", new DateTime(2021, 9, 20, 14, 45, 13, 208, DateTimeKind.Local).AddTicks(3934), new Guid("2299447c-fc61-4aa4-ba03-8c91e4f4b2d5"), "123321", "احمد", "Ahmed", 1, "", false, true, 1, "حسين", "Hussien", "", "", "سيد", "Sayed", "0595489633", 1, "", "", "", null, null, 1, 1, null, "" });

            migrationBuilder.CreateIndex(
                name: "IX_VendorPromo_PromoCodeID",
                schema: "Order",
                table: "VendorPromo",
                column: "PromoCodeID");

            migrationBuilder.CreateIndex(
                name: "IX_VendorPromo_UserId",
                schema: "Order",
                table: "VendorPromo",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorPromo_VendorsID",
                schema: "Order",
                table: "VendorPromo",
                column: "VendorsID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VendorPromo",
                schema: "Order");

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
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 34);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 11);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 12);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 13);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 14);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 15);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 16);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 17);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 18);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 19);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 20);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 21);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 22);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 23);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 24);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 25);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 26);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 27);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 28);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 29);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 30);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 31);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 32);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 33);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 34);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 35);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 36);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 37);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 38);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 39);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 40);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 41);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 42);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 43);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 44);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 45);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 46);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 47);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 48);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 49);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 50);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 51);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 52);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 53);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 54);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 55);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 56);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 57);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 58);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 59);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 60);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 61);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 62);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 63);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 64);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 65);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 66);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 67);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 68);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 69);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 70);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 71);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 72);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 73);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 74);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 75);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 76);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 77);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 78);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 79);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 80);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 81);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 82);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 83);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 84);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 85);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 86);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 87);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 88);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 89);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 90);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 91);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 92);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 93);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 94);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 95);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 96);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 97);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 98);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 99);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 100);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 101);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 102);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 103);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 104);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 105);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 106);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 107);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 108);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 109);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 110);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 111);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 112);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 113);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 114);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 115);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 116);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 117);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 118);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 119);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 120);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 121);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 122);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 123);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 124);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 125);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 126);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 127);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 128);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 129);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 130);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 131);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 132);

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
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 11);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 12);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 13);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 14);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 15);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 16);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 17);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 18);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 19);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 20);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 21);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 22);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 23);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 24);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 25);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 26);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 27);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 28);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 29);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 30);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 31);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 32);

            migrationBuilder.DeleteData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 33);

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
        }
    }
}
