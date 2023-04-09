using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web2Unix.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeHostnameToServerNameInServerEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Hostname",
                table: "Servers",
                newName: "ServerName");

            migrationBuilder.UpdateData(
                table: "WebUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 4, 9, 10, 11, 29, 457, DateTimeKind.Unspecified).AddTicks(5613), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 4, 9, 10, 11, 29, 457, DateTimeKind.Unspecified).AddTicks(5624), new TimeSpan(0, 0, 0, 0, 0)) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ServerName",
                table: "Servers",
                newName: "Hostname");

            migrationBuilder.UpdateData(
                table: "WebUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 4, 3, 7, 25, 9, 145, DateTimeKind.Unspecified).AddTicks(6179), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 4, 3, 7, 25, 9, 145, DateTimeKind.Unspecified).AddTicks(6189), new TimeSpan(0, 0, 0, 0, 0)) });
        }
    }
}
