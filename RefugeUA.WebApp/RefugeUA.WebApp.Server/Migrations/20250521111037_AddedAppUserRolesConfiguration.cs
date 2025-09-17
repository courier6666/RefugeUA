using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RefugeUA.WebApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddedAppUserRolesConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AppRoleId",
                table: "AspNetUserRoles",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Discriminator",
                table: "AspNetRoles",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(21)",
                oldMaxLength: 21);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_AppRoleId",
                table: "AspNetUserRoles",
                column: "AppRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_AppRoleId",
                table: "AspNetUserRoles",
                column: "AppRoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_AppRoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_AppRoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "AppRoleId",
                table: "AspNetUserRoles");

            migrationBuilder.AlterColumn<string>(
                name: "Discriminator",
                table: "AspNetRoles",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(8)",
                oldMaxLength: 8);
        }
    }
}
