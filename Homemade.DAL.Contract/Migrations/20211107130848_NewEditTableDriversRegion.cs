using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Homemade.DAL.Contract.Migrations
{
    public partial class NewEditTableDriversRegion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RegionID",
                schema: "Driver",
                table: "Drivers",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "f9de6885-d7a5-478a-82e3-7b03322ac470");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "56a571f9-7909-40d3-85c9-566de8cac2cb");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "5f06dc58-272d-4965-a65e-7ade5ad72304");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "d4c46e11-20eb-4b57-af9a-574cb31eb73d");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "85c2d14a-a036-4bed-b2e2-20067e2a2b14", "6d960f7a-b434-4e73-9609-c0a6ccc97542" });

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "DeliverySetting",
                keyColumn: "DeliverySettingID",
                keyValue: 1,
                column: "DeliverySettingGuid",
                value: new Guid("163fa158-0a82-4166-b385-1bca6f557e76"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 1,
                column: "TransactionTypeGuid",
                value: new Guid("e1d8a2b1-b4ef-41c9-8b99-c04b18eaf386"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 2,
                column: "TransactionTypeGuid",
                value: new Guid("8a1d71b2-88ff-45c4-8411-47a780e935aa"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 3,
                column: "TransactionTypeGuid",
                value: new Guid("52b169f8-17f8-442b-bb14-26f7bde97a9a"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 4,
                column: "TransactionTypeGuid",
                value: new Guid("e5a9340d-29ab-4453-916f-379b4e09a124"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 5,
                column: "TransactionTypeGuid",
                value: new Guid("7bb132b0-c735-4ff5-ae1b-dd14d0db42e5"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 6,
                column: "TransactionTypeGuid",
                value: new Guid("cb696fc3-5631-43c6-a431-757e9a207aa2"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 7,
                column: "TransactionTypeGuid",
                value: new Guid("58fa552f-8a47-4e06-9d58-d050cfa5a2f2"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 8,
                column: "TransactionTypeGuid",
                value: new Guid("1b45b5f2-459c-4436-a0ff-5dedb9d94872"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 9,
                column: "TransactionTypeGuid",
                value: new Guid("b14481ad-2c49-44f7-8b93-a257b916c6ab"));

            migrationBuilder.UpdateData(
                schema: "Emp",
                table: "Employees",
                keyColumn: "EntityEmpID",
                keyValue: 1,
                columns: new[] { "CreateDate", "EnableDate" },
                values: new object[] { new DateTime(2021, 11, 7, 15, 8, 45, 228, DateTimeKind.Local).AddTicks(7351), new DateTime(2021, 11, 7, 15, 8, 45, 228, DateTimeKind.Local).AddTicks(7697) });

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionAction",
                keyColumn: "PermissionActionID",
                keyValue: 1,
                column: "PermissionActionGuid",
                value: new Guid("40150bb1-89a4-4dc3-9e83-b7ac921ab9c3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionAction",
                keyColumn: "PermissionActionID",
                keyValue: 2,
                column: "PermissionActionGuid",
                value: new Guid("b6a675ee-4301-427a-9496-a74a941155e9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionAction",
                keyColumn: "PermissionActionID",
                keyValue: 3,
                column: "PermissionActionGuid",
                value: new Guid("21bd1356-507a-4ea3-8a62-0a12c53d93ec"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionAction",
                keyColumn: "PermissionActionID",
                keyValue: 4,
                column: "PermissionActionGuid",
                value: new Guid("be7288b4-2553-44be-9def-9d00db4dc7e4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 1,
                column: "PermissionControllerGuid",
                value: new Guid("dcbf0856-1d27-4ab3-8e4d-b0c6862df77f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 2,
                column: "PermissionControllerGuid",
                value: new Guid("d4bf72b1-320e-4be7-b9ae-af978eb51bef"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 3,
                column: "PermissionControllerGuid",
                value: new Guid("c553a6b9-603d-4c4e-8d31-26330b9a4150"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 4,
                column: "PermissionControllerGuid",
                value: new Guid("6ac0e9bc-1491-4547-8d66-0e2ad148bf68"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 5,
                column: "PermissionControllerGuid",
                value: new Guid("1be398dc-be23-449e-893b-ab54d8c23814"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 6,
                column: "PermissionControllerGuid",
                value: new Guid("559b0b84-5ca6-40f5-8dda-66cf19d5775b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 7,
                column: "PermissionControllerGuid",
                value: new Guid("36cb61f5-fcab-46ff-8c69-c90066dae9f2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 8,
                column: "PermissionControllerGuid",
                value: new Guid("a7f3bdd3-f38d-4ed7-bfde-13966dd88aec"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 9,
                column: "PermissionControllerGuid",
                value: new Guid("f1b7e471-fa78-468c-959c-18fb28f01aee"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 10,
                column: "PermissionControllerGuid",
                value: new Guid("4284cead-8bd5-49ac-a78c-ddb2c40f1972"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 11,
                column: "PermissionControllerGuid",
                value: new Guid("9953b8fa-dfa3-4049-9c62-352a82c76c36"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 12,
                column: "PermissionControllerGuid",
                value: new Guid("a9707342-7809-49c2-a828-e1e314a62b73"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 13,
                column: "PermissionControllerGuid",
                value: new Guid("492ff6db-7bc2-4da9-8896-ad542cfc9e43"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 14,
                column: "PermissionControllerGuid",
                value: new Guid("af301314-6bf0-4c6c-bb47-77bdeeb6a6a6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 15,
                column: "PermissionControllerGuid",
                value: new Guid("3191d3c8-d39d-4db2-8beb-5124877a7e49"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 16,
                column: "PermissionControllerGuid",
                value: new Guid("f9bbbcf2-0c46-4ada-8cdd-526bd5630bd9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 17,
                column: "PermissionControllerGuid",
                value: new Guid("e2f7c8e0-61cd-42ee-8074-b5a14185b186"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 18,
                column: "PermissionControllerGuid",
                value: new Guid("1ab9bd5b-6833-4280-8f04-5d2f022c2f56"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 19,
                column: "PermissionControllerGuid",
                value: new Guid("93bc14b8-7cf5-4357-a64e-9b7f473ae814"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 20,
                column: "PermissionControllerGuid",
                value: new Guid("ff3a8be6-e790-4a59-82c6-dcc8ee0a8359"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 21,
                column: "PermissionControllerGuid",
                value: new Guid("6ac98527-a319-478a-aa50-da1d2aad7532"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 22,
                column: "PermissionControllerGuid",
                value: new Guid("63ff09a5-78bd-4dc6-bc75-b38edfefbf24"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 23,
                column: "PermissionControllerGuid",
                value: new Guid("93d5bf2b-845b-41f8-969a-28b95ace7d65"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 24,
                column: "PermissionControllerGuid",
                value: new Guid("18aefc37-896d-44ea-8a73-48d00e8fd32a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 25,
                column: "PermissionControllerGuid",
                value: new Guid("ee7f37c3-b182-4651-ac92-6cf1b097bd50"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 26,
                column: "PermissionControllerGuid",
                value: new Guid("85b3c24b-170c-4efe-bccc-dd0d13d6154c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 27,
                column: "PermissionControllerGuid",
                value: new Guid("175df1b7-ccb4-43ba-a51c-4228547f48c4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 28,
                column: "PermissionControllerGuid",
                value: new Guid("f26c140d-ebf5-4172-8dba-e443b902a7fa"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 29,
                column: "PermissionControllerGuid",
                value: new Guid("27263249-017a-4295-8299-35f18088b9d7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 30,
                column: "PermissionControllerGuid",
                value: new Guid("6caa6e30-c96f-4210-a957-224088f1cd49"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 31,
                column: "PermissionControllerGuid",
                value: new Guid("3f4610fc-6919-4f62-9cd8-bae18b89a7cd"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 32,
                column: "PermissionControllerGuid",
                value: new Guid("2186ea30-7826-4ada-8492-563abb00c6c4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 33,
                column: "PermissionControllerGuid",
                value: new Guid("2674a760-11ef-4b3a-8855-b68287fcfa06"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 34,
                column: "PermissionControllerGuid",
                value: new Guid("d2d3b243-aa5c-49e6-8c86-190546eaba46"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 35,
                column: "PermissionControllerGuid",
                value: new Guid("2b809850-ff81-496f-a1ae-6ad8a0777911"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 36,
                column: "PermissionControllerGuid",
                value: new Guid("79642f87-2d0d-482f-b9d0-125f0f905504"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 37,
                column: "PermissionControllerGuid",
                value: new Guid("4842c44f-0b59-40c0-8ab4-aa5b854937f1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 38,
                column: "PermissionControllerGuid",
                value: new Guid("48c4c4f2-a578-4d6c-be97-14fd2d650422"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 39,
                column: "PermissionControllerGuid",
                value: new Guid("44148ca4-1271-447d-a22e-16d3e9bf3d1e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 40,
                column: "PermissionControllerGuid",
                value: new Guid("0af44bef-51fa-4a6e-abe9-bfd548cea45a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 41,
                column: "PermissionControllerGuid",
                value: new Guid("acf87441-a393-40dd-83f8-972749cd002a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 42,
                column: "PermissionControllerGuid",
                value: new Guid("7d1d7c11-dca1-4aca-882b-06eb13bea8f0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 43,
                column: "PermissionControllerGuid",
                value: new Guid("0c5346da-773a-4f1a-915c-2e614ae4b132"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 44,
                column: "PermissionControllerGuid",
                value: new Guid("023d406c-6d02-4bee-a8ce-03a8bf2a89d1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 45,
                column: "PermissionControllerGuid",
                value: new Guid("ec5dc20a-69ea-421e-b9b0-4b2be670f040"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 46,
                column: "PermissionControllerGuid",
                value: new Guid("9b874a84-6d3c-47e1-b475-64948b32eb2f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 47,
                column: "PermissionControllerGuid",
                value: new Guid("1e9ffba4-8a6d-46d8-961b-eada9a07b26b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 48,
                column: "PermissionControllerGuid",
                value: new Guid("fcbb58c4-42a7-46d0-abba-523c03425018"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 49,
                column: "PermissionControllerGuid",
                value: new Guid("42422cad-e9b2-4038-80eb-0fcf1b7d9721"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 50,
                column: "PermissionControllerGuid",
                value: new Guid("f5240fe7-c4d1-422a-96b0-53f9ec8fa320"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 51,
                column: "PermissionControllerGuid",
                value: new Guid("1fba1e96-d260-4fa9-878b-080e69b933da"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 52,
                column: "PermissionControllerGuid",
                value: new Guid("0f2bff27-4de7-49ab-b466-d3cab021c734"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 53,
                column: "PermissionControllerGuid",
                value: new Guid("924ecd17-dd53-4b73-83f2-9539201845d5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 54,
                column: "PermissionControllerGuid",
                value: new Guid("b4823af8-73d0-4eca-8074-8e0c8fafa6b4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 55,
                column: "PermissionControllerGuid",
                value: new Guid("9afcc397-f14c-48f2-b18a-d9cf3439e6e4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 1,
                column: "PermissionControllerActionGuid",
                value: new Guid("d590beab-2d2c-47cf-b7d9-b118d1d82a22"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 2,
                column: "PermissionControllerActionGuid",
                value: new Guid("4eaa2ef7-a470-4b2e-992c-9aaf73430851"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 3,
                column: "PermissionControllerActionGuid",
                value: new Guid("6f6d729c-6f90-4672-a967-cabdfd320cbc"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 4,
                column: "PermissionControllerActionGuid",
                value: new Guid("0308b66a-c9e2-410b-924b-8032ef5bc99b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 5,
                column: "PermissionControllerActionGuid",
                value: new Guid("4f5f647b-2c6d-4561-ba4b-060aac3ba527"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 6,
                column: "PermissionControllerActionGuid",
                value: new Guid("b85fee06-5bb6-4aba-b245-204002af9654"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 7,
                column: "PermissionControllerActionGuid",
                value: new Guid("9bf79bf5-03ab-4da1-84f3-62a01d5327d6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 8,
                column: "PermissionControllerActionGuid",
                value: new Guid("5bb1d5f8-ee09-48c1-b2d9-7db38a0409b0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 9,
                column: "PermissionControllerActionGuid",
                value: new Guid("15968d60-5b44-4073-8612-4c07796bff20"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 10,
                column: "PermissionControllerActionGuid",
                value: new Guid("abb54d75-95e3-452a-87bd-f1395e559b55"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 11,
                column: "PermissionControllerActionGuid",
                value: new Guid("af33d852-816f-4273-9487-db503c6a17dc"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 12,
                column: "PermissionControllerActionGuid",
                value: new Guid("3ae0c0d9-59a2-4a96-9034-36216d5f0870"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 13,
                column: "PermissionControllerActionGuid",
                value: new Guid("abc13799-bd3e-49f6-b49f-df97c1c323a5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 14,
                column: "PermissionControllerActionGuid",
                value: new Guid("31b81e3a-afa6-4b66-b314-ef8cb0a34646"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 15,
                column: "PermissionControllerActionGuid",
                value: new Guid("f8f8db5a-6a40-422d-9bfe-c88c94d95ec8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 16,
                column: "PermissionControllerActionGuid",
                value: new Guid("836fff69-7ba3-4a33-8809-82bbfc0b29a3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 17,
                column: "PermissionControllerActionGuid",
                value: new Guid("19240610-7e59-4c5a-9b09-0e5171ec290a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 18,
                column: "PermissionControllerActionGuid",
                value: new Guid("bae7100f-82b7-4397-9f98-6961c918196a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 19,
                column: "PermissionControllerActionGuid",
                value: new Guid("2bfa1ec4-6c9a-41cf-b809-46c231365c7f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 20,
                column: "PermissionControllerActionGuid",
                value: new Guid("aaf462fd-7b65-4eee-8233-11c74434769a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 21,
                column: "PermissionControllerActionGuid",
                value: new Guid("b5312b4f-5667-42c1-b541-d30946d245d1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 22,
                column: "PermissionControllerActionGuid",
                value: new Guid("1366ffa0-c194-4feb-98c1-bafd215c02ff"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 23,
                column: "PermissionControllerActionGuid",
                value: new Guid("f08bb1ff-8b06-4faf-b575-495dd458cc45"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 24,
                column: "PermissionControllerActionGuid",
                value: new Guid("6f3662b0-178e-4e6b-81bf-a16b0c34966d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 25,
                column: "PermissionControllerActionGuid",
                value: new Guid("e323c3df-971b-4509-82eb-9380cd044950"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 26,
                column: "PermissionControllerActionGuid",
                value: new Guid("35228b59-1e83-460f-a449-be0aaf0c0373"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 27,
                column: "PermissionControllerActionGuid",
                value: new Guid("3e63d57f-6251-4b69-abc5-dc8bbd0d8da9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 28,
                column: "PermissionControllerActionGuid",
                value: new Guid("9e26807a-fbdd-404d-a65f-4c10b9097d81"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 29,
                column: "PermissionControllerActionGuid",
                value: new Guid("98211f25-0faa-4a7c-b771-84d397fa88e8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 30,
                column: "PermissionControllerActionGuid",
                value: new Guid("b31e1eb9-5c57-4692-99ea-804aecb0f2dd"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 31,
                column: "PermissionControllerActionGuid",
                value: new Guid("0b9951f1-da95-4a4e-9362-36eff02b9775"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 32,
                column: "PermissionControllerActionGuid",
                value: new Guid("5a687c63-f8fc-4e8d-ad45-5d021c9633c6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 33,
                column: "PermissionControllerActionGuid",
                value: new Guid("2b87746c-0f5f-4049-970a-326940b22017"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 34,
                column: "PermissionControllerActionGuid",
                value: new Guid("b6106b33-9d20-43c8-adf5-177c70abb3ad"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 35,
                column: "PermissionControllerActionGuid",
                value: new Guid("b586a8db-49b1-4d2b-a9b3-d21e5c945202"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 36,
                column: "PermissionControllerActionGuid",
                value: new Guid("c73cc66c-9e48-487c-bd44-3c7a777aaeeb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 37,
                column: "PermissionControllerActionGuid",
                value: new Guid("f55533fc-ed05-4a5e-af50-89ae354d3ce0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 38,
                column: "PermissionControllerActionGuid",
                value: new Guid("fa2482d7-07a3-4781-8b48-6ff1f94d592d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 39,
                column: "PermissionControllerActionGuid",
                value: new Guid("109f4378-0231-4050-a78a-b3f5d14c52a8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 40,
                column: "PermissionControllerActionGuid",
                value: new Guid("bc38181c-2cea-401f-95f4-cea6f33f700c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 41,
                column: "PermissionControllerActionGuid",
                value: new Guid("845fb01c-af86-4937-9fcc-d38ff493db59"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 42,
                column: "PermissionControllerActionGuid",
                value: new Guid("2346eb73-62a8-4797-8bdd-0e8f6388ebfd"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 43,
                column: "PermissionControllerActionGuid",
                value: new Guid("74054797-1ad7-4c15-b0ef-d4531abdef3d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 44,
                column: "PermissionControllerActionGuid",
                value: new Guid("54ed7202-51f4-4f78-b692-638335c6d4d1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 45,
                column: "PermissionControllerActionGuid",
                value: new Guid("b729875b-0c57-4241-be78-bd479b0b780a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 46,
                column: "PermissionControllerActionGuid",
                value: new Guid("c325346e-4b5f-462e-8b3b-5d81ac65d399"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 47,
                column: "PermissionControllerActionGuid",
                value: new Guid("bfcb0cd7-ce05-494a-826c-18df071b38f6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 48,
                column: "PermissionControllerActionGuid",
                value: new Guid("ebb9718d-0759-4d62-bfc7-c7eed1ededf6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 49,
                column: "PermissionControllerActionGuid",
                value: new Guid("c6512f7d-add1-401e-a9c8-7574626607ba"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 50,
                column: "PermissionControllerActionGuid",
                value: new Guid("94bfa3d5-c816-49ae-8204-4b7b81c8eeb8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 51,
                column: "PermissionControllerActionGuid",
                value: new Guid("e7988c72-eb8d-4fb9-b150-9e4e0a92ca24"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 52,
                column: "PermissionControllerActionGuid",
                value: new Guid("10bbe177-fdb4-46b3-b70f-9e7cef13c456"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 53,
                column: "PermissionControllerActionGuid",
                value: new Guid("bef0777c-b25b-4aff-b6e7-cfc7c37dddd2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 54,
                column: "PermissionControllerActionGuid",
                value: new Guid("922dea03-599a-4f03-b2fe-d01f31383288"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 55,
                column: "PermissionControllerActionGuid",
                value: new Guid("9bd12f8b-316b-40af-be8e-72d720740892"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 56,
                column: "PermissionControllerActionGuid",
                value: new Guid("1c7385b0-58ae-477e-a20b-68ee5f4eb288"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 57,
                column: "PermissionControllerActionGuid",
                value: new Guid("19495511-01be-4b50-b920-e02ce39cffee"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 58,
                column: "PermissionControllerActionGuid",
                value: new Guid("698f49c2-07a3-4018-85ec-31ca579eeb71"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 59,
                column: "PermissionControllerActionGuid",
                value: new Guid("f535e6fe-c395-433d-8e5e-46e120b85264"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 60,
                column: "PermissionControllerActionGuid",
                value: new Guid("43d2bc70-32c5-4f54-8d09-06873af8623f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 61,
                column: "PermissionControllerActionGuid",
                value: new Guid("b329b1e1-fad9-45ea-96f9-6e8da1dff1e5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 62,
                column: "PermissionControllerActionGuid",
                value: new Guid("b94713e6-ee5f-4be7-86bc-ec87e3d08e29"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 63,
                column: "PermissionControllerActionGuid",
                value: new Guid("772c6926-d3c3-4125-b130-d369bd1b7116"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 64,
                column: "PermissionControllerActionGuid",
                value: new Guid("c46bb005-648c-42ea-b517-10c2ea5f8cba"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 65,
                column: "PermissionControllerActionGuid",
                value: new Guid("bb11f5ee-777a-41f5-b769-b0ec4ed5c96e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 66,
                column: "PermissionControllerActionGuid",
                value: new Guid("e26c3595-83bd-416c-9ce8-53b676ab846d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 67,
                column: "PermissionControllerActionGuid",
                value: new Guid("430e5a5a-c276-4bb7-aa66-41e05b50ca40"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 68,
                column: "PermissionControllerActionGuid",
                value: new Guid("44e0599b-1e7f-4872-a6ea-1fe0480d0a61"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 69,
                column: "PermissionControllerActionGuid",
                value: new Guid("526f2ee8-0fc5-4111-9624-4997afbe0c88"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 70,
                column: "PermissionControllerActionGuid",
                value: new Guid("f4306786-59e7-4981-b766-0bdd59794e27"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 71,
                column: "PermissionControllerActionGuid",
                value: new Guid("308b31b2-1263-4bf9-8334-2e7d2191e3d0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 72,
                column: "PermissionControllerActionGuid",
                value: new Guid("87d891f7-20da-4793-aee9-143a46a24ea8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 73,
                column: "PermissionControllerActionGuid",
                value: new Guid("bda2d7d8-bc31-48c9-8216-444b2f1f80a3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 74,
                column: "PermissionControllerActionGuid",
                value: new Guid("2a6299f8-729f-46c3-ab8c-25d8b5975b0c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 75,
                column: "PermissionControllerActionGuid",
                value: new Guid("40f090f0-95ec-4ac6-8e43-366239e5a669"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 76,
                column: "PermissionControllerActionGuid",
                value: new Guid("260c456d-a144-4c60-a13d-6600d7de2e1a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 77,
                column: "PermissionControllerActionGuid",
                value: new Guid("9d09dad5-c57c-40a4-acfb-db055be774d2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 78,
                column: "PermissionControllerActionGuid",
                value: new Guid("0297994c-65b9-4ddc-a8f0-1d1d1cdf6168"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 79,
                column: "PermissionControllerActionGuid",
                value: new Guid("971b09a6-d092-490a-b0a9-04e3e804f32e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 80,
                column: "PermissionControllerActionGuid",
                value: new Guid("bdb977d6-a713-4e87-909f-37d398407a0e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 81,
                column: "PermissionControllerActionGuid",
                value: new Guid("4ec24411-da81-4ac3-883b-2b3d15dfb0a2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 82,
                column: "PermissionControllerActionGuid",
                value: new Guid("e6e185c8-eda5-4199-a012-8d7876be3230"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 83,
                column: "PermissionControllerActionGuid",
                value: new Guid("a0352404-a84e-4b16-9fb5-794245c4a379"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 84,
                column: "PermissionControllerActionGuid",
                value: new Guid("ebef7c5a-21c8-4983-9158-b14e6f323844"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 85,
                column: "PermissionControllerActionGuid",
                value: new Guid("7187012c-b443-438c-ac87-ffcf2d2507bc"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 86,
                column: "PermissionControllerActionGuid",
                value: new Guid("52efad19-e3b1-4b61-8207-4a7bfc67debe"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 87,
                column: "PermissionControllerActionGuid",
                value: new Guid("274dfe01-0a6e-441d-99ac-541185dfdacf"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 88,
                column: "PermissionControllerActionGuid",
                value: new Guid("8cb8aa70-b5d9-4e89-a0f5-882e7eef57f9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 89,
                column: "PermissionControllerActionGuid",
                value: new Guid("0b0d8641-fe90-4e79-9507-1bf12624f0e1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 90,
                column: "PermissionControllerActionGuid",
                value: new Guid("c85aab70-7169-45bd-9313-e70cf8de2b07"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 91,
                column: "PermissionControllerActionGuid",
                value: new Guid("aaf58e5d-5843-4d3a-9e91-82cf9a703a4e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 92,
                column: "PermissionControllerActionGuid",
                value: new Guid("76a581c9-51d6-4981-8f04-b1611da3959d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 93,
                column: "PermissionControllerActionGuid",
                value: new Guid("d2f963d6-92ec-4cfd-b91f-4243a5d9f9a6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 94,
                column: "PermissionControllerActionGuid",
                value: new Guid("74d44082-20b0-4c20-a6bc-fb4616ce05be"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 95,
                column: "PermissionControllerActionGuid",
                value: new Guid("99eec702-5017-440a-b2c0-d1c426cc3f7b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 96,
                column: "PermissionControllerActionGuid",
                value: new Guid("c89482eb-a462-48ae-aecf-73ed932f0141"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 97,
                column: "PermissionControllerActionGuid",
                value: new Guid("99d9ac46-b3e5-4427-a734-7b15599104ee"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 98,
                column: "PermissionControllerActionGuid",
                value: new Guid("3dbd9bad-6256-49a8-8235-87529a2825ff"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 99,
                column: "PermissionControllerActionGuid",
                value: new Guid("46d99be8-c176-4132-b4f9-a3a34b8524dd"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 100,
                column: "PermissionControllerActionGuid",
                value: new Guid("f0b4b316-d61b-44ee-8a1d-19a5bb3c384b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 101,
                column: "PermissionControllerActionGuid",
                value: new Guid("4b7794c0-ccd1-4a07-891e-a8564f5a0db2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 102,
                column: "PermissionControllerActionGuid",
                value: new Guid("29aae402-5bc5-4a16-89fc-b4daef22cb29"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 103,
                column: "PermissionControllerActionGuid",
                value: new Guid("1e7ed7ee-7194-4d7f-ac47-c909b474973d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 104,
                column: "PermissionControllerActionGuid",
                value: new Guid("6d6d5546-ea1c-4869-a730-a885aacbaf0f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 105,
                column: "PermissionControllerActionGuid",
                value: new Guid("d4064bdd-3839-4c2f-a3af-d929e75ff3d5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 106,
                column: "PermissionControllerActionGuid",
                value: new Guid("166c6e18-fbe1-4a70-b50d-25cc9b79687b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 107,
                column: "PermissionControllerActionGuid",
                value: new Guid("4302f08d-c890-4b02-941b-d15dd3f183fa"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 108,
                column: "PermissionControllerActionGuid",
                value: new Guid("219f4f15-8f3a-4e19-9331-f36f38c0a690"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 109,
                column: "PermissionControllerActionGuid",
                value: new Guid("bba721da-30e1-48cf-a9ea-e96ceeacf21a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 110,
                column: "PermissionControllerActionGuid",
                value: new Guid("22f70aa2-78a1-4dff-aa90-67ded12fab5f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 111,
                column: "PermissionControllerActionGuid",
                value: new Guid("34b7e347-ef2b-4ba2-ae13-8355cadb9aff"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 112,
                column: "PermissionControllerActionGuid",
                value: new Guid("0f5b115d-5ef0-465b-aa3f-6bb41fec6203"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 113,
                column: "PermissionControllerActionGuid",
                value: new Guid("ac817c69-d2a8-4a03-bdc0-0c6820dd708c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 114,
                column: "PermissionControllerActionGuid",
                value: new Guid("343d8ef2-2d76-4fa7-a2c5-7ac9b7788817"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 115,
                column: "PermissionControllerActionGuid",
                value: new Guid("1c7a373c-e3ad-4dc0-a509-694909afbf4b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 116,
                column: "PermissionControllerActionGuid",
                value: new Guid("fce94a91-b8c7-4d26-8cf2-b5e87acf668d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 117,
                column: "PermissionControllerActionGuid",
                value: new Guid("0439dbde-b748-4863-b77e-e379f11d8f42"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 118,
                column: "PermissionControllerActionGuid",
                value: new Guid("297546ab-ec61-44a1-a5d6-ca39aa6d5a22"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 119,
                column: "PermissionControllerActionGuid",
                value: new Guid("2ab7d3b2-38ed-471a-9ada-6cbc05f8a0b1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 120,
                column: "PermissionControllerActionGuid",
                value: new Guid("2a6dbd14-1734-41f0-ae3f-c67c91f0c9f0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 121,
                column: "PermissionControllerActionGuid",
                value: new Guid("ffac24e5-a726-47a8-abe7-8c388d228925"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 122,
                column: "PermissionControllerActionGuid",
                value: new Guid("d152e213-964a-4a23-9c95-1f40a215979c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 123,
                column: "PermissionControllerActionGuid",
                value: new Guid("d6a230da-e852-4392-a838-98a78a065bad"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 124,
                column: "PermissionControllerActionGuid",
                value: new Guid("8479de4e-e66c-4d2d-82b9-52f13cc5df3f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 125,
                column: "PermissionControllerActionGuid",
                value: new Guid("591a656e-606a-4b59-ae45-876f4b73581e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 126,
                column: "PermissionControllerActionGuid",
                value: new Guid("ecb4c878-d9d1-48c8-88ff-8705e7c37a13"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 127,
                column: "PermissionControllerActionGuid",
                value: new Guid("d16d4296-0fed-4bf4-b5db-547c26e0f302"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 128,
                column: "PermissionControllerActionGuid",
                value: new Guid("e613ee4e-f1cd-4a0f-b646-37bd0b7e35c7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 129,
                column: "PermissionControllerActionGuid",
                value: new Guid("a821875f-7188-45ae-a853-ab10fb4b0e10"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 130,
                column: "PermissionControllerActionGuid",
                value: new Guid("d845ab02-6a88-4eb2-a77d-3e1d90cfcc9f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 131,
                column: "PermissionControllerActionGuid",
                value: new Guid("e84a61d9-8631-408b-8f2a-5b20b8c52e74"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 132,
                column: "PermissionControllerActionGuid",
                value: new Guid("dea27234-6bb8-4e66-b09b-7e94c44cb8c8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 133,
                column: "PermissionControllerActionGuid",
                value: new Guid("1b2ce1aa-f6f3-48dd-880b-b6f15cc5fb4a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 134,
                column: "PermissionControllerActionGuid",
                value: new Guid("a148c376-79ca-408e-9fd0-982b1ef1abdc"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 135,
                column: "PermissionControllerActionGuid",
                value: new Guid("925b4c73-eeb4-4eda-845e-dbbc4e30be17"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 136,
                column: "PermissionControllerActionGuid",
                value: new Guid("81fc7638-07a4-4ba3-a417-d680a0a9aa48"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 137,
                column: "PermissionControllerActionGuid",
                value: new Guid("893f01cd-0a60-4475-9851-e6750177201f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 138,
                column: "PermissionControllerActionGuid",
                value: new Guid("118ab742-58eb-4a1d-9a57-e0ada6339cbf"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 139,
                column: "PermissionControllerActionGuid",
                value: new Guid("2ab11247-b53d-417c-b7d6-7b9365e42e85"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 140,
                column: "PermissionControllerActionGuid",
                value: new Guid("9a4b2c2c-0451-4c74-bfc6-b3541b276466"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 141,
                column: "PermissionControllerActionGuid",
                value: new Guid("7ddc62a7-c6fe-4b97-b8d6-d8447da86c76"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 142,
                column: "PermissionControllerActionGuid",
                value: new Guid("20d8cddc-fc32-46d6-87a2-cc0d4d39f80f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 143,
                column: "PermissionControllerActionGuid",
                value: new Guid("a4a0a068-d428-4135-a62e-ba6f6b187cf4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 144,
                column: "PermissionControllerActionGuid",
                value: new Guid("c71b8813-5153-4607-9f06-98ad2b92fed3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 145,
                column: "PermissionControllerActionGuid",
                value: new Guid("c1c3ec32-ad9a-41ae-aeb9-47c16882f9d1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 146,
                column: "PermissionControllerActionGuid",
                value: new Guid("74714232-7382-4649-a0d7-1f3d12167296"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 147,
                column: "PermissionControllerActionGuid",
                value: new Guid("b0502142-98f0-413c-baec-9b2a33513c1d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 148,
                column: "PermissionControllerActionGuid",
                value: new Guid("65e50dbc-63fe-4cf0-bf35-b58733506112"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 149,
                column: "PermissionControllerActionGuid",
                value: new Guid("bbb93fe4-fb30-4700-bf6a-78d5063fb1fe"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 150,
                column: "PermissionControllerActionGuid",
                value: new Guid("c10bcb93-5c0e-4721-973a-ddb910e2fd29"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 151,
                column: "PermissionControllerActionGuid",
                value: new Guid("827bcc10-0ebc-49a8-a0e0-009459f6733e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 152,
                column: "PermissionControllerActionGuid",
                value: new Guid("342f69ed-18d9-4420-826b-193f2c00f4ba"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 153,
                column: "PermissionControllerActionGuid",
                value: new Guid("821394f3-9166-4f9e-ab11-d4366b67049b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 154,
                column: "PermissionControllerActionGuid",
                value: new Guid("ba4b2ee9-ef2d-486f-b893-2e91eedc5b29"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 155,
                column: "PermissionControllerActionGuid",
                value: new Guid("cf423510-2e49-4326-878b-01b565830a9f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 156,
                column: "PermissionControllerActionGuid",
                value: new Guid("6f29a049-c7b5-4ca9-abe4-e9b662556aea"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 157,
                column: "PermissionControllerActionGuid",
                value: new Guid("14c47214-24d0-4b77-9c5c-d6c0bdcdb546"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 158,
                column: "PermissionControllerActionGuid",
                value: new Guid("43243241-77a8-445d-a7ed-7ed360be034d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 159,
                column: "PermissionControllerActionGuid",
                value: new Guid("187ff526-8bc1-4e96-b5d9-bd9c6e72cc8d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 160,
                column: "PermissionControllerActionGuid",
                value: new Guid("92c6f9c7-f9ff-41ef-8e77-eae2986fb789"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 161,
                column: "PermissionControllerActionGuid",
                value: new Guid("1e87eaf6-293a-4538-9bd9-be6854d76477"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 162,
                column: "PermissionControllerActionGuid",
                value: new Guid("b2e386fc-eec2-43fc-bd18-7285dfa8cf9b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 163,
                column: "PermissionControllerActionGuid",
                value: new Guid("3b31ca59-4489-43b0-aa9f-7f0cf0e556e6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 164,
                column: "PermissionControllerActionGuid",
                value: new Guid("f32f66ce-5da2-4001-941b-fe22662d9422"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 165,
                column: "PermissionControllerActionGuid",
                value: new Guid("93c667d9-cb9d-4638-bc4d-c90a02d65006"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 166,
                column: "PermissionControllerActionGuid",
                value: new Guid("cc60a259-4ebb-4712-b1f4-79bb133b19bd"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 167,
                column: "PermissionControllerActionGuid",
                value: new Guid("35267d82-ebcb-47d5-9cdc-9dd6c837ea0a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 168,
                column: "PermissionControllerActionGuid",
                value: new Guid("ee4ef4c7-548c-475b-887c-3cf1edd0e9a1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 169,
                column: "PermissionControllerActionGuid",
                value: new Guid("326a3795-c1c9-4114-b004-e77db9d59805"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 170,
                column: "PermissionControllerActionGuid",
                value: new Guid("112d13eb-73b9-4895-bc8c-575d2adfcf53"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 171,
                column: "PermissionControllerActionGuid",
                value: new Guid("81de8387-970c-45d2-8dff-2d65e438ee05"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 172,
                column: "PermissionControllerActionGuid",
                value: new Guid("bf50468c-630e-4695-ab3f-04446f9f7daa"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 173,
                column: "PermissionControllerActionGuid",
                value: new Guid("1ae39c26-de5f-4400-9b12-e552d63fcc48"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 174,
                column: "PermissionControllerActionGuid",
                value: new Guid("87cdbdb2-703d-4582-8d54-88ef17bd6592"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 175,
                column: "PermissionControllerActionGuid",
                value: new Guid("5bdf9402-6a02-4375-bfd2-19f744e7aa5e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 176,
                column: "PermissionControllerActionGuid",
                value: new Guid("d7541cd7-faa8-4507-853c-2204763c0f6e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 177,
                column: "PermissionControllerActionGuid",
                value: new Guid("8247f5b8-0afe-498a-8ab6-3af18f89743b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 178,
                column: "PermissionControllerActionGuid",
                value: new Guid("0a48e35a-f273-4270-97c0-0fae646e2d45"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 179,
                column: "PermissionControllerActionGuid",
                value: new Guid("3dace134-ec16-4b58-a3e5-b88281120b4c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 180,
                column: "PermissionControllerActionGuid",
                value: new Guid("60d14901-adfe-4697-8b50-9882908118a9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 181,
                column: "PermissionControllerActionGuid",
                value: new Guid("3621069d-60d1-4d79-9487-45a948952085"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 182,
                column: "PermissionControllerActionGuid",
                value: new Guid("270f98ac-a2eb-44b6-804b-047fa2403b40"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 183,
                column: "PermissionControllerActionGuid",
                value: new Guid("91a35b8c-ae7a-4586-8a3b-4eeae35dd85b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 184,
                column: "PermissionControllerActionGuid",
                value: new Guid("54698ea2-ec4b-4594-b6eb-48f0caa7d670"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 185,
                column: "PermissionControllerActionGuid",
                value: new Guid("6c45b9d5-185b-4fe6-b0bf-13a2c89cdf43"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 186,
                column: "PermissionControllerActionGuid",
                value: new Guid("de9c782a-de77-4d23-a947-0d824a53f31c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 187,
                column: "PermissionControllerActionGuid",
                value: new Guid("b7ead546-a7d9-4c6b-ac32-424f83fb5613"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 188,
                column: "PermissionControllerActionGuid",
                value: new Guid("8e906158-df89-4dee-824e-d68cb37152ef"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 189,
                column: "PermissionControllerActionGuid",
                value: new Guid("eb4b4ae7-a6e0-43c2-a4da-7c69e7e9ada3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 190,
                column: "PermissionControllerActionGuid",
                value: new Guid("b620a456-8f50-46f6-ad56-4d224abc0145"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 191,
                column: "PermissionControllerActionGuid",
                value: new Guid("57d521b8-b685-4b2c-a5c1-d1e0dc3e7155"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 192,
                column: "PermissionControllerActionGuid",
                value: new Guid("a348a352-c259-4618-8412-060a8a4d9f71"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 193,
                column: "PermissionControllerActionGuid",
                value: new Guid("c3609b2f-920a-404f-ba93-6c9691c77442"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 194,
                column: "PermissionControllerActionGuid",
                value: new Guid("20aadfcf-a087-4c9f-a289-d98add520791"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 195,
                column: "PermissionControllerActionGuid",
                value: new Guid("de86c4ee-a79e-4be7-82d7-290d069a904a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 196,
                column: "PermissionControllerActionGuid",
                value: new Guid("220720bf-bc40-4fc6-a6b1-c95c632a93b0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 197,
                column: "PermissionControllerActionGuid",
                value: new Guid("2a6b92c2-950f-4b9f-ae5d-62e3356bc752"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 198,
                column: "PermissionControllerActionGuid",
                value: new Guid("c25fb258-a578-424f-b1cd-78c71d73747c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 199,
                column: "PermissionControllerActionGuid",
                value: new Guid("bd98cd40-4756-4068-9c20-a38476a5194b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 200,
                column: "PermissionControllerActionGuid",
                value: new Guid("54a5b1eb-5e3d-4ca7-8edc-6ba49788e6ca"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 201,
                column: "PermissionControllerActionGuid",
                value: new Guid("ca365b8e-a669-453b-b957-04d41882cb46"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 202,
                column: "PermissionControllerActionGuid",
                value: new Guid("ad517c5c-3cf2-4d6c-bc1e-847bcf992753"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 203,
                column: "PermissionControllerActionGuid",
                value: new Guid("a295e861-866b-4397-9789-b99636e7ce26"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 204,
                column: "PermissionControllerActionGuid",
                value: new Guid("5e74697b-dab3-43d2-8fc0-3f78b40c12c6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 205,
                column: "PermissionControllerActionGuid",
                value: new Guid("f47c4b7d-a4f4-4945-81ec-4ace532d83c1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 206,
                column: "PermissionControllerActionGuid",
                value: new Guid("8aff923d-8c07-45ba-908f-0ff12f528e82"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 207,
                column: "PermissionControllerActionGuid",
                value: new Guid("4aeea07d-3d4d-41cd-9937-f5fd81d838e3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 208,
                column: "PermissionControllerActionGuid",
                value: new Guid("9153d791-c7a9-44ec-b532-dfa7908c1522"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 209,
                column: "PermissionControllerActionGuid",
                value: new Guid("9ee9c3f6-44df-469b-b047-16506c41897f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 210,
                column: "PermissionControllerActionGuid",
                value: new Guid("103b87e3-131c-49c1-a646-1c1854d4d156"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 211,
                column: "PermissionControllerActionGuid",
                value: new Guid("32cee667-d983-422c-a2e1-6edf9bb53ff8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 212,
                column: "PermissionControllerActionGuid",
                value: new Guid("5e91adb5-7d3c-4e59-9412-17532ca7a9f1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 213,
                column: "PermissionControllerActionGuid",
                value: new Guid("2aca38bf-c8df-4d9e-ba96-d227bff48bd9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 214,
                column: "PermissionControllerActionGuid",
                value: new Guid("294a0a42-d2b4-4b69-b53c-2a0efa7d100a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 215,
                column: "PermissionControllerActionGuid",
                value: new Guid("b5ffd74a-85ec-4fa2-ae9a-a1b78be14ef2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 216,
                column: "PermissionControllerActionGuid",
                value: new Guid("b6ce7da7-0d90-406e-ad88-3f3be76e9875"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 217,
                column: "PermissionControllerActionGuid",
                value: new Guid("60a4937c-9f4a-4182-b99b-665cf0897c73"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 218,
                column: "PermissionControllerActionGuid",
                value: new Guid("b076ce27-3e73-4319-88bf-27ece2204798"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 219,
                column: "PermissionControllerActionGuid",
                value: new Guid("a7317617-8ea8-4326-a309-f54a85308ab8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 220,
                column: "PermissionControllerActionGuid",
                value: new Guid("4564565d-e110-4397-bcae-34f41be98d36"));

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "City",
                keyColumn: "CityID",
                keyValue: 1,
                columns: new[] { "CityGuid", "CreateDate", "EnableDate" },
                values: new object[] { new Guid("d35939d8-8d8c-4f5d-823b-ed282b6229ab"), new DateTime(2021, 11, 7, 15, 8, 45, 219, DateTimeKind.Local).AddTicks(915), new DateTime(2021, 11, 7, 15, 8, 45, 219, DateTimeKind.Local).AddTicks(1518) });

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "Country",
                keyColumn: "CountryID",
                keyValue: 1,
                columns: new[] { "CountryGuid", "CreateDate", "EnableDate" },
                values: new object[] { new Guid("8012dbeb-083e-4ee4-b743-482b245585cc"), new DateTime(2021, 11, 7, 15, 8, 45, 215, DateTimeKind.Local).AddTicks(6624), new DateTime(2021, 11, 7, 15, 8, 45, 217, DateTimeKind.Local).AddTicks(568) });

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "Departments",
                keyColumn: "DepartmentsID",
                keyValue: 1,
                columns: new[] { "CreateDate", "DepartmentsGuid", "EnableDate" },
                values: new object[] { new DateTime(2021, 11, 7, 15, 8, 45, 227, DateTimeKind.Local).AddTicks(7066), new Guid("a7b656f7-7fdd-4c9c-b1e4-0cd06ffeedea"), new DateTime(2021, 11, 7, 15, 8, 45, 227, DateTimeKind.Local).AddTicks(7625) });

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "Jobs",
                keyColumn: "JobsID",
                keyValue: 1,
                columns: new[] { "CreateDate", "EnableDate", "JobsGuid" },
                values: new object[] { new DateTime(2021, 11, 7, 15, 8, 45, 226, DateTimeKind.Local).AddTicks(5480), new DateTime(2021, 11, 7, 15, 8, 45, 226, DateTimeKind.Local).AddTicks(6078), new Guid("52745bf6-97a1-4566-b449-6140086830b5") });

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "MainCategory",
                keyColumn: "MainCategoryID",
                keyValue: 1,
                columns: new[] { "CreateDate", "EnableDate", "MainCategoryGuid" },
                values: new object[] { new DateTime(2021, 11, 7, 15, 8, 45, 227, DateTimeKind.Local).AddTicks(1182), new DateTime(2021, 11, 7, 15, 8, 45, 227, DateTimeKind.Local).AddTicks(1795), new Guid("1a06b367-d88d-487a-9048-605063dcc9cb") });

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "Nationality",
                keyColumn: "NationalityID",
                keyValue: 1,
                columns: new[] { "CreateDate", "EnableDate", "NationalityGuid" },
                values: new object[] { new DateTime(2021, 11, 7, 15, 8, 45, 219, DateTimeKind.Local).AddTicks(8946), new DateTime(2021, 11, 7, 15, 8, 45, 219, DateTimeKind.Local).AddTicks(9489), new Guid("a43aa3af-c824-4917-a7c0-12ea002b1bfc") });

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "Region",
                keyColumn: "RegionID",
                keyValue: 1,
                columns: new[] { "CreateDate", "EnableDate", "RegionGuid" },
                values: new object[] { new DateTime(2021, 11, 7, 15, 8, 45, 217, DateTimeKind.Local).AddTicks(8902), new DateTime(2021, 11, 7, 15, 8, 45, 217, DateTimeKind.Local).AddTicks(9459), new Guid("f5664dc1-37f5-4559-9250-1f164d35f3b5") });

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_RegionID",
                schema: "Driver",
                table: "Drivers",
                column: "RegionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_Region_RegionID",
                schema: "Driver",
                table: "Drivers",
                column: "RegionID",
                principalSchema: "Setting",
                principalTable: "Region",
                principalColumn: "RegionID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_Region_RegionID",
                schema: "Driver",
                table: "Drivers");

            migrationBuilder.DropIndex(
                name: "IX_Drivers_RegionID",
                schema: "Driver",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "RegionID",
                schema: "Driver",
                table: "Drivers");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "638dde2d-eb89-4a72-b067-80a6cbdfd7f9");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "8bcb6328-f64f-4a7d-8ba7-935551f29f78");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "70afd82d-6cc0-49ac-b029-cf1e08bb159e");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "6360e33b-8165-4e72-a190-ea84caff9e80");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "a1614b6d-b066-4907-aca3-432ffaa382d4", "fea64d1b-4949-4649-8bbe-300a7ba12fa3" });

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "DeliverySetting",
                keyColumn: "DeliverySettingID",
                keyValue: 1,
                column: "DeliverySettingGuid",
                value: new Guid("71e9f4c4-142c-4a3f-9465-a1fb5734cb0c"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 1,
                column: "TransactionTypeGuid",
                value: new Guid("e520e944-7e4f-486a-9a8e-ac3c154e97ae"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 2,
                column: "TransactionTypeGuid",
                value: new Guid("8a01f527-e073-4c04-84e1-ae0372965a1b"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 3,
                column: "TransactionTypeGuid",
                value: new Guid("1a1def69-55a9-4471-bd03-b1bddc0ec843"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 4,
                column: "TransactionTypeGuid",
                value: new Guid("359e0004-fba8-4dca-903e-809e68d93b91"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 5,
                column: "TransactionTypeGuid",
                value: new Guid("e6e68c74-d841-4501-b356-e3cec68dd0b4"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 6,
                column: "TransactionTypeGuid",
                value: new Guid("63d3a746-30f6-4783-a899-0fec6e05d9c8"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 7,
                column: "TransactionTypeGuid",
                value: new Guid("6e215a88-b6ff-4459-9ae6-174ab138c023"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 8,
                column: "TransactionTypeGuid",
                value: new Guid("7e2d74fb-249e-41ae-91ad-c9697a5fa370"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 9,
                column: "TransactionTypeGuid",
                value: new Guid("46f1de32-94c5-442b-9a61-d165b0880629"));

            migrationBuilder.UpdateData(
                schema: "Emp",
                table: "Employees",
                keyColumn: "EntityEmpID",
                keyValue: 1,
                columns: new[] { "CreateDate", "EnableDate" },
                values: new object[] { new DateTime(2021, 11, 7, 10, 28, 30, 425, DateTimeKind.Local).AddTicks(7423), new DateTime(2021, 11, 7, 10, 28, 30, 425, DateTimeKind.Local).AddTicks(7781) });

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionAction",
                keyColumn: "PermissionActionID",
                keyValue: 1,
                column: "PermissionActionGuid",
                value: new Guid("8ab1b003-a462-49b9-928c-de479f7bf80b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionAction",
                keyColumn: "PermissionActionID",
                keyValue: 2,
                column: "PermissionActionGuid",
                value: new Guid("040f8c26-f9b6-4fb5-84f6-c6c5780b8b12"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionAction",
                keyColumn: "PermissionActionID",
                keyValue: 3,
                column: "PermissionActionGuid",
                value: new Guid("712c96eb-99a2-4d77-8d2b-64c11e335fb1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionAction",
                keyColumn: "PermissionActionID",
                keyValue: 4,
                column: "PermissionActionGuid",
                value: new Guid("5652089b-522b-43a0-9db8-e4534c605ac9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 1,
                column: "PermissionControllerGuid",
                value: new Guid("dc9566bc-f0c3-45cd-bb6d-7ebdae6e9633"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 2,
                column: "PermissionControllerGuid",
                value: new Guid("8529a283-14f7-42ed-8485-ea04aa57d9c8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 3,
                column: "PermissionControllerGuid",
                value: new Guid("f8ddf930-b2d4-416a-9b9f-8dcb0b31a8a5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 4,
                column: "PermissionControllerGuid",
                value: new Guid("97fd764d-b8c0-41af-9c8c-e27e4db33fe3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 5,
                column: "PermissionControllerGuid",
                value: new Guid("3c2ed832-a41e-498f-ade9-dea4b2fdbb00"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 6,
                column: "PermissionControllerGuid",
                value: new Guid("ce4d4709-4dc4-4ab8-ab79-bf050adddcc6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 7,
                column: "PermissionControllerGuid",
                value: new Guid("8c875b90-15fe-4c47-a21e-d5300e2a797b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 8,
                column: "PermissionControllerGuid",
                value: new Guid("29316241-32a1-4b0d-9530-4ef3eae675f6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 9,
                column: "PermissionControllerGuid",
                value: new Guid("80b4674b-b084-4615-a061-fa90ea57d05f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 10,
                column: "PermissionControllerGuid",
                value: new Guid("924f31f9-8f21-4403-a8f9-b4bfd1682fe1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 11,
                column: "PermissionControllerGuid",
                value: new Guid("a6ec5baf-fdad-4bce-84be-635dd8bb4e04"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 12,
                column: "PermissionControllerGuid",
                value: new Guid("13eff6c9-0c32-491b-91b5-f6fc24360381"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 13,
                column: "PermissionControllerGuid",
                value: new Guid("f9a567be-d1de-44eb-a4c0-f7b654860dfd"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 14,
                column: "PermissionControllerGuid",
                value: new Guid("c21793eb-6ae8-482f-9f97-783100baca42"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 15,
                column: "PermissionControllerGuid",
                value: new Guid("a7f639e8-5ff9-41fa-8651-b3a253efbaeb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 16,
                column: "PermissionControllerGuid",
                value: new Guid("efe9671a-2fc8-49fa-9462-5c6567427fef"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 17,
                column: "PermissionControllerGuid",
                value: new Guid("a64ad85d-e50d-45fd-82fa-792f4512c5c1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 18,
                column: "PermissionControllerGuid",
                value: new Guid("515429fe-b1b0-4767-ab5e-341c6a645140"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 19,
                column: "PermissionControllerGuid",
                value: new Guid("69b66830-6277-4a28-8c61-f2c83b3e31ae"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 20,
                column: "PermissionControllerGuid",
                value: new Guid("a31a8812-9cc6-4917-b0f8-54337107fee0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 21,
                column: "PermissionControllerGuid",
                value: new Guid("2df58914-cfa0-4b07-b34e-dfd25bb84474"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 22,
                column: "PermissionControllerGuid",
                value: new Guid("8b3b05cd-7ca2-4604-b3ff-a92fc1936631"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 23,
                column: "PermissionControllerGuid",
                value: new Guid("d9839514-2d05-4f03-a398-76c2cab06419"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 24,
                column: "PermissionControllerGuid",
                value: new Guid("3efefc5b-aecd-45ea-b0b3-ce110b88e38a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 25,
                column: "PermissionControllerGuid",
                value: new Guid("1f542669-6925-4a05-9664-b5bd01884062"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 26,
                column: "PermissionControllerGuid",
                value: new Guid("bdefc259-03c1-4450-83d5-d9dcaf160b02"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 27,
                column: "PermissionControllerGuid",
                value: new Guid("99cf8173-3dab-40b4-88e7-192b5c397d88"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 28,
                column: "PermissionControllerGuid",
                value: new Guid("4ca52d2a-aedc-4865-8d4b-bf41be769057"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 29,
                column: "PermissionControllerGuid",
                value: new Guid("97a87e92-f02a-4c50-ac0d-2d8861b41498"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 30,
                column: "PermissionControllerGuid",
                value: new Guid("b77ab0bd-b54d-4b55-8cdc-993359e04dbc"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 31,
                column: "PermissionControllerGuid",
                value: new Guid("e4e61457-3edf-4f3d-b503-70b44e92b66a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 32,
                column: "PermissionControllerGuid",
                value: new Guid("5440d579-6326-47f5-88fa-799f3ae3bc3c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 33,
                column: "PermissionControllerGuid",
                value: new Guid("9fbf8e82-aaa4-4f63-83a8-ec849d1c1a30"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 34,
                column: "PermissionControllerGuid",
                value: new Guid("5f32b5a4-fd24-4f5d-a0e9-48258e2e00c4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 35,
                column: "PermissionControllerGuid",
                value: new Guid("e365dde4-d050-416e-8310-ae568c1964c8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 36,
                column: "PermissionControllerGuid",
                value: new Guid("bde48ac5-4c92-437f-8c6a-310648a1ec30"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 37,
                column: "PermissionControllerGuid",
                value: new Guid("44de549b-8034-4419-a092-18eb328155a4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 38,
                column: "PermissionControllerGuid",
                value: new Guid("0e05b1b5-03f0-455a-aec9-8b375e14afe9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 39,
                column: "PermissionControllerGuid",
                value: new Guid("22cda328-e2cc-4e24-93ab-837dede65096"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 40,
                column: "PermissionControllerGuid",
                value: new Guid("e6909795-f1e4-41dd-96e4-9ad861cf0103"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 41,
                column: "PermissionControllerGuid",
                value: new Guid("5f4c64c4-de12-4b9a-a6bd-7f63030af0bd"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 42,
                column: "PermissionControllerGuid",
                value: new Guid("bc5f5f60-ab4b-4187-9728-417df0e6e118"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 43,
                column: "PermissionControllerGuid",
                value: new Guid("0d517577-23bd-47a8-a3ee-15c0c201247b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 44,
                column: "PermissionControllerGuid",
                value: new Guid("af70b4cd-32d2-41d4-a832-46da55b76f66"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 45,
                column: "PermissionControllerGuid",
                value: new Guid("80cdb51f-a0e4-4e30-a69d-1ed6e1d57bf1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 46,
                column: "PermissionControllerGuid",
                value: new Guid("a9ddddc5-116c-4775-8a2c-4a47a1d22794"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 47,
                column: "PermissionControllerGuid",
                value: new Guid("a2d8b2b4-feb7-4e59-a329-4d629806ef7a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 48,
                column: "PermissionControllerGuid",
                value: new Guid("f626dcec-a0d4-4de8-9c3c-d7c1f963296b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 49,
                column: "PermissionControllerGuid",
                value: new Guid("49a469ee-cf31-4e6e-934c-5703861db681"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 50,
                column: "PermissionControllerGuid",
                value: new Guid("42c71dc3-1233-4cf2-84c8-b33c4a32ea5b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 51,
                column: "PermissionControllerGuid",
                value: new Guid("a9a55ad3-dc4d-40fb-a6f6-67fad892c7b7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 52,
                column: "PermissionControllerGuid",
                value: new Guid("c23c6201-6251-4533-84ca-467c635c9754"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 53,
                column: "PermissionControllerGuid",
                value: new Guid("96850722-9c6c-42f6-a4b6-96d8572e8a75"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 54,
                column: "PermissionControllerGuid",
                value: new Guid("c53a4e10-ac7f-4815-8374-25279417a7db"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 55,
                column: "PermissionControllerGuid",
                value: new Guid("23fbdcac-7b38-4f0b-8842-6a4651ff217a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 1,
                column: "PermissionControllerActionGuid",
                value: new Guid("0d49b23c-31e9-42eb-837c-c5334ff99acd"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 2,
                column: "PermissionControllerActionGuid",
                value: new Guid("b8b3f18b-91c2-4587-bd61-c1337373c108"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 3,
                column: "PermissionControllerActionGuid",
                value: new Guid("37e69d0c-9b9c-458b-ae4b-4f6a8100dad2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 4,
                column: "PermissionControllerActionGuid",
                value: new Guid("cd2337c9-1678-4bc4-a5ae-eb83a847e431"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 5,
                column: "PermissionControllerActionGuid",
                value: new Guid("c1e4c751-8928-46e5-896a-704e36740a4b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 6,
                column: "PermissionControllerActionGuid",
                value: new Guid("ed62ad3d-22e7-4473-b70a-86bdfc3c5bb1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 7,
                column: "PermissionControllerActionGuid",
                value: new Guid("91f0e0c7-5df9-4e41-9800-a68145d53645"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 8,
                column: "PermissionControllerActionGuid",
                value: new Guid("2f894aed-1e75-4c58-8655-caa7b3b79a1d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 9,
                column: "PermissionControllerActionGuid",
                value: new Guid("6a868956-7443-4b29-8c08-56570fc3fdaf"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 10,
                column: "PermissionControllerActionGuid",
                value: new Guid("bc069a2c-7d7b-4fb9-9770-57ebaaa49bae"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 11,
                column: "PermissionControllerActionGuid",
                value: new Guid("64ba019a-dda2-4bbe-9c42-45a7d1d1feba"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 12,
                column: "PermissionControllerActionGuid",
                value: new Guid("6760da7a-f57d-4aaf-a1a9-41f4ddc5818e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 13,
                column: "PermissionControllerActionGuid",
                value: new Guid("5a7c0e04-970a-4ac7-8892-bafe822b0461"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 14,
                column: "PermissionControllerActionGuid",
                value: new Guid("7f2dd2fb-b423-4f09-bc20-291b193601cb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 15,
                column: "PermissionControllerActionGuid",
                value: new Guid("35a2562e-21c7-4dac-b67f-7e1a0b405588"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 16,
                column: "PermissionControllerActionGuid",
                value: new Guid("9628117f-d28c-4c1a-a6a0-cda63efe8441"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 17,
                column: "PermissionControllerActionGuid",
                value: new Guid("bdac7480-b812-44e4-91eb-1a8e26d3f3ad"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 18,
                column: "PermissionControllerActionGuid",
                value: new Guid("c2b63836-6124-403e-af3f-345d8d728bd2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 19,
                column: "PermissionControllerActionGuid",
                value: new Guid("c3652dca-05d7-4d58-ae9e-9855fe46a04e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 20,
                column: "PermissionControllerActionGuid",
                value: new Guid("75ab6d4c-c532-4afa-8fbf-929c5a57beab"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 21,
                column: "PermissionControllerActionGuid",
                value: new Guid("c5a19785-6d53-43c3-9b98-b55e32475325"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 22,
                column: "PermissionControllerActionGuid",
                value: new Guid("3354f954-6fc9-428d-8497-8e4f0f871b71"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 23,
                column: "PermissionControllerActionGuid",
                value: new Guid("e7f4e78a-ae1a-4249-8967-ec5ee9e9c0d5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 24,
                column: "PermissionControllerActionGuid",
                value: new Guid("a0e0760d-8f52-43c3-8d8c-2f4f5e97feea"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 25,
                column: "PermissionControllerActionGuid",
                value: new Guid("04599bae-06c0-47b7-8b33-42124afd0f6e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 26,
                column: "PermissionControllerActionGuid",
                value: new Guid("6bf3fea2-a5ab-4ba5-beef-e0dea183209c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 27,
                column: "PermissionControllerActionGuid",
                value: new Guid("bf299524-ce19-46fb-90b1-e1ced12ae921"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 28,
                column: "PermissionControllerActionGuid",
                value: new Guid("55230655-d74b-4cdf-b553-8feca0987fc2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 29,
                column: "PermissionControllerActionGuid",
                value: new Guid("3bd39845-5c76-4bd8-a4fe-48214d1e8cec"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 30,
                column: "PermissionControllerActionGuid",
                value: new Guid("443089f1-d217-47af-b35b-0819d9c2227b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 31,
                column: "PermissionControllerActionGuid",
                value: new Guid("167cf1bf-d50b-4009-9f27-76e99466e238"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 32,
                column: "PermissionControllerActionGuid",
                value: new Guid("6cc91a62-fd26-4640-9504-70db05328ab9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 33,
                column: "PermissionControllerActionGuid",
                value: new Guid("8d7b060f-c352-46b7-a1c4-4089abe5577a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 34,
                column: "PermissionControllerActionGuid",
                value: new Guid("d1a3dccd-a5d9-47c7-b034-2f8952f471a8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 35,
                column: "PermissionControllerActionGuid",
                value: new Guid("a116125c-1405-4246-8d80-b5ff4e4633bb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 36,
                column: "PermissionControllerActionGuid",
                value: new Guid("aa8d1cbd-e75d-4cdc-80df-4ba2c4217d98"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 37,
                column: "PermissionControllerActionGuid",
                value: new Guid("33280980-c3bf-4436-9dcf-c0f904585fd6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 38,
                column: "PermissionControllerActionGuid",
                value: new Guid("dc34a290-9489-4452-8355-51cab55da7ba"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 39,
                column: "PermissionControllerActionGuid",
                value: new Guid("6345ac97-e95f-4793-968c-74a3f7227845"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 40,
                column: "PermissionControllerActionGuid",
                value: new Guid("4b7d5a6d-7695-4d03-83c8-879a9cfd1e95"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 41,
                column: "PermissionControllerActionGuid",
                value: new Guid("a08e06a2-68c6-4c68-8e7e-453472ec745f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 42,
                column: "PermissionControllerActionGuid",
                value: new Guid("909d95f5-6a66-42f5-b95e-f18492a95465"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 43,
                column: "PermissionControllerActionGuid",
                value: new Guid("1351b9c8-4831-4f7f-a648-e1e34a4584bb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 44,
                column: "PermissionControllerActionGuid",
                value: new Guid("946116bc-037f-4198-9f70-f31f7307560f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 45,
                column: "PermissionControllerActionGuid",
                value: new Guid("35d93e89-a243-4443-ad98-fcff1d71f7e3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 46,
                column: "PermissionControllerActionGuid",
                value: new Guid("925e7f40-7b2e-4aa4-b9bd-5afc9f6b2d2b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 47,
                column: "PermissionControllerActionGuid",
                value: new Guid("84b1f3b6-15d4-4c2d-82f0-ef890c4b35f3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 48,
                column: "PermissionControllerActionGuid",
                value: new Guid("95da0396-0c47-49d4-8ebe-153363332827"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 49,
                column: "PermissionControllerActionGuid",
                value: new Guid("e3d17f76-f922-48e0-92c6-1c1d75fffe67"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 50,
                column: "PermissionControllerActionGuid",
                value: new Guid("074b1c12-413d-47df-a1ef-1d9d504edaee"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 51,
                column: "PermissionControllerActionGuid",
                value: new Guid("a345af5a-f131-43f5-a4d9-4f96b3fb9ce1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 52,
                column: "PermissionControllerActionGuid",
                value: new Guid("fcb4804e-8a83-4989-a713-f2dbbc0425a3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 53,
                column: "PermissionControllerActionGuid",
                value: new Guid("7d4a4a58-2b45-4ea3-9997-2e2e5a29b3f7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 54,
                column: "PermissionControllerActionGuid",
                value: new Guid("727fb8a3-adbd-4188-a521-adaa793f47a4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 55,
                column: "PermissionControllerActionGuid",
                value: new Guid("ea7fe4ae-29b2-4143-97f3-1564bcae6036"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 56,
                column: "PermissionControllerActionGuid",
                value: new Guid("7c411abc-7103-4020-a50f-4e3e8e15cd1b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 57,
                column: "PermissionControllerActionGuid",
                value: new Guid("a8467cc3-1802-458a-b2ea-3b0a45c39f82"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 58,
                column: "PermissionControllerActionGuid",
                value: new Guid("c7729afe-3fce-4e3c-aada-e205e6d5c11d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 59,
                column: "PermissionControllerActionGuid",
                value: new Guid("165c283a-c49b-4f4c-b5bd-eb2731cf490a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 60,
                column: "PermissionControllerActionGuid",
                value: new Guid("e31f8471-e9dd-47e5-8358-71e58225384d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 61,
                column: "PermissionControllerActionGuid",
                value: new Guid("a2b8f941-039d-437d-87b7-ac195992c62c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 62,
                column: "PermissionControllerActionGuid",
                value: new Guid("fb8e8858-52c5-473d-910a-c2e2b0b4f864"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 63,
                column: "PermissionControllerActionGuid",
                value: new Guid("36264a77-866d-422d-a258-a11b5377eb46"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 64,
                column: "PermissionControllerActionGuid",
                value: new Guid("be70e459-5d22-4a2c-a558-ace28d5fd480"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 65,
                column: "PermissionControllerActionGuid",
                value: new Guid("a06672b0-d385-4109-81f8-168b0ac04ede"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 66,
                column: "PermissionControllerActionGuid",
                value: new Guid("4c0df3c9-9604-4e21-a3c0-79f7157bbad5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 67,
                column: "PermissionControllerActionGuid",
                value: new Guid("f30fb886-d113-4ce2-85c9-041c647bdc90"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 68,
                column: "PermissionControllerActionGuid",
                value: new Guid("eafbf268-bbd3-4d82-894c-2162a58c7efd"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 69,
                column: "PermissionControllerActionGuid",
                value: new Guid("10b351f2-44ac-4e06-bbb2-795e2c3a68df"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 70,
                column: "PermissionControllerActionGuid",
                value: new Guid("e593529e-8ff0-41d9-8150-1cada6c37063"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 71,
                column: "PermissionControllerActionGuid",
                value: new Guid("a133d091-7d44-49a8-843a-2eef82320df9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 72,
                column: "PermissionControllerActionGuid",
                value: new Guid("3f5fdc4c-310c-4205-a800-3afe3dfc5f3e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 73,
                column: "PermissionControllerActionGuid",
                value: new Guid("fd9d81b4-83ee-442b-8560-0cae7bb2ea7f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 74,
                column: "PermissionControllerActionGuid",
                value: new Guid("5eaa2a14-6372-42d8-b382-9902c26c92be"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 75,
                column: "PermissionControllerActionGuid",
                value: new Guid("cdd62c1b-34a9-4f47-adbc-e2fea847050f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 76,
                column: "PermissionControllerActionGuid",
                value: new Guid("ba86e73c-03a1-4091-87fc-2924018908b6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 77,
                column: "PermissionControllerActionGuid",
                value: new Guid("f7b92ab5-f8ef-44f9-b49d-1405cbb8c410"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 78,
                column: "PermissionControllerActionGuid",
                value: new Guid("0a776d8c-eefa-4568-8006-3a4a91c9adf1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 79,
                column: "PermissionControllerActionGuid",
                value: new Guid("2176df83-ec2c-4716-b7ba-548ed19e911c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 80,
                column: "PermissionControllerActionGuid",
                value: new Guid("7ba61ae2-f492-4541-9705-a1371e88d92d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 81,
                column: "PermissionControllerActionGuid",
                value: new Guid("0b2e971c-6c4d-48ff-b003-d034e1aef5dc"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 82,
                column: "PermissionControllerActionGuid",
                value: new Guid("bd330d8a-be0e-415c-802f-76c713a3230d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 83,
                column: "PermissionControllerActionGuid",
                value: new Guid("37ab1273-a114-4aa7-b418-c730ea8f2d8e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 84,
                column: "PermissionControllerActionGuid",
                value: new Guid("58b57e10-f223-466d-84fd-07e85f5f738b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 85,
                column: "PermissionControllerActionGuid",
                value: new Guid("6db56955-f5c9-4b8e-aa93-e2ed8968f493"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 86,
                column: "PermissionControllerActionGuid",
                value: new Guid("06b6f428-fc96-41c3-a215-030d1643e942"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 87,
                column: "PermissionControllerActionGuid",
                value: new Guid("4e0d1fa0-ba1a-4430-a021-01f0e472e7ab"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 88,
                column: "PermissionControllerActionGuid",
                value: new Guid("405941e2-1c09-4b49-8b8e-b445709e2633"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 89,
                column: "PermissionControllerActionGuid",
                value: new Guid("d2246c4e-4804-4f6d-966c-87cdc5ab5f10"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 90,
                column: "PermissionControllerActionGuid",
                value: new Guid("4f8e1c46-88c8-452b-8b32-b55167b3ee3d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 91,
                column: "PermissionControllerActionGuid",
                value: new Guid("acee5d91-c954-42f9-976c-772efa0bee3c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 92,
                column: "PermissionControllerActionGuid",
                value: new Guid("0a03ae69-6ebc-440d-8202-d3a4f6910536"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 93,
                column: "PermissionControllerActionGuid",
                value: new Guid("fc1dc00b-dab1-4658-a54f-93ce91ba8aa9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 94,
                column: "PermissionControllerActionGuid",
                value: new Guid("cacbab3c-50be-4cf1-913a-ef7fc56314da"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 95,
                column: "PermissionControllerActionGuid",
                value: new Guid("b695ae84-972a-434d-ab50-0aa1969a64fb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 96,
                column: "PermissionControllerActionGuid",
                value: new Guid("fe615a8d-5f9a-44f1-a2c1-5936f8fca62f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 97,
                column: "PermissionControllerActionGuid",
                value: new Guid("c3d1de77-1f47-40e4-9920-fa0a3b4d2464"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 98,
                column: "PermissionControllerActionGuid",
                value: new Guid("0d664a69-1ca0-4bfa-bcbc-009afcd6be53"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 99,
                column: "PermissionControllerActionGuid",
                value: new Guid("e7ce912f-a417-4756-ac66-b87d68a77f5e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 100,
                column: "PermissionControllerActionGuid",
                value: new Guid("9a5b92ef-a027-4b14-b3b3-1e6e4106364c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 101,
                column: "PermissionControllerActionGuid",
                value: new Guid("7eea80e2-5e2c-4ca9-b89f-161f4f5ab493"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 102,
                column: "PermissionControllerActionGuid",
                value: new Guid("9dd8df57-b41c-4d2f-b280-f609cb95a199"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 103,
                column: "PermissionControllerActionGuid",
                value: new Guid("e5d17918-0601-4cbd-ae76-2135921abba3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 104,
                column: "PermissionControllerActionGuid",
                value: new Guid("2a8ba341-2958-4af1-ba9b-f0701494ce87"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 105,
                column: "PermissionControllerActionGuid",
                value: new Guid("2c930da4-b423-4b2e-b9f2-170722fc5531"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 106,
                column: "PermissionControllerActionGuid",
                value: new Guid("b2d8db20-efa6-455f-b578-b0d0bc104454"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 107,
                column: "PermissionControllerActionGuid",
                value: new Guid("e20014d9-89fc-457b-835d-048b978ceeeb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 108,
                column: "PermissionControllerActionGuid",
                value: new Guid("635085b4-e8a8-4b34-b939-e91abff8453a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 109,
                column: "PermissionControllerActionGuid",
                value: new Guid("efdc955d-dc8a-4f91-a6b8-9698c8b37699"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 110,
                column: "PermissionControllerActionGuid",
                value: new Guid("93256541-2a09-4f39-aa50-715f1212cecd"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 111,
                column: "PermissionControllerActionGuid",
                value: new Guid("f6c3e5d6-64dc-40d1-a0f4-d6fc74f61d5e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 112,
                column: "PermissionControllerActionGuid",
                value: new Guid("6471d1eb-7e96-4617-bb33-1a9659c408bf"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 113,
                column: "PermissionControllerActionGuid",
                value: new Guid("b5b2ada3-e2ed-4ca6-8149-f4f1b35d5c1d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 114,
                column: "PermissionControllerActionGuid",
                value: new Guid("3b7c9827-9576-40af-90c7-1b76c20bdff7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 115,
                column: "PermissionControllerActionGuid",
                value: new Guid("2ab43676-6906-4516-a243-26df2a442b67"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 116,
                column: "PermissionControllerActionGuid",
                value: new Guid("ba20d36a-4898-4ce6-9786-231b47e41b1c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 117,
                column: "PermissionControllerActionGuid",
                value: new Guid("4ce3e6d7-fab8-4262-8d0a-acd16f7e9718"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 118,
                column: "PermissionControllerActionGuid",
                value: new Guid("952f5328-5212-4a4e-b879-8666ae4afb7c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 119,
                column: "PermissionControllerActionGuid",
                value: new Guid("9340749b-de4a-4686-b0c7-649541bbb3fa"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 120,
                column: "PermissionControllerActionGuid",
                value: new Guid("d601641f-a9f3-4639-a9aa-bb841a1e4cdc"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 121,
                column: "PermissionControllerActionGuid",
                value: new Guid("b21ec377-087e-4567-af4f-17df58c9019a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 122,
                column: "PermissionControllerActionGuid",
                value: new Guid("049ba99b-dfbc-45a3-acc9-00f45822f155"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 123,
                column: "PermissionControllerActionGuid",
                value: new Guid("d28cd179-7645-4cb3-8c35-0f3eb4b31999"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 124,
                column: "PermissionControllerActionGuid",
                value: new Guid("f7544643-dc91-44fc-94b7-d4712c27a54b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 125,
                column: "PermissionControllerActionGuid",
                value: new Guid("6ec894d5-f842-42a3-8951-48a1c6053928"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 126,
                column: "PermissionControllerActionGuid",
                value: new Guid("574a2c65-8b1a-4146-8c90-2a2cd3540a2d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 127,
                column: "PermissionControllerActionGuid",
                value: new Guid("a35b53a0-71bb-4a78-bbb3-856da5c15eb0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 128,
                column: "PermissionControllerActionGuid",
                value: new Guid("f7436767-a7fa-4d60-9009-fc5649ffc1d8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 129,
                column: "PermissionControllerActionGuid",
                value: new Guid("efe58fa6-b2bf-4176-802b-2ae951b94781"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 130,
                column: "PermissionControllerActionGuid",
                value: new Guid("01f34575-e6cc-44e7-9758-e45909dd27d5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 131,
                column: "PermissionControllerActionGuid",
                value: new Guid("72e84f04-e335-4b1a-a5b2-2e2b6b57ad93"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 132,
                column: "PermissionControllerActionGuid",
                value: new Guid("64e08b00-cc8a-4131-9ef4-7dff7361b3ee"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 133,
                column: "PermissionControllerActionGuid",
                value: new Guid("dc18cde4-b5c3-4c6e-b038-bc10672b454b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 134,
                column: "PermissionControllerActionGuid",
                value: new Guid("2e73c107-d865-4717-9f14-f9abe00d3912"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 135,
                column: "PermissionControllerActionGuid",
                value: new Guid("8881bbfc-eec0-494f-bfc9-a109e96c82cb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 136,
                column: "PermissionControllerActionGuid",
                value: new Guid("6f63e444-a2ff-4ef2-b93d-5728aa84da03"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 137,
                column: "PermissionControllerActionGuid",
                value: new Guid("9f2b1638-ad6c-47d3-b1e4-1c172b198ea4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 138,
                column: "PermissionControllerActionGuid",
                value: new Guid("7c7fda64-e0ea-42d9-b2b8-fda3dce93928"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 139,
                column: "PermissionControllerActionGuid",
                value: new Guid("dd7ea420-e898-45fb-a853-7764e66af53e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 140,
                column: "PermissionControllerActionGuid",
                value: new Guid("ca66cc8f-8e2b-4807-81da-345f767afec2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 141,
                column: "PermissionControllerActionGuid",
                value: new Guid("7a215cbc-dc93-4aed-845d-4ad686e46b03"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 142,
                column: "PermissionControllerActionGuid",
                value: new Guid("9e8b85b7-f7e3-4a65-8bc0-357c5ed4965b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 143,
                column: "PermissionControllerActionGuid",
                value: new Guid("b200508c-9670-4ba5-b8d8-624d481a9c1e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 144,
                column: "PermissionControllerActionGuid",
                value: new Guid("d25b68d1-5a8f-427a-bba5-0f1960d05afb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 145,
                column: "PermissionControllerActionGuid",
                value: new Guid("6ff190ec-ea36-4293-93b8-6a761bb17eb0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 146,
                column: "PermissionControllerActionGuid",
                value: new Guid("5ca2082f-4c96-499b-9159-b45bf5251019"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 147,
                column: "PermissionControllerActionGuid",
                value: new Guid("10634f3c-3de7-44c9-a865-286123adcb62"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 148,
                column: "PermissionControllerActionGuid",
                value: new Guid("e26ac19d-7b04-4b22-baae-b041b98425c9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 149,
                column: "PermissionControllerActionGuid",
                value: new Guid("69bdea19-075d-4653-bca9-84b9aa2d5f55"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 150,
                column: "PermissionControllerActionGuid",
                value: new Guid("1e7a1378-5aec-4a4c-aa92-c6672a03bdc9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 151,
                column: "PermissionControllerActionGuid",
                value: new Guid("43572ca1-3083-4073-b625-cedf6646d375"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 152,
                column: "PermissionControllerActionGuid",
                value: new Guid("12a2dd40-5ec5-4d24-97bd-e0995e403586"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 153,
                column: "PermissionControllerActionGuid",
                value: new Guid("3291e133-fdb9-4ec7-9289-a3c75a5a8293"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 154,
                column: "PermissionControllerActionGuid",
                value: new Guid("ee4b1539-214e-4ca5-93c0-92dc3c4b55eb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 155,
                column: "PermissionControllerActionGuid",
                value: new Guid("c4c90791-6445-473c-a2aa-61dea04ffb17"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 156,
                column: "PermissionControllerActionGuid",
                value: new Guid("254f01cd-a725-4691-b990-4c8acd6f6030"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 157,
                column: "PermissionControllerActionGuid",
                value: new Guid("53c790d4-35c6-47ad-99ac-98e2281ea9cd"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 158,
                column: "PermissionControllerActionGuid",
                value: new Guid("8f0393bc-2af5-495b-b22f-0764752264c7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 159,
                column: "PermissionControllerActionGuid",
                value: new Guid("4ffb9e79-1bc4-42f7-8145-ae583f2054d3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 160,
                column: "PermissionControllerActionGuid",
                value: new Guid("a354b224-d97c-415e-a5f9-d1524e13cfd0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 161,
                column: "PermissionControllerActionGuid",
                value: new Guid("a4063f77-01cb-483b-9b82-021396ac6407"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 162,
                column: "PermissionControllerActionGuid",
                value: new Guid("7a4f793d-1596-4557-ad85-4bad838da9ae"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 163,
                column: "PermissionControllerActionGuid",
                value: new Guid("53934347-bf8a-4d82-be34-27136facd46c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 164,
                column: "PermissionControllerActionGuid",
                value: new Guid("2d183815-55a7-4d43-b9b4-d37121045bdd"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 165,
                column: "PermissionControllerActionGuid",
                value: new Guid("0c5132d5-29b3-4b98-87df-f52ea11e4864"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 166,
                column: "PermissionControllerActionGuid",
                value: new Guid("6586cbec-68d2-481a-bcb1-51d3af9f5d97"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 167,
                column: "PermissionControllerActionGuid",
                value: new Guid("bed97b9f-b123-4c86-b719-e7a708ec3d80"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 168,
                column: "PermissionControllerActionGuid",
                value: new Guid("5ce84d6b-90b5-4e47-9f03-bbf190bf1df2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 169,
                column: "PermissionControllerActionGuid",
                value: new Guid("faa4e811-a475-4038-bb2a-a3df6ed21f27"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 170,
                column: "PermissionControllerActionGuid",
                value: new Guid("1120f075-7d05-4fd1-8ac8-3fc813d42cac"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 171,
                column: "PermissionControllerActionGuid",
                value: new Guid("a5ccf944-db0b-41a3-a1d0-0e3222a428c1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 172,
                column: "PermissionControllerActionGuid",
                value: new Guid("28ef3599-0b1d-4151-bf99-705bb55af0b6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 173,
                column: "PermissionControllerActionGuid",
                value: new Guid("18c93ad9-9b07-4456-8edd-e7b5da132597"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 174,
                column: "PermissionControllerActionGuid",
                value: new Guid("9de2fa1a-099e-4eae-ade6-9a8cc239388a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 175,
                column: "PermissionControllerActionGuid",
                value: new Guid("f4331580-ab3f-4afd-975f-fdfb9ca2ca58"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 176,
                column: "PermissionControllerActionGuid",
                value: new Guid("a591988b-3551-4283-b628-3845a701f46d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 177,
                column: "PermissionControllerActionGuid",
                value: new Guid("ce9e400f-5efd-43e0-a3db-43a9894ad622"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 178,
                column: "PermissionControllerActionGuid",
                value: new Guid("d60337b1-7fb3-4a04-9572-6967291a4c7c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 179,
                column: "PermissionControllerActionGuid",
                value: new Guid("5b283778-e131-40df-9d05-48e2f9517762"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 180,
                column: "PermissionControllerActionGuid",
                value: new Guid("078d0a71-e895-4854-aedd-f004ddd4c08b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 181,
                column: "PermissionControllerActionGuid",
                value: new Guid("6fca6369-efc2-41a2-8e9a-5b2fe651244e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 182,
                column: "PermissionControllerActionGuid",
                value: new Guid("3ce9003f-866e-4b03-9a0c-118a90652529"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 183,
                column: "PermissionControllerActionGuid",
                value: new Guid("712ac3ea-cdcc-426d-83df-214a531b8e7b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 184,
                column: "PermissionControllerActionGuid",
                value: new Guid("08fc81dd-3b1c-4f1b-8c47-8935ea6a4a24"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 185,
                column: "PermissionControllerActionGuid",
                value: new Guid("58941a07-e728-4312-995a-d2b4558c7ccb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 186,
                column: "PermissionControllerActionGuid",
                value: new Guid("0d276cf2-fd3a-4bb1-8b5f-384ab9b3d2c4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 187,
                column: "PermissionControllerActionGuid",
                value: new Guid("284dca91-7682-4791-9f58-06fc1932fc2a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 188,
                column: "PermissionControllerActionGuid",
                value: new Guid("f4953f0c-d9f8-49b8-a762-f3f8d5575659"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 189,
                column: "PermissionControllerActionGuid",
                value: new Guid("c417fca4-a9b6-45b2-81cb-19f458aa8fba"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 190,
                column: "PermissionControllerActionGuid",
                value: new Guid("7815e0e9-d1cf-4f68-98d1-9d8c07d48c5a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 191,
                column: "PermissionControllerActionGuid",
                value: new Guid("920c6955-6ced-4659-b27d-0e2ff98c6752"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 192,
                column: "PermissionControllerActionGuid",
                value: new Guid("04a00591-cc59-4bd4-8caa-7ee57f9594e2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 193,
                column: "PermissionControllerActionGuid",
                value: new Guid("6277b578-4ddf-4a8d-9c07-5ca8b3475295"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 194,
                column: "PermissionControllerActionGuid",
                value: new Guid("07de663d-ba28-42c6-8909-c64895733d02"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 195,
                column: "PermissionControllerActionGuid",
                value: new Guid("d6dccdf1-15e8-42ee-aaaf-485a80dafd0e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 196,
                column: "PermissionControllerActionGuid",
                value: new Guid("055f5e9f-eaed-4f2c-8167-40eb84ca7481"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 197,
                column: "PermissionControllerActionGuid",
                value: new Guid("a435637e-dc34-448c-bedd-2bf574daf435"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 198,
                column: "PermissionControllerActionGuid",
                value: new Guid("f992a39d-e8bb-4dec-a37f-8643cf19e455"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 199,
                column: "PermissionControllerActionGuid",
                value: new Guid("084dd82b-f56a-410f-ae31-7387bbdea942"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 200,
                column: "PermissionControllerActionGuid",
                value: new Guid("e9d1a1dd-4b7d-492e-b029-61f66eee2e12"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 201,
                column: "PermissionControllerActionGuid",
                value: new Guid("0fda49eb-8ad4-47cd-8426-551c00822862"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 202,
                column: "PermissionControllerActionGuid",
                value: new Guid("2f640b7c-d645-4730-a698-13cc5c1c27ce"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 203,
                column: "PermissionControllerActionGuid",
                value: new Guid("8296ed72-80e2-4b91-811e-e6cd1f86ba10"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 204,
                column: "PermissionControllerActionGuid",
                value: new Guid("bac88597-91bd-45c9-a39e-6bc586720cbf"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 205,
                column: "PermissionControllerActionGuid",
                value: new Guid("abe69f0d-a60b-4938-bd24-2006f2181254"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 206,
                column: "PermissionControllerActionGuid",
                value: new Guid("09387a73-d5ac-4ec2-952e-d3c56525b42e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 207,
                column: "PermissionControllerActionGuid",
                value: new Guid("02252195-f0c3-473a-9105-9709231bcf32"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 208,
                column: "PermissionControllerActionGuid",
                value: new Guid("0d30c16d-bfda-4a5f-b421-07cb620ab526"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 209,
                column: "PermissionControllerActionGuid",
                value: new Guid("4af9bf81-4524-4823-913b-5def2e074fc9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 210,
                column: "PermissionControllerActionGuid",
                value: new Guid("8a5e2c62-0524-4f26-a652-04b51277a8c8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 211,
                column: "PermissionControllerActionGuid",
                value: new Guid("78dfd9a1-2e3c-47f6-8eb0-fad152143413"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 212,
                column: "PermissionControllerActionGuid",
                value: new Guid("7e4aed85-5114-41b5-abc4-cd79e539d8d9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 213,
                column: "PermissionControllerActionGuid",
                value: new Guid("ecaf3e6d-d9e1-4386-a8f8-44a8cd932dde"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 214,
                column: "PermissionControllerActionGuid",
                value: new Guid("b8b5db4d-f939-4451-beee-1414b7e449f4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 215,
                column: "PermissionControllerActionGuid",
                value: new Guid("3456ca30-3449-40c8-85cf-4412c2a99aea"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 216,
                column: "PermissionControllerActionGuid",
                value: new Guid("a0def649-4fd7-48f9-ac52-d37bcfa06a81"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 217,
                column: "PermissionControllerActionGuid",
                value: new Guid("ccb7efd7-3148-43e0-80a8-1e0eb2aabefb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 218,
                column: "PermissionControllerActionGuid",
                value: new Guid("32213835-e9d0-4014-9c68-cef41a5eab98"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 219,
                column: "PermissionControllerActionGuid",
                value: new Guid("fa7d97da-77e5-4973-a63b-67e13b4502c7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 220,
                column: "PermissionControllerActionGuid",
                value: new Guid("403f574a-16ad-434e-99a6-e3e09514cd9e"));

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "City",
                keyColumn: "CityID",
                keyValue: 1,
                columns: new[] { "CityGuid", "CreateDate", "EnableDate" },
                values: new object[] { new Guid("ffc2903c-611f-449f-9803-bdbe2972d32c"), new DateTime(2021, 11, 7, 10, 28, 30, 420, DateTimeKind.Local).AddTicks(5461), new DateTime(2021, 11, 7, 10, 28, 30, 420, DateTimeKind.Local).AddTicks(6030) });

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "Country",
                keyColumn: "CountryID",
                keyValue: 1,
                columns: new[] { "CountryGuid", "CreateDate", "EnableDate" },
                values: new object[] { new Guid("36a8e0b2-c7bf-4df2-b125-8818e966206e"), new DateTime(2021, 11, 7, 10, 28, 30, 417, DateTimeKind.Local).AddTicks(2832), new DateTime(2021, 11, 7, 10, 28, 30, 418, DateTimeKind.Local).AddTicks(7397) });

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "Departments",
                keyColumn: "DepartmentsID",
                keyValue: 1,
                columns: new[] { "CreateDate", "DepartmentsGuid", "EnableDate" },
                values: new object[] { new DateTime(2021, 11, 7, 10, 28, 30, 424, DateTimeKind.Local).AddTicks(7276), new Guid("9030aa26-bbdf-44d9-8c8d-9ba543e94003"), new DateTime(2021, 11, 7, 10, 28, 30, 424, DateTimeKind.Local).AddTicks(8001) });

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "Jobs",
                keyColumn: "JobsID",
                keyValue: 1,
                columns: new[] { "CreateDate", "EnableDate", "JobsGuid" },
                values: new object[] { new DateTime(2021, 11, 7, 10, 28, 30, 423, DateTimeKind.Local).AddTicks(6399), new DateTime(2021, 11, 7, 10, 28, 30, 423, DateTimeKind.Local).AddTicks(6994), new Guid("e673640f-f33b-4c18-b6a9-581552e4e948") });

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "MainCategory",
                keyColumn: "MainCategoryID",
                keyValue: 1,
                columns: new[] { "CreateDate", "EnableDate", "MainCategoryGuid" },
                values: new object[] { new DateTime(2021, 11, 7, 10, 28, 30, 424, DateTimeKind.Local).AddTicks(1468), new DateTime(2021, 11, 7, 10, 28, 30, 424, DateTimeKind.Local).AddTicks(2066), new Guid("a924d45f-c43e-4b35-8caa-ef62d1b4340e") });

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "Nationality",
                keyColumn: "NationalityID",
                keyValue: 1,
                columns: new[] { "CreateDate", "EnableDate", "NationalityGuid" },
                values: new object[] { new DateTime(2021, 11, 7, 10, 28, 30, 421, DateTimeKind.Local).AddTicks(3013), new DateTime(2021, 11, 7, 10, 28, 30, 421, DateTimeKind.Local).AddTicks(3578), new Guid("277c06a2-de04-406d-8fb8-db914dbb6223") });

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "Region",
                keyColumn: "RegionID",
                keyValue: 1,
                columns: new[] { "CreateDate", "EnableDate", "RegionGuid" },
                values: new object[] { new DateTime(2021, 11, 7, 10, 28, 30, 419, DateTimeKind.Local).AddTicks(5130), new DateTime(2021, 11, 7, 10, 28, 30, 419, DateTimeKind.Local).AddTicks(5730), new Guid("358ffb60-c9e7-4e70-8f90-bc7b6b2d0392") });
        }
    }
}
