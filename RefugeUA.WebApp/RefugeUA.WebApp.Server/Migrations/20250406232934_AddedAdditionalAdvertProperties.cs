using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RefugeUA.WebApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddedAdditionalAdvertProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DonationLink",
                table: "VolunteerEvents",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EventType",
                table: "VolunteerEvents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsClosed",
                table: "VolunteerEvents",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsClosed",
                table: "Advertisement",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DonationLink",
                table: "VolunteerEvents");

            migrationBuilder.DropColumn(
                name: "EventType",
                table: "VolunteerEvents");

            migrationBuilder.DropColumn(
                name: "IsClosed",
                table: "VolunteerEvents");

            migrationBuilder.DropColumn(
                name: "IsClosed",
                table: "Advertisement");
        }
    }
}
