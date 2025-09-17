using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RefugeUA.WebApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Region = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    District = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Settlement = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PostalCode = table.Column<string>(type: "nchar(5)", fixedLength: true, maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdvertisementGroups",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvertisementGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProfileImageUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactInformation",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Telegram = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Viber = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Facebook = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactInformation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Path = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VolunteerGroups",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DescriptionContent = table.Column<string>(type: "nvarchar(max)", maxLength: 4096, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VolunteerGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
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
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
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
                    UserId = table.Column<long>(type: "bigint", nullable: false)
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
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false)
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
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
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
                name: "MentalSupportArticles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", maxLength: 4096, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuthorId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MentalSupportArticles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MentalSupportArticles_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Advertisement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", maxLength: 4096, nullable: false),
                    ContactInformationId = table.Column<long>(type: "bigint", nullable: false),
                    AddressId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuthorId = table.Column<long>(type: "bigint", nullable: true),
                    Accepted = table.Column<bool>(type: "bit", nullable: false),
                    NonAcceptenceReason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Advertisement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Advertisement_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Advertisement_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Advertisement_ContactInformation_ContactInformationId",
                        column: x => x.ContactInformationId,
                        principalTable: "ContactInformation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PsychologistInformation",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuthorId = table.Column<long>(type: "bigint", nullable: true),
                    ContactId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PsychologistInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PsychologistInformation_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PsychologistInformation_ContactInformation_ContactId",
                        column: x => x.ContactId,
                        principalTable: "ContactInformation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VolunteerEvents",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", maxLength: 4096, nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AddressId = table.Column<long>(type: "bigint", nullable: false),
                    VolunteerGroupId = table.Column<long>(type: "bigint", nullable: true),
                    OnlineConferenceLink = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VolunteerEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VolunteerEvents_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VolunteerEvents_VolunteerGroups_VolunteerGroupId",
                        column: x => x.VolunteerGroupId,
                        principalTable: "VolunteerGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "VolunteerGroupAdmins",
                columns: table => new
                {
                    AdminVolunteerGroupsId = table.Column<long>(type: "bigint", nullable: false),
                    AdminsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VolunteerGroupAdmins", x => new { x.AdminVolunteerGroupsId, x.AdminsId });
                    table.ForeignKey(
                        name: "FK_VolunteerGroupAdmins_AspNetUsers_AdminsId",
                        column: x => x.AdminsId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VolunteerGroupAdmins_VolunteerGroups_AdminVolunteerGroupsId",
                        column: x => x.AdminVolunteerGroupsId,
                        principalTable: "VolunteerGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VolunteerGroupFollowers",
                columns: table => new
                {
                    FollowerVolunteerGroupsId = table.Column<long>(type: "bigint", nullable: false),
                    FollowersId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VolunteerGroupFollowers", x => new { x.FollowerVolunteerGroupsId, x.FollowersId });
                    table.ForeignKey(
                        name: "FK_VolunteerGroupFollowers_AspNetUsers_FollowersId",
                        column: x => x.FollowersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VolunteerGroupFollowers_VolunteerGroups_FollowerVolunteerGroupsId",
                        column: x => x.FollowerVolunteerGroupsId,
                        principalTable: "VolunteerGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccomodationAdvertisements",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    BuildingType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PetsAllowed = table.Column<bool>(type: "bit", nullable: false),
                    Floors = table.Column<int>(type: "int", nullable: false),
                    NumberOfRooms = table.Column<int>(type: "int", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    AreaSqMeters = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccomodationAdvertisements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccomodationAdvertisements_Advertisement_Id",
                        column: x => x.Id,
                        principalTable: "Advertisement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdvertisementResponses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    AdvertId = table.Column<long>(type: "bigint", nullable: false),
                    ContactInformationId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvertisementResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdvertisementResponses_Advertisement_AdvertId",
                        column: x => x.AdvertId,
                        principalTable: "Advertisement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdvertisementResponses_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdvertisementResponses_ContactInformation_ContactInformationId",
                        column: x => x.ContactInformationId,
                        principalTable: "ContactInformation",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AdvertisementToGroup",
                columns: table => new
                {
                    AdvertisementsId = table.Column<long>(type: "bigint", nullable: false),
                    GroupsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvertisementToGroup", x => new { x.AdvertisementsId, x.GroupsId });
                    table.ForeignKey(
                        name: "FK_AdvertisementToGroup_AdvertisementGroups_GroupsId",
                        column: x => x.GroupsId,
                        principalTable: "AdvertisementGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdvertisementToGroup_Advertisement_AdvertisementsId",
                        column: x => x.AdvertisementsId,
                        principalTable: "Advertisement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EducationAdvertisements",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    EducationType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    InstitutionName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TargetGroup = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsFree = table.Column<bool>(type: "bit", nullable: false),
                    Fee = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Duration = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Language = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationAdvertisements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EducationAdvertisements_Advertisement_Id",
                        column: x => x.Id,
                        principalTable: "Advertisement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkAdvertisements",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    JobPosition = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    SalaryLower = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SalaryUpper = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RequirementsContent = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkAdvertisements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkAdvertisements_Advertisement_Id",
                        column: x => x.Id,
                        principalTable: "Advertisement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventOrganizers",
                columns: table => new
                {
                    OrganizedEventsId = table.Column<long>(type: "bigint", nullable: false),
                    OrganizersId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventOrganizers", x => new { x.OrganizedEventsId, x.OrganizersId });
                    table.ForeignKey(
                        name: "FK_EventOrganizers_AspNetUsers_OrganizersId",
                        column: x => x.OrganizersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventOrganizers_VolunteerEvents_OrganizedEventsId",
                        column: x => x.OrganizedEventsId,
                        principalTable: "VolunteerEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventParticipants",
                columns: table => new
                {
                    ParticipantsId = table.Column<long>(type: "bigint", nullable: false),
                    ParticipatedEventsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventParticipants", x => new { x.ParticipantsId, x.ParticipatedEventsId });
                    table.ForeignKey(
                        name: "FK_EventParticipants_AspNetUsers_ParticipantsId",
                        column: x => x.ParticipantsId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventParticipants_VolunteerEvents_ParticipatedEventsId",
                        column: x => x.ParticipatedEventsId,
                        principalTable: "VolunteerEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VolunteerEventScheduleItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    VolunteerEventId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VolunteerEventScheduleItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VolunteerEventScheduleItems_VolunteerEvents_VolunteerEventId",
                        column: x => x.VolunteerEventId,
                        principalTable: "VolunteerEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccomodationImages",
                columns: table => new
                {
                    AccomodationAdvertisementId = table.Column<long>(type: "bigint", nullable: false),
                    ImagesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccomodationImages", x => new { x.AccomodationAdvertisementId, x.ImagesId });
                    table.ForeignKey(
                        name: "FK_AccomodationImages_AccomodationAdvertisements_AccomodationAdvertisementId",
                        column: x => x.AccomodationAdvertisementId,
                        principalTable: "AccomodationAdvertisements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccomodationImages_Images_ImagesId",
                        column: x => x.ImagesId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccomodationImages_ImagesId",
                table: "AccomodationImages",
                column: "ImagesId");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisement_AddressId",
                table: "Advertisement",
                column: "AddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Advertisement_AuthorId",
                table: "Advertisement",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisement_ContactInformationId",
                table: "Advertisement",
                column: "ContactInformationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Advertisement_Title",
                table: "Advertisement",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AdvertisementGroups_Name",
                table: "AdvertisementGroups",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AdvertisementResponses_AdvertId",
                table: "AdvertisementResponses",
                column: "AdvertId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvertisementResponses_ContactInformationId",
                table: "AdvertisementResponses",
                column: "ContactInformationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AdvertisementResponses_UserId",
                table: "AdvertisementResponses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvertisementToGroup_GroupsId",
                table: "AdvertisementToGroup",
                column: "GroupsId");

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
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Email",
                table: "AspNetUsers",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PhoneNumber",
                table: "AspNetUsers",
                column: "PhoneNumber",
                unique: true,
                filter: "[PhoneNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EventOrganizers_OrganizersId",
                table: "EventOrganizers",
                column: "OrganizersId");

            migrationBuilder.CreateIndex(
                name: "IX_EventParticipants_ParticipatedEventsId",
                table: "EventParticipants",
                column: "ParticipatedEventsId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_Path",
                table: "Images",
                column: "Path",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MentalSupportArticles_AuthorId",
                table: "MentalSupportArticles",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_MentalSupportArticles_Title",
                table: "MentalSupportArticles",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PsychologistInformation_AuthorId",
                table: "PsychologistInformation",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_PsychologistInformation_ContactId",
                table: "PsychologistInformation",
                column: "ContactId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PsychologistInformation_Title",
                table: "PsychologistInformation",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerEvents_AddressId",
                table: "VolunteerEvents",
                column: "AddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerEvents_VolunteerGroupId",
                table: "VolunteerEvents",
                column: "VolunteerGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerEventScheduleItems_VolunteerEventId",
                table: "VolunteerEventScheduleItems",
                column: "VolunteerEventId");

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerGroupAdmins_AdminsId",
                table: "VolunteerGroupAdmins",
                column: "AdminsId");

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerGroupFollowers_FollowersId",
                table: "VolunteerGroupFollowers",
                column: "FollowersId");

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerGroups_Title",
                table: "VolunteerGroups",
                column: "Title",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccomodationImages");

            migrationBuilder.DropTable(
                name: "AdvertisementResponses");

            migrationBuilder.DropTable(
                name: "AdvertisementToGroup");

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
                name: "EducationAdvertisements");

            migrationBuilder.DropTable(
                name: "EventOrganizers");

            migrationBuilder.DropTable(
                name: "EventParticipants");

            migrationBuilder.DropTable(
                name: "MentalSupportArticles");

            migrationBuilder.DropTable(
                name: "PsychologistInformation");

            migrationBuilder.DropTable(
                name: "VolunteerEventScheduleItems");

            migrationBuilder.DropTable(
                name: "VolunteerGroupAdmins");

            migrationBuilder.DropTable(
                name: "VolunteerGroupFollowers");

            migrationBuilder.DropTable(
                name: "WorkAdvertisements");

            migrationBuilder.DropTable(
                name: "AccomodationAdvertisements");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "AdvertisementGroups");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "VolunteerEvents");

            migrationBuilder.DropTable(
                name: "Advertisement");

            migrationBuilder.DropTable(
                name: "VolunteerGroups");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ContactInformation");
        }
    }
}
