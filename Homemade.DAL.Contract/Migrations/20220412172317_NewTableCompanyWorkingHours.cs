using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Homemade.DAL.Contract.Migrations
{
    public partial class NewTableCompanyWorkingHours : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FacebookLink",
                schema: "Setting",
                table: "Configuration",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InstagramLink",
                schema: "Setting",
                table: "Configuration",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SnapchatLink",
                schema: "Setting",
                table: "Configuration",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TwitterLink",
                schema: "Setting",
                table: "Configuration",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CompanyWorkingHours",
                schema: "Setting",
                columns: table => new
                {
                    CompanyWorkingHoursID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyWorkingHoursGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DaysWork = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstShiftWorkFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FirstShiftWorkTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SecoundShiftWorkFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SecoundShiftWorkTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DaysVac = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstShiftVacWorkFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FirstShiftVacWorkTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SecoundShiftVacWorkFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SecoundShiftVacWorkTo = table.Column<DateTime>(type: "datetime2", nullable: true),
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
                    table.PrimaryKey("PK_CompanyWorkingHours", x => x.CompanyWorkingHoursID);
                    table.ForeignKey(
                        name: "FK_CompanyWorkingHours_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "a31df437-b9d1-48fe-ac89-d3c42c8da11a");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "659fb091-c237-4d4d-bc23-f0ebd0d0197c");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "f0096bf1-5ed1-4699-8741-3983afde3a07");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "a4587aa7-1ca8-402a-b706-23080c667765");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "8e890c49-2e4d-4cf3-9cdb-02074ee7145d", "21ab13b7-02ee-4047-ac30-f1501171612d" });

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "DeliverySetting",
                keyColumn: "DeliverySettingID",
                keyValue: 1,
                column: "DeliverySettingGuid",
                value: new Guid("ba8edbfb-c517-4887-9e04-34226754ac17"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 1,
                column: "TransactionTypeGuid",
                value: new Guid("51141f8d-377a-485d-9e97-476deeb6bca6"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 2,
                column: "TransactionTypeGuid",
                value: new Guid("0e224e51-dec1-495b-ab98-47bb479c1cc1"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 3,
                column: "TransactionTypeGuid",
                value: new Guid("638ca9df-105a-47a9-8301-447a231a65c2"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 4,
                column: "TransactionTypeGuid",
                value: new Guid("79c1abbf-9714-41d2-ad0f-66b9aed2f38a"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 5,
                column: "TransactionTypeGuid",
                value: new Guid("6adeaf5a-caaa-4042-8432-0ce148f43a59"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 6,
                column: "TransactionTypeGuid",
                value: new Guid("6b7d3f57-e03a-4ddf-90ec-520ab5000dd0"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 7,
                column: "TransactionTypeGuid",
                value: new Guid("df46a51a-44de-47ab-a8d6-a1655704234b"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 8,
                column: "TransactionTypeGuid",
                value: new Guid("44968799-d25c-439c-91f7-577cd2eee930"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 9,
                column: "TransactionTypeGuid",
                value: new Guid("11435502-c31a-4f5b-aa6c-f32510d4ad67"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 10,
                column: "TransactionTypeGuid",
                value: new Guid("25688dd4-5903-41d5-add9-1959818ea20f"));

            migrationBuilder.UpdateData(
                schema: "Emp",
                table: "Employees",
                keyColumn: "EntityEmpID",
                keyValue: 1,
                columns: new[] { "CreateDate", "EnableDate" },
                values: new object[] { new DateTime(2022, 4, 12, 19, 23, 13, 973, DateTimeKind.Local).AddTicks(2431), new DateTime(2022, 4, 12, 19, 23, 13, 973, DateTimeKind.Local).AddTicks(2767) });

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionAction",
                keyColumn: "PermissionActionID",
                keyValue: 1,
                column: "PermissionActionGuid",
                value: new Guid("91edbfe3-a08e-479f-98a7-0304f3ba1e09"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionAction",
                keyColumn: "PermissionActionID",
                keyValue: 2,
                column: "PermissionActionGuid",
                value: new Guid("b0ed8ae6-105b-4af9-8e4d-4913eb2f73e3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionAction",
                keyColumn: "PermissionActionID",
                keyValue: 3,
                column: "PermissionActionGuid",
                value: new Guid("8fc949fc-bb51-4ad3-b3a9-9acbb53c78cc"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionAction",
                keyColumn: "PermissionActionID",
                keyValue: 4,
                column: "PermissionActionGuid",
                value: new Guid("aa6d9f9b-a2d8-4a13-986c-e9d613ceb3f0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 1,
                column: "PermissionControllerGuid",
                value: new Guid("08b31f0a-d20a-4f73-a308-dbbf9c0727b0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 2,
                column: "PermissionControllerGuid",
                value: new Guid("7463e3ac-3a2c-4bec-940b-54faa5a4d47c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 3,
                column: "PermissionControllerGuid",
                value: new Guid("71fda00a-1435-4d64-94ad-36c2c1fe6f52"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 4,
                column: "PermissionControllerGuid",
                value: new Guid("df21a301-732f-4c1c-9ed5-196dce6ca645"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 5,
                column: "PermissionControllerGuid",
                value: new Guid("e7593738-68b2-4ff9-be55-0728f4697a1f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 6,
                column: "PermissionControllerGuid",
                value: new Guid("3e2d96b9-a94d-4dad-b26b-c0971afdbd0c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 7,
                column: "PermissionControllerGuid",
                value: new Guid("e7929e71-0543-4ccb-bbdb-304d5f75312e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 8,
                column: "PermissionControllerGuid",
                value: new Guid("f5e06d1c-be5a-4ad3-8ade-560fb3ef2e16"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 9,
                column: "PermissionControllerGuid",
                value: new Guid("708e03ec-cd7b-46fd-910e-95023381004b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 10,
                column: "PermissionControllerGuid",
                value: new Guid("c41fac4d-d986-4b8a-b990-5a9de0039fa5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 11,
                column: "PermissionControllerGuid",
                value: new Guid("fcda6cae-b59e-4505-bf95-2f1023b5c836"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 12,
                column: "PermissionControllerGuid",
                value: new Guid("84a2185c-fccd-4cb4-90c0-b5a7b90cca91"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 13,
                column: "PermissionControllerGuid",
                value: new Guid("b008e62f-c2ed-4aed-b5d8-a58ab960bf69"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 14,
                column: "PermissionControllerGuid",
                value: new Guid("f87418fd-85e6-482f-8ce6-4e1a05b5e283"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 15,
                column: "PermissionControllerGuid",
                value: new Guid("6ceffbf0-f133-4660-87ac-811d2e97cd41"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 16,
                column: "PermissionControllerGuid",
                value: new Guid("8e831b4f-882c-41e4-a1ae-8688a2b35e50"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 17,
                column: "PermissionControllerGuid",
                value: new Guid("6b235a39-617c-47e6-a7be-a9a74e203830"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 18,
                column: "PermissionControllerGuid",
                value: new Guid("07ef4fd4-6fc4-4ac7-9a0d-147d0f1c6044"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 19,
                column: "PermissionControllerGuid",
                value: new Guid("a16eb071-75ed-4542-b4fe-cf7e91db06df"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 20,
                column: "PermissionControllerGuid",
                value: new Guid("56ad4a2d-7121-4002-81f5-9b55c8e481ce"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 21,
                column: "PermissionControllerGuid",
                value: new Guid("24863696-017e-4aa5-a62e-20068cdffdf2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 22,
                column: "PermissionControllerGuid",
                value: new Guid("46cb20cc-2867-4054-b077-6a78f682755f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 23,
                column: "PermissionControllerGuid",
                value: new Guid("e2ae972b-98f3-4f61-957e-97dc4b62a498"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 24,
                column: "PermissionControllerGuid",
                value: new Guid("cce01434-c3c5-41fd-a38e-bc86e97d3f05"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 25,
                column: "PermissionControllerGuid",
                value: new Guid("82d46201-63b9-433e-9024-f3ff3160196e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 26,
                column: "PermissionControllerGuid",
                value: new Guid("16d33cd0-db7d-40f1-bf3a-0cd74340f19c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 27,
                column: "PermissionControllerGuid",
                value: new Guid("90a5e365-ba49-49ce-a0a7-bc6043098f4b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 28,
                column: "PermissionControllerGuid",
                value: new Guid("7dc0817f-e2d6-432a-a30d-c79e182e5f53"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 29,
                column: "PermissionControllerGuid",
                value: new Guid("092da78a-c14f-4643-b942-108ade81a742"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 30,
                column: "PermissionControllerGuid",
                value: new Guid("8b202d2d-0e3b-4c0c-bfb7-d05a7a892157"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 31,
                column: "PermissionControllerGuid",
                value: new Guid("5453d7c2-f710-4212-b276-993a91432177"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 32,
                column: "PermissionControllerGuid",
                value: new Guid("44c4ff5a-0ffb-4265-8929-b2874ef0f07e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 33,
                column: "PermissionControllerGuid",
                value: new Guid("6a054182-6a6b-4c4a-845b-ced37c6b1b71"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 34,
                column: "PermissionControllerGuid",
                value: new Guid("62496360-33ba-4007-8075-94f23ddd68fb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 35,
                column: "PermissionControllerGuid",
                value: new Guid("f5f9a0b2-af9a-4028-9008-b2eef471ef31"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 36,
                column: "PermissionControllerGuid",
                value: new Guid("e4d1abb6-b718-44ee-b640-0f161e98d455"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 37,
                column: "PermissionControllerGuid",
                value: new Guid("0969b8b1-bb0c-4fb6-8a24-0c4d2194e563"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 38,
                column: "PermissionControllerGuid",
                value: new Guid("ea46bb8f-a95f-4b5d-b8d7-4e59b3393c23"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 39,
                column: "PermissionControllerGuid",
                value: new Guid("5a8295f1-82b6-4fe0-b14a-da78f96d84a0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 40,
                column: "PermissionControllerGuid",
                value: new Guid("b81fbd10-7dee-420e-a41a-6f60d6f321df"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 41,
                column: "PermissionControllerGuid",
                value: new Guid("3ac46ace-84d2-4779-8707-dc1ce2eeb5c2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 42,
                column: "PermissionControllerGuid",
                value: new Guid("278565bd-ec92-4cdf-9c39-cbe1eb37a60d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 43,
                column: "PermissionControllerGuid",
                value: new Guid("3b3c191f-05c8-4293-9e95-e3dc991012c9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 44,
                column: "PermissionControllerGuid",
                value: new Guid("f9897254-2a2f-4a24-a288-9e022cf57f9a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 45,
                column: "PermissionControllerGuid",
                value: new Guid("0e75b10e-2e1a-4232-b745-d3037b0800f4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 46,
                column: "PermissionControllerGuid",
                value: new Guid("0960e0af-39f8-4743-bbcd-08a558dffe88"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 47,
                column: "PermissionControllerGuid",
                value: new Guid("52746549-db7f-48f8-afc8-f176338b9683"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 48,
                column: "PermissionControllerGuid",
                value: new Guid("ca06a749-e70e-4c34-801e-6a4431ec804b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 49,
                column: "PermissionControllerGuid",
                value: new Guid("f5533589-cb7a-4af4-91d5-16ce022ee34d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 50,
                column: "PermissionControllerGuid",
                value: new Guid("b26b57fa-8c64-4fc1-bae4-6cfe35f5547a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 51,
                column: "PermissionControllerGuid",
                value: new Guid("f4c63eec-f572-438e-a0c8-e778cd235105"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 52,
                column: "PermissionControllerGuid",
                value: new Guid("43c62fd8-316c-4b47-80ff-23a43d6eacbe"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 53,
                column: "PermissionControllerGuid",
                value: new Guid("940e42dd-7253-4e08-91af-3c0257fa62bd"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 54,
                column: "PermissionControllerGuid",
                value: new Guid("3c17f796-c3fe-45ad-9cf0-96b3d3267be0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 55,
                column: "PermissionControllerGuid",
                value: new Guid("da8353ba-4903-433e-9a77-681834591de4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 56,
                column: "PermissionControllerGuid",
                value: new Guid("12cf39ca-494f-4c67-8f07-6b47e631dd20"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 57,
                column: "PermissionControllerGuid",
                value: new Guid("d3c827d2-0715-4980-8d23-e9498ac33260"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 58,
                column: "PermissionControllerGuid",
                value: new Guid("220baf04-280a-4ba1-81c3-fc53c750d8d8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 59,
                column: "PermissionControllerGuid",
                value: new Guid("80ef3d98-417e-4ffa-81ba-8bc2c4bed02e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 60,
                column: "PermissionControllerGuid",
                value: new Guid("34bcaff2-405b-4fc8-ae3e-e6fea17ca576"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 61,
                column: "PermissionControllerGuid",
                value: new Guid("d5be06b0-b45e-4572-ad64-48f19e5e1009"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 1,
                column: "PermissionControllerActionGuid",
                value: new Guid("3bf02cad-aa35-404b-b640-b137c774e9d4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 2,
                column: "PermissionControllerActionGuid",
                value: new Guid("2612dc04-a087-4034-bdbd-cddbefbc4228"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 3,
                column: "PermissionControllerActionGuid",
                value: new Guid("eb5a39dc-1593-424c-b143-de871f38568b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 4,
                column: "PermissionControllerActionGuid",
                value: new Guid("62bfa7c7-aa4e-4495-9577-ccc657c4edcf"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 5,
                column: "PermissionControllerActionGuid",
                value: new Guid("6983b750-1c35-417a-84ae-de7df7a3ed1d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 6,
                column: "PermissionControllerActionGuid",
                value: new Guid("f7613c7f-7310-444f-a3d7-932cfa820307"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 7,
                column: "PermissionControllerActionGuid",
                value: new Guid("6b5ebb11-196f-4722-b0ae-a9071abfcb5b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 8,
                column: "PermissionControllerActionGuid",
                value: new Guid("96cdca17-3983-44ff-a4af-162be482e601"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 9,
                column: "PermissionControllerActionGuid",
                value: new Guid("77e387b8-ca28-4c1e-a576-14a3fa2904be"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 10,
                column: "PermissionControllerActionGuid",
                value: new Guid("861d433f-df58-42f6-a558-dd3dd536b411"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 11,
                column: "PermissionControllerActionGuid",
                value: new Guid("46bec07b-f8fc-4ff7-9fff-2480126cd8ff"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 12,
                column: "PermissionControllerActionGuid",
                value: new Guid("ef4633c6-5ecd-4546-ba30-36dc58f25c33"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 13,
                column: "PermissionControllerActionGuid",
                value: new Guid("96a836ef-915c-4fdd-b9de-b5cab7e0fc10"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 14,
                column: "PermissionControllerActionGuid",
                value: new Guid("677ce904-6433-481f-83e8-48e316a6342b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 15,
                column: "PermissionControllerActionGuid",
                value: new Guid("63139304-088f-4360-aacd-8dfa574f74e8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 16,
                column: "PermissionControllerActionGuid",
                value: new Guid("2ccd4520-56c2-46ce-a914-fc6190ab3d49"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 17,
                column: "PermissionControllerActionGuid",
                value: new Guid("273a8dab-0df2-46fd-af1a-8b433a2fb173"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 18,
                column: "PermissionControllerActionGuid",
                value: new Guid("19cdba6a-0245-4b1b-9b84-e87d1e05817b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 19,
                column: "PermissionControllerActionGuid",
                value: new Guid("4e76af1d-3b1b-4c95-9718-c8fecba68239"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 20,
                column: "PermissionControllerActionGuid",
                value: new Guid("b715a786-4802-454a-9b4a-2777d65366c7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 21,
                column: "PermissionControllerActionGuid",
                value: new Guid("2075f4b3-7528-4543-8622-0019d04ee655"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 22,
                column: "PermissionControllerActionGuid",
                value: new Guid("27ec4c1c-0cf9-489b-9d2c-cee69d64f945"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 23,
                column: "PermissionControllerActionGuid",
                value: new Guid("5a112e92-2e97-43f5-aa8c-79a226b1c1f4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 24,
                column: "PermissionControllerActionGuid",
                value: new Guid("6c2d3915-3969-4abc-8dbc-b867f85124db"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 25,
                column: "PermissionControllerActionGuid",
                value: new Guid("b72b2a13-ccff-4183-b672-9db213c76251"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 26,
                column: "PermissionControllerActionGuid",
                value: new Guid("dc12f841-f060-443c-9793-9b2b603f9fbe"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 27,
                column: "PermissionControllerActionGuid",
                value: new Guid("1092c6b2-562c-4b53-9739-a6fa567c410d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 28,
                column: "PermissionControllerActionGuid",
                value: new Guid("6834b5ca-f405-4da9-a83a-f9657da254f6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 29,
                column: "PermissionControllerActionGuid",
                value: new Guid("40369cde-6f1c-4975-9091-cf37f16b235c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 30,
                column: "PermissionControllerActionGuid",
                value: new Guid("16e5e025-e613-4264-ae47-f26c5a1a96b1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 31,
                column: "PermissionControllerActionGuid",
                value: new Guid("17814d4e-217f-4091-9e4b-e000474de7cb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 32,
                column: "PermissionControllerActionGuid",
                value: new Guid("368c0567-2c56-499c-b7dc-71bebf9657b3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 33,
                column: "PermissionControllerActionGuid",
                value: new Guid("97074331-cc98-4bd6-aebb-ad42459390d8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 34,
                column: "PermissionControllerActionGuid",
                value: new Guid("9a9b8340-1603-4fbb-8416-8fdecf7aab23"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 35,
                column: "PermissionControllerActionGuid",
                value: new Guid("dc0286ca-cd1f-4c80-bf27-62838b29ab01"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 36,
                column: "PermissionControllerActionGuid",
                value: new Guid("316a38b4-fdde-414c-b129-a0daf5633919"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 37,
                column: "PermissionControllerActionGuid",
                value: new Guid("af45d956-1720-4129-a0b2-19919daa7a0a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 38,
                column: "PermissionControllerActionGuid",
                value: new Guid("e80a1afe-28b6-4bb8-9ee1-d8d482b47705"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 39,
                column: "PermissionControllerActionGuid",
                value: new Guid("b3c1ef66-8d3c-4adb-b920-0cb24664413b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 40,
                column: "PermissionControllerActionGuid",
                value: new Guid("77c49dc0-5bec-41f3-bd94-837339180d86"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 41,
                column: "PermissionControllerActionGuid",
                value: new Guid("da087b61-9b0a-48fe-b69a-463708c26368"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 42,
                column: "PermissionControllerActionGuid",
                value: new Guid("c51fb2c1-5d4a-4371-8630-610e1700ef9b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 43,
                column: "PermissionControllerActionGuid",
                value: new Guid("4ef02e03-0056-4710-a1e7-53c0fbc26936"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 44,
                column: "PermissionControllerActionGuid",
                value: new Guid("9ab08795-af9e-4570-a935-c4d8c228a389"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 45,
                column: "PermissionControllerActionGuid",
                value: new Guid("4ed5a9cf-d991-435c-b8ab-b1658f57a912"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 46,
                column: "PermissionControllerActionGuid",
                value: new Guid("2ce6a952-715d-4c75-8a1f-55bf05aeda21"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 47,
                column: "PermissionControllerActionGuid",
                value: new Guid("596404e3-cac9-4c62-8576-443d82d89696"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 48,
                column: "PermissionControllerActionGuid",
                value: new Guid("ceb72d7f-b4c9-4bab-a928-21b2ea6e5471"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 49,
                column: "PermissionControllerActionGuid",
                value: new Guid("a48c2470-e3a9-47a8-8920-b9dd0fbd645e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 50,
                column: "PermissionControllerActionGuid",
                value: new Guid("1858201d-f3bb-4243-80ae-95faed0180ab"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 51,
                column: "PermissionControllerActionGuid",
                value: new Guid("0e69d9cb-f8a3-400f-a48e-8c9a53c39123"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 52,
                column: "PermissionControllerActionGuid",
                value: new Guid("e24f94ed-305b-4379-8aec-3c66136c6d64"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 53,
                column: "PermissionControllerActionGuid",
                value: new Guid("c433988b-d061-4f02-b364-975e5877bb81"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 54,
                column: "PermissionControllerActionGuid",
                value: new Guid("67a70c58-08ec-46f1-a951-f34c7b7b2a4e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 55,
                column: "PermissionControllerActionGuid",
                value: new Guid("841a07e6-4818-482e-889d-a00487f0c5ac"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 56,
                column: "PermissionControllerActionGuid",
                value: new Guid("bb31cff3-2ef8-4ac5-b1df-62b13bd11a8c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 57,
                column: "PermissionControllerActionGuid",
                value: new Guid("8d9b6673-573d-445b-b011-3fc6364d78ac"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 58,
                column: "PermissionControllerActionGuid",
                value: new Guid("53d3c400-3b74-41d8-8da7-1f2994984f60"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 59,
                column: "PermissionControllerActionGuid",
                value: new Guid("4349faea-2be2-4d38-bf24-d08e4a9cfce5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 60,
                column: "PermissionControllerActionGuid",
                value: new Guid("90d4e520-338c-4579-b11d-9d60d43f1784"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 61,
                column: "PermissionControllerActionGuid",
                value: new Guid("ee8bfc27-b7c2-463c-843f-96579cb2039e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 62,
                column: "PermissionControllerActionGuid",
                value: new Guid("2cb785d6-9ef8-4413-ac6e-604a3449dd25"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 63,
                column: "PermissionControllerActionGuid",
                value: new Guid("80787c74-102d-4c5b-aa5d-7af80122794a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 64,
                column: "PermissionControllerActionGuid",
                value: new Guid("117b1949-e052-4339-a93a-c7cc77fdfac6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 65,
                column: "PermissionControllerActionGuid",
                value: new Guid("14d048d9-c6f4-405a-9f7f-a5fdd7a9d698"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 66,
                column: "PermissionControllerActionGuid",
                value: new Guid("60116fa2-5d5f-4e7e-b149-d69a8ea1e9b6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 67,
                column: "PermissionControllerActionGuid",
                value: new Guid("490d4d65-9f8b-44e0-a47d-f9a10457360e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 68,
                column: "PermissionControllerActionGuid",
                value: new Guid("697bfa7b-35e2-4b8d-90f8-cf9b85de7c9d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 69,
                column: "PermissionControllerActionGuid",
                value: new Guid("6096fa60-3c31-419d-9d81-dc12d618d19c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 70,
                column: "PermissionControllerActionGuid",
                value: new Guid("1d64ed8f-f4a2-46f3-b902-b3ecf9619f23"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 71,
                column: "PermissionControllerActionGuid",
                value: new Guid("f44bc892-78a4-46e4-883a-2999a635841b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 72,
                column: "PermissionControllerActionGuid",
                value: new Guid("b6c4e4dc-20be-481a-8e1c-6b6f81c2d7c4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 73,
                column: "PermissionControllerActionGuid",
                value: new Guid("1c47ccf0-a539-4b93-909f-0a03fc5ee56b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 74,
                column: "PermissionControllerActionGuid",
                value: new Guid("5ec4cec3-ddf8-4346-84eb-f5e72af86d06"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 75,
                column: "PermissionControllerActionGuid",
                value: new Guid("89dec7c6-5579-4930-9df8-bd8e97198432"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 76,
                column: "PermissionControllerActionGuid",
                value: new Guid("7b4a3c26-939e-445d-948b-83e03c33c6b0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 77,
                column: "PermissionControllerActionGuid",
                value: new Guid("fbe06738-e992-4b70-b69b-697afee348fa"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 78,
                column: "PermissionControllerActionGuid",
                value: new Guid("faae0e61-9511-451d-bb51-5201a64a6476"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 79,
                column: "PermissionControllerActionGuid",
                value: new Guid("0119d3a8-7ad8-40a8-bddb-1c649e1d8c90"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 80,
                column: "PermissionControllerActionGuid",
                value: new Guid("805cc133-2136-4065-9e16-d5d26089d11e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 81,
                column: "PermissionControllerActionGuid",
                value: new Guid("18d2fa1e-9072-4970-8283-0b277c3d1713"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 82,
                column: "PermissionControllerActionGuid",
                value: new Guid("f39af3a5-a493-4397-8295-b36aad3958ca"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 83,
                column: "PermissionControllerActionGuid",
                value: new Guid("d01231ba-fe03-4150-a64e-4a96d0461973"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 84,
                column: "PermissionControllerActionGuid",
                value: new Guid("84906653-d072-4f73-b9e4-0351d015e0c8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 85,
                column: "PermissionControllerActionGuid",
                value: new Guid("7980d84e-debd-48b3-ac4e-9ed18a80c595"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 86,
                column: "PermissionControllerActionGuid",
                value: new Guid("1e85504e-c312-47b0-8a2b-9dd45308fb78"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 87,
                column: "PermissionControllerActionGuid",
                value: new Guid("149e5f10-7af8-461a-af8c-dc90e7235d59"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 88,
                column: "PermissionControllerActionGuid",
                value: new Guid("6bc36ff2-b1a2-4866-baca-45d8129a86ac"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 89,
                column: "PermissionControllerActionGuid",
                value: new Guid("81082902-44e4-4b68-a913-5687ab567e36"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 90,
                column: "PermissionControllerActionGuid",
                value: new Guid("4a028101-7915-4113-96f8-0d5c5ad6b040"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 91,
                column: "PermissionControllerActionGuid",
                value: new Guid("1a3fc72e-0cb0-4935-8e8a-f186321f269c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 92,
                column: "PermissionControllerActionGuid",
                value: new Guid("9887ff57-7e6f-4695-82f0-91ce5038cf94"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 93,
                column: "PermissionControllerActionGuid",
                value: new Guid("74defa0e-3601-4f47-8b99-a9079d66394b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 94,
                column: "PermissionControllerActionGuid",
                value: new Guid("7880f1bb-31dd-4e45-b641-e65784df7452"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 95,
                column: "PermissionControllerActionGuid",
                value: new Guid("56093c44-033d-4390-ae02-c04c0c92e933"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 96,
                column: "PermissionControllerActionGuid",
                value: new Guid("d4a121a9-72f7-40d6-a098-542af1d13be2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 97,
                column: "PermissionControllerActionGuid",
                value: new Guid("97f18e83-0c39-4e89-b6fb-47a8f887ed1d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 98,
                column: "PermissionControllerActionGuid",
                value: new Guid("65227f03-58ae-44e5-a833-27ea83fee732"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 99,
                column: "PermissionControllerActionGuid",
                value: new Guid("57edd9e8-3453-4255-ad09-12929377024f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 100,
                column: "PermissionControllerActionGuid",
                value: new Guid("bc57c20e-4006-4382-a068-a2b42b880a91"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 101,
                column: "PermissionControllerActionGuid",
                value: new Guid("902c5d8d-c6a5-46f9-a964-4bbac2d55add"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 102,
                column: "PermissionControllerActionGuid",
                value: new Guid("843a6019-c755-435a-ae11-ddde9435b3f1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 103,
                column: "PermissionControllerActionGuid",
                value: new Guid("caed780b-09e9-430b-aa6e-8a145c424cd0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 104,
                column: "PermissionControllerActionGuid",
                value: new Guid("81179614-05fe-4ffb-a618-97d29436107b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 105,
                column: "PermissionControllerActionGuid",
                value: new Guid("ab24e598-e4b1-4eb4-9bfb-7a0eb2a100f3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 106,
                column: "PermissionControllerActionGuid",
                value: new Guid("8282d003-7275-49e0-a2e3-4935d103a278"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 107,
                column: "PermissionControllerActionGuid",
                value: new Guid("756bf4c2-516b-46d6-b545-9799b3d67c25"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 108,
                column: "PermissionControllerActionGuid",
                value: new Guid("f52a8e95-4c66-4fb7-b839-3a773a8e704d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 109,
                column: "PermissionControllerActionGuid",
                value: new Guid("57737aa0-0b04-4090-b055-b3ffe8e629a9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 110,
                column: "PermissionControllerActionGuid",
                value: new Guid("884868c4-61aa-4ec2-ba33-91871c526f31"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 111,
                column: "PermissionControllerActionGuid",
                value: new Guid("20e29c7c-905a-4fb3-bb46-f5c7d35877c0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 112,
                column: "PermissionControllerActionGuid",
                value: new Guid("645de8b4-0412-4b9b-88c2-53540a538b5e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 113,
                column: "PermissionControllerActionGuid",
                value: new Guid("6bebf537-eb14-43af-85d0-e3c32980a2ee"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 114,
                column: "PermissionControllerActionGuid",
                value: new Guid("e2063293-9ea4-473c-880c-4dcc9b78cff4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 115,
                column: "PermissionControllerActionGuid",
                value: new Guid("a0c1353b-c4bf-45a8-8552-09c2c6e823cc"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 116,
                column: "PermissionControllerActionGuid",
                value: new Guid("e2a34801-c737-4d98-ae98-a774f43d02c4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 117,
                column: "PermissionControllerActionGuid",
                value: new Guid("2e074385-b5ef-43e5-893e-a5465d32ed70"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 118,
                column: "PermissionControllerActionGuid",
                value: new Guid("6d62d900-3aec-4c06-90ee-befa8aedc6ad"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 119,
                column: "PermissionControllerActionGuid",
                value: new Guid("5646d8a3-b6f6-454d-9125-20b90119a1dc"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 120,
                column: "PermissionControllerActionGuid",
                value: new Guid("7c71700e-6d92-48e9-939f-878750a55662"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 121,
                column: "PermissionControllerActionGuid",
                value: new Guid("758050d5-28a8-4edc-9fa0-ed6b566c37fb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 122,
                column: "PermissionControllerActionGuid",
                value: new Guid("8457fd60-bc7c-4a69-906b-94b930dcc987"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 123,
                column: "PermissionControllerActionGuid",
                value: new Guid("2eb70867-2720-485f-8d5e-3e3c1f607e01"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 124,
                column: "PermissionControllerActionGuid",
                value: new Guid("1b63e3ad-640e-46e2-8322-75cb477dd948"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 125,
                column: "PermissionControllerActionGuid",
                value: new Guid("0b4c410b-f5ce-4613-8270-efd206ddf9bd"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 126,
                column: "PermissionControllerActionGuid",
                value: new Guid("582aa053-3792-46d4-825f-ec0b92f359e4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 127,
                column: "PermissionControllerActionGuid",
                value: new Guid("2de2dee3-e6b5-4ad9-9e66-608992af78dc"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 128,
                column: "PermissionControllerActionGuid",
                value: new Guid("045c7b45-78ac-4045-b941-f2c1ed2172e3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 129,
                column: "PermissionControllerActionGuid",
                value: new Guid("9c18004c-ee9c-450c-8a0a-d068ed13ecb8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 130,
                column: "PermissionControllerActionGuid",
                value: new Guid("248f8ef4-3332-437d-8f9e-2fe033296ddc"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 131,
                column: "PermissionControllerActionGuid",
                value: new Guid("6467b65b-9f5b-4b7b-9a98-59f12025daa8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 132,
                column: "PermissionControllerActionGuid",
                value: new Guid("f903bf51-3860-4baf-80bc-f26720005626"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 133,
                column: "PermissionControllerActionGuid",
                value: new Guid("252a0a8a-1dcb-4a97-a96f-a59fd7263fc0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 134,
                column: "PermissionControllerActionGuid",
                value: new Guid("7c92bbba-e53e-4446-ac59-2a7dc6d885ff"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 135,
                column: "PermissionControllerActionGuid",
                value: new Guid("6692c31e-41c4-4f57-b166-05ef83422a1e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 136,
                column: "PermissionControllerActionGuid",
                value: new Guid("b56f5d2d-85d1-4ef0-bd1a-8ae67dd2b616"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 137,
                column: "PermissionControllerActionGuid",
                value: new Guid("b99934f8-14d6-413e-a01f-92dbd63c33d6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 138,
                column: "PermissionControllerActionGuid",
                value: new Guid("ac435f3e-47b1-4c90-9f9e-09f6c00029a8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 139,
                column: "PermissionControllerActionGuid",
                value: new Guid("c62db114-ac20-4363-9b9c-bf0ab77f8fbf"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 140,
                column: "PermissionControllerActionGuid",
                value: new Guid("518a8bf2-4888-4477-95d5-d12e1e101ddc"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 141,
                column: "PermissionControllerActionGuid",
                value: new Guid("b01ca368-fa6d-48c6-8ec8-e850f9bf6d57"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 142,
                column: "PermissionControllerActionGuid",
                value: new Guid("b90468ad-36b3-4a0b-b135-81b5fe44c55e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 143,
                column: "PermissionControllerActionGuid",
                value: new Guid("ef01b0e0-6c39-43e0-ad4a-78085ff8167c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 144,
                column: "PermissionControllerActionGuid",
                value: new Guid("c924752f-b0f2-4c39-80be-80931cf3bb19"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 145,
                column: "PermissionControllerActionGuid",
                value: new Guid("4f36bace-fff5-4865-83e2-b80fb49324c1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 146,
                column: "PermissionControllerActionGuid",
                value: new Guid("7117e6c0-c3d2-4a18-9196-36fc3d005349"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 147,
                column: "PermissionControllerActionGuid",
                value: new Guid("76c438e3-47da-4d9b-b8da-f9ef7d045d4b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 148,
                column: "PermissionControllerActionGuid",
                value: new Guid("2484e85f-df81-445d-b1d5-2c63b92bc336"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 149,
                column: "PermissionControllerActionGuid",
                value: new Guid("4f25af46-349e-4d95-ab5b-05e8397504f6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 150,
                column: "PermissionControllerActionGuid",
                value: new Guid("a0e5a100-6644-4c33-bf78-b89dcdd55fe4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 151,
                column: "PermissionControllerActionGuid",
                value: new Guid("b0bc5a8d-a7f3-4a87-9271-1f4d60869604"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 152,
                column: "PermissionControllerActionGuid",
                value: new Guid("1def4d8d-f600-46c5-aaaf-c18a26309ae9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 153,
                column: "PermissionControllerActionGuid",
                value: new Guid("31486ae0-661e-4730-9e80-bae940a70eb2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 154,
                column: "PermissionControllerActionGuid",
                value: new Guid("5700e13d-1b09-41c7-bd10-caddfb5ea845"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 155,
                column: "PermissionControllerActionGuid",
                value: new Guid("a3344a21-faf9-49bc-a4f8-f8cffc1c43e6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 156,
                column: "PermissionControllerActionGuid",
                value: new Guid("4566507d-f581-41c7-9133-a60fdb595c2e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 157,
                column: "PermissionControllerActionGuid",
                value: new Guid("91175872-3c91-4257-b996-d08708a3ccdd"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 158,
                column: "PermissionControllerActionGuid",
                value: new Guid("f588a8bc-9a77-4d94-803b-8bc9924b5273"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 159,
                column: "PermissionControllerActionGuid",
                value: new Guid("f6cb0542-032f-4912-a4b7-76e58f75060e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 160,
                column: "PermissionControllerActionGuid",
                value: new Guid("b30cef04-9bc3-4515-b53a-dc8d37c31eec"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 161,
                column: "PermissionControllerActionGuid",
                value: new Guid("3da05c96-2d77-4e87-b29e-4c18c59d6331"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 162,
                column: "PermissionControllerActionGuid",
                value: new Guid("53a5b312-8bc4-4abd-b2f1-3d4abe592948"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 163,
                column: "PermissionControllerActionGuid",
                value: new Guid("957e6e43-5141-4d9d-a0e1-b2788e85efe8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 164,
                column: "PermissionControllerActionGuid",
                value: new Guid("df7f4721-7770-4d40-bb8f-6f6641c7538b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 165,
                column: "PermissionControllerActionGuid",
                value: new Guid("d85ee01b-8841-4332-9533-ed6a5db238e6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 166,
                column: "PermissionControllerActionGuid",
                value: new Guid("b01e8168-f260-45d5-b40d-cde0cd4266e8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 167,
                column: "PermissionControllerActionGuid",
                value: new Guid("9e7709d7-895d-457d-b55b-37e0e4d1f343"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 168,
                column: "PermissionControllerActionGuid",
                value: new Guid("7e7cc5fb-0609-4bcc-a11b-4910be77652b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 169,
                column: "PermissionControllerActionGuid",
                value: new Guid("35445d8d-0532-4252-a260-bd801476524e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 170,
                column: "PermissionControllerActionGuid",
                value: new Guid("4879f35d-5f79-413d-a57f-0d976d49c25f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 171,
                column: "PermissionControllerActionGuid",
                value: new Guid("63eb5cf4-7961-47c7-a947-298ce38d5512"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 172,
                column: "PermissionControllerActionGuid",
                value: new Guid("966d9915-c296-4872-bd4c-924e412dc559"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 173,
                column: "PermissionControllerActionGuid",
                value: new Guid("4d33cd95-8616-4425-8831-9631fb0cfd94"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 174,
                column: "PermissionControllerActionGuid",
                value: new Guid("157e531b-9be5-4775-a045-61d2b2f01007"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 175,
                column: "PermissionControllerActionGuid",
                value: new Guid("b8cb54fd-5d8b-4325-bed8-9099352ea847"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 176,
                column: "PermissionControllerActionGuid",
                value: new Guid("1dbe5518-fa10-4a72-9a1c-4be8f4f5f0f6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 177,
                column: "PermissionControllerActionGuid",
                value: new Guid("81cd86cc-3b6d-4e2a-962a-7d65c8c00e53"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 178,
                column: "PermissionControllerActionGuid",
                value: new Guid("85f4f648-5ee7-4dd2-b14b-3307e680b274"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 179,
                column: "PermissionControllerActionGuid",
                value: new Guid("0142c395-2fae-4d47-99bd-60acd138ea1e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 180,
                column: "PermissionControllerActionGuid",
                value: new Guid("c2c0dc65-a531-48d7-b80c-8acabeb0cadd"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 181,
                column: "PermissionControllerActionGuid",
                value: new Guid("8c788ae8-3b90-46c1-9cbd-380b137bfaec"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 182,
                column: "PermissionControllerActionGuid",
                value: new Guid("84de20df-2975-4b3a-8f1c-2b492e2f4d30"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 183,
                column: "PermissionControllerActionGuid",
                value: new Guid("fb1f2008-eb79-4da6-864a-3cf259a86a17"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 184,
                column: "PermissionControllerActionGuid",
                value: new Guid("b9584d2b-1681-43eb-94cb-cde313e12a05"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 185,
                column: "PermissionControllerActionGuid",
                value: new Guid("60312cf3-5482-470a-af21-d0952b2c8270"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 186,
                column: "PermissionControllerActionGuid",
                value: new Guid("cf485197-dec5-454d-97a6-9042f7f50719"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 187,
                column: "PermissionControllerActionGuid",
                value: new Guid("b06585b5-7d1c-4f90-b997-9195e335d8fb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 188,
                column: "PermissionControllerActionGuid",
                value: new Guid("d33da930-110d-45e6-b556-ab035d8e18e9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 189,
                column: "PermissionControllerActionGuid",
                value: new Guid("90a825c1-d755-4dc0-90e8-98974bde747e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 190,
                column: "PermissionControllerActionGuid",
                value: new Guid("ef4522c5-97a4-47db-b407-c1c91cfaef6f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 191,
                column: "PermissionControllerActionGuid",
                value: new Guid("84dec5b2-0997-40df-93a6-ee0e1fbe4bf3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 192,
                column: "PermissionControllerActionGuid",
                value: new Guid("fa769149-924e-46f2-9b71-5390413a1035"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 193,
                column: "PermissionControllerActionGuid",
                value: new Guid("f04dfa69-e922-42fb-8d70-3d38dc87f54b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 194,
                column: "PermissionControllerActionGuid",
                value: new Guid("a73e018f-e01b-4e08-91eb-3012abc73d18"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 195,
                column: "PermissionControllerActionGuid",
                value: new Guid("45c2e069-d7fe-42e6-9240-d4f7b7d9f1c6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 196,
                column: "PermissionControllerActionGuid",
                value: new Guid("5bff2bc7-d145-4da1-bb11-0b48f072cc0d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 197,
                column: "PermissionControllerActionGuid",
                value: new Guid("3da3af61-f9be-42a0-a29f-ff174f2679ab"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 198,
                column: "PermissionControllerActionGuid",
                value: new Guid("7e3f7f5c-4a45-4457-ab53-f90d01f5e231"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 199,
                column: "PermissionControllerActionGuid",
                value: new Guid("ab331907-fa40-4052-b042-037e212cb5b2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 200,
                column: "PermissionControllerActionGuid",
                value: new Guid("34595f88-993f-4e6f-b72b-ca7060e189ff"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 201,
                column: "PermissionControllerActionGuid",
                value: new Guid("9498b00e-019b-4da6-b19c-3a68d73b8689"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 202,
                column: "PermissionControllerActionGuid",
                value: new Guid("c63d2bd8-831e-4429-ad98-986765590173"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 203,
                column: "PermissionControllerActionGuid",
                value: new Guid("180e1af5-c337-4986-bd31-309bb8a40ad9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 204,
                column: "PermissionControllerActionGuid",
                value: new Guid("16f07025-dbce-4941-98c1-8c981c86016a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 205,
                column: "PermissionControllerActionGuid",
                value: new Guid("f80c92eb-61cc-4491-a639-a71264aa9cef"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 206,
                column: "PermissionControllerActionGuid",
                value: new Guid("6aa69eea-6b44-4910-b9a2-e238175668d8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 207,
                column: "PermissionControllerActionGuid",
                value: new Guid("aa8ec8f9-a4b3-4174-834f-21a96cf81272"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 208,
                column: "PermissionControllerActionGuid",
                value: new Guid("268b6fea-9b66-4c33-b930-b621f4df5096"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 209,
                column: "PermissionControllerActionGuid",
                value: new Guid("144a4232-4fad-4bd4-9e9a-d2a776f9b81e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 210,
                column: "PermissionControllerActionGuid",
                value: new Guid("730833dd-8b38-492d-a2dd-931260fa2058"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 211,
                column: "PermissionControllerActionGuid",
                value: new Guid("91029268-e444-4642-a0a3-d89f4f7b3dad"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 212,
                column: "PermissionControllerActionGuid",
                value: new Guid("c14531ad-8acd-419b-8692-c7cfc807ea3b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 213,
                column: "PermissionControllerActionGuid",
                value: new Guid("cae5a41f-5955-43f7-bd57-894bc6c81b3e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 214,
                column: "PermissionControllerActionGuid",
                value: new Guid("2a353d24-9b21-4938-84db-888215ec148d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 215,
                column: "PermissionControllerActionGuid",
                value: new Guid("cbcf55c0-ca37-4015-8f30-fee473658cdc"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 216,
                column: "PermissionControllerActionGuid",
                value: new Guid("4b954cf9-49e7-4f7b-8a87-aefa090d1c21"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 217,
                column: "PermissionControllerActionGuid",
                value: new Guid("6251ed40-f914-4c0c-85b6-2bd1f91a5068"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 218,
                column: "PermissionControllerActionGuid",
                value: new Guid("07946394-84c9-46e8-8483-e3c1637c3d84"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 219,
                column: "PermissionControllerActionGuid",
                value: new Guid("b3cd7b32-098d-4acf-8832-643f212a5cd3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 220,
                column: "PermissionControllerActionGuid",
                value: new Guid("f1e02d0a-d1a2-4264-8e8a-4b656a3af12d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 221,
                column: "PermissionControllerActionGuid",
                value: new Guid("10ba022b-eeb3-488e-a3f5-b51f5db2779f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 222,
                column: "PermissionControllerActionGuid",
                value: new Guid("41b708b0-6d15-4ec6-8f11-aa620bec931a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 223,
                column: "PermissionControllerActionGuid",
                value: new Guid("7b771ff9-dbb0-428a-af98-87a3b6bdde6a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 224,
                column: "PermissionControllerActionGuid",
                value: new Guid("abb37457-1a4f-4f10-8da0-c692c7c051dc"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 225,
                column: "PermissionControllerActionGuid",
                value: new Guid("2cec3afd-dcb8-46dc-957a-cd983db81fca"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 226,
                column: "PermissionControllerActionGuid",
                value: new Guid("ed151114-6b2f-4f24-a711-9c790ef26f30"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 227,
                column: "PermissionControllerActionGuid",
                value: new Guid("308dc01c-1f58-427f-84b5-3b83e98af0fa"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 228,
                column: "PermissionControllerActionGuid",
                value: new Guid("a4a9a2be-0fae-43ae-9853-709e485cb666"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 229,
                column: "PermissionControllerActionGuid",
                value: new Guid("0e1fe178-14bb-4571-9920-0c439365aff7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 230,
                column: "PermissionControllerActionGuid",
                value: new Guid("128ce750-6897-4d6f-8208-4bca277e3dc7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 231,
                column: "PermissionControllerActionGuid",
                value: new Guid("d033a9e4-7b41-454a-a57b-567bbac29216"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 232,
                column: "PermissionControllerActionGuid",
                value: new Guid("5fa30209-c0f8-4473-8cfd-6aca3160fe54"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 233,
                column: "PermissionControllerActionGuid",
                value: new Guid("7b9c296c-0747-4881-a181-3791c33ff9a1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 234,
                column: "PermissionControllerActionGuid",
                value: new Guid("11933975-14a6-4d56-9036-0ec6af30ba03"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 235,
                column: "PermissionControllerActionGuid",
                value: new Guid("708c1953-9996-40fa-80f0-51504abd37b8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 236,
                column: "PermissionControllerActionGuid",
                value: new Guid("a9ef36ad-34be-4498-b55c-2c12c7695c74"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 237,
                column: "PermissionControllerActionGuid",
                value: new Guid("9793c54d-f7fe-444c-8567-003575041825"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 238,
                column: "PermissionControllerActionGuid",
                value: new Guid("6877b273-c801-45f1-935b-28bea98ed8ba"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 239,
                column: "PermissionControllerActionGuid",
                value: new Guid("807f9064-ec49-4548-baf0-9860f6da13a7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 240,
                column: "PermissionControllerActionGuid",
                value: new Guid("7dd2d622-43db-4621-b319-4520eb4bff74"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 241,
                column: "PermissionControllerActionGuid",
                value: new Guid("a127ee6e-ee11-46cb-bbee-50fd9452f2f5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 242,
                column: "PermissionControllerActionGuid",
                value: new Guid("9ddac691-9b10-4f2b-bb44-95956923d2cf"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 243,
                column: "PermissionControllerActionGuid",
                value: new Guid("8ad01048-54fc-48fa-bd08-718e856a335e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 244,
                column: "PermissionControllerActionGuid",
                value: new Guid("643efa40-d8f9-4a0f-8615-95b04b1772ae"));

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "City",
                keyColumn: "CityID",
                keyValue: 1,
                columns: new[] { "CityGuid", "CreateDate", "EnableDate" },
                values: new object[] { new Guid("acade0e4-60ff-4933-9a02-5696eecd2338"), new DateTime(2022, 4, 12, 19, 23, 13, 968, DateTimeKind.Local).AddTicks(2381), new DateTime(2022, 4, 12, 19, 23, 13, 968, DateTimeKind.Local).AddTicks(2922) });

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "Country",
                keyColumn: "CountryID",
                keyValue: 1,
                columns: new[] { "CountryGuid", "CreateDate", "EnableDate" },
                values: new object[] { new Guid("661218a4-7f86-4d7e-944b-031ea35b7cbb"), new DateTime(2022, 4, 12, 19, 23, 13, 963, DateTimeKind.Local).AddTicks(4953), new DateTime(2022, 4, 12, 19, 23, 13, 966, DateTimeKind.Local).AddTicks(5598) });

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "Departments",
                keyColumn: "DepartmentsID",
                keyValue: 1,
                columns: new[] { "CreateDate", "DepartmentsGuid", "EnableDate" },
                values: new object[] { new DateTime(2022, 4, 12, 19, 23, 13, 972, DateTimeKind.Local).AddTicks(2471), new Guid("cee54f7f-a0df-4436-a2a9-798298cc8df1"), new DateTime(2022, 4, 12, 19, 23, 13, 972, DateTimeKind.Local).AddTicks(3113) });

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "Jobs",
                keyColumn: "JobsID",
                keyValue: 1,
                columns: new[] { "CreateDate", "EnableDate", "JobsGuid" },
                values: new object[] { new DateTime(2022, 4, 12, 19, 23, 13, 971, DateTimeKind.Local).AddTicks(1917), new DateTime(2022, 4, 12, 19, 23, 13, 971, DateTimeKind.Local).AddTicks(2662), new Guid("f6e3a815-eba2-4e41-9a5b-d7234cafc029") });

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "MainCategory",
                keyColumn: "MainCategoryID",
                keyValue: 1,
                columns: new[] { "CreateDate", "EnableDate", "MainCategoryGuid" },
                values: new object[] { new DateTime(2022, 4, 12, 19, 23, 13, 971, DateTimeKind.Local).AddTicks(6796), new DateTime(2022, 4, 12, 19, 23, 13, 971, DateTimeKind.Local).AddTicks(7397), new Guid("8d1637ea-f6d0-4196-b036-670a4cb3fd9e") });

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "Nationality",
                keyColumn: "NationalityID",
                keyValue: 1,
                columns: new[] { "CreateDate", "EnableDate", "NationalityGuid" },
                values: new object[] { new DateTime(2022, 4, 12, 19, 23, 13, 968, DateTimeKind.Local).AddTicks(9151), new DateTime(2022, 4, 12, 19, 23, 13, 968, DateTimeKind.Local).AddTicks(9694), new Guid("efb10a46-2160-4f8f-94b9-72699a2b3c56") });

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "Region",
                keyColumn: "RegionID",
                keyValue: 1,
                columns: new[] { "CreateDate", "EnableDate", "RegionGuid" },
                values: new object[] { new DateTime(2022, 4, 12, 19, 23, 13, 967, DateTimeKind.Local).AddTicks(2988), new DateTime(2022, 4, 12, 19, 23, 13, 967, DateTimeKind.Local).AddTicks(3549), new Guid("2e349db8-1eab-49f1-8cfc-1b1bedb8fe2a") });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyWorkingHours_UserId",
                schema: "Setting",
                table: "CompanyWorkingHours",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyWorkingHours",
                schema: "Setting");

            migrationBuilder.DropColumn(
                name: "FacebookLink",
                schema: "Setting",
                table: "Configuration");

            migrationBuilder.DropColumn(
                name: "InstagramLink",
                schema: "Setting",
                table: "Configuration");

            migrationBuilder.DropColumn(
                name: "SnapchatLink",
                schema: "Setting",
                table: "Configuration");

            migrationBuilder.DropColumn(
                name: "TwitterLink",
                schema: "Setting",
                table: "Configuration");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "6671b678-5b7d-4b38-9b4c-a14833b86a6d");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "8258d4ba-ee62-4ed7-9c5f-192ae347716b");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "8ac67183-090b-4666-a972-b901a6b4b235");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "215dcf9b-ed97-4887-8606-dd373b928d18");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "d7324be0-c8ed-49d8-8f60-a3f4e20a3852", "91d2aa4e-b48a-4479-83c7-387b565d6eab" });

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "DeliverySetting",
                keyColumn: "DeliverySettingID",
                keyValue: 1,
                column: "DeliverySettingGuid",
                value: new Guid("e90df8a5-9a9e-4e2f-afae-ae3f19e02083"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 1,
                column: "TransactionTypeGuid",
                value: new Guid("349a67a8-e8b6-4d80-bcd7-04d58d07ee4f"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 2,
                column: "TransactionTypeGuid",
                value: new Guid("aab27afb-832e-4185-8c7e-b6c7dbc87f05"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 3,
                column: "TransactionTypeGuid",
                value: new Guid("2db2a523-9ccf-43fc-97eb-7aaae5807ff2"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 4,
                column: "TransactionTypeGuid",
                value: new Guid("fe17831e-af39-411d-b7f2-f08b595fa57c"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 5,
                column: "TransactionTypeGuid",
                value: new Guid("0af55397-20ba-4c2f-a3c9-9f2d2ff248fd"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 6,
                column: "TransactionTypeGuid",
                value: new Guid("47aad692-9ee7-4783-8a4e-d917428f4d87"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 7,
                column: "TransactionTypeGuid",
                value: new Guid("2186cbe3-222f-442f-8f7f-9ea3f8414397"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 8,
                column: "TransactionTypeGuid",
                value: new Guid("4da657e9-2ce9-4795-bf34-026090f5a19c"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 9,
                column: "TransactionTypeGuid",
                value: new Guid("84558806-3102-4829-8be9-498eaaaacddb"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 10,
                column: "TransactionTypeGuid",
                value: new Guid("8da47102-4895-47ab-944c-f684b801d5fa"));

            migrationBuilder.UpdateData(
                schema: "Emp",
                table: "Employees",
                keyColumn: "EntityEmpID",
                keyValue: 1,
                columns: new[] { "CreateDate", "EnableDate" },
                values: new object[] { new DateTime(2022, 3, 10, 10, 26, 27, 904, DateTimeKind.Local).AddTicks(7203), new DateTime(2022, 3, 10, 10, 26, 27, 904, DateTimeKind.Local).AddTicks(7626) });

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionAction",
                keyColumn: "PermissionActionID",
                keyValue: 1,
                column: "PermissionActionGuid",
                value: new Guid("37397ceb-24fc-4c5f-9b37-c6be712c7c87"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionAction",
                keyColumn: "PermissionActionID",
                keyValue: 2,
                column: "PermissionActionGuid",
                value: new Guid("e5ff0ff9-b820-4709-936b-35838aa9595b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionAction",
                keyColumn: "PermissionActionID",
                keyValue: 3,
                column: "PermissionActionGuid",
                value: new Guid("74eff658-cc3e-4f53-9aba-ea193cd14e34"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionAction",
                keyColumn: "PermissionActionID",
                keyValue: 4,
                column: "PermissionActionGuid",
                value: new Guid("57569d1e-345d-4471-9233-a064f8b2e111"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 1,
                column: "PermissionControllerGuid",
                value: new Guid("95f37dac-f7e2-42c7-8b4a-20974afca61f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 2,
                column: "PermissionControllerGuid",
                value: new Guid("71c0afd2-7aa1-4a94-96eb-fe4a819e1263"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 3,
                column: "PermissionControllerGuid",
                value: new Guid("b3b49e33-b539-4cf6-b1f7-69ba13b50f0c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 4,
                column: "PermissionControllerGuid",
                value: new Guid("02d16c41-8aae-4bc3-abf7-ee902defcb3b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 5,
                column: "PermissionControllerGuid",
                value: new Guid("85cb8442-adae-4d6c-b15d-22479c9e66fe"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 6,
                column: "PermissionControllerGuid",
                value: new Guid("c0018a81-6c28-4f19-a46e-227160f3ef28"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 7,
                column: "PermissionControllerGuid",
                value: new Guid("61f7ad8d-28a3-4e44-bc52-490a7be80365"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 8,
                column: "PermissionControllerGuid",
                value: new Guid("a8b66f05-c38c-4b2f-b9e3-edf52510a6e5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 9,
                column: "PermissionControllerGuid",
                value: new Guid("6012c2ed-3069-4943-b5b8-5e7cef173cb9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 10,
                column: "PermissionControllerGuid",
                value: new Guid("f63c39f0-0fd9-4fc7-a014-189fcaf7a482"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 11,
                column: "PermissionControllerGuid",
                value: new Guid("fb104c94-a447-4291-b22d-10efd39f8159"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 12,
                column: "PermissionControllerGuid",
                value: new Guid("c2cdd5d0-803e-41e2-ac6a-a3f0d0101aa2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 13,
                column: "PermissionControllerGuid",
                value: new Guid("7cd28ced-db6e-41cb-b8e6-fb064ca0c32a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 14,
                column: "PermissionControllerGuid",
                value: new Guid("2c9b377e-2fec-403a-a72f-2cceaa5c4355"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 15,
                column: "PermissionControllerGuid",
                value: new Guid("39ae9c8b-8b4f-493b-bb7b-34427915a439"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 16,
                column: "PermissionControllerGuid",
                value: new Guid("76ddd25a-ff7d-43fc-85f0-18f8bbfee340"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 17,
                column: "PermissionControllerGuid",
                value: new Guid("104f8d9f-43f0-48b4-857c-792955d8c6e7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 18,
                column: "PermissionControllerGuid",
                value: new Guid("57ab9b84-48ec-474a-b308-f53582c0e493"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 19,
                column: "PermissionControllerGuid",
                value: new Guid("f07b968f-77ac-401e-9f98-58499cb498a7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 20,
                column: "PermissionControllerGuid",
                value: new Guid("c4817cb7-49c7-47ee-9c47-799358af95bd"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 21,
                column: "PermissionControllerGuid",
                value: new Guid("50851ccc-b131-4ef1-9ed3-dafe57f69dab"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 22,
                column: "PermissionControllerGuid",
                value: new Guid("e87f7a71-d3ab-46d7-adbf-d22cd18a883e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 23,
                column: "PermissionControllerGuid",
                value: new Guid("f0a00d7e-db2c-42c2-83b7-07482241b8a7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 24,
                column: "PermissionControllerGuid",
                value: new Guid("5c135161-7122-4c8b-93a7-ec6ec608b390"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 25,
                column: "PermissionControllerGuid",
                value: new Guid("8eff6631-0c0b-49df-9991-879d36ee077d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 26,
                column: "PermissionControllerGuid",
                value: new Guid("1fd31c47-071b-4cbe-8c2b-ad4b119e17d3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 27,
                column: "PermissionControllerGuid",
                value: new Guid("4cbef696-7b23-44fd-adf2-d3089bab8ba5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 28,
                column: "PermissionControllerGuid",
                value: new Guid("942d5517-8b97-49b0-984b-d19a7aa3b6c1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 29,
                column: "PermissionControllerGuid",
                value: new Guid("dc82fc00-5f75-4132-83c0-f0c84ff4f5da"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 30,
                column: "PermissionControllerGuid",
                value: new Guid("1c4bcbf5-e3a0-4a3d-91f0-470e87f07fcc"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 31,
                column: "PermissionControllerGuid",
                value: new Guid("c1cf41ff-22df-4baf-ac9d-635c2b7ea5a9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 32,
                column: "PermissionControllerGuid",
                value: new Guid("6709e52e-aaaa-4442-bb27-b9c5d603c14c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 33,
                column: "PermissionControllerGuid",
                value: new Guid("9e25fe2a-c209-46e4-8c21-0bce6fb7c7e6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 34,
                column: "PermissionControllerGuid",
                value: new Guid("6fea6236-dcd6-4897-ba0b-9cdc1b04a4d8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 35,
                column: "PermissionControllerGuid",
                value: new Guid("53f9d5db-b807-47c5-8c54-acb9ee18de08"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 36,
                column: "PermissionControllerGuid",
                value: new Guid("18951fe4-fe3a-4167-8feb-0c2b578947a9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 37,
                column: "PermissionControllerGuid",
                value: new Guid("3908821d-ca24-4a76-af63-ca98980162dc"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 38,
                column: "PermissionControllerGuid",
                value: new Guid("e02d9c0b-8048-48f9-ae77-a93e37c3356d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 39,
                column: "PermissionControllerGuid",
                value: new Guid("7ecd8a7c-8bf8-49d1-86a2-48ba3d959d3b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 40,
                column: "PermissionControllerGuid",
                value: new Guid("d458a9a7-7044-4a5f-a96c-3a55ea971f67"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 41,
                column: "PermissionControllerGuid",
                value: new Guid("a274c917-2044-4fec-a3ff-9eec0f7d8246"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 42,
                column: "PermissionControllerGuid",
                value: new Guid("0018d0d5-3c78-42a8-adad-cbceb3869555"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 43,
                column: "PermissionControllerGuid",
                value: new Guid("28e616e0-ef84-44f5-b433-d7877a73d12c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 44,
                column: "PermissionControllerGuid",
                value: new Guid("5bc95364-a6af-4d52-bdca-b14a32a4fc87"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 45,
                column: "PermissionControllerGuid",
                value: new Guid("a840167a-daf5-4332-ac56-ed634cf020a5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 46,
                column: "PermissionControllerGuid",
                value: new Guid("e58067a4-22a5-44fa-88de-d40d3ba9a068"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 47,
                column: "PermissionControllerGuid",
                value: new Guid("ffddefda-d335-47d1-abd0-61ad4d77e465"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 48,
                column: "PermissionControllerGuid",
                value: new Guid("6debb386-1d9b-4fb4-9817-c6900e1c18f7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 49,
                column: "PermissionControllerGuid",
                value: new Guid("414f6593-1c38-488c-b5d1-d42deb9c1c06"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 50,
                column: "PermissionControllerGuid",
                value: new Guid("a8161be5-4522-4231-8880-f8f018d98125"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 51,
                column: "PermissionControllerGuid",
                value: new Guid("f256f032-1ce3-4698-8621-271d518e63cb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 52,
                column: "PermissionControllerGuid",
                value: new Guid("890485b6-f354-4f3f-856b-47ce59a382d4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 53,
                column: "PermissionControllerGuid",
                value: new Guid("64147e75-12c6-461b-b230-a7b47dc630c9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 54,
                column: "PermissionControllerGuid",
                value: new Guid("60248988-3476-478d-992b-42e9ce6ec817"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 55,
                column: "PermissionControllerGuid",
                value: new Guid("7756a164-d11a-44b5-9c18-b365fef14244"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 56,
                column: "PermissionControllerGuid",
                value: new Guid("ca28086e-b002-4210-9c9f-798210437dca"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 57,
                column: "PermissionControllerGuid",
                value: new Guid("cfcc8755-4d46-4afa-af1a-e42e62dd71bb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 58,
                column: "PermissionControllerGuid",
                value: new Guid("0404dd12-a6ac-407f-8994-4e4d2bfe9185"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 59,
                column: "PermissionControllerGuid",
                value: new Guid("b3e84f7c-fc58-4d4f-a232-39fc23fe412a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 60,
                column: "PermissionControllerGuid",
                value: new Guid("5b2fa217-3c0a-46ee-994d-08497af7074e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 61,
                column: "PermissionControllerGuid",
                value: new Guid("9fa7dbb4-454e-4c9a-aa58-584ddf3643a1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 1,
                column: "PermissionControllerActionGuid",
                value: new Guid("b31e10f1-6f98-41bc-b637-592baafccf55"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 2,
                column: "PermissionControllerActionGuid",
                value: new Guid("26449097-19df-44b7-8117-f79ecbfee4ae"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 3,
                column: "PermissionControllerActionGuid",
                value: new Guid("f346e96a-4ed8-452d-82cd-f7b40b9ed1b3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 4,
                column: "PermissionControllerActionGuid",
                value: new Guid("f62066d3-6be0-4b5b-b9da-82a73e8f935d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 5,
                column: "PermissionControllerActionGuid",
                value: new Guid("436cab71-ba1b-4ee1-b729-2c47de35b424"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 6,
                column: "PermissionControllerActionGuid",
                value: new Guid("28ffb0b8-9f78-4c83-a485-e299762f1fca"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 7,
                column: "PermissionControllerActionGuid",
                value: new Guid("269c4f68-0239-41a7-915e-49a678742fb2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 8,
                column: "PermissionControllerActionGuid",
                value: new Guid("288cf5c9-3d3d-46fe-b314-5e335c34a877"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 9,
                column: "PermissionControllerActionGuid",
                value: new Guid("601c0ca0-11d0-4c6e-be2f-746318129bd4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 10,
                column: "PermissionControllerActionGuid",
                value: new Guid("e66228aa-db68-44cf-9fff-6aa9bae58089"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 11,
                column: "PermissionControllerActionGuid",
                value: new Guid("665fd1d6-44bb-4331-86ec-940bd05c2fce"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 12,
                column: "PermissionControllerActionGuid",
                value: new Guid("6b1315ed-2c64-4f8e-a09c-2c4196cc480b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 13,
                column: "PermissionControllerActionGuid",
                value: new Guid("bb1d1380-78bf-4b15-8485-c097e7b7ddad"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 14,
                column: "PermissionControllerActionGuid",
                value: new Guid("ec4710c4-dfe5-455c-836e-cfee8a934a6c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 15,
                column: "PermissionControllerActionGuid",
                value: new Guid("74dfcbad-a20f-4899-9e4c-d5de49f355d9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 16,
                column: "PermissionControllerActionGuid",
                value: new Guid("6365f414-40f7-49bb-8617-06911df15fa1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 17,
                column: "PermissionControllerActionGuid",
                value: new Guid("ca3d147c-48bb-4432-b3bb-9bc3a407fce1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 18,
                column: "PermissionControllerActionGuid",
                value: new Guid("d731f47e-e173-4e10-97b2-be2b89f3e4ec"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 19,
                column: "PermissionControllerActionGuid",
                value: new Guid("d81f2a9f-cedd-47b3-a753-02b9b6172eaa"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 20,
                column: "PermissionControllerActionGuid",
                value: new Guid("0f6c1f6e-61da-4a9c-8017-7c703d29ced2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 21,
                column: "PermissionControllerActionGuid",
                value: new Guid("af3473f1-e1e9-456f-9bd7-4d0294ff1a30"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 22,
                column: "PermissionControllerActionGuid",
                value: new Guid("ecf12a7c-4da9-455d-aafe-4fb5b649233c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 23,
                column: "PermissionControllerActionGuid",
                value: new Guid("795850ab-3dde-4c6a-8fee-a6d61a2e539e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 24,
                column: "PermissionControllerActionGuid",
                value: new Guid("531cf708-b405-4dce-9a80-6ff8acc8dcc0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 25,
                column: "PermissionControllerActionGuid",
                value: new Guid("a5f6f319-3d12-42a2-9a74-59e584f43120"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 26,
                column: "PermissionControllerActionGuid",
                value: new Guid("d8dc3bd5-1671-483d-b1ad-28347783c147"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 27,
                column: "PermissionControllerActionGuid",
                value: new Guid("dc66d6ed-b978-4d67-8f93-12c881bc858b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 28,
                column: "PermissionControllerActionGuid",
                value: new Guid("b3bc9538-fb9e-4f70-9388-7dd909de563b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 29,
                column: "PermissionControllerActionGuid",
                value: new Guid("abcddb6c-16d9-4c05-9b04-884befd7a424"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 30,
                column: "PermissionControllerActionGuid",
                value: new Guid("ad5a6eba-b7e1-4355-aa27-0528c7105007"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 31,
                column: "PermissionControllerActionGuid",
                value: new Guid("9a04e5a0-2915-4732-89f8-cd09f407c8ab"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 32,
                column: "PermissionControllerActionGuid",
                value: new Guid("6113e32b-7f28-4409-8e87-a774ff83e7b1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 33,
                column: "PermissionControllerActionGuid",
                value: new Guid("0feb496c-6296-4ece-9732-b99f35d7f2d2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 34,
                column: "PermissionControllerActionGuid",
                value: new Guid("f6ac9180-5ce3-425e-ba05-358a7028fd99"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 35,
                column: "PermissionControllerActionGuid",
                value: new Guid("da7ba492-d06f-47ee-88a3-7fd028c03519"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 36,
                column: "PermissionControllerActionGuid",
                value: new Guid("70a99355-e008-4689-83d9-d1bce2b89d5b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 37,
                column: "PermissionControllerActionGuid",
                value: new Guid("2d4b25a6-fc24-4c4f-a4f8-d12fea256ed0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 38,
                column: "PermissionControllerActionGuid",
                value: new Guid("58926f8b-da64-40ce-ba7b-0c0030a2dfcf"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 39,
                column: "PermissionControllerActionGuid",
                value: new Guid("7a2e2f8c-5e7c-45c0-8f2b-6ebf84f93357"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 40,
                column: "PermissionControllerActionGuid",
                value: new Guid("8645ff52-dd39-46a5-97c3-ad5ed5b13d8d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 41,
                column: "PermissionControllerActionGuid",
                value: new Guid("23e4fdd0-5d08-4754-b3dd-dba835b64dcc"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 42,
                column: "PermissionControllerActionGuid",
                value: new Guid("974d4325-37aa-4ebc-adbf-21ab7a528faa"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 43,
                column: "PermissionControllerActionGuid",
                value: new Guid("0a1395a3-7a20-419c-b95b-382dba2216d0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 44,
                column: "PermissionControllerActionGuid",
                value: new Guid("1b005521-ef40-4d36-8180-63970a515e5d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 45,
                column: "PermissionControllerActionGuid",
                value: new Guid("90db0202-373e-4fd4-a2e0-ef3e026fb7e2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 46,
                column: "PermissionControllerActionGuid",
                value: new Guid("b8bbaf06-162d-4687-a18f-67445bb32177"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 47,
                column: "PermissionControllerActionGuid",
                value: new Guid("0ec5d4c0-f6a0-4989-bdfb-a4b868223698"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 48,
                column: "PermissionControllerActionGuid",
                value: new Guid("d568ac21-ec0b-4596-ae7d-3bda3a73d92d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 49,
                column: "PermissionControllerActionGuid",
                value: new Guid("10864b93-1b5e-48d2-8244-17deba0a329c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 50,
                column: "PermissionControllerActionGuid",
                value: new Guid("9da6d793-3ae9-48cf-b159-32b07fc1484c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 51,
                column: "PermissionControllerActionGuid",
                value: new Guid("632a02f3-1686-44e7-aef4-6f8796e31afe"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 52,
                column: "PermissionControllerActionGuid",
                value: new Guid("725ea027-fc1b-4649-9c10-4fe0d61f69a7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 53,
                column: "PermissionControllerActionGuid",
                value: new Guid("ca202a49-6dba-40f7-a830-3f4a134069b9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 54,
                column: "PermissionControllerActionGuid",
                value: new Guid("d988bc96-8951-48a9-843c-401af17f3820"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 55,
                column: "PermissionControllerActionGuid",
                value: new Guid("c286828d-7f7e-48e6-bdce-cd5e99b994e7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 56,
                column: "PermissionControllerActionGuid",
                value: new Guid("afdb1f25-0f6e-4604-9aca-1b72aa014df6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 57,
                column: "PermissionControllerActionGuid",
                value: new Guid("5151a5c9-6aff-43f9-8434-09d078931b00"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 58,
                column: "PermissionControllerActionGuid",
                value: new Guid("7dd2baef-2185-4138-91f7-d1ca12b0366b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 59,
                column: "PermissionControllerActionGuid",
                value: new Guid("ae050c98-a334-4197-ae38-6052c2894c4d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 60,
                column: "PermissionControllerActionGuid",
                value: new Guid("7b1eed09-72e0-446f-8f4b-9708954db9f4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 61,
                column: "PermissionControllerActionGuid",
                value: new Guid("320c2ea3-ae12-4a7b-83dd-ace4bd8508e5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 62,
                column: "PermissionControllerActionGuid",
                value: new Guid("7bbac4f2-6d7b-41ad-ab71-6bc6d9aaf677"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 63,
                column: "PermissionControllerActionGuid",
                value: new Guid("1baefd41-b30c-4e74-93b9-bc0193e46a35"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 64,
                column: "PermissionControllerActionGuid",
                value: new Guid("c426a2f7-de38-4ae8-bea8-9bab82179ca1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 65,
                column: "PermissionControllerActionGuid",
                value: new Guid("b9405cc0-a695-4990-ad6e-b5b28f679a77"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 66,
                column: "PermissionControllerActionGuid",
                value: new Guid("ae7aa677-d93d-4d85-982a-1b9e16b6b4a7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 67,
                column: "PermissionControllerActionGuid",
                value: new Guid("538c37bc-0dd0-459e-ac4a-00aaadc3736e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 68,
                column: "PermissionControllerActionGuid",
                value: new Guid("fe0c0f21-fb45-463e-8835-b4600f0d2f7f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 69,
                column: "PermissionControllerActionGuid",
                value: new Guid("4fd03d20-6948-43c4-b501-bb9250f9423e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 70,
                column: "PermissionControllerActionGuid",
                value: new Guid("e8b2ff2e-297b-414e-9657-e790fbade981"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 71,
                column: "PermissionControllerActionGuid",
                value: new Guid("cbd8fd6a-7269-4f79-a8e3-5ca127aa2303"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 72,
                column: "PermissionControllerActionGuid",
                value: new Guid("f8ba1903-6cc3-43e9-89e7-52c9ec5ffa5b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 73,
                column: "PermissionControllerActionGuid",
                value: new Guid("0fd96c05-298a-4fc6-8dfa-64a8c231c8b9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 74,
                column: "PermissionControllerActionGuid",
                value: new Guid("b3d117be-8898-49d4-b0bf-1a289f730421"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 75,
                column: "PermissionControllerActionGuid",
                value: new Guid("bd1a5a4f-3cb7-4317-a8ec-a20292aa7461"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 76,
                column: "PermissionControllerActionGuid",
                value: new Guid("deaf827b-5a54-4858-952e-3048ed687063"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 77,
                column: "PermissionControllerActionGuid",
                value: new Guid("385d5c1c-2478-4b3b-baa6-23f398d520bc"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 78,
                column: "PermissionControllerActionGuid",
                value: new Guid("b617235c-922b-46d8-97b4-449568f62d86"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 79,
                column: "PermissionControllerActionGuid",
                value: new Guid("10caf099-7745-4496-8ec5-e30a151f6511"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 80,
                column: "PermissionControllerActionGuid",
                value: new Guid("11440526-0a60-4cf2-b2b3-a4c934e1cdc3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 81,
                column: "PermissionControllerActionGuid",
                value: new Guid("c72ac1c0-b72c-478b-8926-7b79fdd4966b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 82,
                column: "PermissionControllerActionGuid",
                value: new Guid("5e59a95f-69fe-4a2d-9d0c-421eef458aed"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 83,
                column: "PermissionControllerActionGuid",
                value: new Guid("15719f3c-a2c4-4fb7-9da3-4d1f3c2788e8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 84,
                column: "PermissionControllerActionGuid",
                value: new Guid("06989fb4-7ec7-451a-ac8c-11bcbe84af11"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 85,
                column: "PermissionControllerActionGuid",
                value: new Guid("e4a165d7-94de-4d42-83ce-e151d0fd963f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 86,
                column: "PermissionControllerActionGuid",
                value: new Guid("02b12034-427f-4766-937b-09dc031c0582"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 87,
                column: "PermissionControllerActionGuid",
                value: new Guid("46dbb57a-f532-4e34-8ef9-49dffe49b89d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 88,
                column: "PermissionControllerActionGuid",
                value: new Guid("38cda376-8493-488a-8587-caff1fb388d5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 89,
                column: "PermissionControllerActionGuid",
                value: new Guid("d9c7c6e2-c3ad-4703-a7a6-f6cc4c4588bc"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 90,
                column: "PermissionControllerActionGuid",
                value: new Guid("f853e0da-4c65-463b-b314-b4989c7c199c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 91,
                column: "PermissionControllerActionGuid",
                value: new Guid("dc2116e4-7305-4632-9c0b-2487e773c872"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 92,
                column: "PermissionControllerActionGuid",
                value: new Guid("f0ece622-dba1-451a-b79a-243446889025"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 93,
                column: "PermissionControllerActionGuid",
                value: new Guid("bf41ebf8-b54f-41c4-a7a5-47de2aff58ac"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 94,
                column: "PermissionControllerActionGuid",
                value: new Guid("ada10487-b54f-42c1-b901-e4d42a660eec"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 95,
                column: "PermissionControllerActionGuid",
                value: new Guid("38d26ca8-2a0e-4002-bca3-43b7571b7e98"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 96,
                column: "PermissionControllerActionGuid",
                value: new Guid("3d0698e4-74fb-4a02-9e3e-c055917aff8c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 97,
                column: "PermissionControllerActionGuid",
                value: new Guid("551a458f-ae46-4c09-9adc-d29a0d4bf1b5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 98,
                column: "PermissionControllerActionGuid",
                value: new Guid("9346e751-f4cf-4f2f-b089-bd212a20e49b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 99,
                column: "PermissionControllerActionGuid",
                value: new Guid("0c780f93-0d23-4d99-8fef-ae6cb4f17c48"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 100,
                column: "PermissionControllerActionGuid",
                value: new Guid("ca104b0a-a859-4379-bb4e-f1e66b143c97"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 101,
                column: "PermissionControllerActionGuid",
                value: new Guid("f52707ce-6873-4136-8d35-641a15be2486"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 102,
                column: "PermissionControllerActionGuid",
                value: new Guid("d6fbe624-3499-44b6-a8f6-2883873aca8d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 103,
                column: "PermissionControllerActionGuid",
                value: new Guid("4dd4f9cb-8cd1-4a05-8c33-c072277cbe28"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 104,
                column: "PermissionControllerActionGuid",
                value: new Guid("8af3a34d-8a38-4650-be09-871e7bb048eb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 105,
                column: "PermissionControllerActionGuid",
                value: new Guid("63948b6f-e883-4819-8462-6a50e82e6a9d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 106,
                column: "PermissionControllerActionGuid",
                value: new Guid("69c7fd5d-1065-4c04-80e7-87701eac419a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 107,
                column: "PermissionControllerActionGuid",
                value: new Guid("fcb0dd3a-5569-4db8-8887-de4785490172"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 108,
                column: "PermissionControllerActionGuid",
                value: new Guid("f5b67260-0203-461d-b670-f2f17a2137b6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 109,
                column: "PermissionControllerActionGuid",
                value: new Guid("7d46e3f1-310f-42a3-b628-897831c5f91c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 110,
                column: "PermissionControllerActionGuid",
                value: new Guid("062e921f-260f-41ad-8049-35601c7b5940"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 111,
                column: "PermissionControllerActionGuid",
                value: new Guid("50125032-be72-4b0c-bb61-a04cac2a63e8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 112,
                column: "PermissionControllerActionGuid",
                value: new Guid("fc435bdd-a08c-4e04-bc70-40f769cfc679"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 113,
                column: "PermissionControllerActionGuid",
                value: new Guid("8ad6fd8c-cd15-40d9-8ba9-5663f25fb6b5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 114,
                column: "PermissionControllerActionGuid",
                value: new Guid("d7f4a1f8-502d-45af-92b6-eeee86435c96"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 115,
                column: "PermissionControllerActionGuid",
                value: new Guid("27157cd5-d21b-441b-8963-da0b99be8b7e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 116,
                column: "PermissionControllerActionGuid",
                value: new Guid("7d0a48fc-02c3-47b2-862e-1ba09e384144"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 117,
                column: "PermissionControllerActionGuid",
                value: new Guid("3c3609b9-eb64-4b70-bb33-eba777bde576"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 118,
                column: "PermissionControllerActionGuid",
                value: new Guid("74af7259-7ab4-40c4-b20d-a11f7a2787c3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 119,
                column: "PermissionControllerActionGuid",
                value: new Guid("b0393c19-388e-4de3-8962-178d2f73c13c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 120,
                column: "PermissionControllerActionGuid",
                value: new Guid("9aa9fc97-a200-40a6-8b3d-90a21558cb3c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 121,
                column: "PermissionControllerActionGuid",
                value: new Guid("89ee596d-ea88-48ed-90a2-03ad71004cee"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 122,
                column: "PermissionControllerActionGuid",
                value: new Guid("fdd255ff-d0b7-4b55-9dd8-9ed3c934d29c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 123,
                column: "PermissionControllerActionGuid",
                value: new Guid("c047d599-1f5b-4f25-b913-ead6aa146219"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 124,
                column: "PermissionControllerActionGuid",
                value: new Guid("f28e2c06-b1f6-4993-a428-88c00b153994"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 125,
                column: "PermissionControllerActionGuid",
                value: new Guid("d940c4a6-8271-40cb-ac90-0727fbc346d9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 126,
                column: "PermissionControllerActionGuid",
                value: new Guid("984ffa5e-e1d2-4090-a6f7-3548d2eb7971"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 127,
                column: "PermissionControllerActionGuid",
                value: new Guid("79badb6d-efdd-4d29-8b41-b830a15994a1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 128,
                column: "PermissionControllerActionGuid",
                value: new Guid("bb2c66d5-00ca-4e3e-a7cc-d7b69c3e5e90"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 129,
                column: "PermissionControllerActionGuid",
                value: new Guid("54e0a417-1a07-4877-8f56-b7f7035e387d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 130,
                column: "PermissionControllerActionGuid",
                value: new Guid("644eb310-516d-49d7-a641-57cfc0b434ca"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 131,
                column: "PermissionControllerActionGuid",
                value: new Guid("e2544330-02f4-42b7-8eba-17347987269e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 132,
                column: "PermissionControllerActionGuid",
                value: new Guid("03eb7922-a50f-47ed-bfd7-5544898ea9a4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 133,
                column: "PermissionControllerActionGuid",
                value: new Guid("2510be7d-fd63-44cc-aa17-12faeef42d0e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 134,
                column: "PermissionControllerActionGuid",
                value: new Guid("7fb5e9dd-941e-4acb-bec7-06c5cf58ce78"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 135,
                column: "PermissionControllerActionGuid",
                value: new Guid("9c0e0dfb-8a5e-4493-8fe1-dc7c07c50dd6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 136,
                column: "PermissionControllerActionGuid",
                value: new Guid("a96d483e-55c9-4acd-88ee-a769b97dff4a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 137,
                column: "PermissionControllerActionGuid",
                value: new Guid("887b9af7-2448-40f1-a4fa-1e9a6a9ce0cf"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 138,
                column: "PermissionControllerActionGuid",
                value: new Guid("2da7b556-f817-424f-af90-7f2badf62024"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 139,
                column: "PermissionControllerActionGuid",
                value: new Guid("cd2d0185-1440-46ee-bb83-804e7b6b78d9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 140,
                column: "PermissionControllerActionGuid",
                value: new Guid("9242d982-bb08-4842-bd20-3f636e86c617"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 141,
                column: "PermissionControllerActionGuid",
                value: new Guid("fa14c92a-7f9b-4db5-874e-ea8c1f97fea3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 142,
                column: "PermissionControllerActionGuid",
                value: new Guid("a2f2ce98-c8c8-4981-8ae1-d87a25d69d0c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 143,
                column: "PermissionControllerActionGuid",
                value: new Guid("61fe7af4-2abf-4618-ba74-a2311714edc2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 144,
                column: "PermissionControllerActionGuid",
                value: new Guid("bb82ca5c-0425-4bcb-9022-4f087850fdc8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 145,
                column: "PermissionControllerActionGuid",
                value: new Guid("12d261fb-5612-466a-addb-0849f7b2a28b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 146,
                column: "PermissionControllerActionGuid",
                value: new Guid("eca0bb28-f7fc-49e0-bcdf-8c906e9132bf"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 147,
                column: "PermissionControllerActionGuid",
                value: new Guid("867e0d67-6321-4d40-969d-24974866d0f9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 148,
                column: "PermissionControllerActionGuid",
                value: new Guid("bba5cc4e-5e54-4bed-b302-d24cba08a8f9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 149,
                column: "PermissionControllerActionGuid",
                value: new Guid("3f08f895-81c9-4b2a-a545-e7447049840d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 150,
                column: "PermissionControllerActionGuid",
                value: new Guid("8537e244-6671-4a74-8bd0-2a8cdf55ca0d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 151,
                column: "PermissionControllerActionGuid",
                value: new Guid("1b851234-b7e8-465a-8807-24ef66abfafd"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 152,
                column: "PermissionControllerActionGuid",
                value: new Guid("9f3db34a-c5ef-4c80-90f0-ccdb577a3cc2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 153,
                column: "PermissionControllerActionGuid",
                value: new Guid("999d1199-a282-4fda-bde8-fb5e40405c46"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 154,
                column: "PermissionControllerActionGuid",
                value: new Guid("7f89713f-d961-4c97-af47-b20b88457b4d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 155,
                column: "PermissionControllerActionGuid",
                value: new Guid("d768ae33-5bcb-4418-a79a-c7c86f0a519b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 156,
                column: "PermissionControllerActionGuid",
                value: new Guid("3b063ece-5989-4a73-8946-28dde4c1993b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 157,
                column: "PermissionControllerActionGuid",
                value: new Guid("9965af78-e9c5-42cb-baa9-57e0a234fade"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 158,
                column: "PermissionControllerActionGuid",
                value: new Guid("5ad08106-c459-4657-84ec-397bd2ce1680"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 159,
                column: "PermissionControllerActionGuid",
                value: new Guid("98c7c9ea-c549-44ac-9cbe-b4c3c5091924"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 160,
                column: "PermissionControllerActionGuid",
                value: new Guid("2ada0132-0295-476b-b7c3-cdbeb70765b9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 161,
                column: "PermissionControllerActionGuid",
                value: new Guid("d09c8764-a894-47ab-a92a-137e13697f6d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 162,
                column: "PermissionControllerActionGuid",
                value: new Guid("aee8c4f4-ec91-4efa-9c80-9ee18e619194"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 163,
                column: "PermissionControllerActionGuid",
                value: new Guid("2348ba6e-dab6-4e82-8c2b-020feab6becc"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 164,
                column: "PermissionControllerActionGuid",
                value: new Guid("ef1b47bb-a50b-460b-8335-ef1976bcbfc0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 165,
                column: "PermissionControllerActionGuid",
                value: new Guid("2e46718f-e1c7-448f-adbc-640e632b1223"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 166,
                column: "PermissionControllerActionGuid",
                value: new Guid("dc1c28a5-733a-436e-a1cc-624a9bf93ef0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 167,
                column: "PermissionControllerActionGuid",
                value: new Guid("043dc54b-325a-47b2-abd4-9e623232a5c5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 168,
                column: "PermissionControllerActionGuid",
                value: new Guid("84efa8c6-3dc1-4a58-a353-1aa7ddf86063"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 169,
                column: "PermissionControllerActionGuid",
                value: new Guid("f48c209b-061f-4f5b-a366-021f8892e88b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 170,
                column: "PermissionControllerActionGuid",
                value: new Guid("f9e92ea8-3b96-469c-b43f-f8f73921029c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 171,
                column: "PermissionControllerActionGuid",
                value: new Guid("da470cde-c0d8-444e-be36-cd301bae6498"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 172,
                column: "PermissionControllerActionGuid",
                value: new Guid("f0f12da9-77fa-4683-8b80-0886ee83031c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 173,
                column: "PermissionControllerActionGuid",
                value: new Guid("7dd07e03-7533-4adb-8aae-0311b103d90a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 174,
                column: "PermissionControllerActionGuid",
                value: new Guid("c5ef5590-ca59-4c21-a6c3-d3d5c6824545"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 175,
                column: "PermissionControllerActionGuid",
                value: new Guid("ed5c6223-efc1-4afb-8390-725b5f888025"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 176,
                column: "PermissionControllerActionGuid",
                value: new Guid("e02c4ac3-3c1b-4759-a4d4-9b3f0b674706"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 177,
                column: "PermissionControllerActionGuid",
                value: new Guid("bee4ebae-4c62-47e8-ba41-b5bb25c29c4c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 178,
                column: "PermissionControllerActionGuid",
                value: new Guid("54e3e1c0-3b8e-476b-91ba-f92cfd7467e8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 179,
                column: "PermissionControllerActionGuid",
                value: new Guid("8fbf4d6c-f9c7-429d-b6d0-6a30140ad9a3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 180,
                column: "PermissionControllerActionGuid",
                value: new Guid("24b46f2e-08c3-42ce-b91a-8c93f4e36029"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 181,
                column: "PermissionControllerActionGuid",
                value: new Guid("d0882322-f8ae-4415-a908-435c3203314a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 182,
                column: "PermissionControllerActionGuid",
                value: new Guid("f6ee8516-7f47-4f49-92df-0b55b1709002"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 183,
                column: "PermissionControllerActionGuid",
                value: new Guid("e10470cc-ff17-40e9-9a55-5fc393f7c4a3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 184,
                column: "PermissionControllerActionGuid",
                value: new Guid("0927df7e-5174-47be-9789-bb00ac5d938e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 185,
                column: "PermissionControllerActionGuid",
                value: new Guid("768cecc0-9ac8-4aaa-8b1e-e65311fef219"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 186,
                column: "PermissionControllerActionGuid",
                value: new Guid("7df885b1-038a-49aa-8c6f-274761b11ce5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 187,
                column: "PermissionControllerActionGuid",
                value: new Guid("6f6f4e61-1e84-4751-bff5-8fba00510862"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 188,
                column: "PermissionControllerActionGuid",
                value: new Guid("80f5c811-5d08-450a-a633-11cd630fcd64"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 189,
                column: "PermissionControllerActionGuid",
                value: new Guid("b9be018c-2b0c-4215-a4bf-00776275fc07"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 190,
                column: "PermissionControllerActionGuid",
                value: new Guid("9b651c9f-4894-4d44-aa26-333235ecd709"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 191,
                column: "PermissionControllerActionGuid",
                value: new Guid("b5164bab-f174-4fc2-8b8a-61927cbeb438"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 192,
                column: "PermissionControllerActionGuid",
                value: new Guid("fa25775d-2a17-4f4a-90bf-afb8928ce5d9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 193,
                column: "PermissionControllerActionGuid",
                value: new Guid("83070cc7-ea3b-42c0-b73a-ea3ca5260f88"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 194,
                column: "PermissionControllerActionGuid",
                value: new Guid("8b240ea3-7ba9-4480-983f-70bd7d0b40bd"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 195,
                column: "PermissionControllerActionGuid",
                value: new Guid("5695a196-beaa-4c36-a3fb-ff2fb2543f65"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 196,
                column: "PermissionControllerActionGuid",
                value: new Guid("82e66d1a-78d3-490f-be5a-b0cb66923633"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 197,
                column: "PermissionControllerActionGuid",
                value: new Guid("5023d7f1-cfbf-4cd9-ad9a-74099ae41936"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 198,
                column: "PermissionControllerActionGuid",
                value: new Guid("b60c9ecf-e676-4c68-b7d2-0dcf9c5a463e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 199,
                column: "PermissionControllerActionGuid",
                value: new Guid("72caddf4-1cd3-4b11-8c4b-e7afedd0225e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 200,
                column: "PermissionControllerActionGuid",
                value: new Guid("e15ca399-d60f-4f26-946e-18278694d16c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 201,
                column: "PermissionControllerActionGuid",
                value: new Guid("a0c1ec49-387d-40db-a34a-b2ebb0e6155b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 202,
                column: "PermissionControllerActionGuid",
                value: new Guid("7fa49527-2769-40cc-ab5e-b3be0b0c4bb2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 203,
                column: "PermissionControllerActionGuid",
                value: new Guid("7b11946b-48c6-4697-bfb1-17b2f8c4d32f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 204,
                column: "PermissionControllerActionGuid",
                value: new Guid("f56ef11d-a028-4236-8dd6-badbb918b851"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 205,
                column: "PermissionControllerActionGuid",
                value: new Guid("491d8fbb-7cf9-4327-bbbd-fbb2dc2c7081"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 206,
                column: "PermissionControllerActionGuid",
                value: new Guid("bcaf4d73-dd0a-4577-bdd2-d193eee0d338"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 207,
                column: "PermissionControllerActionGuid",
                value: new Guid("3a8965e3-f2e2-4c43-8714-fd5509184557"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 208,
                column: "PermissionControllerActionGuid",
                value: new Guid("005ceef8-c8f0-4106-8845-8730afc139d5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 209,
                column: "PermissionControllerActionGuid",
                value: new Guid("7d9d65ad-fc0b-4f74-8755-6e4ad98823bd"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 210,
                column: "PermissionControllerActionGuid",
                value: new Guid("e36e7750-c1f5-42c8-bdf4-8d6a00f28bdf"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 211,
                column: "PermissionControllerActionGuid",
                value: new Guid("8775a2ae-b092-404f-8f8b-7e7fb4d332f9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 212,
                column: "PermissionControllerActionGuid",
                value: new Guid("33af0eeb-75da-4611-82a9-e371055b925c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 213,
                column: "PermissionControllerActionGuid",
                value: new Guid("8d45e24f-dbad-44a7-9132-775405128ef2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 214,
                column: "PermissionControllerActionGuid",
                value: new Guid("a4f548f8-4fcf-4170-8c7e-816ef0dadeb0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 215,
                column: "PermissionControllerActionGuid",
                value: new Guid("f1e67322-f4a3-4b23-ac65-5b7abfea1c09"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 216,
                column: "PermissionControllerActionGuid",
                value: new Guid("6d738d59-207d-472e-a1f0-e2b71af564f7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 217,
                column: "PermissionControllerActionGuid",
                value: new Guid("f364d1b9-2d0a-4aff-9cde-581bbefb7672"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 218,
                column: "PermissionControllerActionGuid",
                value: new Guid("fca777e3-b79b-44f1-997b-2c48397f0eec"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 219,
                column: "PermissionControllerActionGuid",
                value: new Guid("cf845f1c-ec9a-48cf-8aff-3e664b2cc10f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 220,
                column: "PermissionControllerActionGuid",
                value: new Guid("dfeac5d5-fb1f-475e-b9d4-9cecb2773a75"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 221,
                column: "PermissionControllerActionGuid",
                value: new Guid("ef4241e8-9621-4ec6-b45a-a80c1fa6e607"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 222,
                column: "PermissionControllerActionGuid",
                value: new Guid("860dc688-7142-41c2-bac4-99d7cd5f44d5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 223,
                column: "PermissionControllerActionGuid",
                value: new Guid("e3958634-830c-4277-bbf7-851c1980209a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 224,
                column: "PermissionControllerActionGuid",
                value: new Guid("e2a4879c-184b-48f3-856c-3cb411e19510"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 225,
                column: "PermissionControllerActionGuid",
                value: new Guid("28019ae8-6e07-4e99-88f7-802b2964a3fe"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 226,
                column: "PermissionControllerActionGuid",
                value: new Guid("df96819e-28c1-4dc8-8bd2-a3864d337c29"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 227,
                column: "PermissionControllerActionGuid",
                value: new Guid("f9b8c31a-de28-41d4-a3c7-83a54412e5e8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 228,
                column: "PermissionControllerActionGuid",
                value: new Guid("85a19e29-6e57-49c8-9531-b56bed299fae"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 229,
                column: "PermissionControllerActionGuid",
                value: new Guid("a7f1b085-ab56-46f9-a768-2c992ac9e4ed"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 230,
                column: "PermissionControllerActionGuid",
                value: new Guid("16ec2ff0-4483-452f-8319-e536c8e9ca35"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 231,
                column: "PermissionControllerActionGuid",
                value: new Guid("a6985888-4e86-48af-a2c1-dca5ad35cb2d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 232,
                column: "PermissionControllerActionGuid",
                value: new Guid("f153b1e0-a242-4d35-bd70-8e4db9c8326a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 233,
                column: "PermissionControllerActionGuid",
                value: new Guid("38bfd67f-3ca0-4536-9978-c1ded97559a2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 234,
                column: "PermissionControllerActionGuid",
                value: new Guid("751aa730-d5ab-49f0-877c-aeb5fb6ceda2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 235,
                column: "PermissionControllerActionGuid",
                value: new Guid("bbf2e696-ffca-4fef-bf34-a54d8d3f4d24"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 236,
                column: "PermissionControllerActionGuid",
                value: new Guid("90ac6c82-d46d-4116-9051-1b3f7964a9e2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 237,
                column: "PermissionControllerActionGuid",
                value: new Guid("fc92d6ed-b0a2-461d-9077-99652ba861e1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 238,
                column: "PermissionControllerActionGuid",
                value: new Guid("e9838922-ed06-4112-8cb3-9a4065e5e614"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 239,
                column: "PermissionControllerActionGuid",
                value: new Guid("2f702091-7aff-481f-86d1-37ca4cf18da9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 240,
                column: "PermissionControllerActionGuid",
                value: new Guid("c45a5db8-9e42-48e0-9f8d-1f4707d6648d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 241,
                column: "PermissionControllerActionGuid",
                value: new Guid("66bb020b-36ba-45dc-9091-4c686e77a056"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 242,
                column: "PermissionControllerActionGuid",
                value: new Guid("dffd0363-c0e4-4100-9a62-6da25681136b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 243,
                column: "PermissionControllerActionGuid",
                value: new Guid("e17f9f31-d780-4c23-9a5d-c896e5bcca18"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 244,
                column: "PermissionControllerActionGuid",
                value: new Guid("027ab280-adb6-4ec7-9df0-a4475113d04c"));

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "City",
                keyColumn: "CityID",
                keyValue: 1,
                columns: new[] { "CityGuid", "CreateDate", "EnableDate" },
                values: new object[] { new Guid("65a754e1-8af8-4ff0-bcfa-1708934da6cf"), new DateTime(2022, 3, 10, 10, 26, 27, 899, DateTimeKind.Local).AddTicks(1146), new DateTime(2022, 3, 10, 10, 26, 27, 899, DateTimeKind.Local).AddTicks(1936) });

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "Country",
                keyColumn: "CountryID",
                keyValue: 1,
                columns: new[] { "CountryGuid", "CreateDate", "EnableDate" },
                values: new object[] { new Guid("0b444289-4f8e-41e3-be8e-135045c7b233"), new DateTime(2022, 3, 10, 10, 26, 27, 895, DateTimeKind.Local).AddTicks(7282), new DateTime(2022, 3, 10, 10, 26, 27, 897, DateTimeKind.Local).AddTicks(847) });

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "Departments",
                keyColumn: "DepartmentsID",
                keyValue: 1,
                columns: new[] { "CreateDate", "DepartmentsGuid", "EnableDate" },
                values: new object[] { new DateTime(2022, 3, 10, 10, 26, 27, 903, DateTimeKind.Local).AddTicks(6492), new Guid("eaa70be9-511e-4050-8f63-98a694c3d199"), new DateTime(2022, 3, 10, 10, 26, 27, 903, DateTimeKind.Local).AddTicks(7232) });

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "Jobs",
                keyColumn: "JobsID",
                keyValue: 1,
                columns: new[] { "CreateDate", "EnableDate", "JobsGuid" },
                values: new object[] { new DateTime(2022, 3, 10, 10, 26, 27, 902, DateTimeKind.Local).AddTicks(4762), new DateTime(2022, 3, 10, 10, 26, 27, 902, DateTimeKind.Local).AddTicks(5518), new Guid("d280d9df-2aa4-427e-9e67-3f936678bb09") });

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "MainCategory",
                keyColumn: "MainCategoryID",
                keyValue: 1,
                columns: new[] { "CreateDate", "EnableDate", "MainCategoryGuid" },
                values: new object[] { new DateTime(2022, 3, 10, 10, 26, 27, 902, DateTimeKind.Local).AddTicks(9975), new DateTime(2022, 3, 10, 10, 26, 27, 903, DateTimeKind.Local).AddTicks(994), new Guid("6c28f0d0-22b9-4cbe-a3e6-cdee74b2d322") });

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "Nationality",
                keyColumn: "NationalityID",
                keyValue: 1,
                columns: new[] { "CreateDate", "EnableDate", "NationalityGuid" },
                values: new object[] { new DateTime(2022, 3, 10, 10, 26, 27, 899, DateTimeKind.Local).AddTicks(9553), new DateTime(2022, 3, 10, 10, 26, 27, 900, DateTimeKind.Local).AddTicks(332), new Guid("fea95120-eb4a-4c5e-af3e-5c2d788c6ab7") });

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "Region",
                keyColumn: "RegionID",
                keyValue: 1,
                columns: new[] { "CreateDate", "EnableDate", "RegionGuid" },
                values: new object[] { new DateTime(2022, 3, 10, 10, 26, 27, 897, DateTimeKind.Local).AddTicks(8914), new DateTime(2022, 3, 10, 10, 26, 27, 897, DateTimeKind.Local).AddTicks(9713), new Guid("3dc99866-dbb3-4d7b-87fd-ed06737c1ace") });
        }
    }
}
