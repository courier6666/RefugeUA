using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RefugeUA.WebApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddedCommunityAdminsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommunityAdmins");
        }
    }
}
