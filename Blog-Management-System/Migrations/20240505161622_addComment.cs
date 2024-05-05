using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class addComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "CommentId", "Body", "ForumId", "Like", "UserId" },
                values: new object[] { 1, "wtf..", 1, 2, 2 });

            migrationBuilder.UpdateData(
                table: "Forums",
                keyColumn: "ForumId",
                keyValue: 1,
                column: "Created_at",
                value: new DateTime(2024, 5, 5, 23, 16, 22, 222, DateTimeKind.Local).AddTicks(2064));

            migrationBuilder.UpdateData(
                table: "Forums",
                keyColumn: "ForumId",
                keyValue: 2,
                column: "Created_at",
                value: new DateTime(2024, 5, 5, 23, 16, 22, 222, DateTimeKind.Local).AddTicks(2075));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created_at",
                value: new DateTime(2024, 5, 5, 23, 16, 22, 222, DateTimeKind.Local).AddTicks(2094));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created_at",
                value: new DateTime(2024, 5, 5, 23, 16, 22, 222, DateTimeKind.Local).AddTicks(2095));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created_at",
                value: new DateTime(2024, 5, 5, 23, 16, 22, 222, DateTimeKind.Local).AddTicks(2096));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "CommentId",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "Forums",
                keyColumn: "ForumId",
                keyValue: 1,
                column: "Created_at",
                value: new DateTime(2024, 5, 5, 23, 14, 30, 704, DateTimeKind.Local).AddTicks(6469));

            migrationBuilder.UpdateData(
                table: "Forums",
                keyColumn: "ForumId",
                keyValue: 2,
                column: "Created_at",
                value: new DateTime(2024, 5, 5, 23, 14, 30, 704, DateTimeKind.Local).AddTicks(6477));

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
    }
}
