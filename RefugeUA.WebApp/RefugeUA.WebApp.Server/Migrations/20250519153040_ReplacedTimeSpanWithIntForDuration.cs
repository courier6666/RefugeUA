using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RefugeUA.WebApp.Server.Migrations
{
    /// <inheritdoc/>
    /// Data is lost in duration column in this migration.
    public partial class ReplacedTimeSpanWithIntForDuration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
              @"ALTER TABLE dbo.EducationAnnouncements
                DROP COLUMN Duration;
                GO
                ALTER TABLE dbo.EducationAnnouncements
                ADD Duration INT NOT NULL
                CONSTRAINT [MAXV_EducationAnnouncements_Duration] CHECK (Duration <= 2191)
                GO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
              @"ALTER TABLE dbo.EducationAnnouncements
                DROP COLUMN Duration;
                GO
                ALTER TABLE dbo.EducationAnnouncements
                ADD Duration time NOT NULL
                GO");
        }
    }
}
