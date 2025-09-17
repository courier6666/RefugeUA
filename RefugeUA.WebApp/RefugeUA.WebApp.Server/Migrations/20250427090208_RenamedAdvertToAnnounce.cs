using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RefugeUA.WebApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class RenamedAdvertToAnnounce : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccomodationImages_AccomodationAdvertisements_AccomodationAdvertisementId",
                table: "AccomodationImages");

            migrationBuilder.DropTable(
                name: "AccomodationAdvertisements");

            migrationBuilder.DropTable(
                name: "AdvertisementResponses");

            migrationBuilder.DropTable(
                name: "AdvertisementToGroup");

            migrationBuilder.DropTable(
                name: "EducationAdvertisements");

            migrationBuilder.DropTable(
                name: "WorkAdvertisements");

            migrationBuilder.DropTable(
                name: "AdvertisementGroups");

            migrationBuilder.DropTable(
                name: "Advertisement");

            migrationBuilder.RenameColumn(
                name: "AccomodationAdvertisementId",
                table: "AccomodationImages",
                newName: "AccomodationAnnouncementId");

            migrationBuilder.CreateTable(
                name: "Announcement",
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
                    NonAcceptenceReason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsClosed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Announcement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Announcement_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Announcement_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Announcement_ContactInformation_ContactInformationId",
                        column: x => x.ContactInformationId,
                        principalTable: "ContactInformation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnnouncementGroups",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnouncementGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccomodationAnnouncements",
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
                    table.PrimaryKey("PK_AccomodationAnnouncements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccomodationAnnouncements_Announcement_Id",
                        column: x => x.Id,
                        principalTable: "Announcement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnnouncementResponses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    AnnouncementId = table.Column<long>(type: "bigint", nullable: false),
                    ContactInformationId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnouncementResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnnouncementResponses_Announcement_AnnouncementId",
                        column: x => x.AnnouncementId,
                        principalTable: "Announcement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnnouncementResponses_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnnouncementResponses_ContactInformation_ContactInformationId",
                        column: x => x.ContactInformationId,
                        principalTable: "ContactInformation",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EducationAnnouncements",
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
                    table.PrimaryKey("PK_EducationAnnouncements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EducationAnnouncements_Announcement_Id",
                        column: x => x.Id,
                        principalTable: "Announcement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkAnnouncements",
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
                    table.PrimaryKey("PK_WorkAnnouncements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkAnnouncements_Announcement_Id",
                        column: x => x.Id,
                        principalTable: "Announcement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnnouncementToGroup",
                columns: table => new
                {
                    AnnouncementsId = table.Column<long>(type: "bigint", nullable: false),
                    GroupsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnouncementToGroup", x => new { x.AnnouncementsId, x.GroupsId });
                    table.ForeignKey(
                        name: "FK_AnnouncementToGroup_AnnouncementGroups_GroupsId",
                        column: x => x.GroupsId,
                        principalTable: "AnnouncementGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnnouncementToGroup_Announcement_AnnouncementsId",
                        column: x => x.AnnouncementsId,
                        principalTable: "Announcement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Announcement_AddressId",
                table: "Announcement",
                column: "AddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Announcement_AuthorId",
                table: "Announcement",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Announcement_ContactInformationId",
                table: "Announcement",
                column: "ContactInformationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Announcement_Title",
                table: "Announcement",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementGroups_Name",
                table: "AnnouncementGroups",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementResponses_AnnouncementId",
                table: "AnnouncementResponses",
                column: "AnnouncementId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementResponses_ContactInformationId",
                table: "AnnouncementResponses",
                column: "ContactInformationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementResponses_UserId",
                table: "AnnouncementResponses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnouncementToGroup_GroupsId",
                table: "AnnouncementToGroup",
                column: "GroupsId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccomodationImages_AccomodationAnnouncements_AccomodationAnnouncementId",
                table: "AccomodationImages",
                column: "AccomodationAnnouncementId",
                principalTable: "AccomodationAnnouncements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccomodationImages_AccomodationAnnouncements_AccomodationAnnouncementId",
                table: "AccomodationImages");

            migrationBuilder.DropTable(
                name: "AccomodationAnnouncements");

            migrationBuilder.DropTable(
                name: "AnnouncementResponses");

            migrationBuilder.DropTable(
                name: "AnnouncementToGroup");

            migrationBuilder.DropTable(
                name: "EducationAnnouncements");

            migrationBuilder.DropTable(
                name: "WorkAnnouncements");

            migrationBuilder.DropTable(
                name: "AnnouncementGroups");

            migrationBuilder.DropTable(
                name: "Announcement");

            migrationBuilder.RenameColumn(
                name: "AccomodationAnnouncementId",
                table: "AccomodationImages",
                newName: "AccomodationAdvertisementId");

            migrationBuilder.CreateTable(
                name: "Advertisement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressId = table.Column<long>(type: "bigint", nullable: false),
                    AuthorId = table.Column<long>(type: "bigint", nullable: true),
                    ContactInformationId = table.Column<long>(type: "bigint", nullable: false),
                    Accepted = table.Column<bool>(type: "bit", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", maxLength: 4096, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsClosed = table.Column<bool>(type: "bit", nullable: false),
                    NonAcceptenceReason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
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
                name: "AccomodationAdvertisements",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    AreaSqMeters = table.Column<float>(type: "real", nullable: true),
                    BuildingType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Floors = table.Column<int>(type: "int", nullable: false),
                    NumberOfRooms = table.Column<int>(type: "int", nullable: false),
                    PetsAllowed = table.Column<bool>(type: "bit", nullable: false)
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
                    AdvertId = table.Column<long>(type: "bigint", nullable: false),
                    ContactInformationId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
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
                name: "EducationAdvertisements",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Duration = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EducationType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Fee = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    InstitutionName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsFree = table.Column<bool>(type: "bit", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TargetGroup = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
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
                    CompanyName = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    JobPosition = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    RequirementsContent = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    SalaryLower = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SalaryUpper = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
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

            migrationBuilder.AddForeignKey(
                name: "FK_AccomodationImages_AccomodationAdvertisements_AccomodationAdvertisementId",
                table: "AccomodationImages",
                column: "AccomodationAdvertisementId",
                principalTable: "AccomodationAdvertisements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
