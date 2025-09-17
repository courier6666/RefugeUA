using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RefugeUA.WebApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCommunityAdminTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommunityAdmins");

            migrationBuilder.AddColumn<string>(
                name: "District",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAccepted",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "District",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsAccepted",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "CommunityAdmins",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    District = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunityAdmins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommunityAdmins_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
