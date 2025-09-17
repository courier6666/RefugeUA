using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RefugeUA.WebApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddressIdNullVolunteerEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VolunteerEvents_AddressId",
                table: "VolunteerEvents");

            migrationBuilder.AlterColumn<long>(
                name: "AddressId",
                table: "VolunteerEvents",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerEvents_AddressId",
                table: "VolunteerEvents",
                column: "AddressId",
                unique: true,
                filter: "[AddressId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VolunteerEvents_AddressId",
                table: "VolunteerEvents");

            migrationBuilder.AlterColumn<long>(
                name: "AddressId",
                table: "VolunteerEvents",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerEvents_AddressId",
                table: "VolunteerEvents",
                column: "AddressId",
                unique: true);
        }
    }
}
