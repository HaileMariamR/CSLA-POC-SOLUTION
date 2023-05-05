using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data_Layer.Migrations
{
    /// <inheritdoc />
    public partial class migrationinit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    HireDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Salary = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserName = table.Column<string>(type: "TEXT", nullable: true),
                    Password = table.Column<string>(type: "TEXT", nullable: true),
                    Role = table.Column<string>(type: "TEXT", nullable: true),
                    RefreshToken = table.Column<string>(type: "TEXT", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "HireDate", "Name", "Salary" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 5, 4, 10, 22, 51, 342, DateTimeKind.Local).AddTicks(561), "Hailemariam", 234234234m },
                    { 2, new DateTime(2023, 5, 4, 10, 22, 51, 342, DateTimeKind.Local).AddTicks(593), "Fikadie", 234234234m },
                    { 3, new DateTime(2023, 5, 4, 10, 22, 51, 342, DateTimeKind.Local).AddTicks(596), "Harry", 234234234m }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "RefreshToken", "RefreshTokenExpiryTime", "Role", "UserName" },
                values: new object[,]
                {
                    { 4, "user1234", "", new DateTime(2023, 5, 4, 10, 22, 51, 342, DateTimeKind.Local).AddTicks(784), "user", "user" },
                    { 5, "admin1234", "", new DateTime(2023, 5, 4, 10, 22, 51, 342, DateTimeKind.Local).AddTicks(791), "admin", "admin" },
                    { 6, "siteadmin1234", "", new DateTime(2023, 5, 4, 10, 22, 51, 342, DateTimeKind.Local).AddTicks(794), "siteadmin", "siteadmin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
