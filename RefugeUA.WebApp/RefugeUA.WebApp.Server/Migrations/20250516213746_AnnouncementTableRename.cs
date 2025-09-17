using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RefugeUA.WebApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class AnnouncementTableRename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccomodationAnnouncements_Announcement_Id",
                table: "AccomodationAnnouncements");

            migrationBuilder.DropForeignKey(
                name: "FK_Announcement_Addresses_AddressId",
                table: "Announcement");

            migrationBuilder.DropForeignKey(
                name: "FK_Announcement_AspNetUsers_AuthorId",
                table: "Announcement");

            migrationBuilder.DropForeignKey(
                name: "FK_Announcement_ContactInformation_ContactInformationId",
                table: "Announcement");

            migrationBuilder.DropForeignKey(
                name: "FK_AnnouncementResponses_Announcement_AnnouncementId",
                table: "AnnouncementResponses");

            migrationBuilder.DropForeignKey(
                name: "FK_AnnouncementToGroup_Announcement_AnnouncementsId",
                table: "AnnouncementToGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_EducationAnnouncements_Announcement_Id",
                table: "EducationAnnouncements");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkAnnouncements_Announcement_Id",
                table: "WorkAnnouncements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Announcement",
                table: "Announcement");

            migrationBuilder.RenameTable(
                name: "Announcement",
                newName: "Announcements");

            migrationBuilder.RenameIndex(
                name: "IX_Announcement_ContactInformationId",
                table: "Announcements",
                newName: "IX_Announcements_ContactInformationId");

            migrationBuilder.RenameIndex(
                name: "IX_Announcement_AuthorId",
                table: "Announcements",
                newName: "IX_Announcements_AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Announcement_AddressId",
                table: "Announcements",
                newName: "IX_Announcements_AddressId");

            migrationBuilder.AlterColumn<long>(
                name: "AuthorId",
                table: "Announcements",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Announcements",
                table: "Announcements",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AccomodationAnnouncements_Announcements_Id",
                table: "AccomodationAnnouncements",
                column: "Id",
                principalTable: "Announcements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnnouncementResponses_Announcements_AnnouncementId",
                table: "AnnouncementResponses",
                column: "AnnouncementId",
                principalTable: "Announcements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Announcements_Addresses_AddressId",
                table: "Announcements",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Announcements_AspNetUsers_AuthorId",
                table: "Announcements",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Announcements_ContactInformation_ContactInformationId",
                table: "Announcements",
                column: "ContactInformationId",
                principalTable: "ContactInformation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnnouncementToGroup_Announcements_AnnouncementsId",
                table: "AnnouncementToGroup",
                column: "AnnouncementsId",
                principalTable: "Announcements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EducationAnnouncements_Announcements_Id",
                table: "EducationAnnouncements",
                column: "Id",
                principalTable: "Announcements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkAnnouncements_Announcements_Id",
                table: "WorkAnnouncements",
                column: "Id",
                principalTable: "Announcements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccomodationAnnouncements_Announcements_Id",
                table: "AccomodationAnnouncements");

            migrationBuilder.DropForeignKey(
                name: "FK_AnnouncementResponses_Announcements_AnnouncementId",
                table: "AnnouncementResponses");

            migrationBuilder.DropForeignKey(
                name: "FK_Announcements_Addresses_AddressId",
                table: "Announcements");

            migrationBuilder.DropForeignKey(
                name: "FK_Announcements_AspNetUsers_AuthorId",
                table: "Announcements");

            migrationBuilder.DropForeignKey(
                name: "FK_Announcements_ContactInformation_ContactInformationId",
                table: "Announcements");

            migrationBuilder.DropForeignKey(
                name: "FK_AnnouncementToGroup_Announcements_AnnouncementsId",
                table: "AnnouncementToGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_EducationAnnouncements_Announcements_Id",
                table: "EducationAnnouncements");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkAnnouncements_Announcements_Id",
                table: "WorkAnnouncements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Announcements",
                table: "Announcements");

            migrationBuilder.RenameTable(
                name: "Announcements",
                newName: "Announcement");

            migrationBuilder.RenameIndex(
                name: "IX_Announcements_ContactInformationId",
                table: "Announcement",
                newName: "IX_Announcement_ContactInformationId");

            migrationBuilder.RenameIndex(
                name: "IX_Announcements_AuthorId",
                table: "Announcement",
                newName: "IX_Announcement_AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Announcements_AddressId",
                table: "Announcement",
                newName: "IX_Announcement_AddressId");

            migrationBuilder.AlterColumn<long>(
                name: "AuthorId",
                table: "Announcement",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Announcement",
                table: "Announcement",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AccomodationAnnouncements_Announcement_Id",
                table: "AccomodationAnnouncements",
                column: "Id",
                principalTable: "Announcement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Announcement_Addresses_AddressId",
                table: "Announcement",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Announcement_AspNetUsers_AuthorId",
                table: "Announcement",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Announcement_ContactInformation_ContactInformationId",
                table: "Announcement",
                column: "ContactInformationId",
                principalTable: "ContactInformation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnnouncementResponses_Announcement_AnnouncementId",
                table: "AnnouncementResponses",
                column: "AnnouncementId",
                principalTable: "Announcement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnnouncementToGroup_Announcement_AnnouncementsId",
                table: "AnnouncementToGroup",
                column: "AnnouncementsId",
                principalTable: "Announcement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EducationAnnouncements_Announcement_Id",
                table: "EducationAnnouncements",
                column: "Id",
                principalTable: "Announcement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkAnnouncements_Announcement_Id",
                table: "WorkAnnouncements",
                column: "Id",
                principalTable: "Announcement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
