using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RefugeUA.WebApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class DatabaeSchemaUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkAnnouncements_WorkCategory_WorkCategoryId",
                table: "WorkAnnouncements");

            migrationBuilder.DropIndex(
                name: "IX_Announcement_Title",
                table: "Announcement");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkCategory",
                table: "WorkCategory");

            migrationBuilder.RenameTable(
                name: "WorkCategory",
                newName: "WorkCategories");

            migrationBuilder.AlterColumn<string>(
                name: "CompanyName",
                table: "WorkAnnouncements",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(400)",
                oldMaxLength: 400,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkCategories",
                table: "WorkCategories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkAnnouncements_WorkCategories_WorkCategoryId",
                table: "WorkAnnouncements",
                column: "WorkCategoryId",
                principalTable: "WorkCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkAnnouncements_WorkCategories_WorkCategoryId",
                table: "WorkAnnouncements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkCategories",
                table: "WorkCategories");

            migrationBuilder.RenameTable(
                name: "WorkCategories",
                newName: "WorkCategory");

            migrationBuilder.AlterColumn<string>(
                name: "CompanyName",
                table: "WorkAnnouncements",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(400)",
                oldMaxLength: 400);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkCategory",
                table: "WorkCategory",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Announcement_Title",
                table: "Announcement",
                column: "Title",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkAnnouncements_WorkCategory_WorkCategoryId",
                table: "WorkAnnouncements",
                column: "WorkCategoryId",
                principalTable: "WorkCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
