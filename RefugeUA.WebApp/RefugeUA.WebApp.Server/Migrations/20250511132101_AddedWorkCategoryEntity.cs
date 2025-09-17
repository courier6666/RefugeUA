using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RefugeUA.WebApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddedWorkCategoryEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "WorkCategoryId",
                table: "WorkAnnouncements",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "Duration",
                table: "EducationAnnouncements",
                type: "time",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<bool>(
                name: "IsFree",
                table: "AccomodationAnnouncements",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "AccomodationAnnouncements",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WorkCategory",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkCategory", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkAnnouncements_WorkCategoryId",
                table: "WorkAnnouncements",
                column: "WorkCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkAnnouncements_WorkCategory_WorkCategoryId",
                table: "WorkAnnouncements",
                column: "WorkCategoryId",
                principalTable: "WorkCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkAnnouncements_WorkCategory_WorkCategoryId",
                table: "WorkAnnouncements");

            migrationBuilder.DropTable(
                name: "WorkCategory");

            migrationBuilder.DropIndex(
                name: "IX_WorkAnnouncements_WorkCategoryId",
                table: "WorkAnnouncements");

            migrationBuilder.DropColumn(
                name: "WorkCategoryId",
                table: "WorkAnnouncements");

            migrationBuilder.DropColumn(
                name: "IsFree",
                table: "AccomodationAnnouncements");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "AccomodationAnnouncements");

            migrationBuilder.AlterColumn<string>(
                name: "Duration",
                table: "EducationAnnouncements",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time",
                oldMaxLength: 100);
        }
    }
}
