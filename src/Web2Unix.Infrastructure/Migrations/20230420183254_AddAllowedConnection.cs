using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web2Unix.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAllowedConnection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AllowedConnections",
                columns: table => new
                {
                    WebUserId = table.Column<int>(type: "int", nullable: false),
                    ServerId = table.Column<int>(type: "int", nullable: false),
                    FromIpAddress = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllowedConnections", x => new { x.WebUserId, x.ServerId });
                    table.ForeignKey(
                        name: "FK_AllowedConnections_Servers_ServerId",
                        column: x => x.ServerId,
                        principalTable: "Servers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AllowedConnections_WebUsers_WebUserId",
                        column: x => x.WebUserId,
                        principalTable: "WebUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AllowedConnections",
                columns: new[] { "ServerId", "WebUserId", "FromIpAddress" },
                values: new object[] { 1, 1, "127.0.0.1" });

            migrationBuilder.UpdateData(
                table: "WebUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 4, 20, 18, 32, 54, 860, DateTimeKind.Unspecified).AddTicks(2419), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 4, 20, 18, 32, 54, 860, DateTimeKind.Unspecified).AddTicks(2434), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.CreateIndex(
                name: "IX_AllowedConnections_ServerId",
                table: "AllowedConnections",
                column: "ServerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllowedConnections");

            migrationBuilder.UpdateData(
                table: "WebUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 4, 9, 10, 11, 29, 457, DateTimeKind.Unspecified).AddTicks(5613), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 4, 9, 10, 11, 29, 457, DateTimeKind.Unspecified).AddTicks(5624), new TimeSpan(0, 0, 0, 0, 0)) });
        }
    }
}
