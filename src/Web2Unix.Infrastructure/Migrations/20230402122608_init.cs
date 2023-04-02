using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Web2Unix.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Servers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hostname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Port = table.Column<short>(type: "smallint", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WebRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WebUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WebUserRoles",
                columns: table => new
                {
                    WebUserId = table.Column<int>(type: "int", nullable: false),
                    WebRoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebUserRoles", x => new { x.WebUserId, x.WebRoleId });
                    table.ForeignKey(
                        name: "FK_WebUserRoles_WebRoles_WebRoleId",
                        column: x => x.WebRoleId,
                        principalTable: "WebRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WebUserRoles_WebUsers_WebUserId",
                        column: x => x.WebUserId,
                        principalTable: "WebUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "WebRoles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "SuperAdmin" },
                    { 2, "Admin" },
                    { 3, "User" }
                });

            migrationBuilder.InsertData(
                table: "WebUsers",
                columns: new[] { "Id", "CreatedAt", "Email", "Password", "UpdatedAt", "Username" },
                values: new object[] { 1, new DateTimeOffset(new DateTime(2023, 4, 2, 12, 26, 8, 34, DateTimeKind.Unspecified).AddTicks(9444), new TimeSpan(0, 0, 0, 0, 0)), "shparuk1996@gmail.com", "temppass", new DateTimeOffset(new DateTime(2023, 4, 2, 12, 26, 8, 34, DateTimeKind.Unspecified).AddTicks(9457), new TimeSpan(0, 0, 0, 0, 0)), "yaroslav" });

            migrationBuilder.InsertData(
                table: "WebUserRoles",
                columns: new[] { "WebRoleId", "WebUserId" },
                values: new object[] { 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_WebUserRoles_WebRoleId",
                table: "WebUserRoles",
                column: "WebRoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Servers");

            migrationBuilder.DropTable(
                name: "WebUserRoles");

            migrationBuilder.DropTable(
                name: "WebRoles");

            migrationBuilder.DropTable(
                name: "WebUsers");
        }
    }
}
