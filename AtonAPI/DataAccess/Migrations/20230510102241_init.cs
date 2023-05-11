using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Login = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Gender = table.Column<int>(type: "integer", nullable: false),
                    Birthday = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Admin = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: false),
                    RevokedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    RevokedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Admin", "Birthday", "CreatedBy", "CreatedOn", "Gender", "Login", "ModifiedBy", "ModifiedOn", "Name", "Password", "RevokedBy", "RevokedOn" },
                values: new object[] { new Guid("3f01c130-c035-4a2d-9e08-d512b46233e7"), true, null, "-", new DateTime(2023, 5, 10, 13, 22, 41, 147, DateTimeKind.Local).AddTicks(3115), 2, "admin", "-", new DateTime(2023, 5, 10, 13, 22, 41, 147, DateTimeKind.Local).AddTicks(3137), "admin", "$2a$11$berb9wh7PtUcJRZQUOLJZeVhaRkz9E22tG3RLm7pjoJB08o7U7XLq", null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
