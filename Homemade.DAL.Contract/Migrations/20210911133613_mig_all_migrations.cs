using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Homemade.DAL.Contract.Migrations
{
    public partial class mig_all_migrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Setting");

            migrationBuilder.EnsureSchema(
                name: "Customer");

            migrationBuilder.EnsureSchema(
                name: "Emp");

            migrationBuilder.EnsureSchema(
                name: "Permission");

            migrationBuilder.EnsureSchema(
                name: "Vendor");

            migrationBuilder.CreateTable(
                name: "Inqueries",
                schema: "Setting",
                columns: table => new
                {
                    InqueriesID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InqueriesGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inqueries", x => x.InqueriesID);
                });

            migrationBuilder.CreateTable(
                name: "PermissionAction",
                schema: "Permission",
                columns: table => new
                {
                    PermissionActionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermissionActionGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermissionActionNameAr = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PermissionActionNameEn = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionAction", x => x.PermissionActionID);
                });

            migrationBuilder.CreateTable(
                name: "PermissionController",
                schema: "Permission",
                columns: table => new
                {
                    PermissionControllerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermissionControllerGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermissionControllerNameAr = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PermissionControllerNameEn = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionController", x => x.PermissionControllerID);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleTypeId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Tokens",
                schema: "Setting",
                columns: table => new
                {
                    TokenId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TokenVal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeviceType = table.Column<int>(type: "int", nullable: false),
                    UserType = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tokens", x => x.TokenId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserType = table.Column<int>(type: "int", nullable: false),
                    UserJWTToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "PermissionControllerAction",
                schema: "Permission",
                columns: table => new
                {
                    PermissionControllerActionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermissionControllerActionGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermissionControllerID = table.Column<int>(type: "int", nullable: false),
                    PermissionActionID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionControllerAction", x => x.PermissionControllerActionID);
                    table.ForeignKey(
                        name: "FK_PermissionControllerAction_PermissionAction_PermissionActionID",
                        column: x => x.PermissionActionID,
                        principalSchema: "Permission",
                        principalTable: "PermissionAction",
                        principalColumn: "PermissionActionID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PermissionControllerAction_PermissionController_PermissionControllerID",
                        column: x => x.PermissionControllerID,
                        principalSchema: "Permission",
                        principalTable: "PermissionController",
                        principalColumn: "PermissionControllerID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Activity",
                schema: "Setting",
                columns: table => new
                {
                    ActivityID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivityGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameAR = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: true),
                    NameEN = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: true),
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
                    table.PrimaryKey("PK_Activity", x => x.ActivityID);
                    table.ForeignKey(
                        name: "FK_Activity_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Banks",
                schema: "Setting",
                columns: table => new
                {
                    BanksID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameAR = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: true),
                    NameEN = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: true),
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
                    table.PrimaryKey("PK_Banks", x => x.BanksID);
                    table.ForeignKey(
                        name: "FK_Banks_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Configuration",
                schema: "Setting",
                columns: table => new
                {
                    ConfigurationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConfigurationGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CRNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanNameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanNameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WebSite = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StreetNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CRImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Banner = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_Configuration", x => x.ConfigurationID);
                    table.ForeignKey(
                        name: "FK_Configuration_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                schema: "Setting",
                columns: table => new
                {
                    CountryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameAR = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: true),
                    NameEN = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: true),
                    Flag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Place = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Long = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Zoom = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_Country", x => x.CountryID);
                    table.ForeignKey(
                        name: "FK_Country_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                schema: "Setting",
                columns: table => new
                {
                    JobsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobsGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameAR = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: true),
                    NameEN = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobTypeId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_Jobs", x => x.JobsID);
                    table.ForeignKey(
                        name: "FK_Jobs_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MainCategory",
                schema: "Setting",
                columns: table => new
                {
                    MainCategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MainCategoryGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameAR = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: true),
                    NameEN = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: true),
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
                    table.PrimaryKey("PK_MainCategory", x => x.MainCategoryID);
                    table.ForeignKey(
                        name: "FK_MainCategory_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MainPage",
                schema: "Setting",
                columns: table => new
                {
                    MainPageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MainPageGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MainPageTypeId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_MainPage", x => x.MainPageID);
                    table.ForeignKey(
                        name: "FK_MainPage_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Nationality",
                schema: "Setting",
                columns: table => new
                {
                    NationalityID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NationalityGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameAR = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: true),
                    NameEN = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: true),
                    DescriptionAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_Nationality", x => x.NationalityID);
                    table.ForeignKey(
                        name: "FK_Nationality_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderStatus",
                schema: "Setting",
                columns: table => new
                {
                    OrderStatusID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderStatusGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameAR = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: true),
                    NameEN = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: true),
                    DescAr = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: true),
                    DescEN = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: true),
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
                    table.PrimaryKey("PK_OrderStatus", x => x.OrderStatusID);
                    table.ForeignKey(
                        name: "FK_OrderStatus_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Package",
                schema: "Setting",
                columns: table => new
                {
                    PackageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackageGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameAR = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: true),
                    NameEN = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: true),
                    DescEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Percent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                    table.PrimaryKey("PK_Package", x => x.PackageID);
                    table.ForeignKey(
                        name: "FK_Package_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PaymentStatus",
                schema: "Setting",
                columns: table => new
                {
                    PaymentStatusID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentStatusGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameAR = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: true),
                    NameEN = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: true),
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
                    table.PrimaryKey("PK_PaymentStatus", x => x.PaymentStatusID);
                    table.ForeignKey(
                        name: "FK_PaymentStatus_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PaymentWay",
                schema: "Setting",
                columns: table => new
                {
                    PaymentWayID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentWayGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameAR = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: true),
                    NameEN = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: true),
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
                    table.PrimaryKey("PK_PaymentWay", x => x.PaymentWayID);
                    table.ForeignKey(
                        name: "FK_PaymentWay_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Slider",
                schema: "Setting",
                columns: table => new
                {
                    SliderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SliderGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameAR = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: true),
                    NameEN = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: true),
                    DescAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescEN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SliderTypeId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_Slider", x => x.SliderID);
                    table.ForeignKey(
                        name: "FK_Slider_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Permission",
                schema: "Permission",
                columns: table => new
                {
                    PermissionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermissionGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PermissionControllerActionID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.PermissionID);
                    table.ForeignKey(
                        name: "FK_Permission_PermissionControllerAction_PermissionControllerActionID",
                        column: x => x.PermissionControllerActionID,
                        principalSchema: "Permission",
                        principalTable: "PermissionControllerAction",
                        principalColumn: "PermissionControllerActionID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Permission_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Permission_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoleConfig",
                schema: "Permission",
                columns: table => new
                {
                    RoleConfigID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    PermissionControllerActionID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleConfig", x => x.RoleConfigID);
                    table.ForeignKey(
                        name: "FK_RoleConfig_PermissionControllerAction_PermissionControllerActionID",
                        column: x => x.PermissionControllerActionID,
                        principalSchema: "Permission",
                        principalTable: "PermissionControllerAction",
                        principalColumn: "PermissionControllerActionID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RoleConfig_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TempPermission",
                schema: "Permission",
                columns: table => new
                {
                    TempPermissionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TempPermissionGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    PermissionControllerActionID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempPermission", x => x.TempPermissionID);
                    table.ForeignKey(
                        name: "FK_TempPermission_PermissionControllerAction_PermissionControllerActionID",
                        column: x => x.PermissionControllerActionID,
                        principalSchema: "Permission",
                        principalTable: "PermissionControllerAction",
                        principalColumn: "PermissionControllerActionID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TempPermission_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PaymentConfiguration",
                schema: "Setting",
                columns: table => new
                {
                    PaymentConfigurationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentConfigurationGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IBANnumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attachment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BanksID = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_PaymentConfiguration", x => x.PaymentConfigurationID);
                    table.ForeignKey(
                        name: "FK_PaymentConfiguration_Banks_BanksID",
                        column: x => x.BanksID,
                        principalSchema: "Setting",
                        principalTable: "Banks",
                        principalColumn: "BanksID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentConfiguration_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Region",
                schema: "Setting",
                columns: table => new
                {
                    RegionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegionGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CountryID = table.Column<int>(type: "int", nullable: false),
                    NameAR = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: true),
                    NameEN = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: true),
                    Place = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Long = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Zoom = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_Region", x => x.RegionID);
                    table.ForeignKey(
                        name: "FK_Region_Country_CountryID",
                        column: x => x.CountryID,
                        principalSchema: "Setting",
                        principalTable: "Country",
                        principalColumn: "CountryID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Region_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                schema: "Setting",
                columns: table => new
                {
                    DepartmentsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentsGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameAR = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: true),
                    NameEN = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: true),
                    DescriptionAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Isunique = table.Column<bool>(type: "bit", nullable: false),
                    MainCategoryID = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_Departments", x => x.DepartmentsID);
                    table.ForeignKey(
                        name: "FK_Departments_MainCategory_MainCategoryID",
                        column: x => x.MainCategoryID,
                        principalSchema: "Setting",
                        principalTable: "MainCategory",
                        principalColumn: "MainCategoryID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Departments_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MainPageDetails",
                schema: "Setting",
                columns: table => new
                {
                    MainPageDetailsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MainPageDetailsGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TitleAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TitleEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VedioLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MainPageID = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_MainPageDetails", x => x.MainPageDetailsID);
                    table.ForeignKey(
                        name: "FK_MainPageDetails_MainPage_MainPageID",
                        column: x => x.MainPageID,
                        principalSchema: "Setting",
                        principalTable: "MainPage",
                        principalColumn: "MainPageID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MainPageDetails_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "City",
                schema: "Setting",
                columns: table => new
                {
                    CityID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegionID = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameAR = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: true),
                    NameEN = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: true),
                    Place = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Long = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Zoom = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_City", x => x.CityID);
                    table.ForeignKey(
                        name: "FK_City_Region_RegionID",
                        column: x => x.RegionID,
                        principalSchema: "Setting",
                        principalTable: "Region",
                        principalColumn: "RegionID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_City_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MainPageImages",
                schema: "Setting",
                columns: table => new
                {
                    MainPageImagesID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MainPageImagesGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VedioUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MainPageDetailsID = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_MainPageImages", x => x.MainPageImagesID);
                    table.ForeignKey(
                        name: "FK_MainPageImages_MainPageDetails_MainPageDetailsID",
                        column: x => x.MainPageDetailsID,
                        principalSchema: "Setting",
                        principalTable: "MainPageDetails",
                        principalColumn: "MainPageDetailsID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MainPageImages_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Block",
                schema: "Setting",
                columns: table => new
                {
                    BlockID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlockGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameAR = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: true),
                    NameEN = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: true),
                    CityID = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_Block", x => x.BlockID);
                    table.ForeignKey(
                        name: "FK_Block_City_CityID",
                        column: x => x.CityID,
                        principalSchema: "Setting",
                        principalTable: "City",
                        principalColumn: "CityID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Block_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                schema: "Customer",
                columns: table => new
                {
                    CustomersID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomersGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SeconedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfilePic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    CityID = table.Column<int>(type: "int", nullable: false),
                    NationalityID = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_Customers", x => x.CustomersID);
                    table.ForeignKey(
                        name: "FK_Customers_City_CityID",
                        column: x => x.CityID,
                        principalSchema: "Setting",
                        principalTable: "City",
                        principalColumn: "CityID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customers_Nationality_NationalityID",
                        column: x => x.NationalityID,
                        principalSchema: "Setting",
                        principalTable: "Nationality",
                        principalColumn: "NationalityID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customers_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                schema: "Emp",
                columns: table => new
                {
                    EntityEmpID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityEmpGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobsID = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsEnable = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EnableDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UserUpdate = table.Column<int>(type: "int", nullable: true),
                    UserDelete = table.Column<int>(type: "int", nullable: true),
                    UserEnable = table.Column<int>(type: "int", nullable: true),
                    FirstNameAR = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: true),
                    MidNameAR = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: true),
                    LastNameAR = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: true),
                    FirstNameEN = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: true),
                    MidNameEN = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: true),
                    LastNameEN = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: true),
                    FileNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    IDNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDateHijri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDateMilady = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BloodTypeId = table.Column<int>(type: "int", nullable: true),
                    MobileNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NationalityID = table.Column<int>(type: "int", nullable: false),
                    CityID = table.Column<int>(type: "int", nullable: false),
                    AddressAR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressEN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lng = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Zoom = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EntityEmpID);
                    table.ForeignKey(
                        name: "FK_Employees_City_CityID",
                        column: x => x.CityID,
                        principalSchema: "Setting",
                        principalTable: "City",
                        principalColumn: "CityID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Jobs_JobsID",
                        column: x => x.JobsID,
                        principalSchema: "Setting",
                        principalTable: "Jobs",
                        principalColumn: "JobsID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Nationality_NationalityID",
                        column: x => x.NationalityID,
                        principalSchema: "Setting",
                        principalTable: "Nationality",
                        principalColumn: "NationalityID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShippingCompany",
                schema: "Setting",
                columns: table => new
                {
                    ShippingCompanyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShippingCompanyGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityID = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_ShippingCompany", x => x.ShippingCompanyID);
                    table.ForeignKey(
                        name: "FK_ShippingCompany_City_CityID",
                        column: x => x.CityID,
                        principalSchema: "Setting",
                        principalTable: "City",
                        principalColumn: "CityID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShippingCompany_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vendors",
                schema: "Vendor",
                columns: table => new
                {
                    VendorsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VendorsGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SeconedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfilePic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    RegisterType = table.Column<int>(type: "int", nullable: false),
                    StoreNameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StoreNameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AboutStoreEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AboutStoreAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Banner = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CRnumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CRPic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IBANNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SwiftCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Lng = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CityID = table.Column<int>(type: "int", nullable: false),
                    ActivityID = table.Column<int>(type: "int", nullable: true),
                    BanksID = table.Column<int>(type: "int", nullable: true),
                    PackageID = table.Column<int>(type: "int", nullable: true),
                    BlockID = table.Column<int>(type: "int", nullable: true),
                    NationalityID = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_Vendors", x => x.VendorsID);
                    table.ForeignKey(
                        name: "FK_Vendors_Activity_ActivityID",
                        column: x => x.ActivityID,
                        principalSchema: "Setting",
                        principalTable: "Activity",
                        principalColumn: "ActivityID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vendors_Banks_BanksID",
                        column: x => x.BanksID,
                        principalSchema: "Setting",
                        principalTable: "Banks",
                        principalColumn: "BanksID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vendors_Block_BlockID",
                        column: x => x.BlockID,
                        principalSchema: "Setting",
                        principalTable: "Block",
                        principalColumn: "BlockID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vendors_City_CityID",
                        column: x => x.CityID,
                        principalSchema: "Setting",
                        principalTable: "City",
                        principalColumn: "CityID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vendors_Nationality_NationalityID",
                        column: x => x.NationalityID,
                        principalSchema: "Setting",
                        principalTable: "Nationality",
                        principalColumn: "NationalityID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vendors_Package_PackageID",
                        column: x => x.PackageID,
                        principalSchema: "Setting",
                        principalTable: "Package",
                        principalColumn: "PackageID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vendors_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerLocation",
                schema: "Customer",
                columns: table => new
                {
                    CustomerLocationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerLocationGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BuildingNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StreetNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UniqueSign = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Lng = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CustomersID = table.Column<int>(type: "int", nullable: false),
                    BlockID = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_CustomerLocation", x => x.CustomerLocationID);
                    table.ForeignKey(
                        name: "FK_CustomerLocation_Block_BlockID",
                        column: x => x.BlockID,
                        principalSchema: "Setting",
                        principalTable: "Block",
                        principalColumn: "BlockID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerLocation_Customers_CustomersID",
                        column: x => x.CustomersID,
                        principalSchema: "Customer",
                        principalTable: "Customers",
                        principalColumn: "CustomersID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerLocation_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                schema: "Vendor",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SKU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StartDiscountDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDiscountDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsFixed = table.Column<bool>(type: "bit", nullable: false),
                    IsHidden = table.Column<bool>(type: "bit", nullable: false),
                    VendorsID = table.Column<int>(type: "int", nullable: false),
                    DepartmentsID = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_Product", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_Product_Departments_DepartmentsID",
                        column: x => x.DepartmentsID,
                        principalSchema: "Setting",
                        principalTable: "Departments",
                        principalColumn: "DepartmentsID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Product_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Product_Vendors_VendorsID",
                        column: x => x.VendorsID,
                        principalSchema: "Vendor",
                        principalTable: "Vendors",
                        principalColumn: "VendorsID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductImage",
                schema: "Vendor",
                columns: table => new
                {
                    ProductImageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductImageGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_ProductImage", x => x.ProductImageID);
                    table.ForeignKey(
                        name: "FK_ProductImage_Product_ProductID",
                        column: x => x.ProductID,
                        principalSchema: "Vendor",
                        principalTable: "Product",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductImage_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductQuestion",
                schema: "Vendor",
                columns: table => new
                {
                    ProductQuestionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductQuestionGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnswerAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnswerEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_ProductQuestion", x => x.ProductQuestionID);
                    table.ForeignKey(
                        name: "FK_ProductQuestion_Product_ProductID",
                        column: x => x.ProductID,
                        principalSchema: "Vendor",
                        principalTable: "Product",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductQuestion_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "RoleId", "ConcurrencyStamp", "Name", "NormalizedName", "RoleTypeId" },
                values: new object[,]
                {
                    { 1, "d0f32596-9897-460a-bc28-4d7caa6c5fbf", "Admin", null, 1 },
                    { 2, "7b834730-5eab-40c8-821c-4abc11f0f5fd", "Operation", null, 1 },
                    { 3, "9c070b19-c1f3-42bd-a525-fd53faaafda2", "Medical Managment", null, 2 },
                    { 4, "08eb73fe-9da4-49b1-9126-d09cf64dcf69", "Doctors", null, 2 },
                    { 5, "efb47804-dbce-4c65-a3d3-712dd0f476dc", "Nurses", null, 2 },
                    { 6, "1c044e7a-7c5d-4b59-bbe6-10c64bedb71b", "Employees", null, 2 }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserJWTToken", "UserName", "UserType" },
                values: new object[] { 1, 0, "27ac0208-27d0-4646-8d67-2d1c1344ba45", "SystemUser@Admin.com", true, false, null, "SystemUser@Admin.com", "SYSTEMUSER", "AQAAAAEAACcQAAAAEP65QXLX6e94ehLc9ntv07Q7n/aO6wf8y6j/z15XE7hfgyZLCNvHmM3Ar6SaTwzC3g==", "012", true, "b4919e9b-af6e-41e7-a422-deb1aa35b9a6", false, null, "SystemUser", 1 });

            migrationBuilder.InsertData(
                schema: "Permission",
                table: "PermissionAction",
                columns: new[] { "PermissionActionID", "PermissionActionGuid", "PermissionActionNameAr", "PermissionActionNameEn" },
                values: new object[,]
                {
                    { 2, new Guid("d0006262-18b8-434d-96ad-3a178fe8f6bb"), "اضافة", "Insert" },
                    { 3, new Guid("fffe5e80-72de-495c-b5ed-e101833707cb"), "تعديل", "Update" },
                    { 4, new Guid("f804522d-c152-462a-8548-9424f27dfa9d"), "حذف", "Delete" },
                    { 1, new Guid("a4ac3eaf-dda3-4aca-b797-6dce0cd86c07"), "عرض", "View" }
                });

            migrationBuilder.InsertData(
                schema: "Permission",
                table: "PermissionController",
                columns: new[] { "PermissionControllerID", "PermissionControllerGuid", "PermissionControllerNameAr", "PermissionControllerNameEn" },
                values: new object[,]
                {
                    { 23, new Guid("e065b80b-c75f-4153-bfe0-bb05e6bad31b"), "الاستفسارات", "Inqueries" },
                    { 22, new Guid("f7aab284-87f5-4d70-b777-1f4154f9095d"), "شركات الشحن", "Shipping Company" },
                    { 21, new Guid("5f514795-e504-42f1-a373-4cb6efb44874"), "البنرات", "Sliders" },
                    { 20, new Guid("9236b7f6-2057-4e51-8459-fb0cb950b55d"), "التصنيفات الرئسيه", "Main categories" },
                    { 19, new Guid("b85781ab-636b-41c5-8444-5e585b5e4b13"), "اعدادت الدفع", "Payment Configuration" },
                    { 18, new Guid("59ebb552-8e85-4212-ad56-b2c0241b3574"), "اعدادت الشركه", " Company Configuration" },
                    { 17, new Guid("26e27655-b90f-4292-a851-b45dea843d73"), "حالات الدفع", "PaymentStatus" },
                    { 16, new Guid("7c4078eb-a2ec-4a83-b148-af058c9359c0"), "باقات التجار", "Package" },
                    { 15, new Guid("f1eb66a3-2805-4863-aee2-304c6f9c53cf"), "الأحياء", "Block" },
                    { 14, new Guid("006c5925-7699-4104-97b7-220c64149e49"), "الأنشطة", "Activity" },
                    { 13, new Guid("93426f35-3dc2-4b03-8595-aa51e02d6332"), "طرق الدفع", "PaymentWay" },
                    { 11, new Guid("21375fa9-2738-4ae9-a590-1595eb8b99f6"), "الاعدادات", "Configuration" },
                    { 10, new Guid("6a72f330-159c-4c85-ad5a-af316fb7e636"), "اعدادات الدور", "Role Configuration" },
                    { 9, new Guid("d878a602-ee1a-468a-a58c-57976d30217c"), "الجنسية", "Nationality" },
                    { 24, new Guid("bd2eae85-18e1-4c79-b2b7-3dd344872bf2"), "الصفحات الرئيسية", "Main Pages" },
                    { 7, new Guid("060c2811-f658-400c-a491-314e58ec2180"), "الاقسام", "Departments" },
                    { 6, new Guid("22bf54be-9435-4e7d-982f-84c8ffe02528"), "الاعدادات العامة", "General Setting" },
                    { 5, new Guid("98fa62e1-3651-41ca-b572-fb8d4ac4ee57"), "المدينة", "City" },
                    { 4, new Guid("9be19926-3b21-4064-82a1-8f60fa65ce97"), "المنطقة", "Region" },
                    { 3, new Guid("a2a34654-d9dd-4d50-a4f9-4070fd042b96"), "الدولة", "Country" },
                    { 2, new Guid("2445a3da-6cea-4331-a3e8-dc6060150844"), "الدور", "Role" },
                    { 1, new Guid("eda866d4-86a5-416f-8de6-2362e60e34d6"), "الصلاحيات", "Permission" },
                    { 12, new Guid("0cd23bcd-ac89-4e27-b455-69f85825b390"), "البنوك", "Bank" },
                    { 8, new Guid("f5ef462d-e986-431d-8aed-b3805b5f168b"), "الموظفين", "Employees" }
                });

            migrationBuilder.InsertData(
                schema: "Permission",
                table: "PermissionControllerAction",
                columns: new[] { "PermissionControllerActionID", "PermissionActionID", "PermissionControllerActionGuid", "PermissionControllerID" },
                values: new object[,]
                {
                    { 1, 1, new Guid("9bc7cb03-d3fc-4f9d-9e26-dc582f88ea0b"), 1 },
                    { 71, 3, new Guid("fa3e3266-c6b7-4931-bdb3-70edf6beddc2"), 18 },
                    { 70, 2, new Guid("0ef3dbeb-6d86-4f66-8385-3297e27b72d9"), 18 },
                    { 69, 1, new Guid("1621f347-27b9-48aa-9c7b-fcb0e27a1f5c"), 18 },
                    { 68, 4, new Guid("bbadfc1f-690d-4979-9a46-ed7d108851a5"), 17 },
                    { 67, 3, new Guid("ee9709ba-6fef-4d4e-a791-8d81c82e892c"), 17 },
                    { 66, 2, new Guid("33704416-c9c3-4567-9857-2b5830d43ed7"), 17 },
                    { 65, 1, new Guid("db11311d-7a00-4e42-ac9e-86a836afbefd"), 17 },
                    { 64, 4, new Guid("4b4529f0-30f0-4414-9ac8-8d5081630eba"), 16 },
                    { 63, 3, new Guid("d4a40ea4-c29f-4a43-9462-0d51fd1f3dca"), 16 },
                    { 72, 4, new Guid("47d49489-dd6b-49d2-996b-dd6aec4df62b"), 18 },
                    { 62, 2, new Guid("9acbf4bf-17ea-47c7-b180-4b08ee2d0eb6"), 16 },
                    { 60, 4, new Guid("cdfa3bab-0e10-44f3-8811-67122ccfe13f"), 15 },
                    { 59, 3, new Guid("09e464eb-6054-4c55-94a1-f82d6ce75322"), 15 },
                    { 58, 2, new Guid("89f536f3-4a24-469c-876a-2a2355776dbb"), 15 },
                    { 57, 1, new Guid("9d48cc8b-fa50-47a4-87ae-b6123b30d9ca"), 15 },
                    { 56, 4, new Guid("e6534f03-292f-4cb7-bfbd-b8de31e12c38"), 14 },
                    { 55, 3, new Guid("333650d4-2cfd-4179-95b1-8902f5c12ab5"), 14 },
                    { 54, 2, new Guid("3c5ff27a-d3bf-41a5-8c5c-99fb7d858067"), 14 },
                    { 53, 1, new Guid("defa8e5c-5165-4ad5-879d-4e90b5e14f5c"), 14 },
                    { 52, 4, new Guid("742ad9cd-7d4e-495f-8f5d-7860844f73b5"), 13 },
                    { 61, 1, new Guid("488f390e-9e22-4d76-9bbe-e863285f9ff2"), 16 },
                    { 73, 1, new Guid("26d22a7f-c564-41a3-96d9-1f35495b98ec"), 19 },
                    { 74, 2, new Guid("6a620453-4c9c-41b1-9d7e-4e584837e96e"), 19 },
                    { 75, 3, new Guid("a8a7e9ce-da06-42e5-aac4-d28661b8bb17"), 19 },
                    { 96, 4, new Guid("7018f28f-1b27-40ee-ac24-d3fd911ee238"), 24 },
                    { 95, 3, new Guid("b8533d0c-5294-43b2-8c1e-fba6ad5f19bf"), 24 },
                    { 94, 2, new Guid("cf02cb42-579b-4be3-b355-a1fd401c4ceb"), 24 },
                    { 93, 1, new Guid("80627ec6-f622-4c8d-a8f2-abf13cc33dc9"), 24 },
                    { 92, 4, new Guid("caedc92a-9225-452b-ba0f-81c63a83a7c8"), 23 },
                    { 91, 3, new Guid("791144b3-16ad-4341-a13f-bfb462a6ad33"), 23 },
                    { 90, 2, new Guid("ef336d61-19ee-46a5-8c3e-68a4a19966b1"), 23 },
                    { 89, 1, new Guid("54cbb35e-709a-4f17-a61a-bb03d4fdb310"), 23 },
                    { 88, 4, new Guid("e95198e0-d5af-4216-90fd-0bfaa13e5e10"), 22 },
                    { 87, 3, new Guid("71f696a6-511d-4cdf-b367-cf4f4a981b30"), 22 },
                    { 86, 2, new Guid("dad60de8-64bb-4a13-9f7d-703197bee741"), 22 },
                    { 85, 1, new Guid("5de75c40-6f7b-42c1-a113-3d5437888254"), 22 },
                    { 84, 4, new Guid("1eb49978-3110-493d-abeb-36b5c787338e"), 21 },
                    { 83, 3, new Guid("8cb666bc-df99-4b90-9828-70926255ba4e"), 21 },
                    { 82, 2, new Guid("d2589b7b-ad99-4394-ad26-16c395bd843c"), 21 },
                    { 81, 1, new Guid("a93cd42a-ae42-4f98-ab3e-cb06b2aea19d"), 21 },
                    { 80, 4, new Guid("aa368c3d-67fc-480d-8125-64e199c77473"), 20 }
                });

            migrationBuilder.InsertData(
                schema: "Permission",
                table: "PermissionControllerAction",
                columns: new[] { "PermissionControllerActionID", "PermissionActionID", "PermissionControllerActionGuid", "PermissionControllerID" },
                values: new object[,]
                {
                    { 79, 3, new Guid("9a143aa2-33e8-47f6-923d-00f73eca3422"), 20 },
                    { 78, 2, new Guid("5427dac3-9b8c-45ae-bce3-e0f891663513"), 20 },
                    { 77, 1, new Guid("3f11c51e-3c91-4b28-92c6-2114f59ec862"), 20 },
                    { 76, 4, new Guid("57c1fb87-7d20-4592-bf6c-71854aca3262"), 19 },
                    { 51, 3, new Guid("e2be6fad-42c9-41f2-87a1-4074cc759785"), 13 },
                    { 49, 1, new Guid("423a706e-ca6d-44d5-a8cc-a7cbe0e71263"), 13 },
                    { 50, 2, new Guid("a73db4a8-66c0-4e86-a179-fe51b10822e1"), 13 },
                    { 23, 3, new Guid("c61ab712-8761-41c3-8278-a03c1ad3a0e5"), 6 },
                    { 21, 1, new Guid("3d9b7e28-951c-4bcb-a65f-3decd33e1b67"), 6 },
                    { 20, 4, new Guid("34717c8d-afc6-4864-9423-ffc2120d2620"), 5 },
                    { 19, 3, new Guid("2b35d300-9ecf-4993-9e30-b235417df277"), 5 },
                    { 18, 2, new Guid("38b73bf8-9e9b-47fe-bbc0-c0954ef18dac"), 5 },
                    { 17, 1, new Guid("338a3b0b-a625-4728-9738-f566dcba723d"), 5 },
                    { 16, 4, new Guid("e2b1c32d-84ff-434a-aa53-7d1a5d2235e1"), 4 },
                    { 15, 3, new Guid("4e176cff-031d-4d1f-9b41-b4eac1bfe916"), 4 },
                    { 14, 2, new Guid("86515398-1e9b-4056-bc14-a376b89c40cb"), 4 },
                    { 13, 1, new Guid("e0cbbdbb-c003-4e95-8aa0-02abd1578c07"), 4 },
                    { 22, 2, new Guid("0fe76d08-cbc1-499a-8db6-1c9bf324c226"), 6 },
                    { 12, 4, new Guid("97d9137d-02ed-4355-9eb1-5ce0a58f5c66"), 3 },
                    { 10, 2, new Guid("90566884-f88d-491a-9e48-4d448bf4f441"), 3 },
                    { 9, 1, new Guid("8a914048-ac17-44b5-a163-8cca7aab7551"), 3 },
                    { 8, 4, new Guid("fdda0029-a3e3-4b16-9bdc-8869da0ef11d"), 2 },
                    { 7, 3, new Guid("9b0b0507-9f03-4382-be49-7feb85a18992"), 2 },
                    { 6, 2, new Guid("9d17d33b-f258-4e38-aab3-75578338ee82"), 2 },
                    { 5, 1, new Guid("36b4c07f-eeb1-423a-93c6-f52298b9f16c"), 2 },
                    { 4, 4, new Guid("8741199f-7e08-48aa-a290-16b84e60b997"), 1 },
                    { 3, 3, new Guid("ebf320f4-914d-4ca9-b003-4f71d7e8a718"), 1 },
                    { 2, 2, new Guid("12813071-2c88-4651-a702-2ee6b3d6692c"), 1 },
                    { 11, 3, new Guid("210bcfd1-f05e-4638-8c58-ce8e12c9ef78"), 3 },
                    { 48, 4, new Guid("d63b9c3a-7e0c-4f7c-9cf6-e568eeab8f09"), 12 },
                    { 24, 4, new Guid("ac28753c-3f5f-4eb6-a408-82b80013ca7b"), 6 },
                    { 37, 1, new Guid("447f0cb3-7f98-4c42-984f-550430bf3ec0"), 10 },
                    { 45, 1, new Guid("006dabbc-6178-4caa-9661-14e0fdd9ddc9"), 12 },
                    { 44, 4, new Guid("6d1f41fb-dfca-4bb9-a64d-fff08abc1c0f"), 11 },
                    { 43, 3, new Guid("1681cff3-9973-428c-9e23-9a98738f4cf5"), 11 },
                    { 42, 2, new Guid("429aab2e-e8b7-4006-a9e3-ef5f83e0201b"), 11 },
                    { 41, 1, new Guid("2873eaca-5d82-4b35-af4f-9ebbfca37270"), 11 },
                    { 40, 4, new Guid("1a124208-ea30-4a39-b92a-b10a8f804289"), 10 },
                    { 39, 3, new Guid("d1c21b9f-4063-4b8b-898c-af2fec3cf5d0"), 10 },
                    { 38, 2, new Guid("821537a1-163e-477c-ba79-74c24969058e"), 10 },
                    { 25, 1, new Guid("bdd20ec5-52f4-4ae3-bd50-340a040522ab"), 7 },
                    { 46, 2, new Guid("29c727ef-3108-4fe5-a432-e303a3b1d3cf"), 12 }
                });

            migrationBuilder.InsertData(
                schema: "Permission",
                table: "PermissionControllerAction",
                columns: new[] { "PermissionControllerActionID", "PermissionActionID", "PermissionControllerActionGuid", "PermissionControllerID" },
                values: new object[,]
                {
                    { 36, 4, new Guid("8fa74554-3f9a-4efa-8df4-da502a8f30e5"), 9 },
                    { 34, 2, new Guid("46014ba8-ce45-4ff5-88c5-465ccd0e3ba4"), 9 },
                    { 33, 1, new Guid("075590e1-beaa-47f4-b64f-8c57e9514b9f"), 9 },
                    { 32, 4, new Guid("9cf7ba20-0877-473d-82d8-265cf3c054f5"), 8 },
                    { 31, 3, new Guid("bd2baa76-c54d-4e89-807e-9f842e6fa9e6"), 8 },
                    { 30, 2, new Guid("5b2410d3-b286-4d39-8af4-ecfb951f4e63"), 8 },
                    { 29, 1, new Guid("a6678c89-c89b-4640-a375-484ee79050f8"), 8 },
                    { 28, 4, new Guid("51f438a7-dc86-4c92-8313-fbfe32a90589"), 7 },
                    { 27, 3, new Guid("387ae1d7-e523-4e9d-9b0b-d09fe0acc74f"), 7 },
                    { 26, 2, new Guid("96cdbdf8-ecb5-44fd-bf36-9d9b8f178f34"), 7 },
                    { 35, 3, new Guid("eea6b008-6267-4c16-8d03-cd90e878a318"), 9 },
                    { 47, 3, new Guid("f43ca4a0-e083-46a5-bb1e-dadff9db4ed1"), 12 }
                });

            migrationBuilder.InsertData(
                schema: "Setting",
                table: "Country",
                columns: new[] { "CountryID", "CountryGuid", "CreateDate", "DeleteDate", "EnableDate", "Extension", "Flag", "IsDeleted", "IsEnable", "Lat", "Long", "NameAR", "NameEN", "Place", "UpdateDate", "UserDelete", "UserEnable", "UserId", "UserUpdate", "Zoom" },
                values: new object[] { 1, new Guid("f02763bb-3ae4-4f4a-8c37-f4585a18c52a"), new DateTime(2021, 9, 11, 15, 36, 12, 177, DateTimeKind.Local).AddTicks(419), null, new DateTime(2021, 9, 11, 15, 36, 12, 178, DateTimeKind.Local).AddTicks(7886), "00966", null, false, true, "", "", "السعودية", "SA", "", null, null, null, 1, null, null });

            migrationBuilder.InsertData(
                schema: "Setting",
                table: "Jobs",
                columns: new[] { "JobsID", "CreateDate", "DeleteDate", "DescriptionAr", "DescriptionEn", "EnableDate", "Image", "IsDeleted", "IsEnable", "JobTypeId", "JobsGuid", "NameAR", "NameEN", "UpdateDate", "UserDelete", "UserEnable", "UserId", "UserUpdate" },
                values: new object[] { 1, new DateTime(2021, 9, 11, 15, 36, 12, 182, DateTimeKind.Local).AddTicks(6663), null, null, null, new DateTime(2021, 9, 11, 15, 36, 12, 182, DateTimeKind.Local).AddTicks(7729), null, false, true, 2, new Guid("5c9ba983-81cf-4ec3-8acf-e4bea92085c9"), "الدمام", "DMM", null, null, null, 1, null });

            migrationBuilder.InsertData(
                schema: "Setting",
                table: "MainCategory",
                columns: new[] { "MainCategoryID", "CreateDate", "DeleteDate", "EnableDate", "IsDeleted", "IsEnable", "MainCategoryGuid", "NameAR", "NameEN", "UpdateDate", "UserDelete", "UserEnable", "UserId", "UserUpdate" },
                values: new object[] { 1, new DateTime(2021, 9, 11, 15, 36, 12, 183, DateTimeKind.Local).AddTicks(2886), null, new DateTime(2021, 9, 11, 15, 36, 12, 183, DateTimeKind.Local).AddTicks(3943), false, true, new Guid("7d46303c-a0e0-4eb9-a346-e31730d9931d"), "الدمام", "DMM", null, null, null, 1, null });

            migrationBuilder.InsertData(
                schema: "Setting",
                table: "Nationality",
                columns: new[] { "NationalityID", "CreateDate", "DeleteDate", "DescriptionAr", "DescriptionEn", "EnableDate", "IsDeleted", "IsEnable", "NameAR", "NameEN", "NationalityGuid", "UpdateDate", "UserDelete", "UserEnable", "UserId", "UserUpdate" },
                values: new object[] { 1, new DateTime(2021, 9, 11, 15, 36, 12, 182, DateTimeKind.Local).AddTicks(71), null, null, null, new DateTime(2021, 9, 11, 15, 36, 12, 182, DateTimeKind.Local).AddTicks(1244), false, true, "الدمام", "DMM", new Guid("0b6639b2-7283-475c-a109-75d37b049397"), null, null, null, 1, null });

            migrationBuilder.InsertData(
                schema: "Setting",
                table: "Departments",
                columns: new[] { "DepartmentsID", "CreateDate", "DeleteDate", "DepartmentsGuid", "DescriptionAr", "DescriptionEn", "EnableDate", "Image", "IsDeleted", "IsEnable", "Isunique", "MainCategoryID", "NameAR", "NameEN", "UpdateDate", "UserDelete", "UserEnable", "UserId", "UserUpdate" },
                values: new object[] { 1, new DateTime(2021, 9, 11, 15, 36, 12, 183, DateTimeKind.Local).AddTicks(9947), null, new Guid("3bd672cc-f9a3-4b75-b9b0-402409369cc1"), null, null, new DateTime(2021, 9, 11, 15, 36, 12, 184, DateTimeKind.Local).AddTicks(1013), null, false, true, false, 1, "الدمام", "DMM", null, null, null, 1, null });

            migrationBuilder.InsertData(
                schema: "Setting",
                table: "Region",
                columns: new[] { "RegionID", "CountryID", "CreateDate", "DeleteDate", "EnableDate", "IsDeleted", "IsEnable", "Lat", "Long", "NameAR", "NameEN", "Place", "RegionGuid", "UpdateDate", "UserDelete", "UserEnable", "UserId", "UserUpdate", "Zoom" },
                values: new object[] { 1, 1, new DateTime(2021, 9, 11, 15, 36, 12, 179, DateTimeKind.Local).AddTicks(7716), null, new DateTime(2021, 9, 11, 15, 36, 12, 179, DateTimeKind.Local).AddTicks(8843), false, true, "", "", "الدمام", "DMM", "", new Guid("037d151f-5902-4cc5-a640-dedfa9264e2d"), null, null, null, 1, null, null });

            migrationBuilder.InsertData(
                schema: "Setting",
                table: "City",
                columns: new[] { "CityID", "CityGuid", "Code", "CreateDate", "DeleteDate", "EnableDate", "IsDeleted", "IsEnable", "Lat", "Long", "NameAR", "NameEN", "Place", "RegionID", "UpdateDate", "UserDelete", "UserEnable", "UserId", "UserUpdate", "Zoom" },
                values: new object[] { 1, new Guid("4104f5ac-418b-4307-905c-d5bfe54b021c"), null, new DateTime(2021, 9, 11, 15, 36, 12, 181, DateTimeKind.Local).AddTicks(366), null, new DateTime(2021, 9, 11, 15, 36, 12, 181, DateTimeKind.Local).AddTicks(1501), false, true, "", "", "الدمام", "DMM", "", 1, null, null, null, 1, null, null });

            migrationBuilder.InsertData(
                schema: "Emp",
                table: "Employees",
                columns: new[] { "EntityEmpID", "AddressAR", "AddressEN", "BirthDateHijri", "BirthDateMilady", "BloodTypeId", "CityID", "CreateDate", "DeleteDate", "Email", "EnableDate", "EntityEmpGuid", "FileNo", "FirstNameAR", "FirstNameEN", "Gender", "IDNo", "IsDeleted", "IsEnable", "JobsID", "LastNameAR", "LastNameEN", "Lat", "Lng", "MidNameAR", "MidNameEN", "MobileNo", "NationalityID", "Notes", "PhoneNumber", "Photo", "UpdateDate", "UserDelete", "UserEnable", "UserId", "UserUpdate", "Zoom" },
                values: new object[] { 1, "الاسماعيليه", "ismailia", "", null, 1, 1, new DateTime(2021, 9, 11, 15, 36, 12, 185, DateTimeKind.Local).AddTicks(2781), null, "SystemUser@Admin.com", new DateTime(2021, 9, 11, 15, 36, 12, 185, DateTimeKind.Local).AddTicks(3383), new Guid("2299447c-fc61-4aa4-ba03-8c91e4f4b2d5"), "123321", "احمد", "Ahmed", 1, "", false, true, 1, "حسين", "Hussien", "", "", "سيد", "Sayed", "0595489633", 1, "", "", "", null, null, 1, 1, null, "" });

            migrationBuilder.CreateIndex(
                name: "IX_Activity_UserId",
                schema: "Setting",
                table: "Activity",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Banks_UserId",
                schema: "Setting",
                table: "Banks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Block_CityID",
                schema: "Setting",
                table: "Block",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_Block_UserId",
                schema: "Setting",
                table: "Block",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_City_RegionID",
                schema: "Setting",
                table: "City",
                column: "RegionID");

            migrationBuilder.CreateIndex(
                name: "IX_City_UserId",
                schema: "Setting",
                table: "City",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Configuration_UserId",
                schema: "Setting",
                table: "Configuration",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Country_UserId",
                schema: "Setting",
                table: "Country",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerLocation_BlockID",
                schema: "Customer",
                table: "CustomerLocation",
                column: "BlockID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerLocation_CustomersID",
                schema: "Customer",
                table: "CustomerLocation",
                column: "CustomersID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerLocation_UserId",
                schema: "Customer",
                table: "CustomerLocation",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CityID",
                schema: "Customer",
                table: "Customers",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_NationalityID",
                schema: "Customer",
                table: "Customers",
                column: "NationalityID");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_UserId",
                schema: "Customer",
                table: "Customers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_MainCategoryID",
                schema: "Setting",
                table: "Departments",
                column: "MainCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_UserId",
                schema: "Setting",
                table: "Departments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CityID",
                schema: "Emp",
                table: "Employees",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_JobsID",
                schema: "Emp",
                table: "Employees",
                column: "JobsID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_NationalityID",
                schema: "Emp",
                table: "Employees",
                column: "NationalityID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UserId",
                schema: "Emp",
                table: "Employees",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_UserId",
                schema: "Setting",
                table: "Jobs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MainCategory_UserId",
                schema: "Setting",
                table: "MainCategory",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MainPage_UserId",
                schema: "Setting",
                table: "MainPage",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MainPageDetails_MainPageID",
                schema: "Setting",
                table: "MainPageDetails",
                column: "MainPageID");

            migrationBuilder.CreateIndex(
                name: "IX_MainPageDetails_UserId",
                schema: "Setting",
                table: "MainPageDetails",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MainPageImages_MainPageDetailsID",
                schema: "Setting",
                table: "MainPageImages",
                column: "MainPageDetailsID");

            migrationBuilder.CreateIndex(
                name: "IX_MainPageImages_UserId",
                schema: "Setting",
                table: "MainPageImages",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Nationality_UserId",
                schema: "Setting",
                table: "Nationality",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderStatus_UserId",
                schema: "Setting",
                table: "OrderStatus",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Package_UserId",
                schema: "Setting",
                table: "Package",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentConfiguration_BanksID",
                schema: "Setting",
                table: "PaymentConfiguration",
                column: "BanksID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentConfiguration_UserId",
                schema: "Setting",
                table: "PaymentConfiguration",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentStatus_UserId",
                schema: "Setting",
                table: "PaymentStatus",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentWay_UserId",
                schema: "Setting",
                table: "PaymentWay",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Permission_PermissionControllerActionID",
                schema: "Permission",
                table: "Permission",
                column: "PermissionControllerActionID");

            migrationBuilder.CreateIndex(
                name: "IX_Permission_RoleId",
                schema: "Permission",
                table: "Permission",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Permission_UserId",
                schema: "Permission",
                table: "Permission",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionControllerAction_PermissionActionID",
                schema: "Permission",
                table: "PermissionControllerAction",
                column: "PermissionActionID");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionControllerAction_PermissionControllerID",
                schema: "Permission",
                table: "PermissionControllerAction",
                column: "PermissionControllerID");

            migrationBuilder.CreateIndex(
                name: "IX_Product_DepartmentsID",
                schema: "Vendor",
                table: "Product",
                column: "DepartmentsID");

            migrationBuilder.CreateIndex(
                name: "IX_Product_UserId",
                schema: "Vendor",
                table: "Product",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_VendorsID",
                schema: "Vendor",
                table: "Product",
                column: "VendorsID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImage_ProductID",
                schema: "Vendor",
                table: "ProductImage",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImage_UserId",
                schema: "Vendor",
                table: "ProductImage",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductQuestion_ProductID",
                schema: "Vendor",
                table: "ProductQuestion",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductQuestion_UserId",
                schema: "Vendor",
                table: "ProductQuestion",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Region_CountryID",
                schema: "Setting",
                table: "Region",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_Region_UserId",
                schema: "Setting",
                table: "Region",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Role",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RoleConfig_PermissionControllerActionID",
                schema: "Permission",
                table: "RoleConfig",
                column: "PermissionControllerActionID");

            migrationBuilder.CreateIndex(
                name: "IX_RoleConfig_RoleId",
                schema: "Permission",
                table: "RoleConfig",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingCompany_CityID",
                schema: "Setting",
                table: "ShippingCompany",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingCompany_UserId",
                schema: "Setting",
                table: "ShippingCompany",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Slider_UserId",
                schema: "Setting",
                table: "Slider",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TempPermission_PermissionControllerActionID",
                schema: "Permission",
                table: "TempPermission",
                column: "PermissionControllerActionID");

            migrationBuilder.CreateIndex(
                name: "IX_TempPermission_RoleId",
                schema: "Permission",
                table: "TempPermission",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "User",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "User",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_ActivityID",
                schema: "Vendor",
                table: "Vendors",
                column: "ActivityID");

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_BanksID",
                schema: "Vendor",
                table: "Vendors",
                column: "BanksID");

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_BlockID",
                schema: "Vendor",
                table: "Vendors",
                column: "BlockID");

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_CityID",
                schema: "Vendor",
                table: "Vendors",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_NationalityID",
                schema: "Vendor",
                table: "Vendors",
                column: "NationalityID");

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_PackageID",
                schema: "Vendor",
                table: "Vendors",
                column: "PackageID");

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_UserId",
                schema: "Vendor",
                table: "Vendors",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Configuration",
                schema: "Setting");

            migrationBuilder.DropTable(
                name: "CustomerLocation",
                schema: "Customer");

            migrationBuilder.DropTable(
                name: "Employees",
                schema: "Emp");

            migrationBuilder.DropTable(
                name: "Inqueries",
                schema: "Setting");

            migrationBuilder.DropTable(
                name: "MainPageImages",
                schema: "Setting");

            migrationBuilder.DropTable(
                name: "OrderStatus",
                schema: "Setting");

            migrationBuilder.DropTable(
                name: "PaymentConfiguration",
                schema: "Setting");

            migrationBuilder.DropTable(
                name: "PaymentStatus",
                schema: "Setting");

            migrationBuilder.DropTable(
                name: "PaymentWay",
                schema: "Setting");

            migrationBuilder.DropTable(
                name: "Permission",
                schema: "Permission");

            migrationBuilder.DropTable(
                name: "ProductImage",
                schema: "Vendor");

            migrationBuilder.DropTable(
                name: "ProductQuestion",
                schema: "Vendor");

            migrationBuilder.DropTable(
                name: "RoleConfig",
                schema: "Permission");

            migrationBuilder.DropTable(
                name: "ShippingCompany",
                schema: "Setting");

            migrationBuilder.DropTable(
                name: "Slider",
                schema: "Setting");

            migrationBuilder.DropTable(
                name: "TempPermission",
                schema: "Permission");

            migrationBuilder.DropTable(
                name: "Tokens",
                schema: "Setting");

            migrationBuilder.DropTable(
                name: "Customers",
                schema: "Customer");

            migrationBuilder.DropTable(
                name: "Jobs",
                schema: "Setting");

            migrationBuilder.DropTable(
                name: "MainPageDetails",
                schema: "Setting");

            migrationBuilder.DropTable(
                name: "Product",
                schema: "Vendor");

            migrationBuilder.DropTable(
                name: "PermissionControllerAction",
                schema: "Permission");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "MainPage",
                schema: "Setting");

            migrationBuilder.DropTable(
                name: "Departments",
                schema: "Setting");

            migrationBuilder.DropTable(
                name: "Vendors",
                schema: "Vendor");

            migrationBuilder.DropTable(
                name: "PermissionAction",
                schema: "Permission");

            migrationBuilder.DropTable(
                name: "PermissionController",
                schema: "Permission");

            migrationBuilder.DropTable(
                name: "MainCategory",
                schema: "Setting");

            migrationBuilder.DropTable(
                name: "Activity",
                schema: "Setting");

            migrationBuilder.DropTable(
                name: "Banks",
                schema: "Setting");

            migrationBuilder.DropTable(
                name: "Block",
                schema: "Setting");

            migrationBuilder.DropTable(
                name: "Nationality",
                schema: "Setting");

            migrationBuilder.DropTable(
                name: "Package",
                schema: "Setting");

            migrationBuilder.DropTable(
                name: "City",
                schema: "Setting");

            migrationBuilder.DropTable(
                name: "Region",
                schema: "Setting");

            migrationBuilder.DropTable(
                name: "Country",
                schema: "Setting");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
