using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Homemade.DAL.Contract.Migrations
{
    public partial class adddriverAppVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DriverAndorid",
                schema: "Setting",
                table: "PaymentConfiguration",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DriverIOS",
                schema: "Setting",
                table: "PaymentConfiguration",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "273c91db-6841-4021-8fcd-6645af02f4f1");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "90bd6e3e-f029-46e0-91e9-b690632c78ff");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "47ac01ad-aa9a-4aa4-b38a-d1b548fe0f41");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "1cc9f2a7-42f3-49c2-a5b8-2394de6cf94e");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "4537ddef-4d99-43c8-bbd1-2f193f569925", "89b2a32e-d15b-4ab5-9828-9a42347b7186" });

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "DeliverySetting",
                keyColumn: "DeliverySettingID",
                keyValue: 1,
                column: "DeliverySettingGuid",
                value: new Guid("0e62828f-096f-400e-b5f6-e668cf66af01"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 1,
                column: "TransactionTypeGuid",
                value: new Guid("103e98c3-8e6a-490e-8d24-65feb0feb512"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 2,
                column: "TransactionTypeGuid",
                value: new Guid("bc5b5f05-82a7-4e3b-9f13-f7c188e722e2"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 3,
                column: "TransactionTypeGuid",
                value: new Guid("3c7e88c7-c763-4d03-b664-2f7be863239d"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 4,
                column: "TransactionTypeGuid",
                value: new Guid("029e60a8-10e3-4520-9c88-414beeba3af2"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 5,
                column: "TransactionTypeGuid",
                value: new Guid("5f80da18-8d09-4ed3-9d9c-a5ebd859cddb"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 6,
                column: "TransactionTypeGuid",
                value: new Guid("839d3cd1-8506-411b-8ed1-66ebb508a84f"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 7,
                column: "TransactionTypeGuid",
                value: new Guid("d4025f1d-240c-4037-be12-fb1eb86f365c"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 8,
                column: "TransactionTypeGuid",
                value: new Guid("4606081f-9a18-43c8-b666-085e9e78c0a8"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 9,
                column: "TransactionTypeGuid",
                value: new Guid("3ffd3bbc-180d-4b45-ae4a-3a4b43606664"));

            migrationBuilder.UpdateData(
                schema: "Emp",
                table: "Employees",
                keyColumn: "EntityEmpID",
                keyValue: 1,
                columns: new[] { "CreateDate", "EnableDate" },
                values: new object[] { new DateTime(2021, 10, 28, 11, 16, 37, 178, DateTimeKind.Local).AddTicks(6525), new DateTime(2021, 10, 28, 11, 16, 37, 178, DateTimeKind.Local).AddTicks(6971) });

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionAction",
                keyColumn: "PermissionActionID",
                keyValue: 1,
                column: "PermissionActionGuid",
                value: new Guid("f1411aae-9f35-4462-9062-10a70c7383aa"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionAction",
                keyColumn: "PermissionActionID",
                keyValue: 2,
                column: "PermissionActionGuid",
                value: new Guid("d14c31ef-79d4-46c7-b9de-87632bd6e50f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionAction",
                keyColumn: "PermissionActionID",
                keyValue: 3,
                column: "PermissionActionGuid",
                value: new Guid("e6923c74-185b-4148-8c48-b40edc4cb5ac"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionAction",
                keyColumn: "PermissionActionID",
                keyValue: 4,
                column: "PermissionActionGuid",
                value: new Guid("c8e77f4d-0bfb-4e8a-b744-e63ebb6f9c3a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 1,
                column: "PermissionControllerGuid",
                value: new Guid("48c42cac-8bbd-47d0-98d5-8b29c7c194aa"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 2,
                column: "PermissionControllerGuid",
                value: new Guid("4c6fce4d-661c-46bc-a5cc-d019e702ba03"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 3,
                column: "PermissionControllerGuid",
                value: new Guid("fda67833-1053-4d22-a0ca-e399f1529780"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 4,
                column: "PermissionControllerGuid",
                value: new Guid("be220a06-3908-431b-a0f4-9f83a7fec517"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 5,
                column: "PermissionControllerGuid",
                value: new Guid("ece79f3f-2d09-4766-97cb-1705b118221b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 6,
                column: "PermissionControllerGuid",
                value: new Guid("880101f3-b163-43f0-a476-7df7e40f242c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 7,
                column: "PermissionControllerGuid",
                value: new Guid("9ec660e0-d207-407e-aca4-fe1b4afaf439"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 8,
                column: "PermissionControllerGuid",
                value: new Guid("cbd92ad7-9a05-464b-baff-b23972543c7a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 9,
                column: "PermissionControllerGuid",
                value: new Guid("bc01532a-a94b-47d6-9d07-ed31c61793c9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 10,
                column: "PermissionControllerGuid",
                value: new Guid("ce85c843-af70-4032-b039-929a74fca6a9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 11,
                column: "PermissionControllerGuid",
                value: new Guid("9430fa35-38d5-4c2f-a9eb-60c9d0d5385c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 12,
                column: "PermissionControllerGuid",
                value: new Guid("86e0925d-d411-4213-93d3-139855d2cdfd"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 13,
                column: "PermissionControllerGuid",
                value: new Guid("9a25769d-0e53-46eb-be49-88def1cdc76f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 14,
                column: "PermissionControllerGuid",
                value: new Guid("bb145118-c80f-44f5-a63f-bca0a43923c3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 15,
                column: "PermissionControllerGuid",
                value: new Guid("639c38a6-ca2c-493c-acd3-4a7550d873a0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 16,
                column: "PermissionControllerGuid",
                value: new Guid("622a5475-2bac-49f7-be05-eb6045ca22a0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 17,
                column: "PermissionControllerGuid",
                value: new Guid("05eef774-1ba9-41d2-86a3-634cbe09c4db"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 18,
                column: "PermissionControllerGuid",
                value: new Guid("84b037ca-6a86-43fb-9359-82154042cfd8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 19,
                column: "PermissionControllerGuid",
                value: new Guid("5ff6ab47-6b4b-40cb-ad8b-84f9df078dff"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 20,
                column: "PermissionControllerGuid",
                value: new Guid("d9670dab-4e21-481f-aacc-76c0db4876fb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 21,
                column: "PermissionControllerGuid",
                value: new Guid("ee46d83d-1782-41e2-a78f-46c54456850a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 22,
                column: "PermissionControllerGuid",
                value: new Guid("68da61a1-bad1-4f46-8a89-b83c34d37b7c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 23,
                column: "PermissionControllerGuid",
                value: new Guid("4a0bf524-2f65-4e75-bf4b-da7be1622863"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 24,
                column: "PermissionControllerGuid",
                value: new Guid("6b5fb2b9-b709-4c69-b2c5-11ec7e23d394"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 25,
                column: "PermissionControllerGuid",
                value: new Guid("cb080f9e-ab90-463b-9fd7-1a5b2c105d6d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 26,
                column: "PermissionControllerGuid",
                value: new Guid("bff66208-c585-4eed-8927-b2b52ff5fe93"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 27,
                column: "PermissionControllerGuid",
                value: new Guid("aef35447-feaf-4ffa-ab2f-923b020997ae"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 28,
                column: "PermissionControllerGuid",
                value: new Guid("fb2af878-98e3-4f63-ba8a-7c2a448d83d6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 29,
                column: "PermissionControllerGuid",
                value: new Guid("f8008362-2011-4411-b3cf-063b39e94f8f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 30,
                column: "PermissionControllerGuid",
                value: new Guid("7c317c65-de3f-4ea7-b3e5-e1f0e9237905"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 31,
                column: "PermissionControllerGuid",
                value: new Guid("b3108504-6a41-4ea0-bed1-cafdc9f6243b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 32,
                column: "PermissionControllerGuid",
                value: new Guid("868ce993-2abf-4703-9269-29f7389d38f5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 33,
                column: "PermissionControllerGuid",
                value: new Guid("b12a9058-4d6c-40dc-aaaa-d703ba99f642"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 34,
                column: "PermissionControllerGuid",
                value: new Guid("633a76c5-10fb-4020-b93e-eab3fc9163f0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 35,
                column: "PermissionControllerGuid",
                value: new Guid("add4cc4e-540f-491d-9aa1-0b3fdcd5b390"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 36,
                column: "PermissionControllerGuid",
                value: new Guid("b4f0cef3-c778-4f28-8f16-bf926b1c10bf"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 37,
                column: "PermissionControllerGuid",
                value: new Guid("e930d908-d87d-4686-a63a-b260377b3153"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 38,
                column: "PermissionControllerGuid",
                value: new Guid("22bb43eb-8101-42f3-ab15-d8955b9428b1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 39,
                column: "PermissionControllerGuid",
                value: new Guid("0f420f90-1076-44e4-8b9b-2a29d1948f9b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 40,
                column: "PermissionControllerGuid",
                value: new Guid("21b4a88e-4fd0-49e1-b793-75d6dbffc628"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 41,
                column: "PermissionControllerGuid",
                value: new Guid("c8aa9baf-06aa-497f-9767-baaf6040ae29"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 42,
                column: "PermissionControllerGuid",
                value: new Guid("f07541fb-baa6-4e26-a998-a4e8ddd5a5f3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 43,
                column: "PermissionControllerGuid",
                value: new Guid("69337caf-c28e-419c-8fca-ab8c7992334b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 44,
                column: "PermissionControllerGuid",
                value: new Guid("9d559124-fdb2-4d05-8259-c24a3e986743"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 45,
                column: "PermissionControllerGuid",
                value: new Guid("3f52e0c4-8da4-4b08-bf59-4479f6873f79"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 46,
                column: "PermissionControllerGuid",
                value: new Guid("200f5fb6-9c16-4ed0-823c-5ce651988087"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 47,
                column: "PermissionControllerGuid",
                value: new Guid("37d0eed2-9747-4fd0-b076-66a6b541eb51"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 1,
                column: "PermissionControllerActionGuid",
                value: new Guid("55dbd157-2f89-4ff8-b796-5ca020c80ec8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 2,
                column: "PermissionControllerActionGuid",
                value: new Guid("ece7b20e-5112-4f83-a006-70457eed8157"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 3,
                column: "PermissionControllerActionGuid",
                value: new Guid("c870834a-0fce-4ad2-911c-cfe73c4b0187"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 4,
                column: "PermissionControllerActionGuid",
                value: new Guid("6b09d716-92d4-4b52-ae1a-60132ba18ec6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 5,
                column: "PermissionControllerActionGuid",
                value: new Guid("206ef894-0e3c-4f22-b355-d350a205cf0b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 6,
                column: "PermissionControllerActionGuid",
                value: new Guid("7e31765d-d3c0-4d6e-b58e-cd2c80f5c3c6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 7,
                column: "PermissionControllerActionGuid",
                value: new Guid("b75987ef-9436-4e31-b13c-bcd11af4bb21"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 8,
                column: "PermissionControllerActionGuid",
                value: new Guid("142c5352-288d-4e86-9fc8-ea9573538b77"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 9,
                column: "PermissionControllerActionGuid",
                value: new Guid("7cfa88aa-e985-4fb1-bc93-2127cc63f5ae"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 10,
                column: "PermissionControllerActionGuid",
                value: new Guid("5447d9f3-b570-4a77-bf9f-ce7d12caa43f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 11,
                column: "PermissionControllerActionGuid",
                value: new Guid("1ebde03c-a1e8-4cd5-9db7-1702ac21f23e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 12,
                column: "PermissionControllerActionGuid",
                value: new Guid("10a959d2-c2f7-4a1e-8c3e-129de8262f85"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 13,
                column: "PermissionControllerActionGuid",
                value: new Guid("3d2d698b-171a-4801-9d72-9097b1aa93c7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 14,
                column: "PermissionControllerActionGuid",
                value: new Guid("a6e77731-543f-4ed1-b68a-34581d6bdb87"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 15,
                column: "PermissionControllerActionGuid",
                value: new Guid("2b9dc2ee-e5e0-4167-a075-a145a0d0021a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 16,
                column: "PermissionControllerActionGuid",
                value: new Guid("4bd7ce3d-bdb9-48fb-b5e2-a9c45a5b6db3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 17,
                column: "PermissionControllerActionGuid",
                value: new Guid("3ab84989-4159-4e53-a654-f0eab6d29598"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 18,
                column: "PermissionControllerActionGuid",
                value: new Guid("6ad4d5a9-50be-4aee-9f1a-1da313554b86"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 19,
                column: "PermissionControllerActionGuid",
                value: new Guid("611001de-0bde-4e64-8633-83efe41eb767"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 20,
                column: "PermissionControllerActionGuid",
                value: new Guid("d95026e3-1e82-4703-9f68-af3e7290d64e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 21,
                column: "PermissionControllerActionGuid",
                value: new Guid("48e763be-1013-4b6b-a0f8-433be83ca2e4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 22,
                column: "PermissionControllerActionGuid",
                value: new Guid("11cf5339-a0be-4bb6-aa87-f6bdcfc893cf"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 23,
                column: "PermissionControllerActionGuid",
                value: new Guid("da8dc591-a57f-4131-a330-8980c85afe85"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 24,
                column: "PermissionControllerActionGuid",
                value: new Guid("44897c0c-93a3-4c7d-959e-1b7e8ce57904"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 25,
                column: "PermissionControllerActionGuid",
                value: new Guid("e7529606-cd54-4a12-af81-fd6b716627ff"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 26,
                column: "PermissionControllerActionGuid",
                value: new Guid("cfd9baf6-dde9-4c8e-931f-a247c2e7edeb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 27,
                column: "PermissionControllerActionGuid",
                value: new Guid("bbe92b29-c499-4f39-b802-11262a1e7ec9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 28,
                column: "PermissionControllerActionGuid",
                value: new Guid("4fe5a6fa-1d6d-4e90-b6a7-70ea550c4471"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 29,
                column: "PermissionControllerActionGuid",
                value: new Guid("42ce8d95-79d0-4d87-975d-c3e93b89a061"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 30,
                column: "PermissionControllerActionGuid",
                value: new Guid("ace094a8-40ba-4ffd-af2e-71cddbc5a52e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 31,
                column: "PermissionControllerActionGuid",
                value: new Guid("6cc14a1d-c0bd-4ca3-83a4-e80f409f6d2a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 32,
                column: "PermissionControllerActionGuid",
                value: new Guid("cdc81e56-80a2-49b6-aaeb-0d4458c7f465"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 33,
                column: "PermissionControllerActionGuid",
                value: new Guid("cc92c8e9-346d-4c89-9f09-8260dd0dd471"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 34,
                column: "PermissionControllerActionGuid",
                value: new Guid("1106f784-ea2b-43cf-b4b5-6628e43e7e2c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 35,
                column: "PermissionControllerActionGuid",
                value: new Guid("8d64f673-19cc-4dbe-9430-52e986d160fd"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 36,
                column: "PermissionControllerActionGuid",
                value: new Guid("6ffdc40d-aa38-4b3b-9f41-3f01229629f0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 37,
                column: "PermissionControllerActionGuid",
                value: new Guid("3db68606-aa78-4c58-bb92-f265ffd09433"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 38,
                column: "PermissionControllerActionGuid",
                value: new Guid("d4d43589-87ba-4e23-8d2d-71d015e5a8ff"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 39,
                column: "PermissionControllerActionGuid",
                value: new Guid("7a68b25e-44b7-470f-8042-6849788d0dec"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 40,
                column: "PermissionControllerActionGuid",
                value: new Guid("209af5dc-a64b-464b-a5b2-a65092c5a328"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 41,
                column: "PermissionControllerActionGuid",
                value: new Guid("77333a45-1980-42a7-8dbf-36167d3f4aa1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 42,
                column: "PermissionControllerActionGuid",
                value: new Guid("23ff690d-6ff9-4013-bcd8-ede4e5788d24"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 43,
                column: "PermissionControllerActionGuid",
                value: new Guid("75f16431-f8dd-436f-8898-246fb3471394"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 44,
                column: "PermissionControllerActionGuid",
                value: new Guid("ee5981b4-ac75-4fd6-b516-76a262fefa10"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 45,
                column: "PermissionControllerActionGuid",
                value: new Guid("71fd4ea1-5c4b-4bd0-84e4-527b3be5cb0c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 46,
                column: "PermissionControllerActionGuid",
                value: new Guid("c6acca8e-9a80-4e00-b72c-3a42ecd199a9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 47,
                column: "PermissionControllerActionGuid",
                value: new Guid("f3ac8a41-236c-48e0-acdd-c3ddf841fa4b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 48,
                column: "PermissionControllerActionGuid",
                value: new Guid("a9259d01-3f37-4a14-9b33-cc8bf3552e0d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 49,
                column: "PermissionControllerActionGuid",
                value: new Guid("24212cc9-dde8-404b-959c-69308bd434fb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 50,
                column: "PermissionControllerActionGuid",
                value: new Guid("4a74f211-6b91-47fc-8ed5-b68bc4afcfe4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 51,
                column: "PermissionControllerActionGuid",
                value: new Guid("59bab5d8-dab8-468d-b5eb-490d176d962e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 52,
                column: "PermissionControllerActionGuid",
                value: new Guid("61c727a1-abe6-4c22-befe-1b0ae2f86449"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 53,
                column: "PermissionControllerActionGuid",
                value: new Guid("d609a59b-9a1d-499b-81fc-3f5d9f811542"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 54,
                column: "PermissionControllerActionGuid",
                value: new Guid("ea8ecc29-2f85-480c-9dbd-b167c30ccb1a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 55,
                column: "PermissionControllerActionGuid",
                value: new Guid("5dd7b3d8-e746-4d3b-8e70-0a675766e1c7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 56,
                column: "PermissionControllerActionGuid",
                value: new Guid("d4d0af72-c63d-4fb4-b2dd-f513fb35a96b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 57,
                column: "PermissionControllerActionGuid",
                value: new Guid("532d4265-43cc-4a9c-9b94-4c2496e62a58"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 58,
                column: "PermissionControllerActionGuid",
                value: new Guid("5b2827a5-b688-4186-af99-1c12a94e3bb0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 59,
                column: "PermissionControllerActionGuid",
                value: new Guid("63a2c831-0ca3-4890-a12b-7c3311b2d43e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 60,
                column: "PermissionControllerActionGuid",
                value: new Guid("9928c54e-949e-4999-9988-6525fe1a8b3d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 61,
                column: "PermissionControllerActionGuid",
                value: new Guid("a947493f-b104-4629-ad8d-8be0bb6777c7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 62,
                column: "PermissionControllerActionGuid",
                value: new Guid("b5f55de3-1505-4bce-b192-a0971b557801"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 63,
                column: "PermissionControllerActionGuid",
                value: new Guid("957c233b-b1af-489d-b6e0-e2d8e5dbe723"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 64,
                column: "PermissionControllerActionGuid",
                value: new Guid("37099aeb-56fb-46d2-ba0b-52a6647ce593"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 65,
                column: "PermissionControllerActionGuid",
                value: new Guid("c1650199-d6c4-4c32-be81-339d2eee23d5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 66,
                column: "PermissionControllerActionGuid",
                value: new Guid("4fe66776-186b-471b-94f7-6187512e5547"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 67,
                column: "PermissionControllerActionGuid",
                value: new Guid("03730294-e8a0-47e2-a0c4-3c5a6afe4c65"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 68,
                column: "PermissionControllerActionGuid",
                value: new Guid("ea4b9fa0-d088-45c0-ad16-7200ffa519fd"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 69,
                column: "PermissionControllerActionGuid",
                value: new Guid("aed2baae-7c14-42b6-8993-7d38b3e65cff"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 70,
                column: "PermissionControllerActionGuid",
                value: new Guid("7e5b8ba5-6bc0-474c-8ec2-6f5d3a6d7abf"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 71,
                column: "PermissionControllerActionGuid",
                value: new Guid("a5d63edc-21a7-4f0f-a112-c76feac39755"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 72,
                column: "PermissionControllerActionGuid",
                value: new Guid("bf9194dc-87cf-4da2-a6fc-a6a90adc8c46"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 73,
                column: "PermissionControllerActionGuid",
                value: new Guid("a19ea19a-90a3-4d4d-b91b-e8f370d16c24"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 74,
                column: "PermissionControllerActionGuid",
                value: new Guid("904be02c-9616-41d1-9181-baf93324c139"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 75,
                column: "PermissionControllerActionGuid",
                value: new Guid("fc5fef06-5b46-44f1-86b0-665be65cdb69"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 76,
                column: "PermissionControllerActionGuid",
                value: new Guid("2b9e1220-817c-42e2-aa29-78232ec81c57"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 77,
                column: "PermissionControllerActionGuid",
                value: new Guid("48ae2ee1-a3e4-4f08-a144-3865b3b2547e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 78,
                column: "PermissionControllerActionGuid",
                value: new Guid("5d7f2266-70e8-48c0-8431-c70cb92b81b1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 79,
                column: "PermissionControllerActionGuid",
                value: new Guid("19ff2960-80d1-4dd1-b6ca-5986e5088e92"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 80,
                column: "PermissionControllerActionGuid",
                value: new Guid("5fe5c9a6-eb4d-4c30-96fa-9bf534c8b695"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 81,
                column: "PermissionControllerActionGuid",
                value: new Guid("4ce9d192-6c05-4a60-a4c4-bb2f6798ad30"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 82,
                column: "PermissionControllerActionGuid",
                value: new Guid("4fc37307-78ef-4080-9223-57ae8efa8b5c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 83,
                column: "PermissionControllerActionGuid",
                value: new Guid("930e4324-877c-4452-9f3f-389a591c02f6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 84,
                column: "PermissionControllerActionGuid",
                value: new Guid("91a849ff-77f1-4508-9d52-5a54fc4eea77"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 85,
                column: "PermissionControllerActionGuid",
                value: new Guid("963d713e-3da0-40f1-a92d-0ce12fee2dbe"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 86,
                column: "PermissionControllerActionGuid",
                value: new Guid("376b0e85-ea1a-4d7b-8264-4206e49a9940"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 87,
                column: "PermissionControllerActionGuid",
                value: new Guid("e8db118b-2034-4d1e-b0d8-04bb7d2eed2a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 88,
                column: "PermissionControllerActionGuid",
                value: new Guid("e61ab272-e078-42ee-8dad-88be96c9b721"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 89,
                column: "PermissionControllerActionGuid",
                value: new Guid("3400cf6f-55c7-477c-97c7-c9561210672d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 90,
                column: "PermissionControllerActionGuid",
                value: new Guid("e7ce54e0-4380-4e45-b6a3-9d0ff9a492e0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 91,
                column: "PermissionControllerActionGuid",
                value: new Guid("38266d10-c35b-4dfe-9007-f2fe2f49dfd8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 92,
                column: "PermissionControllerActionGuid",
                value: new Guid("7f18a303-5eb9-4bf5-af50-a718b39ccdab"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 93,
                column: "PermissionControllerActionGuid",
                value: new Guid("8cb2c79c-1a04-45de-988a-7ab99bfda90c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 94,
                column: "PermissionControllerActionGuid",
                value: new Guid("97977d10-c741-49fd-bd57-6783bc57a6eb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 95,
                column: "PermissionControllerActionGuid",
                value: new Guid("0cdc857d-0102-47f8-b674-46225807c51c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 96,
                column: "PermissionControllerActionGuid",
                value: new Guid("81d51f16-0d53-43c9-a708-f091a5a0f256"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 97,
                column: "PermissionControllerActionGuid",
                value: new Guid("49e1f44e-15fc-4685-b938-180ed9ed93c8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 98,
                column: "PermissionControllerActionGuid",
                value: new Guid("5296ab5f-60f4-4c4a-b375-6f1d9e35e743"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 99,
                column: "PermissionControllerActionGuid",
                value: new Guid("a407f917-b934-4b7a-b15a-0f64d0e387a4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 100,
                column: "PermissionControllerActionGuid",
                value: new Guid("12e3d4ce-1f9d-49ff-a9fa-786f302b55e1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 101,
                column: "PermissionControllerActionGuid",
                value: new Guid("8bf836fb-d977-4f30-9a4c-5be26d7eb069"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 102,
                column: "PermissionControllerActionGuid",
                value: new Guid("fd57f6df-2473-4e94-bc2a-6f1ea0ef228f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 103,
                column: "PermissionControllerActionGuid",
                value: new Guid("99b749f5-46e0-489a-852e-e7b400488322"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 104,
                column: "PermissionControllerActionGuid",
                value: new Guid("1dcc9d38-93a3-43bf-92cd-5bab93aa31cf"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 105,
                column: "PermissionControllerActionGuid",
                value: new Guid("f821fdbb-201d-40c3-9d01-5b7e39013aa2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 106,
                column: "PermissionControllerActionGuid",
                value: new Guid("96c2cd12-1ba6-4902-8592-a75205d2a7f3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 107,
                column: "PermissionControllerActionGuid",
                value: new Guid("198dfabe-df1f-4ffb-8d6e-18aca1ad40a5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 108,
                column: "PermissionControllerActionGuid",
                value: new Guid("309aeb04-b8a5-466f-b082-7e77429ece2b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 109,
                column: "PermissionControllerActionGuid",
                value: new Guid("78268f29-13c2-4f94-ade2-e9b99162e0d6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 110,
                column: "PermissionControllerActionGuid",
                value: new Guid("2732ca53-268c-4605-bc4e-933b0eb60008"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 111,
                column: "PermissionControllerActionGuid",
                value: new Guid("ec9fda1e-3e69-4399-9a2e-4c533a301faa"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 112,
                column: "PermissionControllerActionGuid",
                value: new Guid("ee29d2f8-d1f9-4a55-92c9-479e648202ea"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 113,
                column: "PermissionControllerActionGuid",
                value: new Guid("f8a9bcac-a5c5-4a8a-8235-538c79886115"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 114,
                column: "PermissionControllerActionGuid",
                value: new Guid("81ceb0ba-f5e7-4a90-b944-f2e59faaa77d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 115,
                column: "PermissionControllerActionGuid",
                value: new Guid("b81e97f3-503a-4740-a39a-af57038ef755"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 116,
                column: "PermissionControllerActionGuid",
                value: new Guid("a688e24e-564e-4681-ae60-a9c5b3440986"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 117,
                column: "PermissionControllerActionGuid",
                value: new Guid("69dac7a4-4fe9-475f-aca8-b1c0aeea3a75"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 118,
                column: "PermissionControllerActionGuid",
                value: new Guid("58ddb19a-dfa1-4244-a1f0-b08ccc72dcea"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 119,
                column: "PermissionControllerActionGuid",
                value: new Guid("628c40ef-ed82-4f98-ba58-faf3039b5168"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 120,
                column: "PermissionControllerActionGuid",
                value: new Guid("53172b7f-6846-4689-a367-94f1dac90f63"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 121,
                column: "PermissionControllerActionGuid",
                value: new Guid("b67f4e54-6798-4213-a8e9-f83ca8760d5a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 122,
                column: "PermissionControllerActionGuid",
                value: new Guid("317c20b5-72c7-4402-89f7-3a3c48d331fb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 123,
                column: "PermissionControllerActionGuid",
                value: new Guid("99d517fb-0dc5-4891-9122-0b691df65aa1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 124,
                column: "PermissionControllerActionGuid",
                value: new Guid("83636f0f-3687-4c95-859d-326c6ef01cbd"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 125,
                column: "PermissionControllerActionGuid",
                value: new Guid("b9ccad10-d8df-43aa-885a-5a29bb33dabb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 126,
                column: "PermissionControllerActionGuid",
                value: new Guid("0f7ea7bc-1068-4a1f-aa0b-1563238e2bf7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 127,
                column: "PermissionControllerActionGuid",
                value: new Guid("ccf2c1bf-bcc0-4b5d-acaf-e809eb07aef0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 128,
                column: "PermissionControllerActionGuid",
                value: new Guid("208e6d40-f008-400e-afa5-dbbf6009f948"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 129,
                column: "PermissionControllerActionGuid",
                value: new Guid("ec564c3d-e1b0-487e-ab6c-72de8c8c571a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 130,
                column: "PermissionControllerActionGuid",
                value: new Guid("83a3a37a-8b69-416d-9d7c-1ddcab0dec65"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 131,
                column: "PermissionControllerActionGuid",
                value: new Guid("4e908004-0777-4fdb-be6c-4450d256a5e7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 132,
                column: "PermissionControllerActionGuid",
                value: new Guid("7ee5af22-5a78-482f-a822-43e16472f595"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 133,
                column: "PermissionControllerActionGuid",
                value: new Guid("847307c0-aff4-4198-a9e3-e330a86c3735"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 134,
                column: "PermissionControllerActionGuid",
                value: new Guid("0e4e01a5-4603-48c4-822a-32ed77f9feb1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 135,
                column: "PermissionControllerActionGuid",
                value: new Guid("3ced67d6-98e2-4452-8d45-c09666bef3c9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 136,
                column: "PermissionControllerActionGuid",
                value: new Guid("2a5c7b82-2dea-4e5e-b535-0e7ea535f275"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 137,
                column: "PermissionControllerActionGuid",
                value: new Guid("db01d803-3d11-4556-ad3f-7856627f1359"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 138,
                column: "PermissionControllerActionGuid",
                value: new Guid("2e410775-03e5-4a18-9bc1-6a6c1d326069"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 139,
                column: "PermissionControllerActionGuid",
                value: new Guid("7198c6fe-e95e-4fc9-9dce-80cd1e36f31b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 140,
                column: "PermissionControllerActionGuid",
                value: new Guid("87f1d66d-18e0-4de3-8039-078662b7b9fd"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 141,
                column: "PermissionControllerActionGuid",
                value: new Guid("231a6324-a984-4c2c-8ed0-5fc8ced2564d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 142,
                column: "PermissionControllerActionGuid",
                value: new Guid("6a35af37-4c22-48dd-b279-57b35e77fab1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 143,
                column: "PermissionControllerActionGuid",
                value: new Guid("8210220f-06eb-4912-bfda-f98ec1aaded1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 144,
                column: "PermissionControllerActionGuid",
                value: new Guid("4c31af79-566c-4683-b2b3-0f3e4fb02b40"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 145,
                column: "PermissionControllerActionGuid",
                value: new Guid("8594914d-1f9a-4465-8518-40b3489fe185"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 146,
                column: "PermissionControllerActionGuid",
                value: new Guid("08ea5ef8-b921-4472-9107-367b34ae7bd8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 147,
                column: "PermissionControllerActionGuid",
                value: new Guid("979fee1b-9157-4ba4-a586-fd63ab94a3fd"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 148,
                column: "PermissionControllerActionGuid",
                value: new Guid("5e1755b9-4eeb-4d03-bb47-9fce12255f89"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 149,
                column: "PermissionControllerActionGuid",
                value: new Guid("79d57c0a-be91-40d6-b6ca-ff767dc6415a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 150,
                column: "PermissionControllerActionGuid",
                value: new Guid("da11e5dc-ec64-483f-93c6-c028d06589ad"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 151,
                column: "PermissionControllerActionGuid",
                value: new Guid("74463c56-fec1-4bc0-91df-25086b902521"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 152,
                column: "PermissionControllerActionGuid",
                value: new Guid("0525c002-eb13-41c9-93e0-a1f921efdaa3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 153,
                column: "PermissionControllerActionGuid",
                value: new Guid("3e959eb6-8d35-496b-8f32-089ba17e5f92"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 154,
                column: "PermissionControllerActionGuid",
                value: new Guid("304efef8-10ab-4284-945b-a143e4534520"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 155,
                column: "PermissionControllerActionGuid",
                value: new Guid("cd9435a4-dad0-4d1f-92f0-91b739420293"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 156,
                column: "PermissionControllerActionGuid",
                value: new Guid("684d6a0d-3dc9-420f-a541-2d19a92499bd"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 157,
                column: "PermissionControllerActionGuid",
                value: new Guid("5f5e0653-4ff0-40c7-99ee-3cacd575ce79"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 158,
                column: "PermissionControllerActionGuid",
                value: new Guid("f553d36b-90a9-4f65-9609-b92995ffa969"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 159,
                column: "PermissionControllerActionGuid",
                value: new Guid("39b39fac-4a29-4363-ae24-3f414f0e6651"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 160,
                column: "PermissionControllerActionGuid",
                value: new Guid("3816b5ed-134c-4cbf-8e82-5126a6641f0c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 161,
                column: "PermissionControllerActionGuid",
                value: new Guid("1a00618d-017a-43df-bbfa-3e3d1fecc879"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 162,
                column: "PermissionControllerActionGuid",
                value: new Guid("04058047-7bd3-4e37-9bfb-47660013cacc"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 163,
                column: "PermissionControllerActionGuid",
                value: new Guid("18b0a157-c6b8-4dc3-9d1d-78a55facbae0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 164,
                column: "PermissionControllerActionGuid",
                value: new Guid("3e038844-0944-4524-9c58-4dc3ed645fc4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 165,
                column: "PermissionControllerActionGuid",
                value: new Guid("660fd550-2a08-4d12-9abe-803cc3dac926"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 166,
                column: "PermissionControllerActionGuid",
                value: new Guid("2c5859ce-8bd8-451f-bb0f-b6aad6d56166"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 167,
                column: "PermissionControllerActionGuid",
                value: new Guid("240ba65a-49ce-4974-b8d3-f26441e6e878"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 168,
                column: "PermissionControllerActionGuid",
                value: new Guid("72271de8-7753-42b9-b53a-2e79e1301365"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 169,
                column: "PermissionControllerActionGuid",
                value: new Guid("97b6e4cd-90ab-401e-a0b8-7873f1ffc9ab"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 170,
                column: "PermissionControllerActionGuid",
                value: new Guid("abbb6b6f-a8f6-4bce-b959-b14f1db7399f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 171,
                column: "PermissionControllerActionGuid",
                value: new Guid("d056ce5b-0f56-4f99-a0ec-d5cdb5ac5120"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 172,
                column: "PermissionControllerActionGuid",
                value: new Guid("33a34c9b-7825-43ba-b2db-71840797d570"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 173,
                column: "PermissionControllerActionGuid",
                value: new Guid("b58b593b-09fc-44cd-8909-3eef245541a8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 174,
                column: "PermissionControllerActionGuid",
                value: new Guid("a5a0531d-f80d-4144-972a-85df3f032a30"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 175,
                column: "PermissionControllerActionGuid",
                value: new Guid("1c1956d9-5cfd-4513-9b89-5cfa5032c6a6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 176,
                column: "PermissionControllerActionGuid",
                value: new Guid("0f151f6c-d501-4b93-a133-5fb4c0483f58"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 177,
                column: "PermissionControllerActionGuid",
                value: new Guid("26beae68-0597-4baa-9f29-e72eff813ac8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 178,
                column: "PermissionControllerActionGuid",
                value: new Guid("883a1ed4-8b64-4bd4-95b6-925484c412e5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 179,
                column: "PermissionControllerActionGuid",
                value: new Guid("1b7093c9-9cdf-42ff-816f-ec7c6212a87f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 180,
                column: "PermissionControllerActionGuid",
                value: new Guid("abe9d3d3-a93a-404b-b1a7-935b2d222146"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 181,
                column: "PermissionControllerActionGuid",
                value: new Guid("494c44cb-73ea-4568-b60d-13d9e123b7ea"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 182,
                column: "PermissionControllerActionGuid",
                value: new Guid("d6e38e73-3f79-41e7-83f5-5f3ff5a11998"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 183,
                column: "PermissionControllerActionGuid",
                value: new Guid("c4a7b39f-11f7-406c-b24f-cd57c2f8d453"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 184,
                column: "PermissionControllerActionGuid",
                value: new Guid("68f10c79-7552-4835-8de9-9f353d10da5f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 185,
                column: "PermissionControllerActionGuid",
                value: new Guid("ea51a8fb-cb6c-4d91-bdfe-06ef69debb4a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 186,
                column: "PermissionControllerActionGuid",
                value: new Guid("cd52f9a1-d457-4258-8c35-49024b3da80f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 187,
                column: "PermissionControllerActionGuid",
                value: new Guid("97dd834b-4707-463d-acda-a7a68d1f4931"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 188,
                column: "PermissionControllerActionGuid",
                value: new Guid("a1773d6d-f511-48c1-a529-21a97e68e913"));

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "City",
                keyColumn: "CityID",
                keyValue: 1,
                columns: new[] { "CityGuid", "CreateDate", "EnableDate" },
                values: new object[] { new Guid("68a06cea-9760-4ada-b8ab-a13f14912c60"), new DateTime(2021, 10, 28, 11, 16, 37, 173, DateTimeKind.Local).AddTicks(2229), new DateTime(2021, 10, 28, 11, 16, 37, 173, DateTimeKind.Local).AddTicks(2987) });

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "Country",
                keyColumn: "CountryID",
                keyValue: 1,
                columns: new[] { "CountryGuid", "CreateDate", "EnableDate" },
                values: new object[] { new Guid("a08f3349-6ef1-4c88-b191-2463d09695cf"), new DateTime(2021, 10, 28, 11, 16, 37, 170, DateTimeKind.Local).AddTicks(1112), new DateTime(2021, 10, 28, 11, 16, 37, 171, DateTimeKind.Local).AddTicks(2527) });

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "Departments",
                keyColumn: "DepartmentsID",
                keyValue: 1,
                columns: new[] { "CreateDate", "DepartmentsGuid", "EnableDate" },
                values: new object[] { new DateTime(2021, 10, 28, 11, 16, 37, 177, DateTimeKind.Local).AddTicks(6087), new Guid("71844f82-3a22-4b9e-b157-53013ffff313"), new DateTime(2021, 10, 28, 11, 16, 37, 177, DateTimeKind.Local).AddTicks(6857) });

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "Jobs",
                keyColumn: "JobsID",
                keyValue: 1,
                columns: new[] { "CreateDate", "EnableDate", "JobsGuid" },
                values: new object[] { new DateTime(2021, 10, 28, 11, 16, 37, 176, DateTimeKind.Local).AddTicks(3922), new DateTime(2021, 10, 28, 11, 16, 37, 176, DateTimeKind.Local).AddTicks(5059), new Guid("10f2442e-fe98-48ab-b9f4-f8c0d77a8bb2") });

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "MainCategory",
                keyColumn: "MainCategoryID",
                keyValue: 1,
                columns: new[] { "CreateDate", "EnableDate", "MainCategoryGuid" },
                values: new object[] { new DateTime(2021, 10, 28, 11, 16, 37, 176, DateTimeKind.Local).AddTicks(9538), new DateTime(2021, 10, 28, 11, 16, 37, 177, DateTimeKind.Local).AddTicks(344), new Guid("12f9f8dc-bba5-436e-b43b-4204bb41cc4f") });

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "Nationality",
                keyColumn: "NationalityID",
                keyValue: 1,
                columns: new[] { "CreateDate", "EnableDate", "NationalityGuid" },
                values: new object[] { new DateTime(2021, 10, 28, 11, 16, 37, 174, DateTimeKind.Local).AddTicks(616), new DateTime(2021, 10, 28, 11, 16, 37, 174, DateTimeKind.Local).AddTicks(1383), new Guid("87a3412f-05a6-4738-a92a-b4fb8c56e575") });

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "Region",
                keyColumn: "RegionID",
                keyValue: 1,
                columns: new[] { "CreateDate", "EnableDate", "RegionGuid" },
                values: new object[] { new DateTime(2021, 10, 28, 11, 16, 37, 172, DateTimeKind.Local).AddTicks(834), new DateTime(2021, 10, 28, 11, 16, 37, 172, DateTimeKind.Local).AddTicks(1632), new Guid("03dbf237-8b7d-453e-b98e-b19e1dd146ee") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DriverAndorid",
                schema: "Setting",
                table: "PaymentConfiguration");

            migrationBuilder.DropColumn(
                name: "DriverIOS",
                schema: "Setting",
                table: "PaymentConfiguration");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "416f32b9-a78a-4327-a776-31c961fbd3f7");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "bd80e3e7-4a66-4c7b-90b1-a4cbb0832f91");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "d22f17e8-43d3-48f4-9c6c-41e727fe0c89");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "9a0971a7-84d1-4187-9360-bdcd313203c0");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "263427bd-9a05-4807-b750-181b5c63ac70", "331353e5-0e9c-48be-908c-9ae53cf41048" });

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "DeliverySetting",
                keyColumn: "DeliverySettingID",
                keyValue: 1,
                column: "DeliverySettingGuid",
                value: new Guid("50b5e540-e336-4e72-96d4-da52cd578608"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 1,
                column: "TransactionTypeGuid",
                value: new Guid("4306a541-b76f-45ca-9298-6369d93a8050"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 2,
                column: "TransactionTypeGuid",
                value: new Guid("6731037a-588c-43b2-91a5-3ce833571046"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 3,
                column: "TransactionTypeGuid",
                value: new Guid("566a0823-cb34-44cb-91ed-c17084f66dd4"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 4,
                column: "TransactionTypeGuid",
                value: new Guid("21bf4c49-aad6-450d-b111-fcb3ac8b3303"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 5,
                column: "TransactionTypeGuid",
                value: new Guid("509c448d-d7ed-4805-ad49-5d2104c55d14"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 6,
                column: "TransactionTypeGuid",
                value: new Guid("c778f9d1-5acb-44fe-bd58-88fe92c2c5a5"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 7,
                column: "TransactionTypeGuid",
                value: new Guid("1bb6260b-1146-4137-9fef-6d8b7dd86058"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 8,
                column: "TransactionTypeGuid",
                value: new Guid("ec225d5d-3e01-4618-b42a-289dbc90090d"));

            migrationBuilder.UpdateData(
                schema: "Driver",
                table: "TransactionType",
                keyColumn: "TransactionTypeID",
                keyValue: 9,
                column: "TransactionTypeGuid",
                value: new Guid("a06e1bb8-9bb5-43bb-a127-74a6236d3193"));

            migrationBuilder.UpdateData(
                schema: "Emp",
                table: "Employees",
                keyColumn: "EntityEmpID",
                keyValue: 1,
                columns: new[] { "CreateDate", "EnableDate" },
                values: new object[] { new DateTime(2021, 10, 27, 22, 19, 10, 269, DateTimeKind.Local).AddTicks(8020), new DateTime(2021, 10, 27, 22, 19, 10, 269, DateTimeKind.Local).AddTicks(8357) });

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionAction",
                keyColumn: "PermissionActionID",
                keyValue: 1,
                column: "PermissionActionGuid",
                value: new Guid("838f0f88-84bd-4800-a161-1a60710070ec"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionAction",
                keyColumn: "PermissionActionID",
                keyValue: 2,
                column: "PermissionActionGuid",
                value: new Guid("b7213f8e-4c94-4f76-9014-1135c05aed61"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionAction",
                keyColumn: "PermissionActionID",
                keyValue: 3,
                column: "PermissionActionGuid",
                value: new Guid("32448d34-6da6-4fd7-a4d0-4ecd7486c2e6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionAction",
                keyColumn: "PermissionActionID",
                keyValue: 4,
                column: "PermissionActionGuid",
                value: new Guid("d3452fee-09fb-484b-acfa-784b52b30154"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 1,
                column: "PermissionControllerGuid",
                value: new Guid("76da80fe-9ca0-4909-9295-07e704e51d9f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 2,
                column: "PermissionControllerGuid",
                value: new Guid("7dc60a83-6489-4bb1-814d-cd566adf1a72"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 3,
                column: "PermissionControllerGuid",
                value: new Guid("53a2d385-ce23-4b87-b460-aa5fdb0b06fa"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 4,
                column: "PermissionControllerGuid",
                value: new Guid("81d85c09-1999-4737-b729-d287db569b31"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 5,
                column: "PermissionControllerGuid",
                value: new Guid("1f7013bb-e938-4ed4-ad47-a57b603fbd46"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 6,
                column: "PermissionControllerGuid",
                value: new Guid("640b144c-3973-4f26-b863-848a1cd6957a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 7,
                column: "PermissionControllerGuid",
                value: new Guid("9d236a77-dde8-47b0-8130-f06c11b9ebd8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 8,
                column: "PermissionControllerGuid",
                value: new Guid("cfc06014-0319-4e78-aadb-5e6ccaf5049e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 9,
                column: "PermissionControllerGuid",
                value: new Guid("15f6ff4d-138a-4c1c-99a2-9a1c3433bbc0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 10,
                column: "PermissionControllerGuid",
                value: new Guid("b5e3d49c-9a55-40c4-b53b-8ce5c9463dd9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 11,
                column: "PermissionControllerGuid",
                value: new Guid("dd7e9346-74aa-41d3-9b92-40094d4d8456"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 12,
                column: "PermissionControllerGuid",
                value: new Guid("146f6dcc-02fc-4a88-9611-2d62865a4453"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 13,
                column: "PermissionControllerGuid",
                value: new Guid("abe8b0bc-6eab-4299-801f-ee7ce762eae7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 14,
                column: "PermissionControllerGuid",
                value: new Guid("31030759-f477-47b8-bc16-71cb05131e19"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 15,
                column: "PermissionControllerGuid",
                value: new Guid("ef351eef-f0e8-4d66-867c-72dc9615b15d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 16,
                column: "PermissionControllerGuid",
                value: new Guid("902b924d-1847-4205-ae31-e0606d02bc53"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 17,
                column: "PermissionControllerGuid",
                value: new Guid("af3e859e-9202-4a12-960e-9b5486a2cc06"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 18,
                column: "PermissionControllerGuid",
                value: new Guid("2b1cb66b-962d-48f8-947b-bd8c0b3572e9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 19,
                column: "PermissionControllerGuid",
                value: new Guid("10cb3a7c-75a0-44b8-94e9-3f43b88387d3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 20,
                column: "PermissionControllerGuid",
                value: new Guid("390507a0-5a5f-4b7b-9277-bbda9a72ed8c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 21,
                column: "PermissionControllerGuid",
                value: new Guid("a69ec005-c703-4afc-a619-7ac79ca9e53e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 22,
                column: "PermissionControllerGuid",
                value: new Guid("60ba8fc8-1c50-42c4-aec7-caf11c841d6c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 23,
                column: "PermissionControllerGuid",
                value: new Guid("4a627a31-f735-4421-b4ce-6b501fe97694"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 24,
                column: "PermissionControllerGuid",
                value: new Guid("7bb40a96-598d-450e-8c87-f9888ed9272c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 25,
                column: "PermissionControllerGuid",
                value: new Guid("e5f9db51-91bf-4e08-91da-732be9c2371e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 26,
                column: "PermissionControllerGuid",
                value: new Guid("01e5b808-640f-4bfa-826e-4e8b7f864185"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 27,
                column: "PermissionControllerGuid",
                value: new Guid("53cde5e7-cfda-403a-b281-08bc6ab21227"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 28,
                column: "PermissionControllerGuid",
                value: new Guid("7a3bfbfe-33c9-4354-9b8e-b301c8c46a42"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 29,
                column: "PermissionControllerGuid",
                value: new Guid("309bd166-bd93-4c37-b5e7-21bedeea6557"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 30,
                column: "PermissionControllerGuid",
                value: new Guid("ade100be-2f69-4b48-938d-af8ebadae2e2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 31,
                column: "PermissionControllerGuid",
                value: new Guid("a6604070-cfe4-45b5-bc2a-35770673492c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 32,
                column: "PermissionControllerGuid",
                value: new Guid("c5e5165f-8909-4bd4-890b-a298abdb3d4b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 33,
                column: "PermissionControllerGuid",
                value: new Guid("59636839-c0e4-4eec-b5ec-2ffba6d54a17"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 34,
                column: "PermissionControllerGuid",
                value: new Guid("f4c1892f-27b9-4799-980f-d13816b4100c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 35,
                column: "PermissionControllerGuid",
                value: new Guid("dce66f8d-c9a2-4fad-9a08-135ccc824fd6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 36,
                column: "PermissionControllerGuid",
                value: new Guid("108bb08a-9395-4e88-91e6-2cfdaa3b3a1a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 37,
                column: "PermissionControllerGuid",
                value: new Guid("f2d146e3-dd76-4b81-8d28-9e80ff7e44ef"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 38,
                column: "PermissionControllerGuid",
                value: new Guid("c25b9b1e-35bf-4598-bf03-5ed592b88deb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 39,
                column: "PermissionControllerGuid",
                value: new Guid("6fb1d3df-c2f5-4d6a-ab3b-24812c4e5004"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 40,
                column: "PermissionControllerGuid",
                value: new Guid("d3ed5f8c-d32e-422c-b9b5-602efade6cb3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 41,
                column: "PermissionControllerGuid",
                value: new Guid("502bd401-2efb-4e64-9793-f252039c348a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 42,
                column: "PermissionControllerGuid",
                value: new Guid("97e0160d-fc22-4c73-8674-df346b26a7ea"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 43,
                column: "PermissionControllerGuid",
                value: new Guid("ed89029e-17be-480a-91a3-28d6d3d649b0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 44,
                column: "PermissionControllerGuid",
                value: new Guid("796f6180-a1ef-4c02-b411-e5d0cdcb7d02"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 45,
                column: "PermissionControllerGuid",
                value: new Guid("d07c5ca5-33db-430a-85d5-dab29e7f876e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 46,
                column: "PermissionControllerGuid",
                value: new Guid("522c3220-01d8-4e24-883b-081317b4a125"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionController",
                keyColumn: "PermissionControllerID",
                keyValue: 47,
                column: "PermissionControllerGuid",
                value: new Guid("18c50a7d-da18-4aa4-9d21-edfb65f5a1de"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 1,
                column: "PermissionControllerActionGuid",
                value: new Guid("682d72c4-f26c-4a15-932b-f2873cab27e0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 2,
                column: "PermissionControllerActionGuid",
                value: new Guid("cfc767b5-198e-47c6-979e-7b0d26adfb01"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 3,
                column: "PermissionControllerActionGuid",
                value: new Guid("6dfcdf55-393d-4681-9a3c-2db51656118e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 4,
                column: "PermissionControllerActionGuid",
                value: new Guid("79835f54-a1be-4825-b1dc-5151a9311a1b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 5,
                column: "PermissionControllerActionGuid",
                value: new Guid("266be3d6-ce55-423a-a995-675bcf4dd47f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 6,
                column: "PermissionControllerActionGuid",
                value: new Guid("732997e6-7dcc-4586-a466-f375a2780486"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 7,
                column: "PermissionControllerActionGuid",
                value: new Guid("10fc7c63-d039-48a6-805e-661ea287df3d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 8,
                column: "PermissionControllerActionGuid",
                value: new Guid("f50cd82b-6e48-43f6-bb4b-c9b29d1452c9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 9,
                column: "PermissionControllerActionGuid",
                value: new Guid("cf0ee8a0-ef90-4ac1-b697-e65eb2707dda"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 10,
                column: "PermissionControllerActionGuid",
                value: new Guid("fd4d8852-65f8-4bca-b112-71c123b7e9ab"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 11,
                column: "PermissionControllerActionGuid",
                value: new Guid("497041a3-d223-41b5-be46-60670bf72ac5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 12,
                column: "PermissionControllerActionGuid",
                value: new Guid("a16824e0-fe6c-444f-8147-8d82b5465c73"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 13,
                column: "PermissionControllerActionGuid",
                value: new Guid("e1664ec0-f9c5-4ad3-aacd-b833290a4f39"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 14,
                column: "PermissionControllerActionGuid",
                value: new Guid("43b3ed03-2679-44b8-915b-f16a4d519003"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 15,
                column: "PermissionControllerActionGuid",
                value: new Guid("250cc8dd-a504-41cc-bf7a-56547c732ea5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 16,
                column: "PermissionControllerActionGuid",
                value: new Guid("2e941c5d-cc0f-4ced-acca-2b7a6b68cc00"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 17,
                column: "PermissionControllerActionGuid",
                value: new Guid("78f03e91-52a9-4be0-8d15-0b7de82eabe0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 18,
                column: "PermissionControllerActionGuid",
                value: new Guid("3384773c-ab36-472f-930f-744b1780d71e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 19,
                column: "PermissionControllerActionGuid",
                value: new Guid("7a572ad1-cf88-410e-9b1b-5669373586a4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 20,
                column: "PermissionControllerActionGuid",
                value: new Guid("db777e7e-3972-44e7-b3c4-62d6b3260b89"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 21,
                column: "PermissionControllerActionGuid",
                value: new Guid("7c4b326d-9ce2-4022-aae2-c43133106e6b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 22,
                column: "PermissionControllerActionGuid",
                value: new Guid("c77b8b43-31de-45b8-9a8e-5a721dd70ab5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 23,
                column: "PermissionControllerActionGuid",
                value: new Guid("84ddc04d-59bd-4e29-8348-ed94a4fe3077"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 24,
                column: "PermissionControllerActionGuid",
                value: new Guid("d3ad1486-9428-4360-a322-ef662b00bc7d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 25,
                column: "PermissionControllerActionGuid",
                value: new Guid("10aa33eb-9cc5-443b-9054-3c14f252018c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 26,
                column: "PermissionControllerActionGuid",
                value: new Guid("a36e36c9-b042-450f-a53b-2767c4a94d0e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 27,
                column: "PermissionControllerActionGuid",
                value: new Guid("62f9b6bf-3e53-417c-b9b0-559a70f311ba"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 28,
                column: "PermissionControllerActionGuid",
                value: new Guid("58d1496d-99ff-4396-b0c4-ac873ce6d9fb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 29,
                column: "PermissionControllerActionGuid",
                value: new Guid("1f61ad6a-2503-4560-9702-1aef91191f21"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 30,
                column: "PermissionControllerActionGuid",
                value: new Guid("ebb7ef13-09ac-4eeb-9bda-47019593f84c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 31,
                column: "PermissionControllerActionGuid",
                value: new Guid("fb0ca55e-1ed6-4c0f-afcf-a44461afe5a6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 32,
                column: "PermissionControllerActionGuid",
                value: new Guid("64d9c1a8-6719-4c5b-8262-332be56bc53c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 33,
                column: "PermissionControllerActionGuid",
                value: new Guid("2e89df82-3893-4d12-892c-014bca19fcf0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 34,
                column: "PermissionControllerActionGuid",
                value: new Guid("4b73590b-7f47-420c-86b2-e3285ba11c47"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 35,
                column: "PermissionControllerActionGuid",
                value: new Guid("1c22daa6-d113-4b75-8831-2fb8a8a264a7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 36,
                column: "PermissionControllerActionGuid",
                value: new Guid("d9df5a53-a39d-49f0-b618-3946f4c90738"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 37,
                column: "PermissionControllerActionGuid",
                value: new Guid("0532d7eb-358e-4b45-9533-efd0ebeb801d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 38,
                column: "PermissionControllerActionGuid",
                value: new Guid("f85c2bd3-2ce2-49fb-9593-946ebf1f4daf"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 39,
                column: "PermissionControllerActionGuid",
                value: new Guid("91ee3ac5-c40d-4ba3-96e8-65678557cb8e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 40,
                column: "PermissionControllerActionGuid",
                value: new Guid("56de087e-2277-4cac-9705-acd47e90c920"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 41,
                column: "PermissionControllerActionGuid",
                value: new Guid("f23408cb-9902-4c95-b9cb-e5609dbc2294"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 42,
                column: "PermissionControllerActionGuid",
                value: new Guid("a0645d6f-b521-4945-b610-ed75821252c6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 43,
                column: "PermissionControllerActionGuid",
                value: new Guid("2229241f-0e7c-4e72-910b-16b6ae4e0f56"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 44,
                column: "PermissionControllerActionGuid",
                value: new Guid("edd1acd9-1958-4ec9-ab04-d9022151e84f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 45,
                column: "PermissionControllerActionGuid",
                value: new Guid("88c17591-814c-4cfc-a90d-b626ac187359"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 46,
                column: "PermissionControllerActionGuid",
                value: new Guid("257b36d4-8633-4d99-a95c-00fe4e677718"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 47,
                column: "PermissionControllerActionGuid",
                value: new Guid("e7cf8ecc-344c-4e7e-8d07-c0c15ddbbbf5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 48,
                column: "PermissionControllerActionGuid",
                value: new Guid("a9dd8e9f-64f8-4077-8c25-15c5fab0dfae"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 49,
                column: "PermissionControllerActionGuid",
                value: new Guid("ea4ffea8-0d49-4d23-84a1-b994574225e3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 50,
                column: "PermissionControllerActionGuid",
                value: new Guid("fa4e3ac0-086e-404e-ba8c-e08239d8adb7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 51,
                column: "PermissionControllerActionGuid",
                value: new Guid("5c711075-a660-41b2-8f1b-3253e84c2cbe"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 52,
                column: "PermissionControllerActionGuid",
                value: new Guid("b01f78fa-2928-4f78-8352-b3bf6d996c5a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 53,
                column: "PermissionControllerActionGuid",
                value: new Guid("056d5a34-f608-4b2a-a691-04554978a020"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 54,
                column: "PermissionControllerActionGuid",
                value: new Guid("0ff53656-47f7-4e73-a4e3-41b67bc11698"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 55,
                column: "PermissionControllerActionGuid",
                value: new Guid("dcebfd76-37f9-46f5-a3dd-a2022dfda34f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 56,
                column: "PermissionControllerActionGuid",
                value: new Guid("eb1a110f-fc16-4db5-973b-28772d0f7c3b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 57,
                column: "PermissionControllerActionGuid",
                value: new Guid("4c705df3-6c4f-4ee9-888c-afc71ec46c4c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 58,
                column: "PermissionControllerActionGuid",
                value: new Guid("d2e34392-9dcc-44f4-a852-33a2eafd91cb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 59,
                column: "PermissionControllerActionGuid",
                value: new Guid("3ddafb85-42b1-45c1-bc08-cc2c4302b2b6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 60,
                column: "PermissionControllerActionGuid",
                value: new Guid("45afea34-0d86-4bf3-b77e-9563c39f31e0"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 61,
                column: "PermissionControllerActionGuid",
                value: new Guid("3f6b7bde-2eca-483e-9bbe-5fd18505705c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 62,
                column: "PermissionControllerActionGuid",
                value: new Guid("8acf4cf3-1e5d-4234-b601-0b6a78eebc19"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 63,
                column: "PermissionControllerActionGuid",
                value: new Guid("309c8674-958c-4829-a48a-13b38e64dbde"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 64,
                column: "PermissionControllerActionGuid",
                value: new Guid("1548ec2f-e7f2-480f-9386-3c7ac89f8a8a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 65,
                column: "PermissionControllerActionGuid",
                value: new Guid("eb33d9ca-2c8a-431d-b4a1-d3f5203b5eeb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 66,
                column: "PermissionControllerActionGuid",
                value: new Guid("5518da15-0205-4606-9e35-2ddc29528af3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 67,
                column: "PermissionControllerActionGuid",
                value: new Guid("e07a9719-aab3-4ab0-b01d-a0533bc1537a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 68,
                column: "PermissionControllerActionGuid",
                value: new Guid("934c138d-a41a-45fc-b0d4-e42d7585191e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 69,
                column: "PermissionControllerActionGuid",
                value: new Guid("bb7d25c4-5299-4de8-87ee-7899d2aca14d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 70,
                column: "PermissionControllerActionGuid",
                value: new Guid("336fdf74-58e0-4872-984a-3cf758336ec5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 71,
                column: "PermissionControllerActionGuid",
                value: new Guid("5456860d-bd07-480a-9e5f-5439de034c5d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 72,
                column: "PermissionControllerActionGuid",
                value: new Guid("96aa7c5a-837f-4925-87e8-1bc1add891ff"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 73,
                column: "PermissionControllerActionGuid",
                value: new Guid("3df70362-d22f-4cf4-9160-fde3e5d16aab"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 74,
                column: "PermissionControllerActionGuid",
                value: new Guid("e4ceb7ca-348b-4725-86c3-cb9a8d4957ae"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 75,
                column: "PermissionControllerActionGuid",
                value: new Guid("527f060b-eb22-4500-8fb3-fd870963847e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 76,
                column: "PermissionControllerActionGuid",
                value: new Guid("184e0e7a-6d53-4630-80fa-697d5450207a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 77,
                column: "PermissionControllerActionGuid",
                value: new Guid("6a924242-37d9-43d6-a769-795ffa96d4fa"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 78,
                column: "PermissionControllerActionGuid",
                value: new Guid("f6205e90-030e-43f7-8d7e-249fccbd0303"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 79,
                column: "PermissionControllerActionGuid",
                value: new Guid("41687f84-4532-43cb-a72e-3027a4c2e04c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 80,
                column: "PermissionControllerActionGuid",
                value: new Guid("4f488c60-46f7-4949-bfb3-03e7945438c3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 81,
                column: "PermissionControllerActionGuid",
                value: new Guid("8974470a-7082-489d-adfc-7666a9e202af"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 82,
                column: "PermissionControllerActionGuid",
                value: new Guid("4dbd7195-b082-448c-bf52-adfa605e6e5d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 83,
                column: "PermissionControllerActionGuid",
                value: new Guid("acab1eb2-93f0-4aab-b796-36080fd578a4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 84,
                column: "PermissionControllerActionGuid",
                value: new Guid("4e05b305-4ec6-4a7f-8735-42daa2d37ad2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 85,
                column: "PermissionControllerActionGuid",
                value: new Guid("a9614f3f-8d8e-41d7-8d80-36e2f858dd27"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 86,
                column: "PermissionControllerActionGuid",
                value: new Guid("940e47ac-68de-459f-8e04-0d45d00df62c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 87,
                column: "PermissionControllerActionGuid",
                value: new Guid("25bd411f-1533-494f-984d-17e9f3fc8cbc"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 88,
                column: "PermissionControllerActionGuid",
                value: new Guid("3a7653c5-b577-4723-83d2-4fc8396c5f3f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 89,
                column: "PermissionControllerActionGuid",
                value: new Guid("105e317d-baec-40bd-9712-349c68bd52b3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 90,
                column: "PermissionControllerActionGuid",
                value: new Guid("75994661-3d97-4e05-8f91-7ccfcb038f1e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 91,
                column: "PermissionControllerActionGuid",
                value: new Guid("8b8f618e-ee21-441b-8d64-5d9a69d59321"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 92,
                column: "PermissionControllerActionGuid",
                value: new Guid("3fa4cbf1-250c-45b6-a2db-321414ace8eb"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 93,
                column: "PermissionControllerActionGuid",
                value: new Guid("cf0b4942-27ae-40f8-98b7-8a23857c9b32"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 94,
                column: "PermissionControllerActionGuid",
                value: new Guid("e4da4619-443a-4814-ba6d-e5a29ea8fc4f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 95,
                column: "PermissionControllerActionGuid",
                value: new Guid("8de3d45d-c264-4373-aca8-869dc0483ee5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 96,
                column: "PermissionControllerActionGuid",
                value: new Guid("5a0cac44-8ee4-4917-9d41-a593b119f556"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 97,
                column: "PermissionControllerActionGuid",
                value: new Guid("8254467f-ae72-4430-b612-37094cfcffde"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 98,
                column: "PermissionControllerActionGuid",
                value: new Guid("f804c55c-02cc-4262-a622-cc9fdcf5dc7b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 99,
                column: "PermissionControllerActionGuid",
                value: new Guid("79d6f50e-7979-479a-9a98-3188432ef067"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 100,
                column: "PermissionControllerActionGuid",
                value: new Guid("88878017-55ff-44fa-af81-9a8e4bbb5098"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 101,
                column: "PermissionControllerActionGuid",
                value: new Guid("25b8c961-8106-436b-be33-028c5454ff07"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 102,
                column: "PermissionControllerActionGuid",
                value: new Guid("d7d26339-d76a-4524-b084-adc619569ae1"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 103,
                column: "PermissionControllerActionGuid",
                value: new Guid("65318b4d-b5af-4333-a732-64d10bf50971"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 104,
                column: "PermissionControllerActionGuid",
                value: new Guid("e4f69283-2648-40b4-8a4b-2935abfd11bd"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 105,
                column: "PermissionControllerActionGuid",
                value: new Guid("863ec8a1-2049-4de2-b921-19754eb06611"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 106,
                column: "PermissionControllerActionGuid",
                value: new Guid("6a348399-444a-40a5-9d5a-933756cc2afe"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 107,
                column: "PermissionControllerActionGuid",
                value: new Guid("0a496101-0e3f-441b-bd38-6cebc5e2eeb4"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 108,
                column: "PermissionControllerActionGuid",
                value: new Guid("9a4bdb18-d915-4bb9-9aa7-f920d12ef2e7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 109,
                column: "PermissionControllerActionGuid",
                value: new Guid("47a7b449-6427-426e-ac04-a42b29539621"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 110,
                column: "PermissionControllerActionGuid",
                value: new Guid("f9a1a8ab-f186-4c7a-87d8-6b2fe668df0a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 111,
                column: "PermissionControllerActionGuid",
                value: new Guid("9be166e2-906e-44a2-adcc-45c2f2f05fad"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 112,
                column: "PermissionControllerActionGuid",
                value: new Guid("ee763724-681c-4a16-9c0e-9fef50e3d8cd"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 113,
                column: "PermissionControllerActionGuid",
                value: new Guid("844766c2-6f13-4070-a3ca-dd9dc420c8e9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 114,
                column: "PermissionControllerActionGuid",
                value: new Guid("7af26dbd-cbb2-4aa9-a46b-2e93ead8b2be"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 115,
                column: "PermissionControllerActionGuid",
                value: new Guid("1a8eb95c-05df-48f8-95e0-cd6c69743685"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 116,
                column: "PermissionControllerActionGuid",
                value: new Guid("1b3f6429-1850-41fc-9e3a-b1205988b2f9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 117,
                column: "PermissionControllerActionGuid",
                value: new Guid("22e82577-a390-4c73-88d4-77253825d090"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 118,
                column: "PermissionControllerActionGuid",
                value: new Guid("2a5feacb-49b7-4e3f-9809-eb0deb0f94e6"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 119,
                column: "PermissionControllerActionGuid",
                value: new Guid("2586b6e7-bde7-4a8a-a2c1-96eebcab9fe8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 120,
                column: "PermissionControllerActionGuid",
                value: new Guid("3d8f5c77-6b4d-4434-aac1-3ceb529cd6bf"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 121,
                column: "PermissionControllerActionGuid",
                value: new Guid("3df54eaf-b67b-462f-9f54-f92208f50d03"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 122,
                column: "PermissionControllerActionGuid",
                value: new Guid("4539d771-2707-4d2a-8467-b6eebdad4c0b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 123,
                column: "PermissionControllerActionGuid",
                value: new Guid("4115784b-37c8-4835-8e98-d32cb193293a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 124,
                column: "PermissionControllerActionGuid",
                value: new Guid("a2d5dafc-83b1-4aff-b59c-5033805d6a9f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 125,
                column: "PermissionControllerActionGuid",
                value: new Guid("d30745fc-f11d-4298-9ede-ac65dc2a36d2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 126,
                column: "PermissionControllerActionGuid",
                value: new Guid("27937f89-0ef2-4afd-b817-faa91ec26f86"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 127,
                column: "PermissionControllerActionGuid",
                value: new Guid("c9c785bf-b07e-45f1-b2a5-cc3cf47b840b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 128,
                column: "PermissionControllerActionGuid",
                value: new Guid("8be68bdb-8482-472b-9344-9acd1fd73710"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 129,
                column: "PermissionControllerActionGuid",
                value: new Guid("d17848a4-3fac-4d5b-aea8-873d6343dd0c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 130,
                column: "PermissionControllerActionGuid",
                value: new Guid("c1ed2e74-5ebd-4194-b71c-395b3f6693c8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 131,
                column: "PermissionControllerActionGuid",
                value: new Guid("09cc4847-112d-4794-9673-3715ecbe06c3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 132,
                column: "PermissionControllerActionGuid",
                value: new Guid("3cf1c8a6-f61b-4bb7-9b5e-3f848a79a310"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 133,
                column: "PermissionControllerActionGuid",
                value: new Guid("5ed69d5f-5b95-436d-95bb-7daa26d6d893"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 134,
                column: "PermissionControllerActionGuid",
                value: new Guid("ab2a3ab5-a14e-40c6-912a-122aee0650fd"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 135,
                column: "PermissionControllerActionGuid",
                value: new Guid("ba74a11e-98b8-4d20-9ccf-fb5ea7092cb9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 136,
                column: "PermissionControllerActionGuid",
                value: new Guid("139733a4-353f-4a7d-8958-e46c2062e19d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 137,
                column: "PermissionControllerActionGuid",
                value: new Guid("c7cbd9db-0bd7-4975-be5f-058956478de5"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 138,
                column: "PermissionControllerActionGuid",
                value: new Guid("376ce93a-6c50-4ea7-b057-45652088f126"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 139,
                column: "PermissionControllerActionGuid",
                value: new Guid("27c507f9-b3e2-4d59-ae15-4038ca5eec62"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 140,
                column: "PermissionControllerActionGuid",
                value: new Guid("0bdf7307-ac4b-4abd-8ff8-fb036d91717a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 141,
                column: "PermissionControllerActionGuid",
                value: new Guid("8fad2760-b0d3-4d3f-a104-14f5a4e8f08e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 142,
                column: "PermissionControllerActionGuid",
                value: new Guid("35b88479-74c9-458b-8a04-732d65289568"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 143,
                column: "PermissionControllerActionGuid",
                value: new Guid("cc20ea21-bc9e-442a-8c83-ba5067424377"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 144,
                column: "PermissionControllerActionGuid",
                value: new Guid("b0bd516c-4bb9-493c-a91e-a994ca26b238"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 145,
                column: "PermissionControllerActionGuid",
                value: new Guid("12ce9c65-3e2a-4668-9a1b-72ff41bb56f7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 146,
                column: "PermissionControllerActionGuid",
                value: new Guid("f227257e-6aad-465c-a868-e758979ecac3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 147,
                column: "PermissionControllerActionGuid",
                value: new Guid("6eaac50d-ac90-448a-9105-1f97e239d229"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 148,
                column: "PermissionControllerActionGuid",
                value: new Guid("d3fd41ce-887e-4ac3-902b-7bb06aa74e23"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 149,
                column: "PermissionControllerActionGuid",
                value: new Guid("88f7df4d-faa7-454f-96b1-08b3761dd685"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 150,
                column: "PermissionControllerActionGuid",
                value: new Guid("b4780d66-9182-48e0-adab-13781b8c467e"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 151,
                column: "PermissionControllerActionGuid",
                value: new Guid("f9d2b343-ee82-4308-80d6-7a5d37e4eae2"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 152,
                column: "PermissionControllerActionGuid",
                value: new Guid("89e0a122-c7ee-4dd1-a528-2836c5a46d26"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 153,
                column: "PermissionControllerActionGuid",
                value: new Guid("81b720c9-65e6-44b2-bc3f-1b20a91fe51d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 154,
                column: "PermissionControllerActionGuid",
                value: new Guid("caa2a0e1-a8ab-4a61-b7c6-21c781bed12d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 155,
                column: "PermissionControllerActionGuid",
                value: new Guid("aa8bf68a-bed7-449f-8d80-4f37a08dbb5f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 156,
                column: "PermissionControllerActionGuid",
                value: new Guid("3d394071-fd16-458d-911a-4f0400afb3e3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 157,
                column: "PermissionControllerActionGuid",
                value: new Guid("86c6df27-e6f1-41a0-ae64-e18dff857fbf"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 158,
                column: "PermissionControllerActionGuid",
                value: new Guid("fe48ac5a-35bb-408e-ab7d-9b5ec53d974b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 159,
                column: "PermissionControllerActionGuid",
                value: new Guid("9c7ada11-9fba-409d-940a-bc78230184ef"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 160,
                column: "PermissionControllerActionGuid",
                value: new Guid("abb0088f-b35b-46b5-b764-fcf316ec7234"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 161,
                column: "PermissionControllerActionGuid",
                value: new Guid("453727eb-ae74-48ad-b1e0-3f544ab73051"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 162,
                column: "PermissionControllerActionGuid",
                value: new Guid("649e68c6-160e-4ce9-8372-08ed57383af9"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 163,
                column: "PermissionControllerActionGuid",
                value: new Guid("4615a541-3654-48bc-964c-b24644794a2c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 164,
                column: "PermissionControllerActionGuid",
                value: new Guid("7abc1740-51f6-49f3-bbc1-b14aa406e0b3"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 165,
                column: "PermissionControllerActionGuid",
                value: new Guid("7e40fa11-e0ec-4fb1-9e9e-461d288f216d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 166,
                column: "PermissionControllerActionGuid",
                value: new Guid("70667a98-e7ea-4684-ad84-7f5c77238c07"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 167,
                column: "PermissionControllerActionGuid",
                value: new Guid("ed12f1ff-86d4-4e93-add0-089132ce4827"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 168,
                column: "PermissionControllerActionGuid",
                value: new Guid("296cc9ba-5f7b-474a-80a7-fcade04e3c79"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 169,
                column: "PermissionControllerActionGuid",
                value: new Guid("e513cc76-aa0e-4869-8da3-6fefddfa5efd"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 170,
                column: "PermissionControllerActionGuid",
                value: new Guid("be601bc6-c24f-498b-97d8-8e3c7f772b23"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 171,
                column: "PermissionControllerActionGuid",
                value: new Guid("0835b5d7-f321-443d-86b9-17fcf6064098"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 172,
                column: "PermissionControllerActionGuid",
                value: new Guid("3e849a2e-b0c4-4065-ae15-94b55b960d4d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 173,
                column: "PermissionControllerActionGuid",
                value: new Guid("88cc7159-37ed-4a64-9030-38521625373f"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 174,
                column: "PermissionControllerActionGuid",
                value: new Guid("569635db-fbe8-4400-81c0-2badcf147a8a"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 175,
                column: "PermissionControllerActionGuid",
                value: new Guid("3c616c21-5115-4926-ba50-2bc2411449e8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 176,
                column: "PermissionControllerActionGuid",
                value: new Guid("d45fd095-bcd9-4e68-a9a3-dd05747c6bf7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 177,
                column: "PermissionControllerActionGuid",
                value: new Guid("e5de6b39-00e4-4ae7-9534-cd4b8819fc92"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 178,
                column: "PermissionControllerActionGuid",
                value: new Guid("9e67c32c-0fad-4873-9586-51e1f12d428c"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 179,
                column: "PermissionControllerActionGuid",
                value: new Guid("dcbbc9e0-57df-4c5d-9c71-d1b32c47f490"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 180,
                column: "PermissionControllerActionGuid",
                value: new Guid("390b56c3-463f-4c0e-ae21-d50c0ab0f13d"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 181,
                column: "PermissionControllerActionGuid",
                value: new Guid("d764ed36-7e61-4dff-8707-34dc0c4424c8"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 182,
                column: "PermissionControllerActionGuid",
                value: new Guid("763e0d4b-3ba6-441d-8b25-54184373c720"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 183,
                column: "PermissionControllerActionGuid",
                value: new Guid("ee472c91-5d93-4cb2-a068-aed4c903d7f7"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 184,
                column: "PermissionControllerActionGuid",
                value: new Guid("be956357-c5a9-486a-8bff-af4bb160e98b"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 185,
                column: "PermissionControllerActionGuid",
                value: new Guid("50e49d30-63dc-4877-a9cb-c5c2c2a7f023"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 186,
                column: "PermissionControllerActionGuid",
                value: new Guid("0299f41c-6196-45d0-8a55-a7a1e4894ccc"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 187,
                column: "PermissionControllerActionGuid",
                value: new Guid("f3149a66-1c58-4e5d-b6f1-8adee7f8eadc"));

            migrationBuilder.UpdateData(
                schema: "Permission",
                table: "PermissionControllerAction",
                keyColumn: "PermissionControllerActionID",
                keyValue: 188,
                column: "PermissionControllerActionGuid",
                value: new Guid("a345ca68-3a37-4c5f-b545-a5f996386b2e"));

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "City",
                keyColumn: "CityID",
                keyValue: 1,
                columns: new[] { "CityGuid", "CreateDate", "EnableDate" },
                values: new object[] { new Guid("6ff52f2f-75c7-4924-870b-1bd5118d0d26"), new DateTime(2021, 10, 27, 22, 19, 10, 264, DateTimeKind.Local).AddTicks(9230), new DateTime(2021, 10, 27, 22, 19, 10, 264, DateTimeKind.Local).AddTicks(9781) });

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "Country",
                keyColumn: "CountryID",
                keyValue: 1,
                columns: new[] { "CountryGuid", "CreateDate", "EnableDate" },
                values: new object[] { new Guid("048a9917-6bc7-4427-994a-b6daec7f43f7"), new DateTime(2021, 10, 27, 22, 19, 10, 261, DateTimeKind.Local).AddTicks(9277), new DateTime(2021, 10, 27, 22, 19, 10, 263, DateTimeKind.Local).AddTicks(2334) });

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "Departments",
                keyColumn: "DepartmentsID",
                keyValue: 1,
                columns: new[] { "CreateDate", "DepartmentsGuid", "EnableDate" },
                values: new object[] { new DateTime(2021, 10, 27, 22, 19, 10, 268, DateTimeKind.Local).AddTicks(8794), new Guid("b51ece57-e7db-4944-804e-01748f564b87"), new DateTime(2021, 10, 27, 22, 19, 10, 268, DateTimeKind.Local).AddTicks(9357) });

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "Jobs",
                keyColumn: "JobsID",
                keyValue: 1,
                columns: new[] { "CreateDate", "EnableDate", "JobsGuid" },
                values: new object[] { new DateTime(2021, 10, 27, 22, 19, 10, 267, DateTimeKind.Local).AddTicks(8550), new DateTime(2021, 10, 27, 22, 19, 10, 267, DateTimeKind.Local).AddTicks(9123), new Guid("4ecd629f-534c-4ded-b0b2-f7d6fdcdde49") });

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "MainCategory",
                keyColumn: "MainCategoryID",
                keyValue: 1,
                columns: new[] { "CreateDate", "EnableDate", "MainCategoryGuid" },
                values: new object[] { new DateTime(2021, 10, 27, 22, 19, 10, 268, DateTimeKind.Local).AddTicks(3209), new DateTime(2021, 10, 27, 22, 19, 10, 268, DateTimeKind.Local).AddTicks(3801), new Guid("038559c7-6c7c-443c-a6b4-f25bd93f9999") });

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "Nationality",
                keyColumn: "NationalityID",
                keyValue: 1,
                columns: new[] { "CreateDate", "EnableDate", "NationalityGuid" },
                values: new object[] { new DateTime(2021, 10, 27, 22, 19, 10, 265, DateTimeKind.Local).AddTicks(6046), new DateTime(2021, 10, 27, 22, 19, 10, 265, DateTimeKind.Local).AddTicks(6606), new Guid("b47ae699-9834-45f3-aeaf-a385d2abeb83") });

            migrationBuilder.UpdateData(
                schema: "Setting",
                table: "Region",
                keyColumn: "RegionID",
                keyValue: 1,
                columns: new[] { "CreateDate", "EnableDate", "RegionGuid" },
                values: new object[] { new DateTime(2021, 10, 27, 22, 19, 10, 263, DateTimeKind.Local).AddTicks(9703), new DateTime(2021, 10, 27, 22, 19, 10, 264, DateTimeKind.Local).AddTicks(276), new Guid("af531221-42f5-46fd-b26e-be8b7659cc1c") });
        }
    }
}
