using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PointOfSale.Server.Migrations
{
    public partial class IntialDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AddressTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BarcodeTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BarcodeTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PrefixName = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    NaturalOfWork = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CountOfEmployees = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountOfBranches = table.Column<int>(type: "int", nullable: false),
                    OwnerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OwnerPhone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeviceCodes",
                columns: table => new
                {
                    UserCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DeviceCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SubjectId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SessionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ClientId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceCodes", x => x.UserCode);
                });

            migrationBuilder.CreateTable(
                name: "Emails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FromPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SmtpClient = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SmtpPort = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoyaltyPrograms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PointPerMoneySpent = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoyaltyPrograms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersistedGrants",
                columns: table => new
                {
                    Key = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SubjectId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SessionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ClientId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConsumedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Data = table.Column<string>(type: "nvarchar(max)", maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersistedGrants", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "SahlApplication",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    APPNAME = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SahlApplication", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Taxes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taxes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vendors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerType = table.Column<int>(type: "int", nullable: false),
                    CustomerImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressTypeId = table.Column<int>(type: "int", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyAddressStreet1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyAddressStreet2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobPosition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WebsiteLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_AddressTypes_AddressTypeId",
                        column: x => x.AddressTypeId,
                        principalTable: "AddressTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BarcodeScanners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BarcodeNomenclature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sequence = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Encoding = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BarcodePattern = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BarcodeTypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BarcodeScanners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BarcodeScanners_BarcodeTypes_BarcodeTypeId",
                        column: x => x.BarcodeTypeId,
                        principalTable: "BarcodeTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    Name_AR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Branches_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departments_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PointsPerUnit = table.Column<double>(type: "float", nullable: false),
                    PointsPerMoneySpent = table.Column<double>(type: "float", nullable: false),
                    LoyaltyProgramId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rules_LoyaltyPrograms_LoyaltyProgramId",
                        column: x => x.LoyaltyProgramId,
                        principalTable: "LoyaltyPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SahlApplicationsCompanies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ExpireDate = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SahlApplicationsCompanies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SahlApplicationsCompanies_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SahlApplicationsCompanies_SahlApplication_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "SahlApplication",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerContacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    ContactTypeId = table.Column<int>(type: "int", nullable: false),
                    ContactName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyAddressStreet1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyAddressStreet2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerContacts_ContactTypes_ContactTypeId",
                        column: x => x.ContactTypeId,
                        principalTable: "ContactTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerContacts_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    RoleId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId1",
                        column: x => x.RoleId1,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shop",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    IsRestaurant = table.Column<bool>(type: "bit", nullable: false),
                    LoginWithEmployees = table.Column<bool>(type: "bit", nullable: false),
                    TableManagement = table.Column<bool>(type: "bit", nullable: false),
                    OrderlineNotes = table.Column<bool>(type: "bit", nullable: false),
                    CategoryPictures = table.Column<bool>(type: "bit", nullable: false),
                    RestrictAvailableCategories = table.Column<bool>(type: "bit", nullable: false),
                    VirtualKeyBoard = table.Column<bool>(type: "bit", nullable: false),
                    SetStartCategory = table.Column<bool>(type: "bit", nullable: false),
                    StartCategoryId = table.Column<int>(type: "int", nullable: true),
                    LargeScrollbars = table.Column<bool>(type: "bit", nullable: false),
                    ConnectDevices = table.Column<bool>(type: "bit", nullable: false),
                    PrinterIPAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BarcodeScannerId = table.Column<int>(type: "int", nullable: true),
                    OrderPrinter = table.Column<bool>(type: "bit", nullable: false),
                    FiscalPointOfSaleitionPerOrder = table.Column<bool>(type: "bit", nullable: false),
                    SpecificFiscalPointOfSaleition = table.Column<bool>(type: "bit", nullable: false),
                    FiscalPointOfSaleitionId = table.Column<int>(type: "int", nullable: true),
                    DefaultPricelistId = table.Column<int>(type: "int", nullable: true),
                    PricelistId = table.Column<int>(type: "int", nullable: true),
                    AdvancedPricelists = table.Column<bool>(type: "bit", nullable: false),
                    ProductPrices = table.Column<int>(type: "int", nullable: false),
                    GlobalDiscounts = table.Column<bool>(type: "bit", nullable: false),
                    LoyaltyProgram = table.Column<bool>(type: "bit", nullable: false),
                    PriceControl = table.Column<bool>(type: "bit", nullable: false),
                    PrefillCashPayment = table.Column<bool>(type: "bit", nullable: false),
                    CashControl = table.Column<bool>(type: "bit", nullable: false),
                    AuthorizedDifference = table.Column<double>(type: "float", nullable: false),
                    Tips = table.Column<bool>(type: "bit", nullable: false),
                    TipProductId = table.Column<int>(type: "int", nullable: true),
                    HeaderFooter = table.Column<bool>(type: "bit", nullable: false),
                    Header = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Footer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AutomaticReceiptPrinting = table.Column<bool>(type: "bit", nullable: false),
                    ReprintReceipt = table.Column<bool>(type: "bit", nullable: false),
                    BillPrinting = table.Column<bool>(type: "bit", nullable: false),
                    BillSpliitting = table.Column<bool>(type: "bit", nullable: false),
                    Invoicing = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shop", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shop_BarcodeScanners_BarcodeScannerId",
                        column: x => x.BarcodeScannerId,
                        principalTable: "BarcodeScanners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Shop_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Floors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FloorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShopId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Floors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Floors_Shop_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shop",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MehtodName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cash = table.Column<bool>(type: "bit", nullable: false),
                    ShopId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentMethods_Shop_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shop",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pricelists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    ShopId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pricelists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pricelists_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pricelists_Shop_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shop",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Printers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrinterType = table.Column<int>(type: "int", nullable: false),
                    ProxyIPAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShopId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Printers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Printers_Shop_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shop",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ShopId = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Shop_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shop",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FloorTables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TableName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Seats = table.Column<int>(type: "int", nullable: false),
                    FloorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FloorTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FloorTables_Floors_FloorId",
                        column: x => x.FloorId,
                        principalTable: "Floors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CountryGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryGroupName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PricelistId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CountryGroups_Pricelists_PricelistId",
                        column: x => x.PricelistId,
                        principalTable: "Pricelists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PriceRules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplyOn = table.Column<int>(type: "int", nullable: false),
                    PricelistId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceRules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceRules_Pricelists_PricelistId",
                        column: x => x.PricelistId,
                        principalTable: "Pricelists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentCategoryId = table.Column<int>(type: "int", nullable: true),
                    PrinterId = table.Column<int>(type: "int", nullable: true),
                    ShopId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductCategories_Printers_PrinterId",
                        column: x => x.PrinterId,
                        principalTable: "Printers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductCategories_ProductCategories_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "ProductCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductCategories_Shop_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shop",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryGroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Countries_CountryGroups_CountryGroupId",
                        column: x => x.CountryGroupId,
                        principalTable: "CountryGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CanBeSold = table.Column<bool>(type: "bit", nullable: false),
                    CanBePurchased = table.Column<bool>(type: "bit", nullable: false),
                    CanBeExpensed = table.Column<bool>(type: "bit", nullable: false),
                    CanBeRented = table.Column<bool>(type: "bit", nullable: false),
                    ProductType = table.Column<int>(type: "int", nullable: false),
                    ProductCategoryId = table.Column<int>(type: "int", nullable: true),
                    InternalReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Barcode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SalesPrice = table.Column<double>(type: "float", nullable: false),
                    Cost = table.Column<double>(type: "float", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoicingPolicy = table.Column<int>(type: "int", nullable: false),
                    SubscriptionProduct = table.Column<bool>(type: "bit", nullable: false),
                    IsEventTicket = table.Column<bool>(type: "bit", nullable: false),
                    SalesDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AvailableInPOS = table.Column<bool>(type: "bit", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    ToWeighWithScale = table.Column<bool>(type: "bit", nullable: false),
                    Procurement = table.Column<int>(type: "int", nullable: false),
                    ControlPolicy = table.Column<int>(type: "int", nullable: false),
                    PurchaseDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manufacture = table.Column<bool>(type: "bit", nullable: false),
                    Buy = table.Column<bool>(type: "bit", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    Volume = table.Column<double>(type: "float", nullable: false),
                    ManufacturingLeadTime = table.Column<double>(type: "float", nullable: false),
                    CustomerLeadTime = table.Column<double>(type: "float", nullable: false),
                    HSCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionForDeliveryOrders = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionForReceipts = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_ProductCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ProductCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_ProductCategories_ProductCategoryId",
                        column: x => x.ProductCategoryId,
                        principalTable: "ProductCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FiscalPointOfSaleitions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FiscalPointOfSaleitionName = table.Column<int>(type: "int", nullable: false),
                    DetectAutomatically = table.Column<bool>(type: "bit", nullable: false),
                    VATRequired = table.Column<bool>(type: "bit", nullable: false),
                    CountryGroupId = table.Column<int>(type: "int", nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiscalPointOfSaleitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FiscalPointOfSaleitions_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FiscalPointOfSaleitions_CountryGroups_CountryGroupId",
                        column: x => x.CountryGroupId,
                        principalTable: "CountryGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerTaxs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaxId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerTaxs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerTaxs_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerTaxs_Taxes_TaxId",
                        column: x => x.TaxId,
                        principalTable: "Taxes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OptionalProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    OptionalProductId = table.Column<int>(type: "int", nullable: false),
                    OptionalProductsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OptionalProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OptionalProducts_OptionalProducts_OptionalProductsId",
                        column: x => x.OptionalProductsId,
                        principalTable: "OptionalProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OptionalProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rewards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RewardCost = table.Column<double>(type: "float", nullable: false),
                    MinimumPoints = table.Column<double>(type: "float", nullable: false),
                    RewardType = table.Column<int>(type: "int", nullable: false),
                    GiftProductId = table.Column<int>(type: "int", nullable: true),
                    DiscountProductId = table.Column<int>(type: "int", nullable: true),
                    ApplyDiscountType = table.Column<int>(type: "int", nullable: false),
                    ApplyDiscountAmount = table.Column<double>(type: "float", nullable: false),
                    ApplyDiscountPercentage = table.Column<double>(type: "float", nullable: false),
                    MaxDiscountAmount = table.Column<double>(type: "float", nullable: false),
                    FixedAmount = table.Column<double>(type: "float", nullable: false),
                    MinimumAmount = table.Column<double>(type: "float", nullable: false),
                    LoyaltyProgramId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rewards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rewards_LoyaltyPrograms_LoyaltyProgramId",
                        column: x => x.LoyaltyProgramId,
                        principalTable: "LoyaltyPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rewards_Products_DiscountProductId",
                        column: x => x.DiscountProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rewards_Products_GiftProductId",
                        column: x => x.GiftProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VendorProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    VendorId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VendorProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VendorProducts_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VendorTaxes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaxId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorTaxes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VendorTaxes_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VendorTaxes_Taxes_TaxId",
                        column: x => x.TaxId,
                        principalTable: "Taxes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaxMappings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaxOnProductId = table.Column<int>(type: "int", nullable: true),
                    TaxToApplyId = table.Column<int>(type: "int", nullable: true),
                    FiscalPointOfSaleitionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxMappings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaxMappings_FiscalPointOfSaleitions_FiscalPointOfSaleitionId",
                        column: x => x.FiscalPointOfSaleitionId,
                        principalTable: "FiscalPointOfSaleitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaxMappings_Taxes_TaxOnProductId",
                        column: x => x.TaxOnProductId,
                        principalTable: "Taxes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaxMappings_Taxes_TaxToApplyId",
                        column: x => x.TaxToApplyId,
                        principalTable: "Taxes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

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
                name: "IX_AspNetUserRoles_RoleId1",
                table: "AspNetUserRoles",
                column: "RoleId1");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId1",
                table: "AspNetUserRoles",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CompanyId",
                table: "AspNetUsers",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BarcodeScanners_BarcodeTypeId",
                table: "BarcodeScanners",
                column: "BarcodeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Branches_CompanyId",
                table: "Branches",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_CountryGroupId",
                table: "Countries",
                column: "CountryGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryGroups_PricelistId",
                table: "CountryGroups",
                column: "PricelistId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerContacts_ContactTypeId",
                table: "CustomerContacts",
                column: "ContactTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerContacts_CustomerId",
                table: "CustomerContacts",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_AddressTypeId",
                table: "Customers",
                column: "AddressTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerTaxs_ProductId",
                table: "CustomerTaxs",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerTaxs_TaxId",
                table: "CustomerTaxs",
                column: "TaxId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_CompanyId",
                table: "Departments",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCodes_DeviceCode",
                table: "DeviceCodes",
                column: "DeviceCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCodes_Expiration",
                table: "DeviceCodes",
                column: "Expiration");

            migrationBuilder.CreateIndex(
                name: "IX_FiscalPointOfSaleitions_CountryGroupId",
                table: "FiscalPointOfSaleitions",
                column: "CountryGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_FiscalPointOfSaleitions_CountryId",
                table: "FiscalPointOfSaleitions",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Floors_ShopId",
                table: "Floors",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_FloorTables_FloorId",
                table: "FloorTables",
                column: "FloorId");

            migrationBuilder.CreateIndex(
                name: "IX_OptionalProducts_OptionalProductsId",
                table: "OptionalProducts",
                column: "OptionalProductsId");

            migrationBuilder.CreateIndex(
                name: "IX_OptionalProducts_ProductId",
                table: "OptionalProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethods_ShopId",
                table: "PaymentMethods",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_Expiration",
                table: "PersistedGrants",
                column: "Expiration");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_SubjectId_ClientId_Type",
                table: "PersistedGrants",
                columns: new[] { "SubjectId", "ClientId", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_SubjectId_SessionId_Type",
                table: "PersistedGrants",
                columns: new[] { "SubjectId", "SessionId", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_Pricelists_CurrencyId",
                table: "Pricelists",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Pricelists_ShopId",
                table: "Pricelists",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceRules_PricelistId",
                table: "PriceRules",
                column: "PricelistId");

            migrationBuilder.CreateIndex(
                name: "IX_Printers_ShopId",
                table: "Printers",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_ParentCategoryId",
                table: "ProductCategories",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_PrinterId",
                table: "ProductCategories",
                column: "PrinterId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_ShopId",
                table: "ProductCategories",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CompanyId",
                table: "Products",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductCategoryId",
                table: "Products",
                column: "ProductCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Rewards_DiscountProductId",
                table: "Rewards",
                column: "DiscountProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Rewards_GiftProductId",
                table: "Rewards",
                column: "GiftProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Rewards_LoyaltyProgramId",
                table: "Rewards",
                column: "LoyaltyProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Rules_LoyaltyProgramId",
                table: "Rules",
                column: "LoyaltyProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_SahlApplicationsCompanies_ApplicationId",
                table: "SahlApplicationsCompanies",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_SahlApplicationsCompanies_CompanyId",
                table: "SahlApplicationsCompanies",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Shop_BarcodeScannerId",
                table: "Shop",
                column: "BarcodeScannerId");

            migrationBuilder.CreateIndex(
                name: "IX_Shop_BranchId",
                table: "Shop",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Shop_FiscalPointOfSaleitionId",
                table: "Shop",
                column: "FiscalPointOfSaleitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Shop_PricelistId",
                table: "Shop",
                column: "PricelistId");

            migrationBuilder.CreateIndex(
                name: "IX_Shop_StartCategoryId",
                table: "Shop",
                column: "StartCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Shop_TipProductId",
                table: "Shop",
                column: "TipProductId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxMappings_FiscalPointOfSaleitionId",
                table: "TaxMappings",
                column: "FiscalPointOfSaleitionId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxMappings_TaxOnProductId",
                table: "TaxMappings",
                column: "TaxOnProductId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxMappings_TaxToApplyId",
                table: "TaxMappings",
                column: "TaxToApplyId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ShopId",
                table: "Users",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorProducts_ProductId",
                table: "VendorProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorProducts_VendorId",
                table: "VendorProducts",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorTaxes_ProductId",
                table: "VendorTaxes",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorTaxes_TaxId",
                table: "VendorTaxes",
                column: "TaxId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_Users_UserId1",
                table: "AspNetUserRoles",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Shop_FiscalPointOfSaleitions_FiscalPointOfSaleitionId",
                table: "Shop",
                column: "FiscalPointOfSaleitionId",
                principalTable: "FiscalPointOfSaleitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Shop_Pricelists_PricelistId",
                table: "Shop",
                column: "PricelistId",
                principalTable: "Pricelists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Shop_ProductCategories_StartCategoryId",
                table: "Shop",
                column: "StartCategoryId",
                principalTable: "ProductCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Shop_Products_TipProductId",
                table: "Shop",
                column: "TipProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branches_Companies_CompanyId",
                table: "Branches");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Companies_CompanyId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_BarcodeScanners_BarcodeTypes_BarcodeTypeId",
                table: "BarcodeScanners");

            migrationBuilder.DropForeignKey(
                name: "FK_Countries_CountryGroups_CountryGroupId",
                table: "Countries");

            migrationBuilder.DropForeignKey(
                name: "FK_FiscalPointOfSaleitions_CountryGroups_CountryGroupId",
                table: "FiscalPointOfSaleitions");

            migrationBuilder.DropForeignKey(
                name: "FK_Shop_Pricelists_PricelistId",
                table: "Shop");

            migrationBuilder.DropForeignKey(
                name: "FK_Shop_Products_TipProductId",
                table: "Shop");

            migrationBuilder.DropForeignKey(
                name: "FK_FiscalPointOfSaleitions_Countries_CountryId",
                table: "FiscalPointOfSaleitions");

            migrationBuilder.DropForeignKey(
                name: "FK_Printers_Shop_ShopId",
                table: "Printers");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategories_Shop_ShopId",
                table: "ProductCategories");

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
                name: "CustomerContacts");

            migrationBuilder.DropTable(
                name: "CustomerTaxs");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "DeviceCodes");

            migrationBuilder.DropTable(
                name: "Emails");

            migrationBuilder.DropTable(
                name: "FloorTables");

            migrationBuilder.DropTable(
                name: "OptionalProducts");

            migrationBuilder.DropTable(
                name: "PaymentMethods");

            migrationBuilder.DropTable(
                name: "PersistedGrants");

            migrationBuilder.DropTable(
                name: "PriceRules");

            migrationBuilder.DropTable(
                name: "Rewards");

            migrationBuilder.DropTable(
                name: "Rules");

            migrationBuilder.DropTable(
                name: "SahlApplicationsCompanies");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "TaxMappings");

            migrationBuilder.DropTable(
                name: "VendorProducts");

            migrationBuilder.DropTable(
                name: "VendorTaxes");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ContactTypes");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Floors");

            migrationBuilder.DropTable(
                name: "LoyaltyPrograms");

            migrationBuilder.DropTable(
                name: "SahlApplication");

            migrationBuilder.DropTable(
                name: "Vendors");

            migrationBuilder.DropTable(
                name: "Taxes");

            migrationBuilder.DropTable(
                name: "AddressTypes");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "BarcodeTypes");

            migrationBuilder.DropTable(
                name: "CountryGroups");

            migrationBuilder.DropTable(
                name: "Pricelists");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Shop");

            migrationBuilder.DropTable(
                name: "BarcodeScanners");

            migrationBuilder.DropTable(
                name: "Branches");

            migrationBuilder.DropTable(
                name: "FiscalPointOfSaleitions");

            migrationBuilder.DropTable(
                name: "ProductCategories");

            migrationBuilder.DropTable(
                name: "Printers");
        }
    }
}
