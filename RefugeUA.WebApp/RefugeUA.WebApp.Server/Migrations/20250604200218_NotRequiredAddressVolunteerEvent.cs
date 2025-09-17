using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RefugeUA.WebApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class NotRequiredAddressVolunteerEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VolunteerEvents_Addresses_AddressId",
                table: "VolunteerEvents");

            migrationBuilder.AddForeignKey(
                name: "FK_VolunteerEvents_Addresses_AddressId",
                table: "VolunteerEvents",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VolunteerEvents_Addresses_AddressId",
                table: "VolunteerEvents");

            migrationBuilder.AddForeignKey(
                name: "FK_VolunteerEvents_Addresses_AddressId",
                table: "VolunteerEvents",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
