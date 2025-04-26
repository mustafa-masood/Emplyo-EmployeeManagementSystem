using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Emplyo_EMS.Server.Migrations
{
    /// <inheritdoc />
    public partial class super_admin_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "RoleName" },
                values: new object[,]
                {
                    { 1, "SuperAdmin" },
                    { 2, "HR" },
                    { 3, "Manager" },
                    { 4, "Employee" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreatedAt", "Email", "PasswordHash", "RoleId", "UpdatedAt", "Username" },
                values: new object[] { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "superadmin@emplyo.com", "$2a$11$xvJzqg6pM6ODxYOklq6y3OBZT6gZ.Wcf4wL5MSjNKn6kPZAVv4C4C", 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "superadmin" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "CreatedAt", "DepartmentId", "Email", "FirstName", "JoinDate", "LastName", "ManagerId", "Salary", "UpdatedAt", "UserId" },
                values: new object[] { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "superadmin@emplyo.com", "Super", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Admin", null, 0m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 1);
        }
    }
}
