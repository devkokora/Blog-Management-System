using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Blog_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class addModelForum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Forums",
                columns: new[] { "ForumId", "Body", "Created_at", "Like", "Title", "UserId" },
                values: new object[,]
                {
                    { 1, "hello", new DateTime(2024, 5, 5, 23, 14, 30, 704, DateTimeKind.Local).AddTicks(6469), 3, "I found alien last year", 1 },
                    { 2, "...", new DateTime(2024, 5, 5, 23, 14, 30, 704, DateTimeKind.Local).AddTicks(6477), 18, "Hello from another world", 2 }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created_at",
                value: new DateTime(2024, 5, 5, 23, 14, 30, 704, DateTimeKind.Local).AddTicks(6497));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created_at",
                value: new DateTime(2024, 5, 5, 23, 14, 30, 704, DateTimeKind.Local).AddTicks(6498));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created_at",
                value: new DateTime(2024, 5, 5, 23, 14, 30, 704, DateTimeKind.Local).AddTicks(6499));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Forums",
                keyColumn: "ForumId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Forums",
                keyColumn: "ForumId",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created_at",
                value: new DateTime(2024, 5, 5, 23, 13, 14, 90, DateTimeKind.Local).AddTicks(6785));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created_at",
                value: new DateTime(2024, 5, 5, 23, 13, 14, 90, DateTimeKind.Local).AddTicks(6793));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created_at",
                value: new DateTime(2024, 5, 5, 23, 13, 14, 90, DateTimeKind.Local).AddTicks(6794));
        }
    }
}
